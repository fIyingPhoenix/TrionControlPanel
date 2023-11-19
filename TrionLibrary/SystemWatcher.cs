using Google.Protobuf.Reflection;
using System;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Threading;

namespace TrionLibrary
{
    public  class SystemWatcher
    {

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
        public static void ApplicationStart(string ApplicationName, bool HideWindw)
        {
            try
            {
                using (Process myProcess = new Process())
                {
                    myProcess.StartInfo.UseShellExecute = false;
                    // You can start any process, HelloWorld is a do-nothing example.
                    myProcess.StartInfo.FileName = ApplicationName;

                    if (HideWindw == false)
                    {
                        myProcess.StartInfo.CreateNoWindow = false;
                        myProcess.Start();
                    }
                    else if (HideWindw == true)
                    {
                        myProcess.StartInfo.CreateNoWindow = true;
                        myProcess.Start();
                    }
                }
            }
            catch (Exception ex)
            {
               Data.Message = ex.Message;
            }
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
