using DiscordRPC;
using DiscordRPC.Logging;
using DiscordRPC.Message;
using EasyExploits;
using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Windows.Forms;

/*
This is the old UI for MainDab, which is no longer used.
I also annotated a lot of stuff in the source just in case you came by :)
This is now being used a theme in MainDab, and it can be accessed from the main UI
*/

namespace ProjectMainDab
{
    public partial class AxonSimpleUIForm : Form
    {
        private Module m = new Module(); // New Easyexploits module

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

        public AxonSimpleUIForm()
        {
            InitializeComponent();
            var request = WebRequest.Create("https://thumbs.gfycat.com/ConcreteFriendlyGuillemot-size_restricted.gif");

            using (var response = request.GetResponse())
            using (var stream = response.GetResponseStream())
            {
                pictureBox5.Image = Bitmap.FromStream(stream);
            }
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));

            panel1.Hide(); // Hides DLL options
            var handle = GetConsoleWindow(); // Hides the CMD window
            ShowWindow(handle, SW_HIDE);// Hides the CMD window
            //MonacoEditor.Url = new Uri(string.Format("file:///{0}/monaco-editor/index.html", Directory.GetCurrentDirectory()));
            //MonacoEditor.Document.BackColor = System.Drawing.SystemColors.ControlDark;
            WebClient webClient = new WebClient(); // duh new web client

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
                Details = "MainDab Roblox Exploit", // yeet
                State = "discord.gg/CVpr5sf", // maybe add a invite link?
                Assets = new Assets
                {
                    LargeImageKey = "render",
                    LargeImageText = "render",
                    SmallImageKey = "render"
                }
            });

            //backgroundWorker3.RunWorkerAsync();
        }

        private void HookMouseMove(Control.ControlCollection ctls)
        {
            foreach (Control ctl in ctls)
            {
                ctl.MouseMove += OnMouseMove;
                HookMouseMove(ctl.Controls);
            }
        }

        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            Control ctl = sender as Control;
            if (ctl != null)
            {
                // Map mouse coordinate to form
                Point loc = this.PointToClient(ctl.PointToScreen(e.Location));
                Console.WriteLine("Mouse at {0},{1}", loc.X, loc.Y);
                Opacity = 1;
            }
        }

        private void OnMouseLeave(object sender, EventArgs e)
        {
            Opacity = 0.5;
        }

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd,
                         int Msg, int wParam, int lParam);

        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        [DllImport("kernel32.dll")]
        private static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        private const int SW_HIDE = 0;
        private const int SW_SHOW = 5;

        private void TitleDraggable_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        //  End of draggable windo
        private void CloseButton_Click(object sender, EventArgs e)
        {
            // Environment.Exit(0); // Exit application
            //mpletly transparent
            base.Close();
            MainUI form = new MainUI();
            form.Show();
        }

        public DiscordRpcClient client; // pretty obvious

        private void MinimizeButton_Click(object sender, EventArgs e) => WindowState = FormWindowState.Minimized; // also obvious

        private void Inject_Click(object sender, EventArgs e) => Functions.Inject(); // inject shit duh

        private void ExecuteButton_Click(object sender, EventArgs e)
        {
            if (NamedPipes.NamedPipeExist(NamedPipes.luapipename))//check if the pipe exist
            {
                NamedPipes.LuaPipe(MonacoEditor.Document.InvokeScript("GetMonacoEditorText").ToString());//lua pipe function to send the script
            }
            else
            {
                MessageBox.Show($"Inject {Functions.exploitdllname} before Using this!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);//if the pipe can't be found a messagebox will appear
                return;
            }
        }

        private void ClearButton_Click(object sender, EventArgs e) => MonacoEditor.Document.InvokeScript("SetMonacoEditorText", new object[] { "" });//Clear the MonacoEditor

        private void OpenButton_Click(object sender, EventArgs e)
        {
            if (Functions.openfiledialog.ShowDialog() == DialogResult.OK)//check if the user clicked Ok/open
            {
                try
                {
                    MonacoEditor.Document.InvokeScript("SetMonacoEditorText", new object[]
                    {
                        File.ReadAllText(Functions.openfiledialog.FileName)
                    });//load all the text in the MonacoEditor
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: Could not read file from disk. Original error: {ex.Message}");//display if got error
                }
            }
        }

        private void StatusTimer_Tick(object sender, EventArgs e)
        {
            
            if (NamedPipes.NamedPipeExist(NamedPipes.luapipename))
            {
                StatusLabel.Text = "Injected";
                StatusLabel.ForeColor = System.Drawing.Color.Green; // status stuff
                var request = WebRequest.Create("https://thumbs.gfycat.com/ConcreteFriendlyGuillemot-size_restricted.gif");

                using (var response = request.GetResponse())
                using (var stream = response.GetResponseStream())
                {
                    pictureBox5.Image = Bitmap.FromStream(stream);
                }
            }
            else
            {
                StatusLabel.Text = "Not yet injected";
                StatusLabel.ForeColor = System.Drawing.Color.Red;
            }
        }

        // DO NOT TOUCH THE STUFF BELOW THIS IS FOR THE MONACO EDITOR THANKS
        private void AxonSimpleUIForm_Load(object sender, EventArgs e)
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

        private Timer t1 = new Timer();

        private void main_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;    //cancel the event so the form won't be closed

            t1.Tick += new EventHandler(fadeOut);  //this calls the fade out function
            t1.Start();

            if (Opacity == 0)  //if the form is completly transparent
                e.Cancel = false;   //resume the event - the program can be closed
        }

        private void fadeOut(object sender, EventArgs e)
        {
            if (Opacity <= 0)     //check if opacity is 0
            {
                t1.Stop();    //if it is, we stop the timer
                Close();   //and we try to close the form
            }
            else
                Opacity -= 0.20;
        }

        private void TitleDraggable_Click(object sender, EventArgs e)
        {
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form2 form = new Form2();
            form.Show(); // shows option menu
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }

        private WebClient wc = new WebClient();
        private string defPath = Application.StartupPath + "//Monaco//"; // some varibles

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

        // END OF MONACO CODE NO ONE ASKED FOR
        private void contextMenuStrip1_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
        }

        private void loadToExecutorToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void backgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            Functions.Inject();
            // string credits = ("game.StarterGui:SetCore(\"ChatMakeSystemMessage\", {\r\n    Text = \"Main_EX's\'s API Injected!\";\r\n    Color = Color3.new(0, 185, 0);\r\n    Font = Enum.Font.SourceSansBold;\r\n    FontSize = Enum.FontSize.Size24;\r\n})"); // executes this script on injection
            // NamedPipes.LuaPipe(credits);
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void button6_Click(object sender, EventArgs e)
        {
        }

        private void MonacoEditor_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            // Execute button. If the label contains "Easy" then use EasyExploits API. If contains 123, execute using Axon
          //  if (label3.Text.Contains("easy"))
           // {
                HtmlDocument text = MonacoEditor.Document;
                string scriptName = "GetText";
                object[] args = new string[0];
                object obj = text.InvokeScript(scriptName, args);
                string script = obj.ToString();
                m.ExecuteScript(script);
          //  }
            /*else if (label3.Text.Contains("123"))
            {
                HtmlDocument text = MonacoEditor.Document;
                string scriptName = "GetText";
                object[] args = new string[0];
                object obj = text.InvokeScript(scriptName, args);
                string script = obj.ToString();

                NamedPipes.LuaPipe(script);
            }*/
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            MonacoEditor.Document.InvokeScript("SetText", new object[]
           {
                "" // clears the monaco text
           });
            Console.WriteLine("Monaco cleared");
        }

        private string svCheck = "false";

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            bool flag = this.svCheck == "false";
            if (flag)
            {
                this.svCheck = "true";
                base.Size = new Size(684, 315);
            }
            else
            {
                bool flag2 = this.svCheck == "true";
                if (flag2)
                {
                    this.svCheck = "false";
                    base.Size = new Size(633, 315);
                }
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            // panel1.Show();
            // panel1.BringToFront();

            //Functions.Inject();
            Process[] pname = Process.GetProcessesByName("RobloxPlayerBeta");
            if (pname.Length == 0)
            {
                MessageBox.Show("MainDab isn't started!");
            }
            else
            {
                m.LaunchExploit();
            }
        }

        private void pictureBox6_Click(object sender, EventArgs e)
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

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        // start of panel1 code
        private void button4_Click_1(object sender, EventArgs e)
        {
            // Injects EasyExploits
            label3.Text = ("easy");
            m.LaunchExploit();
            panel1.Hide();
            StatusLabel.Hide();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            StatusLabel.Show();

            // Injects axon
            backgroundWorker1.RunWorkerAsync();
            label3.Text = ("123");
            panel1.Hide();
            StatusLabel.Show();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            // Opening file / loading file
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

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            Form2 form = new Form2();  // new form duh
            form.Show();
        }

        private void StatusLabel_Click(object sender, EventArgs e)
        {
        }

        private void label8_Click(object sender, EventArgs e)
        {
        }

        private void label1_Click_1(object sender, EventArgs e)
        {
        }

        private void backgroundWorker2_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            Process.Start("https://discord.gg/hCAuFab");
        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {
            Process.Start("https://github.com/leonardssy/ProjectDab/tree/master/Previous%20Versions%20of%20MainDab");
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            label3.Text = ("123");
            panel1.Hide();
            StatusLabel.Show();
            MessageBox.Show("Note that this is experimental! It was made to minic MainDabs.dll except that this is bytecode. Don't come complaining at me if it dosen't work", "no actually for once READ MY MESSAGES");
            Functions.Inject();
        }

        private void backgroundWorker3_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            while (true)
            {
            }
        }

        private void label9_Click(object sender, EventArgs e)
        {
        }

        private void MonacoEditor_DocumentCompleted_1(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
        }

        private void contextMenuStrip2_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }
    }
}