using binary_viewer.Converters;
using System.ComponentModel;

namespace binary_viewer.Spec
{
    [TypeConverterAttribute(typeof(ArraySpecConverter))]
    public class ArraySpec : PropertySpec
    {
        public PropertySpec[] PropertyArray { get; set; }

        public override int LengthInBytes
        {
            get
            {
                int arrayLengthInBytes = 0;

                for (int i = 0; i < PropertyArray.Length; ++i)
                {
                    arrayLengthInBytes += PropertyArray[i].LengthInBytes;
                }

                return arrayLengthInBytes;
            }
        }

        public ArraySpec(byte[] fileDataBuffer, int indexOfFirstByte, int lengthOfArray)
            : base(fileDataBuffer, indexOfFirstByte)
        {
            PropertyArray = new PropertySpec[lengthOfArray];

            // each spec is responsible for parsing it's bit of the file.
            // so a dependent element can use the value spec it already has to immediatley use the value it's read in order to fill it's own list.
        }

        public override string PrintPropertyValueText()
        {
            return Name + "[" + PropertyArray.Length + "]";
        }

        public override string PrintPropertyText(string prefixString)
        {
            string stringToReturn = string.Empty;

            stringToReturn += "\n" + prefixString + "array " + Name;

            string newPrefix = prefixString;
            newPrefix += "\t";

            stringToReturn += "\n" + prefixString + "{\n";

            foreach (PropertySpec spec in PropertyArray)
            {
                stringToReturn += spec.PrintPropertyText(newPrefix) + "\n";
            }

            stringToReturn += prefixString + "}\n";

            return stringToReturn;
        }

        public override PropertySpec Clone(byte[] fileDataBuffer, int indexOfFirstByte)
        {
            int fileIndex = indexOfFirstByte;
            ArraySpec newArraySpec = new ArraySpec(fileDataBuffer, fileIndex, PropertyArray.Length);
            newArraySpec.Name = Name;

            for(int i = 0; i < PropertyArray.Length; ++i)
            {
                newArraySpec.PropertyArray[i] = PropertyArray[i].Clone(fileDataBuffer, fileIndex);
                fileIndex += newArraySpec.PropertyArray[i].LengthInBytes;
            }
            
            return newArraySpec;
        }
    }
}
