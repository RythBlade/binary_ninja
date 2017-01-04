using System.ComponentModel;

namespace binary_viewer.Spec
{
    public class FileSpec
    {
        public StructSpec File { get; set; }

        private byte[] fileBuffer;
        private int indexOfFirstByte;

        public byte[] FileBuffer { get { return fileBuffer; } }

        [BrowsableAttribute(false)]
        public int IndexOfFirstByte { get { return indexOfFirstByte; } }

        public FileSpec(byte[] fileDataBuffer, int indexOfFirstByte)
        {
            fileBuffer = fileDataBuffer;
            this.indexOfFirstByte = indexOfFirstByte;
            File = new StructSpec(fileDataBuffer, indexOfFirstByte);
        }

        public string PrintFile()
        {
            return File.PrintPropertyText(string.Empty);
        }
    }
}
