using System.Diagnostics;
using System.Management;
using TrionControlPanel.Alerts;
using TrionControlPanelSettings;

namespace TrionControlPanel.Classes
{
    internal class SystemStatus
    {
        Settings Settings = new();
        int worldRamUsage;
        int totalRam = 0;
        int WorldPID = 0;
        int BnetPID = 0;

        public string WorldStatusName; 
        public string BnetStatusName; 
        public string MySqlStatusName; 

        internal void KillMysql()
        {
            MySqlStatusName = Settings._Data.MySQLExecutableName;
            foreach (var process in Process.GetProcessesByName(MySqlStatusName))
            {
                process.Kill();
            }
        }
        internal void KillWorld()
        {
            WorldStatusName = Settings._Data.WorldExecutableName;
            foreach (var process in Process.GetProcessesByName(WorldStatusName))
            {
                process.Kill();
            }
        }
        internal void KillBnet ()
        {
            BnetStatusName = Settings._Data.BnetExecutableLocation;
            foreach (var process in Process.GetProcessesByName(BnetStatusName))
            {
                process.Kill();
            }
        }
        internal bool WorldStatus()
        {
           WorldStatusName = Settings._Data.WorldExecutableName;
           Process[] pname = Process.GetProcessesByName(WorldStatusName);
            if (pname.Length == 0)
                return false;
            else
                return true;
        }
        internal bool BnetStatus()
        {
            BnetStatusName = Settings._Data.BnetExecutableLocation;
            Process[] pname = Process.GetProcessesByName(BnetStatusName);
            if (pname.Length == 0)
                return false;
            else
                return true;
        }
        internal bool MySQLstatus()
        {
            string MySqlStatusName = Settings._Data.MySQLExecutableName;
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
                WorldStatusName = Settings._Data.WorldExecutableName;
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
               WorldStatusName = Settings._Data.WorldExecutableName;
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
                BnetStatusName = Settings._Data.BnetExecutableLocation; ;
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
                BnetStatusName = Settings._Data.BnetExecutableLocation; 
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
                    myProcess.StartInfo.FileName = Settings._Data.WorldExecutableName;

                    if (Settings._Data.ConsolHide == false)
                    {
                        myProcess.StartInfo.CreateNoWindow = false;
                        myProcess.Start();
                    }
                    else if (Settings._Data.ConsolHide == true)
                    {
                        myProcess.StartInfo.CreateNoWindow = true;
                        myProcess.Start();
                    }
                    FormAlert.ShowAlert("Starting World Server!", NotificationType.Info);
                }
            }
            catch (Exception ex)
            {
                FormAlert.ShowAlert(ex.Message, NotificationType.Error);
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
                    myProcess.StartInfo.FileName = Settings._Data.BnetExecutableName;

                    if (Settings._Data.ConsolHide == false)
                    {
                        myProcess.StartInfo.CreateNoWindow = false;
                        myProcess.Start();
                    }
                    else if (Settings._Data.ConsolHide == true)
                    {
                        myProcess.StartInfo.CreateNoWindow = true;
                        myProcess.Start();
                    }
                    FormAlert.ShowAlert("Starting Bnet Server!", NotificationType.Info);
                }
            }
            catch (Exception ex)
            {
                FormAlert.ShowAlert(ex.Message, NotificationType.Error);
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
                    myProcess.StartInfo.FileName = $@"{Settings._Data.MySQLExecutablePath}";

                    if (Settings._Data.ConsolHide == false)
                    {
                        Process.Start($@"{Settings._Data.MySQLExecutablePath}", "--console");
                    }
                    else if (Settings._Data.ConsolHide == true)
                    {
                        myProcess.StartInfo.CreateNoWindow = true;
                        myProcess.Start();
                    }
                    FormAlert.ShowAlert("Starting MySQL Server!", NotificationType.Info);
                }
            }
            catch (Exception ex)
            {
                FormAlert.ShowAlert(ex.Message, NotificationType.Error);
            }
        }
    }
}
