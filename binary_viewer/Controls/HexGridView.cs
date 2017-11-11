using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                RefreshAllData();
            }
        }

        public int LengthOfData
        {
            get { return DataBufferToDisplay != null ? DataBufferToDisplay.Length : 1; }
        }
        
        public HexGridView()
        {
            InitializeComponent();

            // centre the splitter
            splitContainer.SplitterDistance = (splitContainer.Size.Width - splitContainer.SplitterWidth) / 2;
            splitContainer.IsSplitterFixed = true;

            Resize += HexGridView_Resize;

            SetupCustomGridViewControlSettings(hexView);
            SetupCustomGridViewControlSettings(dataView);            

            hexView.Resize += DataGridView_Resize;
            dataView.Resize += DataGridView_Resize;

            hexView.Scroll += HexView_Scroll;
            dataView.Scroll += DataView_Scroll;
            
            RefreshAllData();
        }

        private void HexGridView_Resize(object sender, EventArgs e)
        {
            splitContainer.SplitterDistance = (splitContainer.Size.Width - splitContainer.SplitterWidth) / 2;
        }

        private void SetupCustomGridViewControlSettings(DataGridView gridView)
        {
            //////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // do some context specific settings in code so they're easier to keep track over rather than using the designer
            //////////////////////////////////////////////////////////////////////////////////////////////////////////////

            gridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            gridView.RowTemplate.Resizable = DataGridViewTriState.False;
            gridView.SelectionMode = DataGridViewSelectionMode.CellSelect;
            gridView.AllowUserToAddRows = false;
            gridView.AllowUserToDeleteRows = false;
            gridView.ColumnHeadersVisible = false;
            gridView.RowHeadersWidth = 50;
            gridView.RowTemplate.Resizable = DataGridViewTriState.False;
            
            //////////////////////////////////////////////////////////////////////////////////////////////////////////////
        }

        private void RefreshAllData()
        {
            ResizeColumns(hexView);
            ResizeRows(hexView);
            PushDataAsHexToGridView(hexView);

            ResizeColumns(dataView);
            ResizeRows(dataView);
            PushDataAsCharactersToGridView(dataView);
        }

        private void DataView_Scroll(object sender, ScrollEventArgs e)
        {
            hexView.FirstDisplayedScrollingColumnIndex = dataView.FirstDisplayedScrollingColumnIndex;
            hexView.FirstDisplayedScrollingRowIndex = dataView.FirstDisplayedScrollingRowIndex;
        }

        private void HexView_Scroll(object sender, ScrollEventArgs e)
        {
            dataView.FirstDisplayedScrollingColumnIndex = hexView.FirstDisplayedScrollingColumnIndex;
            dataView.FirstDisplayedScrollingRowIndex = hexView.FirstDisplayedScrollingRowIndex;
        }

        private void DataGridView_Resize(object sender, EventArgs e)
        {
            DataGridView gridView = sender as DataGridView;

            ResizeColumns(gridView);
            ResizeRows(gridView);

            if (gridView == hexView)
            {
                PushDataAsHexToGridView(gridView);
            }
            else
            {
                PushDataAsCharactersToGridView(gridView);
            }
        }

        void ResizeColumns(DataGridView gridView)
        {
            int rowHeaderWidth = gridView.RowHeadersVisible ? gridView.RowHeadersWidth : 0;
            int gridWidth = Math.Max(1, gridView.Size.Width - rowHeaderWidth - SystemInformation.VerticalScrollBarWidth);

            // we either want enough columns to fill the panel, or display all of the data
            m_numberOfColumns = Math.Min(LengthOfData, gridWidth / ColumnWidth);

            int columnNumberDifference = m_numberOfColumns - gridView.Columns.Count;

            if (columnNumberDifference < 0)
            {
                // there are too many columns, we need to remove some
                int numberToRemove = Math.Abs(columnNumberDifference);
                
                for (int i = 0; i < numberToRemove; ++i)
                {
                    if (gridView.Columns.Count > 0) // safety check to make sure we don't try to remove columns that don't exist
                    {
                        gridView.Columns.RemoveAt(gridView.Columns.Count - 1);
                    }
                    else
                    {
                        break;
                    }
                }
            }
            else
            {
                // we don't have enough columns, so add some more
                for (int i = 0; i < columnNumberDifference; ++i)
                {
                    gridView.Columns.Add($"{gridView.Columns.Count}", $"{gridView.Columns.Count}");
                }
            }

            foreach (DataGridViewColumn column in gridView.Columns)
            {
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                column.Resizable = DataGridViewTriState.False;
                column.Width = ColumnWidth;
            }
        }

        void ResizeRows(DataGridView gridView)
        {
            int numberOfColumns = Math.Max(1, m_numberOfColumns);
            int bufferDivColumns = LengthOfData / numberOfColumns;
            int extraRow = LengthOfData % numberOfColumns;
            m_numberOfRows = Math.Max(
                1
                , bufferDivColumns + extraRow);

            int rowNumberDifference = m_numberOfRows - gridView.Rows.Count;

            if (rowNumberDifference < 0)
            {
                // there are too many rows, we need to remove some
                int numberToRemove = Math.Abs(rowNumberDifference);
                
                for (int i = 0; i < numberToRemove; ++i)
                {
                    gridView.Rows.RemoveAt(gridView.Rows.Count - 1);
                }
            }
            else
            {
                // we don't have enough rows, so add some more
                if (rowNumberDifference > 0)
                {
                    gridView.Rows.Add(rowNumberDifference);
                }
            }
        }

        private void PushDataAsHexToGridView(DataGridView gridView)
        {
            if (DataBufferToDisplay != null)
            {
                int dataItemsWritten = 0;

                // write the data from the buffer into the grid, cell by cell in the appropriate format
                foreach (DataGridViewRow row in gridView.Rows)
                {
                    row.HeaderCell.Value = dataItemsWritten.ToString("X");

                    int columnId = 0;
                    foreach (DataGridViewColumn column in gridView.Columns)
                    {
                        row.Cells[columnId++].Value = DataBufferToDisplay[dataItemsWritten++].ToString("X");

                        if (dataItemsWritten >= DataBufferToDisplay.Length)
                        {
                            return;
                        }
                    }
                }
            }
        }

        private void PushDataAsCharactersToGridView(DataGridView gridView)
        {
            if (DataBufferToDisplay != null)
            {
                int dataItemsWritten = 0;

                // write the data from the buffer into the grid, cell by cell in the appropriate format
                foreach (DataGridViewRow row in gridView.Rows)
                {
                    row.HeaderCell.Value = dataItemsWritten.ToString("X");

                    int columnId = 0;
                    foreach (DataGridViewColumn column in gridView.Columns)
                    {
                        row.Cells[columnId++].Value = Convert.ToChar(DataBufferToDisplay[dataItemsWritten++]);

                        if (dataItemsWritten >= DataBufferToDisplay.Length)
                        {
                            return;
                        }
                    }
                }
            }
        }
    }
}
