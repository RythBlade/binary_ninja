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
            this.maintMenuStrip = new System.Windows.Forms.MenuStrip();
            this.fileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openScriptMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openTargetFileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.refreshDataMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.debugButtonsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toHardCodedTestToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fileSpecOpenFileDialogue = new System.Windows.Forms.OpenFileDialog();
            this.targetFileOpenFileDialogue = new System.Windows.Forms.OpenFileDialog();
            this.saveTestFileDialogue = new System.Windows.Forms.SaveFileDialog();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.mainTabControl = new System.Windows.Forms.TabControl();
            this.gridDisplayTabPage = new System.Windows.Forms.TabPage();
            this.fileDisplayPropertyGrid = new System.Windows.Forms.PropertyGrid();
            this.outputWindowTabPage = new System.Windows.Forms.TabPage();
            this.outputWindowTextBox = new System.Windows.Forms.RichTextBox();
            this.scriptDisplayTabPage = new System.Windows.Forms.TabPage();
            this.scriptViewTextBox = new System.Windows.Forms.RichTextBox();
            this.consoleOutputWindow = new System.Windows.Forms.RichTextBox();
            this.writeTestItemMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.maintMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.mainTabControl.SuspendLayout();
            this.gridDisplayTabPage.SuspendLayout();
            this.outputWindowTabPage.SuspendLayout();
            this.scriptDisplayTabPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // maintMenuStrip
            // 
            this.maintMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileMenuItem,
            this.refreshDataMenuItem,
            this.debugButtonsToolStripMenuItem});
            this.maintMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.maintMenuStrip.Name = "maintMenuStrip";
            this.maintMenuStrip.Size = new System.Drawing.Size(1056, 24);
            this.maintMenuStrip.TabIndex = 1;
            this.maintMenuStrip.Text = "menuStrip1";
            // 
            // fileMenuItem
            // 
            this.fileMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openScriptMenuItem,
            this.openTargetFileMenuItem});
            this.fileMenuItem.Name = "fileMenuItem";
            this.fileMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileMenuItem.Text = "File";
            // 
            // openScriptMenuItem
            // 
            this.openScriptMenuItem.Name = "openScriptMenuItem";
            this.openScriptMenuItem.Size = new System.Drawing.Size(169, 22);
            this.openScriptMenuItem.Text = "Open Script...";
            this.openScriptMenuItem.Click += new System.EventHandler(this.openFileSpecMenuItem_Click);
            // 
            // openTargetFileMenuItem
            // 
            this.openTargetFileMenuItem.Name = "openTargetFileMenuItem";
            this.openTargetFileMenuItem.Size = new System.Drawing.Size(169, 22);
            this.openTargetFileMenuItem.Text = "Open Target File...";
            this.openTargetFileMenuItem.Click += new System.EventHandler(this.openTargetFileMenuItem_Click);
            // 
            // refreshDataMenuItem
            // 
            this.refreshDataMenuItem.Name = "refreshDataMenuItem";
            this.refreshDataMenuItem.Size = new System.Drawing.Size(106, 20);
            this.refreshDataMenuItem.Text = "Refresh File Data";
            this.refreshDataMenuItem.Click += new System.EventHandler(this.refreshDataMenuItem_Click);
            // 
            // debugButtonsToolStripMenuItem
            // 
            this.debugButtonsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toHardCodedTestToolStripMenuItem,
            this.writeTestItemMenuItem});
            this.debugButtonsToolStripMenuItem.Name = "debugButtonsToolStripMenuItem";
            this.debugButtonsToolStripMenuItem.Size = new System.Drawing.Size(98, 20);
            this.debugButtonsToolStripMenuItem.Text = "Debug Buttons";
            // 
            // toHardCodedTestToolStripMenuItem
            // 
            this.toHardCodedTestToolStripMenuItem.Name = "toHardCodedTestToolStripMenuItem";
            this.toHardCodedTestToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.toHardCodedTestToolStripMenuItem.Text = "To Hard Coded Test...";
            this.toHardCodedTestToolStripMenuItem.Click += new System.EventHandler(this.toHardCodedTestToolStripMenuItem_Click_1);
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
            this.splitContainer2.Location = new System.Drawing.Point(0, 24);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.mainTabControl);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.consoleOutputWindow);
            this.splitContainer2.Size = new System.Drawing.Size(1056, 475);
            this.splitContainer2.SplitterDistance = 363;
            this.splitContainer2.TabIndex = 3;
            // 
            // mainTabControl
            // 
            this.mainTabControl.Controls.Add(this.gridDisplayTabPage);
            this.mainTabControl.Controls.Add(this.outputWindowTabPage);
            this.mainTabControl.Controls.Add(this.scriptDisplayTabPage);
            this.mainTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainTabControl.Location = new System.Drawing.Point(0, 0);
            this.mainTabControl.Name = "mainTabControl";
            this.mainTabControl.SelectedIndex = 0;
            this.mainTabControl.Size = new System.Drawing.Size(1056, 363);
            this.mainTabControl.TabIndex = 3;
            // 
            // gridDisplayTabPage
            // 
            this.gridDisplayTabPage.Controls.Add(this.fileDisplayPropertyGrid);
            this.gridDisplayTabPage.Location = new System.Drawing.Point(4, 22);
            this.gridDisplayTabPage.Name = "gridDisplayTabPage";
            this.gridDisplayTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.gridDisplayTabPage.Size = new System.Drawing.Size(1048, 337);
            this.gridDisplayTabPage.TabIndex = 0;
            this.gridDisplayTabPage.Text = "Grid Data Display";
            this.gridDisplayTabPage.UseVisualStyleBackColor = true;
            // 
            // fileDisplayPropertyGrid
            // 
            this.fileDisplayPropertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fileDisplayPropertyGrid.HelpVisible = false;
            this.fileDisplayPropertyGrid.Location = new System.Drawing.Point(3, 3);
            this.fileDisplayPropertyGrid.Name = "fileDisplayPropertyGrid";
            this.fileDisplayPropertyGrid.PropertySort = System.Windows.Forms.PropertySort.NoSort;
            this.fileDisplayPropertyGrid.Size = new System.Drawing.Size(1042, 331);
            this.fileDisplayPropertyGrid.TabIndex = 0;
            this.fileDisplayPropertyGrid.ToolbarVisible = false;
            // 
            // outputWindowTabPage
            // 
            this.outputWindowTabPage.Controls.Add(this.outputWindowTextBox);
            this.outputWindowTabPage.Location = new System.Drawing.Point(4, 22);
            this.outputWindowTabPage.Name = "outputWindowTabPage";
            this.outputWindowTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.outputWindowTabPage.Size = new System.Drawing.Size(1048, 337);
            this.outputWindowTabPage.TabIndex = 1;
            this.outputWindowTabPage.Text = "Text Data Display";
            this.outputWindowTabPage.UseVisualStyleBackColor = true;
            // 
            // outputWindowTextBox
            // 
            this.outputWindowTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.outputWindowTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.outputWindowTextBox.Location = new System.Drawing.Point(3, 3);
            this.outputWindowTextBox.Name = "outputWindowTextBox";
            this.outputWindowTextBox.ReadOnly = true;
            this.outputWindowTextBox.Size = new System.Drawing.Size(1042, 331);
            this.outputWindowTextBox.TabIndex = 0;
            this.outputWindowTextBox.Text = "";
            // 
            // scriptDisplayTabPage
            // 
            this.scriptDisplayTabPage.Controls.Add(this.scriptViewTextBox);
            this.scriptDisplayTabPage.Location = new System.Drawing.Point(4, 22);
            this.scriptDisplayTabPage.Name = "scriptDisplayTabPage";
            this.scriptDisplayTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.scriptDisplayTabPage.Size = new System.Drawing.Size(1048, 337);
            this.scriptDisplayTabPage.TabIndex = 2;
            this.scriptDisplayTabPage.Text = "Script Display";
            this.scriptDisplayTabPage.UseVisualStyleBackColor = true;
            // 
            // scriptViewTextBox
            // 
            this.scriptViewTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scriptViewTextBox.Location = new System.Drawing.Point(3, 3);
            this.scriptViewTextBox.Name = "scriptViewTextBox";
            this.scriptViewTextBox.ReadOnly = true;
            this.scriptViewTextBox.Size = new System.Drawing.Size(1042, 331);
            this.scriptViewTextBox.TabIndex = 0;
            this.scriptViewTextBox.Text = "";
            // 
            // consoleOutputWindow
            // 
            this.consoleOutputWindow.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.consoleOutputWindow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.consoleOutputWindow.ForeColor = System.Drawing.Color.Red;
            this.consoleOutputWindow.Location = new System.Drawing.Point(0, 0);
            this.consoleOutputWindow.Name = "consoleOutputWindow";
            this.consoleOutputWindow.ReadOnly = true;
            this.consoleOutputWindow.Size = new System.Drawing.Size(1056, 108);
            this.consoleOutputWindow.TabIndex = 0;
            this.consoleOutputWindow.Text = "";
            // 
            // writeTestItemMenuItem
            // 
            this.writeTestItemMenuItem.Name = "writeTestItemMenuItem";
            this.writeTestItemMenuItem.Size = new System.Drawing.Size(187, 22);
            this.writeTestItemMenuItem.Text = "Write Test Item...";
            this.writeTestItemMenuItem.Click += new System.EventHandler(this.writeTestItemMenuItem_Click_1);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1056, 499);
            this.Controls.Add(this.splitContainer2);
            this.Controls.Add(this.maintMenuStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.maintMenuStrip;
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
            this.outputWindowTabPage.ResumeLayout(false);
            this.scriptDisplayTabPage.ResumeLayout(false);
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
        private System.Windows.Forms.RichTextBox consoleOutputWindow;
        private System.Windows.Forms.TabControl mainTabControl;
        private System.Windows.Forms.TabPage gridDisplayTabPage;
        private System.Windows.Forms.PropertyGrid fileDisplayPropertyGrid;
        private System.Windows.Forms.TabPage outputWindowTabPage;
        private System.Windows.Forms.RichTextBox outputWindowTextBox;
        private System.Windows.Forms.TabPage scriptDisplayTabPage;
        private System.Windows.Forms.RichTextBox scriptViewTextBox;
        private System.Windows.Forms.ToolStripMenuItem debugButtonsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toHardCodedTestToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem writeTestItemMenuItem;
    }
}

