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
                await Task.Run( () => File.Delete($"{file.Path}/{file.Name}"));
            }
        }
        public async static Task<(List<FileList> MissingFiles, List<FileList> FilesToDelete)> 
            CompareFiles(List<FileList> ServerFiles, List<FileList> LocalFiles, string marker,
            IProgress<string>FileToDelete, IProgress<string>FileToDownload)
        {
            // Create lists to track missing files (from Local) and files to delete (from Local)
            List<FileList> MissingFiles = new();
            List<FileList> FilesToDelete = new();

          // Check each file in the ServerFiles list
           await Task.Run( () =>
           {
               foreach (var serverFile in ServerFiles)
               {
                   // Look for the same file in the LocalFiles list using Hash, Name, and Path for comparison
                   var localFile = LocalFiles.FirstOrDefault(file => file.Hash == serverFile.Hash && file.Name == serverFile.Name && StringBuilder(file.Path.Replace(@"\", "/"), marker) == StringBuilder(serverFile.Path, marker));

                   // If no match is found, mark this file as missing
                   if (localFile == null)
                   {
                       MissingFiles.Add(serverFile);
                   }
                   FileToDownload?.Report($"{MissingFiles.Count}");
               }
           });

            // Now check the LocalFiles list for files that don't exist on the server
            await Task.Run(() =>
            {
                foreach (var localFile in LocalFiles)
                {
                    var serverFile = ServerFiles.FirstOrDefault(file => file.Hash == localFile.Hash && file.Name == localFile.Name && StringBuilder(file.Path.Replace(@"\", "/"), marker) == StringBuilder(localFile.Path, marker));
                    // If no match is found, mark this file as something to be deleted
                    if (serverFile == null)
                    {
                        FilesToDelete.Add(localFile);
                    }
                    FileToDelete?.Report($"{FilesToDelete.Count}");
                }
            });


            // Return the lists as a tuple
            return (MissingFiles, FilesToDelete);
        }
        public static async Task<List<FileList>> ProcessFilesAsync(string filePath, IProgress<string>? progress)
        {
            if (!Directory.Exists(filePath)) Directory.CreateDirectory(filePath);

            var fileList = new List<FileList>();
            var semaphore = new SemaphoreSlim(1000); // Limit concurrent tasks
            var tasks = new List<Task>();

            int processedFiles = 0;

            foreach (var file in Directory.EnumerateFiles(filePath, "*", SearchOption.AllDirectories))
            {
                await semaphore.WaitAsync(); // Control concurrency
                var task = Task.Run(async () =>
                {
                    try
                    {
                        var fileInfo = new FileInfo(file);
                        var fileData = new FileList
                        {
                            Name = fileInfo.Name,
                            Size = fileInfo.Length / 1_000.0,
                            Hash = await MD5FileHasah.GetMd5HashFromFileAsync(file),
                            Path = fileInfo.DirectoryName?.Replace(@"\", "/")!
                        };

                        lock (fileList)
                        {
                            fileList.Add(fileData);
                        }

                        if (Interlocked.Increment(ref processedFiles) % 10 == 0)
                        {
                            progress?.Report($"Processing: {processedFiles} files");
                        }
                    }
                    finally
                    {
                        semaphore.Release();
                    }
                });

                tasks.Add(task);

                // Avoid too many pending tasks in memory
                if (tasks.Count >= 100)
                {
                    await Task.WhenAny(tasks);
                    tasks.RemoveAll(t => t.IsCompleted);
                }
            }

            await Task.WhenAll(tasks);
            progress?.Report("Processing complete.");

            return fileList;
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
