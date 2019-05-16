using System;

namespace IntelXTU_Installer
{
    partial class Program
    {
        static void fileNameFromConsole(ref string fileName)
        {
            while (true)
            {
                if (fileName != "")
                    Console.WriteLine("Given filename {0} is not a valid XTU Installer.", fileName);
                Console.Write("Please enter the file name of proper XTU Installer: ");

                fileName = Console.ReadLine();
                string ver = ValidateInstaller(ref fileName);

                if (ver != "")
                {
                    Console.WriteLine("\nXTU Installer (ver. {0}) found.", ver);
                    break;
                }
            }
        }

        static void ConsoleColorWrite(string s, ConsoleColor foreground, ConsoleColor background = ConsoleColor.Black)
        {
            Console.ForegroundColor = foreground;
            Console.BackgroundColor = background;
            Console.Write(s);
            Console.ResetColor();
        }
    }
}