using MaterialSkin;
using Newtonsoft.Json;
using TrionControlPanel.Desktop.Extensions.Classes.Monitor;
using TrionControlPanel.Desktop.Extensions.Modules;
using TrionControlPanel.Desktop.Extensions.Modules.Lists;

namespace TrionControlPanel.Desktop.Extensions.Classes
{
    // Settings class for handling application settings.
    public class Settings
    {
        // Saves the application settings to a specified file path.
        public static async Task SaveSettings(AppSettings settings, string filePath)
        {
            try
            {
                string json = JsonConvert.SerializeObject(settings, Formatting.Indented);
                await File.WriteAllTextAsync(filePath, json);
            }
            catch (Exception ex)
            {
                // Log the exception (assuming a logging mechanism is available)
                TrionLogger.Log($"Error saving settings: {ex.Message}");
            }
        }

        // Loads the application settings from a specified file path.
        public static AppSettings LoadSettings(string filePath)
        {
            try
            {
                if (!File.Exists(filePath))
                    return new AppSettings(); // Return default settings if the file doesn't exist

                string json = File.ReadAllText(filePath);
                return JsonConvert.DeserializeObject<AppSettings>(json)!;
            }
            catch (Exception ex)
            {
                // Log the exception (assuming a logging mechanism is available)
                TrionLogger.Log($"Error loading settings: {ex.Message}");
                return new AppSettings(); // Return default settings in case of error
            }
        }

        // Creates default settings and saves them to a specified file path.
        public static void CreatSettings(string filePath)
        {
            var customSettings = new AppSettings
            {
                WorldDatabase = "wotlk_world",
                AuthDatabase = "wotlk_auth",
                CharactersDatabase = "wotlk_characters",
                HotfixDatabase = "",
                DBExeLoc = "",
                DBWorkingDir = "",
                DBLocation = "",
                DBServerHost = "localhost",
                DBServerUser = "phoenix",
                DBServerPassword = "phoenix",
                DBServerPort = "3306",
                DBExeName = "mysqld",
                DBInstalled = false,
                CustomWorkingDirectory = "",
                CustomWorldExeLoc = "",
                CustomLogonExeLoc = "",
                CustomLogonExeName = "worldserver",
                CustomWorldExeName = "authserver",
                CustomLogonName = "Custom Core",
                CustomWorldName = "Custom Core",
                LaunchCustomCore = false,
                CustomInstalled = false,
                ClassicWorkingDirectory = "",
                ClassicWorldExeLoc = "",
                ClassicLogonExeLoc = "",
                ClassicLogonExeName = "",
                ClassicWorldExeName = "",
                ClassicWorldName = "",
                ClassicLogonName = "",
                LaunchClassicCore = false,
                ClassicInstalled = false,
                TBCWorkingDirectory = "",
                TBCWorldExeLoc = "",
                TBCLogonExeLoc = "",
                TBCLogonExeName = "",
                TBCWorldExeName = "",
                TBCWorldName = "",
                TBCLogonName = "",
                LaunchTBCCore = false,
                TBCInstalled = false,
                WotLKWorkingDirectory = "",
                WotLKWorldExeLoc = "",
                WotLKLogonExeLoc = "",
                WotLKLogonExeName = "",
                WotLKWorldExeName = "",
                WotLKWorldName = "",
                WotLKLogonName = "",
                LaunchWotLKCore = false,
                WotLKInstalled = false,
                CataWorkingDirectory = "",
                CataWorldExeLoc = "",
                CataLogonExeLoc = "",
                CataLogonExeName = "",
                CataWorldExeName = "",
                CataWorldName = "",
                CataLogonName = "",
                LaunchCataCore = false,
                CataInstalled = false,
                MopWorkingDirectory = "",
                MopWorldExeLoc = "",
                MopLogonExeLoc = "",
                MopLogonExeName = "",
                MopWorldExeName = "",
                MoPWorldName = "",
                MoPLogonName = "",
                LaunchMoPCore = false,
                MOPInstalled = false,
                DDNSDomain = "",
                DDNSUsername = "",
                DDNSPassword = "",
                DDNSInterval = 1000,
                IPAddress = "",
                TrionTheme = Enums.TrionTheme.TrionBlue,
                TrionLanguage = "enUS",
                TrionIcon = "Trion New Logo",
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
                DDNSerivce = Enums.DDNSerivce.DuckDNS,
                SelectedSPP = Enums.SPP.WrathOfTheLichKing,
                SelectedDatabases = Enums.Databases.WotLK,
            };

            try
            {
                var json = JsonConvert.SerializeObject(customSettings, Formatting.Indented);
                using (var writer = new StreamWriter(filePath, false))
                {
                    writer.Write(json);
                }
            }
            catch (Exception ex)
            {
                // Log the exception (assuming a logging mechanism is available)
                TrionLogger.Log($"Error creating settings: {ex.Message}");
            }
        }

        // Creates a MySQL configuration file at the specified location.
        public static void CreateMySQLConfigFile(string Location, string DatabaseDirectory)
        {
            DatabaseDirectory = DatabaseDirectory.Replace(@"\", "/");
            Location = Location.Replace(@"\", "/");
            string configFile = $@"{Location}/my.ini";
            File.Delete(configFile);
            if (!File.Exists(configFile))
            {
                List<string> list = new()
                    {
                        "[client]", 
                        "port=3306",
                        "default-character-set = utf8mb4",
                        @"socket=""/tmp/mysql.sock"" ",
                        //
                        "\n[mysqld]",
                        "# Basic Settings",
                        $@"basedir=""{DatabaseDirectory}""",
                        $@"datadir=""{DatabaseDirectory}/data""",
                        "bind-address=0.0.0.0",
                        "port=3306",                        //maybe change it to 3307 to piss my friend off :D
                        //
                        "\n# Memory Allocation",
                        "innodb_buffer_pool_size=3G",       // 70-80% of total memory, for now 3GB is fine, maybe one day i will make this dynamic
                        "innodb_redo_log_capacity=1G",
                        "innodb_log_buffer_size=128M",      // increased for large transactions (64-128) still not sure.
                        //
                        "\n# Threading",                    // threading (Optimized for 4 Cores) i still dont know what potatos this will run at
                        "innodb_thread_concurrency=8",      // 2x the number of cores, 8 for 4 cores
                        "innodb_read_io_threads=8",         // Increase for read-heavy workloads
                        "innodb_write_io_threads=4",        // reduce since writes are less frequent
                        "thread_cache_size=1000",           // cache threads to reduce overhead
                        //
                        "\n# Connections & Query Performance", 
                        "max_connections=2000",             // 100-200 per core, 2000 for 4 cores. increased for high traffic
                        "max_allowed_packet=512M",          // increased for large queries
                        "table_open_cache=10000",           // increased for high traffic
                        "table_definition_cache=5000",      // improve table structure caching
                        "table_open_cache_instances=6",     // reduce contention
                        "query_prealloc_size=65536",        // Reduce memory fragmentation
                        "query_alloc_block_size=65536",     //
                        //
                        "\n# General storage settings",
                        "default-storage-engine=INNODB",
                        //
                        "\n# Log settings",
                        $@"log_error=""{Location}/logs/database/mysql_error.log""",
                        "slow_query_log=1",
                        $@"slow_query_log_file=""{Location}/logs/database/mysql_slow.log""",
                        "general_log=1",                    // disable general logging for performance for now to see if inpacting performance
                        $@"general_log_file=""{Location}/logs/database/mysql_general.log""",
                        "long_query_time=1",
                        "log_queries_not_using_indexes=1",  // log queries that are not using indexes, helps detect slow queries
                        //
                        "\n# Table and Index Cache",
                        "table_open_cache=8000",
                        "table_open_cache_instances=16",
                        "tmp_table_size=1024",
                        "max_heap_table_size=16384",        // increase for large temporary tables
                        "join_buffer_size=4M",
                        "sort_buffer_size=4M",
                        "read_buffer_size=4M",
                        "read_rnd_buffer_size=4M",
                         //  
                        "\n# InnoDB Performance Tuning",
                        "innodb_flush_log_at_trx_commit=2", // 0 for speed, 2 for safety
                        "innodb_io_capacity=1000",          // IOPS, 1000 for SSD, 200 for HDD (1000 for now)
                        "innodb_io_capacity_max=2000",      // Max IOPS, 2000 for SSD, 400 for HDD (2000 for now)
                        "innodb_flush_neighbors=0",         // 0 for SSD, 1 for HDD
                        "innodb_adaptive_hash_index=ON",    // ON for SSD, OFF for HDD
                        "innodb_lru_scan_depth=2048",       // 1024-2048 for large buffer pools, improves buffer pool efficiency
                        "innodb_buffer_pool_instances=3",   // 1 per GB of buffer pool size, 3 for 3GB
                        "innodb_page_cleaners=2",           // 2 for 2 cores, 1 per core, helps parallel flushing
                        //
                        "\n# Networking & Buffers",
                        "net_buffer_length=512K",
                        "back_log=1024",
                        "\n[mysqld_safe]",
                        "# Increase open files limit",
                        "open-files-limit=65535"
                    };
                try
                {
                    File.WriteAllLines(configFile, list);
                }
                catch (Exception ex)
                {
                    // Log the exception (assuming a logging mechanism is available)
                    TrionLogger.Log($"Error creating MySQL config file: {ex.Message}");
                }
            }
        }

        // Converts a hex color to a Color object.
        public static Color ConvertToColor(Primary hexColor)
        {
            int red = (int)hexColor >> 16 & 0xFF; // Extract the red component
            int green = (int)hexColor >> 8 & 0xFF; // Extract the green component
            int blue = (int)hexColor & 0xFF; // Extract the blue component

            Color color = ColorTranslator.FromHtml($"#{red:X2}{green:X2}{blue:X2}");
            return color;
        }
    }
}