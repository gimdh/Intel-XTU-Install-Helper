
using System;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Management;

namespace IntelXTU_Installer
{
    partial class Program
    {
        static AutoResetEvent XTUFailed;

        static string MSIPath;

        static void StartXTUEXEInstaller(string fileName)
        {
            XTUFailed = new AutoResetEvent(false);

            ProcessStartInfo processStartInfo = new ProcessStartInfo();
            processStartInfo.FileName = fileName;
            processStartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            processStartInfo.CreateNoWindow = true;
            
            using (Process proc = Process.Start(processStartInfo))
            {
                XTUFailed.WaitOne();
                RecursiveKill(proc.Id);
            }
        }

        static void StartXTUMSIInstaller()
        {
            ProcessStartInfo processStartInfo = new ProcessStartInfo();
            processStartInfo.FileName = "msiexec";
            processStartInfo.Arguments = "/i \"" + MSIPath + "\" DISABLEPLATFORMCHECK=1";

            processStartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            processStartInfo.CreateNoWindow = true;

            using (Process proc = Process.Start(processStartInfo))
            {
                proc.WaitForExit();
            }
        }

        static void WaitXTUFailEvent()
        {
            using (EventLog eventLog = new EventLog("Application"))
            {
                eventLog.EntryWritten += new EntryWrittenEventHandler(EventLog_EntryWritten);
                eventLog.EnableRaisingEvents = true;
                XTUFailed.WaitOne();
                eventLog.EnableRaisingEvents = false;
            }
            XTUFailed.Set();
        }

        private static void EventLog_EntryWritten(object sender, EntryWrittenEventArgs e)
        {
            var reg = Regex.Match(e.Entry.Message, @"C:\\ProgramData\\Package Cache\\(.+)[Xx][Tt][Uu](.+)msi");
          
            if (reg.Success)
            {
                MSIPath = reg.Value;
                XTUFailed.Set();
            }
        }

        private static void RecursiveKill(int pid)
        {
            if (pid == 0)
                return;
                
            ManagementObjectSearcher mos = new ManagementObjectSearcher("Select * From Win32_Process Where ParentProcessID=" + pid);
            ManagementObjectCollection moc = mos.Get();
            foreach (ManagementObject mo in moc)
            {
                RecursiveKill(Convert.ToInt32(mo["ProcessID"]));
            }
            try
            {
                Process proc = Process.GetProcessById(pid);
                proc.Kill();
            }
            catch { }
        }
    }
}