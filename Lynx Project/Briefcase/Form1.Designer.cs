namespace Lynx
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.ofd = new System.Windows.Forms.OpenFileDialog();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rftool = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.windowsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tileHorizontallyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tileVerticallyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cascadeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.extrasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.brieftool = new System.Windows.Forms.ToolStripMenuItem();
            this.enableToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.disableToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.levelInfoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.scriptEditorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.creditToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.label1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ofd
            // 
            this.ofd.Filter = "Half-life 2 bsp\'s, xzp cache files (*.bsp, *.xzp)|*.bsp;*.xzp|All Files (*.*)|*.*" +
                "";
            this.ofd.Multiselect = true;
            this.ofd.Title = "bsp/xzp :: File Dialog";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.windowsToolStripMenuItem,
            this.extrasToolStripMenuItem,
            this.helpToolStripMenuItem,
            this.creditToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(851, 24);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.rftool,
            this.toolStripSeparator2,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(35, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("openToolStripMenuItem.Image")));
            this.openToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.openToolStripMenuItem.Text = "&Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // rftool
            // 
            this.rftool.Image = ((System.Drawing.Image)(resources.GetObject("rftool.Image")));
            this.rftool.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.rftool.Name = "rftool";
            this.rftool.Size = new System.Drawing.Size(140, 22);
            this.rftool.Text = "&Recent Files";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(137, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // windowsToolStripMenuItem
            // 
            this.windowsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tileHorizontallyToolStripMenuItem,
            this.tileVerticallyToolStripMenuItem,
            this.cascadeToolStripMenuItem,
            this.toolStripSeparator3});
            this.windowsToolStripMenuItem.Name = "windowsToolStripMenuItem";
            this.windowsToolStripMenuItem.Size = new System.Drawing.Size(62, 20);
            this.windowsToolStripMenuItem.Text = "Windows";
            this.windowsToolStripMenuItem.DropDownClosed += new System.EventHandler(this.windowsToolStripMenuItem_DropDownClosed);
            this.windowsToolStripMenuItem.DropDownOpening += new System.EventHandler(this.windowsToolStripMenuItem_DropDownOpened);
            // 
            // tileHorizontallyToolStripMenuItem
            // 
            this.tileHorizontallyToolStripMenuItem.Name = "tileHorizontallyToolStripMenuItem";
            this.tileHorizontallyToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.tileHorizontallyToolStripMenuItem.Text = "Tile Horizontally";
            this.tileHorizontallyToolStripMenuItem.Click += new System.EventHandler(this.tileHorizontallyToolStripMenuItem_Click);
            // 
            // tileVerticallyToolStripMenuItem
            // 
            this.tileVerticallyToolStripMenuItem.Name = "tileVerticallyToolStripMenuItem";
            this.tileVerticallyToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.tileVerticallyToolStripMenuItem.Text = "Tile Vertically";
            this.tileVerticallyToolStripMenuItem.Click += new System.EventHandler(this.tileVerticallyToolStripMenuItem_Click);
            // 
            // cascadeToolStripMenuItem
            // 
            this.cascadeToolStripMenuItem.Name = "cascadeToolStripMenuItem";
            this.cascadeToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.cascadeToolStripMenuItem.Text = "Cascade";
            this.cascadeToolStripMenuItem.Click += new System.EventHandler(this.cascadeToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(146, 6);
            this.toolStripSeparator3.Visible = false;
            // 
            // extrasToolStripMenuItem
            // 
            this.extrasToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.brieftool,
            this.levelInfoToolStripMenuItem,
            this.settingsToolStripMenuItem});
            this.extrasToolStripMenuItem.Name = "extrasToolStripMenuItem";
            this.extrasToolStripMenuItem.Size = new System.Drawing.Size(50, 20);
            this.extrasToolStripMenuItem.Text = "Extras";
            this.extrasToolStripMenuItem.DropDownOpening += new System.EventHandler(this.extrasToolStripMenuItem_DropDownOpening);
            // 
            // brieftool
            // 
            this.brieftool.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.enableToolStripMenuItem,
            this.disableToolStripMenuItem});
            this.brieftool.Name = "brieftool";
            this.brieftool.Size = new System.Drawing.Size(227, 22);
            this.brieftool.Text = "Lynx as default editor for .bsp\'s";
            // 
            // enableToolStripMenuItem
            // 
            this.enableToolStripMenuItem.Name = "enableToolStripMenuItem";
            this.enableToolStripMenuItem.Size = new System.Drawing.Size(108, 22);
            this.enableToolStripMenuItem.Text = "Enable";
            this.enableToolStripMenuItem.Click += new System.EventHandler(this.etool_Click);
            // 
            // disableToolStripMenuItem
            // 
            this.disableToolStripMenuItem.Name = "disableToolStripMenuItem";
            this.disableToolStripMenuItem.Size = new System.Drawing.Size(108, 22);
            this.disableToolStripMenuItem.Text = "Disable";
            this.disableToolStripMenuItem.Click += new System.EventHandler(this.dtool_Click);
            // 
            // levelInfoToolStripMenuItem
            // 
            this.levelInfoToolStripMenuItem.Name = "levelInfoToolStripMenuItem";
            this.levelInfoToolStripMenuItem.Size = new System.Drawing.Size(227, 22);
            this.levelInfoToolStripMenuItem.Text = "HL2 Level Information";
            this.levelInfoToolStripMenuItem.Click += new System.EventHandler(this.levelInfoToolStripMenuItem_Click);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(227, 22);
            this.settingsToolStripMenuItem.Text = "Settings";
            this.settingsToolStripMenuItem.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.scriptEditorToolStripMenuItem});
            this.helpToolStripMenuItem.Enabled = false;
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(40, 20);
            this.helpToolStripMenuItem.Text = "Help";
            this.helpToolStripMenuItem.Visible = false;
            // 
            // scriptEditorToolStripMenuItem
            // 
            this.scriptEditorToolStripMenuItem.Name = "scriptEditorToolStripMenuItem";
            this.scriptEditorToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            this.scriptEditorToolStripMenuItem.Text = "Script Editor";
            this.scriptEditorToolStripMenuItem.Click += new System.EventHandler(this.scriptEditorToolStripMenuItem_Click);
            // 
            // creditToolStripMenuItem
            // 
            this.creditToolStripMenuItem.Name = "creditToolStripMenuItem";
            this.creditToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.creditToolStripMenuItem.Text = "Credit";
            this.creditToolStripMenuItem.Click += new System.EventHandler(this.creditToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.label1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 610);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(851, 22);
            this.statusStrip1.TabIndex = 5;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(62, 17);
            this.toolStripStatusLabel1.Text = "Last Event:";
            // 
            // label1
            // 
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(24, 17);
            this.label1.Text = "Null";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(851, 632);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Lynx [beta8] - By Prodigy";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog ofd;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem windowsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tileHorizontallyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tileVerticallyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cascadeToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem extrasToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem brieftool;
        private System.Windows.Forms.ToolStripMenuItem enableToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem disableToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem levelInfoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem scriptEditorToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem rftool;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        public System.Windows.Forms.ToolStripStatusLabel label1;
        private System.Windows.Forms.ToolStripMenuItem creditToolStripMenuItem;
    }
}

