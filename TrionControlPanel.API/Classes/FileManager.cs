using System.Collections.Concurrent;
using System.Diagnostics;
using System.Text.RegularExpressions;
using TrionControlPanel.API.Classes.Cryptography;
using TrionControlPanel.API.Classes.Lists;

namespace TrionControlPanel.API.Classes
{
    public static class FileManager
    {
        public static string GetVersion(string Location)
        {
            try
            {
                if (!string.IsNullOrEmpty(Location))
                {
                    if (Location == "N/A")
                    {
                        return "N/A";
                    }
                    var versionInfo = FileVersionInfo.GetVersionInfo(Location);
                    // Define a regular expression pattern to match dates in yyyy-MM-dd or yyyy/MM/dd format
                    if (versionInfo.FileVersion!.Contains("SPP"))
                    {
                        string pattern = @"\b\d{4}[-/]\d{2}[-/]\d{2}\b";
                        // Create a Regex object with the pattern
                        Regex regex = new(pattern);
                        // Find all matches in the text
                        MatchCollection matches = regex.Matches(versionInfo.ToString());
                        // Print each match
                        foreach (Match match in matches.Cast<Match>())
                        {
                            return match.Value;
                        }
                    }
                    return versionInfo.FileVersion;
                }
                return "N/A";
            }
            catch (Exception)
            {
                return "N/A";
            }
        }
        public static async Task<ConcurrentBag<FileList>> GetFilesAsync(string filePath)
        {
            Console.WriteLine($"Loading all files form {filePath}");
            var filePaths = Directory.GetFiles(filePath, "*", SearchOption.AllDirectories);
            var fileList = new ConcurrentBag<FileList>();
            var batchSize = 1000; // Adjust based on your system's capabilities

            // Semaphore to limit the number of concurrent operations
            var semaphore = new SemaphoreSlim(10); // Limit to 10 concurrent tasks, adjust as needed

            var tasks = new List<Task>();

            for (int i = 0; i < filePaths.Length; i += batchSize)
            {
                var batch = filePaths.Skip(i).Take(batchSize).ToArray();

                var task = Task.Run(async () =>
                {
                    await semaphore.WaitAsync(); // Limit concurrency
                    try
                    {
                        foreach (var file in batch)
                        {
                            var fileInfo = new FileInfo(file);
                            var fileData = new FileList
                            {
                                Name = fileInfo.Name,
                                Size = fileInfo.Length / 1_000.0, // Size in KB
                                Hash = await EncryptManager.GetMd5HashFromFileAsync(file), // Async hash calculation
                                Path = fileInfo.DirectoryName?.Replace(@"\", "/") // Optional: Normalize path
                            };
                            fileList.Add(fileData);
                        }
                    }
                    finally
                    {
                        semaphore.Release(); // Release the semaphore after processing
                    }
                });

                tasks.Add(task);
            }

            // Wait for all tasks to complete
            await Task.WhenAll(tasks);

            Console.WriteLine("Done");
            return fileList;
        }
    }
}
