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
            int arrayLength = GetArrayLength(fieldAttributes, listToAddTo);

            if(arrayLength > 0)
            {
                ArraySpec array = new ArraySpec(fileBuffer, currentFileOffset, arrayLength);
                array.Name = name;

                for(int i = 0; i < arrayLength; ++i)
                {
                    ValueSpec value = new ValueSpec(fileBuffer, currentFileOffset);
                    value.TypeOfValue = type;
                    value.LengthOfType = lengthOfType;
                    value.Name = name;

                    array.PropertyArray[i] = value;

                    currentFileOffset += lengthOfType;
                }

                listToAddTo.Add(array);
            }
            else
            {
                ValueSpec value = new ValueSpec(fileBuffer, currentFileOffset);
                value.TypeOfValue = type;
                value.LengthOfType = lengthOfType;
                value.Name = name;

                listToAddTo.Add(value);

                currentFileOffset += lengthOfType;
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
