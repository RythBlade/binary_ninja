namespace binary_viewer.Controls.HexGrid
{
    public class HexGridViewSettings
    {
        public bool ColumnInfoVisible { get; set; } = true;
        
        public bool LineInfoVisible { get; set; } = true;
        public bool StringViewVisible { get; set; } = true;
        
        public bool UseFixedBytesPerLine { get; set; } = false;
        public int BytesPerLine { get; set; } = 16;

        public bool GroupSeparatorVisible { get; set; } = false;
        public int GroupSize { get; set; } = 4;
    }
}
