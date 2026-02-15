// =============================================================================
// MainForm.Monitoring.cs
// Purpose: Resource usage monitoring (CPU, RAM) and process ID display
// Related UI: Progress bars, process ID labels, resource cards on Home page
// Dependencies: PerformanceMonitor, SystemData
// =============================================================================

using MetroFramework.Controls;
using TrionControlPanel.Desktop.Extensions.Application;
using TrionControlPanel.Desktop.Extensions.Classes.Data.Form;
using TrionControlPanel.Desktop.Extensions.Classes.Monitor;

namespace TrionControlPanelDesktop
{
    /// <summary>
    /// Partial class containing resource monitoring and process tracking logic.
    /// Handles CPU/RAM usage display and process ID management.
    /// </summary>
    public partial class MainForm
    {
        #region Process ID Display
        // ─────────────────────────────────────────────────────────────────────

        /// <summary>
        /// Updates the process ID labels for all server types.
        /// Displays comma-separated list of active process IDs or "0" if none.
        /// </summary>
        /// <remarks>
        /// Process IDs are tracked in SystemData and can be cycled through
        /// by clicking on the label (see InitializeCustomUX in MainForm.Helpers.cs).
        /// </remarks>
        private void ProcessesIDUpdate()
        {
            // Database process IDs
            var databaseProcessIDs = SystemData.GetTotalDatabaseProcessIDCount() > 0
                ? string.Join(", ", SystemData.GetDatabaseProcessID().Select(p => p.ID))
                : "0";
            LBLDatabaseProcessID.Text = $"{translator.Translate("LBLProcessID")}: {databaseProcessIDs}";

            // World server process IDs
            var worldProcessIDs = SystemData.GetTotalWorldProcessIDCount() > 0
                ? string.Join(", ", SystemData.GetWorldProcessesID().Select(p => p.ID))
                : "0";
            LBLWorldProcessID.Text = $"{translator.Translate("LBLProcessID")}: {worldProcessIDs}";

            // Logon server process IDs
            var logonProcessIDs = SystemData.GetTotalLogonProcessIDCount() > 0
                ? string.Join(", ", SystemData.GetLogonProcessesID().Select(p => p.ID))
                : "0";
            LBLLogonProcessID.Text = $"{translator.Translate("LBLProcessID")}: {logonProcessIDs}";
        }

        #endregion

        #region Resource Usage Monitoring
        // ─────────────────────────────────────────────────────────────────────

        /// <summary>
        /// Updates resource usage progress bars for machine, world, and logon processes.
        /// Fetches CPU and RAM usage asynchronously to prevent UI freezing.
        /// </summary>
        /// <remarks>
        /// For world and logon servers, monitors the currently selected process
        /// based on the current page (_worldCurrentPage, _logonCurrentPage).
        /// This allows cycling through multiple server instances.
        /// </remarks>
        private async void ResurceUsage()
        {
            // ── Machine Resources ──
            // Overall system CPU usage
            PbarCPUMachineResources.Value = await PerformanceMonitor.GetCpuUtilizationPercentageAsync();

            // Available RAM (total - used)
            PbarRAMMachineResources.Value = await Task.Run(() =>
                PerformanceMonitor.GetTotalRamInMB() - PerformanceMonitor.GetCurentPcRamUsage());

            // Set max values for server progress bars based on available RAM
            PbarRAMLogonResources.Maximum = PbarRAMMachineResources.Value;
            PbarRAMWordResources.Maximum = PbarRAMMachineResources.Value;

            // ── World Server Resources ──
            if (SystemData.GetTotalWorldProcessIDCount() > 0)
            {
                // Get the current page of world processes (supports multiple instances)
                var worldProcess = SystemData.GetWorldProcessesIDPage(_worldCurrentPage, AppPageSize);

                foreach (var process in worldProcess)
                {
                    if (process.ID == 0) break;

                    PbarRAMWordResources.Value = await Task.Run(() =>
                        PerformanceMonitor.ApplicationRamUsage(process.ID));

                    PbarCPUWordResources.Value = await PerformanceMonitor.ApplicationCpuUsageAsync(process.ID);
                }
            }

            // ── Logon Server Resources ──
            if (SystemData.GetTotalLogonProcessIDCount() > 0)
            {
                // Get the current page of logon processes
                var logonProcess = SystemData.GetLogonProcessesIDPage(_logonCurrentPage, AppPageSize);

                foreach (var process in logonProcess)
                {
                    if (process.ID == 0) break;

                    PbarRAMLogonResources.Value = await Task.Run(() =>
                        PerformanceMonitor.ApplicationRamUsage(process.ID));

                    PbarCPULogonResources.Value = await PerformanceMonitor.ApplicationCpuUsageAsync(process.ID);
                }
            }
        }

        #endregion

        #region Timer Event Handlers
        // ─────────────────────────────────────────────────────────────────────

        /// <summary>
        /// Main watcher timer tick handler.
        /// Periodically updates UI state based on server status.
        /// </summary>
        /// <remarks>
        /// This timer runs frequently to keep the UI synchronized with actual server states.
        /// Also handles crash detection if enabled in settings.
        /// </remarks>
        private async void TimerWacher_Tick(object sender, EventArgs e)
        {
            // Update UI state
            EnableLaunchButtonInstalled();
            ToogleButtons();
            ResurceUsage();
            ProcessesIDUpdate();
            RunServerUpdate();

            // Check server running states asynchronously
            await ServerMonitor.ServerRunningLogonAsync();
            await ServerMonitor.ServerRunningWorldAsync();
            await ServerMonitor.ServerRunningDatabaseAsync();

            // Handle crash detection and auto-restart if enabled
            if (settings.ServerCrashDetection)
            {
                await AppExecuteManager.CheckAndRestartServers(settings);
            }
        }

        /// <summary>
        /// Update panel animation timer tick handler.
        /// Flashes update indicators when updates are available.
        /// </summary>
        /// <remarks>
        /// Creates a blinking border effect on panels that have updates available.
        /// Updates the download button text based on whether updates are pending.
        /// </remarks>
        private void TimerPanelAnimation_Tick(object sender, EventArgs e)
        {
            // Trion application update indicator
            AnimateUpdatePanel(
                FormData.UI.Version.Update.Trion,
                PNLUpdateTrion,
                "BTNDownloadUpdatesOn",
                "BTNDownloadUpdatesOff");

            // Database update indicator
            AnimateUpdatePanel(
                FormData.UI.Version.Update.Database,
                PNLUpdateDatabase,
                "BTNDownloadUpdatesOn",
                "BTNDownloadUpdatesOff");

            // Classic SPP update indicator
            AnimateUpdatePanel(
                FormData.UI.Version.Update.Classic,
                PNLUpdateClassicSPP,
                "BTNDownloadUpdatesOn",
                "BTNDownloadUpdatesOff");

            // TBC SPP update indicator
            AnimateUpdatePanel(
                FormData.UI.Version.Update.TBC,
                PNLUpdateTbcSPP,
                "BTNDownloadUpdatesOn",
                "BTNDownloadUpdatesOff");

            // WotLK SPP update indicator
            AnimateUpdatePanel(
                FormData.UI.Version.Update.WotLK,
                PNLUpdateWotlkSpp,
                "BTNDownloadUpdatesOn",
                "BTNDownloadUpdatesOff");

            // Cataclysm SPP update indicator
            AnimateUpdatePanel(
                FormData.UI.Version.Update.Cata,
                PNLUpdateCataSPP,
                "BTNDownloadUpdatesOn",
                "BTNDownloadUpdatesOff");

            // MoP SPP update indicator
            AnimateUpdatePanel(
                FormData.UI.Version.Update.Mop,
                PNLUpdateMopSPP,
                "BTNDownloadUpdatesOn",
                "BTNDownloadUpdatesOff");
        }

        /// <summary>
        /// Helper method to animate an update panel's border.
        /// </summary>
        /// <param name="hasUpdate">Whether an update is available</param>
        /// <param name="panel">The MetroPanel to animate</param>
        /// <param name="onTextKey">Translation key for button text when update available</param>
        /// <param name="offTextKey">Translation key for button text when no update</param>
        private void AnimateUpdatePanel(
            bool hasUpdate,
            MetroPanel panel,
            string onTextKey,
            string offTextKey)
        {
            if (hasUpdate)
            {
                // Toggle border color between green and black for blinking effect
                panel.BorderColor = panel.BorderColor == Color.LimeGreen
                    ? Color.Black
                    : Color.LimeGreen;
                panel.Refresh();
                BTNDownloadUpdates.Text = translator.Translate(onTextKey);
            }
            else
            {
                panel.BorderColor = Color.Black;
                panel.Refresh();
                BTNDownloadUpdates.Text = translator.Translate(offTextKey);
            }
        }

        #endregion
    }
}
