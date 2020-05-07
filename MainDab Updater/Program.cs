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
        // Updater for maindab. Would download when MainDab needed to update.
        static void Main(string[] args)
        {
            Console.WriteLine("Starting update...");
            System.Threading.Thread.Sleep(1000);
            Console.WriteLine("Deleting MainDab...");
            string file = ("MainDab.exe");
            if (File.Exists(file))
            {
                File.Delete(file);
            }
            ServicePointManager.ServerCertificateValidationCallback = (a, b, c, d) => true; // eh just to make sure
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12; // set the protocol type to TLS 1.2
            Console.WriteLine("Downloading new MainDab EXE...");
            var client = new WebClient();
            client.DownloadFile("https://github.com/leonardssy/ProjectDab/blob/master/MainDab.exe?raw=true", "MainDab.exe");
            Console.WriteLine("MainDab downloaded.");
            System.Threading.Thread.Sleep(1000);
            Environment.Exit(0);
        }
    }
}
