// =============================================================================
// ServerStatusChangedEventArgs.cs
// Purpose: Event arguments for server status change notifications
// Used by: AppEvents, MainForm, ServerMonitor
// Step 13 of IMPROVEMENTS.md - Implement Event-Based UI Updates
// =============================================================================

using static TrionControlPanel.Desktop.Extensions.Modules.Enums;

namespace TrionControlPanel.Desktop.Extensions.Events
{
    /// <summary>
    /// Event arguments for server status change notifications.
    /// Contains information about which server changed and its new state.
    /// </summary>
    public class ServerStatusChangedEventArgs : EventArgs
    {
        #region Properties
        // ─────────────────────────────────────────────────────────────────────

        /// <summary>
        /// Gets the type of server that changed (Database, World, or Logon).
        /// </summary>
        public ServerType ServerType { get; }

        /// <summary>
        /// Gets the expansion this server belongs to, if applicable.
        /// Null for database server which is shared across expansions.
        /// </summary>
        public SPP? Expansion { get; }

        /// <summary>
        /// Gets whether the server is currently running.
        /// </summary>
        public bool IsRunning { get; }

        /// <summary>
        /// Gets the process ID of the running server, if available.
        /// </summary>
        public int? ProcessId { get; }

        /// <summary>
        /// Gets the timestamp when this status change occurred.
        /// </summary>
        public DateTime Timestamp { get; }

        /// <summary>
        /// Gets the uptime of the server, if running.
        /// </summary>
        public TimeSpan? Uptime { get; }

        #endregion

        #region Constructors
        // ─────────────────────────────────────────────────────────────────────

        /// <summary>
        /// Initializes a new instance of ServerStatusChangedEventArgs.
        /// </summary>
        /// <param name="serverType">The type of server that changed.</param>
        /// <param name="expansion">The expansion this server belongs to (null for database).</param>
        /// <param name="isRunning">Whether the server is running.</param>
        /// <param name="processId">The process ID if running.</param>
        /// <param name="uptime">The server uptime if running.</param>
        public ServerStatusChangedEventArgs(
            ServerType serverType,
            SPP? expansion,
            bool isRunning,
            int? processId = null,
            TimeSpan? uptime = null)
        {
            ServerType = serverType;
            Expansion = expansion;
            IsRunning = isRunning;
            ProcessId = processId;
            Timestamp = DateTime.Now;
            Uptime = uptime;
        }

        #endregion

        #region Methods
        // ─────────────────────────────────────────────────────────────────────

        /// <summary>
        /// Returns a string representation of the event args for logging.
        /// </summary>
        public override string ToString()
        {
            string expansionStr = Expansion?.ToString() ?? "N/A";
            string pidStr = ProcessId?.ToString() ?? "N/A";
            string status = IsRunning ? "Running" : "Stopped";
            return $"[{ServerType}] Expansion: {expansionStr}, Status: {status}, PID: {pidStr}";
        }

        #endregion
    }
}
