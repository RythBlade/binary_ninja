namespace binary_viewer
{
    partial class MainWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            binary_viewer.Controls.HexGrid.HexGridViewSettings hexGridViewSettings1 = new binary_viewer.Controls.HexGrid.HexGridViewSettings();
            this.maintMenuStrip = new System.Windows.Forms.MenuStrip();
            this.fileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openScriptMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openTargetFileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hexDisplaySettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.scriptToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.parseScriptToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.refreshDataMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.debugButtonsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toHardCodedTestToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.writeTestItemMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fileSpecOpenFileDialogue = new System.Windows.Forms.OpenFileDialog();
            this.targetFileOpenFileDialogue = new System.Windows.Forms.OpenFileDialog();
            this.saveTestFileDialogue = new System.Windows.Forms.SaveFileDialog();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.mainTabControl = new System.Windows.Forms.TabControl();
            this.gridDisplayTabPage = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.fileDisplayPropertyGrid = new System.Windows.Forms.PropertyGrid();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.parseScriptButton = new System.Windows.Forms.ToolStripButton();
            this.scriptViewTextBox = new System.Windows.Forms.RichTextBox();
            this.textDumpTabPage = new System.Windows.Forms.TabPage();
            this.outputWindowTextBox = new System.Windows.Forms.RichTextBox();
            this.secondaryTabControl = new System.Windows.Forms.TabControl();
            this.memoryTabPage = new System.Windows.Forms.TabPage();
            this.consoleTabPage = new System.Windows.Forms.TabPage();
            this.consoleOutputWindow = new System.Windows.Forms.RichTextBox();
            this.targetFileLoad_backgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.targetSpecLoad_backgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.finalise_backgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.hexGridView = new binary_viewer.Controls.HexGrid.HexGridView();
            this.maintMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.mainTabControl.SuspendLayout();
            this.gridDisplayTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.textDumpTabPage.SuspendLayout();
            this.secondaryTabControl.SuspendLayout();
            this.memoryTabPage.SuspendLayout();
            this.consoleTabPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // maintMenuStrip
            // 
            this.maintMenuStrip.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.maintMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileMenuItem,
            this.editToolStripMenuItem,
            this.scriptToolStripMenuItem,
            this.refreshDataMenuItem,
            this.debugButtonsToolStripMenuItem});
            this.maintMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.maintMenuStrip.Name = "maintMenuStrip";
            this.maintMenuStrip.Padding = new System.Windows.Forms.Padding(9, 3, 0, 3);
            this.maintMenuStrip.Size = new System.Drawing.Size(1584, 35);
            this.maintMenuStrip.TabIndex = 1;
            this.maintMenuStrip.Text = "menuStrip1";
            // 
            // fileMenuItem
            // 
            this.fileMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openScriptMenuItem,
            this.openTargetFileMenuItem});
            this.fileMenuItem.Name = "fileMenuItem";
            this.fileMenuItem.Size = new System.Drawing.Size(50, 29);
            this.fileMenuItem.Text = "File";
            // 
            // openScriptMenuItem
            // 
            this.openScriptMenuItem.Name = "openScriptMenuItem";
            this.openScriptMenuItem.Size = new System.Drawing.Size(236, 30);
            this.openScriptMenuItem.Text = "Open Script...";
            this.openScriptMenuItem.Click += new System.EventHandler(this.openFileSpecMenuItem_Click);
            // 
            // openTargetFileMenuItem
            // 
            this.openTargetFileMenuItem.Name = "openTargetFileMenuItem";
            this.openTargetFileMenuItem.Size = new System.Drawing.Size(236, 30);
            this.openTargetFileMenuItem.Text = "Open Target File...";
            this.openTargetFileMenuItem.Click += new System.EventHandler(this.openTargetFileMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.hexDisplaySettingsToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(54, 29);
            this.editToolStripMenuItem.Text = "Edit";
            // 
            // hexDisplaySettingsToolStripMenuItem
            // 
            this.hexDisplaySettingsToolStripMenuItem.Name = "hexDisplaySettingsToolStripMenuItem";
            this.hexDisplaySettingsToolStripMenuItem.Size = new System.Drawing.Size(258, 30);
            this.hexDisplaySettingsToolStripMenuItem.Text = "Hex Display Settings";
            this.hexDisplaySettingsToolStripMenuItem.Click += new System.EventHandler(this.hexDisplaySettingsToolStripMenuItem_Click);
            // 
            // scriptToolStripMenuItem
            // 
            this.scriptToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.parseScriptToolStripMenuItem});
            this.scriptToolStripMenuItem.Name = "scriptToolStripMenuItem";
            this.scriptToolStripMenuItem.Size = new System.Drawing.Size(69, 29);
            this.scriptToolStripMenuItem.Text = "Script";
            // 
            // parseScriptToolStripMenuItem
            // 
            this.parseScriptToolStripMenuItem.Name = "parseScriptToolStripMenuItem";
            this.parseScriptToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.parseScriptToolStripMenuItem.Size = new System.Drawing.Size(228, 30);
            this.parseScriptToolStripMenuItem.Text = "Parse script...";
            this.parseScriptToolStripMenuItem.Click += new System.EventHandler(this.parseScriptButton_Click);
            // 
            // refreshDataMenuItem
            // 
            this.refreshDataMenuItem.Name = "refreshDataMenuItem";
            this.refreshDataMenuItem.Size = new System.Drawing.Size(155, 29);
            this.refreshDataMenuItem.Text = "Refresh File Data";
            this.refreshDataMenuItem.Click += new System.EventHandler(this.refreshDataMenuItem_Click);
            // 
            // debugButtonsToolStripMenuItem
            // 
            this.debugButtonsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toHardCodedTestToolStripMenuItem,
            this.writeTestItemMenuItem});
            this.debugButtonsToolStripMenuItem.Name = "debugButtonsToolStripMenuItem";
            this.debugButtonsToolStripMenuItem.Size = new System.Drawing.Size(144, 29);
            this.debugButtonsToolStripMenuItem.Text = "Debug Buttons";
            // 
            // toHardCodedTestToolStripMenuItem
            // 
            this.toHardCodedTestToolStripMenuItem.Name = "toHardCodedTestToolStripMenuItem";
            this.toHardCodedTestToolStripMenuItem.Size = new System.Drawing.Size(263, 30);
            this.toHardCodedTestToolStripMenuItem.Text = "To Hard Coded Test...";
            this.toHardCodedTestToolStripMenuItem.Click += new System.EventHandler(this.toHardCodedTestToolStripMenuItem_Click_1);
            // 
            // writeTestItemMenuItem
            // 
            this.writeTestItemMenuItem.Name = "writeTestItemMenuItem";
            this.writeTestItemMenuItem.Size = new System.Drawing.Size(263, 30);
            this.writeTestItemMenuItem.Text = "Write Test Item...";
            this.writeTestItemMenuItem.Click += new System.EventHandler(this.writeTestItemMenuItem_Click_1);
            // 
            // fileSpecOpenFileDialogue
            // 
            this.fileSpecOpenFileDialogue.Title = "Open File Spec";
            // 
            // targetFileOpenFileDialogue
            // 
            this.targetFileOpenFileDialogue.ReadOnlyChecked = true;
            this.targetFileOpenFileDialogue.Title = "Open Target File";
            // 
            // saveTestFileDialogue
            // 
            this.saveTestFileDialogue.Title = "Save Test File";
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 35);
            this.splitContainer2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.mainTabControl);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.secondaryTabControl);
            this.splitContainer2.Size = new System.Drawing.Size(1584, 733);
            this.splitContainer2.SplitterDistance = 558;
            this.splitContainer2.SplitterWidth = 6;
            this.splitContainer2.TabIndex = 3;
            // 
            // mainTabControl
            // 
            this.mainTabControl.Controls.Add(this.gridDisplayTabPage);
            this.mainTabControl.Controls.Add(this.textDumpTabPage);
            this.mainTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainTabControl.Location = new System.Drawing.Point(0, 0);
            this.mainTabControl.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.mainTabControl.Name = "mainTabControl";
            this.mainTabControl.SelectedIndex = 0;
            this.mainTabControl.Size = new System.Drawing.Size(1584, 558);
            this.mainTabControl.TabIndex = 3;
            // 
            // gridDisplayTabPage
            // 
            this.gridDisplayTabPage.Controls.Add(this.splitContainer1);
            this.gridDisplayTabPage.Location = new System.Drawing.Point(4, 29);
            this.gridDisplayTabPage.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gridDisplayTabPage.Name = "gridDisplayTabPage";
            this.gridDisplayTabPage.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gridDisplayTabPage.Size = new System.Drawing.Size(1576, 525);
            this.gridDisplayTabPage.TabIndex = 0;
            this.gridDisplayTabPage.Text = "Grid Data Display";
            this.gridDisplayTabPage.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(4, 5);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.fileDisplayPropertyGrid);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.toolStrip1);
            this.splitContainer1.Panel2.Controls.Add(this.scriptViewTextBox);
            this.splitContainer1.Size = new System.Drawing.Size(1568, 515);
            this.splitContainer1.SplitterDistance = 731;
            this.splitContainer1.SplitterWidth = 6;
            this.splitContainer1.TabIndex = 1;
            // 
            // fileDisplayPropertyGrid
            // 
            this.fileDisplayPropertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fileDisplayPropertyGrid.HelpVisible = false;
            this.fileDisplayPropertyGrid.LineColor = System.Drawing.SystemColors.ControlDark;
            this.fileDisplayPropertyGrid.Location = new System.Drawing.Point(0, 0);
            this.fileDisplayPropertyGrid.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.fileDisplayPropertyGrid.Name = "fileDisplayPropertyGrid";
            this.fileDisplayPropertyGrid.PropertySort = System.Windows.Forms.PropertySort.NoSort;
            this.fileDisplayPropertyGrid.Size = new System.Drawing.Size(731, 515);
            this.fileDisplayPropertyGrid.TabIndex = 0;
            this.fileDisplayPropertyGrid.ToolbarVisible = false;
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.parseScriptButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Padding = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.toolStrip1.Size = new System.Drawing.Size(831, 32);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // parseScriptButton
            // 
            this.parseScriptButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.parseScriptButton.Image = ((System.Drawing.Image)(resources.GetObject("parseScriptButton.Image")));
            this.parseScriptButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.parseScriptButton.Name = "parseScriptButton";
            this.parseScriptButton.Size = new System.Drawing.Size(107, 29);
            this.parseScriptButton.Text = "Parse Script";
            this.parseScriptButton.Click += new System.EventHandler(this.parseScriptButton_Click);
            // 
            // scriptViewTextBox
            // 
            this.scriptViewTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.scriptViewTextBox.Location = new System.Drawing.Point(0, 43);
            this.scriptViewTextBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.scriptViewTextBox.Name = "scriptViewTextBox";
            this.scriptViewTextBox.Size = new System.Drawing.Size(829, 465);
            this.scriptViewTextBox.TabIndex = 0;
            this.scriptViewTextBox.Text = "";
            // 
            // textDumpTabPage
            // 
            this.textDumpTabPage.Controls.Add(this.outputWindowTextBox);
            this.textDumpTabPage.Location = new System.Drawing.Point(4, 29);
            this.textDumpTabPage.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textDumpTabPage.Name = "textDumpTabPage";
            this.textDumpTabPage.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textDumpTabPage.Size = new System.Drawing.Size(1576, 525);
            this.textDumpTabPage.TabIndex = 1;
            this.textDumpTabPage.Text = "Text Data Display";
            this.textDumpTabPage.UseVisualStyleBackColor = true;
            // 
            // outputWindowTextBox
            // 
            this.outputWindowTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.outputWindowTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.outputWindowTextBox.Location = new System.Drawing.Point(4, 5);
            this.outputWindowTextBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.outputWindowTextBox.Name = "outputWindowTextBox";
            this.outputWindowTextBox.ReadOnly = true;
            this.outputWindowTextBox.Size = new System.Drawing.Size(1568, 515);
            this.outputWindowTextBox.TabIndex = 0;
            this.outputWindowTextBox.Text = "";
            // 
            // secondaryTabControl
            // 
            this.secondaryTabControl.Controls.Add(this.memoryTabPage);
            this.secondaryTabControl.Controls.Add(this.consoleTabPage);
            this.secondaryTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.secondaryTabControl.Location = new System.Drawing.Point(0, 0);
            this.secondaryTabControl.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.secondaryTabControl.Name = "secondaryTabControl";
            this.secondaryTabControl.SelectedIndex = 0;
            this.secondaryTabControl.Size = new System.Drawing.Size(1584, 169);
            this.secondaryTabControl.TabIndex = 1;
            // 
            // memoryTabPage
            // 
            this.memoryTabPage.Controls.Add(this.hexGridView);
            this.memoryTabPage.Location = new System.Drawing.Point(4, 29);
            this.memoryTabPage.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.memoryTabPage.Name = "memoryTabPage";
            this.memoryTabPage.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.memoryTabPage.Size = new System.Drawing.Size(1576, 136);
            this.memoryTabPage.TabIndex = 0;
            this.memoryTabPage.Text = "Memory View";
            this.memoryTabPage.UseVisualStyleBackColor = true;
            // 
            // consoleTabPage
            // 
            this.consoleTabPage.Controls.Add(this.consoleOutputWindow);
            this.consoleTabPage.Location = new System.Drawing.Point(4, 29);
            this.consoleTabPage.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.consoleTabPage.Name = "consoleTabPage";
            this.consoleTabPage.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.consoleTabPage.Size = new System.Drawing.Size(1576, 136);
            this.consoleTabPage.TabIndex = 1;
            this.consoleTabPage.Text = "Output";
            this.consoleTabPage.UseVisualStyleBackColor = true;
            // 
            // consoleOutputWindow
            // 
            this.consoleOutputWindow.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.consoleOutputWindow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.consoleOutputWindow.ForeColor = System.Drawing.Color.Red;
            this.consoleOutputWindow.Location = new System.Drawing.Point(4, 5);
            this.consoleOutputWindow.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.consoleOutputWindow.Name = "consoleOutputWindow";
            this.consoleOutputWindow.ReadOnly = true;
            this.consoleOutputWindow.Size = new System.Drawing.Size(1568, 126);
            this.consoleOutputWindow.TabIndex = 0;
            this.consoleOutputWindow.Text = "";
            // 
            // targetFileLoad_backgroundWorker
            // 
            this.targetFileLoad_backgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker_DoWork);
            this.targetFileLoad_backgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker_RunWorkerCompleted);
            // 
            // targetSpecLoad_backgroundWorker
            // 
            this.targetSpecLoad_backgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker_DoWork);
            this.targetSpecLoad_backgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker_RunWorkerCompleted);
            // 
            // finalise_backgroundWorker
            // 
            this.finalise_backgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker_DoWork);
            this.finalise_backgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker_RunWorkerCompleted);
            // 
            // hexGridView
            // 
            this.hexGridView.DataBufferToDisplay = null;
            hexGridViewSettings1.BytesPerLine = 16;
            hexGridViewSettings1.ColumnInfoVisible = true;
            hexGridViewSettings1.GroupSeparatorVisible = true;
            hexGridViewSettings1.GroupSize = 4;
            hexGridViewSettings1.LineInfoVisible = false;
            hexGridViewSettings1.StringViewVisible = true;
            hexGridViewSettings1.UseFixedBytesPerLine = false;
            this.hexGridView.DisplaySettings = hexGridViewSettings1;
            this.hexGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hexGridView.Location = new System.Drawing.Point(4, 5);
            this.hexGridView.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.hexGridView.Name = "hexGridView";
            this.hexGridView.Size = new System.Drawing.Size(1568, 126);
            this.hexGridView.TabIndex = 0;
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1584, 768);
            this.Controls.Add(this.splitContainer2);
            this.Controls.Add(this.maintMenuStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.maintMenuStrip;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "MainWindow";
            this.Text = "Binary Ninja";
            this.maintMenuStrip.ResumeLayout(false);
            this.maintMenuStrip.PerformLayout();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.mainTabControl.ResumeLayout(false);
            this.gridDisplayTabPage.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.textDumpTabPage.ResumeLayout(false);
            this.secondaryTabControl.ResumeLayout(false);
            this.memoryTabPage.ResumeLayout(false);
            this.consoleTabPage.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip maintMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openScriptMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openTargetFileMenuItem;
        private System.Windows.Forms.OpenFileDialog fileSpecOpenFileDialogue;
        private System.Windows.Forms.OpenFileDialog targetFileOpenFileDialogue;
        private System.Windows.Forms.ToolStripMenuItem refreshDataMenuItem;
        private System.Windows.Forms.SaveFileDialog saveTestFileDialogue;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.ToolStripMenuItem debugButtonsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toHardCodedTestToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem writeTestItemMenuItem;
        private System.ComponentModel.BackgroundWorker targetFileLoad_backgroundWorker;
        private System.ComponentModel.BackgroundWorker targetSpecLoad_backgroundWorker;
        private System.ComponentModel.BackgroundWorker finalise_backgroundWorker;
        private System.Windows.Forms.TabControl secondaryTabControl;
        private System.Windows.Forms.TabPage memoryTabPage;
        private System.Windows.Forms.TabPage consoleTabPage;
        private System.Windows.Forms.RichTextBox consoleOutputWindow;
        private Controls.HexGrid.HexGridView hexGridView;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hexDisplaySettingsToolStripMenuItem;
        private System.Windows.Forms.TabControl mainTabControl;
        private System.Windows.Forms.TabPage gridDisplayTabPage;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.PropertyGrid fileDisplayPropertyGrid;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton parseScriptButton;
        private System.Windows.Forms.RichTextBox scriptViewTextBox;
        private System.Windows.Forms.TabPage textDumpTabPage;
        private System.Windows.Forms.RichTextBox outputWindowTextBox;
        private System.Windows.Forms.ToolStripMenuItem scriptToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem parseScriptToolStripMenuItem;
    }
}

