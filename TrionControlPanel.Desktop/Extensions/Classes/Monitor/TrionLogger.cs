// =============================================================================
// TrionLogger.cs
// Purpose: Thread-safe logging service for application events and errors
// Used by: All classes throughout the application
// Steps 6, 9, 14 of IMPROVEMENTS.md - XML Docs, Regions, Enhanced Logging
// =============================================================================

using System.Diagnostics;
using System.Runtime.CompilerServices;
using static TrionControlPanel.Desktop.Extensions.Modules.Enums;

namespace TrionControlPanel.Desktop.Extensions.Classes.Monitor
{
    /// <summary>
    /// Provides thread-safe, structured logging functionality for the application.
    /// Writes log entries to a file with timestamp, log level, context, and message.
    /// </summary>
    /// <remarks>
    /// This is a static class that can be called from anywhere in the application.
    /// All log writes are synchronized using a lock to ensure thread safety.
    ///
    /// Enhanced log format: "[timestamp] [LEVEL   ] [context                  ] message"
    ///
    /// Features:
    /// - Automatic caller context capture using CallerMemberName
    /// - Structured exception logging with stack traces
    /// - Specialized server operation logging
    /// - Debug output for development
    /// - Log file rotation support (manual)
    ///
    /// Example usage:
    /// <code>
    /// TrionLogger.Log("Server started successfully");
    /// TrionLogger.Log("Connection failed", "ERROR");
    /// TrionLogger.LogException(ex, "DatabaseConnection");
    /// TrionLogger.LogServerOperation("Start", ServerType.World, SPP.WotLK, true, 12345);
    /// </code>
    /// </remarks>
    public class TrionLogger
    {
        #region Constants
        // ─────────────────────────────────────────────────────────────────────

        /// <summary>
        /// Maximum width for the context column in log output for alignment.
        /// </summary>
        private const int ContextColumnWidth = 25;

        /// <summary>
        /// Maximum width for the log level column in log output for alignment.
        /// </summary>
        private const int LevelColumnWidth = 8;

        /// <summary>
        /// Timestamp format for log entries.
        /// </summary>
        private const string TimestampFormat = "yyyy-MM-dd HH:mm:ss.fff";

        #endregion

        #region Fields
        // ─────────────────────────────────────────────────────────────────────

        /// <summary>
        /// Lock object for ensuring thread-safe file writes.
        /// </summary>
        private static readonly object _lock = new();

        /// <summary>
        /// Path to the log file. Can be changed via <see cref="SetLogFilePath"/>.
        /// Default: "logs/trion/Trion.logs"
        /// </summary>
        private static string _logFilePath = "logs/trion/Trion.logs";

        /// <summary>
        /// Minimum log level to write. Messages below this level are ignored.
        /// </summary>
        private static LogLevel _minimumLevel = LogLevel.Debug;

        /// <summary>
        /// Whether to also write logs to Debug output (for development).
        /// </summary>
        private static bool _writeToDebug = true;

        #endregion

        #region Enums
        // ─────────────────────────────────────────────────────────────────────

        /// <summary>
        /// Log severity levels in order of increasing severity.
        /// </summary>
        public enum LogLevel
        {
            /// <summary>Detailed debugging information.</summary>
            Debug = 0,
            /// <summary>General informational messages.</summary>
            Info = 1,
            /// <summary>Warning conditions that don't prevent operation.</summary>
            Warning = 2,
            /// <summary>Error conditions that may affect operation.</summary>
            Error = 3,
            /// <summary>Critical/fatal errors requiring immediate attention.</summary>
            Critical = 4
        }

        #endregion

        #region Configuration Methods
        // ─────────────────────────────────────────────────────────────────────

        /// <summary>
        /// Sets the path for the log file.
        /// </summary>
        /// <param name="logFilePath">
        /// The full or relative path to the log file.
        /// The directory will be created if it doesn't exist.
        /// </param>
        /// <example>
        /// <code>
        /// TrionLogger.SetLogFilePath("C:/Logs/myapp.log");
        /// TrionLogger.SetLogFilePath("logs/custom/app.log");
        /// </code>
        /// </example>
        public static void SetLogFilePath(string logFilePath)
        {
            _logFilePath = logFilePath;
        }

        /// <summary>
        /// Sets the minimum log level. Messages below this level are ignored.
        /// </summary>
        /// <param name="level">The minimum log level to write.</param>
        /// <example>
        /// <code>
        /// // In production, only log warnings and above
        /// TrionLogger.SetMinimumLevel(LogLevel.Warning);
        ///
        /// // In development, log everything
        /// TrionLogger.SetMinimumLevel(LogLevel.Debug);
        /// </code>
        /// </example>
        public static void SetMinimumLevel(LogLevel level)
        {
            _minimumLevel = level;
        }

        /// <summary>
        /// Enables or disables writing logs to Debug output.
        /// </summary>
        /// <param name="enabled">True to write to Debug output, false to disable.</param>
        public static void SetDebugOutput(bool enabled)
        {
            _writeToDebug = enabled;
        }

        #endregion

        #region Core Logging Methods
        // ─────────────────────────────────────────────────────────────────────

        /// <summary>
        /// Logs a message with the specified level and automatic caller context.
        /// </summary>
        /// <param name="message">The message to log.</param>
        /// <param name="logLevel">
        /// The severity level as a string. Common values:
        /// - "INFO" (default): General information
        /// - "WARNING": Warning conditions
        /// - "ERROR": Error conditions
        /// - "DEBUG": Debug-level information
        /// - "CRITICAL": Critical/fatal errors
        /// </param>
        /// <param name="caller">
        /// Automatically populated with the calling method name.
        /// Can be overridden for custom context.
        /// </param>
        /// <remarks>
        /// This method is thread-safe and can be called from multiple threads simultaneously.
        /// If the log directory doesn't exist, it will be created automatically.
        /// If writing fails, the error is written to the console.
        /// </remarks>
        /// <example>
        /// <code>
        /// // Basic info logging (context = calling method name)
        /// TrionLogger.Log("Application started");
        ///
        /// // Error logging with custom context
        /// TrionLogger.Log($"Failed to connect: {ex.Message}", "ERROR", "DatabaseInit");
        ///
        /// // Debug logging
        /// TrionLogger.Log($"Processing item {id}", "DEBUG");
        /// </code>
        /// </example>
        public static void Log(
            string message,
            string logLevel = "INFO",
            [CallerMemberName] string caller = "")
        {
            // Parse log level string to enum for filtering
            LogLevel level = ParseLogLevel(logLevel);

            // Skip if below minimum level
            if (level < _minimumLevel)
                return;

            // Format the log entry with timestamp, level, context, and message
            string timestamp = DateTime.Now.ToString(TimestampFormat);
            string levelStr = logLevel.PadRight(LevelColumnWidth);
            string contextStr = TruncateOrPad(caller, ContextColumnWidth);
            string logEntry = $"[{timestamp}] [{levelStr}] [{contextStr}] {message}";

            WriteLogEntry(logEntry);
        }

        /// <summary>
        /// Logs a message with a typed log level.
        /// </summary>
        /// <param name="message">The message to log.</param>
        /// <param name="level">The log level enum value.</param>
        /// <param name="caller">Automatically populated with the calling method name.</param>
        public static void Log(
            string message,
            LogLevel level,
            [CallerMemberName] string caller = "")
        {
            Log(message, level.ToString().ToUpper(), caller);
        }

        #endregion

        #region Specialized Logging Methods
        // ─────────────────────────────────────────────────────────────────────

        /// <summary>
        /// Logs an exception with full details including stack trace.
        /// </summary>
        /// <param name="ex">The exception to log.</param>
        /// <param name="context">
        /// Additional context about where the exception occurred.
        /// If empty, uses the calling method name.
        /// </param>
        /// <param name="caller">Automatically populated with the calling method name.</param>
        /// <remarks>
        /// Logs the exception message at ERROR level, then the stack trace at DEBUG level.
        /// Inner exceptions are also logged recursively.
        /// </remarks>
        /// <example>
        /// <code>
        /// try
        /// {
        ///     await database.ConnectAsync();
        /// }
        /// catch (Exception ex)
        /// {
        ///     TrionLogger.LogException(ex, "DatabaseConnection");
        ///     // or just: TrionLogger.LogException(ex);
        /// }
        /// </code>
        /// </example>
        public static void LogException(
            Exception ex,
            string context = "",
            [CallerMemberName] string caller = "")
        {
            string effectiveContext = string.IsNullOrEmpty(context) ? caller : context;

            // Log the main exception message
            Log($"Exception: {ex.GetType().Name}: {ex.Message}", "ERROR", effectiveContext);

            // Log the stack trace at DEBUG level (only if DEBUG logging is enabled)
            if (_minimumLevel <= LogLevel.Debug && !string.IsNullOrEmpty(ex.StackTrace))
            {
                Log($"Stack Trace:\n{ex.StackTrace}", "DEBUG", effectiveContext);
            }

            // Log inner exceptions recursively
            if (ex.InnerException != null)
            {
                Log($"Inner Exception: {ex.InnerException.GetType().Name}: {ex.InnerException.Message}",
                    "ERROR", effectiveContext);

                if (_minimumLevel <= LogLevel.Debug && !string.IsNullOrEmpty(ex.InnerException.StackTrace))
                {
                    Log($"Inner Stack Trace:\n{ex.InnerException.StackTrace}", "DEBUG", effectiveContext);
                }
            }
        }

        /// <summary>
        /// Logs a server operation with standardized format.
        /// </summary>
        /// <param name="operation">The operation being performed (e.g., "Start", "Stop", "Restart").</param>
        /// <param name="serverType">The type of server (Database, World, Logon).</param>
        /// <param name="expansion">The expansion for World/Logon servers, null for Database.</param>
        /// <param name="success">Whether the operation succeeded.</param>
        /// <param name="processId">The process ID if applicable.</param>
        /// <param name="additionalInfo">Optional additional information to include.</param>
        /// <remarks>
        /// Creates a standardized log entry for server operations:
        /// "[timestamp] [INFO    ] [ServerOps                ] Start | Server: World | Expansion: WotLK | PID: 12345 | Status: SUCCESS"
        /// </remarks>
        /// <example>
        /// <code>
        /// // Log a successful server start
        /// TrionLogger.LogServerOperation("Start", ServerType.World, SPP.WrathOfTheLichKing, true, 12345);
        ///
        /// // Log a failed server stop
        /// TrionLogger.LogServerOperation("Stop", ServerType.Database, null, false, additionalInfo: "Timeout");
        /// </code>
        /// </example>
        public static void LogServerOperation(
            string operation,
            ServerType serverType,
            SPP? expansion,
            bool success,
            int? processId = null,
            string? additionalInfo = null)
        {
            string expansionStr = expansion?.ToString() ?? "N/A";
            string pidStr = processId?.ToString() ?? "N/A";
            string status = success ? "SUCCESS" : "FAILED";
            string level = success ? "INFO" : "ERROR";

            string message = $"{operation} | Server: {serverType} | Expansion: {expansionStr} | PID: {pidStr} | Status: {status}";

            if (!string.IsNullOrEmpty(additionalInfo))
            {
                message += $" | Info: {additionalInfo}";
            }

            Log(message, level, "ServerOps");
        }

        /// <summary>
        /// Logs a database operation with standardized format.
        /// </summary>
        /// <param name="operation">The operation (e.g., "Query", "Insert", "Update", "Delete").</param>
        /// <param name="tableName">The table being operated on.</param>
        /// <param name="success">Whether the operation succeeded.</param>
        /// <param name="rowsAffected">Number of rows affected, if applicable.</param>
        /// <param name="additionalInfo">Optional additional information.</param>
        public static void LogDatabaseOperation(
            string operation,
            string tableName,
            bool success,
            int? rowsAffected = null,
            string? additionalInfo = null)
        {
            string status = success ? "SUCCESS" : "FAILED";
            string level = success ? "DEBUG" : "ERROR";
            string rowsStr = rowsAffected?.ToString() ?? "N/A";

            string message = $"{operation} | Table: {tableName} | Rows: {rowsStr} | Status: {status}";

            if (!string.IsNullOrEmpty(additionalInfo))
            {
                message += $" | Info: {additionalInfo}";
            }

            Log(message, level, "DatabaseOps");
        }

        /// <summary>
        /// Logs application lifecycle events (startup, shutdown, etc.).
        /// </summary>
        /// <param name="event">The lifecycle event (e.g., "Starting", "Started", "Stopping", "Stopped").</param>
        /// <param name="additionalInfo">Optional additional information.</param>
        public static void LogAppLifecycle(string @event, string? additionalInfo = null)
        {
            string message = $"Application {@event}";

            if (!string.IsNullOrEmpty(additionalInfo))
            {
                message += $" | {additionalInfo}";
            }

            Log(message, "INFO", "AppLifecycle");
        }

        /// <summary>
        /// Logs a performance metric.
        /// </summary>
        /// <param name="metricName">The name of the metric.</param>
        /// <param name="value">The metric value.</param>
        /// <param name="unit">The unit of measurement (e.g., "ms", "MB", "%").</param>
        public static void LogPerformance(string metricName, double value, string unit)
        {
            Log($"{metricName}: {value:F2} {unit}", "DEBUG", "Performance");
        }

        #endregion

        #region Convenience Methods
        // ─────────────────────────────────────────────────────────────────────

        /// <summary>
        /// Logs a debug message.
        /// </summary>
        public static void Debug(string message, [CallerMemberName] string caller = "")
            => Log(message, "DEBUG", caller);

        /// <summary>
        /// Logs an info message.
        /// </summary>
        public static void Info(string message, [CallerMemberName] string caller = "")
            => Log(message, "INFO", caller);

        /// <summary>
        /// Logs a warning message.
        /// </summary>
        public static void Warning(string message, [CallerMemberName] string caller = "")
            => Log(message, "WARNING", caller);

        /// <summary>
        /// Logs an error message.
        /// </summary>
        public static void Error(string message, [CallerMemberName] string caller = "")
            => Log(message, "ERROR", caller);

        /// <summary>
        /// Logs a critical message.
        /// </summary>
        public static void Critical(string message, [CallerMemberName] string caller = "")
            => Log(message, "CRITICAL", caller);

        #endregion

        #region Private Helper Methods
        // ─────────────────────────────────────────────────────────────────────

        /// <summary>
        /// Writes a formatted log entry to the file and optionally to Debug output.
        /// </summary>
        private static void WriteLogEntry(string logEntry)
        {
            try
            {
                // Write to Debug output if enabled
                if (_writeToDebug)
                {
                    System.Diagnostics.Debug.WriteLine(logEntry);
                }

                // Ensure thread safety using a lock
                lock (_lock)
                {
                    // Ensure the directory exists
                    string? directory = Path.GetDirectoryName(_logFilePath);
                    if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
                    {
                        Directory.CreateDirectory(directory);
                    }

                    // Write the log entry, appending to existing content
                    using StreamWriter writer = new(_logFilePath, append: true);
                    writer.WriteLine(logEntry);
                }
            }
            catch (Exception ex)
            {
                // Fallback: write to console if file logging fails
                Console.WriteLine($"Failed to write log: {ex.Message}");
                Console.WriteLine($"Original log entry: {logEntry}");
            }
        }

        /// <summary>
        /// Parses a log level string to the LogLevel enum.
        /// </summary>
        private static LogLevel ParseLogLevel(string levelStr)
        {
            return levelStr.ToUpperInvariant() switch
            {
                "DEBUG" => LogLevel.Debug,
                "INFO" => LogLevel.Info,
                "WARNING" or "WARN" => LogLevel.Warning,
                "ERROR" or "ERR" => LogLevel.Error,
                "CRITICAL" or "FATAL" or "CRIT" => LogLevel.Critical,
                _ => LogLevel.Info
            };
        }

        /// <summary>
        /// Truncates or pads a string to a specific width for column alignment.
        /// </summary>
        private static string TruncateOrPad(string value, int width)
        {
            if (string.IsNullOrEmpty(value))
                return new string(' ', width);

            if (value.Length > width)
                return value.Substring(0, width - 2) + "..";

            return value.PadRight(width);
        }

        #endregion
    }
}
