using System;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Threading;

namespace TrionLibrary
{
    public  class SystemWatcher
    {
        public static string Message = string.Empty;
        public static int TotalRam()
        {
            int totalRam = 0;
            ObjectQuery wql = new ObjectQuery("SELECT * FROM Win32_OperatingSystem");
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(wql);
            ManagementObjectCollection results = searcher.Get();
            double res;
            foreach (ManagementObject result in results.Cast<ManagementObject>())
            {
                res = Convert.ToDouble(result["TotalVisibleMemorySize"]);
                double fres = Math.Round((res / 1024d));
                totalRam = Convert.ToInt32(fres);
            }
            return totalRam;
        }
        public static int MachineCpuUtilization()
        {
            var cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total", Environment.MachineName);
            cpuCounter.NextValue();
            Thread.Sleep(1000);
            return (int)cpuCounter.NextValue();
        }
        public static int CurentPcRamUsage()
        {
            int curentram = 0;
            ObjectQuery wql = new ObjectQuery("SELECT * FROM Win32_OperatingSystem");
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(wql);
            ManagementObjectCollection results = searcher.Get();
            double res;
            foreach (ManagementObject result in results.Cast<ManagementObject>())
            {
                    res = Convert.ToDouble(result["FreePhysicalMemory"]);
                    double fres = Math.Round(res / 1024d);
                    curentram =  Convert.ToInt32(fres);
            }
                return curentram; 
        }
        public static EnumModels.ServerStatus ApplicationRuning(string ApplicationName)
        {
            Process[] pname = Process.GetProcessesByName(ApplicationName);
            if (pname.Length <= 0)
                return EnumModels.ServerStatus.NotRunning;
            else
                return EnumModels.ServerStatus.Running;
        }
        public static EnumModels.ServerStatus ApplicationKill(string ApplicationName)
        {
            
            foreach (var process in Process.GetProcessesByName(ApplicationName))
            {
                try { process.Kill(); return EnumModels.ServerStatus.NotRunning; }
                catch (Exception) { return EnumModels.ServerStatus.Running; }
            }
            return EnumModels.ServerStatus.Running;
        }
        public static int ApplicationRamUsage(string ApplicationName)
        {
            if (ApplicationRuning(ApplicationName) == EnumModels.ServerStatus.Running)
            {
                try
                {
                    PerformanceCounter PC = new PerformanceCounter
                    {
                        CategoryName = "Process",
                        CounterName = "Working Set - Private",
                        InstanceName = ApplicationName
                    };
                    int memsize = Convert.ToInt32(PC.NextValue()) / (int)(1024 * 1024);
                    PC.Close();
                    PC.Dispose();
                    return memsize;
                }
                catch 
                {
                    return 0;
                }
            }
            return 0;
        }
    }
}
