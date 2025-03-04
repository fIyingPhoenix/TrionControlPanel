using MaterialSkin;
using Newtonsoft.Json;
using TrionControlPanel.Desktop.Extensions.Modules;
using TrionControlPanel.Desktop.Extensions.Modules.Lists;

namespace TrionControlPanel.Desktop.Extensions.Classes
{
    public class Settings
    {
        // Serializes the AppSettings object to a JSON file asynchronously
        public static async Task SaveSettings(AppSettings settings, string filePath)
        {
            string json = JsonConvert.SerializeObject(settings, Formatting.Indented); // Serialize settings to JSON
            await File.WriteAllTextAsync(filePath, json); // Write JSON to file asynchronously
        }

        // Loads the AppSettings object from a JSON file. Returns default settings if file doesn't exist
        public static AppSettings LoadSettings(string filePath)
        {
            if (!File.Exists(filePath))
                return new AppSettings(); // Return default settings if file doesn't exist

            string json = File.ReadAllText(filePath); // Read file contents
            return JsonConvert.DeserializeObject<AppSettings>(json)!; // Deserialize JSON into AppSettings object
        }

        // Creates a new settings file with predefined values for application settings
        public static void CreatSettings(string filePath)
        {
            // Create custom settings with default values
            var customSettings = new AppSettings
            {
                // Database Names
                WorldDatabase = "wotlk_world",
                AuthDatabase = "wotlk_auth",
                CharactersDatabase = "wotlk_characters",
                HotfixDatabase = "",
                // Database Settings
                DBExeLoc = "",
                DBWorkingDir = "",
                DBLocation = "",
                DBServerHost = "localhost",
                DBServerUser = "phoenix",
                DBServerPassword = "phoenix",
                DBServerPort = "3306",
                DBExeName = "mysqld",
                DBInstalled = false,
                // Custom Cores
                CustomWorkingDirectory = "",
                CustomWorldExeLoc = "",
                CustomLogonExeLoc = "",
                CustomLogonExeName = "worldserver",
                CustomWorldExeName = "authserver",
                LaunchCustomCore = false,
                CustomInstalled = false,
                // Classic Core
                ClassicWorkingDirectory = "",
                ClassicWorldExeLoc = "",
                ClassicLogonExeLoc = "",
                ClassicLogonExeName = "",
                ClassicWorldExeName = "",
                ClassicWorldName = "",
                ClassicLogonName = "",
                LaunchClassicCore = false,
                ClassicInstalled = false,
                // TBC Core
                TBCWorkingDirectory = "",
                TBCWorldExeLoc = "",
                TBCLogonExeLoc = "",
                TBCLogonExeName = "",
                TBCWorldExeName = "",
                TBCWorldName = "",
                TBCLogonName = "",
                LaunchTBCCore = false,
                TBCInstalled = false,
                // WotLK Core
                WotLKWorkingDirectory = "",
                WotLKWorldExeLoc = "",
                WotLKLogonExeLoc = "",
                WotLKLogonExeName = "",
                WotLKWorldExeName = "",
                WotLKWorldName = "",
                WotLKLogonName = "",
                LaunchWotLKCore = false,
                WotLKInstalled = false,
                // Cata Core
                CataWorkingDirectory = "",
                CataWorldExeLoc = "",
                CataLogonExeLoc = "",
                CataLogonExeName = "",
                CataWorldExeName = "",
                CataWorldName = "",
                CataLogonName = "",
                LaunchCataCore = false,
                CataInstalled = false,
                // Mop Core
                MopWorkingDirectory = "",
                MopWorldExeLoc = "",
                MopLogonExeLoc = "",
                MopLogonExeName = "",
                MopWorldExeName = "",
                MoPWorldName = "",
                MoPLogonName = "",
                LaunchMoPCore = false,
                MOPInstalled = false,
                // DDNS Settings
                DDNSDomain = "",
                DDNSUsername = "",
                DDNSPassword = "",
                DDNSInterval = 1000,
                IPAddress = "",
                // Trion Settings
                TrionTheme = Enums.TrionTheme.TrionBlue,
                TrionLanguage = "enUS",
                AutoUpdateCore = true,
                AutoUpdateTrion = true,
                AutoUpdateDatabase = true,
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
                DDNSerivce = Enums.DDNSService.DuckDNS,
                SelectedSPP = Enums.SPP.WrathOfTheLichKing,
                SelectedDatabases = Enums.Databases.WotLK,
            };

            // Serialize custom settings to JSON
            var json = JsonConvert.SerializeObject(customSettings, Formatting.Indented);
            // Write settings to the file
            using (var writer = new StreamWriter(filePath, false))
            {
                writer.Write(json); // Write JSON data to file
            }
        }

        // Creates a MySQL configuration file with predefined settings
        public static void CreateMySQLConfigFile(string Location, string DatabaseDirectory)
        {
            var NewDatabaseDirectory = DatabaseDirectory.Replace(@"\", "/");
            string configFile = $@"{Location.Replace(@"\", "/")}/my.ini"; // Path to the MySQL configuration file
            if (File.Exists(configFile))File.Delete(configFile);
            // Check if the configuration file already exists
            if (!File.Exists(configFile))
            {
                List<string> list = new()
                {
                    "[client]",
                    "port=3306",
                    "default-character-set = utf8mb4",
                    $@"socket={NewDatabaseDirectory}/tmp/mysql.sock",
                    "",
                    "[mysqld]",
                    "",
                    "# Basic Settings",
                    $@"basedir=""{NewDatabaseDirectory}",
                    $@"datadir=""{NewDatabaseDirectory}/data""",
                    "bind-address=0.0.0.0",
                    "port=3306",
                    "",
                    "# Memory Allocation",
                    "innodb_buffer_pool_size=3G",
                    "innodb_log_file_size=1G",
                    "innodb_log_buffer_size=64M",
                    "",
                    "# Threading",
                    "innodb_thread_concurrency=4",
                    "innodb_read_io_threads=2",
                    "innodb_write_io_threads=2",
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
                    $@"log_error=""{NewDatabaseDirectory}/logs/mysql_error.log""",
                    $@"slow_query_log_file=""{NewDatabaseDirectory}/logs/mysql_slow.log""",
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
                    $@"general_log_file=""{NewDatabaseDirectory}/logs/mysql_general.log""",
                    "",
                    "[mysqld_safe]",
                    "# Increase open files limit",
                    "open-files-limit=65535"
                };

                // Write the configuration list to the file (this part is missing in the original code, added it for completeness)
                File.WriteAllLines(configFile, list);
            }
        }

        // Converts a MaterialSkin Primary color to a System.Drawing.Color object
        public static Color ConvertToColor(Primary hexColor)
        {
            // Extract RGB components from the hex color
            int red = (int)hexColor >> 16 & 0xFF;  // Red component
            int green = (int)hexColor >> 8 & 0xFF; // Green component
            int blue = (int)hexColor & 0xFF;       // Blue component

            // Convert the RGB components to a Color object using ColorTranslator
            Color color = ColorTranslator.FromHtml($"#{red:X2}{green:X2}{blue:X2}");

            return color; // Return the Color object
        }
    }
}
