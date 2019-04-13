using binary_viewer.Spec;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace binary_viewer.Script
{
    public class ScriptConsumerCSharp : IScriptConsumer
    {
        private static string mainEntryName = "Main";

        private string scriptText = string.Empty;
        private FileSpec fileSpec = null;
        private string errorText = string.Empty;
        private List<string> errorsFound = new List<string>();
        private byte[] fileBuffer = null;

        string IScriptConsumer.ErrorOutput
        {
            get { return errorText; }
            set { errorText = value; }
        }

        List<string> IScriptConsumer.FoundErrors
        {
            get { return errorsFound; }
        }
        
        public bool HasErrors
        {
            get { return errorsFound.Count > 0; }
        }

        public List<string> Errors
        {
            get { return errorsFound; }
        }
        
        public ScriptConsumerCSharp(string scriptToConsume, FileSpec fileSpecToPopulate)
        {
            scriptText = scriptToConsume;
            fileSpec = fileSpecToPopulate;
            fileBuffer = fileSpecToPopulate.FileBuffer;
        }

        void IScriptConsumer.ParseScript()
        {
            // clear up error output
            errorsFound.Clear();
            errorText = string.Empty;

            // create our compiler + includes and compile
            CodeDomProvider csc = CodeDomProvider.CreateProvider("CSharp");
            CompilerParameters parameters = new CompilerParameters(new[] { "System.Core.dll", "binary_viewer.exe" }, "", true);

            parameters.GenerateInMemory = true;
            parameters.GenerateExecutable = false;

            CompilerResults results = csc.CompileAssemblyFromSource(parameters, scriptText);
            
            // process result - output errors or populate the file spec
            if (results.Errors.HasErrors)
            {
                errorText = string.Empty;

                IEnumerable<CompilerError> foundErrors = results.Errors.Cast<CompilerError>();

                foreach(CompilerError error in foundErrors)
                {
                    WriteNewError(error.ErrorText);
                }
            }
            else
            {
                Type typeToParse = null;

                // look for the main entry point and parse it
                foreach (Type foundType in results.CompiledAssembly.ExportedTypes)
                {
                    if(foundType.FullName == mainEntryName)
                    {
                        if (typeToParse == null)
                        {
                            typeToParse = foundType;
                        }
                        else
                        {
                            WriteNewError($"More than one \"{mainEntryName}\" was found in the file spec. You must only have 1.");
                            break;
                        }
                    }
                }

                if (typeToParse == null)
                {
                    WriteNewError($"No class called \"{mainEntryName}\" could be found. There must be at least 1 as the main entry point for the script.");
                }
                else if (!HasErrors)
                {
                    int currentFileOffset = 0;

                    ParseCustomType(typeToParse, ref currentFileOffset, fileSpec.File.Properties);
                }
            }
        }

        private void ParseCustomType(Type customType, ref int currentFileOffset, List<PropertySpec> listToAddTo)
        {
            foreach (FieldInfo foundField in customType.GetFields())
            {
                IList<CustomAttributeData> customAttributes = foundField.CustomAttributes.ToList();

                if (foundField.FieldType == typeof(int))
                {
                    AddNewProperty(Spec.ValueType.eInt32, foundField.Name, ref currentFileOffset, 4, customAttributes, listToAddTo);
                }
                else if (foundField.FieldType == typeof(uint))
                {
                    AddNewProperty(Spec.ValueType.eUnsignedInt32, foundField.Name, ref currentFileOffset, 4, customAttributes, listToAddTo);
                }
                else if (foundField.FieldType == typeof(long))
                {
                    AddNewProperty(Spec.ValueType.eInt64, foundField.Name, ref currentFileOffset, 8, customAttributes, listToAddTo);
                }
                else if (foundField.FieldType == typeof(ulong))
                {
                    AddNewProperty(Spec.ValueType.eUnsignedInt64, foundField.Name, ref currentFileOffset, 8, customAttributes, listToAddTo);
                }
                else if (foundField.FieldType == typeof(float))
                {
                    AddNewProperty(Spec.ValueType.eFloat, foundField.Name, ref currentFileOffset, 4, customAttributes, listToAddTo);
                }
                else if (foundField.FieldType == typeof(double))
                {
                    AddNewProperty(Spec.ValueType.eDouble, foundField.Name, ref currentFileOffset, 8, customAttributes, listToAddTo);
                }
                else if (foundField.FieldType == typeof(char))
                {
                    AddNewProperty(Spec.ValueType.eChar, foundField.Name, ref currentFileOffset, 1, customAttributes, listToAddTo);
                }
                else
                {
                    StructSpec newTypeStruct = new StructSpec(fileBuffer, currentFileOffset);
                    newTypeStruct.Name = foundField.Name;
                    listToAddTo.Add(newTypeStruct);
                    ParseCustomType(foundField.FieldType, ref currentFileOffset, newTypeStruct.Properties);
                }
            }
        }

        private void AddNewProperty(
            Spec.ValueType type
            , string name
            , ref int currentFileOffset
            , int lengthOfType
            , IList<CustomAttributeData> fieldAttributes
            , List<PropertySpec> listToAddTo)
        {

            // this is the place to determine the array dimensions and pointer-ness.
            // use an OffsetPointerSpec for pointers. Have 4 pointer types.
            // at a point in the future could possibly expand the types to be a general number of bytes/bits and we can keep track of how far threw
            // the file we are....but we don't do file streaming yet, so probably don't need anything capable of counting that high just yet.

            OffsetPointerSpec nextPointer = null; // points to the newest pointer we've made in the chain

            for( int i = 0; i < fieldAttributes.Count; ++i)
            {
                CustomAttributeData customAttribute = fieldAttributes[i];

                if (customAttribute.AttributeType == typeof(BnPointer))
                {
                    if(customAttribute.ConstructorArguments.Count != 1)
                    {
                        // don't know if this is necessary! THe c# compiler should do this for us!
                        WriteNewError( $"Custom attribute {customAttribute.ToString()} found with an incorrect number of arguments. There should only be 1 argument." );
                        return;
                    }

                    PointerType pointerType = (PointerType)customAttribute.ConstructorArguments[0].Value;

                    switch (pointerType)
                    {
                        case PointerType.ePointer32:
                            //calculate a new offset, using the current pointer value, if the pointer is already populated
                            int fileOffsetToUse = nextPointer == null ? currentFileOffset : nextPointer.OffsetIntoFileOfTarget;
                            OffsetPointerSpec newPointer = new OffsetPointerSpec(fileBuffer, fileOffsetToUse, 0);
                            newPointer.Name = name;

                            // add it to the "linked list" of pointers
                            if (nextPointer != null)
                            {
                                nextPointer.Target = newPointer;
                            }

                            nextPointer = newPointer;
                            
                            // only increment the file offset on the first pointer as any pointers to pointers are navigating the file manually
                            if (i == 0) { currentFileOffset += 4; }
                            break;
                        /*case PointerType.ePointer64:
                            nextPointer = new OffsetPointerSpec(fileBuffer, currentFileOffset, 0);
                            currentFileOffset += 8;
                            break;
                        case PointerType.eOffset32:
                            nextPointer = new OffsetPointerSpec(fileBuffer, currentFileOffset, currentFileOffset);
                            currentFileOffset += 4;
                            break;
                        case PointerType.eOffset64:
                            nextPointer = new OffsetPointerSpec(fileBuffer, currentFileOffset, currentFileOffset);
                            currentFileOffset += 8;
                            break;*/
                        case PointerType.eNotAPointer:
                        default:
                            WriteNewError($"Invalid pointer type: {pointerType}");
                            break;
                    }
                }
            }

            int offSetForStartOfValue = nextPointer == null ? currentFileOffset : nextPointer.OffsetIntoFileOfTarget;

            int arrayLength = 0;//GetArrayLength(fieldAttributes, listToAddTo);

            if(arrayLength > 0)
            {
                ArraySpec array = new ArraySpec(fileBuffer, offSetForStartOfValue, arrayLength);
                array.Name = name;

                int arrayTotalLength = 0;

                for(int i = 0; i < arrayLength; ++i)
                {
                    ValueSpec value = new ValueSpec(fileBuffer, offSetForStartOfValue);
                    value.TypeOfValue = type;
                    value.LengthOfType = lengthOfType;
                    value.Name = name;

                    array.PropertyArray[i] = value;
                    
                    arrayTotalLength += lengthOfType;
                }

                if (nextPointer == null)
                {
                    currentFileOffset += arrayTotalLength;
                    listToAddTo.Add(array);
                }
                else
                {
                    // if we're the child of a pointer, then don't update the file offset as it's already done
                    nextPointer.Target = array;
                    listToAddTo.Add(nextPointer);
                }
            }
            else
            {
                ValueSpec value = new ValueSpec(fileBuffer, offSetForStartOfValue);
                value.TypeOfValue = type;
                value.LengthOfType = lengthOfType;
                value.Name = name;
                
                if (nextPointer == null)
                {
                    currentFileOffset += lengthOfType;
                    listToAddTo.Add(value);
                }
                else
                {
                    // if we're the child of a pointer, then don't update the file offset as it's already done
                    nextPointer.Target = value;
                    listToAddTo.Add(nextPointer);
                }
            }
        }

        private int GetArrayLength(IList<CustomAttributeData> fieldAttributes, List<PropertySpec> currentMembers)
        {
            int arrayLength = 0;

            if(fieldAttributes.Count > 0)
            {
                if(fieldAttributes.Count > 1)
                {
                    WriteNewError("There are too many attributes on a variable. You should only have a maximum of 1 array attribute on any member. Only the last one will be used.");
                }

                foreach(CustomAttributeData attribute in fieldAttributes)
                {
                    if( attribute.AttributeType == typeof(BnArray))
                    {
                        CustomAttributeTypedArgument argument = attribute.ConstructorArguments[0];
                        Type argumentType = argument.ArgumentType;

                        if (argumentType == typeof(int))
                        {
                            // read off the array length directly from the attribute
                            arrayLength = (int)argument.Value;
                        }
                        else if (argumentType == typeof(string))
                        {
                            string variableName = (string)argument.Value;

                            // look for the named variable and read its value
                            foreach (PropertySpec spec in currentMembers)
                            {
                                if (spec.GetType() == typeof(ValueSpec) && spec.Name == variableName)
                                {
                                    ValueSpec valueSpec = (ValueSpec)spec;
                                    switch (valueSpec.TypeOfValue)
                                    {
                                        case Spec.ValueType.eInt32:
                                            arrayLength = valueSpec.GetAsInt32();
                                            // error on negative value
                                            break;
                                        case Spec.ValueType.eUnsignedInt32:
                                            arrayLength = (int)valueSpec.GetAsUint32();
                                            break;
                                        case Spec.ValueType.eInt64:
                                            // error on negative value
                                            arrayLength = (int)valueSpec.GetAsInt64();
                                            break;
                                        case Spec.ValueType.eUnsignedInt64:
                                            arrayLength = (int)valueSpec.GetAsUint64();
                                            break;
                                        case Spec.ValueType.eChar:
                                            // error on negative value
                                            arrayLength = (int)valueSpec.GetAsChar();
                                            break;
                                        case Spec.ValueType.eString:
                                        case Spec.ValueType.eCustom:
                                        case Spec.ValueType.eFloat:
                                        case Spec.ValueType.eDouble:
                                        default:
                                            // error, incompatible or unknown type
                                            WriteNewError($"Attempting to use an incompatible value \'{valueSpec.Name}\' of type \'{valueSpec.TypeOfValue.ToString()}\' as an array length. Ensure type is an integer type");
                                            break;
                                    }
                                }
                            }
                        }
                        else
                        {
                            // the c# compiler should've caught this for us, but just in case it doesn't, add an error
                            WriteNewError("Array attribute found with invalid constructor arguments.");
                        }
                    }
                    else
                    {
                        // this is a catch all for when we add more attribute types, or if they try to use the build in C# ones
                        // they're only allowed to use our BnArray for arrays.
                        WriteNewError($"An unknown or invalid attribute type \'{attribute.AttributeType}\' was found in the script.");
                    }
                }
            }

            return arrayLength;
        }

        private void WriteNewError(string error)
        {
            errorText += $"{error}\r\n";
            errorsFound.Add(error);
        }
    }
}
