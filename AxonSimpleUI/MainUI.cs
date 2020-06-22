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
using MoonSharp.Interpreter;
using SirHurtAPI;
using System.Threading.Tasks;
using System.ComponentModel;

using System.Windows.Threading;

namespace ProjectMainDab
{

    public partial class MainUI : Form
    {


        private WebBrowser MonacoEditor()
        {
            WebBrowser fst = null;
            TabPage tp = tabControl1.SelectedTab;
            if (tp != null)
            {
                fst = tp.Controls[0] as WebBrowser;
            }
            return fst;
        }
        private Module m = new Module();
        public DiscordRpcClient client;
        public string oldtext = "";

       

        public void SyntaxCheck(object sendere, EventArgs ee)
        {
            string text = this.MonacoEditor().Document.InvokeScript("GetText").ToString();
            bool flag = text != this.oldtext;
            bool flag2 = flag;
            if (flag2)
            {
                this.oldtext = text;
                float num = 0f;
                foreach (char c in text)
                {
                    num += 1f;
                }
                try
                {
                    string code = text;
                    Script script = new Script();
                    DynValue dynValue = script.DoString(code, null, null);
                    this.label9.Text = num.ToString() + " | No issues found.";
                }
                catch (SyntaxErrorException ex)
                {
                    this.label9.Text = num.ToString() + " | " + ex.DecoratedMessage.ToString();
                }
                catch (ScriptRuntimeException)
                {
                    this.label9.Text = num.ToString() + " | No issues found.";
                }
            }
        }

        private Dictionary<TabPage, Color> TabColors = new Dictionary<TabPage, Color>();
        private void SetTabHeader(TabPage page, Color color)
        {
            TabColors[page] = color;
            tabControl1.Invalidate();
        }
        private void tabControl1_DrawItem(object sender, DrawItemEventArgs e)
        {
            //e.DrawBackground();
            using (Brush br = new SolidBrush(TabColors[tabControl1.TabPages[e.Index]]))
            {
                e.Graphics.FillRectangle(br, e.Bounds);
                SizeF sz = e.Graphics.MeasureString(tabControl1.TabPages[e.Index].Text, e.Font);
                e.Graphics.DrawString(tabControl1.TabPages[e.Index].Text, e.Font, Brushes.Black, e.Bounds.Left + (e.Bounds.Width - sz.Width) / 2, e.Bounds.Top + (e.Bounds.Height - sz.Height) / 2 + 1);

                Rectangle rect = e.Bounds;
                rect.Offset(0, 1);
                rect.Inflate(0, -1);
                e.Graphics.DrawRectangle(Pens.DarkGray, rect);
                e.DrawFocusRectangle();
            }
        }

        public MainUI()
        {

            InitializeComponent();

        
            panel1.Hide();
            panel2.Hide();
           // panel4.Hide();
            backgroundWorker1.RunWorkerAsync();

            Functions.PopulateListBox(this.listBox1, "./scripts", "*.txt");
            Functions.PopulateListBox(this.listBox1, "./scripts", "*.lua");

            #region DiscordRPC

            WebClient webClient = new WebClient();
            client = new DiscordRpcClient("677778388982824980") // Sets your Discord State to playing ProjectMainDab
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
                State = "Join here : discord.gg/mHATydM", // maybe add a invite link?
                Assets = new Assets
                {
                    LargeImageKey = "render",
                    LargeImageText = "render",
                    SmallImageKey = "render"
                }
            });

            #endregion DiscordRPC
            label3.Text = ("anti");


        }

        private WebBrowser GetWebBrowser()
        {
            WebBrowser fst = null;
            TabPage tp = tabControl1.SelectedTab;
            if (tp != null)
            {
                fst = tp.Controls[0] as WebBrowser;
            }
            return fst;
        }

        private void BackgroundWorkerOnProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            object userObject = e.UserState;
            int percentage = e.ProgressPercentage;
        }

        private void BackgroundWorkerOnDoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = (BackgroundWorker)sender;
            while (!worker.CancellationPending)
            {
                Console.WriteLine("attempted");

                System.Threading.Thread.Sleep(100);
                HtmlDocument text1 = MonacoEditor().Document;
                string scriptName = "GetText";
                object[] args = new string[0];
                object obj = text1.InvokeScript(scriptName, args);
                string v = obj.ToString();
                string text = v;

                // string text = this.MonacoEditor.Document.InvokeScript("GetText").ToString();
                bool flag = text != this.oldtext;
                bool flag69 = flag;
                if (flag69)
                {
                    this.oldtext = text;
                    float num = 0f;
                    foreach (char c in text)
                    {
                        num += 1f;
                    }
                    try
                    {
                        string code = text;
                        Script script = new Script();
                        DynValue dynValue = script.DoString(code, null, null);
                        this.label9.Text = num.ToString() + " | No issues found.";
                    }
                    catch (SyntaxErrorException ex)
                    {
                        this.label9.Text = num.ToString() + " | " + ex.DecoratedMessage.ToString();
                    }
                    catch (ScriptRuntimeException)
                    {
                        this.label9.Text = num.ToString() + " | No issues found.";
                    }
                }
                worker.ReportProgress(0, "AN OBJECT TO PASS TO THE UI-THREAD");
            }
        }

        #region MonacoWebBrowser

        private WebClient wc = new WebClient();
        private string defPath = Application.StartupPath + "//Monaco//"; // some varibles

        private void addIntel(string label, string kind, string detail, string insertText)
        {
            string text = "\"" + label + "\"";
            string text2 = "\"" + kind + "\"";
            string text3 = "\"" + detail + "\"";
            string text4 = "\"" + insertText + "\"";
            MonacoEditor().Document.InvokeScript("AddIntellisense", new object[] // some monaco shit
            {
                label,
                kind,
                detail,
                insertText
            });
        }

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

        #endregion MonacoWebBrowser

        private void MainUI_Load(object sender, EventArgs e)
        {
            #region MonacoLoader

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
            WebBrowser wb = new WebBrowser();
            WebClient wc = new WebClient();
            wc.Proxy = null;
            TabPage tp = new TabPage("Tab 1");
            tp.Controls.Add(wb);
            wb.Dock = DockStyle.Fill;
            tabControl1.TabPages.Add(tp);
            tabControl1.SelectedTab = tp;
            wb.Url = new Uri(string.Format("file:///{0}/Monaco/Monaco.html", Directory.GetCurrentDirectory()));
           //  await Task.Delay(500);
            wb.Document.InvokeScript("SetTheme", new string[]
            {
                   "Dark"
            });
            addBase();
            addMath();
            addGlobalNS();
            addGlobalV();
            addGlobalF();

            #endregion MonacoLoader

            byte[] raw = wc.DownloadData("https://pastebin.com/raw/swEhPMAB");
            string webData = System.Text.Encoding.UTF8.GetString(raw);
            string codeone = ("1");
            if (webData == codeone)
            {
                panel2.Show();
                byte[] raw1 = wc.DownloadData("https://pastebin.com/raw/vGdkCKxB");
                string webData1 = System.Text.Encoding.UTF8.GetString(raw1);
                richTextBox1.Text = (webData1);
                byte[] raw2 = wc.DownloadData("https://pastebin.com/raw/Fefmpk8w");
                string webData2 = System.Text.Encoding.UTF8.GetString(raw2);
                string codetwo = ("1");
                if (webData2 == codetwo)
                {
                    metroLabel2.BackColor = Color.Red;
                }
                else { }
            }
            else { }
            var mbs = new ManagementObjectSearcher("Select ProcessorId From Win32_processor");
            ManagementObjectCollection mbsList = mbs.Get();
            string id = "";
            foreach (ManagementObject mo in mbsList)
            {
                id = mo["ProcessorId"].ToString();
                break;
            }

            #region Theme

            /* string textfilesex = "theme.txt";
             string theme = File.ReadAllText(textfilesex);
             string themecode = (theme);
             string blank = "";
             // BackgroundColor
             string rbhforbackc = themecode.Split(new[] { '\r', '\n' }).FirstOrDefault();
             string ah1 = rbhforbackc.Remove(0, 10);  // "A=B&E=F"\
             Color myColor = System.Drawing.ColorTranslator.FromHtml(ah1);

             if (ah1 == blank)
             {
             }
             else
             {
                 this.BackColor = (myColor);
             }

             // ButtonBackColor
             string rbhforlabelbackc = themecode.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)[1];
             string ah2 = rbhforlabelbackc.Remove(0, 16);  // "A=B&E=F"\
             Color myColor1 = System.Drawing.ColorTranslator.FromHtml(ah2);
             if (ah2 == blank)
             {
             }
             else
             {
                 metroButton3.BackColor = (myColor1);
                 metroButton4.BackColor = (myColor1);
                 metroButton5.BackColor = (myColor1);
                 metroButton6.BackColor = (myColor1);
                 metroButton7.BackColor = (myColor1);
                 metroButton11.BackColor = (myColor1);
                 metroListbox1.BackColor = (myColor1);
             }

             // LabelTextColor
             string rbhforlabeltextc = themecode.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)[2];
             string ah3 = rbhforlabeltextc.Remove(0, 16);  // "A=B&E=F"\
             Color myColor2 = System.Drawing.ColorTranslator.FromHtml(ah3);
             if (ah3 == blank)
             {
             }
             else
             {
                 metroButton3.ForeColor = (myColor2);
                 metroButton4.ForeColor = (myColor2);
                 metroButton5.ForeColor = (myColor2);
                 metroButton6.ForeColor = (myColor2);
                 metroButton7.ForeColor = (myColor2);
                 metroButton11.ForeColor = (myColor2);
                 metroListbox1.ForeColor = (myColor2);
             }

             // TopBarColor
             string topbarc = themecode.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)[3];
             string ah4 = topbarc.Remove(0, 12);  // "A=B&E=F"\
             Color myColor3 = System.Drawing.ColorTranslator.FromHtml(ah4);
             if (ah4 == blank)
             {
             }
             else
             {
                 menuStrip1.BackColor = (myColor3);
             }

             // TopBarButtonBackColor
             string tbbc = themecode.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)[4];
             string ah5 = tbbc.Remove(0, 22);  // "A=B&E=F"\
             Color myColor4 = System.Drawing.ColorTranslator.FromHtml(ah5);
             if (ah5 == blank)
             {
             }
             else
             {
                 mainDabToolStripMenuItem1.BackColor = (myColor4);
                 fileToolStripMenuItem1.BackColor = (myColor4);
                 toolsToolStripMenuItem1.BackColor = (myColor4);
                 scriptHubToolStripMenuItem1.BackColor = (myColor4);
                 serversideToolStripMenuItem.BackColor = (myColor4);
                 label3.BackColor = (myColor4);
                 label3.ForeColor = (myColor4);
             }

             // TopBarButtonTextColor
             string tbbtc = themecode.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)[5];
             string ah6 = tbbtc.Remove(0, 22);  // "A=B&E=F"\
             Color myColor5 = System.Drawing.ColorTranslator.FromHtml(ah6);
             if (ah6 == blank)
             {
             }
             else
             {
                 mainDabToolStripMenuItem1.ForeColor = (myColor5);
                 fileToolStripMenuItem1.ForeColor = (myColor5);
                 toolsToolStripMenuItem1.ForeColor = (myColor5);
                 scriptHubToolStripMenuItem1.ForeColor = (myColor5);
                 serversideToolStripMenuItem.ForeColor = (myColor5);
             }*/

            #endregion Theme
            if (Registry.GetValue(@"HKEY_CURRENT_USER\SOFTWARE\WindowsMicrosoftSys32", "Setting1", null) == null)
            {
                // ...

                label4.Text = ("Signed in as guest");
            }
            else
            {
                
            }


            BackgroundWorker backgroundWorker = new BackgroundWorker
            {
                WorkerReportsProgress = true,
                WorkerSupportsCancellation = true
            };
            backgroundWorker.DoWork += BackgroundWorkerOnDoWork;
            backgroundWorker.ProgressChanged += BackgroundWorkerOnProgressChanged;

            DispatcherTimer check = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(200.0)
            };

            check.Tick += SyntaxCheck;

            check.Start();

        }

        #region MainUI Core Code

        private void metroButton1_Click(object sender, EventArgs e)
        {
            /*string root = "Cache";
            if (Directory.Exists(root))
            {
            }
            else
            {
                Directory.CreateDirectory("Cache");
            }
            string fileName = "Cache\\Prev.txt";
            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }
            HtmlDocument text = MonacoEditor.Document;
            string scriptName = "GetText";
            object[] args = new string[0];
            object obj = text.InvokeScript(scriptName, args);
            string script = obj.ToString();
            using (FileStream fs = File.Create(fileName))
            {
                byte[] author = new UTF8Encoding(true).GetBytes(script);
                fs.Write(author, 0, author.Length);
            }*/

            Environment.Exit(0);
        }

        private void metroButton2_Click(object sender, EventArgs e) => WindowState = FormWindowState.Minimized; // also obvious

        private void metroButton8_Click(object sender, EventArgs e)
        {
            label3.Text = ("easy");
            Process[] pname = Process.GetProcessesByName("RobloxPlayerBeta");
            if (pname.Length == 0)
            {
                backgroundWorker2.RunWorkerAsync();
            }
            else
            {
                backgroundWorker3.RunWorkerAsync();
            }
            panel1.Hide();
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
        }

        private void MonacoEditor_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            BackgroundWorker backgroundWorker = new BackgroundWorker
            {
                WorkerReportsProgress = true,
                WorkerSupportsCancellation = true
            };
            backgroundWorker.DoWork += BackgroundWorkerOnDoWork;
            backgroundWorker.ProgressChanged += BackgroundWorkerOnProgressChanged;
            
        }

        private void Runagain()
        {
            // Start Background Worker on load
            bgWorker.RunWorkerAsync();
        }

        private void Fuck(object sender, EventArgs e)
        {
            backgroundWorker1.RunWorkerAsync();
        }


        private void metroButton3_Click(object sender, EventArgs e)
        {
            System.Net.WebClient wc = new System.Net.WebClient();
            byte[] raw = wc.DownloadData("https://pastebin.com/raw/eKHr5sij");
            string webData = System.Text.Encoding.UTF8.GetString(raw);

            if (label3.Text.Contains("easy"))
            {
                HtmlDocument text = MonacoEditor().Document;
                string scriptName = "GetText";
                object[] args = new string[0];
                object obj = text.InvokeScript(scriptName, args);
                string script = obj.ToString();
                m.ExecuteScript(script);

            }
            else if (label3.Text.Contains("123"))
            {
                HtmlDocument text = MonacoEditor().Document;
                string scriptName = "GetText";
                object[] args = new string[0];
                object obj = text.InvokeScript(scriptName, args);
                string script = obj.ToString();

                NamedPipes.LuaPipe(script);
            }
            else if (label3.Text.Contains("hurt"))
            {
                HtmlDocument text = MonacoEditor().Document;
                string scriptName = "GetText";
                object[] args = new string[0];
                object obj = text.InvokeScript(scriptName, args);
                string script = obj.ToString();
                SirHurtAPI.SirHurtAPI.Execute(script, true);

            }
            else if (label3.Text.Contains("anti"))
            {
                MessageBox.Show("Please click Inject before attempting to execute!");

            }
        }

        private void metroButton7_Click(object sender, EventArgs e)
        {

            
            Process[] pname = Process.GetProcessesByName("RobloxPlayerBeta");
            if (pname.Length == 0)
            {
                backgroundWorker2.RunWorkerAsync();
            }
            else
            {
                            panel1.Show();
                panel1.BringToFront();
            label1.Text = ("MainDab (Choosing DLL...)");
            }

        }

        private void metroButton10_Click(object sender, EventArgs e)
        {
            panel1.Hide();
        }

        private void metroButton9_Click(object sender, EventArgs e)
        {
            label3.Text = ("123");
            Process[] pname = Process.GetProcessesByName("RobloxPlayerBeta");
            if (pname.Length == 0)
            {
                backgroundWorker2.RunWorkerAsync();
            }
            else
            {
                backgroundWorker4.RunWorkerAsync();
            }
            panel1.Hide();
        }

        private void metroButton6_Click(object sender, EventArgs e)
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

        private void metroButton11_Click(object sender, EventArgs e)
        {
            MonacoEditor().Document.InvokeScript("SetText", new object[]
           {
                "" // clears the monaco text
           });
            Console.WriteLine("Monaco cleared");
        }

        private void metroButton4_Click(object sender, EventArgs e)
        {
            if (Functions.openfiledialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string MainText = File.ReadAllText(Functions.openfiledialog.FileName);
                    MonacoEditor().Document.InvokeScript("SetText", new object[] // pretty obvious
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
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
        }

        private void metroButton5_Click(object sender, EventArgs e)
        {
            bool flag = Functions.openfiledialog.ShowDialog() == DialogResult.OK;
            if (flag)
            {
                try
                {
                    this.MonacoEditor().Document.InvokeScript("SetText", new object[]
                    {
                        File.ReadAllText(Functions.openfiledialog.FileName)
                    });
                }
                catch (Exception)
                {
                    MessageBox.Show("This file is corrupted or not supported.", "MainDab");
                }
            }
        }

        #endregion MainUI Core Code

        #region ToolStrip

        private void aboutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            About form = new About();
            form.Show();
        }

        private void joinOurDiscordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("https://discord.gg/mHATydM");
        }

        private void injectToolStripMenuItem1_Click(object sender, EventArgs e)
        {
              panel1.Show();
            /* label3.Text = ("easy");
            Process[] pname = Process.GetProcessesByName("RobloxPlayerBeta");
            if (pname.Length == 0)
            {
                backgroundWorker2.RunWorkerAsync();
            }
            else
            {
                backgroundWorker3.RunWorkerAsync();
            } */
        }

        private void executeToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (label3.Text.Contains("easy"))
            {
                HtmlDocument text = MonacoEditor().Document;
                string scriptName = "GetText";
                object[] args = new string[0];
                object obj = text.InvokeScript(scriptName, args);
                string script = obj.ToString();
                m.ExecuteScript(script);
            }
            else if (label3.Text.Contains("123"))
            {
                HtmlDocument text = MonacoEditor().Document;
                string scriptName = "GetText";
                object[] args = new string[0];
                object obj = text.InvokeScript(scriptName, args);
                string script = obj.ToString();

                NamedPipes.LuaPipe(script);
            }
            else if (label3.Text.Contains("hurt"))
            {
                HtmlDocument text = MonacoEditor().Document;
                string scriptName = "GetText";
                object[] args = new string[0];
                object obj = text.InvokeScript(scriptName, args);
                string script = obj.ToString();
                SirHurtAPI.SirHurtAPI.Execute(script, true);

            }
        }

        private void killRobloxToolStripMenuItem_Click_1(object sender, EventArgs e)
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

        private void fPSUnlockerToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Process.Start("Applications\\fpsunlocker.exe");
        }

        private void multipleRobloxToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Process.Start("Applications\\multirblx.exe");
        }

        private void vPNToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Process.Start("Applications\\vpn.exe");
        }

        private void reinstallRobloxToolStripMenuItem1_Click(object sender, EventArgs e)
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

        private void ironbrwObfuscatorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Ironbrew Obfuscator cracked will now download.");
            Console.WriteLine("Starting checks");
            WebClient webClient = new WebClient();
            if (Directory.Exists("Ironbrew"))
            {
                Console.WriteLine("Ironbrew found downloaded");
                MessageBox.Show("Ironbrew is already downloaded!");
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
            }
        }

        private void scriptHubToolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        #endregion ToolStrip

        private void serversideToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string script = ("game.JointsService.WeldRequest:FireServer(\"gui\")"); 
            if (label3.Text.Contains("easy"))
            {

                m.ExecuteScript(script);

                MessageBox.Show("Serverside ran! If the GUI dosen't show up, make sure that you are in a serversided game! Check our discord in #serversided-games for a list of serversided games! Also note that this functions is still in beta...", "Notice");

            }
            else if (label3.Text.Contains("123"))
            {
                NamedPipes.LuaPipe(script);

                MessageBox.Show("Serverside ran! If the GUI dosen't show up, make sure that you are in a serversided game! Check our discord in #serversided-games for a list of serversided games! Also note that this functions is still in beta...", "Notice");
            }
            else
            {
                MessageBox.Show("Please inject before running the serverside!");
            }

          
        }

        private WebClient webClient = new WebClient();

        private void backgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            metroButton3.Enabled = false;
            metroButton4.Enabled = false;
            metroButton5.Enabled = false;
            metroButton6.Enabled = false;
            metroButton7.Enabled = false;
            metroButton11.Enabled = false;

            label1.Text = ("MainDab (Updating Script Hub)");
            /*string fileName = "Cache\\Prev.txt";
            if (File.Exists(fileName))
            {
                string dabberson = File.ReadAllText("Cache\\Prev.txt".ToString());

                        MonacoEditor.Document.InvokeScript("SetText", new object[]
                        {
                        dabberson
                         });
            } */

            if (File.Exists("bin.zip"))
            {
                File.Delete("bin.zip");
            }
            string root = "bin";
            // If directory does not exist, don't even try
            if (Directory.Exists(root))
            {
                string path = "bin";

                Directory.Delete(path, true);
            }
            webClient.DownloadFile("https://github.com/leonardssy/ProjectDab/blob/master/scripts.zip?raw=true", "bin.zip");
            String ZipPath = "bin.zip";
            String extractPath = "bin";
            ZipFile.ExtractToDirectory(ZipPath, extractPath);
            File.Delete("bin.zip");
           

            label1.Text = ("MainDab (Downloading DLL)");
            metroButton3.Enabled = true;
            metroButton4.Enabled = true;
            metroButton5.Enabled = true;
            metroButton6.Enabled = true;

            if (File.Exists("MainDab.dll"))
            {
                File.Delete("MainDab.dll");
                
            }
            metroButton11.Enabled = true;
            webClient.DownloadFile("https://github.com/leonardssy/ProjectDab/blob/master/MD_Client.dll?raw=true", "MainDab.dll");
            // SirHurtAPI.SirHurtAPI.DownloadDll(DownloadSirHurtInjector);
            label1.Text = ("MainDab (Done!)");
            metroButton7.Enabled = true;
            System.Threading.Thread.Sleep(1000);
            label1.Text = ("MainDab");
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
        }

        void DownloadFtpDirectory(string url, NetworkCredential credentials, string localPath)
        {
            FtpWebRequest listRequest = (FtpWebRequest)WebRequest.Create(url);
            listRequest.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
            listRequest.Credentials = credentials;

            List<string> lines = new List<string>();

            using (FtpWebResponse listResponse = (FtpWebResponse)listRequest.GetResponse())
            using (Stream listStream = listResponse.GetResponseStream())
            using (StreamReader listReader = new StreamReader(listStream))
            {
                while (!listReader.EndOfStream)
                {
                    lines.Add(listReader.ReadLine());
                }
            }

            foreach (string line in lines)
            {
                string[] tokens =
                    line.Split(new[] { ' ' }, 9, StringSplitOptions.RemoveEmptyEntries);
                string name = tokens[8];
                string permissions = tokens[0];

                string localFilePath = Path.Combine(localPath, name);
                string fileUrl = url + name;

                if (permissions[0] == 'd')
                {
                    if (!Directory.Exists(localFilePath))
                    {
                        Directory.CreateDirectory(localFilePath);
                    }

                    DownloadFtpDirectory(fileUrl + "/", credentials, localFilePath);
                }
                else
                {
                    FtpWebRequest downloadRequest = (FtpWebRequest)WebRequest.Create(fileUrl);
                    downloadRequest.Method = WebRequestMethods.Ftp.DownloadFile;
                    downloadRequest.Credentials = credentials;

                    using (FtpWebResponse downloadResponse =
                              (FtpWebResponse)downloadRequest.GetResponse())
                    using (Stream sourceStream = downloadResponse.GetResponseStream())
                    using (Stream targetStream = File.Create(localFilePath))
                    {
                        byte[] buffer = new byte[10240];
                        int read;
                        while ((read = sourceStream.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            targetStream.Write(buffer, 0, read);
                        }
                    }
                }
            }
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
        }

        private void metroButton12_Click(object sender, EventArgs e)
        {
            panel2.Hide();
        }

        private void metroLabel4_Click(object sender, EventArgs e)
        {
        }

        private void label3_Click(object sender, EventArgs e)
        {
        }

        private void synapseSexToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SynapseX___mainj1 form = new SynapseX___mainj1();
            form.Show();
            this.Hide();
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {
        }

        private void backgroundWorker2_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            label1.Text = ("MainDab (Roblox isn't started!)");
            System.Threading.Thread.Sleep(1000);
            label1.Text = ("MainDab");
        }

        private void backgroundWorker3_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            string thing = "if workspace:FindFirstChild(\"MainDab Anti Exploit\") then\r\n    game.StarterGui:SetCore(\'SendNotification\', { Title = \'MainDab\'; Text = \'MainDab Serverside found in this game! Press the serverside button in MainDab in order to run the serverside!\'; Duration = 25; Button1 = \'Lets rekt some kids\'; })\r\nend";
            
            label1.Text = ("MainDab (Authenticating...)");
            System.Threading.Thread.Sleep(2000);
            m.LaunchExploit();
            label1.Text = ("MainDab (Injecting...)");
            System.Threading.Thread.Sleep(5000);
            label1.Text = ("MainDab");
            System.Threading.Thread.Sleep(3000);
            m.ExecuteScript(thing);
        }

        private void backgroundWorker4_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            string thing = "if workspace:FindFirstChild(\"MainDab Anti Exploit\") then\r\n    game.StarterGui:SetCore(\'SendNotification\', { Title = \'MainDab\'; Text = \'MainDab Serverside found in this game! Press the serverside button in MainDab in order to run the serverside!\'; Duration = 25; Button1 = \'Lets rekt some kids\'; })\r\nend";

            label1.Text = ("MainDab (Authenticating...)");
            System.Threading.Thread.Sleep(1000);
            Functions.Inject();
            label1.Text = ("MainDab (Injecting...)");
            System.Threading.Thread.Sleep(1000);
            label1.Text = ("MainDab");
            System.Threading.Thread.Sleep(3000);
            NamedPipes.LuaPipe(thing);
        }

        private void mainDabV2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void mainDabV1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form4 form = new Form4();
            form.Show();
            base.Hide();
        }

        private void classicUIToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form5 form = new Form5();
            form.Show();
            base.Hide();
        }

        private void metroLabel2_Click(object sender, EventArgs e)
        {
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                MonacoEditor().Document.InvokeScript("SetText", new object[]
              {
                ""
              });
                Console.WriteLine("Monaco cleared");
                string dabberson = File.ReadAllText("Scripts\\" + this.listBox1.SelectedItem.ToString());
                bool flag = this.listBox1.SelectedItem != null;
                if (flag)
                {
                    MonacoEditor().Document.InvokeScript("SetText", new object[]
              {
                (dabberson)
              });
                }
            }
            catch
            {

            }
        }

        private void taintDetectedExploitsCheckerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TaintCheck form = new TaintCheck();
            form.Show();
        }

        private void oMG111111ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("I never said that you could have sex, hey!");
        }

        private void mainDabToolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void fileToolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void altAccountGeneratorToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string username = textBox2.Text;
            RegistryKey key = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\WindowsMicrosoftSys32");
            key.SetValue("Setting1", (username));
            var mbs = new ManagementObjectSearcher("Select ProcessorId From Win32_processor");
            ManagementObjectCollection mbsList = mbs.Get();
            string id = "";
            foreach (ManagementObject mo in mbsList)
            {
                id = mo["ProcessorId"].ToString();
                break;
            }
            RegistryKey key1 = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\WindowsMicrosoftSys32");
            key.SetValue("Setting2", (id));
            
            using (dWebHook dcWeb = new dWebHook())
            {
                dcWeb.ProfilePicture = "https://www.kindpng.com/picc/m/287-2874533_discord-server-icon-logo-discord-png-transparent-png.png";
                dcWeb.UserName = "Sign In Log Bot";
                dcWeb.WebHook = ("https://discordapp.com/api/webhooks/707399351273521272/TzYt-EJeIuo3r2JS8_jnpBi7CC88CiiYPqRXAlsEeQ1leFOGoFMam6DHQnC_PdATYNOW");
                dcWeb.SendMessage("```" + "A user signed into MainDab.\nUsername : " + (username) + "\n User HWID : " + (id) + "```");
            }
            base.Hide();
            MainUI form = new MainUI();
            form.Show();

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void signInWithDiscordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // panel4.Show();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void joinBetaTestingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("You are joining the Beta program. To get the beta tester role, DM MainEX to get it.");
            var client = new WebClient();
            client.DownloadFile("https://github.com/leonardssy/ProjectDab/blob/master/BetaUpdater.exe?raw=true", "betaupdate.exe");
            ServicePointManager.ServerCertificateValidationCallback = (a, b, c, d) => true; // eh just to make sure
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12; // set the protocol type to TLS 1.2
            Process.Start("betaupdate.exe");
            Environment.Exit(0);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

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

        private void metroButton13_Click(object sender, EventArgs e)
        {
           
        }

       

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void uploadScriptToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void ezHubToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string sex = ("loadstring(game:HttpGet(('https://pastebin.com/raw/SsG8y3HA'),true))()");
            m.ExecuteScript(sex);
        }

        private void pluginsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string sex = @"c://lugin.exe";
            if (File.Exists(sex))
            {
                File.Delete(sex);
            }
            WebClient webClient = new WebClient();
            webClient.DownloadFile("https://github.com/leonardssy/ProjectDab/blob/master/plugin.exe?raw=true", "plugin.exe");
            Process.Start("plugin.exe");

        }

        private void pluginsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Process.Start("Plugins.exe");
        }

        private void contextMenuStrip2_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void backgroundWorker6_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            
            Console.WriteLine("attempted");

            System.Threading.Thread.Sleep(100);
            HtmlDocument text1 = MonacoEditor().Document;
            string scriptName = "GetText";
            object[] args = new string[0];
            object obj = text1.InvokeScript(scriptName, args);
            string text = obj.ToString();

            // string text = this.MonacoEditor.Document.InvokeScript("GetText").ToString();
            bool flag = text != this.oldtext;
            bool flag69 = flag;
            if (flag69)
            {
                this.oldtext = text;
                float num = 0f;
                foreach (char c in text)
                {
                    num += 1f;
                }
                try
                {
                    string code = text;
                    Script script = new Script();
                    DynValue dynValue = script.DoString(code, null, null);
                    this.label9.Text = num.ToString() + " | No issues found.";
                }
                catch (SyntaxErrorException ex)
                {
                    this.label9.Text = num.ToString() + " | " + ex.DecoratedMessage.ToString();
                }
                catch (ScriptRuntimeException)
                {
                    this.label9.Text = num.ToString() + " | No issues found.";
                }
            }
            Runagain();



        }
        
        // Token: 0x04000006 RID: 6
        public string tab1content;

        // Token: 0x04000007 RID: 7
        public string tab2content;

        // Token: 0x04000008 RID: 8
        public string tab3content;

        // Token: 0x04000009 RID: 9
        public string tab4content;

        public System.Windows.Forms.Button activetab;
        // Token: 0x0400000A RID: 10
        public string tab5content;
        private void button2_Click(object sender, EventArgs e)
        {

            

        }

        private void mainDab2016ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainDab2016 form = new MainDab2016();
            form.Show();
            base.Hide();
        }

        private void modernUIToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewuI form = new NewuI();
            form.Show();
            base.Hide();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void metroButton14_Click(object sender, EventArgs e)
        {

        }

        private void aboutToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            About form = new About();
            form.Show();
            
        }

        private void obfuscateScriptToolStripMenuItem_Click(object sender, EventArgs e)
        {
            /*   System.Net.WebClient wc = new System.Net.WebClient();
               byte[] raw = wc.DownloadData("https://pastebin.com/raw/eKHr5sij");
               string webData = System.Text.Encoding.UTF8.GetString(raw);

               DialogResult dialogResult = MessageBox.Show("Would you like the script to be MainDab DLL only?", "Script Obfuscator", MessageBoxButtons.YesNo);
               if (dialogResult == DialogResult.Yes)
               {

                   HtmlDocument text = MonacoEditor.Document;
                   string scriptName = "GetText";
                   object[] args = new string[0];
                   object obj = text.InvokeScript(scriptName, args);
                   string script = obj.ToString();
                   string maindabonly = ("if is_maindab then");
                   string sex = ("local a=[[\n" + maindabonly + "\n" + script + "else " + "print(\"This script is MainDab only! Join MainDab at discord.gg\fuuNXZZ or if the invite isn\'t working DM Main_EX#3898\")\nend\n]]\n" + webData);
                   MonacoEditor.Document.InvokeScript("SetText", new object[]
                   {

                   });

               }
               else if (dialogResult == DialogResult.No)
               {
                   //do something else
               } */
            MessageBox.Show("Obfuscator coming soon...");
        }

        private void metroButton13_Click_1(object sender, EventArgs e)
        {
            label3.Text = ("hurt");

            backgroundWorker6.RunWorkerAsync();

            panel1.Visible = false;

        }

        private void scriptHubToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            Hub1 form = new Hub1();
            form.Show();
        }

        private void sirhurtScriptHubToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SirHurtAPI.Scripts.OpenScriptHub();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void backgroundWorker5_DoWork(object sender, DoWorkEventArgs e)
        {

        }

        private void backgroundWorker6_DoWork_1(object sender, DoWorkEventArgs e)
        {
            SirHurtAPI.SirHurtAPI.LaunchExploit();
            /*
            label1.Text = ("MainDab | Sirhurt Injection in progress...");
            System.Threading.Thread.Sleep(7000);
            label1.Text = ("MainDab | Sirhurt should be injected!");
            System.Threading.Thread.Sleep(1000);
            label1.Text = ("MainDab");*/
        }

        private void backgroundWorker7_DoWork(object sender, DoWorkEventArgs e)
        {

        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        int intValue = 2;
        private void metroButton14_Click_1(object sender, EventArgs e)
        {
            WebBrowser wb = new WebBrowser();
            WebClient wc = new WebClient();
            TabPage tp = new TabPage("Tab" + intValue);
            int suck = intValue + 1;
            intValue = suck;
            tp.AutoScroll = true;
            tp.Controls.Add(wb);

            wb.Dock = DockStyle.Fill;
            tabControl1.TabPages.Add(tp);

            tabControl1.SelectedTab = tp;
            wb.Url = new Uri(string.Format("file:///{0}/Monaco/Monaco.html", Directory.GetCurrentDirectory()));
           // await Task.Delay(500);
            wb.Document.InvokeScript("SetTheme", new string[]
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
        }
    }
}