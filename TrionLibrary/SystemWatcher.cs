using System;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Threading;

namespace TrionLibrary
{
    public class SystemWatcher
    {
        //fix "lodctr /R"
        //static Process[] ProcessID;

        private static string OSRuinning()
        {
            if (Environment.OSVersion.Platform == PlatformID.Unix) return "Unix";
            else if (Environment.OSVersion.Platform == PlatformID.Win32NT) return "Widnows";
            else return "Unknown";
        }
        public static int TotalRam()
        {
            double totalRamInMB = 0;
            try
            {
                if (OSRuinning() == "Widnows")
                {
                    // Create a new instance of the ManagementClass
                    ManagementClass managementClass = new ManagementClass("Win32_ComputerSystem");

                    // Get the total physical memory (RAM)
                    ManagementObjectCollection managementObjects = managementClass.GetInstances();
                    ulong totalRam = 0;
                    foreach (ManagementObject obj in managementObjects.Cast<ManagementObject>())
                    {
                        totalRam += (ulong)obj["TotalPhysicalMemory"];
                    }
                    // Convert bytes to gigabytes
                    totalRamInMB = totalRam / (1024 * 1024);

                }
                else if (OSRuinning() == "Unix")
                {
                    // Specify the shell command to get total RAM
                    string shellCommand = "cat /proc/meminfo | grep MemTotal";

                    // Create a ProcessStartInfo instance to specify the shell command
                    ProcessStartInfo processStartInfo = new ProcessStartInfo()
                    {
                        FileName = "/bin/bash",
                        Arguments = $"-c \"{shellCommand}\"",
                        RedirectStandardOutput = true,
                        UseShellExecute = false,
                        CreateNoWindow = true
                    };

                    // Create and start the process to execute the shell command
                    Process process = new Process()
                    {
                        StartInfo = processStartInfo
                    };
                    process.Start();

                    // Read the output of the shell command
                    string output = process.StandardOutput.ReadToEnd();

                    // Close the process
                    process.WaitForExit();

                    // Parse the output to extract the total RAM
                    string[] outputParts = output.Split(new char[] { ' ', ':' }, StringSplitOptions.RemoveEmptyEntries);
                    if (outputParts.Length >= 2 && outputParts[0] == "MemTotal")
                    {
                        if (long.TryParse(outputParts[1], out long totalRamKB))
                        {
                            // Convert total RAM from kilobytes to megabytes
                            double totalRamMB = totalRamKB / 1024.0;
                            return Convert.ToInt32(totalRamInMB);
                        }
                        else
                        {
                            return 0;
                        }
                    }
                    else
                    {
                        return 0;
                    }
                }

                // Return the total RAM
                return Convert.ToInt32(totalRamInMB);
            }
            catch
            {
                return 0;
            }
        }
        public static int MachineCpuUtilization()
        {
            // Create a new instance of the PerformanceCounter
            var cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total", Environment.MachineName);
            cpuCounter.NextValue();
            Thread.Sleep(1000);
            return (int)cpuCounter.NextValue();
        }
        public static int CurentPcRamUsage()
        {
            if (OSRuinning() == "Widnows")
            {
                // Specify the category and counter for memory usage
                string categoryName = "Memory";
                string counterName = "Available MBytes"; // You can also use "Available MBytes" for available memory

                // Create a PerformanceCounter instance
                PerformanceCounter performanceCounter = new PerformanceCounter(categoryName, counterName);

                // Get the memory usage in megabytes
                float memoryUsageMB = performanceCounter.NextValue();
                return Convert.ToInt32(memoryUsageMB);
            }
            else if (OSRuinning() == "Unix")
            {
                return 0;
            }
            return 0;
        }
        public static bool ApplicationRuning(string ApplicationName)
        {
            Process[] ProcessID = Process.GetProcessesByName(ApplicationName);
            if (ProcessID.Length <= 0)
                return false;
            else
                return true;
        }
        public static void ApplicationStart(string Application, string Name, bool HideWindw, string Arguments)
        {
            Data.Message = $@"Starting {Name}!";
            Thread.Sleep(100);
            try
            {
                using (Process myProcess = new Process())
                {
                    myProcess.StartInfo.UseShellExecute = false;
                    // You can start any process, HelloWorld is a do-nothing example.
                    myProcess.StartInfo.FileName = Application;
                    if (Arguments != null)
                    {
                        myProcess.StartInfo.Arguments = Arguments;

                    }
                    if (HideWindw == false)
                    {
                        myProcess.StartInfo.CreateNoWindow = false;
                        myProcess.Start();

                    }
                    if (HideWindw == true)
                    {
                        myProcess.StartInfo.CreateNoWindow = true;
                        myProcess.Start();
                    }
                    // complete the task
                    Data.Message = $@"Successfully rune {Name}!";
                }
            }
            catch (Exception ex)
            {
                Data.Message = ex.Message;
            }
        }
        static int GetProcessIdByName(string processName)
        {
            try
            {
                Process[] processes = Process.GetProcessesByName(processName);
                if (processes.Length > 0)
                {
                    return processes[0].Id;
                }
                else
                {
                    return -1; // Return -1 if no process with the specified name is found
                }
            }
            catch (Exception)
            {
                return -1; // Return -1 if an error occurs
            }
        }
        public static void ApplicationKill(string ApplicationName)
        {
            foreach (var process in Process.GetProcessesByName(ApplicationName))
            {
                try { process.Kill(); }
                catch (Exception) {  }
            }   
        }
        public static int ApplicationRamUsage(string ApplicationName)
        {
            if (ApplicationRuning(ApplicationName) == true)
            {   
                try
                {
                    Process process = Process.GetProcessById(GetProcessIdByName(ApplicationName));
                    PerformanceCounter ramCounter = new PerformanceCounter("Process", "Working Set", process.ProcessName);
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
            return 0;
        }
        public static int ApplicationCpuUsage(string ApplicationName)
        {
            try
            {
                if (ApplicationRuning(ApplicationName) == true)
                {
                    Process process = Process.GetProcessById(GetProcessIdByName(ApplicationName));

                    // Get the initial CPU time for all cores
                    TimeSpan[] initialCpuTimes = new TimeSpan[Environment.ProcessorCount];
                    for (int i = 0; i < Environment.ProcessorCount; i++)
                    {
                        initialCpuTimes[i] = process.TotalProcessorTime;
                    }

                    // Wait for some time
                    Thread.Sleep(1000); // Wait for 3 seconds (adjust as needed)

                    // Refresh the process object
                    process.Refresh();

                    // Get the CPU time after the delay for all cores
                    TimeSpan[] cpuTimesAfterDelay = new TimeSpan[Environment.ProcessorCount];
                    for (int i = 0; i < Environment.ProcessorCount; i++)
                    {
                        cpuTimesAfterDelay[i] = process.TotalProcessorTime;
                    }
                    // Calculate the CPU usage percentage for all cores
                    double[] cpuUsagePercentages = new double[Environment.ProcessorCount];
                    for (int i = 0; i < Environment.ProcessorCount; i++)
                    {
                        cpuUsagePercentages[i] = ((cpuTimesAfterDelay[i] - initialCpuTimes[i]).TotalMilliseconds /
                                                   (DateTime.Now - process.StartTime).TotalMilliseconds) * 100;
                    }
                    // Calculate the average CPU usage across all cores
                    double totalCpuUsagePercentage = 0;
                    foreach (double cpuUsagePercentage in cpuUsagePercentages)
                    {
                        totalCpuUsagePercentage += cpuUsagePercentage;
                    }
                    double averageCpuUsagePercentage = totalCpuUsagePercentage / Environment.ProcessorCount;

                    // Limit CPU usage to 100%
                    averageCpuUsagePercentage = Math.Min(averageCpuUsagePercentage, 100);

                    // Convert the CPU usage percentage to an integer
                    int cpuUsagePercentageInt = (int)Math.Round(averageCpuUsagePercentage);
                     return cpuUsagePercentageInt;

                }
                else { return 0; }
            }
            catch
            {
                return 0;
            }
        }
    }
}
