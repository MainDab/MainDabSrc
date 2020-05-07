using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjectMainDab
{
    public partial class TaintCheck : Form
    {
		// Taint/Detected exploit checker. Feel free to copy off this code
        public TaintCheck()
        {
            InitializeComponent();
			int num = 0;
			foreach (string item in Directory.EnumerateFiles(Environment.GetEnvironmentVariable("LocalAppData") + "\\Roblox\\Logs\\archive", "*.ini"))
			{
				string text = File.ReadAllText(item);
				if (text.Contains("IsTainted=true"))
				{
					num++;
					if (text.Contains("TaintingModuleDirectory=") && text.Contains("TaintingModule="))
					{
						string text2 = text.Substring(text.IndexOf("TaintingModule=") + "TaintingModule=".Length);
						text2 = text2.Substring(0, text2.IndexOf("\n"));
						string text3 = text.Substring(text.IndexOf("TaintingModuleDirectory=") + "TaintingModuleDirectory=".Length);
						text3 = text3.Substring(0, text3.IndexOf("\n")) + "\\" + text2;
						RichTextBox richTextBox = richTextBox1;
						richTextBox.Text = richTextBox.Text + File.GetLastWriteTime(item) + ": " + text3 + "\n";
					}
				}
			}
			label3.Text = "Number Of Detections: " + num.ToString();
		}

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

		private void label5_Click(object sender, EventArgs e)
		{
			base.Hide();
		}
	}
}
