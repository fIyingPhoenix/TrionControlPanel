using System.Diagnostics;
using System.IO.Compression;
using TrionControlPanelDesktop.FormData;
using TrionLibrary;

namespace TrionControlPanelDesktop.Controls
{
    public partial class DownloadControl : UserControl
    {
        private static bool ListFull = false;
        // List of URLs to download
        private static List<UrlData> DownloadList = new();
        // Counting Downloaded URLs
        private int TotalDownloads = 0;
        private int CurrentDownload = 0;
        public static string Title { get; set; }
        public static bool InstallSPP { get; set; }
        public static bool InstallMySQL { get; set; }
        public DownloadControl()
        {
            Dock = DockStyle.Fill;
            InitializeComponent();
        }
        static List<string> ReadLinesFromString(string inputString)
        {
            // Split the inputString into lines using newline characters
            string[] linesArray = inputString.Split(new[] { "\n", "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            // Create a new list and add the lines to it
            List<string> linesList = new(linesArray);
            return linesList;
        }
        public async static Task AddToList(string Weblink)
        {
            DownloadList.Clear();
            using (HttpClient client = new())
            {
                if (!string.IsNullOrEmpty(Weblink))
                {
                    // Download the text file content
                    string fileContent = await client.GetStringAsync(Weblink);
                    List<string> strings = ReadLinesFromString(fileContent).Where(s => !s.StartsWith("#")).ToList();
                    // Split the file content by comma and add each item to the list
                    foreach (var lines in strings)
                    {
                        string[] Entry = lines.Split(',');
                        UrlData newUrlList = new();
                        if (Entry[1].Contains("1drv.ms"))
                        {
                            newUrlList.FileName = Entry[0];
                            newUrlList.FileWebLink = UIData.DownloadOneDriveAPI(Entry[1]);
                            newUrlList.FileType = Entry[2];
                        }
                        else
                        {
                            newUrlList.FileName = Path.GetFileNameWithoutExtension(Entry[1]);
                            newUrlList.FileWebLink = Entry[1];
                            newUrlList.FileType = Path.GetExtension(Entry[1]);
                        }
                        DownloadList.Add(newUrlList);
                        UIData.CurrentDownloads++;
                    }
                    ListFull = true;
                }
                else
                {
                    //error!
                }
            }
        }
        private async Task Download()
        {
            string downloadDirectory = Directory.GetCurrentDirectory();
            using (HttpClient client = new())
            {
                foreach (var url in DownloadList)
                {
                    TotalDownloads = DownloadList.Count;
                    // Update Downloaded URLs
                    CurrentDownload++;
                    try
                    {
                        string FullFile;
                        string downloadPath;
                        // Send GET request to the server
                        using (HttpResponseMessage response = await client.GetAsync(url.FileWebLink, HttpCompletionOption.ResponseHeadersRead))
                        {
                            // Update Label
                            LBLQueue.Text = $"Queue: {CurrentDownload} / {TotalDownloads}";
                            LBLStatus.Text = "Status: Connect!";
                            // Check if request was successful
                            response.EnsureSuccessStatusCode();
                            LBLStatus.Text = "Status: Read File!";
                            // Get the file name from the URL
                            string fileName = Path.GetFileName(url.FileName);
                            string fileType = url.FileType;
                            FullFile = $@"{fileName}{fileType}";
                            downloadPath = Path.Combine(downloadDirectory, FullFile);
                            LBLDownloadName.Text = @$"Name: {FullFile}";
                            LBLStatus.Text = "Status: Prepare Download!";
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
                                    Stopwatch stopwatch = Stopwatch.StartNew();
                                    // Read from the stream in chunks and write to the file stream
                                    while ((bytesRead = await stream.ReadAsync(buffer)) > 0)
                                    {
                                        LBLStatus.Text = "Status: Downloading!";
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
                            LBLStatus.Text = "Status: Done!";
                            // Delay task so we dont get Still in use error
                            await Task.Delay(1500);
                            if (fileType.Contains(".zip"))
                            {
                                await UnzipFileAsync(Path.Combine(Directory.GetCurrentDirectory(), FullFile), Directory.GetCurrentDirectory());
                            }
                            else if (fileType.Contains(".exe"))
                            {
                                UIData.CurrentDownloads--;
                                Process.Start(downloadPath);
                                Environment.Exit(0);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Data.Message = ex.Message;
                    }
                }
            }
        }
        private async Task UnzipFileAsync(string zipFilePath, string extractPath)
        {
            try
            {
                PBARDownload.Maximum = 100;
                PBARDownload.LabelText = "%";
                LBLStatus.Text = "Status: Prepare to Unpackage!";
                using (FileStream zipFileStream = File.OpenRead(zipFilePath))
                {
                    LBLStatus.Text = "Status: Read File!";
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
                                LBLStatus.Text = "Status: Prepare directorys!";
                                Directory.CreateDirectory(directoryName); // Create directory if it doesn't exist
                            }
                            if (!entry.Name.Equals(string.Empty))
                            {
                                LBLStatus.Text = "Status: Write Data!";
                                LBLFIleName.Text = @$"File: {entry.Name}";
                                using (Stream entryStream = entry.Open())
                                using (FileStream outputStream = File.Create(fullPath))
                                {
                                    byte[] buffer = new byte[4096]; //8 KB buffer
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
                LBLStatus.Text = "Unzip operation completed successfully.";
                LBLFIleName.Text = @$"File: ";
                File.Delete(zipFilePath);

                if (CurrentDownload == TotalDownloads)
                {
                    CurrentDownload = 0;
                    MainForm.LoadDownload = true;
                    Data.Message = "Download complete!";
                    string path = Directory.GetCurrentDirectory();
                    if (Directory.Exists($@"{path}\database\data")) { Directory.Delete($@"{path}\database\data", true); }
                    if (Data.GetExecutableLocation(path, Data.Settings.MySQLExecutableName) != string.Empty)
                    {
                        Data.Settings.MySQLExecutableLocation = Data.GetExecutableLocation(path, Data.Settings.MySQLExecutableName);
                        if (Data.Settings.MySQLExecutableLocation != string.Empty)
                        {
                            string BinFolder = Path.GetDirectoryName(Data.Settings.MySQLExecutableLocation)!;
                            Data.Settings.MySQLLocation = Path.GetFullPath(Path.Combine(BinFolder, @"..\"));
                        }
                    }
                    if (Data.GetExecutableLocation(path, Data.Settings.WorldExecutableName) != string.Empty)
                    {
                        Data.Settings.LogonExecutableLocation = Data.GetExecutableLocation(path, Data.Settings.LogonExecutableName);
                        Data.Settings.WorldExecutableLocation = Data.GetExecutableLocation(path, Data.Settings.WorldExecutableName);
                        Data.Settings.CoreLocation = Path.GetDirectoryName(Data.Settings.WorldExecutableLocation);
                    }
                    await Data.SaveSettings();
                    if (InstallSPP == true)
                    {
                        InstallSPP = false;
                        ListFull = false;
                        string SQLLocation = $@"{path}\database\extra\initSPP.sql";
                        SystemWatcher.ApplicationStart(Data.Settings.MySQLExecutableLocation, Data.Settings.MySQLExecutableName, Data.Settings.ConsolHide, $"--initialize-insecure --init-file=\"{SQLLocation}\" --console");
                    }
                    if (InstallMySQL == true)
                    {
                        InstallMySQL = false;
                        ListFull = false;
                        string SQLLocation = $@"{path}\database\extra\initMySQL.sql";
                        SystemWatcher.ApplicationStart(Data.Settings.MySQLExecutableLocation, Data.Settings.MySQLExecutableName, Data.Settings.ConsolHide, $"--initialize-insecure --init-file=\"{SQLLocation}\" --console");
                    }

                }
                UIData.CurrentDownloads--;
            }
            catch (Exception ex)
            {
                Data.Message = $"Error: {ex.Message}";
            }
        }
        private void TimerWacher_Tick(object sender, EventArgs e)
        {
            if (ListFull == false)
            {
                TimerDownloadStart.Start();
            }
        }
        private void DownloadControl_Load(object sender, EventArgs e)
        {
            //LBLQueue.Text = $@"Queue: {CurrentDownload} / {TotalDownloads}";
            LBLTitle.Text = Title;
        }
        private async void TimerDownloadStart_Tick(object sender, EventArgs e)
        {
            if (ListFull == true)
            {
                TimerDownloadStart.Stop();
                await Download();
            }
        }
    }
}
