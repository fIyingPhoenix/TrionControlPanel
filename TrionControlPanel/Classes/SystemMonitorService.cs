using System.Diagnostics;
namespace TrionControlPanel.Classes
{
    public class SystemMonitorService
    {
        private readonly PerformanceCounter _cpuCounter;
        private readonly PerformanceCounter _memoryCounter;
        private readonly PerformanceCounter _diskCounter;

        public SystemMonitorService()
        {
            // Initialize performance counters for CPU and memory usage
            _cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
            _memoryCounter = new PerformanceCounter("Memory", "Available MBytes");
            _diskCounter = new PerformanceCounter("PhysicalDisk", "% Disk Time", "_Total");
        }

        // Get the current CPU usage
        public float GetCpuUsage()
        {
            return _cpuCounter.NextValue();
        }

        // Get the available memory in MB
        public float GetAvailableMemory()
        {
            return _memoryCounter.NextValue();
        }

        // Get total managed memory (for .NET GC heap)
        public long GetTotalManagedMemory()
        {
            return GC.GetTotalMemory(false);
        }
        public float GetDiskUsage()
        {
            return _diskCounter.NextValue();
        }
    }
}
