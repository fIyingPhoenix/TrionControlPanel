using System.Diagnostics;
using System.Management;
namespace TrionControlPanel.Desktop.Extensions.Classes.Monitor
{
    // Monitors system performance metrics such as CPU and RAM usage.
    public class PerformanceMonitor
    {
        private static bool RamUsageHight { get; set; } // Indicates if RAM usage is high.

        // Gets the total RAM in megabytes.
        public static int GetTotalRamInMB()
        {
            try
            {
                ManagementClass managementClass = new("Win32_ComputerSystem");
                ManagementObjectCollection managementObjects = managementClass.GetInstances();
                ulong totalRam = 0;
                foreach (ManagementObject obj in managementObjects.Cast<ManagementObject>())
                {
                    totalRam += (ulong)obj["TotalPhysicalMemory"];
                }
                double totalRamInMB = totalRam / (1024 * 1024);
                return Convert.ToInt32(totalRamInMB);
            }
            catch
            {
                return 0;
            }
        }

        // Gets the CPU utilization percentage.
        public static int GetCpuUtilizationPercentage()
        {
            PerformanceCounter cpuCounters = new("Processor Information", "% Processor Utility", "_Total");
            dynamic firstValue = cpuCounters.NextValue();
            Thread.Sleep(500);
            dynamic SecValue = cpuCounters.NextValue();
            if (SecValue > 100) { SecValue = 100; }
            return (int)SecValue;
        }

        // Gets the current PC RAM usage in megabytes.
        public static int GetCurentPcRamUsage()
        {
            try
            {
                string categoryName = "Memory";
                string counterName = "Available MBytes";
                PerformanceCounter performanceCounter = new(categoryName, counterName);
                float memoryUsageMB = performanceCounter.NextValue();
                return Convert.ToInt32(memoryUsageMB);
            }
            catch
            {
                return 0;
            }
        }

        // Monitors RAM usage percentage and triggers an alert if it exceeds 80%.
        public static void RamProcentage(int TotalRam, int UsedRam)
        {
            var RamProcent = CalculatePercentage(TotalRam, UsedRam);
            if (RamProcent > 80 && RamUsageHight == false)
            {
                RamUsageHight = true;
            }
            if (RamProcent < 80)
            {
                RamUsageHight = false;
            }
        }

        // Calculates the percentage of used RAM.
        private static double CalculatePercentage(double TotalRam, double UsedRam)
        {
            return TotalRam / UsedRam * 100;
        }

        // Gets the RAM usage of a specific application in megabytes.
        public static int ApplicationRamUsage(int ProcessId)
        {
            try
            {
                Process process = Process.GetProcessById(ProcessId);
                PerformanceCounter ramCounter = new("Process", "Working Set", process.ProcessName);
                while (true)
                {
                    double ram = ramCounter.NextValue();
                    return Convert.ToInt32(ram / 1024d / 1024d);
                }
            }
            catch
            {
                return 0;
            }
        }

        // Gets the CPU usage of a specific application as a percentage.
        public static int ApplicationCpuUsage(int ProcessID)
        {
            try
            {
                Process process = Process.GetProcessById(ProcessID);
                if (process == null)
                {
                    return 0;
                }

                using PerformanceCounter cpuCounter = new("Process", "% Processor Time", process.ProcessName, true);
                cpuCounter.NextValue(); // Discard the first value
                Thread.Sleep(500); // Wait a bit to get a more accurate reading
                return (int)(cpuCounter.NextValue() / Environment.ProcessorCount);
            }
            catch
            {
                return 0;
            }
        }
    }
}
