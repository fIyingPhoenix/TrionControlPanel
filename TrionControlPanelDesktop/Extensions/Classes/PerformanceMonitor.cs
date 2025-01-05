using System.Diagnostics;
using System.Management;
namespace TrionControlPanelDesktop.Extensions.Classes
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
            return (TotalRam / UsedRam) * 100;
        }
    }
}
