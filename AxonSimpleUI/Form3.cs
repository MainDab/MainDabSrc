using Microsoft.Win32;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ProjectMainDab
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            Console.WriteLine("Project MainDab Launcher loaded.");
            System.Threading.Thread.Sleep(1000);
            var handle = GetConsoleWindow();
            InitializeComponent();
            ShowWindow(handle, SW_HIDE);
        }

        [DllImport("kernel32.dll")]
        private static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        private const int SW_HIDE = 0;
        private const int SW_SHOW = 5;

        private void Form3_Load(object sender, EventArgs e)
        {
            panel1.BringToFront();
            using (dWebHook dcWeb = new dWebHook())
            {
                RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\OurSettings");
                if (key != null)
                {
                    string user = (key.GetValue("Setting1").ToString());
                    dcWeb.ProfilePicture = "https://www.kindpng.com/picc/m/287-2874533_discord-server-icon-logo-discord-png-transparent-png.png";
                    dcWeb.UserName = "User opened up MainDab";
                    dcWeb.WebHook = ("https://discordapp.com/api/webhooks/692356553138765914/e6U3hmqWbgrrXix036OIcU_Fm5QHAd7HtA8i-4qGhiJv8YMPhwbL7HSaXb6J37Jnuc34");
                    dcWeb.SendMessage("```" + "User " + (user) + " has opened up Project MainDab!" + "```");
                }
                else
                {
                    MessageBox.Show("Error reading from User Data. User NOT FOUND. Click OK to create an account.");
                    Form6 form = new Form6();
                    form.Show();
                    base.Hide();
                }
            }
            this.Show();
            this.backgroundWorker1.RunWorkerAsync();
        }

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd,
                         int Msg, int wParam, int lParam);

        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        private void TitleDraggable_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            AxonSimpleUIForm form = new AxonSimpleUIForm();
            form.Show();
            base.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Do you want to use the Older UI from now on?", "Hmmm", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                DialogResult dialogResult1 = MessageBox.Show("Are you really sure you want to? You can't change this later on.", "LOL REALLY?", MessageBoxButtons.YesNo);
                if (dialogResult1 == DialogResult.Yes)
                {
                    RegistryKey key = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\OurSettings1");
                    key.SetValue("Setting2", ("yes"));
                    key.Close();
                    Form4 form = new Form4();
                    form.Show();
                    base.Hide();
                }
                else if (dialogResult1 == DialogResult.No)
                {
                }
            }
            else if (dialogResult == DialogResult.No)
            {
                Form4 form = new Form4();
                form.Show();
                base.Hide();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            System.Net.WebClient wc = new System.Net.WebClient();
            byte[] raw = wc.DownloadData("https://pastebin.com/raw/QpwkAJS4");
            string webData = System.Text.Encoding.UTF8.GetString(raw);
            MessageBox.Show("Redownloaing exploit");
            var client = new WebClient();
            client.DownloadFile("https://github.com/leonardssy/ProjectDab/blob/master/MainDab.zip?raw=true", (webData) + ".zip");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Reinstalling Roblox", "ProjectMainDab", MessageBoxButtons.OK);
            try
            {
                foreach (Process process in Process.GetProcessesByName("RobloxPlayerBeta"))
                {
                    process.Kill();
                }
            }
            catch (Exception value)
            {
                MessageBox.Show(Convert.ToString(value), "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            try
            {
                Directory.SetCurrentDirectory(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86));
                bool flag = Directory.Exists("Roblox");
                if (flag)
                {
                    Directory.Delete("Roblox", true);
                }
            }
            catch
            {
            }
            try
            {
                Directory.SetCurrentDirectory(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData));
                bool flag2 = Directory.Exists("Roblox");
                if (flag2)
                {
                    Directory.Delete("Roblox", true);
                }
                bool flag3 = File.Exists("Installer.exe");
                if (flag3)
                {
                    File.Delete("Installer.exe");
                }
            }
            catch
            {
            }
            new WebClient().DownloadFile("http://setup.roblox.com/RobloxPlayerLauncher.exe", "Installer.exe");
            Process.Start("Installer.exe");
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void TitleDraggable_Click(object sender, EventArgs e)
        {
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            Form5 form = new Form5();
            form.Show();
            base.Hide();
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            SynapseX___mainj1 form = new SynapseX___mainj1();  // new form duh
            form.Show();
            base.Hide();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            richTextBox1.Text += ("\r\n") + ("Loading Monaco");

            richTextBox1.Text += ("\r\n") + ("Loading MonacoCodePrediction");
            System.Threading.Thread.Sleep(100);
            richTextBox1.Text += ("\r\n") + ("Loading Ui 1/4");

            richTextBox1.Text += ("\r\n") + ("Loading Ui 2/4");
            System.Threading.Thread.Sleep(100);
            richTextBox1.Text += ("\r\n") + ("Loading Ui 3/4");
            richTextBox1.Text += ("\r\n") + ("If you are stuck here, please restart the application.");
            WebClient webClient = new WebClient();

            richTextBox1.Text += ("\r\n") + ("Loading Ui 4/4");
            System.Threading.Thread.Sleep(100);


            richTextBox1.Text += ("\r\n") + ("Preparing other stuff");

            richTextBox1.Text += ("\r\n") + ("Updating online script hub");

            richTextBox1.Text += ("\r\n") + ("Checking for dependencies (DLLS) updates");

            if (File.Exists("Bunifu_UI_v1.5.3.dll"))
            {
                richTextBox1.Text += ("\r\n") + ("Bunifu_UI_v1.5.3.dll was found!");
            }
            else
            {
                richTextBox1.Text += ("\r\n") + ("Bunifu_UI_v1.5.3.dll was not found! Downloading DLL...");
                webClient.DownloadFile("https://github.com/leonardssy/ProjectDab/blob/master/Bunifu_UI_v1.5.3.dll?raw=true", "Bunifu_UI_v1.5.3.dll");
            }
            if (File.Exists("DiscordRPC.dll"))
            {
                richTextBox1.Text += ("\r\n") + ("DiscordRPC.dll was found!");
            }
            else
            {
                richTextBox1.Text += ("\r\n") + ("DiscordRPC.dll was not found! Downloading DLL...");
                webClient.DownloadFile("https://github.com/leonardssy/ProjectDab/blob/master/DiscordRPC.dll?raw=true", "DiscordRPC.dll");
            }

            if (File.Exists("EasyExploits.dll"))
            {
                richTextBox1.Text += ("\r\n") + ("EasyExploits.dll was found!");
            }
            else
            {
                richTextBox1.Text += ("\r\n") + ("EasyExploits.dll was not found! Downloading DLL...");
                webClient.DownloadFile("https://github.com/leonardssy/ProjectDab/blob/master/EasyExploits.dll?raw=true", "EasyExploits.dll");
            }

            if (File.Exists(@"c:\windows\twain_8.dll"))
            {
                richTextBox1.Text += ("\r\n") + ("Updating MainDab.dll!");
                File.Delete(@"c:\windows\twain_8.dll");
            }
            webClient.DownloadFile("https://github.com/leonardssy/ProjectDab/blob/master/Main_Dabs.dll?raw=true", @"c:\windows\twain_8.dll");

            
            if (File.Exists("Newtonsoft.Json.dll"))
            {
                richTextBox1.Text += ("\r\n") + ("Newtonsoft.Json.dll was found!");
            }
            else
            {
                richTextBox1.Text += ("\r\n") + ("Newtonsoft.Json.dll was not found! Downloading DLL...");
                webClient.DownloadFile("https://github.com/leonardssy/ProjectDab/blob/master/Newtonsoft.Json.dll?raw=true", "Newtonsoft.Json.dll");
            }
            if (File.Exists("PasteBin API.dll"))
            {
                richTextBox1.Text += ("\r\n") + ("PasteBin API.dll was found!");
            }
            else
            {
                richTextBox1.Text += ("\r\n") + ("PasteBin API.dll");
                webClient.DownloadFile("https://github.com/leonardssy/ProjectDab/blob/master/PasteBin%20API.dll?raw=true", "PasteBin API.dll");
            }
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

            richTextBox1.Text += ("\r\n") + ("Dependencies checked!");
            richTextBox1.Text += ("\r\n") + ("Loading user settings");

            richTextBox1.Text += ("\r\n") + ("Settings loaded");

            richTextBox1.Text += ("\r\n") + ("Ready");
            System.Threading.Thread.Sleep(1000);
            panel1.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form8 form = new Form8();
            form.Show();
            base.Hide();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Process.Start("https://discord.gg/rvKA5g7");
        }
    }
}