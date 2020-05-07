using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;
using EasyExploits;

namespace ProjectMainDab
{
    // Work in progress script hub
    public partial class Hub1 : Form
    {
        Module m = new Module();
        public Hub1()
        {
            InitializeComponent();
            
        }

        private void Hub1_Load(object sender, EventArgs e)
        {
            
        }

       

        private void pictureBox14_Click(object sender, EventArgs e)
        {
                    }

        private void metroButton13_Click(object sender, EventArgs e)
        {
            string script = ("loadstring(game:HttpGet(('https://pastebin.com/raw/qUPwqTyr'),true))()");
            m.ExecuteScript(script);
        }

        private void metroButton19_Click(object sender, EventArgs e)
        {

        }
    }
}