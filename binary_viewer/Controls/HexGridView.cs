using System;
using System.Windows.Forms;

namespace binary_viewer.Controls
{
    public partial class HexGridView : UserControl
    {
        private static int ColumnWidth = 22;

        private byte[] m_dataBufferToDisplay = null;
        private int m_numberOfColumns = 0;
        private int m_numberOfRows = 0;

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
