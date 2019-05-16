using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IntelXTU_Installer
{
    partial class Program
    {
        static void Main(string[] args)
        {
            string fileName = "";

            try
            {
                if (args.Count() == 2)
                {
                    fileName = args[1].Trim();

                    string ver = ValidateInstaller(ref fileName);

                    if (ver != "")
                    {
                        Console.WriteLine("XTU Installer (ver. {0}) found.", ver);
                    }
                    else
                    {
                        fileNameFromConsole(ref fileName);
                    }
                }
                else
                {
                    fileNameFromConsole(ref fileName);
                }

                Console.WriteLine("\n\nStarting installation process in seconds.");
                Console.Write("Please follow the installer and do ");
                ConsoleColorWrite("NOT", ConsoleColor.Red);
                Console.WriteLine(" close the installer even if it throws an error.");
                Thread.Sleep(3000);

                Task.Run(() => StartXTUEXEInstaller(fileName));
                WaitXTUFailEvent();

                Console.WriteLine("\n\nNow starting actual installer in seconds.");
                Console.WriteLine("Please follow the installer. This should install properly without any error");
                Thread.Sleep(3000);

                StartXTUMSIInstaller();

                Console.WriteLine("\n\nNow it's done! Enjoy Intel XTU.");
                Console.ReadLine();
            }
            catch (Exception e)
            {
                Logger(e);
                return;
            }
        }

        static string ValidateInstaller(ref string fileName)
        {
            //Accepts input w/o file extension
            if (!fileName.EndsWith(".exe"))
                fileName += ".exe";

            if (!File.Exists(fileName))
                return "";

            var fileInfo = FileVersionInfo.GetVersionInfo(fileName);

            if (fileInfo.FileDescription.Trim() != "Intel(R) Extreme Tuning Utility"
                || fileInfo.ProductName.Trim() != "Intel(R) Extreme Tuning Utility"
                || fileInfo.LegalCopyright.Trim() != "Copyright (c) Intel Corporation. All rights reserved."
                || fileInfo.OriginalFilename.Trim() != "XTUUISetup.exe")
            {
                return "";
            }

            return fileInfo.ProductVersion;
        }

        static void Logger(Exception e)
        {
            using (StreamWriter writer = new StreamWriter("error.log"))
            {
                writer.WriteLine(DateTime.Now.ToString());
                writer.WriteLine("Message:\n{0}StackTrace:\n{1}", e.Message, e.StackTrace);
            }
            Console.WriteLine("\n\nSomething went wrong. ");
        }
    }
}
