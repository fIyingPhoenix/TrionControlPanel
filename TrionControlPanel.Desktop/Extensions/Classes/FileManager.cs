
using System.Diagnostics;
using System.Text;
using System.Text.Json;
using TrionControlPanel.Desktop.Extensions.Classes.Monitor;
using TrionControlPanel.Desktop.Extensions.Cryptography;
using TrionControlPanel.Desktop.Extensions.Modules.Lists;
using TrionControlPanelDesktop.Extensions.Modules;

namespace TrionControlPanel.Desktop.Extensions.Classes
{
    // FileManager class for handling file operations such as downloading, deleting, and processing files.
    internal class FileManager
    {
        // Builds a substring from the input string starting from the marker's position.
        public static string StringBuilder(string input, string? marker)
        {
            int index = input.IndexOf(marker);
            return index >= 0 ? input.Substring(index) : input;
        }

        // Asynchronously downloads a file from the server.
        public static async Task DownloadFileAsync(FileList file, string marker, CancellationToken cancellationToken,
            IProgress<double>? progress = null, IProgress<double>? elapsedTime = null, IProgress<double>? speed = null)
        {
            await Task.Run(async () =>
            {
                try
                {
                    string url = Links.APIRequests.DownlaodFiles(); // API base URL
                    TrionLogger.Log($"Requesting: {url}", "INFO");

                    using HttpClient client = new();

                    // Ensure that the file path is valid and not too long
                    string filePath = $"{file.Path}/{file.Name}";
                    if (filePath.Length > 2000) // Adjust length limit as per your needs
                    {
                        TrionLogger.Log("File path is too long!", "ERROR");
                        return;
                    }

                    var requestObj = new { filePath };
                    string jsonContent = JsonSerializer.Serialize(requestObj);
                    var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                    // Send POST request to get the file
                    using HttpResponseMessage response = await client.PostAsync(url, content, cancellationToken);

                    if (!response.IsSuccessStatusCode)
                    {
                        string errorMessage = await response.Content.ReadAsStringAsync(cancellationToken);
                        TrionLogger.Log($"Download Failed: {response.ReasonPhrase}\nDetails: {errorMessage}", "ERROR");
                        return;
                    }

                    string downloadPath = $"{Directory.GetCurrentDirectory()}{StringBuilder(file.Path, marker)}";
                    if (!Directory.Exists(downloadPath)) Directory.CreateDirectory(downloadPath);
                    string fileDownload = Path.Combine(downloadPath, file.Name);

                    // Save the file to disk
                    using (var stream = await response.Content.ReadAsStreamAsync(cancellationToken))
                    using (var fileStream = new FileStream(fileDownload, FileMode.Create, FileAccess.Write, FileShare.None, bufferSize: 8192, useAsync: true))
                    {
                        byte[] buffer = new byte[2048];
                        long totalBytesRead = 0;
                        long previousBytesRead = 0;
                        DateTime startTime = DateTime.Now;
                        Stopwatch stopwatch = Stopwatch.StartNew();

                        int bytesRead;
                        while ((bytesRead = await stream.ReadAsync(buffer.AsMemory(0, buffer.Length), cancellationToken)) > 0)
                        {
                            await fileStream.WriteAsync(buffer.AsMemory(0, bytesRead), cancellationToken);
                            totalBytesRead += bytesRead;

                            // Report progress, elapsed time, and speed every second
                            if (stopwatch.ElapsedMilliseconds >= 1)
                            {
                                double progressValue = (double)totalBytesRead / response.Content.Headers.ContentLength!.Value * 100;
                                double elapsedTimeValue = (DateTime.Now - startTime).TotalSeconds;
                                double speedValue = (totalBytesRead - previousBytesRead) / 1024.0 / 1024.0;  // in MB/s

                                progress?.Report(progressValue);
                                elapsedTime?.Report(elapsedTimeValue);
                                speed?.Report(speedValue);

                                previousBytesRead = totalBytesRead;
                                stopwatch.Restart();
                            }
                        }
                        bytesRead = 0;
                        Array.Clear(buffer, 0, buffer.Length);

                    }
                }
                catch (OperationCanceledException)
                {
                    TrionLogger.Log("Download was canceled.", "CANCELED");
                }
                catch (HttpRequestException ex)
                {
                    TrionLogger.Log($"HTTP request error: {ex.Message}", "ERROR");
                }
                catch (IOException ex)
                {
                    TrionLogger.Log($"File I/O error: {ex.Message}", "ERROR");
                }
                catch (Exception ex)
                {
                    TrionLogger.Log($"An unexpected error occurred: {ex.Message}", "ERROR");
                }
            });
        }

        // Asynchronously deletes a list of files.
        public static async Task DeleteFiles(List<FileList> files)
        {
            foreach (var file in files)
            {
                // Run the delete operation in a separate task to avoid blocking the calling thread
                await Task.Run(() => File.Delete($"{file.Path}/{file.Name}"));
            }
        }

        // Compares server files with local files and returns lists of missing files and files to delete.
        public async static Task<(List<FileList> MissingFiles, List<FileList> FilesToDelete)>
            CompareFiles(List<FileList> ServerFiles, List<FileList> LocalFiles, string marker,
            IProgress<string> FileToDelete, IProgress<string> FileToDownload)
        {
            List<FileList> MissingFiles = new();
            List<FileList> FilesToDelete = new();

            // Check each file in the ServerFiles list
            await Task.Run(() =>
            {
                foreach (var serverFile in ServerFiles)
                {
                    // Look for the same file in the LocalFiles list using Hash, Name, and Path for comparison
                    var localFile = LocalFiles.FirstOrDefault(file => file.Hash == serverFile.Hash && file.Name == serverFile.Name && StringBuilder(file.Path.Replace(@"\", "/"), marker) == StringBuilder(serverFile.Path, marker));
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
                    if (serverFile == null)
                    {
                        FilesToDelete.Add(localFile);
                    }
                    FileToDelete?.Report($"{FilesToDelete.Count}");
                }
            });

            return (MissingFiles, FilesToDelete);
        }

        // Asynchronously processes files in a directory and returns a list of file information.
        public static async Task<List<FileList>> ProcessFilesAsync(string filePath, IProgress<string>? progress, CancellationToken cancellationToken = default)
        {
            if (!Directory.Exists(filePath)) Directory.CreateDirectory(filePath);

            var fileList = new List<FileList>();
            var semaphore = new SemaphoreSlim(1000); // Limit concurrent tasks
            var tasks = new List<Task>();
            int processedFiles = 0;
            var totalFiles = Directory.EnumerateFiles(filePath, "*", SearchOption.AllDirectories);
            foreach (var file in totalFiles)
            {
                processedFiles++;
                await semaphore.WaitAsync(cancellationToken); // Control concurrency
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
                        progress?.Report($"{processedFiles} / {totalFiles.Count()}");
                    }
                    finally
                    {
                        semaphore.Release();
                    }
                }, cancellationToken);

                tasks.Add(task);

                // Avoid too many pending tasks in memory
                if (tasks.Count >= 100)
                {
                    await Task.WhenAny(tasks);
                    tasks.RemoveAll(t => t.IsCompleted);
                }
            }

            await Task.WhenAll(tasks);

            return fileList;
        }

        // Searches for a specific executable file within a directory and its subdirectories.
        public static string GetExecutableLocation(string location, string Executable)
        {
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
