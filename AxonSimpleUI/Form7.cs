using System;
using System.ComponentModel;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Windows.Forms;

namespace ProjectMainDab
{
    public partial class Form7 : Form
    {
        public Form7()
        {
            InitializeComponent();
            this.backgroundWorker1.RunWorkerAsync();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            richTextBox1.Text += ("\r\n") + ("Splash Screen credits to default.jpg");
            System.Threading.Thread.Sleep(500);
            richTextBox1.Text += ("\r\n") + ("Loading Monaco");
            System.Threading.Thread.Sleep(500);
            richTextBox1.Text += ("\r\n") + ("Loading MonacoCodePrediction");
            System.Threading.Thread.Sleep(1000);
            richTextBox1.Text += ("\r\n") + ("Loading Ui 1/7");
            System.Threading.Thread.Sleep(100);
            richTextBox1.Text += ("\r\n") + ("Loading Ui 2/7");
            System.Threading.Thread.Sleep(100);
            richTextBox1.Text += ("\r\n") + ("Loading Ui 3/7");
            WebClient webClient = new WebClient();
            if (Directory.Exists("bin"))
            {
                string path = "bin";
                Directory.Delete(path, true);
                webClient.DownloadFile("https://github.com/leonardssy/ProjectDab/blob/master/scripts.zip?raw=true", "bin.zip");
                ZipFile.ExtractToDirectory("bin.zip", "bin");
                if (File.Exists("bin.zip"))
                {
                    File.Delete("bin.zip");
                }
            }
            else
            {
                webClient.DownloadFile("https://github.com/leonardssy/ProjectDab/blob/master/scripts.zip?raw=true", "bin.zip");
                ZipFile.ExtractToDirectory("bin.zip", "bin");
                if (File.Exists("bin.zip"))
                {
                    File.Delete("bin.zip");
                }
            }

            richTextBox1.Text += ("\r\n") + ("Loading Ui 4/7");
            System.Threading.Thread.Sleep(100);
            richTextBox1.Text += ("\r\n") + ("Loading Ui 5/7");
            System.Threading.Thread.Sleep(100);
            richTextBox1.Text += ("\r\n") + ("Loading Ui 6/7");
            System.Threading.Thread.Sleep(100);
            richTextBox1.Text += ("\r\n") + ("Loading Ui 7/7");
            System.Threading.Thread.Sleep(100);
            richTextBox1.Text += ("\r\n") + ("Preparing other stuff");
            System.Threading.Thread.Sleep(100);
            richTextBox1.Text += ("\r\n") + ("Updating online script hub");
            System.Threading.Thread.Sleep(200);
            richTextBox1.Text += ("\r\n") + ("Checking for dependencies updates");
            System.Threading.Thread.Sleep(500);
            richTextBox1.Text += ("\r\n") + ("No errors found in dependencies!");
            richTextBox1.Text += ("\r\n") + ("Loading user settings");
            System.Threading.Thread.Sleep(100);
            richTextBox1.Text += ("\r\n") + ("Settings loaded");
            System.Threading.Thread.Sleep(100);
            richTextBox1.Text += ("\r\n") + ("Ready");

            form.Show();
            base.Hide();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
        }
    }
}
