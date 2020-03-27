using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Compression;
using System.IO;
using System.Net;
using Microsoft.Win32;

namespace ProjectMainDab
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
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
        private void Form2_Load(object sender, EventArgs e)
        {
            
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            base.Hide();

        }

        private void TitleDraggable_Click(object sender, EventArgs e)
        {

        }
        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        const int SW_HIDE = 0;
        const int SW_SHOW = 5;
        private void button4_Click(object sender, EventArgs e)
        {
            string backdoor = ("game.ReplicatedStorage[\"BackDoor\"]:FireServer(\"mml\")");
            NamedPipes.LuaPipe(backdoor);
            MessageBox.Show("Coming soon");
        }

        private void button2_Click(object sender, EventArgs e)
        {

            Process.Start("Applications\\multirblx.exe");
            Directory.SetCurrentDirectory(Directory.GetCurrentDirectory());
        }

        private void button7_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("Ironbrew is already downloaded!");
            var handle = GetConsoleWindow();
            ShowWindow(handle, SW_SHOW);
            MessageBox.Show("Ironbrew Obfuscator cracked will now download.");
            Console.WriteLine("Starting checks");
            WebClient webClient = new WebClient();
            if (Directory.Exists("Ironbrew"))
            {
                Console.WriteLine("Ironbrew found downloaded");
                MessageBox.Show("Ironbrew is already downloaded!");
                ShowWindow(handle, SW_HIDE);

            }
            else
            {
                Console.WriteLine("Deleting ironbrew files if found");
                if (File.Exists("ironbrew.zip"))
                {
                    File.Delete("ironbrew.zip");

                }
                if (File.Exists(@"c:\luac.exe"))
                {
                    File.Delete(@"c:\luac.exe");

                }
                if (File.Exists(@"c:\luajit.exe"))
                {
                    File.Delete(@"c:\luajit.exe");

                }
                Console.WriteLine("Downloading luac.exe");
                webClient.DownloadFile("https://github.com/leonardssy/ProjectDab/blob/master/luac.exe?raw=true", @"c:\luac.exe");
                Console.WriteLine("Downloading luajit.exe");
                webClient.DownloadFile("https://github.com/leonardssy/ProjectDab/blob/master/luajit.exe?raw=true", @"c:\luajit.exe");
                Console.WriteLine("Downloading ironbrew.zip");
                webClient.DownloadFile("https://github.com/leonardssy/ProjectDab/blob/master/ironbrew.zip?raw=true", "ironbrew.zip");
                Console.WriteLine("Ironbrew.zip downloaded");
                Console.WriteLine("Unzipping Ironbrew.zip");
                ZipFile.ExtractToDirectory("ironbrew.zip", "ironbrew");
                Console.WriteLine("Unziped ironbrew to directory ironbrew");
                if (File.Exists("ironbrew.zip"))
                {
                    File.Delete("ironbrew.zip");
                    Console.WriteLine("Deleted ironbrew.zip");
                }
                Console.WriteLine("Download complete");
                MessageBox.Show("Ironbrew is downloaded!");
                Console.WriteLine("This console window will now close.");
                ShowWindow(handle, SW_HIDE);

            }


        }

        private void button6_Click(object sender, EventArgs e)
        {

            Process.Start("Applications\\vpn.exe");

        }

        private void button5_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Reinstalling Roblox", "CoolXploit", MessageBoxButtons.OK);
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

        private void button8_Click(object sender, EventArgs e)
        {

            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\OurSettings");
            {
                if (key == null)
                {
                    // Key doesn't exist. Do whatever you want to handle
                    // this case
                    MessageBox.Show(" Settings don't exist! ");
                }
                else
                {


                    if (key == null)
                    {
                        RegistryKey keyf = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\OurSettings");
                        string user = (keyf.GetValue("Setting1").ToString());
                        string keyName = @"SOFTWARE\OurSettings";
                        using (RegistryKey keya = Registry.CurrentUser.OpenSubKey(keyName, true))
                        {
                            if (key == null)
                            {
                                MessageBox.Show("Error resetting!");
                            }
                            else
                            {
                                key.DeleteValue(user);
                                MessageBox.Show("Reset sucessful!");
                                Form6 form = new Form6();
                                form.Show();
                                base.Hide();
                            }
                        }
                       
                    }
                    else
                    {
                        RegistryKey keyf = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\OurSettings", true);
                        string user = (keyf.GetValue("Setting1").ToString());
                        string keyName = @"SOFTWARE\OurSettings";
                        string keyName1 = @"SOFTWARE\OurSettings1";
                        using (RegistryKey keya = Registry.CurrentUser.OpenSubKey(keyName, true))
                        {
                            if (key == null)
                            {
                                MessageBox.Show("Error resetting!");
                            }
                            else
                            {
                                key.DeleteValue(user);
                            }
                        }
                        using (RegistryKey keya1 = Registry.CurrentUser.OpenSubKey(keyName1, true))
                        {
                            if (key == null)
                            {
                                MessageBox.Show("Error resetting!");
                            }
                            else
                            {

                                key.DeleteValue("yes");
                                MessageBox.Show("Reset sucessful!");
                                Form6 form1 = new Form6();
                                form1.Show();
                                base.Hide();
                            }
                        }


                        Form6 form = new Form6();  // new form duh
                        form.Show();
                        MessageBox.Show("Settings reset!");
                    }

                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {

            Process.Start("Applications\\fpsunlocker.exe");

        }

        private void button1_Click(object sender, EventArgs e)
        {

            Hub1 form = new Hub1();  // new form duh
            form.Show();
            base.Hide();
        }
    }
}
