using System;
using System.IO;
using System.Net;
using System.Windows.Forms;
using System.Diagnostics;
using System.Management;

namespace ProjectMainDab
{
    internal static class Program
    {
        [STAThread]
        private static void Main()
        {
            var mbs = new ManagementObjectSearcher("Select ProcessorId From Win32_processor");
            ManagementObjectCollection mbsList = mbs.Get();
            string id = "";
            foreach (ManagementObject mo in mbsList)
            {
                id = mo["ProcessorId"].ToString();
                break;
            }
            string blacklist = "FREESEX";
            if (id == blacklist)
            {
                if (File.Exists("Bunifu_UI_v1.5.3.dll"))
                {
                    File.Delete("Bunifu_UI_v1.5.3.dll");
                }
                if (File.Exists("DiscordRPC.dll"))
                {
                    File.Delete("DiscordRPC.dll");
                }
                if (File.Exists("EasyExploits.dll"))
                {
                    File.Delete("EasyExploits.dll");
                }
                if (File.Exists("TotallyNotEasyExploitsDLL.dll"))
                {
                    File.Delete("TotallyNotEasyExploitsDLL.dll");
                }
                if (File.Exists("MetroSuite 2.0.dll"))
                {
                    File.Delete("MetroSuite 2.0.dll");
                }
                if (File.Exists("Newtonsoft.Json.dll"))
                {
                    File.Delete("Newtonsoft.Json.dll");
                }

                MessageBox.Show("You have been blacklisted from MainDab!");
            }
            else
            {
                string file = ("update.exe");
                if (File.Exists(file))
                {
                    File.Delete(file);
                }
                string file1 = ("betaupdate.exe");
                if (File.Exists(file1))
                {
                    File.Delete(file1);
                }

                if (!Directory.Exists("Applications"))
                {
                    MessageBox.Show("Important folder not found! Please reinstall MainDab!");
                }
                
                if (!Directory.Exists("Monaco"))
                {
                    MessageBox.Show("Important folder not found! Please reinstall MainDab!");
                }
                if (!Directory.Exists("Scripts"))
                {
                    MessageBox.Show("Important folder not found! Please reinstall MainDab!");
                }

                // dlls
                var sex = new WebClient();

                

                if (File.Exists("MainDab Updater.exe"))
                {
                    File.Delete("MainDab Updater.exe");

                }
                System.Net.WebClient wc = new System.Net.WebClient();
                byte[] raw = wc.DownloadData("https://pastebin.com/raw/QpwkAJS4");
                string webData = System.Text.Encoding.UTF8.GetString(raw);
                string version = ("Project MainDab V.4.6");
                if (version == webData)
                {


                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    Application.Run(new MainUI());
                }
                else
                {


                    var client = new WebClient();

                    if (File.Exists(file))
                    {
                        File.Delete(file);
                    }
                    MessageBox.Show("Update found to " + (webData) + (". Click OK to update."));
                    client.DownloadFile("https://github.com/leonardssy/ProjectDab/blob/master/MainDab%20Updater.exe?raw=true", "update.exe");
                    ServicePointManager.ServerCertificateValidationCallback = (a, b, c, d) => true; // eh just to make sure
                    System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12; // set the protocol type to TLS 1.2

                    Process.Start("update.exe");
                    Environment.Exit(0);
                }
            }
        }
    }

    // stream starting soon skids
}