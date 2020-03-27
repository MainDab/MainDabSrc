using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;
using EasyExploits;

namespace ProjectMainDab
{
    public partial class Hub1 : Form
    {
        Module m = new Module();
        public Hub1()
        {
            InitializeComponent();
            richTextBox2.Text = ("warn(\"MainDab Application Note - Please choose a script from the script list first before executing!\")");
        }

        private void Hub1_Load(object sender, EventArgs e)
        {
            listBox1.Items.Clear();//Clear Items in the LuaScriptList
            Functions.PopulateListBox1(listBox1, "./bin/scripts", "*.txt");
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string dabberson = File.ReadAllText("bin\\scripts\\" + this.listBox1.SelectedItem.ToString());
            bool flag = this.listBox1.SelectedItem != null;
            if (flag)
            {
                string poopy = (dabberson);
                richTextBox2.Text = (poopy);
                string mainscript = poopy.Split(new[] { '\r', '\n' }).FirstOrDefault();
                string ah1 = mainscript.Remove(0, 2);  // "A=B&E=F"
                richTextBox1.Text = (ah1);
                string img = poopy.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)[1];
                string a = (img);
                string ah = a.Remove(0, 2);  // "A=B&E=F"
                string img1 = (ah);
                var request = WebRequest.Create(img1);
                using (var response = request.GetResponse())
                using (var stream = response.GetResponseStream())
                {
                    pictureBox1.Image = Bitmap.FromStream(stream);
                }

            }

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            m.ExecuteScript(richTextBox2.Text);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            NamedPipes.LuaPipe(richTextBox2.Text);
        }
    }
}
