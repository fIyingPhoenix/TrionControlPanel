using TrionControlPanel.Desktop.Extensions.Classes;
using TrionControlPanel.Desktop.Extensions.Classes.Monitor;
using TrionControlPanel.Desktop.Extensions.Classes.Network;
using TrionControlPanel.Desktop.Extensions.Modules.Lists;
using TrionControlPanelDesktop.Extensions.Modules;
using static TrionControlPanel.Desktop.Extensions.Modules.Enums;

namespace TrionControlPanelDesktop
{
    public partial class MainForm
    {
        /// <summary>
        /// Checks for existing installations of the database and SPP emulators
        /// and marks them as installed in settings if found.
        /// </summary>
        private async Task CheckAndMarkInstalled()
        {
            TrionLogger.Log("Starting auto-detection of installed components.");

            // Auto-detect Database
            await AutoDetectDatabase();

            // Auto-detect SPP Emulators
            await AutoDetectSppEmulators();

            // Save updated settings
            await Settings.SaveSettings(settings, "Settings.json");

            TrionLogger.Log("Finished auto-detection of installed components.");
        }

        /// <summary>
        /// Attempts to auto-detect a database installation.
        /// </summary>
        private async Task AutoDetectDatabase()
        {
            if (!settings.DBInstalled)
            {
                string dbInstallPath = Links.Install.Database.Replace("/", @"");
                if (Directory.Exists(dbInstallPath))
                {
                    // Check for mysqld.exe within the bin subdirectory
                    string exeLoc = FileManager.GetExecutableLocation($@"{dbInstallPath}\bin", "mysqld");

                    // Check for a common MySQL data directory
                    string dataPath = Path.Combine(dbInstallPath, "data");

                    if (!string.IsNullOrEmpty(exeLoc) && Directory.Exists(dataPath))
                    {
                        TrionLogger.Log($"Detected existing database installation at: {dbInstallPath}");

                        // Populate database settings
                        settings.DBInstalled = true;
                        settings.DBLocation = dbInstallPath;
                        settings.DBWorkingDir = $@"{dbInstallPath}\bin";
                        settings.DBExeLoc = exeLoc;
                        settings.DBExeName = "mysqld";

                        TrionLogger.Log("Database marked as installed in settings.");
                    }
                    else
                    {
                        TrionLogger.Log($"Existing database directory found at {dbInstallPath}, but key files (mysqld.exe or data folder) are missing. Not marking as installed.");
                    }
                }
                else
                {
                    TrionLogger.Log("No existing database directory found. Skipping auto-detection for database.");
                }
            }
        }

        /// <summary>
        /// Attempts to auto-detect SPP emulator installations.
        /// </summary>
        private async Task AutoDetectSppEmulators()
        {
            var allSpps = settings.GetAllExpansionConfigs();
            foreach (var kvp in allSpps)
            {
                SPP sppEnum = kvp.Key;
                ExpansionConfig config = kvp.Value;

                if (!config.IsInstalled)
                {
                    string sppInstallPath = typeof(Links.Install).GetProperty(sppEnum.ToString())?.GetValue(null)?.ToString().Replace("/", @"\");

                    if (string.IsNullOrEmpty(sppInstallPath)) continue;

                    if (Directory.Exists(sppInstallPath))
                    {
                        TrionLogger.Log($"Existing directory found for {sppEnum} at: {sppInstallPath}");

                        if (NetworkManager.IsOffline)
                        {
                            TrionLogger.Warning($"Skipping full file verification for {sppEnum} in offline mode. Cannot compare with server manifest.");
                            // If user explicitly asks for *some* form of offline detection
                            // we would put simpler checks here, e.g., just checking for executables.
                            // For now, adhering to "same logic as update" implies network dependency for full verification.
                            continue;
                        }

                        TrionLogger.Log($"Performing full file verification for {sppEnum} (online mode).");

                        try
                        {
                            // 1. Get local files
                            var localFiles = await FileManager.ProcessFilesAsync(sppInstallPath, null, CancellationToken.None);

                            if (!localFiles.Any())
                            {
                                TrionLogger.Log($"No files found in {sppInstallPath}. Not marking {sppEnum} as installed.");
                                continue;
                            }

                            // 2. Get server manifest
                            string apiKeyIdentifier = sppEnum.ToString().ToLower();
                            var serverFiles = await NetworkManager.GetServerFiles(Links.APIRequests.GetInstallFiles(apiKeyIdentifier, settings.SupporterKey), null);

                            if (serverFiles == null || !serverFiles.Any())
                            {
                                TrionLogger.Warning($"Could not retrieve server manifest for {sppEnum}. Not marking as installed.");
                                continue;
                            }

                            // 3. Compare files
                            var (missingFiles, filesToDelete) = await FileManager.CompareFiles(serverFiles, localFiles, sppInstallPath, null, null);

                            if (!missingFiles.Any()) // If no files are missing compared to server manifest
                            {
                                TrionLogger.Log($"Files for {sppEnum} are consistent with server manifest. Marking as installed.");

                                // Populate expansion settings
                                config.IsInstalled = true;
                                config.WorkingDirectory = sppInstallPath;

                                // Determine executable names based on SPP type
                                string worldExeName = "", logonExeName = "";
                                switch (sppEnum)
                                {
                                    case SPP.Classic:
                                        worldExeName = "mangosd";
                                        logonExeName = "realmd";
                                        config.WorldDisplayName = "WoW Classic World";
                                        config.LogonDisplayName = "WoW Classic Logon";
                                        break;
                                    case SPP.TheBurningCrusade:
                                        worldExeName = "mangosd";
                                        logonExeName = "realmd";
                                        config.WorldDisplayName = "The Burning Crusade World";
                                        config.LogonDisplayName = "The Burning Crusade Logon";
                                        break;
                                    case SPP.WrathOfTheLichKing:
                                        worldExeName = "worldserver";
                                        logonExeName = "authserver";
                                        config.WorldDisplayName = "Wrath of the Lich King World";
                                        config.LogonDisplayName = "Wrath of the Lich King Logon";
                                        break;
                                    case SPP.Cataclysm:
                                        worldExeName = "worldserver";
                                        logonExeName = "authserver";
                                        config.WorldDisplayName = "Cataclysm World";
                                        config.LogonDisplayName = "Cataclysm Logon";
                                        break;
                                    case SPP.MistsOfPandaria:
                                        worldExeName = "worldserver";
                                        logonExeName = "authserver";
                                        config.WorldDisplayName = "Mists of Pandaria World";
                                        config.LogonDisplayName = "Mists of Pandaria Logon";
                                        break;
                                    case SPP.Custom: // Handle custom as well, though it's less likely to be auto-detected by server manifest
                                        // Custom core executables are already in settings.
                                        worldExeName = config.WorldExeName; // Use existing name
                                        logonExeName = config.LogonExeName; // Use existing name
                                        break;
                                }

                                // Find actual executable locations
                                config.WorldExePath = FileManager.GetExecutableLocation(sppInstallPath, worldExeName);
                                config.LogonExePath = FileManager.GetExecutableLocation(sppInstallPath, logonExeName);
                                config.WorldExeName = worldExeName;
                                config.LogonExeName = logonExeName;

                                settings.SetExpansionConfig(sppEnum, config);
                            }
                            else
                            {
                                TrionLogger.Log($"Files for {sppEnum} are NOT consistent with server manifest (missing files detected). Not marking as installed.");
                            }
                        }
                        catch (Exception ex)
                        {
                            TrionLogger.Error($"Error during auto-detection for {sppEnum}: {ex.Message}");
                        }
                    }
                    else
                    {
                        TrionLogger.Log($"No existing directory found for {sppEnum}. Skipping auto-detection.");
                    }
                }
            }
        }
    }
}
