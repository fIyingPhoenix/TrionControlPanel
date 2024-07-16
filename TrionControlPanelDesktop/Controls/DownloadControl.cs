using System.Diagnostics;
using System.IO.Compression;
using TrionControlPanelDesktop.Data;
using TrionLibrary.Models;
using TrionLibrary.Sys;

namespace TrionControlPanelDesktop.Controls
{
    public partial class DownloadControl : UserControl
    {
        public static int TotalDownloads { get; set; }
        //Current Queue;
        private int CurrentDownload = 0;
        public static string Title { get; set; }
        public DownloadControl()
        {
            Dock = DockStyle.Fill;
            InitializeComponent();
            Data.Download.Infos.ListFull = false;
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
            if (Data.Download.Infos.ListFull == false)
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
        }
    }
}
