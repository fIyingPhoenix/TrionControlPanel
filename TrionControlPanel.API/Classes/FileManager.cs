using System.Collections.Concurrent;
using TrionControlPanel.API.Classes.Cryptography;
using TrionControlPanel.API.Classes.Lists;

namespace TrionControlPanel.API.Classes
{
    public static class FileManager
    {
        // Retrieves the version of a file or resource from the specified location.
        // Returns "N/A" if the location is invalid or cannot be read.
        public static string GetVersion(string Location)
        {
            // Early exit if location is invalid or explicitly set to "N/A"
            if (string.IsNullOrEmpty(Location) || Location == "N/A")
            {
                return "N/A";
            }

            try
            {
                // Attempt to read the file content from the location
                File.ReadAllText(Location);
                return "File Read Success"; // Can be more specific (e.g., file version extracted if applicable)
            }
            catch (Exception ex)
            {
                // Instead of silently returning "N/A", log the exception for debugging purposes
                TrionLogger.LogException(ex);
                return "N/A"; // Return "N/A" if reading the file fails
            }
        }

        // Asynchronously retrieves a list of files from the specified directory and calculates their hashes.
        // Uses concurrency control (SemaphoreSlim) to process batches of files concurrently for performance.
        public static async Task<ConcurrentBag<FileList>> GetFilesAsync(string filePath)
        {
            Console.WriteLine($"Loading all files from {filePath}");

            // Get all file paths from the directory (including subdirectories)
            var filePaths = Directory.GetFiles(filePath, "*", SearchOption.AllDirectories);
            var fileList = new ConcurrentBag<FileList>(); // Thread-safe collection to store the file data

            var batchSize = 1000; // Size of each batch for concurrent processing (adjustable)

            // Semaphore to control the concurrency, limiting to 10 concurrent tasks at a time
            var semaphore = new SemaphoreSlim(10); // Limit to 10 concurrent operations, adjust as needed

            var tasks = new List<Task>(); // List to hold all the tasks for batch processing

            // Process files in batches for improved performance
            for (int i = 0; i < filePaths.Length; i += batchSize)
            {
                var batch = filePaths.Skip(i).Take(batchSize).ToArray();

                var task = ProcessBatchAsync(batch, semaphore, fileList); // Async method to process a batch of files
                tasks.Add(task); // Add the task to the list of tasks to wait for
            }

            // Wait for all the tasks to complete
            await Task.WhenAll(tasks);

            Console.WriteLine("Done loading files.");

            return fileList; // Return the list of file data
        }

        // Method to process a batch of files concurrently, limiting the number of concurrent tasks
        private static async Task ProcessBatchAsync(string[] batch, SemaphoreSlim semaphore, ConcurrentBag<FileList> fileList)
        {
            await semaphore.WaitAsync(); // Wait for semaphore before processing the batch

            try
            {
                // Process each file in the current batch
                foreach (var file in batch)
                {
                    var fileInfo = new FileInfo(file); // Get file information

                    var fileData = new FileList
                    {
                        Name = fileInfo.Name, // Store file name
                        Size = fileInfo.Length / 1_048_576.0, // Convert file size to MB (1 MB = 1,048,576 bytes)
                        Hash = await EncryptManager.GetMd5HashFromFileAsync(file), // Calculate MD5 hash asynchronously
                        Path = fileInfo.DirectoryName?.Replace(@"\", "/")!// Normalize the file path (Windows to UNIX style)
                    };

                    // Add the file data to the concurrent collection
                    fileList.Add(fileData);
                }
            }
            finally
            {
                semaphore.Release(); // Release the semaphore after processing the batch
            }
        }
    }
}
