// =============================================================================
// AppEvents.cs
// Purpose: Centralized event hub for application-wide notifications
// Used by: MainForm, AppExecuteManager, ServerMonitor, all services
// Step 13 of IMPROVEMENTS.md - Implement Event-Based UI Updates
// =============================================================================

using TrionControlPanel.Desktop.Extensions.Classes.Monitor;
using TrionControlPanel.Desktop.Extensions.Modules.Lists;
using static TrionControlPanel.Desktop.Extensions.Modules.Enums;

namespace TrionControlPanel.Desktop.Extensions.Events
{
    /// <summary>
    /// Centralized event hub for application-wide notifications.
    /// Provides a decoupled way to communicate state changes between components.
    /// </summary>
    /// <remarks>
    /// Usage pattern:
    /// 1. Subscribe to events in UI components (MainForm)
    /// 2. Raise events from services when state changes
    /// 3. UI components handle events with thread-safe Invoke calls
    ///
    /// This pattern decouples business logic from UI updates and makes
    /// the codebase more maintainable and testable.
    /// </remarks>
    public static class AppEvents
    {
        #region Server Status Events
        // ─────────────────────────────────────────────────────────────────────

        /// <summary>
        /// Raised when any server's running status changes (started/stopped).
        /// </summary>
        /// <remarks>
        /// Subscribers should check the ServerType property to determine
        /// which server changed and update the appropriate UI elements.
        /// </remarks>
        public static event EventHandler<ServerStatusChangedEventArgs>? ServerStatusChanged;

        /// <summary>
        /// Raises the ServerStatusChanged event.
        /// </summary>
        /// <param name="serverType">The type of server (Database, World, Logon).</param>
        /// <param name="expansion">The expansion for World/Logon servers, null for Database.</param>
        /// <param name="isRunning">True if the server is now running.</param>
        /// <param name="processId">The process ID if running.</param>
        /// <param name="uptime">The server uptime if running.</param>
        public static void RaiseServerStatusChanged(
            ServerType serverType,
            SPP? expansion,
            bool isRunning,
            int? processId = null,
            TimeSpan? uptime = null)
        {
            TrionLogger.Debug($"Event: ServerStatusChanged | {serverType} | {expansion?.ToString() ?? "N/A"} | Running: {isRunning} | PID: {processId?.ToString() ?? "N/A"}");
            ServerStatusChanged?.Invoke(null, new ServerStatusChangedEventArgs(
                serverType, expansion, isRunning, processId, uptime));
        }

        /// <summary>
        /// Convenience method to raise a Database server status change.
        /// </summary>
        public static void RaiseDatabaseStatusChanged(bool isRunning, int? processId = null)
        {
            RaiseServerStatusChanged(ServerType.Database, null, isRunning, processId);
        }

        /// <summary>
        /// Convenience method to raise a World server status change.
        /// </summary>
        public static void RaiseWorldStatusChanged(SPP expansion, bool isRunning, int? processId = null)
        {
            RaiseServerStatusChanged(ServerType.World, expansion, isRunning, processId);
        }

        /// <summary>
        /// Convenience method to raise a Logon server status change.
        /// </summary>
        public static void RaiseLogonStatusChanged(SPP expansion, bool isRunning, int? processId = null)
        {
            RaiseServerStatusChanged(ServerType.Logon, expansion, isRunning, processId);
        }

        #endregion

        #region Notification Events
        // ─────────────────────────────────────────────────────────────────────

        /// <summary>
        /// Raised when a notification should be displayed to the user.
        /// </summary>
        public static event EventHandler<NotificationEventArgs>? NotificationRequested;

        /// <summary>
        /// Raises the NotificationRequested event.
        /// </summary>
        /// <param name="args">The notification event arguments.</param>
        public static void RaiseNotification(NotificationEventArgs args)
        {
            TrionLogger.Debug($"Event: Notification | {args}");
            NotificationRequested?.Invoke(null, args);
        }

        /// <summary>
        /// Convenience method to raise an info notification.
        /// </summary>
        public static void NotifyInfo(string message, string? title = null)
        {
            RaiseNotification(NotificationEventArgs.Info(message, title));
        }

        /// <summary>
        /// Convenience method to raise a success notification.
        /// </summary>
        public static void NotifySuccess(string message, string? title = null)
        {
            RaiseNotification(NotificationEventArgs.Success(message, title));
        }

        /// <summary>
        /// Convenience method to raise a warning notification.
        /// </summary>
        public static void NotifyWarning(string message, string? title = null)
        {
            RaiseNotification(NotificationEventArgs.Warning(message, title));
        }

        /// <summary>
        /// Convenience method to raise an error notification.
        /// </summary>
        public static void NotifyError(string message, string? title = null)
        {
            RaiseNotification(NotificationEventArgs.Error(message, title));
        }

        #endregion

        #region Settings Events
        // ─────────────────────────────────────────────────────────────────────

        /// <summary>
        /// Raised when application settings are modified and saved.
        /// </summary>
        public static event EventHandler<AppSettings>? SettingsChanged;

        /// <summary>
        /// Raises the SettingsChanged event.
        /// </summary>
        /// <param name="settings">The updated settings object.</param>
        public static void RaiseSettingsChanged(AppSettings settings)
        {
            TrionLogger.Debug("Event: SettingsChanged");
            SettingsChanged?.Invoke(null, settings);
        }

        #endregion

        #region Installation Events
        // ─────────────────────────────────────────────────────────────────────

        /// <summary>
        /// Event arguments for installation progress updates.
        /// </summary>
        public class InstallationProgressEventArgs : EventArgs
        {
            /// <summary>Gets the name of what is being installed.</summary>
            public string InstallationName { get; }

            /// <summary>Gets the current progress percentage (0-100).</summary>
            public double ProgressPercent { get; }

            /// <summary>Gets the current status message.</summary>
            public string StatusMessage { get; }

            /// <summary>Gets whether the installation is complete.</summary>
            public bool IsComplete { get; }

            /// <summary>Gets whether the installation was successful (only valid when IsComplete).</summary>
            public bool IsSuccess { get; }

            public InstallationProgressEventArgs(
                string installationName,
                double progressPercent,
                string statusMessage,
                bool isComplete = false,
                bool isSuccess = false)
            {
                InstallationName = installationName;
                ProgressPercent = progressPercent;
                StatusMessage = statusMessage;
                IsComplete = isComplete;
                IsSuccess = isSuccess;
            }
        }

        /// <summary>
        /// Raised when installation progress updates.
        /// </summary>
        public static event EventHandler<InstallationProgressEventArgs>? InstallationProgress;

        /// <summary>
        /// Raises the InstallationProgress event.
        /// </summary>
        public static void RaiseInstallationProgress(
            string installationName,
            double progressPercent,
            string statusMessage,
            bool isComplete = false,
            bool isSuccess = false)
        {
            if (isComplete)
            {
                TrionLogger.Info($"Event: InstallationComplete | {installationName} | Success: {isSuccess} | {statusMessage}");
            }

            InstallationProgress?.Invoke(null, new InstallationProgressEventArgs(
                installationName, progressPercent, statusMessage, isComplete, isSuccess));
        }

        #endregion

        #region Resource Monitoring Events
        // ─────────────────────────────────────────────────────────────────────

        /// <summary>
        /// Event arguments for resource usage updates.
        /// </summary>
        public class ResourceUsageEventArgs : EventArgs
        {
            /// <summary>Gets the CPU usage percentage.</summary>
            public int CpuPercent { get; }

            /// <summary>Gets the RAM usage in MB.</summary>
            public int RamUsageMB { get; }

            /// <summary>Gets the total RAM in MB.</summary>
            public int TotalRamMB { get; }

            /// <summary>Gets the process ID this data belongs to (0 for system-wide).</summary>
            public int ProcessId { get; }

            /// <summary>Gets the server type this data belongs to.</summary>
            public ServerType? ServerType { get; }

            public ResourceUsageEventArgs(
                int cpuPercent,
                int ramUsageMB,
                int totalRamMB,
                int processId = 0,
                ServerType? serverType = null)
            {
                CpuPercent = cpuPercent;
                RamUsageMB = ramUsageMB;
                TotalRamMB = totalRamMB;
                ProcessId = processId;
                ServerType = serverType;
            }
        }

        /// <summary>
        /// Raised when resource usage data is updated.
        /// </summary>
        public static event EventHandler<ResourceUsageEventArgs>? ResourceUsageUpdated;

        /// <summary>
        /// Raises the ResourceUsageUpdated event.
        /// </summary>
        public static void RaiseResourceUsageUpdated(
            int cpuPercent,
            int ramUsageMB,
            int totalRamMB,
            int processId = 0,
            ServerType? serverType = null)
        {
            ResourceUsageUpdated?.Invoke(null, new ResourceUsageEventArgs(
                cpuPercent, ramUsageMB, totalRamMB, processId, serverType));
        }

        #endregion

        #region Utility Methods
        // ─────────────────────────────────────────────────────────────────────

        /// <summary>
        /// Clears all event subscriptions.
        /// Call this when the application is shutting down to prevent memory leaks.
        /// </summary>
        public static void ClearAllSubscriptions()
        {
            TrionLogger.Debug("All event subscriptions cleared");
            ServerStatusChanged = null;
            NotificationRequested = null;
            SettingsChanged = null;
            InstallationProgress = null;
            ResourceUsageUpdated = null;
        }

        #endregion
    }
}
