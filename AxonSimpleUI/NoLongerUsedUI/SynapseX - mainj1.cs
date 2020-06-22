using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Bunifu.Framework.UI;
using Microsoft.Win32;
using Bunifu;
using EasyExploits;

namespace ProjectMainDab
{
    // Token: 0x02000002 RID: 2
    public partial class SynapseX___mainj1 : Form
    {
        private Module m = new Module(); // New Easyexploits module
        // Token: 0x06000001 RID: 1
        [DllImport("WeAreDevs_API.cpp.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool LaunchExploit();

        // Token: 0x06000002 RID: 2
        [DllImport("WeAreDevs_API.cpp.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool SendLuaCScript(string script);

        // Token: 0x06000003 RID: 3
        [DllImport("WeAreDevs_API.cpp.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool SendLimitedLuaScript(string script);

        // Token: 0x06000004 RID: 4
        [DllImport("WeAreDevs_API.cpp.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool SendCommand(string script);

        // Token: 0x06000005 RID: 5 RVA: 0x00002050 File Offset: 0x00000250
        public SynapseX___mainj1()
        {
            this.InitializeComponent();
        }

        // Token: 0x06000006 RID: 6 RVA: 0x0000209E File Offset: 0x0000029E
        private void button4_Click(object sender, EventArgs e)
        {
            this.webBrowser1.Document.InvokeScript("SetText", new object[]
            {
                ""
            });
        }

        // Token: 0x06000007 RID: 7 RVA: 0x000020C5 File Offset: 0x000002C5
        private void fastColoredTextBox1_Load(object sender, EventArgs e)
        {
        }

        // Token: 0x06000008 RID: 8 RVA: 0x000020C8 File Offset: 0x000002C8
        private void button9_Click(object sender, EventArgs e)
        {
            Form2 form = new Form2();
            form.ShowDialog();
        }

        // Token: 0x06000009 RID: 9 RVA: 0x000020E4 File Offset: 0x000002E4
        private void button5_Click(object sender, EventArgs e)
        {
            bool flag = Functions.openfiledialog.ShowDialog() == DialogResult.OK;
            if (flag)
            {
                try
                {
                    this.webBrowser1.Document.InvokeScript("SetText", new object[]
                    {
                        File.ReadAllText(Functions.openfiledialog.FileName)
                    });
                }
                catch (Exception )
                {
                    MessageBox.Show("This file is corrupted or not supported.", "Synapse X");
                }
            }
        }

        // Token: 0x0600000A RID: 10 RVA: 0x0000215C File Offset: 0x0000035C
        private void button7_Click(object sender, EventArgs e)
        {
           /* HtmlDocument document = this.webBrowser1.Document;
            string scriptName = "GetText";
            object[] args = new string[0];
            string value = document.InvokeScript(scriptName, args).ToString();
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text Files (.txt)|*.txt";
            saveFileDialog.Title = "Save Script";
            bool flag = saveFileDialog.ShowDialog() != DialogResult.OK;
            if (!flag)
            {
                StreamWriter streamWriter = new StreamWriter(saveFileDialog.FileName);
                streamWriter.Write(value);
                streamWriter.Close();
            } */
        }

        // Token: 0x0600000B RID: 11 RVA: 0x000021DC File Offset: 0x000003DC
        private void addIntel(string label, string kind, string detail, string insertText)
        {
            string text = "\"" + label + "\"";
            string text2 = "\"" + kind + "\"";
            string text3 = "\"" + detail + "\"";
            string text4 = "\"" + insertText + "\"";
            this.webBrowser1.Document.InvokeScript("AddIntellisense", new object[]
            {
                label,
                kind,
                detail,
                insertText
            });
        }

        // Token: 0x0600000C RID: 12 RVA: 0x0000225C File Offset: 0x0000045C
        private void addGlobalF()
        {
            string[] array = File.ReadAllLines(this.defPath + "//globalf.txt");
            foreach (string text in array)
            {
                bool flag = text.Contains(':');
                bool flag2 = flag;
                if (flag2)
                {
                    this.addIntel(text, "Function", text, text.Substring(1));
                }
                else
                {
                    this.addIntel(text, "Function", text, text);
                }
            }
        }

        // Token: 0x0600000D RID: 13 RVA: 0x000022D4 File Offset: 0x000004D4
        private void addGlobalV()
        {
            foreach (string text in File.ReadLines(this.defPath + "//globalv.txt"))
            {
                this.addIntel(text, "Variable", text, text);
            }
        }

        // Token: 0x0600000E RID: 14 RVA: 0x00002340 File Offset: 0x00000540
        private void addGlobalNS()
        {
            foreach (string text in File.ReadLines(this.defPath + "//globalns.txt"))
            {
                this.addIntel(text, "Class", text, text);
            }
        }

        // Token: 0x0600000F RID: 15 RVA: 0x000023AC File Offset: 0x000005AC
        private void addMath()
        {
            foreach (string text in File.ReadLines(this.defPath + "//classfunc.txt"))
            {
                this.addIntel(text, "Method", text, text);
            }
        }

        // Token: 0x06000010 RID: 16 RVA: 0x00002418 File Offset: 0x00000618
        private void addBase()
        {
            foreach (string text in File.ReadLines(this.defPath + "//base.txt"))
            {
                this.addIntel(text, "Keyword", text, text);
            }
        }

        // Token: 0x06000011 RID: 17 RVA: 0x00002484 File Offset: 0x00000684
        private async void Form1_Load(object sender, EventArgs e)
        {
            base.TopMost = true;
            this.listBox1.Items.Clear();
            Functions.PopulateListBox(this.listBox1, "./scripts", "*.txt");
            Functions.PopulateListBox(this.listBox1, "./scripts", "*.lua");
            WebClient wc = new WebClient();
            wc.Proxy = null;
            try
            {
                RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Internet Explorer\\Main\\FeatureControl\\FEATURE_BROWSER_EMULATION", true);
                string friendlyName = AppDomain.CurrentDomain.FriendlyName;
                bool flag2 = registryKey.GetValue(friendlyName) == null;
                bool flag3 = flag2;
                if (flag3)
                {
                    registryKey.SetValue(friendlyName, 11001, RegistryValueKind.DWord);
                }
                registryKey = null;
                friendlyName = null;
                registryKey = null;
                friendlyName = null;
            }
            catch (Exception)
            {
            }
            this.webBrowser1.Url = new Uri(string.Format("file:///{0}/Monaco/Monaco.html", Directory.GetCurrentDirectory()));
            await Task.Delay(500);
            this.webBrowser1.Document.InvokeScript("SetTheme", new string[]
            {
                "Dark"
            });
            this.addBase();
            this.addMath();
            this.addGlobalNS();
            this.addGlobalV();
            this.addGlobalF();
            this.webBrowser1.Document.InvokeScript("SetText", new object[]
            {
                ""
            });
        }

        // Token: 0x06000012 RID: 18 RVA: 0x000024CE File Offset: 0x000006CE
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            webBrowser1.Document.InvokeScript("SetText", new object[]
           {
                ""
           });
            Console.WriteLine("Monaco cleared");
            string dabberson = File.ReadAllText("Scripts\\" + this.listBox1.SelectedItem.ToString());
            bool flag = this.listBox1.SelectedItem != null;
            if (flag)
            {
                webBrowser1.Document.InvokeScript("SetText", new object[]
          {
                (dabberson)
          });


            }
        }

        // Token: 0x06000013 RID: 19 RVA: 0x000024F6 File Offset: 0x000006F6
        private void button10_Click(object sender, EventArgs e)
        {
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

        // Token: 0x06000014 RID: 20 RVA: 0x00002500 File Offset: 0x00000700
        private void button1_Click(object sender, EventArgs e)
        {
            HtmlDocument text = webBrowser1.Document;
            string scriptName = "GetText";
            object[] args = new string[0];
            object obj = text.InvokeScript(scriptName, args);
            string script = obj.ToString();
            
            
                m.ExecuteScript(script);
            
        }

        // Token: 0x06000015 RID: 21 RVA: 0x0000254C File Offset: 0x0000074C
        private void Button6_Click(object sender, EventArgs e)
        {

        }

        // Token: 0x06000016 RID: 22 RVA: 0x00002573 File Offset: 0x00000773
        private void Button3_MouseEnter(object sender, EventArgs e)
        {
            this.button3.FlatAppearance.BorderSize = 1;
            this.button3.FlatAppearance.BorderColor = Color.White;
        }

        // Token: 0x06000017 RID: 23 RVA: 0x0000259E File Offset: 0x0000079E
        private void Button3_MouseLeave(object sender, EventArgs e)
        {
            this.button3.FlatAppearance.BorderSize = 0;
            this.button3.FlatAppearance.BorderColor = Color.White;
        }

        // Token: 0x06000018 RID: 24 RVA: 0x000025C9 File Offset: 0x000007C9
        private void Button2_MouseEnter(object sender, EventArgs e)
        {
            this.button2.FlatAppearance.BorderSize = 1;
            this.button2.FlatAppearance.BorderColor = Color.White;
        }

        // Token: 0x06000019 RID: 25 RVA: 0x000025F4 File Offset: 0x000007F4
        private void Button2_MouseLeave(object sender, EventArgs e)
        {
            this.button2.FlatAppearance.BorderSize = 0;
            this.button2.FlatAppearance.BorderColor = Color.White;
        }

        // Token: 0x0600001A RID: 26 RVA: 0x0000261F File Offset: 0x0000081F
        private void Button1_MouseEnter(object sender, EventArgs e)
        {
            this.button1.FlatAppearance.BorderSize = 1;
            this.button1.FlatAppearance.BorderColor = Color.White;
        }

        // Token: 0x0600001B RID: 27 RVA: 0x0000264A File Offset: 0x0000084A
        private void Button1_MouseLeave(object sender, EventArgs e)
        {
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatAppearance.BorderColor = Color.White;
        }

        // Token: 0x0600001C RID: 28 RVA: 0x00002675 File Offset: 0x00000875
        private void Button4_MouseEnter(object sender, EventArgs e)
        {
            this.button4.FlatAppearance.BorderSize = 1;
            this.button4.FlatAppearance.BorderColor = Color.White;
        }

        // Token: 0x0600001D RID: 29 RVA: 0x000026A0 File Offset: 0x000008A0
        private void Button4_MouseLeave(object sender, EventArgs e)
        {
            this.button4.FlatAppearance.BorderSize = 0;
            this.button4.FlatAppearance.BorderColor = Color.White;
        }

        // Token: 0x0600001E RID: 30 RVA: 0x000026CB File Offset: 0x000008CB
        private void Button5_MouseEnter(object sender, EventArgs e)
        {
            this.button5.FlatAppearance.BorderSize = 1;
            this.button5.FlatAppearance.BorderColor = Color.White;
        }

        // Token: 0x0600001F RID: 31 RVA: 0x000026F6 File Offset: 0x000008F6
        private void Button5_MouseLeave(object sender, EventArgs e)
        {
            this.button5.FlatAppearance.BorderSize = 0;
            this.button5.FlatAppearance.BorderColor = Color.White;
        }

        // Token: 0x06000020 RID: 32 RVA: 0x00002721 File Offset: 0x00000921
        private void Button6_MouseEnter(object sender, EventArgs e)
        {
            this.button6.FlatAppearance.BorderSize = 1;
            this.button6.FlatAppearance.BorderColor = Color.White;
        }

        // Token: 0x06000021 RID: 33 RVA: 0x0000274C File Offset: 0x0000094C
        private void Button6_MouseLeave(object sender, EventArgs e)
        {
            this.button6.FlatAppearance.BorderSize = 0;
            this.button6.FlatAppearance.BorderColor = Color.White;
        }

        // Token: 0x06000022 RID: 34 RVA: 0x00002777 File Offset: 0x00000977
        private void Button7_MouseEnter(object sender, EventArgs e)
        {
            this.button7.FlatAppearance.BorderSize = 1;
            this.button7.FlatAppearance.BorderColor = Color.White;
        }

        // Token: 0x06000023 RID: 35 RVA: 0x000027A2 File Offset: 0x000009A2
        private void Button7_MouseLeave(object sender, EventArgs e)
        {
            this.button7.FlatAppearance.BorderSize = 0;
            this.button7.FlatAppearance.BorderColor = Color.White;
        }

        // Token: 0x06000024 RID: 36 RVA: 0x000027CD File Offset: 0x000009CD
        private void Button8_MouseEnter(object sender, EventArgs e)
        {
            this.button8.FlatAppearance.BorderSize = 1;
            this.button8.FlatAppearance.BorderColor = Color.White;
        }

        // Token: 0x06000025 RID: 37 RVA: 0x000027F8 File Offset: 0x000009F8
        private void Button8_MouseLeave(object sender, EventArgs e)
        {
            this.button8.FlatAppearance.BorderSize = 0;
            this.button8.FlatAppearance.BorderColor = Color.White;
        }

        // Token: 0x06000026 RID: 38 RVA: 0x00002823 File Offset: 0x00000A23
        private void Button10_MouseEnter(object sender, EventArgs e)
        {
            this.button10.FlatAppearance.BorderSize = 1;
            this.button10.FlatAppearance.BorderColor = Color.White;
        }

        // Token: 0x06000027 RID: 39 RVA: 0x0000284E File Offset: 0x00000A4E
        private void Button10_MouseLeave(object sender, EventArgs e)
        {
            this.button10.FlatAppearance.BorderSize = 0;
            this.button10.FlatAppearance.BorderColor = Color.White;
        }

        // Token: 0x06000028 RID: 40 RVA: 0x00002879 File Offset: 0x00000A79
        

        // Token: 0x0600002A RID: 42 RVA: 0x000028CF File Offset: 0x00000ACF
        private void Button2_Click(object sender, EventArgs e)
        {

            base.Close();
            MainUI form = new MainUI();
            form.Show();
        }

        // Token: 0x0600002B RID: 43 RVA: 0x000028D9 File Offset: 0x00000AD9
        private void Button3_Click(object sender, EventArgs e)
        {
            base.WindowState = FormWindowState.Minimized;
        }

        // Token: 0x0600002C RID: 44 RVA: 0x000028E4 File Offset: 0x00000AE4
        private void Button8_Click(object sender, EventArgs e)
        {
            Process.Start("www.pornhub.com");
        }

        // Token: 0x0600002D RID: 45 RVA: 0x000028F4 File Offset: 0x00000AF4
        private void Label3_Click(object sender, EventArgs e)
        {
        }

        // Token: 0x0600002E RID: 46 RVA: 0x000028F7 File Offset: 0x00000AF7
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
        }

        // Token: 0x0600002F RID: 47 RVA: 0x000028FA File Offset: 0x00000AFA
        private void panel1_Paint(object sender, PaintEventArgs e)
        {
        }

        // Token: 0x06000030 RID: 48 RVA: 0x00002900 File Offset: 0x00000B00
        

        // Token: 0x04000002 RID: 2
        private WebClient wc = new WebClient();

        // Token: 0x04000003 RID: 3
        private string defPath = Application.StartupPath + "//Monaco//";

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

        }
    }
}
