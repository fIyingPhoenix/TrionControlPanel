using System.Diagnostics;
using System.Management;

namespace TrionControlPanel.Desktop.Extensions.Classes.Monitor
{
    public class PerformanceMonitor
    {
        private static bool RamUsageHight { get; set; }

        // Get the total RAM in MB
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
                TrionLogger.Log($"Total RAM: {totalRamInMB} MB", "INFO");
                return Convert.ToInt32(totalRamInMB);
            }
            catch (Exception ex)
            {
                TrionLogger.Log($"Error in GetTotalRamInMB: {ex.Message}", "ERROR");
                return 0;
            }
        }

        // Get the CPU utilization percentage
        public static int GetCpuUtilizationPercentage()
        {
            try
            {
                PerformanceCounter cpuCounters = new("Processor Information", "% Processor Utility", "_Total");
                dynamic firstValue = cpuCounters.NextValue();
                Thread.Sleep(500);
                dynamic secondValue = cpuCounters.NextValue();

                if (secondValue > 100) { secondValue = 100; }

                //TrionLogger.Log($"CPU Utilization: {secondValue}%", "INFO");
                return (int)secondValue;
            }
            catch (Exception ex)
            {
                TrionLogger.Log($"Error in GetCpuUtilizationPercentage: {ex.Message}", "ERROR");
                return 0;
            }
        }

        // Get the current PC's available RAM
        public static int GetCurentPcRamUsage()
        {
            try
            {
                string categoryName = "Memory";
                string counterName = "Available MBytes"; // Available memory

                PerformanceCounter performanceCounter = new(categoryName, counterName);
                float memoryUsageMB = performanceCounter.NextValue();

                TrionLogger.Log($"Available RAM: {memoryUsageMB} MB", "INFO");
                return Convert.ToInt32(memoryUsageMB);
            }
            catch (Exception ex)
            {
                TrionLogger.Log($"Error in GetCurentPcRamUsage: {ex.Message}", "ERROR");
                return 0;
            }
        }

        // Monitor RAM percentage usage
        public static void RamProcentage(int TotalRam, int UsedRam)
        {
            try
            {
                var RamProcent = CalculatePercentage(TotalRam, UsedRam);
                if (RamProcent > 80 && RamUsageHight == false)
                {
                    // Alert about high RAM usage
                    TrionLogger.Log("High RAM usage detected! Over 80%.", "WARNING");
                    RamUsageHight = true;
                }

                if (RamProcent < 80 || RamProcent > 80 && RamUsageHight == true)
                {
                    RamUsageHight = false;
                }
            }
            catch (Exception ex)
            {
                TrionLogger.Log($"Error in RamProcentage: {ex.Message}", "ERROR");
            }
        }

        // Calculate the percentage of used RAM
        private static double CalculatePercentage(double TotalRam, double UsedRam)
        {
            return (UsedRam / TotalRam) * 100;
        }

        // Get RAM usage for a specific application by its Process ID
        public static int ApplicationRamUsage(int ProcessId)
        {
            try
            {
                Process process = Process.GetProcessById(ProcessId);
                PerformanceCounter ramCounter = new("Process", "Working Set", process.ProcessName);
                double ram = ramCounter.NextValue();
                int ramUsageMB = Convert.ToInt32(ram / 1024d / 1024d);

                //TrionLogger.Log($"Application {process.ProcessName} (PID {ProcessId}) RAM usage: {ramUsageMB} MB", "INFO");
                return ramUsageMB;
            }
            catch (Exception ex)
            {
                TrionLogger.Log($"Error in ApplicationRamUsage for PID {ProcessId}: {ex.Message}", "ERROR");
                return 0;
            }
        }

        // Get CPU usage for a specific application by its Process ID
        public static int ApplicationCpuUsage(int ProcessID)
        {
            try
            {
                Process process = Process.GetProcessById(ProcessID);
                TimeSpan startCpuUsage = process.TotalProcessorTime;
                DateTime startTime = DateTime.UtcNow;

                Thread.Sleep(500); // Wait a second to get a CPU usage sample

                TimeSpan endCpuUsage = process.TotalProcessorTime;
                DateTime endTime = DateTime.UtcNow;

                double cpuUsedMs = (endCpuUsage - startCpuUsage).TotalMilliseconds;
                double totalMsPassed = (endTime - startTime).TotalMilliseconds;

                double cpuUsageTotal = (cpuUsedMs / totalMsPassed) * 100 / Environment.ProcessorCount;

                if (cpuUsageTotal + 5 > 100) { cpuUsageTotal = 100; }

                //TrionLogger.Log($"Application {process.ProcessName} (PID {ProcessID}) CPU usage: {cpuUsageTotal}%", "INFO");
                return (int)cpuUsageTotal;
            }
            catch (Exception ex)
            {
                TrionLogger.Log($"Error in ApplicationCpuUsage for PID {ProcessID}: {ex.Message}", "ERROR");
                return 0;
            }
        }
    }
}
