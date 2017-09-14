using binary_viewer.Converters;
using System.ComponentModel;

namespace binary_viewer.Spec
{
    [TypeConverterAttribute(typeof(PropertySpecConverter))]
    public abstract class PropertySpec
    { 
        public string Name { get; set; }

        public abstract int LengthInBytes { get; }

        public string Value
        {
            get
            {
                return PrintPropertyValueText();
            }
        }

        [BrowsableAttribute(false)]
        protected byte[] fileData;

        [BrowsableAttribute(false)]
        protected int firstByteOfValue;
        
        public int FirstByteOfValue { get; }

        public PropertySpec(byte[] fileDataBuffer, int indexOfFirstByte)
        {
            Name = string.Empty;

            fileData = fileDataBuffer;
            firstByteOfValue = indexOfFirstByte;
        }

        public abstract string PrintPropertyValueText();

        public abstract string PrintPropertyText(string prefixString);

        public abstract PropertySpec Clone(byte[] fileDataBuffer, int indexOfFirstByte);
    }
}
