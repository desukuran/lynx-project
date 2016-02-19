using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Collections;

namespace Lynx
{
    public partial class Mapform : Form
    {
        Form1 form1;

        //----------------MISC
        public struct Map
        {
            public string Filename;
            public string Filepath;
        }
        Map map = new Map();

        static string
        drive = Path.GetPathRoot(Application.StartupPath),
        folder = drive + @"Program Files\HomeBrewGames\Lynx\";

        //----------------SCRIPT
        List<UniqueClassname> ListofUniqueClassnames = new List<UniqueClassname>();
        Pos pos = new Pos();
        Script script = new Script();
        ArrayList ScriptClasses = new ArrayList();
        int FindStart, LastStart;
        string LastSearch;
        bool FstTime1 = true, FstTime2 = true, OverLabel = false, ClassChangesMade = false;
        Panel pp = new Panel();
        Label lbl;
        public struct Script
        {
            public bool Loaded;
            public long Offset;
            public long Length;
            public int Classes;
            public bool AtEndofMap;
            public string Filepath;
            public List<byte> ByteArray;
            public bool Nloaded;
            public bool BDloaded;
        }
        public struct UniqueClassname
        {
            public string UniqueName;
            public ArrayList ContainingNodes;
        }
        public struct Node
        {
            public ArrayList Names;
            public ArrayList Values;
        }
        public struct Pos
        {
            public TreeNode Parent;
            public int Index;
        }

        //----------------REFERENCES
        public struct References
        {
            public bool Loaded;
            public string XZPfilepath;
            public int Offset;
            public int Amount;
            public int Size;
            public ArrayList RefArray;
            public bool EIloaded;
        }
        References references = new References();

        public Mapform(Form1 f, string filepath)
        {
            InitializeComponent();

            form1 = f;
            map.Filepath = filepath;
            map.Filename = Path.GetFileNameWithoutExtension(filepath);
            pp.Height = 15;
            pp.Width = 0;
        }

        #region Misc

        public void Panels(Panel p)
        {
            if (!p.Visible)
            {
                foreach (Control box in splitContainer1.Panel2.Controls)
                    box.Visible = (box == p);
            }
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

        private void Form4_Activated(object sender, EventArgs e)
        {
            form1.FocusedForm = this;
        }

        private void Form4_FormClosing(object sender, FormClosingEventArgs e)
        {
            foreach (ToolStripMenuItem tsmi in form1.rftool.DropDownItems)
                if (tsmi.Text == map.Filepath) return;

            ToolStripMenuItem tsm = new ToolStripMenuItem();
            tsm.Text = map.Filepath;
            tsm.Click += new EventHandler(form1.tsm_Click);
            form1.rftool.DropDownItems.Add(tsm);

            if (form1.rftool.DropDownItems.Count > 5)
                form1.rftool.DropDownItems.RemoveAt(0);

            form1.SaveRecentFiles();
        }

        public string StrReverse(string str)
        {
            string rtnstr = "";

            for (int x = str.Length - 1; x >= 0; x--)
            {
                rtnstr += str.Substring(x, 1);
            }
            return rtnstr;
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            tv1.ExpandAll();

            if (form1.autoloadscripts) LoadScript(false);
            Application.DoEvents();
            if (form1.autoloadreferences) LoadReferences();
        }

        private void tv1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (tv1.SelectedNode == tv1.Nodes[0])
                Panels(sepanel);

            //normal
            else if (tv1.SelectedNode == tv1.Nodes[0].Nodes[0].Nodes[0])
                Panels(seviewnormpanel);

            //grouped
            else if (tv1.SelectedNode == tv1.Nodes[0].Nodes[0].Nodes[1])
                Panels(seviewgpanel);

            if (tv1.SelectedNode == tv1.Nodes[1])
                Panels(repanel);

            if (tv1.SelectedNode == tv1.Nodes[1].Nodes[0])
                Panels(reeipanel);

        }

        #endregion

        #region Script Tab

        private void button5_Click(object sender, EventArgs e)
        {
            LoadScript(true);
        }

        public void LoadScript(bool empty)
        {
            form1.label1.Text = "Loading Script...";
            Application.DoEvents();

            progressBar2.Maximum = 4;
            script = new Script();

            //open the map in a reader
            BinaryReader br = new BinaryReader(new FileStream(map.Filepath,
                FileMode.Open, FileAccess.Read, FileShare.ReadWrite));

            //store the start and length of the script
            br.BaseStream.Position = 8;
            script.Offset = br.ReadInt32();
            script.Length = br.ReadInt32();

            progressBar2.Value++;
            Application.DoEvents();

            //if the script.start + script.length = the length of the map, return true
            script.AtEndofMap = (script.Offset + script.Length == br.BaseStream.Length);

            progressBar2.Value++;
            Application.DoEvents();

            br.BaseStream.Position = script.Offset;
            script.ByteArray = new List<byte>();

            progressBar2.Value++;
            Application.DoEvents();

            int x = 0;
            while (x < script.Length)
            {
                x++;
                byte a = br.ReadByte();
                if (a == 10) //this is that square symbol
                {
                    //13 and 10 are a new line
                    a = 13;
                    script.ByteArray.Add(a);
                    a = 10;
                    script.ByteArray.Add(a);
                }
                else if (a == 0) //nothingness
                {
                    //64 is a @
                    a = 64; script.ByteArray.Add(a);
                }
                else script.ByteArray.Add(a);

                //125 is a }
                if (a == 125) script.Classes++;
            }
            br.Close();

            progressBar2.Value++;
            Application.DoEvents();

            label1.Text = "Script Offset: " + script.Offset.ToString();
            label2.Text = "Script Length: " + script.Length.ToString() + " Bytes";
            if (script.AtEndofMap) label6.Text = "Position in Map: End";
            else label6.Text = "Position in Map: Inside";
            label3.Text = "Classes in Script: " + script.Classes.ToString();

            if (empty)
            {
                pos.Index = -1;
                scriptbox.Text = "";
                tv2.Nodes.Clear();
                panel3.Controls.Clear();
            }

            button5.Text = "Reload Script";
            ClassChangesMade = false;
            script.Loaded = true;
            progressBar2.Value = 0;
            form1.label1.Text = "Script Loaded";
        }

        public void InjectScript(List<byte> bscript)
        {
            string msgstr = "Script saved successfully";
            string labtxt = "Script saved";

            bool dsf = true;
            if (bscript.Count < script.Length) //smaller
            {
                DialogResult dr = new DialogResult();

                if (!script.AtEndofMap) //if the script isnt at the end of the map
                    dr = MessageBox.Show("The new script is too small, insert blanks?", "",
                        MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation);

                if (dr == DialogResult.Cancel)
                    dsf = false;

                else if (dr == DialogResult.Yes) //insert blanks
                {
                    int BlanksToInsert = (int)script.Length - bscript.Count;
                    byte c = 0; //a nothingness
                    for (int x = 0; x < BlanksToInsert; x++)
                    {
                        bscript.Add(c);
                    }
                    msgstr = "Blanks inserted and script saved successfully!";
                    labtxt = "Blanks inserted and script saved";
                }
            }
            else if (bscript.Count > script.Length) //bigger
            {
                DialogResult dr = new DialogResult();

                if (!script.AtEndofMap) //if the script isnt at the end of the map
                    dr = MessageBox.Show("The new script is too big, save it to the end of the map instead?", "",
                        MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation);

                if (dr == DialogResult.Cancel)
                    dsf = false;

                //if the dr = yes or the script is at the end of the map..
                else if (dr == DialogResult.Yes || script.AtEndofMap)
                {
                    //open the map in a writer
                    BinaryWriter bwr = new BinaryWriter(new FileStream(map.Filepath, FileMode.Open,
                        FileAccess.Write, FileShare.ReadWrite));

                    long len = bwr.BaseStream.Length;

                    if (script.AtEndofMap) len = script.Offset;

                    bwr.BaseStream.Seek(len, SeekOrigin.Begin);
                    bwr.Write(bscript.ToArray()); //write the script at the end of the map

                    //correct offset 8 and 12
                    script.Offset = len;
                    script.Length = bscript.Count;

                    bwr.BaseStream.Position = 8;
                    bwr.Write(script.Offset);

                    bwr.BaseStream.Position = 12;
                    bwr.Write(script.Length);

                    dsf = false;
                    bwr.Close();

                    script.AtEndofMap = true;

                    if (!script.AtEndofMap)
                    {
                        labtxt = "Script position in map re-allocated to the end of the map and script saved";
                        msgstr = "Script position in map re-allocated to the end of the map and script saved successfully!";
                    }

                    form1.label1.Text = labtxt;
                    Application.DoEvents();
                    MessageBox.Show(msgstr, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

            if (dsf)
            {
                if (script.AtEndofMap)
                {
                    BinaryReader byr = new BinaryReader(new FileStream(map.Filepath, FileMode.Open,
                        FileAccess.Read, FileShare.ReadWrite));

                    byte[] bar = new byte[byr.BaseStream.Length];
                    bar = byr.ReadBytes((int)script.Offset);
                    byr.Close();

                    BinaryWriter byw = new BinaryWriter(new FileStream(map.Filepath, FileMode.Create,
                        FileAccess.Write, FileShare.ReadWrite));

                    byw.Write(bar);
                    byw.Write(bscript.ToArray());
                    Application.DoEvents();

                    script.Length = bscript.Count;
                    byw.BaseStream.Position = 12;
                    byw.Write(script.Length);

                    byw.Close();
                }
                else //if the script isnt at the end of the map
                {
                    BinaryWriter bw = new BinaryWriter(new FileStream(map.Filepath, FileMode.Open,
                        FileAccess.Write, FileShare.ReadWrite));
                    bw.BaseStream.Seek(script.Offset, SeekOrigin.Begin);

                    bw.Write(bscript.ToArray());
                    bw.Close();
                }

                form1.label1.Text = labtxt;
                Application.DoEvents();
                MessageBox.Show(msgstr, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        #region Normal View

        private void seviewnormpanel_VisibleChanged(object sender, EventArgs e)
        {
            if (script.Loaded && !script.Nloaded && seviewnormpanel.Visible)
            {
                script.Nloaded = true;

                StreamReader sr = new StreamReader(new MemoryStream(script.ByteArray.ToArray()));
                scriptbox.Text = sr.ReadToEnd();
                sr.Close();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Find();
            button3.Focus();
        }

        public void Find()
        {
            //find the word
            int pos = scriptbox.Text.IndexOf(findbox.Text, FindStart, scriptbox.TextLength - FindStart);

            if (pos == -1) //we get -1 if the word doesnt exist
                if (FindStart == 0) return; //return if it searched the whole document
                //else search again from the start of the document
                else pos = scriptbox.Text.IndexOf(findbox.Text, 0, scriptbox.TextLength);

            if (pos != -1) //if the word was found
            {
                scriptbox.SelectionStart = pos;
                scriptbox.SelectionLength = findbox.TextLength;
                scriptbox.ScrollToCaret();

                //store the position of the last letter selected
                FindStart = scriptbox.SelectionStart + scriptbox.SelectionLength;
                //remember the text searched for
                LastSearch = findbox.Text;
                //remember the start
                LastStart = FindStart;
                form1.label1.Text = "Found " + findbox.Text;
            }
            else form1.label1.Text = "No more occurrences of " + findbox.Text + " were found";
        }

        private void button9_Click(object sender, EventArgs e)
        {
            scriptbox.Text = scriptbox.Text.Replace(findbox.Text, replacebox.Text);
            form1.label1.Text = findbox.Text + " replaced with " + replacebox.Text;
        }

        private void findbox_TextChanged(object sender, EventArgs e)
        {
            if (findbox.TextLength > 0 && scriptbox.TextLength > 0)
            {
                //if the user's input equals the last search performed...
                if (findbox.Text == LastSearch)
                    FindStart = LastStart; //..make sure we start from the last position

                //else reset the starting position
                else FindStart = 0;

                button3.Enabled = true;
                button9.Enabled = (replacebox.TextLength > 0);
            }
            else
            {
                button3.Enabled = false;
                button9.Enabled = false;
            }
        }

        private void replacebox_TextChanged(object sender, EventArgs e)
        {
            Replace();
        }

        public void Replace()
        {
            button9.Enabled = (scriptbox.TextLength != 0 &&
                findbox.TextLength != 0 && replacebox.TextLength != 0);
        }

        private void scriptbox_TextChanged(object sender, EventArgs e)
        {
            //reset these find variables
            FindStart = 0; LastStart = 0;

            if (scriptbox.TextLength == 0)
            {
                button1.Enabled = false; //save changes
                button2.Enabled = false; //save to file
                button3.Enabled = false; //find
                button9.Enabled = false; //replace
            }
            else
            {
                button1.Enabled = true;
                button2.Enabled = true;

                if (findbox.TextLength == 0)
                    button3.Enabled = false;
                else
                {
                    button3.Enabled = true;
                    button9.Enabled = (replacebox.TextLength > 0);
                }
            }
        }

        private void findbox_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (e.Control && e.KeyCode == Keys.A) tb.SelectAll();

            if (tb == findbox)
                if (e.KeyCode == Keys.Enter) Find();

                else if (tb == replacebox)
                    if (e.KeyCode == Keys.Enter) Replace();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            s.FileName = map.Filename + " script";
            if (s.ShowDialog() == DialogResult.Cancel) return;
            StreamWriter sw = File.CreateText(s.FileName);
            sw.Write(scriptbox.Text);
            sw.Close();
            form1.label1.Text = "Script saved to " + Path.GetFileName(s.FileName);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (o.ShowDialog() == DialogResult.Cancel) return;
            StreamReader sr = File.OpenText(o.FileName);
            scriptbox.Text = sr.ReadToEnd();
            sr.Close();
            form1.label1.Text = Path.GetFileName(s.FileName) + " opened";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string str1 = GenerateFile();
            StreamWriter sw = File.CreateText(str1);
            sw.Write(scriptbox.Text);
            sw.Close();

            BinaryReader br = new BinaryReader(new FileStream(str1, FileMode.Open,
                FileAccess.Read, FileShare.Read));

            long leng = br.BaseStream.Length; //store the length
            br.BaseStream.Seek(0, SeekOrigin.Begin); //goto the start

            //this will store what is going back into the map
            List<byte> input = new List<byte>();

            int cnt = 0;
            for (int x = 0; x < leng; x++)
            {
                byte a = br.ReadByte();
                if (a != 13) //dont include these so the new lines we entered get turned back to squares
                {
                    if (a != 64) input.Add(a);
                    else //if its a @ write a 0 in
                    {
                        a = 0;
                        input.Add(a);
                    }
                    cnt++;
                }
            }

            br.Close();
            File.Delete(str1);

            InjectScript(input);
            Application.DoEvents();

            LoadScript(false);
            script.Nloaded = true;
        }

        #endregion

        #region Grouped View

        private void seviewbdpanel_VisibleChanged(object sender, EventArgs e)
        {
            Application.DoEvents();
            if (script.Loaded && !script.BDloaded && seviewgpanel.Visible)
            {
                script.BDloaded = true;

                button11.Enabled = false;
                button6.Enabled = false;
                button7.Enabled = false;

                ConvertByteArrayToNodes(script.ByteArray.ToArray());
                DisplayNodes();

                form1.label1.Text = "All classes loaded";

                button11.Enabled = (tv2.Nodes.Count > 0);
                button6.Enabled = (tv2.Nodes.Count > 0);
                button7.Enabled = true;
            }
        }

        public void DisplayNodes()
        {
            tv2.Nodes.Clear();
            tv2.Nodes.Add(map.Filename);

            TreeNode worldspawnclassNode = new TreeNode();
            //go through the classname array and add'em all to the list
            foreach (UniqueClassname un in ListofUniqueClassnames)
            {
                TreeNode tn = new TreeNode();
                tn.Text = un.UniqueName;
                tn.Tag = un;

                if (un.UniqueName == "worldspawn") worldspawnclassNode = tn;
                else tv2.Nodes[0].Nodes.Add(tn);
            }
            tv2.Sorted = true;
            tv2.Sorted = false;

            tv2.Nodes[0].Nodes.Insert(0, worldspawnclassNode);

            ListofUniqueClassnames.Clear();

            foreach (TreeNode nd in tv2.Nodes[0].Nodes)
                ListofUniqueClassnames.Add((UniqueClassname)nd.Tag);

            for (int x = 0; x < ListofUniqueClassnames.Count; x++)
            {
                int count = 1;
                foreach (Node nd in ListofUniqueClassnames[x].ContainingNodes)
                {
                    TreeNode t = new TreeNode();
                    t.Text = "Node " + count.ToString();
                    t.Tag = nd;
                    tv2.Nodes[0].Nodes[x].Nodes.Add(t);
                    count++;
                }
            }

            tv2.Nodes[0].Expand();
        }

        public void SaveClassChanges()
        {
            if (ClassChangesMade)
            {
                int cnt = 0, cnt2 = 0;
                UniqueClassname ucn = (UniqueClassname)ListofUniqueClassnames[pos.Parent.Index];
                Node n = (Node)ucn.ContainingNodes[pos.Index];

                foreach (Control box in panel3.Controls)
                {
                    if (box == pp) continue;
                    if (box.GetType() == lbl.GetType())
                    {
                        n.Names[cnt] = box.Text;
                        cnt++;
                    }
                    else
                    {
                        n.Values[cnt2] = box.Text;
                        cnt2++;
                    }
                }
            }
        }

        private void tv2_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (tv2.SelectedNode == null || tv2.SelectedNode.Nodes.Count >= 1) return;

            SaveClassChanges();

            pos.Parent = tv2.SelectedNode.Parent;
            pos.Index = tv2.SelectedNode.Index;
            LoadClass();
        }

        public void LoadClass()
        {
            //get the class from the classes arraylist
            UniqueClassname ucn = (UniqueClassname)ListofUniqueClassnames[pos.Parent.Index];
            Node n = (Node)ucn.ContainingNodes[pos.Index];

            panel3.Controls.Clear();

            int ypos = -15;
            int width = panel3.Width - 50;

            //go through all the classes names
            for (int x = 0; x < n.Names.Count; x++)
            {
                ypos += 30;

                Label l = new Label();
                l.Text = n.Names[x].ToString();
                l.Tag = n.Names[x].ToString();
                l.AutoSize = true;
                l.MouseEnter += new EventHandler(l_MouseEnter);
                l.MouseLeave += new EventHandler(l_MouseLeave);
                l.TextChanged += new EventHandler(ClassTextChanged);
                l.Left = 20;
                l.Top = ypos;

                ypos += 15;

                TextBox t = new TextBox();
                t.Text = n.Values[x].ToString();
                t.Tag = n.Values[x].ToString();
                t.Width = width;
                t.Left = 20;
                t.Top = ypos;
                t.TextChanged += new EventHandler(ClassTextChanged);

                panel3.Controls.Add(l);
                panel3.Controls.Add(t);
            }

            //add in the panel as well so we get a gap at the bottom
            ypos += 20;
            pp.Top = ypos;
            panel3.Controls.Add(pp);
        }

        public void l_MouseEnter(object sender, EventArgs e)
        {
            OverLabel = true;
            lbl = (Label)sender;
        }

        public void l_MouseLeave(object sender, EventArgs e)
        { OverLabel = false; }

        public void ClassTextChanged(object sender, EventArgs e)
        {
            //for every control in the properties panel
            foreach (Control box in panel3.Controls)
            {
                //if its the panel continue on to the next loop
                if (box == pp) continue;
                //if the control (either a label or textbox) label isnt the same as its tag
                if (box.Text != box.Tag.ToString())
                {
                    //allow changes to be saved (as changes have been made)
                    ClassChangesMade = true;
                    return;
                }
            }
            ClassChangesMade = false;
        }

        public void ConvertByteArrayToNodes(byte[] ByteArray)
        {
            MemoryStream ms = new MemoryStream(ByteArray);

            //..open the script in a reader
            StreamReader sr = new StreamReader(ms);

            ListofUniqueClassnames = new List<UniqueClassname>();
            UniqueClassname ucn = new UniqueClassname();
            Node n = new Node();
            n.Names = new ArrayList();
            n.Values = new ArrayList();

            //while we're not at the end of the stream..
            while (!sr.EndOfStream)
            {
                //read and store the current line (also advances the reader to the next line)
                string str = sr.ReadLine();
                if (str == "{") str = sr.ReadLine();
                if (str == " " || str == null || str == "@" || str.Length <= 0) continue; //to the next loop

                //if this is an end to a class..
                if (str == "}")
                {
                    //add the class to our arraylist of classes
                    ucn.ContainingNodes.Add(n);

                    //then re-declare sc
                    n = new Node();
                    n.Names = new ArrayList();
                    n.Values = new ArrayList();
                }
                //if the 1st letter is an apostrophe..
                else if (str.Substring(0, 1) == "\"")
                {
                    string[] splt = str.Split('"');
                    string name = "";
                    string val = "";

                    if (splt.Length > 3)
                    {
                        name = splt[1];
                        val = splt[3];
                    }

                    //if the name = classname; store its value in our classname array
                    if (name == "classname")
                    {
                        bool jk = false;
                        foreach (UniqueClassname un in ListofUniqueClassnames)
                        {
                            if (un.UniqueName == val)
                            {
                                jk = true;
                                ucn = un;
                            }
                        }

                        if (!jk)
                        {
                            UniqueClassname un = new UniqueClassname();
                            un.ContainingNodes = new ArrayList();
                            un.UniqueName = val;
                            ucn = un;
                            ListofUniqueClassnames.Add(ucn);
                        }
                    }
                    else
                    {
                        //else add the name and value to the current class
                        n.Names.Add(name);
                        n.Values.Add(val);
                    }
                }
            }
            sr.Close();
        }

        public void WriteNodesUsingStreamWriter(StreamWriter sw)
        {
            //go through all the classnames
            foreach (UniqueClassname ucn in ListofUniqueClassnames)
            {
                foreach (Node nd in ucn.ContainingNodes)
                {
                    //the starting bracket
                    sw.WriteLine("{");
                    sw.WriteLine("\"classname\" \"" + ucn.UniqueName + "\"");

                    for (int x = 0; x < nd.Names.Count; x++)
                        sw.WriteLine("\"" + nd.Names[x].ToString() + "\" \"" + nd.Values[x].ToString() + "\"");

                    sw.WriteLine("}"); //the closing bracket
                }
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            SaveClassChanges();

            string save2 = GenerateFile();
            StreamWriter sw = File.CreateText(save2);
            WriteNodesUsingStreamWriter(sw);
            sw.Close();

            BinaryReader br = new BinaryReader(new FileStream(save2, FileMode.Open, 
                FileAccess.ReadWrite, FileShare.ReadWrite));

            long leng = br.BaseStream.Length; //store the length
            br.BaseStream.Seek(0, SeekOrigin.Begin); //goto the start
         
            //this will store what is going back into the map
            List<byte> input = new List<byte>();

            for (int x = 0; x < leng; x++)
            {
                byte a = br.ReadByte();
                if (a != 13) //dont include these so the new lines we entered get turned back to squares
                {
                    if (a != 64) input.Add(a);
                    else //if its a @ write a 0 in
                    {
                        a = 0;
                        input.Add(a);
                    }
                }
            }

            br.Close();
            File.Delete(save2);

            InjectScript(input);
            Application.DoEvents();

            LoadScript(false);
            script.BDloaded = true;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            SaveClassChanges();

            s.FileName = map.Filename + " script";
            if (s.ShowDialog() == DialogResult.Cancel) return;

            StreamWriter sw = File.CreateText(s.FileName);
            WriteNodesUsingStreamWriter(sw);
            sw.Close();

            form1.label1.Text = "Script saved to " + Path.GetFileName(s.FileName);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (o.ShowDialog() == DialogResult.Cancel) return;
            tv2.Nodes.Clear();

            BinaryReader br = new BinaryReader(new FileStream(o.FileName, 
                FileMode.Open, FileAccess.Read, FileShare.Read));

            byte[] bytes = br.ReadBytes((int)br.BaseStream.Length);
            br.Close();

            ConvertByteArrayToNodes(bytes);
            DisplayNodes();

            form1.label1.Text = Path.GetFileName(o.FileName) + " opened";
            button11.Enabled = (tv2.Nodes.Count > 0);
            button6.Enabled = (tv2.Nodes.Count > 0);
            button7.Enabled = true;
        }

        private void ctms2_Opening(object sender, CancelEventArgs e)
        {
            if (tv2.SelectedNode != null)
            {
                rtool.Visible = true;
                rmtool.Visible = true;
                stool.Visible = true;
            }
            else
            {
                rtool.Visible = false;
                rmtool.Visible = false;
                stool.Visible = false;
            }
        }

        private void rtool_DropDownOpening(object sender, EventArgs e)
        {
            if (tv2.SelectedNode.Nodes.Count >= 1)
            {
                ttb1.Tag = tv2.SelectedNode.Text;
                ttb1.Text = tv2.SelectedNode.Text;
            }
            else
            {
                ttb1.Tag = tv2.SelectedNode.Parent.Text;
                ttb1.Text = tv2.SelectedNode.Parent.Text;
            }
        }

        private void ttb1_TextChanged(object sender, EventArgs e)
        {
            renamet.Enabled = (ttb1.TextLength > 0 || ttb1.Text != ttb1.Tag.ToString());
        }

        private void renamet_Click(object sender, EventArgs e)
        {
            //need to add in a check if the node already exists
            string name = ttb1.Text;
            bool unique = true;
            UniqueClassname ucn = new UniqueClassname();
            foreach (UniqueClassname uc in ListofUniqueClassnames)
            {
                if (uc.UniqueName == name)
                {
                    unique = false;
                    ucn = uc;
                    break;
                }
            }

            if (unique)
            {
                if (tv2.SelectedNode.Nodes.Count <= 0)
                {
                    ucn.ContainingNodes = new ArrayList();
                    ucn.UniqueName = ttb1.Text;
                    ucn.ContainingNodes.Add((Node)tv2.SelectedNode.Tag);
                    ListofUniqueClassnames.Add(ucn);
                    DisplayNodes();
                    form1.label1.Text = "Node Renamed";
                }
                else
                {
                    Node n = (Node)tv2.SelectedNode.Tag;
                    n.Names[tv2.SelectedNode.Index] = ttb1.Text;
                    tv2.SelectedNode.Text = ttb1.Text;
                    form1.label1.Text = "Nodes Renamed";
                }
            }
            else
            {
                if (tv2.SelectedNode.Nodes.Count <= 0)//item
                {
                    ucn.ContainingNodes.Add((Node)tv2.SelectedNode.Tag);
                    UniqueClassname u = (UniqueClassname)ListofUniqueClassnames[tv2.SelectedNode.Parent.Index];
                    u.ContainingNodes.RemoveAt(tv2.SelectedNode.Index);
                    DisplayNodes();
                    form1.label1.Text = "Node Renamed";
                }
                else //folder
                {
                    UniqueClassname u = (UniqueClassname)ListofUniqueClassnames[tv2.SelectedNode.Index];
                    for (int x = 0; x < u.ContainingNodes.Count; x++)
                        ucn.ContainingNodes.Add(u.ContainingNodes[x]);

                    ListofUniqueClassnames.RemoveAt(tv2.SelectedNode.Index);
                    DisplayNodes();
                    form1.label1.Text = "Nodes Renamed";
                }
            }
        }

        private void rmtool_Click(object sender, EventArgs e)
        {
            if (tv2.SelectedNode.Nodes.Count <= 0)
            {
                if (MessageBox.Show("Are you sure you wish to remove this entire node?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                {
                    UniqueClassname ucn = (UniqueClassname)ListofUniqueClassnames[pos.Parent.Index];
                    ucn.ContainingNodes.RemoveAt(pos.Index);
                    panel3.Controls.Clear();
                    tv2.Nodes.Remove(tv2.SelectedNode);
                    form1.label1.Text = "Node Removed";

                    button11.Enabled = (tv2.Nodes.Count > 0);
                    button6.Enabled = (tv2.Nodes.Count > 0);
                }
            }
            else
            {
                if (MessageBox.Show("Are you sure you wish to remove all these nodes?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                {
                    ListofUniqueClassnames.RemoveAt(tv2.SelectedNode.Index);
                    tv2.Nodes.Remove(tv2.SelectedNode);
                    form1.label1.Text = "Nodes Removed";

                    button11.Enabled = (tv2.Nodes.Count > 0);
                    button6.Enabled = (tv2.Nodes.Count > 0);
                }
            }
        }

        private void ttb2_TextChanged(object sender, EventArgs e)
        {
            addt.Enabled = (ttb2.TextLength > 0);
        }

        private void addt_Click(object sender, EventArgs e)
        {
            string name = ttb2.Text;
            bool Unique = true;
            UniqueClassname un = new UniqueClassname();
            foreach (UniqueClassname ucn in ListofUniqueClassnames)
            {
                if (ucn.UniqueName == name)
                {
                    Unique = false;
                    un = ucn;
                    break;
                }
            }

            int index = 0;
            Node n = new Node();
            n.Names = new ArrayList();
            n.Values = new ArrayList();

            if (Unique)
            {
                un = new UniqueClassname();
                un.ContainingNodes = new ArrayList();
                un.UniqueName = name;
                un.ContainingNodes.Add(n);
                ListofUniqueClassnames.Add(un);
            }
            else
            {
                un.ContainingNodes.Add(n);
                index = ListofUniqueClassnames.IndexOf(un);
            }

            DisplayNodes();
            tv2.SelectedNode = tv2.Nodes[index].LastNode;

            ttb2.Text = "";
            button11.Enabled = (tv2.Nodes.Count > 0);
            button6.Enabled = (tv2.Nodes.Count > 0);
        }

        private void ctms3_Opening(object sender, CancelEventArgs e)
        {
            if (OverLabel)
            {
                rt.Visible = true;
                rmt.Visible = true;

                if (tv2.SelectedNode != null)
                {
                    st.Visible = true;
                    at.Enabled = true;
                }
                else
                {
                    st.Visible = false;
                    at.Enabled = false;
                }
            }
            else
            {
                rt.Visible = false;
                rmt.Visible = false;
                st.Visible = false;
                at.Enabled = (tv2.SelectedNode != null);
            }
        }

        private void rt_DropDownOpening(object sender, EventArgs e)
        {
            ttb3.Text = lbl.Text;
        }

        private void ttb3_TextChanged(object sender, EventArgs e)
        {
            rentool.Enabled = (ttb3.TextLength > 0 || ttb3.Text != lbl.Text);
        }

        private void rentool_Click(object sender, EventArgs e)
        {
            Node n = (Node)tv2.SelectedNode.Tag;
            for (int x = 0; x < panel3.Controls.Count; x++)
            {
                if (panel3.Controls[x] == lbl)
                {
                    n.Names[x] = ttb3.Text;
                    lbl.Text = ttb3.Text;
                    return;
                }
            }
        }

        private void rmt_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you wish to remove this information?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
            {
                int cnt = 0;
                for (int x = 0; x < panel3.Controls.Count; x++)
                {
                    if (panel3.Controls[x] == lbl)
                    {
                        Node n = (Node)tv2.SelectedNode.Tag;
                        n.Names.RemoveAt(cnt);
                        n.Values.RemoveAt(cnt);
                        LoadClass();
                        return;
                    }
                    else if (panel3.Controls[x].GetType() == typeof(Label)) cnt++;
                }
            }
        }

        private void ttb4_MouseDown(object sender, MouseEventArgs e)
        {
            if (FstTime1)
            {
                ttb4.Text = "";
                FstTime1 = false;
            }
        }

        private void ttb4_TextChanged(object sender, EventArgs e)
        {
            adtol.Enabled = (ttb4.TextLength > 0);
        }

        private void ttb5_MouseDown(object sender, MouseEventArgs e)
        {
            if (FstTime2)
            {
                ttb5.Text = "";
                FstTime2 = false;
            }
        }

        private void adtol_Click(object sender, EventArgs e)
        {
            Node n = (Node)tv2.SelectedNode.Tag;
            n.Names.Add(ttb4.Text);
            n.Values.Add(ttb5.Text);

            ttb4.Text = "Name";
            ttb5.Text = "Value";
            FstTime1 = true;
            FstTime2 = true;

            LoadClass();
        }

        private void tv2_DragEnter(object sender, DragEventArgs e)
        {
            if ((e.Data.GetDataPresent("System.Windows.Forms.TreeNode")))
            {
                e.Effect = DragDropEffects.Copy;
                this.Focus();
            }
            else e.Effect = DragDropEffects.None;
        }

        private void tv2_DragDrop(object sender, DragEventArgs e)
        {
            TreeNode OrigNode = ((TreeNode)(e.Data.GetData("System.Windows.Forms.TreeNode")));

            if (e.Data.GetDataPresent("System.Windows.Forms.TreeNode", false))
            {
                UniqueClassname un = new UniqueClassname();
                bool unique = true;

                if (OrigNode.Nodes.Count == 0)
                {
                    string pname = OrigNode.Parent.Text;
                    foreach (UniqueClassname ucn in ListofUniqueClassnames)
                    {
                        if (ucn.UniqueName == pname)
                        {
                            unique = false;
                            un = ucn;
                            break;
                        }
                    }

                    if (unique)
                    {
                        un.UniqueName = pname;
                        un.ContainingNodes = new ArrayList();
                        un.ContainingNodes.Add((Node)OrigNode.Tag);
                        ListofUniqueClassnames.Add(un);
                    }
                    else un.ContainingNodes.Add((Node)OrigNode.Tag);
                }
                else //its a parent
                {
                    Application.DoEvents();
                    foreach (UniqueClassname ucn in ListofUniqueClassnames)
                    {
                        if (ucn.UniqueName == OrigNode.Text)
                        {
                            unique = false;
                            un = ucn;
                            break;
                        }
                    }

                    if (unique)
                    {
                        un.UniqueName = OrigNode.Text;
                        un.ContainingNodes = new ArrayList();
                        foreach (TreeNode t in OrigNode.Nodes)
                            un.ContainingNodes.Add((Node)t.Tag);

                        ListofUniqueClassnames.Add(un);

                    }
                    else //isnt unique
                    {
                        foreach (TreeNode t in OrigNode.Nodes)
                            un.ContainingNodes.Add((Node)t.Tag);
                    }
                }
                DisplayNodes();
            }
        }

        private void tv2_ItemDrag(object sender, ItemDragEventArgs e)
        {
            DoDragDrop(e.Item, DragDropEffects.Move | DragDropEffects.Copy);
        }

        #endregion

        #endregion

        #region Reference Tab

        private void button8_Click(object sender, EventArgs e)
        {
            LoadReferences();
        }

        public void LoadReferences()
        {
            // bool ShowDialog = true;
            //if (references.Loaded)
            //    ShowDialog = (MessageBox.Show("Do you wish to select a new .xzp file?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes);

            //if (ShowDialog)
            //{
            //    if (ox.ShowDialog() == DialogResult.Cancel) return;
            //    references.XZPfilepath = ox.FileName;
            // }

            form1.label1.Text = "Loading references...";
            Application.DoEvents();

            //open up the map in our reader
            BinaryReader br = new BinaryReader(new FileStream(map.Filepath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite));

            int num1 = 0;

            //if the scripts at the end, num1 = the script start
            if (script.AtEndofMap) num1 = (int)script.Offset;
            //else num1 = the length of the map
            else num1 = (int)(br.BaseStream.Length) - 1;

            int end = 0;
            bool tFzX = false;
            //goes backwards through the map
            for (int x = num1; x >= 0; x--)
            {
                br.BaseStream.Position = x;
                //store the byte from the current stream
                byte a = br.ReadByte();
                if (a == 46) //46 is the dot before the ext
                {
                    if (tFzX)
                    {
                        //store the end of the listings
                        end = x + 3;
                        break;
                    }
                }
                if (a == 116) //a t
                {
                    string s = "t";
                    s += br.ReadChar();
                    s += br.ReadChar();
                    s += br.ReadChar();

                    if (s == "tFzX")
                    {
                        tFzX = true;
                        br.BaseStream.Position -= 3;
                    }
                }
            }

            //goto the end of the listings
            br.BaseStream.Position = end;
            references.Offset = end;

            references.Amount = 0;
            references.RefArray = new ArrayList();
            string word = "";
            bool FoundStart = false;

            while (FoundStart == false)
            {
                byte a = br.ReadByte();
                if (a == 0) //0 is the dot inbetween every string
                {
                    //reverse the string and add it to the arraylist
                    references.RefArray.Add(StrReverse(word));
                    references.Amount++;
                    word = ""; //reset the word
                }
                else if (a == 67) //this is the byte at the start of the listings
                {
                    FoundStart = true;
                    references.RefArray.Add(StrReverse(word));
                    references.Amount++;
                }
                else word += Convert.ToChar(a);

                //move back 2
                br.BaseStream.Position -= 2;
                references.Offset -= 1;
            }
            references.Offset += 2;
            label12.Text = "Offset of 1st Reference: " + references.Offset.ToString();
            label11.Text = "Amount of References: " + references.Amount.ToString();

            references.Loaded = true;
            references.EIloaded = false;
            button8.Text = "Reload References";
            form1.label1.Text = "References loaded";
        }

        private void listv_Resize(object sender, EventArgs e)
        {
            listv.Columns[0].Width = listv.Width - 25;
        }

        private void reeipanel_VisibleChanged(object sender, EventArgs e)
        {
            Application.DoEvents();
            if (references.Loaded && !references.EIloaded && reeipanel.Visible)
            {
                references.EIloaded = true;

                listv.Items.Clear();
                //go through the arraylist backwards and add the items to the listview
                for (int x = references.RefArray.Count - 1; x >= 0; x--)
                    listv.Items.Add(references.RefArray[x].ToString());
            }
        }

        #endregion

        private void panel3_Resize(object sender, EventArgs e)
        {
            int width = panel3.Width - 50;

           // pp.Width = width;
            foreach (Control box in panel3.Controls)
            {
                if (box.GetType() == typeof(TextBox))
                    box.Width = width;
            }
        }
    }
}