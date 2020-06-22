using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;

namespace MainDab_Updater
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new WebClient();
            Console.WriteLine("Starting update...");
            Console.Title = "MainDab | Updating MainDab...";
            System.Threading.Thread.Sleep(1000);
            Console.WriteLine("Deleting MainDab...");
            string file = ("MainDab.exe");
            if (File.Exists(file))
            {
                File.Delete(file);
            }
            string file1 = ("MainDabBeta.exe");
            if (File.Exists(file1))
            {
                File.Delete(file1);
            }
            if (File.Exists("TotallyNotEasyExploits.dll")) 
            {
                File.Delete("TotallyNotEasyExploits.dll");
                Console.WriteLine("Replacing EasyExploits API with new one...");
            }
            if (File.Exists("TotallyNotEasyExploitsDLL.dll"))
            {
                File.Delete("TotallyNotEasyExploitsDLL.dll");
                Console.WriteLine("Deleting old API");
            }
            if (File.Exists("EasyExploits.dll"))
            {
                File.Delete("TotallyNotEasyExploits.dll");
                
            }
            else
            {
                Console.WriteLine("Downloading new EasyExploits API...");
                var client1 = new WebClient();
                client1.DownloadFile("https://github.com/leonardssy/ProjectDab/blob/master/EasyExploits.dll?raw=true", "EasyExploits.dll");
            }
            if (File.Exists("MoonSharp.Interpreter.dll"))
            {

            }
            else
            {

                Console.WriteLine("Downloading MoonSharp.Interpreter...");
                var client1 = new WebClient();
                client1.DownloadFile("https://github.com/leonardssy/ProjectDab/blob/master/MoonSharp.Interpreter.dll?raw=true", "MoonSharp.Interpreter.dll");
            }
            if (File.Exists("Plugins.exe"))
            {

            }
            if (File.Exists("Applications\\vpn.exe"))
            {
                Console.WriteLine("Deleting VPN...");
                File.Delete("Applications\\vpn.exe");
            }
            else
            {
                Console.WriteLine("Downloading plugins");
                client.DownloadFile("https://github.com/leonardssy/ProjectDab/blob/master/Plugins.exe?raw=true", "Plugins.exe");
            }
            if (File.Exists("PluginUpdate.exe"))
            {
              
            }
            else
            {
                Console.WriteLine("Downloading plguin updater");
               
                client.DownloadFile("https://github.com/leonardssy/ProjectDab/blob/master/PluginUpdate.exe?raw=true", "PluginUpdate.exe");
            }
            if (!Directory.Exists("autoexec"))
            {
                Console.WriteLine("Creating autoexec folder...");
                Directory.CreateDirectory("autoexec");
            }
            if (!Directory.Exists("workspace"))
            {
                Console.WriteLine("Creating workspace folder...");
                Directory.CreateDirectory("workspace");
            }
            ServicePointManager.ServerCertificateValidationCallback = (a, b, c, d) => true; // eh just to make sure
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12; // set the protocol type to TLS 1.2
            Console.WriteLine("Downloading new MainDab EXE...");
           //  var client = new WebClient();
            client.DownloadFile("https://github.com/leonardssy/ProjectDab/blob/master/MainDab.exe?raw=true", "MainDab.exe");
            Console.WriteLine("MainDab downloaded.");
            Console.Title = "MainDab | Starting MainDab in 5 seconds";
            System.Net.WebClient wc = new System.Net.WebClient();
            byte[] raw = wc.DownloadData("https://pastebin.com/raw/6pVUMAGi");
            string webData = System.Text.Encoding.UTF8.GetString(raw);
            Console.WriteLine("\n" + webData + "\n");
            System.Threading.Thread.Sleep(1000);
            Console.Title = "MainDab | Starting MainDab in 4 seconds";
            System.Threading.Thread.Sleep(1000);
            Console.Title = "MainDab | Starting MainDab in 3 seconds";
            System.Threading.Thread.Sleep(1000);
            Console.Title = "MainDab | Starting MainDab in 2 seconds";
            System.Threading.Thread.Sleep(1000);
            Console.Title = "MainDab | Starting MainDab in 1 seconds";
            System.Threading.Thread.Sleep(1000);
            Console.Title = "MainDab | Starting MainDab...";
            System.Threading.Thread.Sleep(500);
            Process.Start("MainDab.exe");
            Environment.Exit(0);
        }
    }
}
