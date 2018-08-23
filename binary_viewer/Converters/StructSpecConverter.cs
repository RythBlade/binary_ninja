using binary_viewer.Spec;
using System;
using System.ComponentModel;

namespace binary_viewer.Converters
{
    public class StructSpecConverter : ExpandableObjectConverter
    {
        public override object ConvertTo(ITypeDescriptorContext context,
                             System.Globalization.CultureInfo culture,
                             object value, Type destType)
        {
            if (destType == typeof(string) && value is StructSpec)
            {
                StructSpec emp = value as StructSpec;
                return emp != null ? emp.Value : string.Empty;
            }

            return base.ConvertTo(context, culture, value, destType);
        }
    }
}
