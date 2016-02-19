namespace Lynx
{
    partial class Help
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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Why am i seeing @ \'s?");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Manual Mode", new System.Windows.Forms.TreeNode[] {
            treeNode1});
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("What is simple mode?");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("What is a segment?");
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("How do i add classnames?");
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("How do i rename/remove classnames?");
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("How do i add properties?");
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("How do i rename/remove properties?");
            System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("Simple Mode", new System.Windows.Forms.TreeNode[] {
            treeNode3,
            treeNode4,
            treeNode5,
            treeNode6,
            treeNode7,
            treeNode8});
            System.Windows.Forms.TreeNode treeNode10 = new System.Windows.Forms.TreeNode("What happens if the script is smaller than the original size?");
            System.Windows.Forms.TreeNode treeNode11 = new System.Windows.Forms.TreeNode("What happens if the script is bigger than the original size?");
            System.Windows.Forms.TreeNode treeNode12 = new System.Windows.Forms.TreeNode("The Injector", new System.Windows.Forms.TreeNode[] {
            treeNode10,
            treeNode11});
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.treev = new System.Windows.Forms.TreeView();
            this.label1 = new System.Windows.Forms.Label();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.treev);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.label1);
            this.splitContainer1.Size = new System.Drawing.Size(608, 244);
            this.splitContainer1.SplitterDistance = 153;
            this.splitContainer1.TabIndex = 0;
            // 
            // treev
            // 
            this.treev.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treev.Location = new System.Drawing.Point(0, 0);
            this.treev.Name = "treev";
            treeNode1.Name = "Node0";
            treeNode1.Text = "Why am i seeing @ \'s?";
            treeNode2.Name = "Node0";
            treeNode2.Text = "Manual Mode";
            treeNode3.Name = "Node2";
            treeNode3.Text = "What is simple mode?";
            treeNode4.Name = "Node6";
            treeNode4.Text = "What is a segment?";
            treeNode5.Name = "Node8";
            treeNode5.Text = "How do i add classnames?";
            treeNode6.Name = "Node10";
            treeNode6.Text = "How do i rename/remove classnames?";
            treeNode7.Name = "Node12";
            treeNode7.Text = "How do i add properties?";
            treeNode8.Name = "Node13";
            treeNode8.Text = "How do i rename/remove properties?";
            treeNode9.Name = "Node1";
            treeNode9.Text = "Simple Mode";
            treeNode10.Name = "Node1";
            treeNode10.Text = "What happens if the script is smaller than the original size?";
            treeNode11.Name = "Node2";
            treeNode11.Text = "What happens if the script is bigger than the original size?";
            treeNode12.Name = "Node7";
            treeNode12.Text = "The Injector";
            this.treev.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode2,
            treeNode9,
            treeNode12});
            this.treev.Size = new System.Drawing.Size(608, 153);
            this.treev.TabIndex = 0;
            this.treev.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treev_AfterSelect);
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(608, 87);
            this.label1.TabIndex = 1;
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // Form3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(608, 244);
            this.Controls.Add(this.splitContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form3";
            this.Text = "Lynx :: Script Editor Help";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView treev;
        private System.Windows.Forms.Label label1;
    }
}