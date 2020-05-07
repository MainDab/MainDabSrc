using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjectMainDab
{
    // This is one of MainDab's theme. This is what MainDab could have looked like back in 2016!
    // This is now being used a theme in MainDab, and it can be accessed from the main UI
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
            richTextBox2.ReadOnly = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string the42Gzy24isgay = richTextBox1.Text;
            NamedPipes.LuaPipe(the42Gzy24isgay);
            richTextBox2.Text += ("\r\n") + ("Sending script");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Functions.Inject();
            richTextBox2.Text += ("\r\n") + ("Injecting DLL");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (Functions.openfiledialog.ShowDialog() == DialogResult.OK)
            {
                try
                {

                    string MainText = File.ReadAllText(Functions.openfiledialog.FileName);
                    richTextBox1.Text = (MainText);
                    richTextBox2.Text += ("\r\n") + ("File opened.");

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                    Console.WriteLine("File can't be opened for some reason");
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = ("");
            richTextBox2.Text += ("\r\n") + ("Textbox cleared.");
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Project_MainDab_Load(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            base.Close();
            MainUI form = new MainUI();
            form.Show();
        }
    }
}
