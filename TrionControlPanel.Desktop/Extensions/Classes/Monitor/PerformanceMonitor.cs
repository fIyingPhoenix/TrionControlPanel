// =============================================================================
// PerformanceMonitor.cs
// Purpose: Monitors system performance metrics such as CPU and RAM usage
// Dependencies: System.Diagnostics, System.Management
// Step 9 of IMPROVEMENTS.md - Region-based file organization
// =============================================================================

using System.Diagnostics;
using System.Management;

namespace TrionControlPanel.Desktop.Extensions.Classes.Monitor
{
    /// <summary>
    /// Monitors system performance metrics such as CPU and RAM usage.
    /// Provides methods to track both system-wide and per-process resource consumption.
    /// </summary>
    public class PerformanceMonitor
    {
        #region Constants
        // ─────────────────────────────────────────────────────────────────────

        /// <summary>
        /// RAM usage percentage threshold for triggering high usage alerts.
        /// </summary>
        private const int HIGH_RAM_THRESHOLD_PERCENT = 80;

        /// <summary>
        /// Delay in milliseconds between performance counter readings for accurate values.
        /// </summary>
        private const int COUNTER_SAMPLE_DELAY_MS = 500;

        /// <summary>
        /// Number of bytes in one megabyte (1024 * 1024).
        /// </summary>
        private const double BytesPerMB = 1024.0 * 1024.0;

        /// <summary>
        /// Maximum CPU percentage value (capped to prevent values over 100%).
        /// </summary>
        private const int MaxCpuPercent = 100;

        #endregion

        #region Fields
        // ─────────────────────────────────────────────────────────────────────

        /// <summary>
        /// Indicates if RAM usage is currently above the high threshold.
        /// </summary>
        private static bool RamUsageHigh { get; set; }

        #endregion

        #region Public Methods - System Metrics
        // ─────────────────────────────────────────────────────────────────────

        /// <summary>
        /// Gets the total physical RAM installed in the system.
        /// </summary>
        /// <returns>Total RAM in megabytes, or 0 if unable to determine.</returns>
        public static int GetTotalRamInMB()
        {
            try
            {
                using ManagementClass managementClass = new("Win32_ComputerSystem");
                using ManagementObjectCollection managementObjects = managementClass.GetInstances();
                ulong totalRam = 0;
                foreach (ManagementObject obj in managementObjects.Cast<ManagementObject>())
                {
                    totalRam += (ulong)obj["TotalPhysicalMemory"];
                }
                double totalRamInMB = totalRam / BytesPerMB;
                return Convert.ToInt32(totalRamInMB);
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// Gets the current CPU utilization percentage across all processors.
        /// </summary>
        /// <returns>CPU usage as a percentage (0-100).</returns>
        /// <remarks>
        /// This method waits ~500ms asynchronously to get an accurate reading.
        /// Values above 100% are clamped to 100%.
        /// </remarks>
        public static async Task<int> GetCpuUtilizationPercentageAsync()
        {
            using PerformanceCounter cpuCounters = new("Processor Information", "% Processor Utility", "_Total");
            dynamic firstValue = cpuCounters.NextValue();
            await Task.Delay(COUNTER_SAMPLE_DELAY_MS);
            dynamic SecValue = cpuCounters.NextValue();
            if (SecValue > MaxCpuPercent) { SecValue = MaxCpuPercent; }
            return (int)SecValue;
        }

        /// <summary>
        /// Gets the current available RAM in the system.
        /// </summary>
        /// <returns>Available RAM in megabytes, or 0 if unable to determine.</returns>
        public static int GetCurentPcRamUsage()
        {
            try
            {
                using PerformanceCounter performanceCounter = new("Memory", "Available MBytes");
                float memoryUsageMB = performanceCounter.NextValue();
                return Convert.ToInt32(memoryUsageMB);
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// Monitors RAM usage percentage and updates the high usage flag.
        /// </summary>
        /// <param name="TotalRam">Total system RAM in MB.</param>
        /// <param name="UsedRam">Currently used RAM in MB.</param>
        /// <remarks>
        /// Sets RamUsageHigh flag when usage exceeds 80%.
        /// This can be used to trigger alerts or warnings to the user.
        /// </remarks>
        public static void RamProcentage(int TotalRam, int UsedRam)
        {
            var RamProcent = CalculatePercentage(TotalRam, UsedRam);
            if (RamProcent > HIGH_RAM_THRESHOLD_PERCENT && RamUsageHigh == false)
            {
                RamUsageHigh = true;
            }
            if (RamProcent < HIGH_RAM_THRESHOLD_PERCENT)
            {
                RamUsageHigh = false;
            }
        }

        #endregion

        #region Public Methods - Application Metrics
        // ─────────────────────────────────────────────────────────────────────

        /// <summary>
        /// Gets the RAM usage of a specific process.
        /// </summary>
        /// <param name="ProcessId">The process ID to monitor.</param>
        /// <returns>RAM usage in megabytes, or 0 if process not found.</returns>
        public static int ApplicationRamUsage(int ProcessId)
        {
            try
            {
                Process process = Process.GetProcessById(ProcessId);
                using PerformanceCounter ramCounter = new("Process", "Working Set", process.ProcessName);
                double ram = ramCounter.NextValue();
                return Convert.ToInt32(ram / BytesPerMB);
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// Gets the CPU usage of a specific process.
        /// </summary>
        /// <param name="ProcessID">The process ID to monitor.</param>
        /// <returns>CPU usage as a percentage (normalized by processor count), or 0 if process not found.</returns>
        /// <remarks>
        /// This method waits ~500ms asynchronously to get an accurate reading.
        /// The percentage is divided by processor count to get a normalized value.
        /// </remarks>
        public static async Task<int> ApplicationCpuUsageAsync(int ProcessID)
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
                await Task.Delay(COUNTER_SAMPLE_DELAY_MS);
                return (int)(cpuCounter.NextValue() / Environment.ProcessorCount);
            }
            catch
            {
                return 0;
            }
        }

        #endregion

        #region Private Methods
        // ─────────────────────────────────────────────────────────────────────

        /// <summary>
        /// Calculates the percentage of used RAM relative to total RAM.
        /// </summary>
        /// <param name="TotalRam">Total system RAM.</param>
        /// <param name="UsedRam">Currently used RAM.</param>
        /// <returns>Usage percentage as a double.</returns>
        private static double CalculatePercentage(double TotalRam, double UsedRam)
        {
            return TotalRam / UsedRam * 100;
        }

        #endregion
    }
}
