namespace Lynx
{
    partial class LevelInfo
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
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem(new string[] {
            "Intro",
            "Point Insertion 1",
            "On starting the game; this is the video at the start"}, -1);
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem(new string[] {
            "d1_trainstation_01",
            "Point Insertion 2",
            "On the train right after the video"}, -1);
            System.Windows.Forms.ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem(new string[] {
            "d1_trainstation_02",
            "Point Insertion 3",
            "Just after stacking up the boxes and jumping out the window"}, -1);
            System.Windows.Forms.ListViewItem listViewItem4 = new System.Windows.Forms.ListViewItem(new string[] {
            "d1_trainstation_03",
            "Point Insertion 4",
            "On entering the flats"}, -1);
            System.Windows.Forms.ListViewItem listViewItem5 = new System.Windows.Forms.ListViewItem(new string[] {
            "d1_trainstation_04",
            "Point Insertion 5",
            "After the guy holds the door off to the combine"}, -1);
            System.Windows.Forms.ListViewItem listViewItem6 = new System.Windows.Forms.ListViewItem(new string[] {
            "d1_trainstation_05",
            "A Red Letter Day 1",
            "On following Alyx down the corridor after going down the lift"}, -1);
            System.Windows.Forms.ListViewItem listViewItem7 = new System.Windows.Forms.ListViewItem(new string[] {
            "d1_trainstation_06",
            "A Red Letter Day 2",
            "Just after failing to teleport"}, -1);
            System.Windows.Forms.ListViewItem listViewItem8 = new System.Windows.Forms.ListViewItem(new string[] {
            "d1_canals_01",
            "Route Kanal 1",
            "On going down the stairs after train jumping"}, -1);
            System.Windows.Forms.ListViewItem listViewItem9 = new System.Windows.Forms.ListViewItem(new string[] {
            "d1_canals_02",
            "Route Kanal 2",
            "MANHACKS!"}, -1);
            this.listv1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.SuspendLayout();
            // 
            // listv1
            // 
            this.listv1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.listv1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listv1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listv1.ForeColor = System.Drawing.Color.Black;
            this.listv1.FullRowSelect = true;
            this.listv1.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2,
            listViewItem3,
            listViewItem4,
            listViewItem5,
            listViewItem6,
            listViewItem7,
            listViewItem8,
            listViewItem9});
            this.listv1.Location = new System.Drawing.Point(0, 0);
            this.listv1.Name = "listv1";
            this.listv1.Size = new System.Drawing.Size(550, 266);
            this.listv1.TabIndex = 0;
            this.listv1.UseCompatibleStateImageBehavior = false;
            this.listv1.View = System.Windows.Forms.View.Details;
            this.listv1.SelectedIndexChanged += new System.EventHandler(this.listv1_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Filename";
            this.columnHeader1.Width = 145;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Ingame name";
            this.columnHeader2.Width = 134;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Time of loading";
            this.columnHeader3.Width = 236;
            // 
            // LevelInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(550, 266);
            this.Controls.Add(this.listv1);
            this.Name = "LevelInfo";
            this.Text = "Lynx :: Level Info";
            this.Resize += new System.EventHandler(this.Form2_Resize);
            this.Load += new System.EventHandler(this.Form2_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView listv1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
    }
}