using binary_viewer.Spec;
using System;
using System.ComponentModel;

namespace binary_viewer.Converters
{
    class ArraySpecConverter : ExpandableObjectConverter
    {
        public override object ConvertTo(ITypeDescriptorContext context,
                             System.Globalization.CultureInfo culture,
                             object value, Type destType)
        {
            if (destType == typeof(string) && value is ArraySpec)
            {
                ArraySpec emp = value as ArraySpec;

                string stringToOutput = string.Empty;

                if (emp != null)
                {
                    foreach (PropertySpec spec in emp.PropertyArray)
                    {
                        stringToOutput += spec.Value + ", ";
                    }
                }

                return stringToOutput;
            }

            return base.ConvertTo(context, culture, value, destType);
        }
    }
}
