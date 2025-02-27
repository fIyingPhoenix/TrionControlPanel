using System.Collections.Concurrent;
using TrionControlPanel.Desktop.Extensions.Cryptography;
using TrionControlPanel.Desktop.Extensions.Modules.Lists;

namespace TrionControlPanel.Desktop.Extensions.Classes
{
    internal class FileManager
    {
        public async Task DownloadFileAsync(FileList file)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string url = $"http://yourapiurl.com/DownloadFile?filePath={Uri.EscapeDataString(file.Path)}";

                    HttpResponseMessage response = await client.GetAsync(url, HttpCompletionOption.ResponseHeadersRead);

                    if (response.IsSuccessStatusCode)
                    {
                        long fileSize = response.Content.Headers.ContentLength ?? 0;
                        string fileName = file.Name;

                        // Display file name and size
                        MessageBox.Show($"File: {fileName}, Size: {fileSize / 1024} KB");

                        using (var stream = await response.Content.ReadAsStreamAsync())
                        {
                            using (var fileStream = new FileStream(file.Path, FileMode.Create, FileAccess.Write, FileShare.None))
                            {
                                byte[] buffer = new byte[8192];
                                int bytesRead;
                                long totalBytesRead = 0;
                                DateTime startTime = DateTime.Now;

                                progressBar.Maximum = (int)fileSize;
                                progressBar.Value = 0;

                                while ((bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length)) > 0)
                                {
                                    await fileStream.WriteAsync(buffer, 0, bytesRead);
                                    totalBytesRead += bytesRead;

                                    double progress = (double)totalBytesRead / fileSize * 100;
                                    double elapsedTime = (DateTime.Now - startTime).TotalSeconds;

                                    double speed = totalBytesRead / 1024 / elapsedTime;

                                    // Update ProgressBar
                                    progressBar.Value = (int)totalBytesRead;

                                    // Optionally, you can update a label to show the progress and speed
                                    Console.WriteLine($"Progress: {progress:0.00}% | " +
                                                      $"Downloaded: {totalBytesRead / 1024} KB | " +
                                                      $"Speed: {speed:0.00} KB/s");
                                }
                            }
                        }

                        MessageBox.Show("File downloaded successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show($"Error: {response.ReasonPhrase}", "Download Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        // Asynchronous method to delete a file for cheap 
        public static async Task DeleteFiles(List<FileList> files)
        {
            foreach (var file in files)
            {
                // Run the delete operation in a separate task to avoid blocking the calling thread
                await Task.Run( () => File.Delete(file.Path));
            }
        }
        public static async Task<(List<FileList> MissingFiles, List<FileList> FilesToDelete)> CompareFiles(List<FileList> ServerFiles, List<FileList> LocalFiles)
        {
            // Create lists to track missing files (from Local) and files to delete (from Local)
            List<FileList> MissingFiles = new();
            List<FileList> FilesToDelete = new();

            // Check each file in the ServerFiles list
            foreach (var serverFile in ServerFiles)
            {
                // Look for the same file in the LocalFiles list using Hash, Name, and Path for comparison
                var localFile = LocalFiles.FirstOrDefault(file => file.Hash == serverFile.Hash && file.Name == serverFile.Name && file.Path == serverFile.Path);

                // If no match is found, mark this file as missing
                if (localFile == null)
                {
                    MissingFiles.Add(serverFile);
                }
            }

            // Now check the LocalFiles list for files that don't exist on the server
            foreach (var localFile in LocalFiles)
            {
                var serverFile = ServerFiles.FirstOrDefault(file => file.Hash == localFile.Hash && file.Name == localFile.Name && file.Path == localFile.Path);

                // If no match is found, mark this file as something to be deleted
                if (serverFile == null)
                {
                    FilesToDelete.Add(localFile);
                }
            }

            // Return the lists as a tuple
            return (MissingFiles, FilesToDelete);
        }
        public static async Task<ConcurrentBag<FileList>> GetFilesAsync(string filePath, IProgress<string>? progress = null)
        {
            // Ensure the target directory exists
            if (!Directory.Exists(filePath)) Directory.CreateDirectory(filePath);

            // Get all the file paths in the given directory and its subdirectories
            var filePaths = Directory.GetFiles(filePath, "*", SearchOption.AllDirectories);

            // Create a collection to store file details
            var fileList = new ConcurrentBag<FileList>();

            // Define the size of file batches to process at a time (helps with performance)
            var batchSize = 100; // This can be adjusted based on system capabilities, 

            // Use a semaphore to limit the number of concurrent tasks running at once
            var semaphore = new SemaphoreSlim(10);

            // A list to hold all the tasks that will process files asynchronously
            var tasks = new List<Task>();

            int totalFiles = filePaths.Length;
            int processedFiles = 0;

            // Process files in batches to avoid overwhelming the system with too many simultaneous tasks
            for (int i = 0; i < totalFiles; i += batchSize)
            {
                var batch = filePaths.Skip(i).Take(batchSize).ToArray();

                var task = Task.Run(async () =>
                {
                    await semaphore.WaitAsync(); // Wait for permission to start a task
                    try
                    {
                        // Process each file in the batch
                        foreach (var file in batch)
                        {
                            var fileInfo = new FileInfo(file);
                            var fileData = new FileList
                            {
                                Name = fileInfo.Name, // File name
                                Size = fileInfo.Length / 1_000.0, // Convert size to KB
                                Hash = await MD5FileHasah.GetMd5HashFromFileAsync(file), // Calculate file hash asynchronously
                                Path = fileInfo.DirectoryName?.Replace(@"\", "/")! // Normalize file path (replace backslashes with forward slashes)
                            };

                            // Add the file's information to the collection
                            fileList.Add(fileData);

                            // Update progress for the user
                            Interlocked.Increment(ref processedFiles);
                            progress?.Report($"Processing: {processedFiles}/{totalFiles} files");
                        }
                    }
                    finally
                    {
                        semaphore.Release(); // Release the semaphore to allow another task to run
                    }
                });

                tasks.Add(task); // Add this task to the list
            }

            // Wait for all tasks to complete
            await Task.WhenAll(tasks);
            progress?.Report("Processing complete.");

            return fileList; // Return the list of files that were processed
        }
        public static string GetExecutableLocation(string location, string Executable)
        {
            // Search for a specific executable file within a directory and its subdirectories
            if (Executable != null)
            {
                foreach (string f in Directory.EnumerateFiles(location, $"{Executable}.exe", SearchOption.AllDirectories))
                {
                    return f; // Return the full path of the found executable
                }
            }
            return string.Empty; // Return an empty string if no executable is found
        }
    }
}
