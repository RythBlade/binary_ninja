using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace binary_viewer.Threading
{
    class TargetFileLoadPayLoad
    {
        public string TargetFileName { get; set; }
        public byte[] TargetFileBuffer { get; set; }
    }
}
