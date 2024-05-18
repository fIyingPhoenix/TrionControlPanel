using System.Diagnostics;
using System.Dynamic;

namespace TrionWorker
{
    public class Worker
    {
        
        
        public static void Main(string[] args)
        {
            if(args.Contains("--FixLoading"))
            {
                RunPowerShellCommand("lodctr /R");
            }
        }
        static void RunPowerShellCommand(string command)
        {
            // Launch PowerShell process with the command
            ProcessStartInfo psi = new()
            {
                FileName = "powershell.exe",
                Arguments = "-NoProfile -ExecutionPolicy Bypass -Command " + command,
                Verb = "runas" // Run PowerShell as administrator
            };

            Process.Start(psi); 
        }
    }
}
