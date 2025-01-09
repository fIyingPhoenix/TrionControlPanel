using System.Diagnostics;
using System.Management;
namespace TrionControlPanel.Desktop.Extensions.Classes.Monitor
{
    public class PerformanceMonitor
    {
        private static bool RamUsageHight { get; set; }
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
                // Convert bytes to gigabytes
                double totalRamInMB = totalRam / (1024 * 1024);
                // Return the total RAM
                return Convert.ToInt32(totalRamInMB);
            }
            catch
            {
                return 0;
            }
        }
        public static int GetCpuUtilizationPercentage()
        {
            // Initialize PerformanceCounters for each CPU core
            // int coreCount = Environment.ProcessorCount;
            // Create an instance of PerformanceCounter to monitor the total CPU usage
            PerformanceCounter cpuCounters = new("Processor Information", "% Processor Utility", "_Total");
            // Discard the first value
            dynamic firstValue = cpuCounters.NextValue();
            // Give some time to initialize
            Thread.Sleep(500);
            //report
            dynamic SecValue = cpuCounters.NextValue();
            if (SecValue > 100) { SecValue = 100; }
            return (int)SecValue;

        }
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
        public static void RamProcentage(int TotalRam, int UsedRam)
        {
            var RamProcent = CalculatePercentage(TotalRam, UsedRam);
            if (RamProcent > 80 && RamUsageHight == false)
            {
                // alert here
                RamUsageHight = true;
            }
            if (RamProcent < 80)
            {
                RamUsageHight = false;
            }
        }
        private static double CalculatePercentage(double TotalRam, double UsedRam)
        {
            return TotalRam / UsedRam * 100;
        }
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
        public static int ApplicationCpuUsage(int ProcessID)
        {
            try
            {
                Process process = Process.GetProcessById(ProcessID);
                if (process == null)
                {
                    // Infos.Message = "Could not find process with PID " + ProcessID;
                }

                TimeSpan startCpuUsage = process.TotalProcessorTime;
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
