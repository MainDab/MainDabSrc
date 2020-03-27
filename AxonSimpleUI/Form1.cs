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
using System.Diagnostics;
using EasyExploits;
using System.Drawing;
using PasteBin_API;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;


/*
HEY THERE YOU LITTLE SKID
YOU AREN'T SUPPOSED TO BE LOOKING AT THIS SOURCE!\
HOW DID YOU ANYWAY DEOBFUSCATE FUJIFUSCATOR THOUGH...
ANYWAYS ENJOY HAVING THE SOURCE... I GUESS ITS THE END FOR ME.
I also annotated a lot of stuff in the source just in case you came by :)
*/

namespace ProjectMainDab
{

    public partial class AxonSimpleUIForm : Form
    {
        Module m = new Module(); // New Easyexploits module

        public AxonSimpleUIForm()
        {


            InitializeComponent();
            panel2.Hide();
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
                Details = "ProjectMainDab", // yeet
                State = "Exploiting in Roblox", // maybe add a invite link?
                Assets = new Assets
                {
                    LargeImageKey = "render",
                    LargeImageText = "render",
                    SmallImageKey = "render"
                }
            });
            backgroundWorker2.RunWorkerAsync();

        }

        // Start of Draggable Window
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd,
                         int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();
        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        const int SW_HIDE = 0;
        const int SW_SHOW = 5;
        private void TitleDraggable_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
        //  End of draggable windo
        private void CloseButton_Click(object sender, EventArgs e) => Environment.Exit(0); // Exit application
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
                 "-- Made by wolfie#6969" // stop skidding whoever is reading this

            });



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

        WebClient wc = new WebClient();
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
            if (label3.Text.Contains("easy"))
            {
                HtmlDocument text = MonacoEditor.Document;
                string scriptName = "GetText";
                object[] args = new string[0];
                object obj = text.InvokeScript(scriptName, args);
                string script = obj.ToString();
                m.ExecuteScript(script);
            }
            else if (label3.Text.Contains("123"))
            {
                HtmlDocument text = MonacoEditor.Document;
                string scriptName = "GetText";
                object[] args = new string[0];
                object obj = text.InvokeScript(scriptName, args);
                string script = obj.ToString();
               
                    NamedPipes.LuaPipe(script);
                

            }
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
            panel1.Show();
            panel1.BringToFront();

            //Functions.Inject();

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

        private void button3_Click_1(object sender, EventArgs e) // set theme
        {
            this.BackColor = Color.FromArgb(50,50,50);
            tabControl1.TabPages[0].BackColor = Color.FromArgb(50, 50, 50);
            tabControl1.TabPages[1].BackColor = Color.FromArgb(50, 50, 50);
            TitleDraggable.ForeColor = Color.White;
            TitleDraggable.BackColor = Color.FromArgb(50, 50, 50);
            TitleDraggable.ForeColor = Color.White;
            label1.BackColor = Color.FromArgb(50, 50, 50);
            label1.ForeColor = Color.White;
            pictureBox1.BackColor = Color.FromArgb(50, 50, 50);
            MinimizeButton.BackColor = Color.FromArgb(50, 50, 50);
            MinimizeButton.ForeColor = Color.White;
            CloseButton.BackColor = Color.FromArgb(50, 50, 50);

            button3.BackColor = Color.FromArgb(50, 50, 50);
            button3.ForeColor = Color.White;
            button6.BackColor = Color.FromArgb(50, 50, 50);
            button6.ForeColor = Color.White;
            button7.BackColor = Color.FromArgb(50, 50, 50);
            button7.ForeColor = Color.White;


        }

        private void button6_Click_1(object sender, EventArgs e) // setting theme
        {
            this.BackColor = Color.White;
            tabControl1.TabPages[0].BackColor = Color.White;
            tabControl1.TabPages[1].BackColor = Color.White;
            TitleDraggable.ForeColor = Color.Black;
            TitleDraggable.BackColor = Color.White;

            label1.BackColor = Color.White;
            label1.ForeColor = Color.Black;
            pictureBox1.BackColor = Color.White;
            MinimizeButton.BackColor = Color.White;
            MinimizeButton.ForeColor = Color.Black;
            CloseButton.BackColor = Color.Black;

            button3.BackColor = Color.FromArgb(198, 198, 198);
            button3.ForeColor = Color.White;
            button6.BackColor = Color.FromArgb(198,198, 198);
            button6.ForeColor = Color.White;
            button7.BackColor = Color.FromArgb(198, 198, 198);
            button7.ForeColor = Color.White;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Form4 form = new Form4();
            form.Show(); // Some more themes
            base.Hide();
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void MonacoEditor_DocumentCompleted_1(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            panel2.Show();
            panel2.BringToFront();
            
        }

        private void label4_Click(object sender, EventArgs e)
        {
            panel2.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
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
            panel2.Hide();
            core.PostAnnon((sdfskfsjkdfnasjkjdldi), (script));
            MessageBox.Show("Script sent! Check the Discord Channel to see the link");
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
            label1.Text = ("V");
            System.Threading.Thread.Sleep(100);
            label1.Text = ("Ve");
            System.Threading.Thread.Sleep(100);
            label1.Text = ("Ver");
            System.Threading.Thread.Sleep(100);
            label1.Text = ("Vers");
            System.Threading.Thread.Sleep(100);
            label1.Text = ("Versi");
            System.Threading.Thread.Sleep(100);
            label1.Text = ("Versio");
            System.Threading.Thread.Sleep(100);
            label1.Text = ("Version");
            System.Threading.Thread.Sleep(100);
            label1.Text = ("Version 2");
            System.Threading.Thread.Sleep(100);
            label1.Text = ("Version 2.");
            System.Threading.Thread.Sleep(100);
            label1.Text = ("Version 2.5");
            System.Threading.Thread.Sleep(100);
            label1.Text = ("Version 2.5 |");
            System.Threading.Thread.Sleep(100);
            label1.Text = ("Version 2.5 | B");
            System.Threading.Thread.Sleep(100);
            label1.Text = ("Version 2.5 | Bu");
            System.Threading.Thread.Sleep(100);
            label1.Text = ("Version 2.5 | Bui");
            System.Threading.Thread.Sleep(100);
            label1.Text = ("Version 2.5 | Buil");
            System.Threading.Thread.Sleep(100);
            label1.Text = ("Version 2.5 | Build");
            System.Threading.Thread.Sleep(100);
            label1.Text = ("Version 2.5 | Build 1");
            System.Threading.Thread.Sleep(100);
            label1.Text = ("Version 2.5 | Build 19");



        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            Process.Start("https://discord.gg/rvKA5g7");
        }
    }
}
