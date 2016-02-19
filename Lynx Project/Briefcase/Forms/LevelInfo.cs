using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Lynx
{
    public partial class LevelInfo : Form
    {
        public LevelInfo()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            int d3 = listv1.Width / 4;
            listv1.Columns[0].Width = d3 - 8;
            listv1.Columns[1].Width = d3 - 8;
            listv1.Columns[2].Width = (d3 * 2) - 8;
        }

        private void Form2_Resize(object sender, EventArgs e)
        {
            int d3 = listv1.Width / 4;
            listv1.Columns[0].Width = d3 - 8;
            listv1.Columns[1].Width = d3 - 8;
            listv1.Columns[2].Width = (d3 * 2) - 8;
        }

        private void listv1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}