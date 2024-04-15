using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace TrionLibrary
{
    public class Data
    {
        public class Version
        {
            public static async Task<string> GetOnline(string Location)
            {
                using HttpClient client = new HttpClient();
                if (!string.IsNullOrEmpty(Location))
                {
                    HttpResponseMessage response = await client.GetAsync(Location);
                    if (response.IsSuccessStatusCode)
                    {
                        return await response.Content.ReadAsStringAsync();
                    }
                    else
                    {
                        return ($"N/A");
                    }
                }
                else
                {
                    return ($"N/A");
                }
            }
            public static string GetLocal(string Location)
            {
                try
                {
                    if (!string.IsNullOrEmpty(Location))
                    {
                        if (Location.Contains(Settings.WorldExecutableName) || Location.Contains(Settings.LogonExecutableName))
                        {
                            var versionInfo = FileVersionInfo.GetVersionInfo(Location);
                            // Define a regular expression pattern to match dates in yyyy-MM-dd or yyyy/MM/dd format
                            if (versionInfo.FileVersion.Contains("SPP"))
                            {
                                string pattern = @"\b\d{4}[-/]\d{2}[-/]\d{2}\b";
                                // Create a Regex object with the pattern
                                Regex regex = new Regex(pattern);
                                // Find all matches in the text
                                MatchCollection matches = regex.Matches(versionInfo.ToString());
                                // Print each match
                                foreach (Match match in matches)
                                {
                                    return match.Value;
                                }
                            }
                            return "N/A";
                        }
                        else
                        {
                            var versionInfo = FileVersionInfo.GetVersionInfo(Location);
                            return versionInfo.FileVersion;
                        }
                    }
                    else
                    {
                        return "N/A";
                    }
                }
                catch (Exception ex)
                {
                    Data.Message = $@"Failed to get the application version! {ex.Message}";
                    return "N/A";
                }
                
            }
        }
        //Error Message (4 later) dont feel do it now
        public static string Message = string.Empty;
        //Settings File Location
        public static string SettingsDataFile = $@"{Directory.GetCurrentDirectory()}\Settings.xml"; //HardCoded File Location. Maybe one day a dynamic one
        //Global Settings Data
        public static SettingsList Settings = new SettingsList();
        public static string GetExecutableLocation(string location, string Executable)
        {
            //Search for files in a directory and all subdirectories
            if (Executable != null)
            {
                foreach (string f in Directory.EnumerateFiles(location, $"{Executable}.exe", SearchOption.AllDirectories))
                {
                    return f;
                }
            }
            return string.Empty;
        }
        public static void CreateSettingsFile(bool PopulateSettingsData)
        {
            if (!File.Exists(SettingsDataFile))
            {
                var createFile = File.Create(SettingsDataFile); //To fix the "File in use Error!"
                createFile.Close();
            }
            if (PopulateSettingsData == true)
            {
                Settings.WorkingDirectory = Directory.GetCurrentDirectory();
                Settings.WorldDatabase = "world";
                Settings.AuthDatabase = "auth";
                Settings.CharactersDatabase = "characters";
                Settings.MySQLLocation = "";
                Settings.MySQLServerHost = "localhost";
                Settings.MySQLServerUser = "acore";
                Settings.MySQLServerPassword = "acore";
                Settings.MySQLServerPort = "3306";
                Settings.MySQLExecutableName = "mysqld";
                Settings.CoreLocation = "";
                Settings.WorldExecutableLocation = "";
                Settings.LogonExecutableLocation = "";
                Settings.MySQLExecutableLocation = "";
                Settings.WorldExecutableName = "worldserver";
                Settings.LogonExecutableName = "authserver";
                Settings.ServerCrash = false;
                Settings.NotificationSound = true;
                Settings.ConsolHide = false;
                Settings.StayInTray = false;
                Settings.RunWithWindows = false;
                Settings.CustomNames = false;
                Settings.RunServerWithWindows = false;
                Settings.AutoUpdateCore = false;
                Settings.AutoUpdateTrion = true;
                Settings.FirstRun = true;
                Settings.SelectedCore = EnumModels.Cores.AzerothCore;
                WriteData(Settings, SettingsDataFile);
            }
        }
        public static Task SaveSettings()
        {
            try
            {
               if (File.Exists(SettingsDataFile))
                    WriteData(Settings, SettingsDataFile);
                return Task.CompletedTask;
            }
            catch (Exception ex)
            {
                Message = ex.Message;
                throw new Exception(ex.Message);
            }
        }
        public static async Task LoadSettings()
        {
            try
            {
                if (File.Exists(SettingsDataFile))
                {
                    Settings = ReaderData(SettingsDataFile);
                }
                else
                {
                    CreateSettingsFile(true);
                    Settings = ReaderData(SettingsDataFile);
                }
            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }
            await Task.CompletedTask;
        }
        private static void WriteData(object o, string fileName)
        {
            XmlSerializer serializer = new XmlSerializer(o.GetType());
            TextWriter writer = new StreamWriter(fileName);
            serializer.Serialize(writer, o);
            writer.Close();
        }
        private static SettingsList ReaderData(string fileName)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(SettingsList));
            FileStream reader = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read);
            Settings = (SettingsList)serializer.Deserialize(reader);
            reader.Close();
            return Settings;
        }
        public static void CreateMySQLConfigFile(string Location)
        {
            string ConfigFile = $@"{Location}\my.ini";
            string DirectoryMYSQL = Settings.MySQLLocation.Replace(@"\","/");
            
            if (!File.Exists(ConfigFile))
            {
                var createFile = File.Create(ConfigFile);
                createFile.Close();
                List<string> lines = File.ReadAllLines(ConfigFile).ToList();
                lines.Add("[client]");
                lines.Add("port=3306");
                lines.Add("default-character-set = utf8mb4");
                lines.Add("socket=/tmp/mysql.sock");
                lines.Add("");
                lines.Add("[mysqld]");
                lines.Add("port=3306");
                lines.Add("socket=/tmp/mysql.sock");
                lines.Add("key_buffer_size=16M");
                lines.Add("max_allowed_packet=1G");
                lines.Add($"basedir={DirectoryMYSQL}");
                lines.Add($"datadir={DirectoryMYSQL}data");
                lines.Add("");
                lines.Add("[mysqldump]");
                lines.Add("quick");
                lines.Add("max_allowed_packet = 512M");
                lines.Add("");
                File.WriteAllLines(ConfigFile, lines);
            }
        }
    }
    public class SettingsList
    {  
        public string WorkingDirectory;
        public string WorldDatabase;
        public string AuthDatabase;
        public string CharactersDatabase;
        public string MySQLLocation;
        public string MySQLServerHost;
        public string MySQLServerUser;
        public string MySQLServerPassword;
        public string MySQLServerPort;
        public string MySQLExecutableName;
        public string MySQLExecutablePath;
        public string CoreLocation;
        public string MySQLExecutableLocation;
        public string WorldExecutableLocation;
        public string LogonExecutableLocation;
        public string WorldExecutableName;
        public string LogonExecutableName;
        public bool AutoUpdateCore;
        public bool AutoUpdateTrion;
        public bool AutoUpdateMySQL;
        public bool ServerCrash;
        public bool NotificationSound;
        public bool ConsolHide;
        public bool StayInTray;
        public bool RunWithWindows;
        public bool CustomNames;
        public bool RunServerWithWindows;
        public bool FirstRun;
        public EnumModels.Cores SelectedCore;
    }

}
