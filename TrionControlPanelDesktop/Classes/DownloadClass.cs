using System.Xml.Linq;
using TrionControlPanelDesktop.Controls;
using TrionControlPanelDesktop.FormData;
using TrionCryptography;

namespace TrionControlPanelDesktop.Classes
{

    internal class DownloadClass
    {
        public class FileInfo
        {
            public string FileName { get; set; }
            public string FileFullName { get; set; }
            public string FileHash { get; set; }
        }
        // Function to compare file hashes and export changes to XML Online
        public static async Task CompareAndExportChangesOnline(string folderPath, string previousXmlUrl)
        {
            DownloadControl.DownloadList.Clear();

           var previousFileInfos = new List<FileInfo>();

            // Load previous XML file from the web
            using (var httpClient = new HttpClient())
            {
                var xmlContent = await httpClient.GetStringAsync(previousXmlUrl);
                var previousXml = XDocument.Parse(xmlContent);
                previousFileInfos = (from file in previousXml.Root!.Elements("File")
                                     select new FileInfo
                                     {
                                         FileName = file.Element("FileName")!.Value,
                                         FileFullName = file.Element("FileFullName")!.Value,
                                         FileHash = file.Element("FileHash")!.Value
                                     }).ToList();
            }

            var currentFileInfos = new List<FileInfo>();

            var allFiles = FileHashComparer.GetAllFiles(folderPath).ToList();
            int totalFiles = allFiles.Count;
            int currentFileIndex = 0;

            // Calculate SHA-256 hashes for all files in the folder
            foreach (var file in allFiles)
            {
                string _fileName = Path.GetFileName(file);
                var fileInfo = new FileInfo
                {
                    FileName = _fileName.Replace(@"\", "/"),
                    FileFullName = file,
                    FileHash = FileHashComparer.CalculateSHA256(file)
                };
                currentFileInfos.Add(fileInfo);

                currentFileIndex++;

                // Calculate progress percentage
                double progressPercentage = (double)currentFileIndex / totalFiles * 100;
            }

            // Identify missing files (present in previous XML but not in current folder)
            var missingFiles = previousFileInfos.Where(previous => !currentFileInfos.Any(current => current.FileFullName == previous.FileFullName));

            // Compare current file hashes with previous ones and export changes to XML
            var changedFiles = currentFileInfos.Where(current => !previousFileInfos.Any(previous => previous.FileFullName == current.FileFullName && previous.FileHash == current.FileHash));

            // Combine missing files and changed files
            var allChangedFiles = missingFiles.Concat(changedFiles);

            // Export all changes to List
            foreach (var file in allChangedFiles)
            {

                DownloadControl.DownloadList.Add(
                    new UrlData()
                    {
                        FileName = file.FileName,
                        FileFullName = file.FileFullName,
                        FileHash =  file.FileHash
                    }
                    );

                User.UI.Download.CurrentDownloads++;
            }
            
            MessageBox.Show("it Runs");
            DownloadControl.ListFull = true;
            MainForm.LoadDownload = true;
        }
    }
}
