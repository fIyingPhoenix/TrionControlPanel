using System;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Runtime.InteropServices;
using System.Threading;

namespace TrionLibrary
{
    public class SystemWatcher
    {
        //fix "lodctr /R"
        //static Process[] ProcessID;
        public static Process pWorldServer;
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
        private static string OSRuinning()
        {
            if (Environment.OSVersion.Platform == PlatformID.Unix) return "Unix";
            else if (Environment.OSVersion.Platform == PlatformID.Win32NT) return "Widnows";
            else return "Unknown";
        }
        public static int MachineTotalRam()
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
        static float GetCpuUsage(PerformanceCounter cpuCounter)
        {
            // Average out multiple samples
            float total = 0f;
            int samples = 5;
            for (int i = 0; i < samples; i++)
            {
                total += cpuCounter.NextValue();
                Thread.Sleep(200); // Adjust sleep time to balance responsiveness and accuracy
            }
            return total / samples;
        }
        public static int MachineCpuUtilization()
        {
            // Create an instance of PerformanceCounter to monitor the total CPU usage
            PerformanceCounter cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");

            // Give some time to initialize
            cpuCounter.NextValue();
            Thread.Sleep(1000);

            // Continuous monitoring
            while (true)
            {
                float cpuUsage = GetCpuUsage(cpuCounter);
                return (int)cpuUsage;
            }
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
        public static bool IsApplicationRunning(string ProcessName)
        {
            Process[] ProcessID = Process.GetProcessesByName(ProcessName);
            if (ProcessID.Length <= 0)
                return false;
            else
                return true;
        }
        public static bool ApplicationRuning(int ProcessId)
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
        public static int ApplicationStart(string Application, string WorkingDirectory,  string Name, bool HideWindw, string Arguments)
        {
            Data.Message = $@"Starting {Name}!";
            Thread.Sleep(100);
            try
            {
                using Process myProcess = new Process();
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

                Data.Message = $@"Successfully rune {Name}!";
                return myProcess.Id;
            }
            catch (Exception ex)
            {
                Data.Message = ex.Message;
                return 0;
            }
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
        public static void ApplicationStop(int ApplicationID)
        {
            Thread ApplicationStopThread = new Thread(() =>
            {
                var process = Process.GetProcessById(ApplicationID);

                SendCtrlC(process);
                if (!process.WaitForExit(15000)) // wait for 15 seconds, save world!
                {
                    // If the process did not exit, forcefully terminate it
                    Data.Message = "The process did not exit for more then 15 Secoinds. Kill it!";
                    process.Kill();
                }
            });
            ApplicationStopThread.Start();
        }
        private static void SendCtrlC(Process process)
        {
            // Attach to the process's console
            if (AttachConsole((uint)process.Id))
            {
                // Set up a control-C event handler to ignore it in this process
                SetConsoleCtrlHandler(null, true);

                // Send Ctrl+C to the console process group
                GenerateConsoleCtrlEvent(ConsoleCtrlEvent.CTRL_C, 0);

                // Allow some time for the event to be processed
                Thread.Sleep(1000);

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
        public static int ApplicationCpuUsage(int ProcessID)
        {
            try
            {
                Process process = Process.GetProcessById(ProcessID);
                // Get the initial CPU time for all cores
                TimeSpan[] initialCpuTimes = new TimeSpan[Environment.ProcessorCount];
                for (int i = 0; i < Environment.ProcessorCount; i++)
                {
                    initialCpuTimes[i] = process.TotalProcessorTime;
                }
                // Wait for some time
                Thread.Sleep(3000); // Wait for 3 seconds (adjust as needed)
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
                return (int)Math.Round(averageCpuUsagePercentage);
            }
            catch
            {
                return 0;
            }
        }
    }
}
