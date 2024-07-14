using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TrionLibrary.Crypto
{
    public class FileHash
    {
        // Structure to hold file information
        public class FileInfo
        {
            public string FileName { get; set; }
            public string FileFullName { get; set; }
            public string FileHash { get; set; }
        }
        // Function to calculate SHA-256 hash of a file
        public static string CalculateSHA256(string filePath)
        {
            using (var sha256 = SHA256.Create())
            {
                using (var stream = File.OpenRead(filePath))
                {
                    return BitConverter.ToString(sha256.ComputeHash(stream)).Replace("-", String.Empty);
                }
            }
        }
        // Function to recursively get all files in a folder and its subfolders
        public static IEnumerable<string> GetAllFiles(string path)
        {
            var files = Directory.GetFiles(path);
            foreach (var file in files)
                yield return file;

            var dirs = Directory.GetDirectories(path);
            foreach (var dir in dirs)
            {
                foreach (var file in GetAllFiles(dir))
                    yield return file;
            }
        }
        // Function to generate XML file containing file information
        private static void ExportToXML(IEnumerable<FileInfo> fileInfos, string xmlFilePath)
        {
            var xml = new XElement("Files",
                            from fileInfo in fileInfos
                            select new XElement("File",
                                        new XElement("FileName", fileInfo.FileName),
                                        new XElement("FileFullName", fileInfo.FileFullName),
                                        new XElement("FileHash", fileInfo.FileHash)
                            )
                        );
            xml.Save(xmlFilePath);
        }
        // Function to compare file hashes and export changes to XML Offline
        public static void CompareAndExportChangesOffline(string folderPath, string previousXmlFilePath, string currentXmlFilePath)
        {
            var previousFileInfos = new List<FileInfo>();

            // If previous XML file exists, load its content
            if (File.Exists(previousXmlFilePath))
            {
                var previousXml = XDocument.Load(previousXmlFilePath);
                previousFileInfos = (from file in previousXml.Root!.Elements("File")
                                     select new FileInfo
                                     {
                                         FileName = file.Element("FileName")!.Value,
                                         FileFullName = file.Element("FileFullName")!.Value,
                                         FileHash = file.Element("FileHash")!.Value
                                     }).ToList();
            }

            var currentFileInfos = new List<FileInfo>();

            var allFiles = GetAllFiles(folderPath).ToList();
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
                    FileHash = CalculateSHA256(file)
                };
                currentFileInfos.Add(fileInfo);

                currentFileIndex++;

                // Calculate progress percentage
                double progressPercentage = (double)currentFileIndex / totalFiles * 100;

                // Report progress
                Console.WriteLine($"Processing file: {fileInfo.FileName} ({currentFileIndex}/{totalFiles}) - {progressPercentage:F2}% completed");
            }

            // Export current file information to XML
            ExportToXML(currentFileInfos, currentXmlFilePath);

            // Identify missing files (present in previous XML but not in current folder)
            var missingFiles = previousFileInfos.Where(previous => !currentFileInfos.Any(current => current.FileFullName == previous.FileFullName));

            // Compare current file hashes with previous ones and export changes to XML
            var changedFiles = currentFileInfos.Where(current => !previousFileInfos.Any(previous => previous.FileFullName == current.FileFullName && previous.FileHash == current.FileHash));

            // Combine missing files and changed files
            var allChangedFiles = missingFiles.Concat(changedFiles);

            // Export all changes to XML
            ExportToXML(allChangedFiles, currentXmlFilePath.Replace(".xml", "_changes.xml"));
        }
        // Function to compare file hashes and export changes to XML Online
        public static async Task CompareAndExportChangesOnline(string folderPath, string previousXmlUrl, string currentXmlFilePath)
        {
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

            var allFiles = GetAllFiles(folderPath).ToList();
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
                    FileHash = CalculateSHA256(file)
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
               
            }

        }
        // Function to export file hashes to XML
        public static void ExportFileHashesToXML(string folderPath, string xmlFilePath)
        {
            var fileInfos = new List<FileInfo>();
            var allFiles = GetAllFiles(folderPath).ToList();
            int totalFiles = allFiles.Count;
            int currentFileIndex = 0;

            // Calculate SHA-256 hashes for all files in the folder
            foreach (var file in allFiles)
            {
                string _fileName = Path.GetFileName(file);
                var fileInfo = new FileInfo
                {
                    FileName = _fileName,
                    FileFullName = file.Replace(@"\", "/"),
                    FileHash = CalculateSHA256(file)
                };
                fileInfos.Add(fileInfo);

                currentFileIndex++;

                // Calculate progress percentage
                double progressPercentage = (double)currentFileIndex / totalFiles * 100;

                // Report progress
                Console.WriteLine($"{progressPercentage:F2}% Processing file: {fileInfo.FileName} ({currentFileIndex}/{totalFiles}) - completed");
            }
            // Export file information to XML
            ExportToXML(fileInfos, xmlFilePath + "FileHashes.xml");
        }
    }
}