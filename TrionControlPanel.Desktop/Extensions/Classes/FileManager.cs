using System.Collections.Concurrent;
using TrionControlPanel.Desktop.Extensions.Cryptography;
using TrionControlPanel.Desktop.Extensions.Modules.Lists;

namespace TrionControlPanel.Desktop.Extensions.Classes
{
    internal class FileManager
    {
        public static async Task<ConcurrentBag<FileList>> GetFilesAsync(string filePath, IProgress<string>? progress = null)
        {
            if(!Directory.Exists(filePath))Directory.CreateDirectory(filePath);
            var filePaths = Directory.GetFiles(filePath, "*", SearchOption.AllDirectories);
            var fileList = new ConcurrentBag<FileList>();
            var batchSize = 100; // Adjust based on system capabilities

            var semaphore = new SemaphoreSlim(10); // Limit concurrent tasks

            var tasks = new List<Task>();
            int totalFiles = filePaths.Length;
            int processedFiles = 0;

            for (int i = 0; i < totalFiles; i += batchSize)
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
                                Hash = await MD5FileHasah.GetMd5HashFromFileAsync(file), // Async hash calculation
                                Path = fileInfo.DirectoryName?.Replace(@"\", "/") // Normalize path
                            };
                            fileList.Add(fileData);

                            // Report progress
                            Interlocked.Increment(ref processedFiles);
                            progress?.Report($"Processing: {processedFiles}/{totalFiles} files");
                        }
                    }
                    finally
                    {
                        semaphore.Release(); // Release the semaphore
                    }
                });

                tasks.Add(task);
            }

            await Task.WhenAll(tasks);
            progress?.Report("Processing complete.");

            return fileList;
        }
    }
}
