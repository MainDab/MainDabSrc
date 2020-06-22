using System.IO;
using System.Threading;
using System.Windows.Forms;
using System;

namespace ProjectMainDab
{
    internal class Functions
    {
        public static string exploitdllname = "MainDab.dll";//Axon.dll this is the name of your dll

        public static void Inject()
        {
            new Thread(() =>
            {
                if (NamedPipes.NamedPipeExist(NamedPipes.luapipename))//check if the pipe exist
                {
                    MessageBox.Show("Already injected!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);//if the pipe exist that's mean that we don't need to inject
                    return;
                }
                else if (!NamedPipes.NamedPipeExist(NamedPipes.luapipename))//check if the pipe don't exist
                {
                    switch (Injector.DllInjector.GetInstance.Inject("RobloxPlayerBeta", AppDomain.CurrentDomain.BaseDirectory + exploitdllname))//Process name and dll directory
                    {
                        case Injector.DllInjectionResult.DllNotFound://if can't find the dll
                            MessageBox.Show("MainDab.dll was not found! Please restart the application!");//display messagebox to tell that dll was not found
                            return;

                        case Injector.DllInjectionResult.GameProcessNotFound://if can't find the process
                            MessageBox.Show("Couldn't find RobloxPlayerBeta.exe!", "Roblox isn't started!");//display messagebox to tell that proccess was not found
                            return;

                        case Injector.DllInjectionResult.InjectionFailed://if injection fails(this don't work or only on special cases)
                            MessageBox.Show("Injection Failed!", "Failed for whatever reason (try kill roblox or restart ur pc)", MessageBoxButtons.OK, MessageBoxIcon.Error);//display messagebox to tell that injection failed
                            return;
                    }
                    
                }
            }).Start();
        }

        public static OpenFileDialog openfiledialog = new OpenFileDialog
        {
            Filter = "Script File|*.txt;*.lua|All files (*.*)|*.*",//add txt,lua and all files filter
            FilterIndex = 1,//choose what filter will be the default
            RestoreDirectory = true,//restore the last used directory
            Title = "Open File"//OpenFileDialog Tittle
        };//Initialize OpenFileDialog

        public static void PopulateListBox(ListBox lsb, string Folder, string FileType)
        {
            DirectoryInfo dinfo = new DirectoryInfo(Folder);
            FileInfo[] Files = dinfo.GetFiles(FileType);
            foreach (FileInfo file in Files)
            {
                lsb.Items.Add(file.Name);
            }
        }

        public static void PopulateListBox1(ListBox lsb, string Folder, string FileType)
        {
            DirectoryInfo dinfo = new DirectoryInfo(Folder);
            FileInfo[] Files = dinfo.GetFiles(FileType);
            foreach (FileInfo file in Files)
            {
                lsb.Items.Add(file.Name);
            }
        }
    }
}