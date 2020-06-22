using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PluginUpdate
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Updating plugins");
            string file = ("Plugins.exe");
            if (File.Exists(file))
            {
                File.Delete(file);
            }
            
           
            ServicePointManager.ServerCertificateValidationCallback = (a, b, c, d) => true; // eh just to make sure
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12; // set the protocol type to TLS 1.2
            Console.WriteLine("Downloading new Plugins");
            var client = new WebClient();
            client.DownloadFile("https://github.com/leonardssy/ProjectDab/blob/master/Plugins.exe?raw=true", "Plugins.exe");
            Console.WriteLine("Plugins downloaded.");
            System.Threading.Thread.Sleep(1000);
            Process.Start("Plugins.exe");
            Environment.Exit(0);
        }
    }
}
