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
                    errorText += $"{error.ErrorText}\r\n";
                    errorsFound.Add(error.ErrorText);
                }
            }
            else
            {
                Type typeToParse = null;

                foreach (Type foundType in results.CompiledAssembly.ExportedTypes)
                {
                    if(foundType.FullName == "Main")
                    {
                        typeToParse = foundType;
                    }
                }

                int currentFileOffset = 0;

                ParseCustomType(typeToParse, ref currentFileOffset, fileSpec.File.Properties);
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
                                            break;
                                        case Spec.ValueType.eUnsignedInt32:
                                            arrayLength = (int)valueSpec.GetAsUint32();
                                            break;
                                        case Spec.ValueType.eInt64:
                                            arrayLength = (int)valueSpec.GetAsInt64();
                                            break;
                                        case Spec.ValueType.eUnsignedInt64:
                                            arrayLength = (int)valueSpec.GetAsUint64();
                                            break;
                                        case Spec.ValueType.eChar:
                                            arrayLength = (int)valueSpec.GetAsChar();
                                            break;
                                        case Spec.ValueType.eString:
                                        case Spec.ValueType.eCustom:
                                        case Spec.ValueType.eFloat:
                                        case Spec.ValueType.eDouble:
                                        default:
                                            // error, incompatible or unknown type
                                            break;
                                    }
                                }
                            }
                        }
                        else
                        {
                            // errors
                        }
                    }
                    else
                    {
                        // warning about unknown attributes
                    }
                }
            }

            return arrayLength;
        }
    }
}
