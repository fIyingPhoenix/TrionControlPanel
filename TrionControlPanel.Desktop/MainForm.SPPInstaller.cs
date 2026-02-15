// =============================================================================
// MainForm.SPPInstaller.cs
// Purpose: Single Player Project install/repair/uninstall functionality
// Related UI: BTNInstallSPP, BTNRepairSPP, BTNUninstallSPP, TabDownloader
// Dependencies: FileManager, NetworkManager, AppServiceManager
// =============================================================================

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
    /// Partial class containing SPP (Single Player Project) installer logic.
    /// Handles downloading, installing, repairing, and uninstalling expansion packs.
    /// </summary>
    public partial class MainForm
    {
        #region Install SPP
        // ─────────────────────────────────────────────────────────────────────

        /// <summary>
        /// Main install button handler. Installs the selected expansion pack.
        /// </summary>
        private async void BTNInstallSPP_Click(object sender, EventArgs e)
        {
            MainFormTabControler.SelectedTab = TabDownloader;
            RefreshDownloader();
            FormData.UI.Form.InstallingEmulator = true;

            // Ensure database is running before installation
            if (!FormData.UI.Form.DBRunning)
            {
                BTNStartDatabase_Click(sender, e);
                return;
            }

            try
            {
                // Install based on selected expansion
                switch (settings.SelectedSPP)
                {
                    case SPP.Classic:
                        await TryInstallExpansion("Classic", "/classic",
                            FormData.Infos.Install.Classic,
                            FormData.UI.Version.Online.Classic,
                            settings.ClassicInstalled,
                            v => FormData.Infos.Install.Classic = v);
                        break;

                    case SPP.TheBurningCrusade:
                        await TryInstallExpansion("TBC", "/tbc",
                            FormData.Infos.Install.TBC,
                            FormData.UI.Version.Online.TBC,
                            settings.TBCInstalled,
                            v => FormData.Infos.Install.TBC = v);
                        break;

                    case SPP.WrathOfTheLichKing:
                        await TryInstallExpansion("WotLK", "/wotlk",
                            FormData.Infos.Install.WotLK,
                            FormData.UI.Version.Online.WotLK,
                            settings.WotLKInstalled,
                            v => FormData.Infos.Install.WotLK = v);
                        break;

                    case SPP.Cataclysm:
                        await TryInstallExpansion("Cata", "/cata",
                            FormData.Infos.Install.Cata,
                            FormData.UI.Version.Online.Cata,
                            settings.CataInstalled,
                            v => FormData.Infos.Install.Cata = v);
                        break;

                    case SPP.MistsOfPandaria:
                        await TryInstallExpansion("MoP", "/mop",
                            FormData.Infos.Install.Mop,
                            FormData.UI.Version.Online.Mop,
                            settings.MOPInstalled,
                            v => FormData.Infos.Install.Mop = v);
                        break;

                    default:
                        AlertBox.Show(translator.Translate("AlerBoxFailedGettingEmulator"), NotificationType.Info, settings);
                        break;
                }

                AlertBox.Show(translator.Translate("SPPInstalationSucces"), NotificationType.Info, settings);
            }
            catch (Exception ex)
            {
                AlertBox.Show($"An error occurred: {ex.Message}", NotificationType.Error, settings);
            }
        }

        /// <summary>
        /// Attempts to install an expansion, checking prerequisites first.
        /// </summary>
        private async Task TryInstallExpansion(
            string name,
            string folder,
            bool isInstalling,
            string onlineVersion,
            bool isInstalled,
            Action<bool> setInstallStatus)
        {
            if (isInstalling)
            {
                AlertBox.Show(translator.Translate("AlerBoxEmulatorInstalled"), NotificationType.Info, settings);
                FormData.UI.Form.InstallingEmulator = false;
                return;
            }

            if (onlineVersion.Contains("N/A"))
            {
                AlertBox.Show(translator.Translate("AlerBoxEmulatorNotFound"), NotificationType.Info, settings);
                FormData.UI.Form.InstallingEmulator = false;
                return;
            }

            MainFormTabControler.SelectedTab = TabDownloader;
            await InstallExpansionAsync(name, folder, isInstalled, setInstallStatus);
        }

        /// <summary>
        /// Performs the actual expansion installation.
        /// </summary>
        private async Task InstallExpansionAsync(
            string expansionName,
            string folderPath,
            bool isInstalled,
            Action<bool> setInstallStatus)
        {
            if (isInstalled)
            {
                AlertBox.Show(
                    string.Format(CultureInfo.InvariantCulture,
                        translator.Translate("AlerBoxEmulatroInstalled"),
                        $"{expansionName} Emulator"),
                    NotificationType.Info, settings);
                return;
            }

            _cancellationTokenSource = new CancellationTokenSource();

            // Wrapper that also sets the global installing flag
            Action<bool> setExpansionInstallingStatus = (isInstalling) =>
            {
                FormData.UI.Form.InstallingEmulator = isInstalling;
                setInstallStatus(isInstalling);
            };

            TrionLogger.Log($"installationName {expansionName}, apiKeyIdentifier: {expansionName.ToLower()}, " +
                $"destinationFolder: {folderPath}, title: {string.Format(CultureInfo.InvariantCulture, translator.Translate("LBLInstallEmulatorTitle"), expansionName)}");

            await PerformInstallationAsync(
                installationName: expansionName,
                apiKeyIdentifier: expansionName.ToLower(),
                destinationFolder: folderPath,
                title: string.Format(CultureInfo.InvariantCulture,
                    translator.Translate("LBLInstallEmulatorTitle"), expansionName),
                setInstallingStatus: setExpansionInstallingStatus,
                cancellationToken: _cancellationTokenSource.Token,
                deleteFilesAfterUnzip: true,
                showRemoveFilesCard: true);
        }

        #endregion

        #region Repair SPP
        // ─────────────────────────────────────────────────────────────────────

        /// <summary>
        /// Main repair button handler. Repairs the selected expansion pack.
        /// </summary>
        private async void BTNRepairSPP_Click(object sender, EventArgs e)
        {
            _cancellationTokenSource = new CancellationTokenSource();
            _cancellationToken = _cancellationTokenSource.Token;
            FormData.UI.Form.InstallingEmulator = true;

            try
            {
                switch (settings.SelectedSPP)
                {
                    case SPP.Classic:
                        await RepairExpansionAsync("Classic", "/classic", settings.ClassicInstalled);
                        break;
                    case SPP.TheBurningCrusade:
                        await RepairExpansionAsync("TBC", "/tbc", settings.TBCInstalled);
                        break;
                    case SPP.WrathOfTheLichKing:
                        await RepairExpansionAsync("WotLK", "/wotlk", settings.WotLKInstalled);
                        break;
                    case SPP.Cataclysm:
                        await RepairExpansionAsync("Cata", "/cata", settings.CataInstalled);
                        break;
                    case SPP.MistsOfPandaria:
                        await RepairExpansionAsync("MoP", "/mop", settings.MOPInstalled);
                        break;
                    default:
                        AlertBox.Show(translator.Translate("AlerBoxFailedGettingEmulator"), NotificationType.Info, settings);
                        break;
                }

                AlertBox.Show(translator.Translate("SPPRepairSuccess"), NotificationType.Info, settings);
            }
            catch (Exception ex)
            {
                AlertBox.Show($"An error occurred: {ex.Message}", NotificationType.Error, settings);
            }
            finally
            {
                FormData.UI.Form.InstallingEmulator = false;
            }
        }

        /// <summary>
        /// Repairs an expansion by comparing local files with server manifest
        /// and downloading any missing or corrupt files.
        /// </summary>
        private async Task RepairExpansionAsync(string expansionName, string folderPath, bool isInstalled)
        {
            if (!isInstalled)
            {
                AlertBox.Show(
                    string.Format(CultureInfo.InvariantCulture,
                        translator.Translate("AlerBoxEmulatroNotInstalled"),
                        $"{expansionName} Emulator"),
                    NotificationType.Info, settings);
                FormData.UI.Form.InstallingEmulator = false;
                return;
            }

            DLCardRemoweFiles.Visible = true;
            LBLInstallEmulatorTitle.Text = string.Format(CultureInfo.InvariantCulture,
                translator.Translate("LBLRepairEmulatorTitle"),
                $"{expansionName} Emulator");

            MainFormTabControler.SelectedTab = TabDownloader;
            await Task.Delay(1000);

            // Create progress reporters for UI updates
            var serverFilesProgress = new Progress<string>(msg =>
                LBLServerFiles.Text = string.Format(CultureInfo.InvariantCulture, translator.Translate("LBLServerFiles"), msg));
            var localFilesProgress = new Progress<string>(msg =>
                LBLLocalFiles.Text = string.Format(CultureInfo.InvariantCulture, translator.Translate("LBLLocalFiles"), msg));
            var filesToBeDeletedProgress = new Progress<string>(msg =>
                LBLFilesToBeRemoved.Text = string.Format(CultureInfo.InvariantCulture, translator.Translate("LBLFilesToBeRemoved"), msg));
            var filesToBeDownloadedProgress = new Progress<string>(msg =>
                LBLFilesToBeDownloaded.Text = string.Format(CultureInfo.InvariantCulture, translator.Translate("LBLFilesToBeDownloaded"), msg));
            var downloadSpeedProgress = new Progress<double>(speed =>
                LBLDownloadSpeed.Text = string.Format(CultureInfo.InvariantCulture, translator.Translate("LBLDownloadSpeed"), $"{speed:0.##} MB/s"));
            var downloadProgress = new Progress<double>(progress =>
                PBarCurrentDownlaod.Value = (int)progress);

            // Get server and local file lists concurrently
            var serverFilesTask = Task.Run(() =>
                NetworkManager.GetServerFiles(
                    Links.APIRequests.GetReapirFiles(expansionName.ToLower(), settings.SupporterKey),
                    serverFilesProgress));
            var localFilesTask = Task.Run(() =>
                FileManager.ProcessFilesAsync(Links.Install.WotLK, localFilesProgress));

            await Task.WhenAll(serverFilesTask, localFilesTask);

            var serverFiles = await serverFilesTask;
            var localFiles = await localFilesTask;

            // Compare and get missing/corrupt files
            var (missingFiles, filesToDelete) = await FileManager.CompareFiles(
                serverFiles, localFiles, folderPath,
                filesToBeDeletedProgress, filesToBeDownloadedProgress);

            PBARTotalDownload.Maximum = missingFiles.Count;

            // Download missing files
            foreach (var file in missingFiles)
            {
                LBLFileName.Text = string.Format(CultureInfo.InvariantCulture,
                    translator.Translate("LBLFileName"), file.Name);
                LBLFileSize.Text = string.Format(CultureInfo.InvariantCulture,
                    translator.Translate("LBLFileSize"), $"{file.Size:0.##} MB");

                await FileManager.DownloadFileAsync(
                    file, folderPath, expansionName, settings.SupporterKey,
                    _cancellationToken, downloadProgress, null, downloadSpeedProgress);

                PBARTotalDownload.Value++;
            }

            AlertBox.Show($"{expansionName} Repair Completed!", NotificationType.Success, settings);
        }

        #endregion

        #region Uninstall SPP
        // ─────────────────────────────────────────────────────────────────────

        /// <summary>
        /// Main uninstall button handler. Uninstalls the selected expansion.
        /// </summary>
        private async void BTNUninstallSPP_Click(object sender, EventArgs e)
        {
            AlertBox.Show(translator.Translate("SPPUninstall"), NotificationType.Info, settings);

            switch (settings.SelectedSPP)
            {
                case SPP.Classic:
                    await AppServiceManager.StartUninstall(settings.ClassicWorkingDirectory);
                    settings.ClassicInstalled = false;
                    settings.LaunchClassicCore = false;
                    break;
                case SPP.TheBurningCrusade:
                    await AppServiceManager.StartUninstall(settings.TBCWorkingDirectory);
                    settings.TBCInstalled = false;
                    settings.LaunchTBCCore = false;
                    break;
                case SPP.WrathOfTheLichKing:
                    await AppServiceManager.StartUninstall(settings.WotLKWorkingDirectory);
                    settings.WotLKInstalled = false;
                    settings.LaunchWotLKCore = false;
                    break;
                case SPP.Cataclysm:
                    await AppServiceManager.StartUninstall(settings.CataWorkingDirectory);
                    settings.CataInstalled = false;
                    settings.LaunchCataCore = false;
                    break;
                case SPP.MistsOfPandaria:
                    await AppServiceManager.StartUninstall(settings.MopWorkingDirectory);
                    settings.MOPInstalled = false;
                    settings.LaunchMoPCore = false;
                    break;
            }
        }

        #endregion

        #region Installation Core Logic
        // ─────────────────────────────────────────────────────────────────────

        /// <summary>
        /// Core installation routine that handles file downloading and extraction.
        /// Used by both fresh installs and database setup.
        /// </summary>
        private async Task PerformInstallationAsync(
            string installationName,
            string apiKeyIdentifier,
            string destinationFolder,
            string title,
            Action<bool> setInstallingStatus,
            CancellationToken cancellationToken,
            bool deleteFilesAfterUnzip = false,
            bool showRemoveFilesCard = false)
        {
            // Prepare download UI
            RefreshDownloader();
            CardLocalFiles.Visible = false;
            DLCardRemoweFiles.Visible = showRemoveFilesCard;
            LBLInstallEmulatorTitle.Text = title;
            Update();

            // Special handling for database installation
            if (installationName.Contains("database"))
            {
                LBLFilesToBeRemoved.Text = translator.Translate("LBLInitDatabaseWaiting");
                DLCardRemoweFiles.Visible = true;
            }

            await Task.Delay(1000);
            setInstallingStatus(true);

            // Create progress reporters
            var serverFilesProgress = new Progress<string>(msg =>
                LBLServerFiles.Text = string.Format(CultureInfo.InvariantCulture, translator.Translate("LBLServerFiles"), msg));
            var downloadSpeedProgress = new Progress<double>(speed =>
                LBLDownloadSpeed.Text = string.Format(CultureInfo.InvariantCulture, translator.Translate("LBLDownloadSpeed"), $"{speed:0.##} MB/s"));
            var downloadProgress = new Progress<double>(progress =>
                PBarCurrentDownlaod.Value = (int)progress);

            // Get file list from server
            var serverFiles = await Task.Run(() =>
                NetworkManager.GetServerFiles(
                    Links.APIRequests.GetInstallFiles(apiKeyIdentifier, settings.SupporterKey),
                    serverFilesProgress));

            // Configure progress bar (2 steps per file: download + unzip)
            PBARTotalDownload.Maximum = serverFiles.Count * 2;
            LBLFilesToBeDownloaded.Text = string.Format(CultureInfo.InvariantCulture,
                translator.Translate("LBLFilesToBeDownloaded"), serverFiles.Count);

            // Download and extract each file
            foreach (var file in serverFiles)
            {
                if (cancellationToken.IsCancellationRequested) break;

                LBLFileName.Text = string.Format(CultureInfo.InvariantCulture,
                    translator.Translate("LBLFileName"), file.Name);
                LBLFileSize.Text = string.Format(CultureInfo.InvariantCulture,
                    translator.Translate("LBLFileSize"), $"{file.Size:0.##} MB");

                // Download step
                LBLCurrentDownload.Text = translator.Translate("LBLCurrentDownload");
                await FileManager.DownloadFileAsync(
                    file, destinationFolder, installationName, settings.SupporterKey,
                    cancellationToken, downloadProgress, null, downloadSpeedProgress);
                PBARTotalDownload.Value++;

                if (cancellationToken.IsCancellationRequested) break;

                // Extract step
                LBLCurrentDownload.Text = translator.Translate("LBLCurrenInstall");
                await FileManager.UnzipFileAsync(
                    file, destinationFolder, cancellationToken,
                    downloadProgress, null, downloadSpeedProgress);
                PBARTotalDownload.Value++;
            }

            // Clean up downloaded archives if requested
            if (deleteFilesAfterUnzip && !cancellationToken.IsCancellationRequested)
            {
                await FileManager.DeleteInstallFiles(serverFiles, destinationFolder);
            }

            InstallFinished();
            setInstallingStatus(false);
        }

        #endregion

        #region SPP Version Selection
        // ─────────────────────────────────────────────────────────────────────

        /// <summary>
        /// Handles SPP version combo box selection change.
        /// </summary>
        private void CBOXSPPVersion_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selected = CBOXSPPVersion.SelectedItem?.ToString();

            if (selected == translator.Translate("SPPver1"))
                settings.SelectedSPP = SPP.Classic;
            else if (selected == translator.Translate("SPPver2"))
                settings.SelectedSPP = SPP.TheBurningCrusade;
            else if (selected == translator.Translate("SPPver3"))
                settings.SelectedSPP = SPP.WrathOfTheLichKing;
            else if (selected == translator.Translate("SPPver4"))
                settings.SelectedSPP = SPP.Cataclysm;
            else if (selected == translator.Translate("SPPver5"))
                settings.SelectedSPP = SPP.MistsOfPandaria;
        }

        #endregion
    }
}
