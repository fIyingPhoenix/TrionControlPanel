// =============================================================================
// Settings.cs
// Purpose: Application settings persistence and configuration management
// Used by: MainForm, LoadingScreen, all components requiring settings
// Steps 6, 11 of IMPROVEMENTS.md - XML Documentation, Inline Comments
// =============================================================================

using MaterialSkin;
using Newtonsoft.Json;
using TrionControlPanel.Desktop.Extensions.Classes.Monitor;
using TrionControlPanel.Desktop.Extensions.Modules;
using TrionControlPanel.Desktop.Extensions.Modules.Lists;

namespace TrionControlPanel.Desktop.Extensions.Classes
{
    /// <summary>
    /// Provides methods for loading, saving, and creating application settings.
    /// Handles JSON serialization of <see cref="AppSettings"/> objects.
    /// </summary>
    /// <remarks>
    /// This static class is responsible for:
    /// - Loading settings from JSON files
    /// - Saving settings to JSON files
    /// - Creating default settings for first-run scenarios
    /// - Creating MySQL configuration files
    /// - Color conversion utilities for theming
    ///
    /// Settings are stored in Settings.json in the application directory.
    /// </remarks>
    public class Settings
    {
        #region Settings Persistence
        // ─────────────────────────────────────────────────────────────────────

        /// <summary>
        /// Saves the application settings to a JSON file asynchronously.
        /// </summary>
        /// <param name="settings">The settings object to save.</param>
        /// <param name="filePath">The full path to the settings file.</param>
        /// <returns>A task that completes when the settings have been saved.</returns>
        /// <remarks>
        /// The settings are serialized to JSON with indented formatting for readability.
        /// Any errors during saving are logged via <see cref="TrionLogger"/>.
        /// </remarks>
        /// <example>
        /// <code>
        /// var settings = new AppSettings { TrionLanguage = "enUS" };
        /// await Settings.SaveSettings(settings, "Settings.json");
        /// </code>
        /// </example>
        public static async Task SaveSettings(AppSettings settings, string filePath)
        {
            try
            {
                string json = JsonConvert.SerializeObject(settings, Formatting.Indented);
                await File.WriteAllTextAsync(filePath, json);
            }
            catch (Exception ex)
            {
                TrionLogger.Log($"Error saving settings: {ex.Message}", "ERROR");
            }
        }

        /// <summary>
        /// Loads application settings from a JSON file.
        /// </summary>
        /// <param name="filePath">The full path to the settings file.</param>
        /// <returns>
        /// The loaded <see cref="AppSettings"/> object, or a new instance with
        /// default values if the file doesn't exist or an error occurs.
        /// </returns>
        /// <remarks>
        /// This method is synchronous for use during application startup.
        /// If the file doesn't exist or contains invalid JSON, default settings are returned.
        /// </remarks>
        /// <example>
        /// <code>
        /// var settings = Settings.LoadSettings("Settings.json");
        /// string language = settings.TrionLanguage;
        /// </code>
        /// </example>
        public static AppSettings LoadSettings(string filePath)
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    TrionLogger.Info($"Settings file not found ({filePath}), using defaults");
                    return new AppSettings();
                }

                string json = File.ReadAllText(filePath);
                return JsonConvert.DeserializeObject<AppSettings>(json)!;
            }
            catch (Exception ex)
            {
                TrionLogger.Log($"Error loading settings: {ex.Message}", "ERROR");
                return new AppSettings();
            }
        }

        /// <summary>
        /// Creates a default settings file with initial values.
        /// </summary>
        /// <param name="filePath">The full path where the settings file will be created.</param>
        /// <remarks>
        /// This method is typically called on first run to create an initial Settings.json.
        /// The default settings include:
        /// - Database: localhost:3306, user: phoenix, password: phoenix
        /// - WotLK as the default selected expansion
        /// - AzerothCore as the default emulator
        /// - TrionBlue theme, enUS language
        /// - Auto-update enabled for Trion, Core, and Database
        /// - Console hidden by default
        /// </remarks>
        public static void CreateSettings(string filePath)
        {
            var customSettings = new AppSettings
            {
                // Database defaults
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

                // Custom core defaults
                CustomWorkingDirectory = "",
                CustomWorldExeLoc = "",
                CustomLogonExeLoc = "",
                CustomLogonExeName = "worldserver",
                CustomWorldExeName = "authserver",
                CustomLogonName = "Custom Core",
                CustomWorldName = "Custom Core",
                LaunchCustomCore = false,
                CustomInstalled = false,

                // Classic defaults
                ClassicWorkingDirectory = "",
                ClassicWorldExeLoc = "",
                ClassicLogonExeLoc = "",
                ClassicLogonExeName = "",
                ClassicWorldExeName = "",
                ClassicWorldName = "",
                ClassicLogonName = "",
                LaunchClassicCore = false,
                ClassicInstalled = false,

                // TBC defaults
                TBCWorkingDirectory = "",
                TBCWorldExeLoc = "",
                TBCLogonExeLoc = "",
                TBCLogonExeName = "",
                TBCWorldExeName = "",
                TBCWorldName = "",
                TBCLogonName = "",
                LaunchTBCCore = false,
                TBCInstalled = false,

                // WotLK defaults
                WotLKWorkingDirectory = "",
                WotLKWorldExeLoc = "",
                WotLKLogonExeLoc = "",
                WotLKLogonExeName = "",
                WotLKWorldExeName = "",
                WotLKWorldName = "",
                WotLKLogonName = "",
                LaunchWotLKCore = false,
                WotLKInstalled = false,

                // Cataclysm defaults
                CataWorkingDirectory = "",
                CataWorldExeLoc = "",
                CataLogonExeLoc = "",
                CataLogonExeName = "",
                CataWorldExeName = "",
                CataWorldName = "",
                CataLogonName = "",
                LaunchCataCore = false,
                CataInstalled = false,

                // MoP defaults
                MopWorkingDirectory = "",
                MopWorldExeLoc = "",
                MopLogonExeLoc = "",
                MopLogonExeName = "",
                MopWorldExeName = "",
                MoPWorldName = "",
                MoPLogonName = "",
                LaunchMoPCore = false,
                MOPInstalled = false,

                // DDNS defaults
                DDNSDomain = "",
                DDNSUsername = "",
                DDNSPassword = "",
                DDNSInterval = 1000,
                IPAddress = "",

                // Trion preferences
                TrionTheme = Enums.TrionTheme.TrionBlue,
                TrionLanguage = "enUS",
                TrionIcon = "Trion New Logo",
                AutoUpdateCore = true,
                AutoUpdateTrion = true,
                AutoUpdateDatabase = true,
                NotificationSound = false,
                ConsoleHide = true,
                StayInTray = false,
                RunWithWindows = false,
                CustomNames = false,
                RunServerWithWindows = false,
                FirstRun = false,
                DDNSRunOnStartup = false,
                ServerCrashDetection = false,

                // Default selections
                SelectedCore = Enums.Cores.AzerothCore,
                DDNSService = Enums.DDNSService.DuckDNS,
                SelectedSPP = Enums.SPP.WrathOfTheLichKing,
                SelectedDatabases = Enums.Databases.WotLK,
            };

            try
            {
                var json = JsonConvert.SerializeObject(customSettings, Formatting.Indented);
                using var writer = new StreamWriter(filePath, false);
                writer.Write(json);
            }
            catch (Exception ex)
            {
                TrionLogger.Log($"Error creating settings: {ex.Message}", "ERROR");
            }
        }

        #endregion

        #region MySQL Configuration
        // ─────────────────────────────────────────────────────────────────────

        /// <summary>
        /// Creates a MySQL configuration file (my.ini) with optimized settings.
        /// </summary>
        /// <param name="Location">The location where my.ini will be created.</param>
        /// <param name="DatabaseDirectory">The MySQL installation directory (basedir).</param>
        /// <remarks>
        /// Creates an optimized MySQL 8 configuration with:
        /// - 3GB InnoDB buffer pool
        /// - UTF8MB4 character set
        /// - Performance tuning for gaming server workloads
        /// - Logging configuration (error, slow query)
        /// - Security settings (bind to all interfaces)
        ///
        /// The existing my.ini file will be deleted and recreated.
        /// Paths in the config use forward slashes for MySQL compatibility.
        /// </remarks>
        public static void CreateMySQLConfigFile(string Location, string DatabaseDirectory)
        {
            // Convert paths to use forward slashes for MySQL
            DatabaseDirectory = DatabaseDirectory.Replace(@"\", "/");
            Location = Location.Replace(@"\", "/");
            string configFile = $@"{Location}/my.ini";

            // Remove existing config
            File.Delete(configFile);

            if (!File.Exists(configFile))
            {
                List<string> list =
                [
                    // Client settings
                    "[client]",
                    "port=3306",
                    "default-character-set=utf8mb4",
                    @"socket=""/tmp/mysql.sock""",

                    // Server settings
                    "\n[mysqld]",
                    "# Basic Settings",
                    $@"basedir=""{DatabaseDirectory}""",
                    $@"datadir=""{DatabaseDirectory}/data""",
                    "bind-address=0.0.0.0",
                    "port=3306",

                    "\n# Character Set & Collation",
                    "character-set-server=utf8mb4",
                    "collation-server=utf8mb4_0900_ai_ci",

                    "\n# Security",
                    @"secure-file-priv=""",

                    "\n# Memory Allocation",
                    "innodb_buffer_pool_size=3G",
                    "innodb_log_buffer_size=128M",
                    "innodb_redo_log_capacity=1G",

                    "\n# Threading (MySQL 8 handles concurrency well)",
                    "innodb_read_io_threads=8",
                    "innodb_write_io_threads=4",
                    "thread_cache_size=1000",

                    "\n# Connections & Query Performance",
                    "max_connections=2000",
                    "max_allowed_packet=512M",
                    "table_open_cache=10000",
                    "table_definition_cache=5000",
                    "table_open_cache_instances=6",
                    "query_prealloc_size=65536",
                    "query_alloc_block_size=65536",

                    "\n# Storage Engine",
                    "default-storage-engine=INNODB",

                    "\n# Logging (disable general_log in production)",
                    $@"log_error=""{Location}/logs/database/mysql_error.log""",
                    "slow_query_log=1",
                    $@"slow_query_log_file=""{Location}/logs/database/mysql_slow.log""",
                    "general_log=0",
                    $@"general_log_file=""{Location}/logs/database/mysql_general.log""",
                    "long_query_time=1",
                    "log_queries_not_using_indexes=1",

                    "\n# Temporary Tables & Buffers",
                    "tmp_table_size=512M",
                    "max_heap_table_size=512M",
                    "join_buffer_size=512K",
                    "sort_buffer_size=512K",
                    "read_buffer_size=512K",
                    "read_rnd_buffer_size=512K",

                    "\n# InnoDB Performance Tuning",
                    "innodb_flush_log_at_trx_commit=2",
                    "innodb_io_capacity=1000",
                    "innodb_io_capacity_max=2000",
                    "innodb_flush_neighbors=0",
                    "innodb_adaptive_hash_index=ON",
                    "innodb_lru_scan_depth=2048",
                    "innodb_buffer_pool_instances=3",
                    "innodb_page_cleaners=2",

                    "\n# SQL Behavior",
                    "sql_mode=STRICT_TRANS_TABLES,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION",

                    "\n# Monitoring",
                    "performance_schema=ON",

                    "\n# Networking",
                    "net_buffer_length=512K",
                    "back_log=1024",

                    "\n[mysqld_safe]",
                    "open-files-limit=65535"
                ];

                try
                {
                    File.WriteAllLines(configFile, list);
                }
                catch (Exception ex)
                {
                    TrionLogger.Log($"Error creating MySQL config file: {ex.Message}", "ERROR");
                }
            }
        }

        #endregion

        #region Utility Methods
        // ─────────────────────────────────────────────────────────────────────

        /// <summary>
        /// Converts a MaterialSkin Primary color enumeration to a <see cref="Color"/> object.
        /// </summary>
        /// <param name="hexColor">The MaterialSkin Primary color value.</param>
        /// <returns>A <see cref="Color"/> object representing the color.</returns>
        /// <remarks>
        /// This method extracts RGB components from the Primary enum's underlying integer value.
        /// Used for theme customization and UI color matching.
        /// </remarks>
        /// <example>
        /// <code>
        /// Color themeColor = Settings.ConvertToColor(Primary.Blue500);
        /// button.BackColor = themeColor;
        /// </code>
        /// </example>
        public static Color ConvertToColor(Primary hexColor)
        {
            // Extract RGB components from the integer value
            int red = (int)hexColor >> 16 & 0xFF;
            int green = (int)hexColor >> 8 & 0xFF;
            int blue = (int)hexColor & 0xFF;

            // Convert to Color using HTML format
            Color color = ColorTranslator.FromHtml($"#{red:X2}{green:X2}{blue:X2}");
            return color;
        }

        #endregion
    }
}
