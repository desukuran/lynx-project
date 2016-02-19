using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Lynx
{
    public partial class Settings : Form
    {
        static string drive = Path.GetPathRoot(Application.StartupPath);
        static string folder = drive + @"Program Files\HomeBrewGames\Lynx\";
        string settings = folder + "Settings.cfg";
        Form1 form1;

        public Settings(Form1 f)
        {
            InitializeComponent();
            form1 = f;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);

            StreamWriter sw = File.CreateText(settings);

            sw.WriteLine(cb1.Checked.ToString());
            sw.WriteLine(cb2.Checked.ToString());

            sw.Close();

            form1.autoloadscripts = cb1.Checked;
            form1.autoloadreferences = cb2.Checked;

            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}