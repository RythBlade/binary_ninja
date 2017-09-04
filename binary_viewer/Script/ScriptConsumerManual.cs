using binary_viewer.Spec;
using System;
using System.Collections.Generic;

namespace binary_viewer.Script
{
    public enum PointerType
    {
        ePointer32
        , ePointer64
        , eOffset32
        , eOffset64
        , eNotAPointer
    }

    public class ScriptConsumerManual : IScriptConsumer
    {
        private FileSpec fileSpec;

        //////////
        // Script Parsing
        /////////
        private string scriptText;

        /// <summary>
        /// Hints
        /// </summary>
        private List<Tuple<string, int>> foundStructs;

        private string scriptFileName;
        private int startOfFileTagInScript;

        /// <summary>
        /// Parse Stack + offsets
        /// </summary>
        private Stack<StackContents> parseStack;
        private byte[] fileBuffer;
        //private int currentOffsetIntoFileSpec;

        private int ScriptLocation
        {
            get { return parseStack.Count > 0 ? parseStack.Peek().ScriptLocation : -1; }
            set { if (parseStack.Count > 0) { parseStack.Peek().ScriptLocation = value; } }
        }

        public string ErrorOutput { get; set; }

        public ScriptConsumerManual(string scriptToConsume, FileSpec fileSpecToPopulate)
        {
            fileSpec = fileSpecToPopulate;
            scriptText = scriptToConsume;
            foundStructs = new List<Tuple<string, int>>();
            scriptFileName = string.Empty;
            startOfFileTagInScript = 0;
            parseStack = new Stack<StackContents>();
            fileBuffer = fileSpecToPopulate.FileBuffer;
            ErrorOutput = string.Empty;

            // the file is the highest level of the stack. When the stack is empty - we're done with the file!
            StackContents newLevel = new StackContents();
            newLevel.CurrentStruct = fileSpecToPopulate.File;
            newLevel.ScriptLocation = 0;
            newLevel.CurrentOffsetIntoFileSpec = fileSpecToPopulate.IndexOfFirstByte;
            newLevel.OffsetIntoFileSpecStackStarts = fileSpecToPopulate.IndexOfFirstByte;
            parseStack.Push(newLevel);
        }

        public void ParseScript()
        {
            CleanString();

            ReadToFileStartTag();

            ReadPastWhiteSpace();

            ReadIntoScope();

            while (ScriptLocation < scriptText.Length)
            {
                ReadPastWhiteSpace();

                if(scriptText[ScriptLocation] == '}')
                {
                    if(parseStack.Peek().ThisIsAnArray)
                    {
                        // start by grabbing the array we've just populated
                        StructSpec structSpecToInstance = parseStack.Peek().CurrentStruct;
                        ArraySpec targetArray = parseStack.Peek().TheArray;

                        targetArray.PropertyArray[0] = structSpecToInstance;

                        // now clone it and add it to the target array (don't need to worry about adding it to the parent struct - the array is already there.
                        for(int i = 1; i < parseStack.Peek().ArrayLength; ++i)
                        {
                            targetArray.PropertyArray[i] = structSpecToInstance.Clone(fileBuffer, parseStack.Peek().CurrentOffsetIntoFileSpec);
                            parseStack.Peek().CurrentOffsetIntoFileSpec += targetArray.PropertyArray[i].LengthInBytes;
                        }
                    }

                    // ok - we're done with this scope level, so pop it off the stack
                    parseStack.Pop();

                    // is the stack completely empty
                    if (parseStack.Count <= 0)
                    {
                        // we're done!!!!! \o/
                        break;
                    }
                }
                else
                {
                    ReadVariable();
                }
            }
        }

        private void ReadPastWhiteSpace()
        {
            while(ScriptLocation < scriptText.Length)
            {
                if(IsWhiteSpaceCharacter(scriptText[ScriptLocation]))
                {
                    ++ScriptLocation;
                }
                else
                {
                    break;
                }
            }
        }

        private int GetArrayLengthFromVariableName(string variableName)
        {
            int arrayLength = -1;

            int variableNameLength = variableName.Length;

            int positionInString = 0;

            int startArrayLength = -1;
            int endOfArrayLength = -1;

            while (positionInString < variableNameLength)
            {
                if (variableName[positionInString] == '[')
                {
                    startArrayLength = positionInString + 1;
                }
                else if (variableName[positionInString] == ']')
                {
                    // end of array length
                    endOfArrayLength = positionInString - 1;
                }
                else if (variableName[positionInString] == ';')
                {
                    ////////////////////////////////////////////////
                    // Appropriate errors and warnings!
                }

                ++positionInString;
            }

            if(startArrayLength != -1 && endOfArrayLength != -1)
            {
                string arrayLengthString = variableName.Substring(startArrayLength, endOfArrayLength - startArrayLength + 1);

                ////////////////////////////////////////////////
                // Appropriate errors and warnings!
                // array length needs work!!!

                bool foundTheValue = false;

                foreach (ValueSpec scopedValue in parseStack.Peek().ScopedProperties)
                {
                    if(scopedValue.Name == arrayLengthString)
                    {
                        switch (scopedValue.TypeOfValue)
                        {
                            
                            case Spec.ValueType.eInt32:
                                arrayLength = scopedValue.GetAsInt32();
                                foundTheValue = true;
                                break;
                            case Spec.ValueType.eUnsignedInt32:
                                arrayLength = (int)scopedValue.GetAsUint32();
                                foundTheValue = true;
                                break;
                            case Spec.ValueType.eInt64:
                                arrayLength = (int)scopedValue.GetAsInt64();
                                foundTheValue = true;
                                WriteErrorString($"The variable {scopedValue.Name} was used as an array length but may have lost precision as we only support 4 byte integer lengths and this is an 8 byte integer");
                                break;
                            case Spec.ValueType.eUnsignedInt64:
                                arrayLength = (int)scopedValue.GetAsUint64();
                                foundTheValue = true;
                                WriteErrorString($"The variable {scopedValue.Name} was used as an array length but may have lost precision as we only support 4 byte integer lengths and this is an 8 byte unsigned integer");
                                break;
                            default:
                                WriteErrorString($"The variable {scopedValue.Name} was used as an array length and is of type {scopedValue.GetType().ToString()} which is not an integer");
                                break;
                        }
                    }
                }

                if (!foundTheValue)
                {
                    arrayLength = int.Parse(arrayLengthString);
                }
            }
            else if(startArrayLength != -1 || endOfArrayLength != -1)
            {
                WriteErrorString($"Incomplete array definition found: '{variableName}', on line: {CalculateLineNumber(scriptText, 0, ScriptLocation)}");
            }

            return arrayLength;
        }

        private PointerType GetPointerTypeFromText( string potentialPointerType)
        {
            PointerType typeToReturn = PointerType.eNotAPointer;

            if (string.Compare(potentialPointerType, "pointer32") == 0)
            {
                typeToReturn = PointerType.ePointer32;
            }
            else if (string.Compare(potentialPointerType, "pointer64") == 0)
            {
                typeToReturn = PointerType.ePointer64;
            }
            else if (string.Compare(potentialPointerType, "offset32") == 0)
            {
                typeToReturn = PointerType.eOffset32;
            }
            else if (string.Compare(potentialPointerType, "offset64") == 0)
            {
                typeToReturn = PointerType.eOffset64;
            }

            return typeToReturn;
        }

        private void ReadVariable()
        {
            // we should already be past the white space so just get on with reading the variable

            string pointerTypeString = ReadNextWord();
            PointerType pointerType = GetPointerTypeFromText(pointerTypeString);

            if(pointerType != PointerType.eNotAPointer)
            {
                ReadPastWhiteSpace();
            }

            // if it's not a pointer - then this is the type string, so copy it.
            // if it was a pointer - then read the variable type in
            string variableType = pointerType == PointerType.eNotAPointer ? pointerTypeString : ReadNextWord();
            ReadPastWhiteSpace();
            string variableName = ReadNextWord();
            
            // variable name MUST be followed by ';'
            if(scriptText[ScriptLocation - 1] == ';')
            {
                variableName = variableName.Substring(0, variableName.Length - 1);
            }
            else
            {
                ////////////////////////////////////////
                // some error!!
                ////////////////////////////////////////
                WriteErrorString($"Variable name {variableName} wasn't followed by a ';' on line: {CalculateLineNumber(scriptText, 0, ScriptLocation)}");
            }

            ReadPastWhiteSpace();

            Spec.ValueType type = GetIntrinsicTypeFromText(variableType);

            int arrayLength = GetArrayLengthFromVariableName(variableName);

            int fileOffsetPosition = parseStack.Peek().CurrentOffsetIntoFileSpec;

            OffsetPointerSpec newPointer = null;
            
            switch (pointerType)
            {
                case PointerType.ePointer32:
                    newPointer = new OffsetPointerSpec(fileBuffer, fileOffsetPosition, 0);
                    parseStack.Peek().CurrentOffsetIntoFileSpec += 4;
                    break;
                case PointerType.ePointer64:
                    newPointer = new OffsetPointerSpec(fileBuffer, fileOffsetPosition, 0);
                    parseStack.Peek().CurrentOffsetIntoFileSpec += 8;
                    break;
                case PointerType.eOffset32:
                    newPointer = new OffsetPointerSpec(fileBuffer, fileOffsetPosition, parseStack.Peek().OffsetIntoFileSpecStackStarts);
                    parseStack.Peek().CurrentOffsetIntoFileSpec += 4;
                    break;
                case PointerType.eOffset64:
                    newPointer = new OffsetPointerSpec(fileBuffer, fileOffsetPosition, parseStack.Peek().OffsetIntoFileSpecStackStarts);
                    parseStack.Peek().CurrentOffsetIntoFileSpec += 8;
                    break;
                case PointerType.eNotAPointer:
                    // carry on from where we got to!
                    newPointer = null;
                    fileOffsetPosition = parseStack.Peek().CurrentOffsetIntoFileSpec;
                    break;
                default:
                    WriteErrorString($"A found pointer '{pointerTypeString}' type was unknown on line: {CalculateLineNumber(scriptText, 0, ScriptLocation)}");
                    newPointer = null;
                    fileOffsetPosition = parseStack.Peek().CurrentOffsetIntoFileSpec;
                    break;
            }

            if (newPointer != null)
            {
                newPointer.Name = variableName;

                fileOffsetPosition = newPointer.OffsetIntoFileOfTarget;

                parseStack.Peek().CurrentStruct.Properties.Add(newPointer);
            }

            if (type == Spec.ValueType.eCustom)
            {
                // unknown intrinsic types mean it's a custom user type (struct)
                // find and parse it
                bool foundStruct = false;

                foreach(Tuple<string, int> tuple in foundStructs)
                {
                    // is this the one we want?
                    if (string.Compare(tuple.Item1, variableType) == 0)
                    {
                        StructSpec newStruct = new StructSpec(fileBuffer, fileOffsetPosition);
                        newStruct.Name = variableName;
                        // don't increase the file buffer offset as a struct isn't actually any data!

                        if(arrayLength != -1) // this is an array - so do some shenanigans
                        {
                            // add the struct as a child of the current scope, set the script index to the start of the struct
                            // and then push it onto the stack (the main loop will read through and instance one of these structs
                            // and on completion will duplicate it to populate the array.)
                            ArraySpec arraySpec = new ArraySpec(fileBuffer, fileOffsetPosition, arrayLength);
                            arraySpec.Name = variableName;

                            if (pointerType == PointerType.eNotAPointer)
                            {
                                parseStack.Peek().CurrentStruct.Properties.Add(arraySpec);
                            }
                            else
                            {
                                newPointer.Target = arraySpec;
                            }
                            
                            StackContents newStackContent = new StackContents();
                            newStackContent.CurrentStruct = newStruct;
                            newStackContent.ScriptLocation = tuple.Item2; // adjust the script pointer
                            newStackContent.ThisIsAnArray = true;
                            newStackContent.ArrayLength = arrayLength;
                            newStackContent.TheArray = arraySpec;
                            newStackContent.OffsetIntoFileSpecStackStarts = fileOffsetPosition;
                            newStackContent.CurrentOffsetIntoFileSpec = fileOffsetPosition;
                            parseStack.Push(newStackContent);
                        }
                        else
                        {
                            // add the struct as a child of the current scope, set the script index to the start of the struct
                            // and then push it onto the stack
                            if (pointerType == PointerType.eNotAPointer)
                            {
                                parseStack.Peek().CurrentStruct.Properties.Add(newStruct);
                            }
                            else
                            {
                                newPointer.Target = newStruct;
                            }

                            StackContents newStackContent = new StackContents();
                            newStackContent.CurrentStruct = newStruct;
                            newStackContent.ScriptLocation = tuple.Item2; // adjust the script pointer
                            newStackContent.ThisIsAnArray = false;
                            newStackContent.ArrayLength = arrayLength;
                            newStackContent.OffsetIntoFileSpecStackStarts = fileOffsetPosition;
                            newStackContent.CurrentOffsetIntoFileSpec = fileOffsetPosition;
                            parseStack.Push(newStackContent);
                        }
                        
                        // read into the scope like the main parser does, ready for the main loop to take over!
                        ReadPastWhiteSpace();

                        ReadIntoScope();

                        foundStruct = true;

                        break;
                    }
                }

                if( !foundStruct)
                {
                    WriteErrorString($"Line: {CalculateLineNumber(scriptText, 0, ScriptLocation)} Counldn't fine specified struct: {variableType}");
                }
            }
            else
            {
                int lengthOfType = 0;

                switch (type)
                {
                    case Spec.ValueType.eFloat:
                        lengthOfType = 4;
                        break;
                    case Spec.ValueType.eDouble:
                        lengthOfType = 8;
                        break;
                    case Spec.ValueType.eInt32:
                        lengthOfType = 4;
                        break;
                    case Spec.ValueType.eUnsignedInt32:
                        lengthOfType = 4;
                        break;
                    case Spec.ValueType.eInt64:
                        lengthOfType = 8;
                        break;
                    case Spec.ValueType.eUnsignedInt64:
                        lengthOfType = 8;
                        break;
                    case Spec.ValueType.eChar:
                        lengthOfType = 1;
                        break;
                    case Spec.ValueType.eString:
                        break;
                    case Spec.ValueType.eCustom:
                        break;
                    default:
                        break;
                }
                
                if (arrayLength == -1)
                {

                    ValueSpec newValueSpec = new ValueSpec(fileBuffer, fileOffsetPosition);
                    newValueSpec.Name = variableName;
                    newValueSpec.LengthOfType = lengthOfType;
                    newValueSpec.TypeOfValue = type;
                    
                    // if we're pointing at a different part of the file, don't advance the file pointer along!
                    if (pointerType == PointerType.eNotAPointer)
                    {
                        parseStack.Peek().CurrentStruct.Properties.Add(newValueSpec);
                        parseStack.Peek().CurrentOffsetIntoFileSpec += lengthOfType;
                    }
                    else
                    {
                        // if a pointer - make the array a child of the pointer
                        newPointer.Target = newValueSpec;
                    }

                    parseStack.Peek().ScopedProperties.Add(newValueSpec);
                }
                else
                {
                    ArraySpec arraySpec = new ArraySpec(fileBuffer, fileOffsetPosition, arrayLength);
                    arraySpec.Name = variableName;

                    // if not a pointer, add it to the current scope
                    if (pointerType == PointerType.eNotAPointer)
                    {
                        parseStack.Peek().CurrentStruct.Properties.Add(arraySpec);
                    }
                    else
                    {
                        // if a pointer - make the array a child of the pointer
                        newPointer.Target = arraySpec;
                    }

                    for (int i = 0; i < arrayLength; ++i)
                    {
                        ValueSpec newValueSpec = new ValueSpec(fileBuffer, fileOffsetPosition);
                        int arrayOfFirstArrayBracket = variableName.IndexOf('[');
                        newValueSpec.Name = $"{variableName.Substring(0, arrayOfFirstArrayBracket)}[{i}]";
                        newValueSpec.LengthOfType = lengthOfType;
                        newValueSpec.TypeOfValue = type;
                        arraySpec.PropertyArray[i] = newValueSpec;

                        // advance the appropriate file pointer based on the pointer type
                        if (pointerType == PointerType.eNotAPointer)
                        {
                            parseStack.Peek().CurrentOffsetIntoFileSpec += lengthOfType;
                        }
                        else
                        {
                            fileOffsetPosition += lengthOfType;
                        }
                    }
                }
            }
        }

        private Spec.ValueType GetIntrinsicTypeFromText(string nameToDetermineTypeOf)
        {
            Spec.ValueType typeToReturn = Spec.ValueType.eCustom;

            if(string.Compare( nameToDetermineTypeOf, "float") == 0)
            {
                typeToReturn = Spec.ValueType.eFloat;
            }
            else if (string.Compare(nameToDetermineTypeOf, "double") == 0)
            {
                typeToReturn = Spec.ValueType.eDouble;
            }
            else if (string.Compare(nameToDetermineTypeOf, "int32") == 0)
            {
                typeToReturn = Spec.ValueType.eInt32;
            }
            else if (string.Compare(nameToDetermineTypeOf, "uint32") == 0)
            {
                typeToReturn = Spec.ValueType.eUnsignedInt32;
            }
            else if (string.Compare(nameToDetermineTypeOf, "int64") == 0)
            {
                typeToReturn = Spec.ValueType.eInt64;
            }
            else if (string.Compare(nameToDetermineTypeOf, "uint64") == 0)
            {
                typeToReturn = Spec.ValueType.eUnsignedInt64;
            }
            else if (string.Compare(nameToDetermineTypeOf, "char") == 0)
            {
                typeToReturn = Spec.ValueType.eChar;
            }

            return typeToReturn;
        }

        private string ReadNextWord()
        {
            string stringToReturn = string.Empty;
            int startOfWord = ScriptLocation;

            while(ScriptLocation < scriptText.Length)
            {
                //////////////////////////////////////////
                // possibly some errors in here about illegal characters in the words! (e.g. punctuation)
                //////////////////////////////////////////
                if (!IsWhiteSpaceCharacter(scriptText[ScriptLocation]))
                {
                    ++ScriptLocation;
                }
                else
                {
                    // we're now on the first character after the word so break out
                    break;
                }
            }

            stringToReturn = scriptText.Substring(startOfWord, ScriptLocation - startOfWord);
            
            return stringToReturn;
        }

        private void ReadIntoScope()
        {
            if (scriptText[ScriptLocation] == '{') // ok - this should be the open brace of the file
            {
                ++ScriptLocation;

                // we're on the first character of scope - so break out
                return;
            }
            else
            {
                WriteErrorString($"Line: {CalculateLineNumber(scriptText, 0, ScriptLocation)} Expected a '{{'");
                //////////////////////////////////////////////////////////////////////////
                // Error here about unexpected character
                //////////////////////////////////////////////////////////////////////////
            }
        }

        private void CleanString()
        {
            scriptText = scriptText.Trim();
        }

        private void ReadToFileStartTag()
        {
            // find the 
            while(ScriptLocation < scriptText.Length)
            {
                // only 'struct' or 'file' are valid at the start of the file
                if(scriptText[ScriptLocation] == 's')
                {
                    string definitionName = string.Empty;

                    if (ReadDefinitionAndName("struct", out definitionName) )
                    {
                        foundStructs.Add(new Tuple<string, int>(definitionName, ScriptLocation));
                    }
                    else
                    {
                        //WriteErrorString(string.Format(
                        //    "Line: {0} Expected a 'struct' but found: {1}"
                        //    , CalculateLineNumber(scriptText, 0, ScriptLocation)
                        //    , scriptText.Substring(ScriptLocation, 6)));
                        //////////////////////////////////////////////////////////////////////////
                        // error here that the expected type was missing and/or is invalid
                        //////////////////////////////////////////////////////////////////////////
                    }
                }
                else if (scriptText[ScriptLocation] == 'f')
                {
                    string definitionName = string.Empty;
                    int startOfFileTag = ScriptLocation;

                    if (ReadDefinitionAndName("file", out definitionName))
                    {
                        scriptFileName = definitionName;
                        startOfFileTagInScript = startOfFileTag;

                        parseStack.Peek().CurrentStruct.Name = scriptFileName;

                        // ok - we've just read the name of the file tag and we're on the first character after the name
                        // break out now!
                        break;
                    }
                    else
                    {
                        //WriteErrorString(string.Format(
                        //    "Line: {0} Expected a 'file' but found: {1}"
                        //    , CalculateLineNumber(scriptText, 0, ScriptLocation)
                        //    , scriptText.Substring(ScriptLocation, 4)));
                        //////////////////////////////////////////////////////////////////////////
                        // error here that the expected type was missing and/or is invalid
                        //////////////////////////////////////////////////////////////////////////
                    }
                }

                ++ScriptLocation;
            }
        }

        private bool ReadDefinitionAndName(string typeToLookFor, out string readName)
        {
            bool valueToReturn = true;

            if (string.Compare(scriptText, ScriptLocation, typeToLookFor, 0, typeToLookFor.Length) == 0)
            {
                // ok - we've found the type we're after - pull out the name and remember where it was
                ScriptLocation += typeToLookFor.Length;

                //////////////////////////////////////////////////////////////////////////
                // error here about expecting there to be at least 1 white space!
                //////////////////////////////////////////////////////////////////////////

                // move past the separating white space to the start of the name
                ReadPastWhiteSpace();

                int endOfStructName = ScriptLocation;

                // read to the end of the name
                while (endOfStructName < scriptText.Length)
                {
                    if (!IsWhiteSpaceCharacter(scriptText[endOfStructName]))
                    {
                        ++endOfStructName;
                    }
                    else
                    {
                        // we've found the end of the name
                        break;
                    }
                }

                readName = scriptText.Substring(ScriptLocation, endOfStructName - ScriptLocation);

                ScriptLocation = endOfStructName;
                valueToReturn = true;
            }
            else
            {
                readName = string.Empty;
                valueToReturn = false;
            }

            return valueToReturn;
        }

        private bool IsWhiteSpaceCharacter(char characterToTest)
        {
            return characterToTest == ' '
                || characterToTest == '\n'
                || characterToTest == '\t'
                || characterToTest == '\r';
        }

        private void WriteErrorString(string error)
        {
            ErrorOutput += error + "\n";
        }

        private int CalculateLineNumber(string stringToCheck, int startIndex, int endIndex)
        {
            int countToReturn = 0;

            for(int i = startIndex; i < endIndex; ++i)
            {
                if(stringToCheck[i] == '\n')
                {
                    ++countToReturn;
                }
            }

            return countToReturn;
        }
    }
}
