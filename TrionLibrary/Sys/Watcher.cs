using System;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;


namespace TrionLibrary.Sys
{
    public class Watcher
    {
        //fix "lodctr /R"
        //static Process[] ProcessID;
        private const int SampleCount = 5;

        private static readonly char[] separator = [' ', ':'];

        // PInvoke declarations
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool AttachConsole(uint dwProcessId);

        [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
        private static extern bool FreeConsole();

        [DllImport("kernel32.dll")]
        private static extern bool SetConsoleCtrlHandler(ConsoleCtrlDelegate handler, bool add);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool GenerateConsoleCtrlEvent(ConsoleCtrlEvent sigevent, uint dwProcessGroupId);

        private delegate bool ConsoleCtrlDelegate(CtrlTypes CtrlType);
        private enum ConsoleCtrlEvent
        {
            CTRL_C = 0,
            CTRL_BREAK = 1,
            CTRL_CLOSE = 2,
            CTRL_LOGOFF = 5,
            CTRL_SHUTDOWN = 6
        }
        private enum CtrlTypes
        {
            CTRL_C_EVENT = 0,
            CTRL_BREAK_EVENT,
            CTRL_CLOSE_EVENT,
            CTRL_LOGOFF_EVENT = 5,
            CTRL_SHUTDOWN_EVENT
        }

        public static string OSRuinning()
        {
            if (Environment.OSVersion.Platform == PlatformID.Unix) return "Unix";
            else if (Environment.OSVersion.Platform == PlatformID.Win32NT) return "Windows";
            else return "Unknown";
        }
        public static int MachineTotalRam()
        {
            double totalRamInMB = 0;
            try
            {
                if (OSRuinning() == "Windows")
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
                    totalRamInMB = totalRam / (1024 * 1024);

                }
                else if (OSRuinning() == "Unix")
                {
                    // Specify the shell command to get total RAM
                    string shellCommand = "cat /proc/meminfo | grep MemTotal";

                    // Create a ProcessStartInfo instance to specify the shell command
                    ProcessStartInfo processStartInfo = new()
                    {
                        FileName = "/bin/bash",
                        Arguments = $"-c \"{shellCommand}\"",
                        RedirectStandardOutput = true,
                        UseShellExecute = false,
                        CreateNoWindow = true
                    };

                    // Create and start the process to execute the shell command
                    Process process = new()
                    {
                        StartInfo = processStartInfo
                    };
                    process.Start();

                    // Read the output of the shell command
                    string output = process.StandardOutput.ReadToEnd();

                    // Close the process
                    process.WaitForExit();

                    // Parse the output to extract the total RAM
                    string[] outputParts = output.Split(separator, StringSplitOptions.RemoveEmptyEntries);
                    if (outputParts.Length >= 2 && outputParts[0] == "MemTotal")
                    {
                        if (long.TryParse(outputParts[1], out long totalRamKB))
                        {
                            // Convert total RAM from kilobytes to megabytes
                            totalRamInMB = totalRamKB / 1024.0;
                        }
                        else
                        {
                            totalRamInMB = 0;
                        }
                    }
                    else
                    {
                        totalRamInMB = 0;
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
            if (OSRuinning() == "Windows")
            {
                try
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
                catch
                {
                    return 0;
                }
            }
            else if (OSRuinning() == "Unix")
            {
                var process = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = "/bin/bash",
                        Arguments = "-c \"top -bn1 | grep 'Cpu(s)'\"",
                        RedirectStandardOutput = true,
                        UseShellExecute = false,
                        CreateNoWindow = true,
                    }
                };

                process.Start();
                string result = process.StandardOutput.ReadToEnd();
                process.WaitForExit();

                // Parse the result to get CPU usage (e.g., extract idle and calculate usage)
                string[] cpuStats = result.Split(',', StringSplitOptions.RemoveEmptyEntries);
                string idleString = cpuStats.First(stat => stat.Contains("id")).Trim();
                float idle = float.Parse(idleString.Split(' ')[0]);
                return 100 - (int)idle;
            }
            return 0;   
        }
        public static int CurentPcRamUsage()
        {
            if (OSRuinning() == "Windows")
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
            else if (OSRuinning() == "Unix")
            {
                return 0;
            }
            return 0;
        }
        public static bool IsApplicationRunningName(string ProcessName)
        {
            Process[] ProcessID = Process.GetProcessesByName(ProcessName);
            if (ProcessID.Length <= 0)
                return false;
            else
                return true;
        }
        public static bool IsApplicationRuning(int ProcessId)
        {
            try
            {
                Process process = Process.GetProcessById(ProcessId);
                return !process.HasExited;
            }
            catch (ArgumentException)
            {
                return false; // Process with the specified ID does not exist
            }
        }
        public static async Task<int> ApplicationStart(string Application, string WorkingDirectory, string Name, bool HideWindw, string Arguments)
        {
            int id = 0;
            await Task.Run(() =>
             {
                 try
                 {
                     using Process myProcess = new();
                     myProcess.StartInfo.UseShellExecute = false;
                     // You can start any process, HelloWorld is a do-nothing example.
                     myProcess.StartInfo.FileName = Application;
                     myProcess.StartInfo.WorkingDirectory = WorkingDirectory;
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

                     Infos.Message = $@"Started: {Name}!";
                     id = myProcess.Id;
                 }
                 catch (Exception ex)
                 {
                     Infos.Message = ex.Message;
                     id = 0;
                 }
             });
            return id;
        }
        public static int GetProcessIdByName(string processName)
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
        public static void ApplicationKill(int ApplicationID)
        {
            var process = Process.GetProcessById(ApplicationID);
            process.Kill(true);// Ensures the process is killed immediately
        }
        public static async Task ApplicationStop(int ApplicationID)
        {
            try
            {
                Infos.Message = "Stopping Process:" + ApplicationID;
                var process = Process.GetProcessById(ApplicationID);
                await SendCtrlC(process);
                if (!process.WaitForExit(15000)) // wait for 15 seconds, save world!
                {
                    // If the process did not exit, forcefully terminate it
                    Infos.Message = "The process did not exit for more then 15 seconds. Kill it!";
                    process.Kill(true);
                }
            }
            catch (Exception ex)
            {
                 Infos.Message = $"Error: {ex.Message}"; 
            }
        }
        private static async Task SendCtrlC(Process process)
        {
            // Attach to the process's console
            if (AttachConsole((uint)process.Id))
            {
                // Set up a control-C event handler to ignore it in this process
                SetConsoleCtrlHandler(null, true);

                // Send Ctrl+C to the console process group
                GenerateConsoleCtrlEvent(ConsoleCtrlEvent.CTRL_C, 0);

                // Allow some time for the event to be processed
                await Task.Delay(1000);

                // Detach from the console
                FreeConsole();

                // Re-enable the control-C handling in this process
                SetConsoleCtrlHandler(null, false);
            }
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
                    Infos.Message = "Could not find process with PID " + ProcessID;
                }

                TimeSpan startCpuUsage = process.TotalProcessorTime;
                DateTime startTime = DateTime.UtcNow;

                Thread.Sleep(500); // Wait a second to get a CPU usage sample

                TimeSpan endCpuUsage = process.TotalProcessorTime;
                DateTime endTime = DateTime.UtcNow;

                double cpuUsedMs = (endCpuUsage - startCpuUsage).TotalMilliseconds;
                double totalMsPassed = (endTime - startTime).TotalMilliseconds;

                double cpuUsageTotal = (cpuUsedMs / totalMsPassed) * 100 / Environment.ProcessorCount;

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
