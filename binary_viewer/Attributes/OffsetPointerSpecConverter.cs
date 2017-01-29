using binary_viewer.Spec;
using System;
using System.ComponentModel;

namespace binary_viewer.Attributes
{
    class OffsetPointerSpecConverter : ExpandableObjectConverter
    {
        public override object ConvertTo(ITypeDescriptorContext context,
                             System.Globalization.CultureInfo culture,
                             object value, Type destType)
        {
            if (destType == typeof(string) && value is OffsetPointerSpec)
            {
                OffsetPointerSpec emp = value as OffsetPointerSpec;

                string stringToOutput = string.Empty;

                if (emp != null)
                {
                    stringToOutput += $"{emp.OffsetValue} {{ {emp.Target.Value} }}";
                }

                return stringToOutput;
            }

            return base.ConvertTo(context, culture, value, destType);
        }
    }
}
