using binary_viewer.Converters;
using System.ComponentModel;

namespace binary_viewer.Spec
{
    [TypeConverterAttribute(typeof(OffsetPointerSpecConverter))]
    class OffsetPointerSpec : PropertySpec
    {
        public PropertySpec Target { get; set; }
        
        public override int LengthInBytes
        {
            get
            {
                return 4;
            }
        }

        public uint OffsetValue
        {
            get
            {
                return System.BitConverter.ToUInt32(fileData, firstByteOfValue);
            }
        }

        public int OffsetIntoFileOfTarget
        {
            get
            {
                return (int)OffsetValue + m_indexOfFirstByteOfScope;
            }
        }

        private int m_indexOfFirstByteOfScope;

        public OffsetPointerSpec(byte[] fileDataBuffer, int indexOfFirstByte, int indexOfFirstByteInScope)
            : base(fileDataBuffer, indexOfFirstByte)
        {
            // each spec is responsible for parsing it's bit of the file.
            // so a dependent element can use the value spec it already has to immediately use the value it's read in order to fill it's own list.

            m_indexOfFirstByteOfScope = indexOfFirstByteInScope;
        }

        public override string PrintPropertyValueText()
        {
            return OffsetValue.ToString();
        }

        public override string PrintPropertyText(string prefixString)
        {
            return prefixString + Name + " : " + PrintPropertyValueText() + "\n";
        }

        public override PropertySpec Clone(byte[] fileDataBuffer, int indexOfFirstByte)
        {
            OffsetPointerSpec newOffsetPointer = new OffsetPointerSpec(fileDataBuffer, indexOfFirstByte, m_indexOfFirstByteOfScope);
            newOffsetPointer.Target = Target.Clone(fileDataBuffer, (int)OffsetValue);
            newOffsetPointer.Name = Name;

            return newOffsetPointer;
        }
    }
}
