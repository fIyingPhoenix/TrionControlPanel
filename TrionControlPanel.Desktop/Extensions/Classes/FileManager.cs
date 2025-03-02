using System.Collections.Concurrent;
using System.Text;
using System.Text.Json;
using TrionControlPanel.Desktop.Extensions.Classes.Monitor;
using TrionControlPanel.Desktop.Extensions.Cryptography;
using TrionControlPanel.Desktop.Extensions.Modules.Lists;
using TrionControlPanelDesktop.Extensions.Modules;

namespace TrionControlPanel.Desktop.Extensions.Classes
{
    internal class FileManager
    {
        public static string StringBuilder(string input, string? marker)
        {
            int index = input.IndexOf(marker);

            if (index >= 0)
            {
                // Start the substring from the marker's position (i.e., from index + marker.Length)
                return input.Substring(index);
            }
            else
            {
                return input;
            }
        }

        public static async Task DownloadFileAsync(FileList file,string marker, CancellationToken cancellationToken,
            IProgress<double>? progress = null,
            IProgress<double>? elapsedTime = null,
            IProgress<double>? speed = null)
        {
            try
            {
                using (HttpClient client = new())
                {
                    string url = Links.APIRequests.DownlaodFiles(); // API base URL
                    TrionLogger.Log($"Requesting: {url}", "INFO");

                    // Prepare the JSON request body
                    var requestObj = new { filePath = $"{file.Path}/{file.Name}" };
                    string jsonContent = JsonSerializer.Serialize(requestObj);
                    var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                    TrionLogger.Log($"Payload: {jsonContent}", "INFO");

                    // Send POST request with the JSON body
                    using (HttpResponseMessage response = await client.PostAsync(url, content, cancellationToken))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            // Get file size (if available)
                            long fileSize = response.Content.Headers.ContentLength ?? 0;
                            string fileName = file.Name;
                            string DownloadPath = $"{Directory.GetCurrentDirectory()}{StringBuilder(file.Path, marker)}";
                            if (!Directory.Exists(DownloadPath)) { Directory.CreateDirectory(DownloadPath); }
                            string FileDownload = $"{DownloadPath}/{file.Name}";
                            // Save the file to disk
                            using (var stream = await response.Content.ReadAsStreamAsync(cancellationToken))
                            using (var fileStream = new FileStream(FileDownload, FileMode.Create, FileAccess.Write, FileShare.None))
                            {
                                byte[] buffer = new byte[8192];
                                long totalBytesRead = 0;
                                DateTime startTime = DateTime.Now;

                                int bytesRead;
                                while ((bytesRead = await stream.ReadAsync(buffer, cancellationToken)) > 0)
                                {
                                    await fileStream.WriteAsync(buffer.AsMemory(0, bytesRead), cancellationToken);
                                    totalBytesRead += bytesRead;

                                    // Calculate progress, elapsed time, and speed
                                    double progressValue = (double)totalBytesRead / fileSize * 100;
                                    double elapsedTimeValue = (DateTime.Now - startTime).TotalSeconds;
                                    double speedValue = totalBytesRead / 1024.0 / 1024.0 / elapsedTimeValue;  // in MB/s

                                    progress?.Report(progressValue);
                                    elapsedTime?.Report(elapsedTimeValue);
                                    speed?.Report(speedValue);
                                }

                                TrionLogger.Log("File downloaded successfully!", "SUCCESS");
                            }
                        }
                        else
                        {
                            string errorMessage = await response.Content.ReadAsStringAsync(cancellationToken);
                            TrionLogger.Log($"Download Failed: {response.ReasonPhrase}\nDetails: {errorMessage}", "ERROR");
                        }
                    }
                }
            }
            catch (OperationCanceledException)
            {
                TrionLogger.Log("Download was canceled.", "CANCELED");
            }
            catch (Exception ex)
            {
                TrionLogger.Log($"An error occurred: {ex.Message}", "ERROR");
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
        public async static Task<(List<FileList> MissingFiles, List<FileList> FilesToDelete)> CompareFiles(List<FileList> ServerFiles, List<FileList> LocalFiles, string marker)
        {
            // Create lists to track missing files (from Local) and files to delete (from Local)
            List<FileList> MissingFiles = new();
            List<FileList> FilesToDelete = new();

            // Check each file in the ServerFiles list
            foreach (var serverFile in ServerFiles)
            {
                // Look for the same file in the LocalFiles list using Hash, Name, and Path for comparison
                var localFile = LocalFiles.FirstOrDefault(file => file.Hash == serverFile.Hash && file.Name == serverFile.Name && StringBuilder(file.Path, marker) == StringBuilder(serverFile.Path, marker));

                // If no match is found, mark this file as missing
                if (localFile == null)
                {
                    MissingFiles.Add(serverFile);
                }
            }

            // Now check the LocalFiles list for files that don't exist on the server
            foreach (var localFile in LocalFiles)
            {
                
                var serverFile = ServerFiles.FirstOrDefault(file => file.Hash == localFile.Hash && file.Name == localFile.Name && StringBuilder(file.Path, marker) == StringBuilder(localFile.Path, marker));

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
