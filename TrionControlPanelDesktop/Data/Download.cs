using System.Diagnostics;
using System.Xml.Linq;
using TrionLibrary.Crypto;
using TrionLibrary.Models;

namespace TrionControlPanelDesktop.Data
{
    internal class Download
    {
        public class Infos
        {

            public class Install
            {
                public static bool Database { get; set; }
                public static bool Classic { get; set; }
                public static bool TBC { get; set; }
                public static bool WotLK { get; set; }
                public static bool lCata { get; set; }  
                public static bool Mop { get; set; }
            }
            public class Download
            {
                public static int TotalCount { get; set; }
                public static int CurrentCount { get; set; }
                public static string FileName {  get; set; }
                public static int FileSizeMB {  get; set; }
                public static int ReadSizeMB { get; set; }
                public static int Progress { get; set; }
                public static int Speed { get; set; }
            }


            public static bool ListFull { get; set; }

            public static double ProgressPercentage;
        }
        // Function to compare file hashes and export changes to XML Online
        public static async Task<List<Lists.File>> CompareAndExportChangesOnline(string folderPath, string previousXmlUrl)
        {
            var previousFileInfos = new List<Lists.File>();

            // Load previous XML file from the web
            using (var httpClient = new HttpClient())
            {
                var xmlContent = await httpClient.GetStringAsync(previousXmlUrl);
                var previousXml = XDocument.Parse(xmlContent);
                previousFileInfos = (from file in previousXml.Root!.Elements("File")
                                     select new Lists.File
                                     {
                                         FileName = file.Element("FileName")!.Value,
                                         FileFullName = file.Element("FileFullName")!.Value,
                                         FileHash = file.Element("FileHash")!.Value
                                     }).ToList();
            }

            var currentFileInfos = new List<Lists.File>();

            var allFiles = FileHash.GetAllFiles(folderPath).ToList();
            int totalFiles = allFiles.Count;
            int currentFileIndex = 0;

            // Calculate SHA-256 hashes for all files in the folder
            foreach (var file in allFiles)
            {
                string _fileName = Path.GetFileName(file);
                var fileInfo = new Lists.File
                {
                    FileName = _fileName,
                    FileFullName = file.Replace(@"\", "/"),
                    FileHash = FileHash.CalculateSHA256(file)
                };
                currentFileInfos.Add(fileInfo);

                currentFileIndex++;

                // Calculate progress percentage
                Infos.ProgressPercentage = (double)currentFileIndex / totalFiles * 100;
            }

            // Identify missing files (present in previous XML but not in current folder)
            var missingFiles = previousFileInfos.Where(previous => !currentFileInfos.Any(current => current.FileHash == previous.FileHash));

            // Compare current file hashes with previous ones and export changes to XML
            var changedFiles = currentFileInfos.Where(current => !previousFileInfos.Any(previous => previous.FileName == current.FileName && previous.FileHash == current.FileHash));

            // Combine missing files and changed files
            var allChangedFiles = missingFiles.Concat(changedFiles);

            // Export all changes to List
            List<Lists.File> csvContent = new();

            foreach (var file in allChangedFiles)
            {
                csvContent.Add(
                    new Lists.File()
                    {
                        FileName = file.FileName,
                        FileFullName = file.FileFullName,
                        FileHash = file.FileHash
                    }
                    );
            }
            return csvContent;
        }
        public async Task StartDownload(List<Lists.File> DownloadList)
        {
            Infos.Download.TotalCount = DownloadList.Count;
            using (HttpClient client = new())
            {
                foreach (var url in DownloadList.ToList())
                {
                    Infos.Download.CurrentCount++;
                    User.UI.Download.CurrentDownloads--;
                    try
                    {
                        string DownloadLinkg = @$"{Links.MainHost}{url.FileFullName}";
                        Infos.Download.FileName = url.FileName;
                        using (HttpResponseMessage response = await client.GetAsync(DownloadLinkg, HttpCompletionOption.ResponseHeadersRead))
                        {
                            // Check if request was successful
                            response.EnsureSuccessStatusCode();

                            string directoryPath = Path.GetDirectoryName(url.FileFullName)!;
                            if (!Directory.Exists(directoryPath)) { Directory.CreateDirectory(directoryPath); }
                            // Create a file stream to write the downloaded content
                            using (FileStream fileStream = new(url.FileFullName, FileMode.Create, FileAccess.Write, FileShare.None))
                            {
                                // Get the response stream
                                using (Stream stream = await response.Content.ReadAsStreamAsync())
                                {
                                    byte[] buffer = new byte[8192]; // 8 KB buffer
                                    int bytesRead;
                                    long totalBytesRead = 0;
                                    long totalDownloadSize = response.Content.Headers.ContentLength ?? -1;
                                    Stopwatch stopwatch = Stopwatch.StartNew();
                                    // Read from the stream in chunks and write to the file stream
                                    while ((bytesRead = await stream.ReadAsync(buffer)) > 0)
                                    {
                                        await fileStream.WriteAsync(buffer.AsMemory(0, bytesRead));
                                        totalBytesRead += bytesRead;
                                        // Calculate download speed
                                        double elapsedTimeSeconds = stopwatch.Elapsed.TotalSeconds;
                                        double speedMBps = totalBytesRead / 1024 / 1024 / elapsedTimeSeconds; // bytes to MBps
                                        // Display progress
                                        double totalDownloadSizeMB = (double)totalDownloadSize / 1024 / 1024; // bytes to MB
                                        double totalBytesReadMB = (double)totalBytesRead / 1024 / 1024; // bytes to MB

                                        Infos.Download.FileSizeMB = (int)totalDownloadSizeMB;
                                        Infos.Download.ReadSizeMB = (int)totalBytesReadMB;
                                        Infos.Download.Speed = (int)speedMBps;
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        TrionLibrary.Sys.Infos.Message = ex.Message;
                        DownloadList.Clear();
                    }
                }
                DownloadList.Clear();
            }
        }
    }
}
