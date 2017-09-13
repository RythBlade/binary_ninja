using binary_viewer.Spec;
using System;
using System.ComponentModel;

namespace binary_viewer.Converters
{
    public class PropertySpecConverter : ExpandableObjectConverter
    {
        public override object ConvertTo(ITypeDescriptorContext context,
                             System.Globalization.CultureInfo culture,
                             object value, Type destType)
        {
            if (destType == typeof(string) && value is PropertySpec)
            {
                PropertySpec emp = value as PropertySpec;
                return emp != null ? emp.Value : string.Empty;
            }

            return base.ConvertTo(context, culture, value, destType);
        }
    }
}
