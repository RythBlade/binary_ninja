using System;
using System.Windows.Forms;

namespace binary_viewer.Controls
{
    public partial class HexGridView : UserControl
    {
        private byte[] m_dataBufferToDisplay = null;

        public byte[] DataBufferToDisplay
        {
            get
            {
                return m_dataBufferToDisplay;
            }

            set
            {
                m_dataBufferToDisplay = value;

                if (m_dataBufferToDisplay == null)
                {
                    hexBox.ByteProvider = null;
                }
                else
                {
                    hexBox.ByteProvider = new Be.Windows.Forms.DynamicByteProvider(m_dataBufferToDisplay);
                }
            }
        }

        public HexGridViewSettings DisplaySettings
        {
            get
            {
                HexGridViewSettings settings = new HexGridViewSettings();

                settings.BytesPerLine = hexBox.BytesPerLine;
                settings.ColumnInfoVisible = hexBox.ColumnInfoVisible;
                settings.GroupSeparatorVisible = hexBox.GroupSeparatorVisible;
                settings.GroupSize = hexBox.GroupSize;
                settings.LineInfoVisible = hexBox.LineInfoVisible;
                settings.StringViewVisible = hexBox.StringViewVisible;
                settings.UseFixedBytesPerLine = hexBox.UseFixedBytesPerLine;

                return settings;
            }

            set
            {
                hexBox.BytesPerLine = value.BytesPerLine;
                hexBox.ColumnInfoVisible = value.ColumnInfoVisible;
                hexBox.GroupSeparatorVisible = value.GroupSeparatorVisible;
                hexBox.GroupSize = value.GroupSize;
                hexBox.LineInfoVisible = value.LineInfoVisible;
                hexBox.StringViewVisible = value.StringViewVisible;
                hexBox.UseFixedBytesPerLine = value.UseFixedBytesPerLine;
            }
        }

        public int LengthOfData
        {
            get { return DataBufferToDisplay != null ? DataBufferToDisplay.Length : 1; }
        }
        
        public HexGridView()
        {
            InitializeComponent();
        }
    }
}
