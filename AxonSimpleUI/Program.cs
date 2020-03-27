using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Windows.Forms;

namespace ProjectMainDab
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {

            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);

            
            System.Net.WebClient wc = new System.Net.WebClient();
            byte[] raw = wc.DownloadData("");
            string webData = System.Text.Encoding.UTF8.GetString(raw);
            string version = ("Project MainDab V.2.5.WEGOTRAIDED");
            if (version == webData)
            {
                RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\OurSettings");
                RegistryKey key1 = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\OurSettings1");

                if (key1 != null)
                {


                    
                    

                        WebClient webClient = new WebClient();


                        if (File.Exists("Bunifu_UI_v1.5.3.dll"))
                        {
                        }
                        else
                        {

                            webClient.DownloadFile("", "Bunifu_UI_v1.5.3.dll");
                        }
                        if (File.Exists("DiscordRPC.dll"))
                        {

                        }
                        else
                        {

                            webClient.DownloadFile("", "DiscordRPC.dll");
                        }

                        if (File.Exists("EasyExploits.dll"))
                        {

                        }
                        else
                        {

                            webClient.DownloadFile("", "EasyExploits.dll");
                        }

                    
                        if (File.Exists("Newtonsoft.Json.dll"))
                        {

                        }
                        else
                        {

                            webClient.DownloadFile("", "Newtonsoft.Json.dll");
                        }
                        if (File.Exists("PasteBin API.dll"))
                        {

                        }
                        else
                        {

                            webClient.DownloadFile("", "PasteBin API.dll");
                        }
                        
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    Application.Run(new Form4());

                }
                //if it does exist, retrieve the stored values
                else if (key != null)
                {
                    
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    // Application.Run(new Form3());
                    Application.Run(new Form3());
                }
                else
                {
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    Application.Run(new Form6());
                }
            }
            else
            {
                MessageBox.Show("Update found to " + (webData) + ("! Click OK and the Update will download as a .zip file"));
                var client = new WebClient();
                client.DownloadFile("", (webData) + ".zip");
            }
        }
    }
}