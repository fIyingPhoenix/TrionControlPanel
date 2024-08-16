using System.Diagnostics;
using System.IO.Compression;
using System.Xml.Linq;
using TrionControlPanelDesktop.Data;
using TrionLibrary.Crypto;
using TrionLibrary.Sys;
using TrionLibrary.Models;
using TrionControlPanelDesktop.Download;
using TrionLibrary.Setting;
using TrionLibrary.Network;


namespace TrionControlPanelDesktop.Controls
{
    public partial class DownloadControl : UserControl
    {
        public static bool StartDownload { get; set; }
        public static string XmlUrlp { get; set; }
        public static string DownloadLocation { get; set; }
        public static string Title { get; set; }
        public static int CurrentDownloadCount { get; set; }

        private int TotalDownloads = 0;
        private int CurrentDownload = 0;
        //
        public static List<Lists.File> DownlaodList = [];
        public DownloadControl()
        {
            Dock = DockStyle.Fill;
            InitializeComponent();
            CheckServers();
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
            CheckServers();
        }
        private void DownloadControl_Load(object sender, EventArgs e)
        {
            LBLTitle.Text = Title;
        }
        public static string StringBuilder(string input, string WordlToReplace, string WordlToReplaceWith)
        {
            return input.Replace(WordlToReplace, WordlToReplaceWith);
        }
        public static async Task CompareAndExportChangesOnline(string folderPath, string previousXmlUrl, IProgress<string> progress, bool startDownload)
        {
            string LogString;
            var previousFileInfos = new List<Lists.File>();
            LogString = "Connecting to Server..";
            progress.Report(LogString);
            // Load previous XML file from the web
            using (var httpClient = new HttpClient())
            {
                LogString = "Reading Server Files..";
                progress.Report(LogString);
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
            LogString = "Reading local Files..";
            progress.Report(LogString);
            var allFiles = FileHash.GetAllFiles(folderPath).ToList();
            // Calculate SHA-256 hashes for all files in the folder
            LogString = "Calculating files hash..";
            progress.Report(LogString);
            int currentFile = 0;
            foreach (var file in allFiles)
            {
                currentFile++;
                var fileLocation = StringBuilder(file, $"{Directory.GetCurrentDirectory()}", "").Replace(@"/", "");
                string _fileName = Path.GetFileName(file);
                var fileInfo = new Lists.File
                {
                    FileName = _fileName,
                    FileFullName = fileLocation.Replace(@"\", "/"),
                    FileHash = await FileHash.CalculateSHA256(file)
                };
                
                currentFileInfos.Add(fileInfo);      
            }
            // Identify missing files (present in previous XML but not in current folder) 
            var missingFiles = previousFileInfos.Where(previous => !currentFileInfos.Any(current => current.FileName== previous.FileName && current.FileFullName == previous.FileFullName && current.FileHash == previous.FileHash));
            LogString = "Compare Files..";
            progress.Report(LogString);
            // Compare current file hashes with previous ones and export changes to XML Maybe one day for a better repair!
            //var changedFiles = currentFileInfos.Where(current => !previousFileInfos.Any(previous => previous.FileName == current.FileName && previous.FileFullName == current.FileFullName && previous.FileHash == current.FileHash));
            LogString = "Add changes to download list....";
            progress.Report(LogString);
            // Combine missing files and changed files
            //var allChangedFiles = missingFiles.Concat(changedFiles);
            var fullList = new List<Lists.File>();
            foreach (var file in missingFiles)
            {
                DownlaodList.Add(
                  new Lists.File()
                  {
                      FileName = file.FileName,
                      FileFullName = file.FileFullName,
                      FileHash = file.FileHash
                  }
                  );         
            }
            LogString = $"Finish! Added files {DownlaodList.Count()}. Starting Download!";
            progress.Report(LogString);
            previousFileInfos.Clear();
            currentFileInfos.Clear();
            missingFiles = null;
            StartDownload = startDownload;
        }
        private async Task DownloadAsync(List<Lists.File> DownloadList)
        {
            if (DownloadList.Count != 0)
            {
                CurrentDownloadCount = DownloadList.Count;
                using (HttpClient client = new())
                {
                    foreach (var url in DownloadList)
                    {
                        TotalDownloads = DownloadList.Count;
                        // Update Downloaded URLs
                        CurrentDownloadCount--;
                        CurrentDownload++;
                        try
                        {
                            string DownloadLinkg = @$"{Links.MainCDNHost}{url.FileFullName}";
                            // Send GET request to the server
                            using (HttpResponseMessage response = await client.GetAsync(DownloadLinkg, HttpCompletionOption.ResponseHeadersRead))
                            {
                                // Update Label
                                LBLQueue.Text = $"Queue: {CurrentDownload} / {TotalDownloads}";
                                // Check if request was successful
                                response.EnsureSuccessStatusCode();
                                var FullDirecotry = $"{Directory.GetCurrentDirectory()}/{url.FileFullName}".Replace("/", @"\");
                                string directoryPath = Path.GetDirectoryName(FullDirecotry)!;
                                if (!Directory.Exists(directoryPath)) { Directory.CreateDirectory(directoryPath); }

                                LBLDownloadName.Text = @$"Name: {url.FileName}";
                                // Create a file stream to write the downloaded content
                                using (FileStream fileStream = new(FullDirecotry, FileMode.Create, FileAccess.Write, FileShare.None))
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
                        }
                    }
                }  
            } 
            DownlaodList.Clear();
            InstallFinished();
            DownloadData.Infos.Install.Classic = false;
            DownloadData.Infos.Install.TBC = false;
            DownloadData.Infos.Install.WotLK = false;
            DownloadData.Infos.Install.Cata = false;
            DownloadData.Infos.Install.Mop = false;
            DownloadData.Infos.Install.Database = false;
        }
        private async void TimerDownloadStart_Tick(object sender, EventArgs e)
        {
            if (StartDownload)
            {
                StartDownload = false;
                await DownloadAsync(DownlaodList);
            }
        }
        private static void InstallFinished()
        {
            if (DownloadData.Infos.Install.Classic)
            {
                string classic = Links.Install.Classic.Replace("/", @"\");
                Setting.List.ClassicInstalled = true;
                Setting.List.ClassicWorkingDirectory = classic;
                Setting.List.ClassicLogonExeLoc = Infos.GetExecutableLocation(classic, "realmd");
                Setting.List.ClassicLogonExeName = "realmd";
                Setting.List.ClassicWorldExeLoc = Infos.GetExecutableLocation(classic, "mangosd");
                Setting.List.ClassicWorldExeName = "mangosd";
                Setting.List.ClassicLogonName = "WoW Classic Logon";
                Setting.List.ClassicWorldName = "WoW Classic World";
            }
            if (DownloadData.Infos.Install.TBC)
            {
                string TBC = Links.Install.TBC.Replace("/", @"\");
                Setting.List.TBCInstalled = true;
                Setting.List.TBCWorkingDirectory = TBC;
                Setting.List.TBCLogonExeLoc = Infos.GetExecutableLocation(TBC, "realmd");
                Setting.List.TBCLogonExeName = "realmd";
                Setting.List.TBCWorldExeLoc = Infos.GetExecutableLocation(TBC, "mangosd");
                Setting.List.TBCWorldExeName = "mangosd";
                Setting.List.TBCLogonName = "The Burning Crusade Logon";
                Setting.List.TBCWorldName = "The Burning Crusade World";
            }
            if (DownloadData.Infos.Install.WotLK)
            {
                string WotLK = Links.Install.WotLK.Replace("/", @"\");
                Setting.List.WotLKInstalled = true;
                Setting.List.WotLKWorkingDirectory = WotLK;
                Setting.List.WotLKLogonExeLoc = Infos.GetExecutableLocation(WotLK, "authserver");
                Setting.List.WotLKLogonExeName = "authserver";
                Setting.List.WotLKWorldExeLoc = Infos.GetExecutableLocation(WotLK, "worldserver");
                Setting.List.WotLKLogonName = "Wrath of the Lich King Logon";
                Setting.List.WotLKWorldName = "Wrath of the Lich King World";
            }
            if (DownloadData.Infos.Install.Cata)
            {
                string cata = Links.Install.Cata.Replace("/", @"\");
                Setting.List.CataInstalled = true;
                Setting.List.CataWorkingDirectory = cata;
                Setting.List.CataLogonExeLoc = Infos.GetExecutableLocation(cata, "authserver");
                Setting.List.CataLogonExeName = "authserver";
                Setting.List.CataWorldExeLoc = Infos.GetExecutableLocation(cata, "worldserver"); 
                Setting.List.CataWorldExeName = "worldserver";
                Setting.List.CataLogonName = "Cataclysm Logon";
                Setting.List.CataWorldName = "Cataclysm World";
            }
            if (DownloadData.Infos.Install.Mop)
            {
                string Mop = Links.Install.Mop.Replace("/", @"\");
                Setting.List.MOPInstalled = true;
                Setting.List.MopWorkingDirectory = Mop;
                Setting.List.MopLogonExeLoc = Infos.GetExecutableLocation(Mop, "authserver");
                Setting.List.MopLogonExeName = "authserver";
                Setting.List.MopWorldExeLoc = Infos.GetExecutableLocation(Mop, "authserver");
                Setting.List.MopWorldExeName = "worldserver";
                Setting.List.MoPLogonName = "Mists of Pandaria Logon";
                Setting.List.MoPWorldName = "Mists of Pandaria World";
            }
            if (DownloadData.Infos.Install.Database == true)
            {
                Setting.CreateMySQLConfigFile(Directory.GetCurrentDirectory());
                string Database = Links.Install.Database.Replace("/", @"\");
                Setting.List.DBInstalled = true;
                Setting.List.DBLocation = $@"{Database}";
                Setting.List.DBWorkingDir = $@"{Database}\bin";
                Setting.List.DBExeLoc = Infos.GetExecutableLocation($@"{Database}\bin", "mysqld");
                Setting.List.DBExeName = "mysqld";
                string SQLLocation = $@"{Database}\extra\initDatabase.sql";
               _ = Watcher.ApplicationStart(Setting.List.DBExeLoc, Setting.List.DBWorkingDir, "Initialize MySQL", false, $"--initialize-insecure --init-file=\"{SQLLocation}\" --console");
            }
            Setting.Save();
            SettingsControl.RefreshData = true; 
        }
        private async void CheckServers()
        {
            if (await Helper.IsWebsiteOnlineAsync(Links.MainCDNHost))
            {
                PBoxMainCDN.Image = Properties.Resources.cloud_online_50;
            }
            else
            {
                PBoxMainCDN.Image = Properties.Resources.cloud_offline_50;
            }
            if (await Helper.IsWebsiteOnlineAsync(Links.BackupCDNHost))
            {
                PBoxBackupCDN.Image = Properties.Resources.cloud_online_50;
            }
            else
            {
                PBoxBackupCDN.Image = Properties.Resources.cloud_offline_50;
            }
            if (await Helper.IsWebsiteOnlineAsync(Links.WebServer))
            {
                PBoxWebServer.Image = Properties.Resources.cloud_online_50;
            }
            else
            {
                PBoxWebServer.Image = Properties.Resources.cloud_offline_50;
            }
            if (await Helper.IsWebsiteOnlineAsync(Links.APIServer))
            {
                PBoxAPIServer.Image = Properties.Resources.cloud_online_50;
            }
            else
            {
                PBoxAPIServer.Image = Properties.Resources.cloud_offline_50;
            }
        }
    }
}
