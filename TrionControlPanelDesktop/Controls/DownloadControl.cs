using System.Diagnostics;
using System.IO.Compression;
using TrionControlPanelDesktop.Data;
using TrionLibrary.Models;
using TrionLibrary.Sys;

namespace TrionControlPanelDesktop.Controls
{
    public partial class DownloadControl : UserControl
    {
        
        // List of URLs to download
        public static List<Lists.File> DownloadList = [];

        // Counting Downloaded URLs
        public static int TotalDownloads { get; set; }
        //Current Queue;
        private int CurrentDownload = 0;
        public static string Title { get; set; }
        public DownloadControl()
        {
            Dock = DockStyle.Fill;
            InitializeComponent();
            Data.Download.ListFull = false;
            TotalDownloads= 0;
        }
        private static readonly string[] separator = ["\n", "\r\n"];
        static List<string> ReadLinesFromString(string inputString)
        {
            // Split the inputString into lines using newline characters
            string[] linesArray = inputString.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            // Create a new list and add the lines to it
            List<string> linesList = new(linesArray);
            return linesList;
        }
        private async Task Download()
        {
            TotalDownloads = DownloadList.Count;
            using (HttpClient client = new())
            {
                foreach (var url in DownloadList.ToList())
                {
                    CurrentDownload++;
                    User.UI.Download.CurrentDownloads--;
                    LBLQueue.Text = $"Queue: {CurrentDownload} / {TotalDownloads}";
                    try
                    {
                        string DownloadLinkg = @$"{Links.MainHost}{url.FileFullName}";
                        LBLDownloadName.Text = $"Task: {url.FileName}";
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
                                        // Update PBar Maximum Value
                                        PBARDownload.LabelText = "MB";
                                        PBARDownload.Maximum = (int)totalDownloadSizeMB;
                                        PBARDownload.Value = (int)totalBytesReadMB;
                                        LBLDownloadSize.Text = $@"Size: {totalDownloadSizeMB:F2} MB";
                                        LBLDownloadSpeed.Text = $@"Speed: {speedMBps:F2}MBps";
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception ex) 
                    { 
                        Infos.Message = ex.Message;
                        DownloadList.Clear();
                    }  
                }
                DownloadList.Clear();
            }
        }
        private async Task UnzipFileAsync(string zipFilePath, string extractPath)
        {
            try
            {
                PBARDownload.Maximum = 100;
                PBARDownload.LabelText = "%";
                using (FileStream zipFileStream = File.OpenRead(zipFilePath))
                {
                    
                    using (ZipArchive archive = new(zipFileStream, ZipArchiveMode.Read))
                    {
                        long totalBytes = 0;
                        long extractedBytes = 0;
                        Stopwatch stopwatch = Stopwatch.StartNew();
                        foreach (ZipArchiveEntry entry in archive.Entries)
                        {
                            totalBytes += entry.Length;
                        }
                        foreach (ZipArchiveEntry entry in archive.Entries)
                        {
                            string fullPath = Path.Combine(extractPath, entry.FullName);
                            string? directoryName = Path.GetDirectoryName(fullPath);
                            if (!string.IsNullOrEmpty(directoryName) && !Directory.Exists(directoryName))
                            {
                               
                                Directory.CreateDirectory(directoryName); // Create directory if it doesn't exist
                            }
                            if (!entry.Name.Equals(string.Empty))
                            {
                              
                                using (Stream entryStream = entry.Open())
                                using (FileStream outputStream = File.Create(fullPath))
                                {
                                    byte[] buffer = new byte[4096]; //4 KB buffer
                                    int bytesRead;
                                    while ((bytesRead = await entryStream.ReadAsync(buffer)) > 0)
                                    {
                                        // Calculate writing speed
                                        double elapsedSeconds = stopwatch.Elapsed.TotalSeconds;
                                        double speedMbps = (extractedBytes / (1024 * 1024)) / elapsedSeconds;

                                        await outputStream.WriteAsync(buffer.AsMemory(0, bytesRead));
                                        extractedBytes += bytesRead;
                                        double progress = (double)extractedBytes / totalBytes * 100;
                                        PBARDownload.Value = (int)progress;
                                        LBLDownloadSpeed.Text = $@"Speed: {speedMbps:F2}MBps";
                                    }
                                }
                            }
                        }
                    }
                }
            
            }
            catch (Exception ex)
            {
                Infos.Message = $"Error: {ex.Message}";
            }
        }
        private void TimerWacher_Tick(object sender, EventArgs e)
        {
            if (Data.Download.ListFull == false)
            {
                TimerDownloadStart.Start();
            }
        }
        private void DownloadControl_Load(object sender, EventArgs e)
        {
            LBLQueue.Text = $@"Queue: {CurrentDownload} / {TotalDownloads}";
            LBLTitle.Text = Title;
        }
        private async void TimerDownloadStart_Tick(object sender, EventArgs e)
        {
            if (Data.Download.ListFull == true)
            {
                TimerDownloadStart.Stop();
                await Download();
            }
        }
    }
}
