// =============================================================================
// NotificationEventArgs.cs
// Purpose: Event arguments for notification display requests
// Used by: AppEvents, MainForm, AlertBox
// Step 13 of IMPROVEMENTS.md - Implement Event-Based UI Updates
// =============================================================================

using static TrionControlPanel.Desktop.Extensions.Notification.AlertBox;

namespace TrionControlPanel.Desktop.Extensions.Events
{
    /// <summary>
    /// Event arguments for notification display requests.
    /// Used to trigger toast/alert notifications from any part of the application.
    /// </summary>
    public class NotificationEventArgs : EventArgs
    {
        #region Properties
        // ─────────────────────────────────────────────────────────────────────

        /// <summary>
        /// Gets the notification message to display.
        /// </summary>
        public string Message { get; }

        /// <summary>
        /// Gets the type of notification (Info, Success, Warning, Error).
        /// </summary>
        public NotificationType Type { get; }

        /// <summary>
        /// Gets the timestamp when this notification was requested.
        /// </summary>
        public DateTime Timestamp { get; }

        /// <summary>
        /// Gets the optional title for the notification.
        /// </summary>
        public string? Title { get; }

        /// <summary>
        /// Gets the duration in milliseconds to display the notification.
        /// Null means use the default duration.
        /// </summary>
        public int? DurationMs { get; }

        #endregion

        #region Constructors
        // ─────────────────────────────────────────────────────────────────────

        /// <summary>
        /// Initializes a new instance of NotificationEventArgs.
        /// </summary>
        /// <param name="message">The notification message.</param>
        /// <param name="type">The notification type.</param>
        /// <param name="title">Optional title for the notification.</param>
        /// <param name="durationMs">Optional display duration in milliseconds.</param>
        public NotificationEventArgs(
            string message,
            NotificationType type,
            string? title = null,
            int? durationMs = null)
        {
            Message = message ?? throw new ArgumentNullException(nameof(message));
            Type = type;
            Title = title;
            DurationMs = durationMs;
            Timestamp = DateTime.Now;
        }

        #endregion

        #region Factory Methods
        // ─────────────────────────────────────────────────────────────────────

        /// <summary>
        /// Creates an info notification.
        /// </summary>
        public static NotificationEventArgs Info(string message, string? title = null)
            => new(message, NotificationType.Info, title);

        /// <summary>
        /// Creates a success notification.
        /// </summary>
        public static NotificationEventArgs Success(string message, string? title = null)
            => new(message, NotificationType.Success, title);

        /// <summary>
        /// Creates a warning notification.
        /// </summary>
        public static NotificationEventArgs Warning(string message, string? title = null)
            => new(message, NotificationType.Warning, title);

        /// <summary>
        /// Creates an error notification.
        /// </summary>
        public static NotificationEventArgs Error(string message, string? title = null)
            => new(message, NotificationType.Error, title);

        #endregion

        #region Methods
        // ─────────────────────────────────────────────────────────────────────

        /// <summary>
        /// Returns a string representation of the event args for logging.
        /// </summary>
        public override string ToString()
        {
            return $"[{Type}] {Title ?? "Notification"}: {Message}";
        }

        #endregion
    }
}
