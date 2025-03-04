using System.Text;
using System.Text.Json;
using TrionControlPanel.Desktop.Extensions.Classes.Monitor;
using TrionControlPanel.Desktop.Extensions.Cryptography;
using TrionControlPanel.Desktop.Extensions.Modules.Lists;
using TrionControlPanelDesktop.Extensions.Modules;
using System.Threading.Tasks;
using System.Threading;

namespace TrionControlPanel.Desktop.Extensions.Classes
{
    internal class FileManager
    {
        // Retrieves the substring starting from the given marker or returns the original string if the marker is not found.
        public static string StringBuilder(string input, string? marker)
        {
            int index = input.IndexOf(marker);
            return index >= 0 ? input.Substring(index) : input;
        }

        // Asynchronously downloads a file from the server, reports progress, speed, and elapsed time.
        public static async Task DownloadFileAsync(FileList file, string marker, CancellationToken cancellationToken,
            IProgress<double>? progress = null,
            IProgress<double>? elapsedTime = null,
            IProgress<double>? speed = null)
        {
            try
            {
                using (HttpClient client = new())
                {
                    string url = Links.APIRequests.DownlaodFiles(); // Base API URL for file download
                    // Create the JSON body with the file's path and name
                    var requestObj = new { filePath = $"{file.Path}/{file.Name}" };
                    string jsonContent = JsonSerializer.Serialize(requestObj);
                    var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                    using (HttpResponseMessage response = await client.PostAsync(url, content, cancellationToken))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            long fileSize = response.Content.Headers.ContentLength ?? 0; // Get file size for progress tracking
                            string downloadPath = $"{Directory.GetCurrentDirectory()}{StringBuilder(file.Path, marker)}";

                            // Ensure the download directory exists
                            if (!Directory.Exists(downloadPath)) Directory.CreateDirectory(downloadPath);

                            string fileDownload = $"{downloadPath}/{file.Name}";
                            await SaveFileAsync(response, fileDownload, fileSize, progress, elapsedTime, speed, cancellationToken);
                        }
                        else
                        {
                            // Log error if download fails
                            string errorMessage = await response.Content.ReadAsStringAsync(cancellationToken);
                            TrionLogger.Log($"Download Failed: {response.ReasonPhrase}\nDetails: {errorMessage}", "ERROR");
                        }
                    }
                }
            }
            catch (OperationCanceledException)
            {
                TrionLogger.Log("Download was canceled.", "INFO");
            }
            catch (Exception ex)
            {
                TrionLogger.Log($"Error occurred during file download: {ex.Message}", "ERROR");
            }
        }

        // Saves the downloaded file to disk and tracks progress, elapsed time, and download speed.
        private static async Task SaveFileAsync(HttpResponseMessage response, string fileDownload, long fileSize,
            IProgress<double>? progress, IProgress<double>? elapsedTime, IProgress<double>? speed, CancellationToken cancellationToken)
        {
            using (var stream = await response.Content.ReadAsStreamAsync(cancellationToken))
            using (var fileStream = new FileStream(fileDownload, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                byte[] buffer = new byte[8192];
                long totalBytesRead = 0;
                DateTime startTime = DateTime.Now;

                int bytesRead;
                // Read the file in chunks and save it to the disk
                while ((bytesRead = await stream.ReadAsync(buffer, cancellationToken)) > 0)
                {
                    await fileStream.WriteAsync(buffer.AsMemory(0, bytesRead), cancellationToken);
                    totalBytesRead += bytesRead;

                    // Calculate and report progress, elapsed time, and download speed
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

        // Asynchronously deletes files from the local system.
        public static async Task DeleteFiles(List<FileList> files)
        {
            var tasks = files.Select(file => Task.Run(() =>
            {
                try
                {
                    string filePath = $"{file.Path}/{file.Name}";
                    File.Delete(filePath); // Delete the file from disk
                    TrionLogger.Log($"Deleted file: {filePath}", "INFO");
                }
                catch (Exception ex)
                {
                    // Log error if file deletion fails
                    TrionLogger.Log($"Error deleting file {file.Name}: {ex.Message}", "ERROR");
                }
            })).ToList();

            await Task.WhenAll(tasks); // Wait for all delete tasks to complete
        }

        // Compares files between the server and the local system, reports missing or outdated files.
        public async static Task<(List<FileList> MissingFiles, List<FileList> FilesToDelete)>
            CompareFiles(List<FileList> ServerFiles, List<FileList> LocalFiles, string marker,
            IProgress<string> FileToDelete, IProgress<string> FileToDownload)
        {
            List<FileList> missingFiles = new();
            List<FileList> filesToDelete = new();

            // Find missing files by comparing hashes, names, and paths
            await Task.WhenAll(ServerFiles.Select(async serverFile =>
            {
                await Task.Delay(1);
                var localFile = LocalFiles.FirstOrDefault(file =>
                    file.Hash == serverFile.Hash && file.Name == serverFile.Name &&
                    StringBuilder(file.Path.Replace(@"\", "/"), marker) == StringBuilder(serverFile.Path, marker));
                
                if (localFile == null)
                {
                    missingFiles.Add(serverFile);               
                }
            }));

            // Find files to delete by comparing hashes, names, and paths
            await Task.WhenAll(LocalFiles.Select(async localFile =>
            {
                await Task.Delay(1);
                var serverFile = ServerFiles.FirstOrDefault(file =>
                    file.Hash == localFile.Hash && file.Name == localFile.Name &&
                    StringBuilder(file.Path.Replace(@"\", "/"), marker) == StringBuilder(localFile.Path, marker));

                if (serverFile == null)
                {
                    filesToDelete.Add(localFile);     
                }
            }));
            FileToDelete?.Report($"{filesToDelete.Count}");
            FileToDownload?.Report($"{missingFiles.Count}");
            return (missingFiles, filesToDelete);
        }

        // Processes files in a given directory asynchronously, reports progress as it processes files.
        public static async Task<List<FileList>> ProcessFilesAsync(string filePath, IProgress<string>? progress)
        {
            if (!Directory.Exists(filePath)) Directory.CreateDirectory(filePath); // Ensure the directory exists

            var fileList = new List<FileList>();
            var semaphore = new SemaphoreSlim(1000); // Limit the number of concurrent tasks
            var tasks = new List<Task>();

            int processedFiles = 0;
            var Files = Directory.EnumerateFiles(filePath, "*", SearchOption.AllDirectories);
            var TotalFiles = Files.Count();   
            foreach (var file in Files)
            {
                await semaphore.WaitAsync(); // Wait for permission to process a file

                var task = Task.Run(async () =>
                {
                    try
                    {
                        var fileInfo = new FileInfo(file);
                        var fileData = new FileList
                        {
                            Name = fileInfo.Name,
                            Size = fileInfo.Length / 1_000.0, // Convert size to kilobytes
                            Hash = await MD5FileHash.GetMd5HashFromFileAsync(file), // Calculate file hash
                            Path = fileInfo.DirectoryName?.Replace(@"\", "/")! // Normalize path
                        };

                        lock (fileList)
                        {
                            fileList.Add(fileData); // Add file info to the list in a thread-safe way
                        }

                        // Report progress every 10 files processed
                        if (Interlocked.Increment(ref processedFiles) % 10 == 0)
                        {
                            progress?.Report($"{processedFiles} / {TotalFiles}");
                        }
                    }
                    finally
                    {
                        semaphore.Release(); // Release semaphore slot
                    }
                });

                tasks.Add(task);

                // Avoid too many tasks in memory by waiting for some to finish
                if (tasks.Count >= 100)
                {
                    await Task.WhenAny(tasks); // Wait for any task to complete
                    tasks.RemoveAll(t => t.IsCompleted); // Remove completed tasks
                }
            }

            await Task.WhenAll(tasks); // Wait for all tasks to complete
            progress?.Report("Processing complete.");

            return fileList;
        }

        // Searches for a specific executable within a directory and subdirectories.
        public static string GetExecutableLocation(string location, string Executable)
        {
            foreach (string file in Directory.EnumerateFiles(location, $"{Executable}.exe", SearchOption.AllDirectories))
            {
                return file; // Return the path of the found executable
            }
            return string.Empty; // Return an empty string if not found
        }
    }
}
