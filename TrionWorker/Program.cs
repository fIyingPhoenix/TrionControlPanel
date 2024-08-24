using System.Diagnostics;
using TrionLibrary.Crypto;

namespace TrionWorker
{
    public class Worker
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                DisplayUsageInstructions();
                Console.ReadLine();
                return;
            }
            string commands = args[0];
            var arguments = ParseArguments(args.Skip(1).ToArray());

            switch (commands)
            {
                case "FixLoading":
                    RunPowerShellCommand("lodctr /R");
                    Console.ReadLine();
                    break;
                case "GetHash":
                    if (!arguments.ContainsKey("directory"))
                    {
                        DisplayOpenUsage(commands);
                        Console.ReadLine();
                    }
                    FileHash.ExportFileHashesToXML(arguments["directory"], AppDomain.CurrentDomain.BaseDirectory);
                    Console.ReadLine();
                    break;
                case "CompareHash":
                    FileHash.CompareAndExportChangesOffline(arguments["directory"], arguments["old"], arguments["new"]);
                    break;
                default:
                    DisplayUsageInstructions();
                    Console.ReadLine();
                    return;
            }
        }
        static void DisplayOpenUsage(string Command)
        {
            switch(Command)
            {
                case "GetHash":
                    Console.WriteLine("Error: 'GetHash' command requires '--Directory' arguments.");
                    Console.WriteLine("Usage: TrionWorker GetHash --Directory <directory>");
                    break;
                default:
                    DisplayUsageInstructions();
                    return;
            }       
        }
        static void DisplayUsageInstructions()
        {
            Console.WriteLine("Usage: TrionWorker [command] [arguments]");
            Console.WriteLine("Available commands:");
            Console.WriteLine("FixLoading  : Restores counter registry settings and explanatory text from current registry settings and cached performance files related to the registry.");
            Console.WriteLine("GetHash --Directory <directory>  : The program will create an XML file named file_hashes.xml in the specified directory, containing the SHA-256 hash, filename, and directory for each file.");
            // Include other available commands...
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
            Console.WriteLine("Restores counter registry settings and explanatory text from current registry settings and cached performance files related to the registry.");
        }
        static Dictionary<string, string> ParseArguments(string[] args)
        {
            var arguments = new Dictionary<string, string>();
            for (int i = 0; i < args.Length; i += 2)
            {
                if (i + 1 < args.Length)
                {
                    string key = args[i].ToLower().TrimStart('-');
                    string value = args[i + 1];
                    arguments[key] = value;
                }
            }
            return arguments;
        }
    }
}
