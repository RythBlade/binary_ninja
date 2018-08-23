namespace binary_viewer.Controls.HexGrid
{
    public class MarkDataEventArgs
    {
        public readonly int StartByteIndex;
        public readonly int SelectionLength;

        public MarkDataEventArgs(int startByteIndex, int selectionLength)
        {
            StartByteIndex = startByteIndex;
            SelectionLength = selectionLength;
        }
    }
}
