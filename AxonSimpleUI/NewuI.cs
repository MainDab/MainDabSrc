using DiscordRPC;
using DiscordRPC.Logging;
using DiscordRPC.Message;
using EasyExploits;
using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Management;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace ProjectMainDab
{
    public partial class NewuI : Form
    {
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
        (
            int nLeftRect,     // x-coordinate of upper-left corner
            int nTopRect,      // y-coordinate of upper-left corner
            int nRightRect,    // x-coordinate of lower-right corner
            int nBottomRect,   // y-coordinate of lower-right corner
            int nWidthEllipse, // width of ellipse
            int nHeightEllipse // height of ellipse
        ); 
        private Module m = new Module();
        public DiscordRpcClient client;
        Timer t1 = new Timer();
        void fadeIn(object sender, EventArgs e)
        {
            if (Opacity >= 1)
                t1.Stop();   //this stops the timer if the form is completely displayed
            else
                Opacity += 0.05;
        }
        public NewuI()
        {
            InitializeComponent();
           //  this.FormBorderStyle = FormBorderStyle.None;
           //  Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
            backgroundWorker2.RunWorkerAsync();
            WebClient webClient = new WebClient();
            client = new DiscordRpcClient("714648958265327737") // Sets your Discord State to playing ProjectMainDab
            {
                Logger = new ConsoleLogger
                {
                    Level = LogLevel.Warning
                }
            };

            client.OnReady += delegate (object sender, ReadyMessage e)
            {
            };
            client.OnPresenceUpdate += delegate (object sender, PresenceMessage e)
            {
            };
            client.Initialize();
            client.SetPresence(new RichPresence
            {
                Details = "Using MainDab Roblox Exploit", // yeet
                State = "Join here : discord.gg/TKcPeaq", // maybe add a invite link?
                Assets = new Assets
                {
                    LargeImageKey = "render",
                    LargeImageText = "render",
                    SmallImageKey = "render"
                }
            });
            Opacity = 0;      //first the opacity is 0

            t1.Interval = 10;  //we'll increase the opacity every 10ms
            t1.Tick += new EventHandler(fadeIn);  //this calls the function that changes opacity 
            t1.Start();
        }
        void fadeOut(object sender, EventArgs e)
        {
            if (Opacity <= 0)     //check if opacity is 0
            {
                t1.Stop();    //if it is, we stop the timer
                Close();   //and we try to close the form
            }
            else
                Opacity -= 0.05;
        }
        private void NewuI_Load(object sender, EventArgs e)
        {
           
            WebClient wc = new WebClient
            {
                Proxy = null
            };
            try
            {
                RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Internet Explorer\\Main\\FeatureControl\\FEATURE_BROWSER_EMULATION", true);
                string friendlyName = AppDomain.CurrentDomain.FriendlyName;
                bool flag2 = registryKey.GetValue(friendlyName) == null;
                if (flag2)
                {
                    registryKey.SetValue(friendlyName, 11001, RegistryValueKind.DWord);
                }
                registryKey = null;
                friendlyName = null;
            }
            catch (Exception)
            {
            }
            MonacoEditor.Url = new Uri(string.Format("file:///{0}/Monaco/Monaco.html", Directory.GetCurrentDirectory()));

            MonacoEditor.Document.InvokeScript("SetTheme", new string[]
            {
                   "Dark"
                   /*
                    There are 2 Themes Dark and Light
                   */
            });
            addBase();
            addMath();
            addGlobalNS();
            addGlobalV();
            addGlobalF();
            MonacoEditor.Document.InvokeScript("SetText", new object[]
            {
                 "-- Made by MainEX" // stop skidding whoever is reading this
            });
        }
        private void addIntel(string label, string kind, string detail, string insertText)
        {
            string text = "\"" + label + "\"";
            string text2 = "\"" + kind + "\"";
            string text3 = "\"" + detail + "\"";
            string text4 = "\"" + insertText + "\"";
            MonacoEditor.Document.InvokeScript("AddIntellisense", new object[] // some monaco shit
            {
                label,
                kind,
                detail,
                insertText
            });
        }
        private string defPath = Application.StartupPath + "//Monaco//"; // some varibles
        // START OF MONACO CODE NO ONE ASKED FOR
        private void addGlobalF()
        {
            string[] array = File.ReadAllLines(defPath + "//globalf.txt");
            foreach (string text in array)
            {
                bool flag = text.Contains(':');
                if (flag)
                {
                    addIntel(text, "Function", text, text.Substring(1));
                }
                else
                {
                    addIntel(text, "Function", text, text);
                }
            }
        }

        private void addGlobalV()
        {
            foreach (string text in File.ReadLines(defPath + "//globalv.txt"))
            {
                addIntel(text, "Variable", text, text);
            }
        }

        private void addGlobalNS()
        {
            foreach (string text in File.ReadLines(defPath + "//globalns.txt"))
            {
                addIntel(text, "Class", text, text);
            }
        }

        private void addMath()
        {
            foreach (string text in File.ReadLines(defPath + "//classfunc.txt"))
            {
                addIntel(text, "Method", text, text);
            }
        }

        private void addBase()
        {
            foreach (string text in File.ReadLines(defPath + "//base.txt"))
            {
                addIntel(text, "Keyword", text, text);
            }
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            HtmlDocument text = MonacoEditor.Document;
            string scriptName = "GetText";
            object[] args = new string[0];
            object obj = text.InvokeScript(scriptName, args);
            string script = obj.ToString();
            m.ExecuteScript(script);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (Functions.openfiledialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string MainText = File.ReadAllText(Functions.openfiledialog.FileName);
                    MonacoEditor.Document.InvokeScript("SetText", new object[] // pretty obvious
                    {
                          MainText
                    });
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                    Console.WriteLine("File can't be opened");
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MonacoEditor.Document.InvokeScript("SetText", new object[]
           {
                "" // clears the monaco text
           });
        }

        private void button4_Click(object sender, EventArgs e)
        {
            backgroundWorker1.RunWorkerAsync();

        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (Process.GetProcessesByName("RobloxPlayerBeta").Length == 0) //killing roblox
            {
                return;
            }
            else
            {
                try
                {
                    Process[] proc = Process.GetProcessesByName("RobloxPlayerBeta"); // finds roblox
                    proc[0].Kill();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Execution.BringToFront();
            Execution.Show();
        }

        private void backgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            Process[] pname = Process.GetProcessesByName("RobloxPlayerBeta");
            if (pname.Length == 0)
            {
                MessageBox.Show("Roblox isn't started!");
            }
            else
            {
                panel4.Show();
                button4.Enabled = false;
                label4.Text = ("Injecting EasyExploits...");
                m.LaunchExploit();
                System.Threading.Thread.Sleep(5000);
                button4.Enabled = true;
                panel4.Hide();
            }
           
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Tools.BringToFront();
            Tools.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            using (StreamWriter w = File.AppendText(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.System), @"drivers\etc\hosts")))
            {
                w.WriteLine("# Anti-Banwave Code");
                w.WriteLine("127.0.0.1 data.roblox.com");
                w.WriteLine("127.0.0.1 roblox.sp.backtrace.io");
            }
            MessageBox.Show("AntiBanWave activated!");
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Process.Start("Applications\\fpsunlocker.exe");
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Process.Start("Applications\\vpn.exe");
        }

        private void button9_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Reinstalling Roblox", "MainDab", MessageBoxButtons.OK);
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

        private void button10_Click(object sender, EventArgs e)
        {
            TaintCheck form = new TaintCheck();
            form.Show();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            string sex = ("loadstring(game:HttpGet(('https://pastebin.com/raw/SsG8y3HA'),true))()");
            m.ExecuteScript(sex);
        }

        private void button12_Click(object sender, EventArgs e)
        {
            Process.Start("https://discord.gg/mHATydM");
        }

        private void button13_Click(object sender, EventArgs e)
        {
            Process.Start("Applications\\multirblx.exe");
        }

        private void button15_Click(object sender, EventArgs e)
        {
            string script = ("if workspace[\"MainDab Anti Exploit\"] == nil then\r\nprint(\"Serverside not found in game!\")\r\nelse\r\nprint(\"Serverside found in game!\")\r\nend");
            m.ExecuteScript(script);
            MessageBox.Show("Press F9 to see the details. If it's backdoored, it should print it is. If you encounter an errorm that means the the serverside dosen't exist in the game!", "Notice");
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {

        }

        private void backgroundWorker2_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            
            label4.Text = "Updating Script Hub";
            if (File.Exists("bin.zip"))
            {
                File.Delete("bin.zip");
            }
            string root = "bin";
            if (Directory.Exists(root))
            {
                string path = "bin";

                Directory.Delete(path, true);
            }
           listBox1.Items.Clear();//Clear Items in the LuaScriptList

            WebClient webClient = new WebClient();
            webClient.DownloadFile("https://github.com/leonardssy/ProjectDab/blob/master/scripts.zip?raw=true", "bin.zip");
            String ZipPath = "bin.zip";
            String extractPath = "bin";
            ZipFile.ExtractToDirectory(ZipPath, extractPath);
            File.Delete("bin.zip");
            listBox1.Items.Clear();//Clear Items in the LuaScriptList
            Functions.PopulateListBox1(listBox1, "./bin/scripts", "*.txt");
            label4.Text = "Update done!";
            System.Threading.Thread.Sleep(1000);

            panel4.Hide();
        }

        private void ScriptHub_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button17_Click(object sender, EventArgs e)
        {
            About form = new About();
            form.Show();
        }

        private void button18_Click(object sender, EventArgs e)
        {
            MainUI form = new MainUI();
            form.Show();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Serverside.BringToFront();
            Serverside.Show();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            ScriptHub.BringToFront();
            ScriptHub.Show();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            Others.BringToFront();
            Others.Show();
            ScriptHub.Hide();
            Serverside.Hide();
            Execution.Hide();
            Others.BringToFront();
            Others.Show();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button16_Click(object sender, EventArgs e)
        {
            m.ExecuteScript(richTextBox2.Text);
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
                    pictureBox6.Image = Bitmap.FromStream(stream);
                }

            }
        }

        private void label2_Click(object sender, EventArgs e)
        {
            
                Environment.Exit(0);
            
        }

        private void label3_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            
        }

        private void trackBar1_Scroll_1(object sender, EventArgs e)
        {
            
        }

        private void button14_Click(object sender, EventArgs e)
        {
            string script = ("game.JointsService.WeldRequest:FireServer(\"gui\")");
            m.ExecuteScript(script);
            MessageBox.Show("Serverside ran! If the GUI dosen't show up, check that you are in a serversided game! Check our discord in #serversided-games for a list of serversided games!", "Notice");
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            panel3.BringToFront();
            panel3.Show();
        }
    }
}
