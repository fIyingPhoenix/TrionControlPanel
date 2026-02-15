// =============================================================================
// MainForm.Events.cs
// Purpose: Event subscription and handlers for the centralized event system
// Related UI: All UI elements that react to state changes
// Dependencies: AppEvents, ServerStatusChangedEventArgs, NotificationEventArgs
// Step 13 of IMPROVEMENTS.md - Implement Event-Based UI Updates
// =============================================================================

using TrionControlPanel.Desktop.Extensions.Classes.Data.Form;
using TrionControlPanel.Desktop.Extensions.Events;
using TrionControlPanel.Desktop.Extensions.Notification;
using TrionControlPanel.Desktop.Properties;
using static TrionControlPanel.Desktop.Extensions.Modules.Enums;
using static TrionControlPanel.Desktop.Extensions.Notification.AlertBox;

namespace TrionControlPanelDesktop
{
    /// <summary>
    /// Partial class containing event subscription and handler methods.
    /// Centralizes all reactions to application-wide events for cleaner code.
    /// </summary>
    public partial class MainForm
    {
        #region Event Subscription
        // ─────────────────────────────────────────────────────────────────────

        /// <summary>
        /// Subscribes to all application-wide events.
        /// Call this during form initialization.
        /// </summary>
        private void SubscribeToAppEvents()
        {
            AppEvents.ServerStatusChanged += OnServerStatusChanged;
            AppEvents.NotificationRequested += OnNotificationRequested;
            AppEvents.SettingsChanged += OnSettingsChanged;
            AppEvents.InstallationProgress += OnInstallationProgress;
            AppEvents.ResourceUsageUpdated += OnResourceUsageUpdated;
        }

        /// <summary>
        /// Unsubscribes from all application-wide events.
        /// Call this during form disposal to prevent memory leaks.
        /// </summary>
        private void UnsubscribeFromAppEvents()
        {
            AppEvents.ServerStatusChanged -= OnServerStatusChanged;
            AppEvents.NotificationRequested -= OnNotificationRequested;
            AppEvents.SettingsChanged -= OnSettingsChanged;
            AppEvents.InstallationProgress -= OnInstallationProgress;
            AppEvents.ResourceUsageUpdated -= OnResourceUsageUpdated;
        }

        #endregion

        #region Server Status Event Handlers
        // ─────────────────────────────────────────────────────────────────────

        /// <summary>
        /// Handles server status change events.
        /// Updates the appropriate UI elements based on which server changed.
        /// </summary>
        private void OnServerStatusChanged(object? sender, ServerStatusChangedEventArgs e)
        {
            // Ensure we're on the UI thread
            if (InvokeRequired)
            {
                Invoke(() => OnServerStatusChanged(sender, e));
                return;
            }

            switch (e.ServerType)
            {
                case ServerType.Database:
                    UpdateDatabaseServerUI(e.IsRunning);
                    break;

                case ServerType.World:
                    UpdateWorldServerUI(e.IsRunning, e.Expansion);
                    break;

                case ServerType.Logon:
                    UpdateLogonServerUI(e.IsRunning, e.Expansion);
                    break;
            }
        }

        /// <summary>
        /// Updates the database server UI elements.
        /// </summary>
        private void UpdateDatabaseServerUI(bool isRunning)
        {
            // Update button text
            BTNStartDatabase.Text = isRunning
                ? translator.Translate("BTNStartDatabaseTextON")
                : translator.Translate("BTNStartDatabaseTextOFF");

            // Update status panel
            if (isRunning)
            {
                SetServerPanelOnline(PNLDatanasServerStatus, PICDatabaseServerStatus);
            }
            else
            {
                SetServerPanelOffline(PNLDatanasServerStatus, PICDatabaseServerStatus);
                LBLUpTimeDatabase.Text = $"{translator.Translate("LBLUpTime")}: 0D : 0H : 0M : 0S";
            }
        }

        /// <summary>
        /// Updates the world server UI elements.
        /// </summary>
        private void UpdateWorldServerUI(bool isRunning, SPP? expansion)
        {
            // Update main button text
            BTNStartWorld.Text = isRunning
                ? translator.Translate("BTNStartWorldTextON")
                : translator.Translate("BTNStartWorldTextOFF");

            // Update status panel
            if (isRunning)
            {
                SetServerPanelOnline(PNLWorldServerStatus, PICWorldServerStatus);
            }
            else
            {
                SetServerPanelOffline(PNLWorldServerStatus, PICWorldServerStatus);
                LBLUpTimeWorld.Text = $"{translator.Translate("LBLUpTime")}: 0D : 0H : 0M : 0S";
            }
        }

        /// <summary>
        /// Updates the logon server UI elements.
        /// </summary>
        private void UpdateLogonServerUI(bool isRunning, SPP? expansion)
        {
            // Update main button text
            BTNStartLogon.Text = isRunning
                ? translator.Translate("BTNStartLogonTextON")
                : translator.Translate("BTNStartLogonTextOFF");

            // Update status panel
            if (isRunning)
            {
                SetServerPanelOnline(PNLLogonServerStatus, PICLogonServerStatus);
            }
            else
            {
                SetServerPanelOffline(PNLLogonServerStatus, PICLogonServerStatus);
                LBLUpTimeLogon.Text = $"{translator.Translate("LBLUpTime")}: 0D : 0H : 0M : 0S";
            }
        }

        #endregion

        #region Notification Event Handlers
        // ─────────────────────────────────────────────────────────────────────

        private int _notificationId;

        /// <summary>
        /// Handles notification request events.
        /// Displays the notification using the AlertBox system and logs it to the grid.
        /// </summary>
        private void OnNotificationRequested(object? sender, NotificationEventArgs e)
        {
            // Ensure we're on the UI thread
            if (InvokeRequired)
            {
                Invoke(() => OnNotificationRequested(sender, e));
                return;
            }

            // Display the notification using the existing AlertBox
            AlertBox.Show(e.Message, e.Type, settings);

            // Add to notification history grid
            _notificationId++;
            DGVNotifications.Rows.Insert(0, _notificationId, $"[{e.Type}] {e.Message}", e.Timestamp.ToString("yyyy-MM-dd HH:mm:ss"));
            BTNClean.NotificationCount = DGVNotifications.Rows.Count;
        }

        /// <summary>
        /// Clears all notification history from the grid.
        /// </summary>
        private void BTNClean_Click(object sender, EventArgs e)
        {
            DGVNotifications.Rows.Clear();
            _notificationId = 0;
            BTNClean.NotificationCount = 0;
        }

        #endregion

        #region Settings Event Handlers
        // ─────────────────────────────────────────────────────────────────────

        /// <summary>
        /// Handles settings change events.
        /// Refreshes the UI to reflect the new settings.
        /// </summary>
        private void OnSettingsChanged(object? sender, TrionControlPanel.Desktop.Extensions.Modules.Lists.AppSettings e)
        {
            // Ensure we're on the UI thread
            if (InvokeRequired)
            {
                Invoke(() => OnSettingsChanged(sender, e));
                return;
            }

            // Store the new settings
            settings = e;

            // Refresh UI elements that depend on settings
            LoadSettingsUI();
            LoadSkin();
        }

        #endregion

        #region Installation Progress Event Handlers
        // ─────────────────────────────────────────────────────────────────────

        /// <summary>
        /// Handles installation progress events.
        /// Updates the downloader UI with progress information.
        /// </summary>
        private void OnInstallationProgress(object? sender, AppEvents.InstallationProgressEventArgs e)
        {
            // Ensure we're on the UI thread
            if (InvokeRequired)
            {
                Invoke(() => OnInstallationProgress(sender, e));
                return;
            }

            // Update progress bar
            PBarCurrentDownlaod.Value = (int)e.ProgressPercent;

            // Update status label
            LBLCurrentDownload.Text = e.StatusMessage;

            // Update title with installation name
            LBLInstallEmulatorTitle.Text = e.InstallationName;

            // Handle completion
            if (e.IsComplete)
            {
                if (e.IsSuccess)
                {
                    AppEvents.NotifySuccess($"{e.InstallationName} completed successfully!");
                }
                else
                {
                    AppEvents.NotifyError($"{e.InstallationName} failed!");
                }

                // Reset progress
                PBarCurrentDownlaod.Value = 0;
            }
        }

        #endregion

        #region Resource Usage Event Handlers
        // ─────────────────────────────────────────────────────────────────────

        /// <summary>
        /// Handles resource usage update events.
        /// Updates the appropriate progress bars based on the data source.
        /// </summary>
        private void OnResourceUsageUpdated(object? sender, AppEvents.ResourceUsageEventArgs e)
        {
            // Ensure we're on the UI thread
            if (InvokeRequired)
            {
                Invoke(() => OnResourceUsageUpdated(sender, e));
                return;
            }

            // Determine which progress bars to update based on server type
            if (e.ServerType == null || e.ProcessId == 0)
            {
                // System-wide resources
                PbarCPUMachineResources.Value = Math.Min(e.CpuPercent, 100);
                PbarRAMMachineResources.Value = Math.Min(e.RamUsageMB, PbarRAMMachineResources.Maximum);
            }
            else
            {
                switch (e.ServerType)
                {
                    case ServerType.World:
                        PbarCPUWordResources.Value = Math.Min(e.CpuPercent, 100);
                        PbarRAMWordResources.Value = Math.Min(e.RamUsageMB, PbarRAMWordResources.Maximum);
                        break;

                    case ServerType.Logon:
                        PbarCPULogonResources.Value = Math.Min(e.CpuPercent, 100);
                        PbarRAMLogonResources.Value = Math.Min(e.RamUsageMB, PbarRAMLogonResources.Maximum);
                        break;
                }
            }
        }

        #endregion

        #region Event-Based UI Update Helpers
        // ─────────────────────────────────────────────────────────────────────

        /// <summary>
        /// Helper method to request a notification via the event system.
        /// This can be called from anywhere in MainForm to show a notification.
        /// </summary>
        /// <param name="message">The message to display.</param>
        /// <param name="type">The notification type.</param>
        protected void ShowNotification(string message, NotificationType type)
        {
            AppEvents.RaiseNotification(new NotificationEventArgs(message, type));
        }

        /// <summary>
        /// Helper method to notify that the database server status changed.
        /// </summary>
        protected void NotifyDatabaseStatusChanged(bool isRunning, int? processId = null)
        {
            AppEvents.RaiseDatabaseStatusChanged(isRunning, processId);
        }

        /// <summary>
        /// Helper method to notify that a world server status changed.
        /// </summary>
        protected void NotifyWorldStatusChanged(SPP expansion, bool isRunning, int? processId = null)
        {
            AppEvents.RaiseWorldStatusChanged(expansion, isRunning, processId);
        }

        /// <summary>
        /// Helper method to notify that a logon server status changed.
        /// </summary>
        protected void NotifyLogonStatusChanged(SPP expansion, bool isRunning, int? processId = null)
        {
            AppEvents.RaiseLogonStatusChanged(expansion, isRunning, processId);
        }

        #endregion
    }
}
