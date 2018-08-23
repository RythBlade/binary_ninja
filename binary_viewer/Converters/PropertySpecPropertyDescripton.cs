using binary_viewer.Spec;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace binary_viewer.Converters
{
    class PropertySpecPropertyDescripton : PropertyDescriptor
    {
        private List<PropertySpec> targetProperties = null;
        private int index = -1;

        public PropertySpecPropertyDescripton(
            List<PropertySpec> properties
            , int indexOfProperty) : base("#" + indexOfProperty.ToString(), null)
        {
            targetProperties = properties;
            index = indexOfProperty;
        }

        public override AttributeCollection Attributes
        {
            get
            {
                return new AttributeCollection(null);
            }
        }

        public override bool CanResetValue(object component)
        {
            return true;
        }

        public override Type ComponentType
        {
            get
            {
                return this.targetProperties.GetType();
            }
        }

        public override string DisplayName
        {
            get
            {
                PropertySpec emp = targetProperties[index];
                return emp.Name;
            }
        }

        public override string Description
        {
            get
            {
                return string.Empty;
            }
        }

        public override object GetValue(object component)
        {
            return this.targetProperties[index];
        }

        public override bool IsReadOnly
        {
            get { return true; }
        }

        public override string Name
        {
            get { return "#" + index.ToString(); }
        }

        public override Type PropertyType
        {
            get { return this.targetProperties[index].GetType(); }
        }

        public override void ResetValue(object component) { }

        public override bool ShouldSerializeValue(object component)
        {
            return true;
        }

        public override void SetValue(object component, object value)
        {
        }
    }
}
