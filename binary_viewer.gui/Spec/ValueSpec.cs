using System;

namespace binary_viewer.Spec
{
    public enum ValueType
    {
        eFloat
        , eDouble
        , eInt32
        , eUnsignedInt32
        , eInt64
        , eUnsignedInt64
        , eChar
        , eString
        , eCustom
    }

    public class ValueSpec : PropertySpec
    {
        public ValueType TypeOfValue { get; set; }
        
        public int LengthOfType { get; set; }

        public override int LengthInBytes { get { return LengthOfType; } }

        public ValueSpec(byte[] fileDataBuffer, int indexOfFirstByte)
            : base(fileDataBuffer, indexOfFirstByte)
        {
            TypeOfValue = ValueType.eInt32;
            LengthOfType = 0;
        }

        public int GetAsInt32()
        {
            return BitConverter.ToInt32(fileData, firstByteOfValue);
        }

        public uint GetAsUint32()
        {
            return BitConverter.ToUInt32(fileData, firstByteOfValue);
        }

        public long GetAsInt64()
        {
            return BitConverter.ToInt64(fileData, firstByteOfValue);
        }

        public ulong GetAsUint64()
        {
            return BitConverter.ToUInt64(fileData, firstByteOfValue);
        }

        public double GetAsDouble()
        {
            return BitConverter.ToDouble(fileData, firstByteOfValue);
        }

        public float GetAsFloat()
        {
            return BitConverter.ToSingle(fileData, firstByteOfValue);
        }

        public char GetAsChar()
        {
            return Convert.ToChar(fileData[firstByteOfValue]);
        }

        public string GetAsString()
        {
            string stringToReturn = string.Empty;

            for( int i = 0; i < LengthOfType; ++i)
            {
                stringToReturn += Convert.ToChar(fileData[firstByteOfValue + i]);
            }

            return stringToReturn;
        }

        public override string PrintPropertyValueText()
        {
            switch (TypeOfValue)
            {
                case ValueType.eFloat:
                    return GetAsFloat().ToString("0.00");
                case ValueType.eDouble:
                    return GetAsDouble().ToString("0.00");
                case ValueType.eInt32:
                    return GetAsInt32().ToString();
                case ValueType.eUnsignedInt32:
                    return GetAsUint32().ToString();
                case ValueType.eInt64:
                    return GetAsInt64().ToString();
                case ValueType.eUnsignedInt64:
                    return GetAsUint64().ToString();
                case ValueType.eChar:
                    char charValue = GetAsChar();
                    return $"\'{charValue.ToString()}\' {((int)charValue).ToString()}";
                case ValueType.eString:
                    break;
                case ValueType.eCustom:
                    return "Custom Type";
                default:
                    break;
            }

            return "Not implemented";
        }

        public override string PrintPropertyText(string prefixString)
        {
            return prefixString + Name + " : " + PrintPropertyValueText();
        }

        public override PropertySpec Clone(byte[] fileDataBuffer, int indexOfFirstByte)
        {
            ValueSpec newValueSpec = new ValueSpec(fileDataBuffer, indexOfFirstByte);

            newValueSpec.Name = Name;
            newValueSpec.LengthOfType = LengthOfType;
            newValueSpec.TypeOfValue = TypeOfValue;
            
            return newValueSpec;
        }
    }
}
