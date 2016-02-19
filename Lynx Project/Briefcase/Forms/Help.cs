using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Lynx
{
    public partial class Help : Form
    {
        public Help()
        {
            InitializeComponent();
        }

        private void treev_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (treev.SelectedNode == treev.Nodes[0].Nodes[0])
            {
                label1.Text = "This is because in hex it was a 0, which is nothingness.. so Lynx reads it as @";
            }
            else if (treev.SelectedNode == treev.Nodes[1].Nodes[0])
            {
                label1.Text = "Simple mode is just the current script cut up into loads of parts." + Environment.NewLine +
                    "The classnames are displayed on the left, and clicking on them will display the rest of the segments properties on the right.";
            }
            else if (treev.SelectedNode == treev.Nodes[1].Nodes[1])
            {
                label1.Text = "A segment is simply what Lynx calls the code inbetween an opening and closing bracket {}";
            }
            else if (treev.SelectedNode == treev.Nodes[1].Nodes[2])
            {
                label1.Text = "With your mouse hovering over the classname list, simply right-click:" + Environment.NewLine +
                    "You will be presented with the option to type in a name and add it.";
            }
            else if (treev.SelectedNode == treev.Nodes[1].Nodes[3])
            {
                label1.Text = "Once you have selected a classname; simply right-click and you will be presented with the options to do either.";
            }
            else if (treev.SelectedNode == treev.Nodes[1].Nodes[4])
            {
                label1.Text = "With your mouse hovering over the properties panel, simply right-click:" + Environment.NewLine +
                    "You will be presented with the option to type in a name/value and add it.";
            }
            else if (treev.SelectedNode == treev.Nodes[1].Nodes[5])
            {
                label1.Text = "With your mouse hovering over the label of the property you wish to edit:" + Environment.NewLine +
                    "Just right-click and you will be presented with the options to do either.";
            }
            else if (treev.SelectedNode == treev.Nodes[2].Nodes[0])
            {
                label1.Text = "A msgbox will appear with 3 options..." + Environment.NewLine + Environment.NewLine +
                    "Choosing Yes will add blanks to the script until it is the correct size, then inject." + Environment.NewLine +
                    "Choosing No will inject it as if it were the correct size... which will most probably break your map." + Environment.NewLine +
                    "Choosing Cancel will stop the injection process.";
            }
            else if (treev.SelectedNode == treev.Nodes[2].Nodes[1])
            {
                label1.Text = "A msgbox will appear with 3 options..." + Environment.NewLine + Environment.NewLine +
                     "Choosing Yes will add the script to the end of the map, Lynx will then update 2 references with the new position and length." + Environment.NewLine +
                     "Choosing No will inject it as if it were the correct size... which will most probably break your map." + Environment.NewLine +
                     "Choosing Cancel will stop the injection process.";
            }
            else
            {
                label1.Text = "";
            }
        }
    }
}