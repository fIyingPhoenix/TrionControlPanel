using TrionControlPanel.Properties;
using System.Diagnostics;
using System.Management;
using TrionControlPanel.Forms;

namespace TrionControlPanel.Classes
{
    internal class StatusClass
    {
        int worldRamUsage;
        int totalRam = 0;
        int WorldPID = 0;
        int BnetPID = 0;

        public string WorldStatusName = Settings.Default.WorldCoreName;
        public string BnetStatusName = Settings.Default.BnetCoreName;
        public string MySqlStatusName = Settings.Default.MySQLCoreName;
        public static void Alert(string message, NotificationType eType)
        {
            //make the laert work.
            FormAlert frm = new(); //dont change this. its fix the Cannot access a disposed object and scall the notification up.
            frm.ShowAlert(message, eType);
        }
        internal void KillMysql()
        {
            MySqlStatusName = Settings.Default.MySQLCoreName;
            foreach (var process in Process.GetProcessesByName(MySqlStatusName))
            {
                process.Kill();
            }
        }
        internal void KillWorld()
        {
            WorldStatusName = Settings.Default.WorldCoreName;
            foreach (var process in Process.GetProcessesByName(WorldStatusName))
            {
                process.Kill();
            }
        }
        internal void KillBnet ()
        {
            BnetStatusName = Settings.Default.BnetCoreName;
            foreach (var process in Process.GetProcessesByName(BnetStatusName))
            {
                process.Kill();
            }
        }
        internal bool WorldStatus()
        {
           WorldStatusName = Settings.Default.WorldCoreName;
           Process[] pname = Process.GetProcessesByName(WorldStatusName);
            if (pname.Length == 0)
                return false;
            else
                return true;
        }
        internal bool BnetStatus()
        {
            BnetStatusName = Settings.Default.BnetCoreName;
            Process[] pname = Process.GetProcessesByName(BnetStatusName);
            if (pname.Length == 0)
                return false;
            else
                return true;
        }
        internal bool MySQLstatus()
        {
            string MySqlStatusName = Settings.Default.MySQLCoreName;
            Process[] pname = Process.GetProcessesByName(MySqlStatusName);
            if (pname.Length == 0)
                return false;
            else
                return true;
        }
        internal int TotalPCRam()
        {
            ObjectQuery wql = new("SELECT * FROM Win32_OperatingSystem");
            ManagementObjectSearcher searcher = new(wql);
            ManagementObjectCollection results = searcher.Get();

            double res;

            foreach (ManagementObject result in results)
            {
                res = Convert.ToDouble(result["TotalVisibleMemorySize"]);
                double fres = Math.Round((res / 1024d));
                totalRam = Convert.ToInt32(fres.ToString());
            }
            return totalRam;
        }
        internal  int TotalCpuUsage()
        {
            var cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total", Environment.MachineName);
            cpuCounter.NextValue();
            Thread.Sleep(1000);
            return (int)cpuCounter.NextValue();
        }
        internal int CurentPcRamUsage()
        {
            try 
            {
                ManagementClass cimobject2 = new("Win32_PerfFormattedData_PerfOS_Memory");
                ManagementObjectCollection results = cimobject2.GetInstances();
                double res;

                foreach (ManagementObject result in results)
                {
                    res = Convert.ToDouble(result["AvailableMBytes"]);
                    double fres = Math.Round(res);
                    totalRam = Convert.ToInt32(fres.ToString());
                }
                return totalRam;
            }
            catch
            {
                return 0;
            }
        }
        internal int WorldRamUsage()
        {
            try
             {
                WorldStatusName = Settings.Default.WorldCoreName;
                var processes = Process.GetProcessesByName(WorldStatusName);
                foreach (var p in processes)
                {
                    WorldPID = p.Id;
                }
                Process process = Process.GetProcessById(WorldPID);
                PerformanceCounter ramCounter = new("Process", "Working Set", process.ProcessName);
                while (true)
                {
                    double ram = ramCounter.NextValue();
                    worldRamUsage = Convert.ToInt32(ram / 1024d / 1024d);
                    return worldRamUsage;
                }
            }
            catch 
            {
                 return 0;
            }
        }
        internal int WorldCpuUsage()
        {
            try
            {
               WorldStatusName = Settings.Default.WorldCoreName;
                var processes = Process.GetProcessesByName(WorldStatusName);
                foreach (var p in processes)
                {
                    WorldPID = p.Id;
                }
                Process process = Process.GetProcessById(WorldPID);
                var cpuCounter = new PerformanceCounter("Process", "% Processor Time", process.ProcessName);
                cpuCounter.NextValue();
                Thread.Sleep(1000);
                return (int)cpuCounter.NextValue();
            }
            catch
            {
                return 0;
            }
        }
        internal int BnetRamUsage()
        {
            try
            {
                BnetStatusName = Settings.Default.BnetCoreName;
                var processes = Process.GetProcessesByName(BnetStatusName);
                foreach (var p in processes)
                {
                    BnetPID = p.Id;
                }
                Process process = Process.GetProcessById(BnetPID);
                PerformanceCounter ramCounter = new("Process", "Working Set", process.ProcessName);
                while (true)
                {
                    double ram = ramCounter.NextValue();
                    worldRamUsage = Convert.ToInt32(ram / 1024d / 1024d);
                    return worldRamUsage;
                }
            }
            catch
            {
                return 0;
            }
        }
        internal int BnetCpuUsage()
        {
            try
            {
                BnetStatusName = Settings.Default.BnetCoreName;
                var processes = Process.GetProcessesByName(BnetStatusName);
                foreach (var p in processes)
                {
                    BnetPID = p.Id;
                }
                Process process = Process.GetProcessById(BnetPID);
                var cpuCounter = new PerformanceCounter("Process", "% Processor Time", process.ProcessName);
                cpuCounter.NextValue();
                Thread.Sleep(1000);

                return (int)cpuCounter.NextValue();
            }
            catch
            {
                return 0;
            }
        }
        internal void StartWorld()
        {
            try
            {
                using (Process myProcess = new())
                {
                    myProcess.StartInfo.UseShellExecute = false;
                    // You can start any process, HelloWorld is a do-nothing example.
                    myProcess.StartInfo.FileName = Settings.Default.WorldCoreLocation;

                    if (Settings.Default.TogleConsolHide == false)
                    {
                        myProcess.StartInfo.CreateNoWindow = false;
                        myProcess.Start();
                    }
                    else if (Settings.Default.TogleConsolHide == true)
                    {
                        myProcess.StartInfo.CreateNoWindow = true;
                        myProcess.Start();
                    }
                    Alert("Starting World Server!", NotificationType.Info);
                }
            }
            catch (Exception ex)
            {
                Alert(ex.Message, NotificationType.Error);
            }
        }
        internal void StartBnet()
        {
            try
            {
                using (Process myProcess = new())
                {
                    myProcess.StartInfo.UseShellExecute = false;
                    // You can start any process, HelloWorld is a do-nothing example.
                    myProcess.StartInfo.FileName = Settings.Default.BnetCoreLocation;

                    if (Settings.Default.TogleConsolHide == false)
                    {
                        myProcess.StartInfo.CreateNoWindow = false;
                        myProcess.Start();
                    }
                    else if (Settings.Default.TogleConsolHide == true)
                    {
                        myProcess.StartInfo.CreateNoWindow = true;
                        myProcess.Start();
                    }
                    Alert("Starting Bnet Server!", NotificationType.Info);
                }
            }
            catch (Exception ex)
            {
                Alert(ex.Message, NotificationType.Error);
            }
        }
        internal void StartMysql()
        {
            try
            {
                using (Process myProcess = new())
                {
                    myProcess.StartInfo.UseShellExecute = false;
                    // You can start any process, HelloWorld is a do-nothing example.
                    myProcess.StartInfo.FileName = $@"{Settings.Default.MySQLocation}";

                    if (Settings.Default.TogleConsolHide == false)
                    {
                        Process.Start($@"{Settings.Default.MySQLocation}", "--console");
                    }
                    else if (Settings.Default.TogleConsolHide == true)
                    {
                        myProcess.StartInfo.CreateNoWindow = true;
                        myProcess.Start();
                    }
                    Alert("Starting MySQL Server!", NotificationType.Info);
                }
            }
            catch (Exception ex)
            {
                Alert(ex.Message, NotificationType.Error);
            }
        }

    }
}
