using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TrionControlPanelDesktop.FormData;

namespace TrionControlPanelDesktop.Controls
{
    public partial class DownloadControl : UserControl
    {

        // List of URLs to download
        private static List<string> DownloadList = new() {
          //Test Files!
         "https://ash-speed.hetzner.com/100MB.bin",
         "https://ash-speed.hetzner.com/1GB.bin"
        };
        // Counting Downloaded URLs
        private int TotalDownloads = 0;
        public DownloadControl()
        {
            Dock = DockStyle.Fill;
            InitializeComponent();
        }
        public static void AddToList(string URL)
        {
            DownloadList.Add(URL);
        }
        private async Task Download()
        {
            string downloadDirectory = Directory.GetCurrentDirectory();

            using (HttpClient client = new())
            {
                foreach (var url in DownloadList)
                {
                    // Update Downloaded URLs
                    TotalDownloads++;

                    // Update Label
                    LBLQueue.Text = $@"{TotalDownloads} / {DownloadList.Count}";

                    try
                    {
                        // Send GET request to the server
                        using (HttpResponseMessage response = await client.GetAsync(url, HttpCompletionOption.ResponseHeadersRead))
                        {
                            // Check if request was successful
                            response.EnsureSuccessStatusCode();

                            // Get the file name from the URL
                            string fileName = Path.GetFileName(url);
                            string downloadPath = Path.Combine(downloadDirectory, fileName);
                            LBLDownloadName.Text = @$"Name: {fileName}";
                            // Create a file stream to write the downloaded content
                            using (FileStream fileStream = new(downloadPath, FileMode.Create, FileAccess.Write, FileShare.None))
                            {
                                // Get the response stream
                                using (Stream stream = await response.Content.ReadAsStreamAsync())
                                {
                                    byte[] buffer = new byte[8192]; // 8 KB buffer
                                    int bytesRead;
                                    long totalBytesRead = 0;
                                    long totalDownloadSize = response.Content.Headers.ContentLength ?? -1;

                                    // Update PBar Maximum Value


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
                                        PBARDownload.Maximum = (int)totalDownloadSizeMB;
                                        PBARDownload.Value = (int)totalBytesReadMB;
                                        LBLDownloadSize.Text = $@"Size: {totalBytesReadMB:F2} MB";
                                        LBLDownloadSpeed.Text = $@"Download Speed: {speedMBps:F2}MBps";
                                    }
                                }
                            }


                        }
                    }
                    catch
                    {

                    }
                }

            }
        }

        private void TimerWacher_Tick(object sender, EventArgs e)
        {

        }

        private void DownloadControl_Load(object sender, EventArgs e)
        {
            LBLQueue.Text = $@"{TotalDownloads} / {DownloadList.Count}";
        }

        private async void BTNTestDownload_ClickAsync(object sender, EventArgs e)
        {
            await Download();
        }
    }
}
