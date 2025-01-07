using Newtonsoft.Json;
using TrionControlPanel.Desktop.Extensions.Modules.Lists;
using TrionControlPanelDesktop.Extensions.Modules;

namespace TrionControlPanelDesktop.Extensions.Classes
{
    public class Settings
    {
        public static void SaveSettings(AppSettings settings, string filePath)
        {
            string json = JsonConvert.SerializeObject(settings, Formatting.Indented);
            File.WriteAllText(filePath, json);
        }
        public static AppSettings LoadSettings(string filePath)
        {
            if (!File.Exists(filePath))
                return new AppSettings(); // Return default settings if the file doesn't exist

            string json = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<AppSettings>(json)!;
        }
        public static void CreatSettings(string filePath)
        {
            // Create custom settings with desired initial values
            var customSettings = new AppSettings
            {
                //Database Names
                WorldDatabase = "wotlk_world",
                AuthDatabase = "wotlk_auth",
                CharactersDatabase = "wotlk_characters",
                HotfixDatabase = "N/A",
                //Dataabase Settings
                DBExeLoc = "N/A",
                DBWorkingDir = "N/A",
                DBLocation = "N/A",
                DBServerHost = "localhost",
                DBServerUser = "phoenix",
                DBServerPassword = "phoenix",
                DBServerPort = "3306",
                DBExeName = "mysqld",
                DBInstalled = false,
                //Custom Cores
                CustomWorkingDirectory = "",
                CustomWorldExeLoc = "",
                CustomLogonExeLoc = "",
                CustomLogonExeName = "worldserver",
                CustomWorldExeName = "authserver",
                LaunchCustomCore = false,
                CustomInstalled = false,
                //Classic Core
                ClassicWorkingDirectory = "",
                ClassicWorldExeLoc = "",
                ClassicLogonExeLoc = "",
                ClassicLogonExeName = "",
                ClassicWorldExeName = "",
                ClassicWorldName = "",
                ClassicLogonName = "",
                LaunchClassicCore = false,
                ClassicInstalled = false,
                //TBC Core
                TBCWorkingDirectory = "",
                TBCWorldExeLoc = "",
                TBCLogonExeLoc = "",
                TBCLogonExeName = "",
                TBCWorldExeName = "",
                TBCWorldName = "",
                TBCLogonName = "",
                LaunchTBCCore = false,
                TBCInstalled = false,
                //WotLK Core
                WotLKWorkingDirectory = "",
                WotLKWorldExeLoc = "",
                WotLKLogonExeLoc = "",
                WotLKLogonExeName = "",
                WotLKWorldExeName = "",
                WotLKWorldName = "",
                WotLKLogonName = "",
                LaunchWotLKCore = false,
                WotLKInstalled = false,
                //Cata Core
                CataWorkingDirectory = "",
                CataWorldExeLoc = "",
                CataLogonExeLoc = "",
                CataLogonExeName = "",
                CataWorldExeName = "",
                CataWorldName = "",
                CataLogonName = "",
                LaunchCataCore = false,
                CataInstalled = false,
                //Mop Core
                MopWorkingDirectory = "",
                MopWorldExeLoc = "",
                MopLogonExeLoc = "",
                MopLogonExeName = "",
                MopWorldExeName = "",
                MoPWorldName = "",
                MoPLogonName = "",
                LaunchMoPCore = false,
                MOPInstalled = false,
                //DDNS Settings
                DDNSDomain = "N/A",
                DDNSUsername = "N/A",
                DDNSPassword = "N/A",
                DDNSInterval = 1000,
                IPAddress = "",
                //Trion
                TrionTheme = Enums.TrionTheme.TrionBlue,
                TrionLanguage = "en",
                AutoUpdateCore = true,
                AutoUpdateTrion = true,
                AutoUpdateDatabaseL = true,
                NotificationSound = false,
                ConsolHide = false,
                StayInTray = false,
                RunWithWindows = false,
                CustomNames = false,
                RunServerWithWindows = false,
                FirstRun = false,
                DDNSRunOnStartup = false,
                ServerCrashDetection = false,
                SelectedCore = Enums.Cores.AzerothCore,
                DDNSerivce = Enums.DDNSerivce.DuckDNS,
                SelectedSPP = Enums.SPP.WrathOfTheLichKing,
                SelectedDatabases = Enums.Databases.WotLK,
            };

            var json = JsonConvert.SerializeObject(customSettings, Formatting.Indented);
            // Using statement ensures that the file is closed properly after writing
            using (var writer = new StreamWriter(filePath, false))
            {
                writer.Write(json);
            }
        }
        public static void CreateMySQLConfigFile(string Location, string DatabaseDirectory)
        {
            string configFile = $@"{Location}\my.ini";

            if (!File.Exists(configFile))
            {
                var lines = new List<string>
                {
                    "[client]",
                    "port=3306",
                    "default-character-set = utf8mb4",
                    @"socket=""\tmp\mysql.sock""",
                    "",
                    "[mysqld]",
                    "",
                    "# Basic Settings",
                    $@"basedir=""{DatabaseDirectory}""",
                    $@"datadir=""{DatabaseDirectory}\data""",
                    "bind-address=0.0.0.0",
                    "port=3306",
                    "skip_name_resolve=1",
                    "",
                    "# Memory Allocation",
                    "innodb_buffer_pool_size=3G",
                    "innodb_log_file_size=1G",
                    "innodb_log_buffer_size=64M",
                     "",
                    "# Threading",
                    "innodb_thread_concurrency=4",
                    "innodb_read_io_threads=4",
                    "innodb_write_io_threads=4",
                    "thread_cache_size=2000",
                    "",
                    "# Connections",
                    "max_connections=10000",
                    "max_allowed_packet=1G",
                    "table_open_cache=20000",
                    "",
                    "# General settings",
                    "default-storage-engine=INNODB",
                    "",
                    "# Log settings",
                    $@"log_error=""{DatabaseDirectory}\logs\mysql_error.log""",
                    $@"slow_query_log_file=""{DatabaseDirectory}\logs\mysql_slow.log""",
                    "slow_query_log=1",
                    "long_query_time=2",
                    "",
                    "# Table and Index Cache",
                    "table_open_cache=8192",
                    "table_open_cache_instances=16",
                    "# Networking",
                    "net_buffer_length=64K",
                    "",
                    "# Console output",
                    "general_log=1",
                    $@"general_log_file=""{DatabaseDirectory}\logs\mysql_general.log""",
                    "",
                    "[mysqld_safe]",
                    "# Increase open files limit",
                    "open-files-limit=65535"
                };
            }
        }
    }
}