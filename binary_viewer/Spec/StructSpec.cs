using binary_viewer.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace binary_viewer.Spec
{
    [TypeConverterAttribute(typeof(StructSpecConverter))]
    public class StructSpec : PropertySpec, ICustomTypeDescriptor
    {
        public List<PropertySpec> Properties { get; set; }

        public override int LengthInBytes
        {
            get
            {
                int arrayLengthInBytes = 0;

                for (int i = 0; i < Properties.Count; ++i)
                {
                    arrayLengthInBytes += Properties[i].LengthInBytes;
                }

                return arrayLengthInBytes;
            }
        }

        public StructSpec(byte[] fileDataBuffer, int indexOfFirstByte)
            : base(fileDataBuffer, indexOfFirstByte)
        {
            Properties = new List<PropertySpec>();
        }

        public override string PrintPropertyValueText()
        {
            return "[" + Properties.Count + "]";
        }

        public override string PrintPropertyText(string prefixString)
        {
            string stringToReturn = string.Empty;

            stringToReturn += prefixString + "struct " + Name + "\n" + prefixString + "{\n";

            string newPrefix = prefixString;
            newPrefix += "\t";

            foreach (PropertySpec spec in Properties)
            {
                stringToReturn += spec.PrintPropertyText(newPrefix) + "\n";
            }

            stringToReturn += prefixString + "}\n";

            return stringToReturn;
        }

        public override PropertySpec Clone(byte[] fileDataBuffer, int indexOfFirstByte)
        {
            int fileIndex = indexOfFirstByte;
            StructSpec newArraySpec = new StructSpec(fileDataBuffer, fileIndex);
            newArraySpec.Name = Name;

            for (int i = 0; i < Properties.Count; ++i)
            {
                newArraySpec.Properties.Add(Properties[i].Clone(fileDataBuffer, fileIndex));
                fileIndex += Properties[i].LengthInBytes;
            }

            return newArraySpec;
        }

        #region Custom type description
        public String GetClassName()
        {
            return TypeDescriptor.GetClassName(this, true);
        }

        public AttributeCollection GetAttributes()
        {
            return TypeDescriptor.GetAttributes(this, true);
        }

        public String GetComponentName()
        {
            return TypeDescriptor.GetComponentName(this, true);
        }

        public TypeConverter GetConverter()
        {
            return TypeDescriptor.GetConverter(this, true);
        }

        public EventDescriptor GetDefaultEvent()
        {
            return TypeDescriptor.GetDefaultEvent(this, true);
        }

        public PropertyDescriptor GetDefaultProperty()
        {
            return TypeDescriptor.GetDefaultProperty(this, true);
        }

        public object GetEditor(Type editorBaseType)
        {
            return TypeDescriptor.GetEditor(this, editorBaseType, true);
        }

        public EventDescriptorCollection GetEvents(Attribute[] attributes)
        {
            return TypeDescriptor.GetEvents(this, attributes, true);
        }

        public EventDescriptorCollection GetEvents()
        {
            return TypeDescriptor.GetEvents(this, true);
        }

        public object GetPropertyOwner(PropertyDescriptor pd)
        {
            return this;
        }


        #endregion

        #region Custom Display stuff
        public PropertyDescriptorCollection GetProperties(Attribute[] attributes)
        {
            return GetProperties();
        }

        public PropertyDescriptorCollection GetProperties()
        {
            // Create a new collection object PropertyDescriptorCollection
            PropertyDescriptorCollection pds = new PropertyDescriptorCollection(null);

            // Iterate the list of employees
            for (int i = 0; i < this.Properties.Count; i++)
            {
                // For each employee create a property descriptor 
                // and add it to the 
                // PropertyDescriptorCollection instance
                PropertySpecPropertyDescripton pd = new PropertySpecPropertyDescripton(Properties, i);
                pds.Add(pd);
            }
            return pds;
        }
        #endregion
    }
}
