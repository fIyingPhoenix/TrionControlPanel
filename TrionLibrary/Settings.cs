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
            public static string GetLocal(string Location , string ExecName)
            {
                try
                {
                    if (Data.Settings.SelectedCore == EnumModels.Cores.AzerothCore)
                    {
                        if (!string.IsNullOrEmpty(Location))
                        {
                            if (Location.Contains(ExecName))
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
                                    foreach (Match match in matches.Cast<Match>())
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
                    else { return "N/A"; }
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
                Settings.CustomWorkingDirectory = Directory.GetCurrentDirectory();
                Settings.WorldDatabase = "acore_world";
                Settings.AuthDatabase = "acore_auth";
                Settings.CharactersDatabase = "acore_characters";
                Settings.DBExeLoca = "";
                Settings.DBLocation = "";
                Settings.DBServerHost = "localhost";
                Settings.DBServerUser = "acore";
                Settings.DBServerPassword = "acore";
                Settings.DBServerPort = "3306";
                Settings.DBExecutableName = "mysqld";
                Settings.CustomWorkingDirectory = "";
                Settings.CustomWorldExeLoc = "";
                Settings.CustomLogonExeLoc = "";
                Settings.CustomWorldExeName = "worldserver";
                Settings.CustomLogonExeName = "authserver";
                Settings.DDNSDomain = "";
                Settings.DDNSInterval = 1000;
                Settings.DDNSUsername = "";
                Settings.DDNSPassword = "";
                Settings.IPAddress = "0.0.0.0";
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
                Settings.DDNSRunOnStartup = false;
                Settings.ClassicInstalled= false;
                Settings.TBCInstalled =false;
                Settings.WotLKInstalled = false;
                Settings.CataInstalled = false;
                Settings.MOPInstalled = false;
                Settings.SelectedCore = EnumModels.Cores.AzerothCore;
                Settings.DDNSerivce = EnumModels.DDNSerivce.DuckDNS;
                Settings.SelectedSPP = EnumModels.SPP.WrathOfTheLichKing;
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
            string DirectoryMYSQL = Settings.DBLocation.Replace(@"\","/");
            
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
                lines.Add("key_buffer_size=256M");
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
        //Database Names
        public string WorldDatabase;
        public string AuthDatabase;
        public string CharactersDatabase;
        public string HotfixDatabase;
        //Dataabase Settings
        public string DBExeLoca;
        public string DBLocation;
        public string DBServerHost;
        public string DBServerUser;
        public string DBServerPassword;
        public string DBServerPort;
        public string DBExecutableName;
        public string DBExecutablePath;
        //Custom Cores
        public string CustomWorkingDirectory;
        public string CustomWorldExeLoc;
        public string CustomLogonExeLoc;
        public string CustomLogonExeName;
        public string CustomWorldExeName;
        public bool CustomInstalled;
        //Classic Core
        public string ClassicWorkingDirectory;
        public string ClassicDBExeLoca;
        public string ClassicWorldExeLoc;
        public string ClassicLogonExeLoc;
        public string ClassicLogonExeName;
        public string ClassicWorldExeName;
        public bool ClassicInstalled;
        //TBC Core
        public string TBCWorkingDirectory;
        public string TBCDBExeLoca;
        public string TBCWorldExeLoc;
        public string TBCLogonExeLoc;
        public string TBCLogonExeName;
        public string TBCWorldExeName;
        public bool TBCInstalled;
        //WotLK Core
        public string WotLKWorkingDirectory;
        public string WotLKDBExeLoca;
        public string WotLKWorldExeLoc;
        public string WotLKLogonExeLoc;
        public string WotLKLogonExeName;
        public string WotLKWorldExeName;
        public bool WotLKInstalled;
        //Cata Core
        public string CataWorkingDirectory;
        public string CataDBExeLoca;
        public string CataWorldExeLoc;
        public string CataLogonExeLoc;
        public string CataLogonExeName;
        public string CataWorldExeName;
        public bool CataInstalled;
        //Mop Core
        public string MopWorkingDirectory;
        public string MopDBExeLoca;
        public string MopWorldExeLoc;
        public string MopLogonExeLoc;
        public string MopLogonExeName;
        public string MopWorldExeName;
        public bool MOPInstalled;
        //DDNS Settings
        public string DDNSDomain;
        public string DDNSUsername;
        public string DDNSPassword;
        public string IPAddress;
        //Trion
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
        public bool DDNSRunOnStartup;
        public int DDNSInterval;
        public EnumModels.Cores SelectedCore;
        public EnumModels.DDNSerivce DDNSerivce;
        public EnumModels.SPP SelectedSPP;
    }

}
