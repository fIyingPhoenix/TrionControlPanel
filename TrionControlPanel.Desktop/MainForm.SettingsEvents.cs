// =============================================================================
// MainForm.SettingsEvents.cs
// Purpose: Event handlers for settings page controls
// Related UI: TabSettings, toggle buttons, combo boxes, text inputs
// Dependencies: Settings, AppSettings
// =============================================================================

using System.Diagnostics;
using System.Globalization;
using TrionControlPanel.Desktop.Extensions.Application;
using TrionControlPanel.Desktop.Extensions.Classes;
using TrionControlPanel.Desktop.Extensions.Classes.Data.Form;
using TrionControlPanel.Desktop.Extensions.Classes.Monitor;
using TrionControlPanel.Desktop.Extensions.Classes.Network;
using TrionControlPanel.Desktop.Extensions.Database;
using TrionControlPanel.Desktop.Extensions.Modules;
using TrionControlPanel.Desktop.Extensions.Notification;
using TrionControlPanelDesktop.Extensions.Modules;
using static TrionControlPanel.Desktop.Extensions.Modules.Enums;
using static TrionControlPanel.Desktop.Extensions.Notification.AlertBox;

namespace TrionControlPanelDesktop
{
    /// <summary>
    /// Partial class containing event handlers for settings controls.
    /// Handles user interactions with settings toggles, dropdowns, and buttons.
    /// </summary>
    public partial class MainForm
    {
        #region Trion Settings Events
        // ─────────────────────────────────────────────────────────────────────

        private void TGLServerCrashDetection_CheckedChanged(object sender, EventArgs e)
        {
            settings.ServerCrashDetection = TGLServerCrashDetection.Checked;
        }

        private void TGLServerStartup_CheckedChanged(object sender, EventArgs e)
        {
            settings.RunServerWithWindows = TGLServerStartup.Checked;
        }

        private void TGLRunTrionStartup_CheckedChanged(object sender, EventArgs e)
        {
            settings.RunWithWindows = TGLRunTrionStartup.Checked;
        }

        private void TGLHideConsole_CheckedChanged(object sender, EventArgs e)
        {
            settings.ConsoleHide = TGLHideConsole.Checked;
        }

        private void TGLNotificationSound_CheckedChanged(object sender, EventArgs e)
        {
            settings.NotificationSound = TGLNotificationSound.Checked;
        }

        private void TGLStayInTray_CheckedChanged(object sender, EventArgs e)
        {
            settings.StayInTray = TGLStayInTray.Checked;
        }

        private void TGLAutoUpdateTrion_CheckedChanged(object sender, EventArgs e)
        {
            settings.AutoUpdateTrion = TGLAutoUpdateTrion.Checked;
        }

        private void TGLAutoUpdateCore_CheckedChanged(object sender, EventArgs e)
        {
            settings.AutoUpdateCore = TGLAutoUpdateCore.Checked;
        }

        private void TGLAutoUpdateDatabase_CheckedChanged(object sender, EventArgs e)
        {
            settings.AutoUpdateDatabase = TGLAutoUpdateDatabase.Checked;
        }

        /// <summary>
        /// Handles color theme selection change.
        /// </summary>
        private void CBOXColorSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            settings.TrionTheme = CBOXColorSelect.SelectedItem?.ToString() switch
            {
                "TrionBlue" => TrionTheme.TrionBlue,
                "Purple" => TrionTheme.Purple,
                "Orange" => TrionTheme.Orange,
                "Green" => TrionTheme.Green,
                _ => settings.TrionTheme
            };
            LoadSkin();
        }

        /// <summary>
        /// Handles language selection change.
        /// </summary>
        private void CBOXLanguageSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            settings.TrionLanguage = CBOXLanguageSelect.SelectedItem?.ToString() ?? "en";
            LoadLangauge();
            Refresh();
        }

        /// <summary>
        /// Handles icon selection change.
        /// </summary>
        private void CBOXTrionIcon_SelectedIndexChanged(object sender, EventArgs e)
        {
            settings.TrionIcon = CBOXTrionIcon.SelectedItem?.ToString() ?? "Trion New Logo";
            ChangeFormIcon(settings.TrionIcon);
        }

        /// <summary>
        /// Toggles visibility of the supporter key.
        /// </summary>
        private void BTNReviveSupporterKey_Click(object sender, EventArgs e)
        {
            TXTSupporterKey.PasswordChar = TXTSupporterKey.PasswordChar == '⛊' ? '\0' : '⛊';
        }

        #endregion

        #region Custom Emulator Settings Events
        // ─────────────────────────────────────────────────────────────────────

        private void TGLUseCustomServer_CheckedChanged(object sender, EventArgs e)
        {
            settings.LaunchCustomCore = TGLUseCustomServer.Checked;
        }

        private void TGLCustomNames_CheckedChanged(object sender, EventArgs e)
        {
            settings.CustomNames = TGLCustomNames.Checked;
            TXTCustomWorldName.ReadOnly = !settings.CustomNames;
            TXTCustomDatabaseName.ReadOnly = !settings.CustomNames;
            TXTCustomAuthName.ReadOnly = !settings.CustomNames;
        }

        /// <summary>
        /// Handles emulator core selection change.
        /// </summary>
        private void CBOXSelectedEmulators_SelectedIndexChanged(object sender, EventArgs e)
        {
            settings.SelectedCore = CBOXSelectedEmulators.SelectedItem?.ToString() switch
            {
                "AzerothCore" => Cores.AzerothCore,
                "CMaNGOS" => Cores.CMaNGOS,
                "CypherCore" => Cores.CypherCore,
                "TrinityCore" => Cores.TrinityCore,
                "TrinityCore Classic" => Cores.TrinityCoreClassic,
                "VMaNGOS" => Cores.VMaNGOS,
                _ => settings.SelectedCore
            };
        }

        /// <summary>
        /// Opens the emulator folder in Explorer.
        /// </summary>
        private void BTNEmulatorLocation_Click(object sender, EventArgs e)
        {
            string? path = settings.SelectedCore switch
            {
                // Return appropriate path based on selected core
                _ => null
            };

            if (string.IsNullOrWhiteSpace(path) || !Directory.Exists(path))
            {
                AlertBox.Show("Emulator folder not found.", NotificationType.Info, settings);
                return;
            }

            Process.Start(new ProcessStartInfo
            {
                FileName = path,
                UseShellExecute = true,
                Verb = "open"
            });
        }

        /// <summary>
        /// Opens the database folder in Explorer.
        /// </summary>
        private void BTNDatabaseLocation_Click(object sender, EventArgs e)
        {
            string path = settings.DBLocation;

            if (string.IsNullOrWhiteSpace(path) || !Directory.Exists(path))
            {
                AlertBox.Show("Database folder not found.", NotificationType.Info, settings);
                return;
            }

            Process.Start(new ProcessStartInfo
            {
                FileName = path,
                UseShellExecute = true,
                Verb = "open"
            });
        }

        #endregion

        #region Database Settings Events
        // ─────────────────────────────────────────────────────────────────────

        /// <summary>
        /// Handles the database toggle buttons for selecting expansion databases.
        /// </summary>
        private void HandleDatabaseToggle(string dbPrefix, CheckBox toggled)
        {
            // Enable/disable custom database name fields
            bool isCustom = dbPrefix == "custom";
            TXTAuthDatabase.ReadOnly = !isCustom;
            TXTCharDatabase.ReadOnly = !isCustom;
            TXTWorldDatabase.ReadOnly = !isCustom;

            // Uncheck all other toggles (radio button behavior)
            foreach (var toggle in new[] { TGLClassicDB, TGLTbcDB, TGLWotlkDB, TGLCataDB, TGLMopDB, TGLCustomDB })
            {
                if (toggle != toggled)
                    toggle.Checked = false;
            }

            // Set database names based on prefix
            if (!isCustom)
            {
                settings.AuthDatabase = $"{dbPrefix}_auth";
                settings.CharactersDatabase = $"{dbPrefix}_characters";
                settings.WorldDatabase = $"{dbPrefix}_world";
                LoadSettingsUI();
            }
        }

        private void TGLClassicDB_CheckedChanged(object sender, EventArgs e) =>
            HandleDatabaseToggle("classic", TGLClassicDB);

        private void TGLTbcDB_CheckedChanged(object sender, EventArgs e) =>
            HandleDatabaseToggle("tbc", TGLTbcDB);

        private void TGLWotlkDB_CheckedChanged(object sender, EventArgs e) =>
            HandleDatabaseToggle("wotlk", TGLWotlkDB);

        private void TGLCataDB_CheckedChanged(object sender, EventArgs e) =>
            HandleDatabaseToggle("cata", TGLCataDB);

        private void TGLMopDB_CheckedChanged(object sender, EventArgs e) =>
            HandleDatabaseToggle("mop", TGLMopDB);

        private void TGLCustomDB_CheckedChanged(object sender, EventArgs e) =>
            HandleDatabaseToggle("custom", TGLCustomDB);

        /// <summary>
        /// Fixes the database by reinitializing it.
        /// </summary>
        private async void BTNFixDatabase_Click(object sender, EventArgs e)
        {
            string database = Links.Install.Database.Replace("/", @"\");
            string sqlLocation = $@"{database}\extra\initSTDDatabase.sql";
            string folderPath = $@"{database}\data";

            await FileManager.DeleteFolderAsync(folderPath);
            await AppExecuteManager.ApplicationStart(
                settings.DBExeLoc,
                settings.DBWorkingDir,
                "initialize MySQL",
                true,
                $"--initialize-insecure --init-file=\"{sqlLocation}\" --console");
        }

        #endregion

        #region Text Input Event Handlers
        // ─────────────────────────────────────────────────────────────────────

        /// <summary>
        /// Restricts text input to numbers only.
        /// Used for port numbers, intervals, etc.
        /// </summary>
        private void TXTOnlyNumbers(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// Handles text changes with debounced auto-save.
        /// Saves settings 1 second after the user stops typing.
        /// </summary>
        private void TXTBox_TextChanged(object sender, EventArgs e)
        {
            TimerKeyPress?.Dispose();
            TimerKeyPress = new System.Threading.Timer(SaveDataFormTextbox, null, 1000, Timeout.Infinite);
        }

        /// <summary>
        /// Saves text input values to settings.
        /// Called by the debounce timer.
        /// </summary>
        private void SaveDataFormTextbox(object? state)
        {
            settings.DDNSDomain = TXTDDNSDomain.Text;
            settings.DDNSPassword = TXTDDNSPassword.Text;
            settings.DDNSInterval = int.Parse(TXTDDNSInterval.Text, CultureInfo.InvariantCulture);
            settings.DDNSUsername = TXTDDNSUsername.Text;
            settings.SupporterKey = TXTSupporterKey.Text;
            settings.AuthDatabase = TXTAuthDatabase.Text;
            settings.CharactersDatabase = TXTCharDatabase.Text;
            settings.WorldDatabase = TXTWorldDatabase.Text;
            settings.DBServerHost = TXTDatabaseHost.Text;
            settings.DBServerPassword = TXTDatabasePassword.Text;
            settings.DBServerPort = TXTDatabasePort.Text;
            settings.DBServerUser = TXTDatabaseUser.Text;
            settings.CustomWorldExeName = TXTCustomWorldName.Text;
            settings.CustomLogonExeName = TXTCustomAuthName.Text;
            settings.DBExeName = TXTCustomDatabaseName.Text;
        }

        #endregion

        #region Support and Updates
        // ─────────────────────────────────────────────────────────────────────

        /// <summary>
        /// Opens the support page in the default browser.
        /// </summary>
        private void BTNShowSupport_Click(object sender, EventArgs e)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = "https://flying-phoenix.dev/support.php",
                UseShellExecute = true
            });
        }

        /// <summary>
        /// Update timer tick handler.
        /// Periodically checks for updates when online.
        /// </summary>
        private async void TimerUpdate_Tick(object sender, EventArgs e)
        {
            if (NetworkManager.IsOffline) return;

            await UpdateSppVersion();
            AppUpdateManager.CheckForUpdate(settings);
            await StartAutoUpdate();
        }

        /// <summary>
        /// Updates version labels with local and online version information.
        /// </summary>
        private async Task UpdateSppVersion()
        {
            AppUpdateManager.GetSPPVersionOffline(settings);
            await AppUpdateManager.GetSPPVersionOnline(settings);

            LBLTrionVersion.Text = string.Format(CultureInfo.InvariantCulture,
                translator.Translate("LBLTrionVersion"),
                FormData.UI.Version.Local.Trion, FormData.UI.Version.Online.Trion);
            LBLDBVersion.Text = string.Format(CultureInfo.InvariantCulture,
                translator.Translate("LBLDBVersion"),
                FormData.UI.Version.Local.Database, FormData.UI.Version.Online.Database);
            LBLClassicVersion.Text = string.Format(CultureInfo.InvariantCulture,
                translator.Translate("LBLClassicVersion"),
                FormData.UI.Version.Local.Classic, FormData.UI.Version.Online.Classic);
            LBLTBCVersion.Text = string.Format(CultureInfo.InvariantCulture,
                translator.Translate("LBLTBCVersion"),
                FormData.UI.Version.Local.TBC, FormData.UI.Version.Online.TBC);
            LBLWotLKVersion.Text = string.Format(CultureInfo.InvariantCulture,
                translator.Translate("LBLWotLKVersion"),
                FormData.UI.Version.Local.WotLK, FormData.UI.Version.Online.WotLK);
            LBLCataVersion.Text = string.Format(CultureInfo.InvariantCulture,
                translator.Translate("LBLCataVersion"),
                FormData.UI.Version.Local.Cata, FormData.UI.Version.Online.Cata);
            LBLMoPVersion.Text = string.Format(CultureInfo.InvariantCulture,
                translator.Translate("LBLMoPVersion"),
                FormData.UI.Version.Local.Mop, FormData.UI.Version.Online.Mop);
        }

        /// <summary>
        /// Performs automatic updates for enabled components.
        /// </summary>
        private async Task StartAutoUpdate()
        {
            // Trion application update
            if (FormData.UI.Version.Update.Trion && settings.AutoUpdateTrion)
            {
                await InstallExpansionAsync("trion", "", settings.ClassicInstalled,
                    v => FormData.Infos.Install.Trion = v);
            }

            // Database update
            if (FormData.UI.Version.Update.Database && settings.AutoUpdateTrion)
            {
                if (FormData.UI.Form.DBRunning)
                {
                    await AppExecuteManager.StopDatabase();
                }
                await RepairExpansionAsync("database", "/database", settings.DBInstalled);
            }

            // Classic expansion update
            if (FormData.UI.Version.Update.Classic && settings.AutoUpdateCore)
            {
                if (FormData.UI.Form.ClassicLogonRunning || FormData.UI.Form.ClassicWorldRunning)
                {
                    await AppExecuteManager.StopLogon();
                    await AppExecuteManager.StopWorld();
                }
                await RepairExpansionAsync("classic", "/classic", settings.ClassicInstalled);
            }

            // TBC expansion update
            if (FormData.UI.Version.Update.TBC && settings.AutoUpdateCore)
            {
                if (FormData.UI.Form.TBCLogonRunning || FormData.UI.Form.TBCWorldRunning)
                {
                    await AppExecuteManager.StopLogon();
                    await AppExecuteManager.StopWorld();
                }
                await RepairExpansionAsync("tbc", "/tbc", settings.TBCInstalled);
            }

            // WotLK expansion update
            if (FormData.UI.Version.Update.WotLK && settings.AutoUpdateCore)
            {
                if (FormData.UI.Form.WotLKLogonRunning || FormData.UI.Form.WotLKWorldRunning)
                {
                    await AppExecuteManager.StopLogon();
                    await AppExecuteManager.StopWorld();
                }
                await RepairExpansionAsync("wotlk", "/wotlk", settings.WotLKInstalled);
            }

            // Cataclysm expansion update
            if (FormData.UI.Version.Update.Cata && settings.AutoUpdateCore)
            {
                if (FormData.UI.Form.CataLogonRunning || FormData.UI.Form.CataWorldRunning)
                {
                    await AppExecuteManager.StopLogon();
                    await AppExecuteManager.StopWorld();
                }
                await RepairExpansionAsync("cata", "/cata", settings.CataInstalled);
            }

            // MoP expansion update
            if (FormData.UI.Version.Update.Mop && settings.AutoUpdateCore)
            {
                if (FormData.UI.Form.MOPLogonRunning || FormData.UI.Form.MOPWorldRunning)
                {
                    await AppExecuteManager.StopLogon();
                    await AppExecuteManager.StopWorld();
                }
                await RepairExpansionAsync("mop", "/mop", settings.CataInstalled);
            }
        }

        #endregion

        #region Downloader UI Helpers
        // ─────────────────────────────────────────────────────────────────────

        /// <summary>
        /// Resets the downloader UI to its initial state.
        /// </summary>
        private void RefreshDownloader()
        {
            CardLocalFiles.Visible = true;
            LBLCurrentDownload.Text = translator.Translate("LBLCurrentDownload");
            LBLServerFiles.Text = translator.Translate("LBLServerFilesIDLE");
            LBLLocalFiles.Text = translator.Translate("LBLLocalFilesIDLE");
            LBLFilesToBeDownloaded.Text = translator.Translate("LBLFilesToBeDownloadedIDLE");
            LBLFilesToBeRemoved.Text = translator.Translate("LBLFilesToBeRemovedIDLE");
            LBLFileName.Text = translator.Translate("LBLFileNameIDLE");
            LBLFileSize.Text = translator.Translate("LBLFileSizeIDLE");
            INITSpinner.Visible = false;
            PBarCurrentDownlaod.Value = 0;
            PBARTotalDownload.Value = 0;
        }

        #endregion
    }
}
