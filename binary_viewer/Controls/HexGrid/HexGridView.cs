using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace binary_viewer.Controls.HexGrid
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

        [Description("Occurs, when the value of InsertActive property has changed.")]
        public event MarkDataEventHandler MarkDataFloat;

        public HexGridView()
        {
            InitializeComponent();

            hexBox.ContextMenuStripChanged += HexBox_ContextMenuChanged;

            SetupCustomContextMenuOptions();
        }

        private void HexBox_ContextMenuChanged(object sender, System.EventArgs e)
        {
            SetupCustomContextMenuOptions();
        }

        private void SetupCustomContextMenuOptions()
        {
            if(hexBox.ContextMenuStrip != null)
            {
                // Setup the new menu items
                ToolStripMenuItem floatToolStripMenuItem = new ToolStripMenuItem("Float", null, new EventHandler(FloatMenuItem_Click));

                ToolStripMenuItem scriptEditToolStripMenuItem = new ToolStripMenuItem("Script", null, new ToolStripItem[] { floatToolStripMenuItem });

                // add new menu items to the context menu
                hexBox.ContextMenuStrip.Items.Add(new ToolStripSeparator());
                hexBox.ContextMenuStrip.Items.Add(scriptEditToolStripMenuItem);
            }
        }
        
        void FloatMenuItem_Click(object sender, EventArgs e)
        {
            MarkDataFloat?.Invoke(this, new MarkDataEventArgs() );
        }
    }
}
