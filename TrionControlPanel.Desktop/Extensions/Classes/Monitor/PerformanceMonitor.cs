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
                // Create a new instance of the ManagementClass
                ManagementClass managementClass = new("Win32_ComputerSystem");
                // Get the total physical memory (RAM)
                ManagementObjectCollection managementObjects = managementClass.GetInstances();
                ulong totalRam = 0;
                foreach (ManagementObject obj in managementObjects.Cast<ManagementObject>())
                {
                    totalRam += (ulong)obj["TotalPhysicalMemory"];
                }
                // Convert bytes to megabytes
                double totalRamInMB = totalRam / (1024 * 1024);
                // Return the total RAM
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
            // Create an instance of PerformanceCounter to monitor the total CPU usage
            PerformanceCounter cpuCounters = new("Processor Information", "% Processor Utility", "_Total");
            // Discard the first value
            dynamic firstValue = cpuCounters.NextValue();
            // Give some time to initialize
            Thread.Sleep(500);
            // Get the second value
            dynamic SecValue = cpuCounters.NextValue();
            if (SecValue > 100) { SecValue = 100; }
            return (int)SecValue;
        }

        // Gets the current PC RAM usage in megabytes.
        public static int GetCurentPcRamUsage()
        {
            // Specify the category and counter for memory usage
            string categoryName = "Memory";
            string counterName = "Available MBytes"; // You can also use "Available MBytes" for available memory

            // Create a PerformanceCounter instance
            PerformanceCounter performanceCounter = new(categoryName, counterName);

            // Get the memory usage in megabytes
            float memoryUsageMB = performanceCounter.NextValue();
            return Convert.ToInt32(memoryUsageMB);
        }

        // Monitors RAM usage percentage and triggers an alert if it exceeds 80%.
        public static void RamProcentage(int TotalRam, int UsedRam)
        {
            var RamProcent = CalculatePercentage(TotalRam, UsedRam);
            if (RamProcent > 80 && RamUsageHight == false)
            {
                // Alert here
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
                    // Infos.Message = "Could not find process with PID " + ProcessID;
                }

                TimeSpan startCpuUsage = process!.TotalProcessorTime;
                DateTime startTime = DateTime.UtcNow;

                Thread.Sleep(500); // Wait a second to get a CPU usage sample

                TimeSpan endCpuUsage = process.TotalProcessorTime;
                DateTime endTime = DateTime.UtcNow;

                double cpuUsedMs = (endCpuUsage - startCpuUsage).TotalMilliseconds;
                double totalMsPassed = (endTime - startTime).TotalMilliseconds;

                double cpuUsageTotal = cpuUsedMs / totalMsPassed * 100 / Environment.ProcessorCount;

                if (cpuUsageTotal + 5 > 100) { cpuUsageTotal = 100; }
                return (int)cpuUsageTotal;
            }
            catch
            {
                return 0;
            }
        }
    }
}
