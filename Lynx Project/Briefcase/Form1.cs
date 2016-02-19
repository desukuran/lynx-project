using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;

namespace Lynx
{
    public partial class Form1 : Form
    {
        ToolStripMenuItem[] tools;
        public Form FocusedForm;

        //our form2 and form3
        LevelInfo lvlinfo;
        Help scripthelp;
        Settings settingsform;

        static string drive = Path.GetPathRoot(Application.StartupPath);
        static string folder = drive + @"Program Files\HomeBrewGames\Lynx\";
        string recent = folder + "RecentFiles.cfg";
        string settings = folder + "Settings.cfg";

        public bool autoloadscripts = false;
        public bool autoloadreferences = false;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadRecentFiles();

            if (File.Exists(settings))
            {
                StreamReader sr = File.OpenText(settings);
                autoloadscripts = Convert.ToBoolean(sr.ReadLine());
                autoloadreferences = Convert.ToBoolean(sr.ReadLine());
                sr.Close();
            }

            //if we were opened by opening a .bsp, load it
            string str1 = Environment.CommandLine.Substring(Environment.CommandLine.LastIndexOf("\"") + 2);
            if (str1.Length > 0) OpenMap(str1);
        }

        public void SaveRecentFiles()
        {
            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);

            StreamWriter sw = File.CreateText(recent);
            for (int x = 0; x < rftool.DropDownItems.Count; x++)
                sw.WriteLine(rftool.DropDownItems[x].Text);

            sw.Close();
        }

        public void LoadRecentFiles()
        {
            if (!File.Exists(recent)) return;

            StreamReader sr = File.OpenText(recent);
            while (!sr.EndOfStream)
            {
                ToolStripMenuItem tsm = new ToolStripMenuItem();
                tsm.Text = sr.ReadLine();
                tsm.Click += new EventHandler(tsm_Click);
                rftool.DropDownItems.Add(tsm);
            }
            sr.Close();
        }


        //This is where fun begins and ends.
        public void tsm_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem tsm = (ToolStripMenuItem)sender;
            if (File.Exists(tsm.Text))
            {
                if (Path.GetExtension(tsm.Text) == ".bsp")
                {
                    if (!Opened(tsm.Text)) OpenMap(tsm.Text);
                }
                else if (Path.GetExtension(tsm.Text) == ".xzp")
                {
                    if (!Opened(tsm.Text)) OpenXZP(tsm.Text);
                }
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //dont continue if the user presses cancel
            if (ofd.ShowDialog() == DialogResult.Cancel) return;

            if (Path.GetExtension(ofd.FileName) == ".bsp")
            {
                foreach (string str in ofd.FileNames)
                    if (!Opened(str)) OpenMap(str);
            }
            else if (Path.GetExtension(ofd.FileName) == ".xzp")
            {
                foreach (string str in ofd.FileNames)
                    if (!Opened(str)) OpenXZP(str);
            }
        }

        public bool Opened(string str)
        {
            foreach (Form f in this.MdiChildren)
            {
                if (f.Text == str)
                    return true;
            }
            return false;
        }

        public void OpenMap(string str)
        {
            Mapform f4 = new Mapform(this, str);
            f4.Text = str;
            f4.MdiParent = this;
            f4.Show();
        }

        public void OpenXZP(string str)
        {
            xzpform f6 = new xzpform(this, str);
            f6.Text = str;
            f6.MdiParent = this;
            f6.Show();
        }

        //BSPS ARE IN LYNX
        private void etool_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to make Lynx the default editor for .bsp's?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                try
                {
                    RegistryKey reg = Registry.ClassesRoot.CreateSubKey(@"Prodigy\Lynx_v1");
                    reg.SetValue("", "Half Life 2 Map");

                    reg = Registry.ClassesRoot.CreateSubKey(@"Prodigy\Lynx_v1\shell\open\command");
                    reg.SetValue("", "\"" + Application.ExecutablePath + "\" %1");

                    reg = Registry.ClassesRoot.CreateSubKey(@"Prodigy\Lynx_v1\DefaultIcon");
                    reg.SetValue("", Application.ExecutablePath + ",0");

                    reg = Registry.ClassesRoot.CreateSubKey(".bsp");
                    reg.SetValue("", @"Prodigy\Lynx_v1");

                    reg.Close();
                    label1.Text = "Lynx made default editor for bsp's";
                    Application.DoEvents();
                    MessageBox.Show("Lynx is now the default editor for .bsp's :)", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private void dtool_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to stop Lynx from being the default editor for .bsp's?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                try
                {
                    RegistryKey reg = Registry.ClassesRoot.CreateSubKey("Prodigy");
                    reg.DeleteSubKeyTree("Lynx_v1");
                    reg = Registry.ClassesRoot.CreateSubKey(".bsp");
                    reg.SetValue("", "");

                    reg.Close();
                    label1.Text = "Lynx stopped from being the default editor for bsp's";
                    Application.DoEvents();
                    MessageBox.Show("Lynx is no longer the default editor for .bsp's.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        //on opening check if Lynx is the default editor for .bsp's, tick accordingly
        private void extrasToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            RegistryKey reg = Registry.ClassesRoot.CreateSubKey(".bsp");
            brieftool.Checked = (reg.GetValue("", "").ToString() == @"Prodigy\Lynx_v1");
            reg.Close();
        }

        private void levelInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lvlinfo = new LevelInfo();
            lvlinfo.Show();
        }

        private void scriptEditorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            scripthelp = new Help();
            scripthelp.Show();
        }

        private void tileHorizontallyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void tileVerticallyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void cascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void windowsToolStripMenuItem_DropDownOpened(object sender, EventArgs e)
        {
            tools = new ToolStripMenuItem[this.MdiChildren.Length];
            if (this.MdiChildren.Length > 0)
            {
                toolStripSeparator3.Visible = true;
                int count = 0;

                foreach (Form f in this.MdiChildren)
                {
                    ToolStripMenuItem tm = new ToolStripMenuItem();
                    tm.Text = f.Text;
                    tm.Tag = f;
                    tm.Click += new EventHandler(tm_Click);
                    if (f == FocusedForm) tm.Checked = true;
                    tools[count] = tm;
                    count++;
                }
                windowsToolStripMenuItem.DropDownItems.AddRange(tools);
            }
            else toolStripSeparator3.Visible = false;
        }

        void tm_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem tm = (ToolStripMenuItem)sender;
            Form f = (Form)tm.Tag;
            f.Focus();
        }

        private void windowsToolStripMenuItem_DropDownClosed(object sender, EventArgs e)
        {
            for (int x = tools.Length - 1; x >= 0; x--)
            {
                windowsToolStripMenuItem.DropDownItems.RemoveAt(4 + x);
            }
            toolStripSeparator3.Visible = false;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            settingsform = new Settings(this);
            settingsform.cb1.Checked = autoloadscripts;
            settingsform.cb2.Checked = autoloadreferences;
            settingsform.Show();
        }

        private void creditToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Creator: Prodigy" + Environment.NewLine +
                Environment.NewLine + "Thanks goes to:" + Environment.NewLine +
                "Andrew Koester for the document on the structure of the XZP", "And the credit goes to...", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}