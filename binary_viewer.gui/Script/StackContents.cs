using binary_viewer.Spec;
using System.Collections.Generic;

namespace binary_viewer.Script
{
    public class StackContents
    {
        public StructSpec CurrentStruct { get; set; }
        public int ScriptLocation { get; set; }
        public List<ValueSpec> ScopedProperties { get; set; }

        public bool ThisIsAnArray { get; set; }
        public int ArrayLength { get; set; }
        public ArraySpec TheArray { get; set; }
        public int CurrentOffsetIntoFileSpec { get; set; }
        public int OffsetIntoFileSpecStackStarts { get; set; }

        public StackContents()
        {
            ScriptLocation = 0;
            ScopedProperties = new List<ValueSpec>();
            ThisIsAnArray = false;
            ArrayLength = 0;
            TheArray = null;
            CurrentStruct = null;
            CurrentOffsetIntoFileSpec = 0;
            OffsetIntoFileSpecStackStarts = 0;
        }

    }
}
