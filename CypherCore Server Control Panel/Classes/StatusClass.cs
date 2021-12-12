using CypherCoreServerLaucher.Properties;
using System.Diagnostics;
using System.Management;

namespace CypherCore_Server_Laucher.Classes
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
        public string ApacheStatusName = Settings.Default.ApacheCoreName;

        internal void KillWorld()
        {
            foreach (var process in Process.GetProcessesByName(WorldStatusName))
            {
                process.Kill();
            }
        }
        internal void KillBnet ()
        {

            foreach (var process in Process.GetProcessesByName(BnetStatusName))
            {
                process.Kill();
            }

        }
        internal bool WorldStatus()
        {
            Process[] pname = Process.GetProcessesByName(WorldStatusName);
            if (pname.Length == 0)
                return false;
            else
                return true;
        }
        internal bool BnetStatus()
        {
            Process[] pname = Process.GetProcessesByName(BnetStatusName);
            if (pname.Length == 0)
                return false;
            else
                return true;
        }
        internal bool MySQLstatus()
        {
            Process[] pname = Process.GetProcessesByName(MySqlStatusName);
            if (pname.Length == 0)
                return false;
            else
                return true;
        }
        internal bool ApacheStatus()
        {
            Process[] pname = Process.GetProcessesByName(ApacheStatusName);
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
    }
}
