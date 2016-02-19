using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Collections;
using Microsoft.Win32;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace Lynx
{
    public partial class xzpform : Form
    {
        Form1 form1;
        public struct XZP
        {
            public string Filename;
            public string Filepath;
        }
        XZP xzp = new XZP();

        public struct ItemInfo
        {
            public int ID;

            public int Filesize;
            public int FilesizeOffset;

            public int Offset;           
            public int OffsetOffset;

            public int ID2;

            public int FilepathOffset;
            public int FilepathOffsetOffset;

            public int CreationTime;
            public string Filepath;

            public int NodeIndex;
        }
        ArrayList ItemsArray = new ArrayList();
        
        TreeView tv3 = new TreeView();

        static string 
        drive = Path.GetPathRoot(Application.StartupPath),
        folder = drive + @"Program Files\HomeBrewGames\Lynx\";

        [DllImport("shell32.dll", EntryPoint = "ExtractIconA")]
        static extern int ExtractIcon(int hInst, string lpszExeFileName, int nIconIndex);

        public xzpform(Form1 f, string filepath)
        {
            InitializeComponent();
            form1 = f;
            xzp.Filepath = filepath;
            xzp.Filename = Path.GetFileNameWithoutExtension(filepath);
        }

        private void listv_Resize(object sender, EventArgs e)
        {
            int width = listv.Width / 5;
            listv.Columns[0].Width = width * 4 -12;
            listv.Columns[1].Width = width -12;
        }

        private void Form6_Load(object sender, EventArgs e)
        {
            int width = listv.Width / 5;
            listv.Columns[0].Width = width * 4 - 12;
            listv.Columns[1].Width = width - 12;

            LoadXZP();
        }

        public void LoadXZP()
        {
            BinaryReader br = new BinaryReader(new FileStream(xzp.Filepath, 
                FileMode.Open, FileAccess.Read, FileShare.ReadWrite));

            br.BaseStream.Position = 0;

            int ID = br.ReadInt32();
            int unk1 = br.ReadInt32();
            int NumFiles = br.ReadInt32();
            int NumFilesText = br.ReadInt32();
            int unk2 = br.ReadInt32();
            int unk3 = br.ReadInt32();
            int NumFileNames = br.ReadInt32();
            int FileInfoOffset = br.ReadInt32();
            int FATtableSize = br.ReadInt32();

            ItemsArray = new ArrayList();
            for (int x = 0; x < NumFilesText; x++)
            {
                ItemInfo inf = new ItemInfo();
                inf.ID = br.ReadInt32();

                inf.Filesize = br.ReadInt32();
                inf.FilesizeOffset = (int)br.BaseStream.Position - 4;

                inf.Offset = br.ReadInt32();
                inf.OffsetOffset = (int)br.BaseStream.Position - 4;

                ItemsArray.Add(inf);
            }

            br.BaseStream.Position = FileInfoOffset;

            for (int x = 0; x < NumFilesText; x++)
            {
                ItemInfo inf = (ItemInfo)ItemsArray[x];
                inf.ID2 = br.ReadInt32();

                inf.FilepathOffset = br.ReadInt32();
                inf.FilepathOffsetOffset = (int)br.BaseStream.Position - 4;

                inf.CreationTime = br.ReadInt32();

                ItemsArray[x] = inf;
            }

            for (int x = 0; x < ItemsArray.Count; x++)
            {
                ItemInfo inf = (ItemInfo)ItemsArray[x];
                br.BaseStream.Position = inf.FilepathOffset;

                char chr;
                while ((chr = br.ReadChar()) != '\0')
                    inf.Filepath += chr;

                ItemsArray[x] = inf;
            }
            br.Close();

            tv3.Nodes.Clear();

            TreeNode parent = new TreeNode();
            parent.Text = "Root";
            tv3.Nodes.Add(parent);

            for (int z = 0; z < ItemsArray.Count; z++)
            {
                ItemInfo inf = (ItemInfo)ItemsArray[z];

                string[] parts = inf.Filepath.Split('\\');
                parent = tv3.Nodes[0];

                for (int x = 0; x < parts.Length; x++)
                {
                    if (x == parts.Length - 1)
                    {
                        TreeNode tn = new TreeNode();
                        tn.Text = parts[x];
                        inf.NodeIndex = parent.Nodes.Count;
                        tn.Tag = inf;
                        parent.Nodes.Add(tn);
                    }
                    else
                    {
                        bool exists = false;
                        foreach (TreeNode tn in parent.Nodes)
                        {
                            if (tn.Text == parts[x])
                            {
                                exists = true;
                                parent = tn;
                                break;
                            }
                        }
                        if (!exists)
                        {
                            ItemInfo i = new ItemInfo();
                            i.NodeIndex = parent.Nodes.Count;

                            TreeNode tn = new TreeNode();
                            tn.Text = parts[x];
                            tn.Tag = i;

                            parent.Nodes.Add(tn);
                            parent = tn;
                        }
                    }
                }
            }

            tv3.SelectedNode = tv3.Nodes[0];
            LoadDisplay();
        }

        public void LoadDisplay()
        {
            listv.Items.Clear();
            listv.Sorting = SortOrder.Ascending;

            ArrayList lv = new ArrayList();

            foreach (TreeNode tn in tv3.SelectedNode.Nodes)
            {
                if (tn.Nodes.Count == 0) //a file
                {
                    ItemInfo inf = (ItemInfo)tn.Tag;

                    ListViewItem a = new ListViewItem();
                    a.Text = tn.Text;
                    a.Tag = inf;

                    //this will get the image it will use
                    try
                    {
                        string ext = Path.GetExtension(inf.Filepath);

                        //go into registry here
                        RegistryKey reg = Registry.ClassesRoot.CreateSubKey(ext);
                        string type = reg.GetValue("").ToString();

                        reg = Registry.ClassesRoot.CreateSubKey(type + "\\DefaultIcon");

                        string strloc = reg.GetValue("").ToString();
                        int indx = Convert.ToInt32(strloc.Substring(strloc.LastIndexOf(",") + 1));
                        int iconID = ExtractIcon(this.Handle.ToInt32(), strloc.Remove(strloc.LastIndexOf(",")), indx);
                        imageList1.Images.Add(new Bitmap(Icon.FromHandle(new IntPtr(iconID)).ToBitmap()));
                        a.ImageIndex = imageList1.Images.Count - 1;

                        reg.Close();
                    }
                    catch
                    {
                        a.ImageIndex = 2;
                    }

                    ListViewItem.ListViewSubItem b = new ListViewItem.ListViewSubItem();
                    if (inf.Filesize >= 1024) b.Text = (inf.Filesize / 1024).ToString() + " KB";
                    else b.Text = inf.Filesize.ToString() + " B";

                    a.SubItems.Add(b);
                    listv.Items.Add(a);
                }
            }

            for (int x = 0; x < listv.Items.Count; x++) lv.Add(listv.Items[x]);
            listv.Items.Clear();

            foreach (TreeNode tn in tv3.SelectedNode.Nodes)
            {
                if (tn.Nodes.Count > 0) //a directory
                {
                    ItemInfo inf = (ItemInfo)tn.Tag;
                    ListViewItem a = new ListViewItem();
                    a.Text = tn.Text;
                    a.Tag = inf;
                    a.ImageIndex = 0;

                    listv.Items.Add(a);
                }
            }

            listv.Sorting = SortOrder.None;

            if (tv3.SelectedNode.Parent != null)
            {
                ItemInfo i = new ItemInfo();
                i.NodeIndex = -1;

                ListViewItem a = new ListViewItem();
                a.Font = new Font(a.Font.FontFamily, a.Font.Size, FontStyle.Bold);
                a.ForeColor = Color.Teal;
                a.Text = "Parent Directory..";
                a.Tag = i;
                a.ImageIndex = 3;
                listv.Items.Insert(0, a);
            }

            for (int x = 0; x < lv.Count; x++) listv.Items.Add((ListViewItem)lv[x]);
        }

        public string GenerateFile()
        {
            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);

            string str = "";
            int cnt = 0;
            bool Unfinished = true;
            while (Unfinished)
            {
                str = folder + "dump " + cnt.ToString() + ".txt";
                if (!File.Exists(str)) Unfinished = false;
                else cnt++;
            }
            return str;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ExtractFile(null, true);
        }

        public void ExtractFile(string extract2, bool msg)
        {
            if (listv.FocusedItem != null)
            {
                if (extract2 == null)
                {
                    string ext = listv.FocusedItem.Text.Substring(listv.FocusedItem.Text.LastIndexOf(".") + 1);
                    string name = listv.FocusedItem.Text.Remove(listv.FocusedItem.Text.LastIndexOf("."));
                    s.Filter = ext + " files (*." + ext + ")|*." + ext + "|All Files (*.*)|*.*";
                    s.FileName = name;
                    if (s.ShowDialog() == DialogResult.Cancel) return;
                    extract2 = s.FileName;
                }

                BinaryReader br = new BinaryReader(new FileStream(xzp.Filepath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite));

                ItemInfo inf = (ItemInfo)listv.FocusedItem.Tag;
                br.BaseStream.Position = inf.Offset;
                byte[] ba = new byte[inf.Filesize];
                ba = br.ReadBytes(inf.Filesize);
                BinaryWriter bw = new BinaryWriter(new FileStream(extract2, FileMode.Create, FileAccess.Write, FileShare.ReadWrite));
                bw.Write(ba);
                
                bw.Close();
                br.Close();
               
                if (msg) MessageBox.Show(listv.FocusedItem.Text + " extracted successfully!", "", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //continue if we have a valid focused item
            if (listv.FocusedItem != null)
            {
                //stop if the user presses cancel
                if (ofd.ShowDialog() == DialogResult.Cancel) return;

                //the info about the selected item
                ItemInfo fis = (ItemInfo)listv.FocusedItem.Tag;

                //open the xzp in a writer
                BinaryWriter bw = new BinaryWriter(new FileStream(xzp.Filepath, FileMode.Open, FileAccess.Write, FileShare.ReadWrite));
               
                //open the file we're injecting in a reader
                BinaryReader br = new BinaryReader(new FileStream(ofd.FileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite));
                int brlength = (int)br.BaseStream.Length;

                //--------------------------------------INJECT
                if (brlength == fis.Filesize)
                    NormalInject(br, brlength, bw, fis, ofd.FileName);

                else if (brlength < fis.Filesize)
                    SmallerInject(br, brlength, bw, fis, ofd.FileName);

                else if (brlength > fis.Filesize)
                    BiggerInject(br, brlength, bw, fis, ofd.FileName);
                //----------------------------------------------
            }
        }

        public void NormalInject(BinaryReader br, int brlength, BinaryWriter bw, ItemInfo fis, string filepath)
        {
            br.BaseStream.Position = 0;
            byte[] ba = new byte[brlength];
            ba = br.ReadBytes(brlength);

            bw.BaseStream.Position = fis.Offset;
            bw.Write(ba);

            br.Close();
            bw.Close();

            MessageBox.Show(Path.GetFileName(filepath) + " injected successfully!", "Normal Inject", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void SmallerInject(BinaryReader br, int brlength, BinaryWriter bw, ItemInfo fis, string filepath)
        {
            br.BaseStream.Position = 0;
            byte[] ba = new byte[brlength];
            ba = br.ReadBytes(brlength);

            bw.BaseStream.Position = fis.Offset;
            bw.Write(ba);

            //record how much bigger the new file is
            int diff = fis.Filesize - brlength;

            byte byt = 0;
            for (int x = 0; x < diff; x++) bw.Write(byt);

            br.Close();
            bw.Close();

            MessageBox.Show(Path.GetFileName(filepath) + " injected successfully!", "Smaller Inject", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void BiggerInject(BinaryReader br, int brlength, BinaryWriter bw, ItemInfo fis, string filepath)
        {
            //open up the xzp in a reader
            BinaryReader br2 = new BinaryReader(new FileStream(xzp.Filepath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite));

            //record the length of the xzp
            int br2length = (int)br2.BaseStream.Length;

            //record how much bigger the new file is
            int BiggerBy = brlength - fis.Filesize;

            string tmpfile = GenerateFile(); //open up the tmpfile in a writer
            BinaryWriter bw2 = new BinaryWriter(new FileStream(tmpfile, FileMode.Create, FileAccess.Write, FileShare.ReadWrite));

            //in the xzp; goto the end of the file we're overwriting
            br2.BaseStream.Position = (fis.Offset + fis.Filesize);

            //record the length from our position to the end of the map
            int readcount = (int)bw.BaseStream.Length - (fis.Offset + fis.Filesize);
            pb1.Maximum = readcount;

            //the num of bytes we will read
            int number = 100000;
            byte[] byte1 = new byte[number];

            //while were not at the end of the map
            while (br2.BaseStream.Position < br2length)
            {
                //the length from our position to the end of the map
                long diff = br2length - br2.BaseStream.Position;

                //if the lengh is smalle rthan number
                if (diff < number)
                {
                    //read only the difference and write it to the tmpfile
                    byte1 = new byte[diff];
                    byte1 = br2.ReadBytes((int)diff);
                    bw2.Write(byte1);
                    pb1.Value += (int)diff;
                    Application.DoEvents();
                }
                else //read numberbytes and write them to the tmpfile
                {
                    byte1 = br2.ReadBytes(number);
                    bw2.Write(byte1);
                    pb1.Value += number;
                    Application.DoEvents();
                }
            }

            bw2.Close();

            //the file we are injecting
            br.BaseStream.Position = 0;
            byte[] ba = new byte[brlength];
            ba = br.ReadBytes(brlength);

            //write it in
            bw.BaseStream.Position = fis.Offset;
            bw.Write(ba);

            BinaryReader br3 = new BinaryReader(new FileStream(tmpfile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite));
            pb1.Value = 0;
            byte[] byte2 = new byte[number];

            //while were not at the end of the tmpfile
            while (br3.BaseStream.Position < readcount)
            {
                //the length from our position to the end of the tmpfile
                long diff = readcount - br3.BaseStream.Position;

                //if the lengh is smalle rthan number
                if (diff < number)
                {
                    //read only the difference and write it to the map
                    byte2 = new byte[diff];
                    byte2 = br3.ReadBytes((int)diff);
                    bw.Write(byte2);
                    pb1.Value += (int)diff;
                    Application.DoEvents();
                }
                else //read numberbytes and write them to the map
                {
                    byte2 = br3.ReadBytes(number);
                    bw.Write(byte2);
                    pb1.Value += number;
                    Application.DoEvents();
                }
            }
            //Go on, git
            br3.Close();

            //write in the new filesize
            fis.Filesize = brlength;
            bw.BaseStream.Position = fis.FilesizeOffset;
            bw.Write(fis.Filesize);

            //reset the progressbar
            pb1.Value = 0;
            pb1.Maximum = ItemsArray.Count;

            //go through all the items
            for (int x = 0; x < ItemsArray.Count; x++)
            {
                ItemInfo box = (ItemInfo)ItemsArray[x];

                //and write their new offset
                box.Offset += BiggerBy;
                bw.BaseStream.Position = box.OffsetOffset;
                bw.Write(box.Offset);

                //as well as their new filepathoffset
                box.FilepathOffset += BiggerBy;
                bw.BaseStream.Position = box.FilepathOffsetOffset;
                bw.Write(box.FilepathOffset);

                pb1.Value = x;
                Application.DoEvents();
            }

            br.Close();
            bw.Close();

            string str;
            if (brlength >= 1024) str = (brlength / 1024f).ToString() + " KB";
            else str = brlength.ToString() + " B";
            listv.FocusedItem.SubItems[1].Text = str;
            File.Delete(tmpfile);
            MessageBox.Show(Path.GetFileName(filepath) + " injected successfully!", "Bigger Inject", MessageBoxButtons.OK, MessageBoxIcon.Information);
            pb1.Value = 0;
        }

        private void listv_DoubleClick(object sender, EventArgs e)
        {
            if (listv.FocusedItem != null)
            {
                int index = ((ItemInfo)listv.FocusedItem.Tag).NodeIndex;
                if (index == -1)
                {
                    if (tv3.SelectedNode != null && tv3.SelectedNode.Parent != null)
                    {
                        tv3.SelectedNode = tv3.SelectedNode.Parent;
                        LoadDisplay();
                        button1.Enabled = false;
                        button2.Enabled = false;
                    }
                }
                else
                {
                    if (tv3.SelectedNode.Nodes[index].Nodes.Count > 0)
                    {
                        tv3.SelectedNode = tv3.SelectedNode.Nodes[index];
                        LoadDisplay();
                    }
                    else
                    {
                        if (listv.FocusedItem.ImageIndex != 2)
                        {
                            try
                            {
                                string ext = Path.GetExtension(((ItemInfo)listv.FocusedItem.Tag).Filepath);

                                //go into registry here
                                RegistryKey reg = Registry.ClassesRoot.CreateSubKey(ext);
                                string type = reg.GetValue("").ToString();

                                reg = Registry.ClassesRoot.CreateSubKey(type + @"\shell\open\command");

                                string lok = GenerateFile();
                                ExtractFile(lok, false);

                                string str1 = reg.GetValue("").ToString().Replace(" %1", "");

                                Process p = new Process();
                                p.StartInfo.FileName = str1;
                                p.StartInfo.Arguments = lok;
                                p.Start();

                                reg.Close();
                            }
                            catch
                            { }
                        }
                    }
                }
            }
        }

        private void listv_SelectedIndexChanged(object sender, EventArgs e)
        {
            button1.Enabled = (listv.FocusedItem.Tag != null);
            button2.Enabled = (listv.FocusedItem.Tag != null);
        }

        private void Form6_Activated(object sender, EventArgs e)
        {
            form1.FocusedForm = this;
        }

        private void Form6_FormClosing(object sender, FormClosingEventArgs e)
        {
            foreach (ToolStripMenuItem tool in form1.rftool.DropDownItems)
                if (tool.Text == this.Text) return;

            ToolStripMenuItem t = new ToolStripMenuItem();
            t.Text = this.Text;
            t.Click += new EventHandler(form1.tsm_Click);
            form1.rftool.DropDownItems.Add(t);

            if (form1.rftool.DropDownItems.Count > 5)
                form1.rftool.DropDownItems.RemoveAt(0);

            form1.SaveRecentFiles();
        }

        private void label13_Click(object sender, EventArgs e)
        {

        }
    }
}