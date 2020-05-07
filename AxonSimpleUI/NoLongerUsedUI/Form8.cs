using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjectMainDab
{
    // Discontinued part of MainDab, no one used it.
    public partial class Form8 : Form
    {
        public Form8()
        {
            InitializeComponent();
        }

        WebClient webClient = new WebClient();
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            
            if (File.Exists("MainDabCMD.exe"))
            {
                MessageBox.Show("MainDabCMD is already downloaded!");
               
            }
            else
            {
                webClient.DownloadFile("https://github.com/leonardssy/ProjectDab/blob/master/MainDabCMD.exe?raw=true", "MainDabCMD.exe");
                Process.Start("MainDabCMD.exe");
                Environment.Exit(0);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (File.Exists("MainDabChiSimp.exe"))
            {
                MessageBox.Show("MainDabChiSimp.exe");

            }
            else
            {
                webClient.DownloadFile("https://github.com/leonardssy/ProjectDab/blob/master/MainDabChiSimp.exe?raw=true", "MainDabChiSimp.exe");
                Process.Start("MainDabChiSimp.exe");
                Environment.Exit(0);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            if (File.Exists("MainDabChi.exe"))
            {
                MessageBox.Show("MainDabChi.exe");

            }
            else
            {
                webClient.DownloadFile("https://github.com/leonardssy/ProjectDab/blob/master/MainDabChi.exe?raw=true", "MainDabChi.exe");
                Process.Start("MainDabChi.exe");
                Environment.Exit(0);
            }
        }

        private void Form8_Load(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }
    }
}
