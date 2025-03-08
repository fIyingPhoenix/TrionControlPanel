
namespace TrionControlPanel.Desktop.Extensions.Classes.Monitor
{
    public class TrionLogger
    {
        private static readonly object _lock = new();  // Lock for thread-safety
        private static readonly string _logFilePath = "logs/Trion.logs"; // Log file path
        private static readonly int _maxFileSize = 10 * 1024 * 1024; // 10 MB max log file size before rotation
        private static readonly string _configuredLogLevel = "INFO"; // Default log level to filter logs (can be config-driven)

        // Method to log messages synchronously with different log levels
        public static void Log(string message, string logLevel = "INFO")
        {
            // Skip logging if the log level doesn't match the configured log level (e.g., only log ERROR or INFO)
            if (!ShouldLog(logLevel))
            {
                return;
            }

            string logEntry = FormatLogEntry(message, logLevel);  // Prepare the log entry with timestamp and level

            try
            {
                // Ensure thread safety when writing to the log file
                lock (_lock)
                {
                    RotateLogFileIfNecessary();  // Check if log rotation is needed

                    // Append the log entry to the log file
                    using (StreamWriter writer = new(_logFilePath, append: true))
                    {
                        writer.WriteLine(logEntry);
                    }
                }
            }
            catch (Exception ex)
            {
                // If writing to the log fails, fallback to writing to the console
                Console.WriteLine($"Failed to write log: {ex.Message}");
            }
        }

        // Method to check if log rotation is necessary (based on file size)
        private static void RotateLogFileIfNecessary()
        {
            // Check if the log file exists and its size exceeds the limit
            if (File.Exists(_logFilePath) && new FileInfo(_logFilePath).Length > _maxFileSize)
            {
                try
                {
                    // Rotate the log file by renaming it with the current date (e.g., Trion_2025-03-04.logs)
                    string newFileName = $"{Path.GetFileNameWithoutExtension(_logFilePath)}_{DateTime.Now:yyyy-MM-dd}.logs";
                    File.Move(_logFilePath, newFileName);  // Rename the existing log file
                }
                catch (Exception ex)
                {
                    // Log rotation failed, output error to console
                    Console.WriteLine($"Failed to rotate log file: {ex.Message}");
                }
            }
        }

        // Method to log exceptions with their stack trace
        public static void LogException(Exception ex)
        {
            // Log the exception with the stack trace for better debugging
            string message = $"Exception: {ex.Message}\nStack Trace: {ex.StackTrace}";
            Log(message, "ERROR");
        }

        // Helper method to determine if the message should be logged based on the log level
        private static bool ShouldLog(string logLevel)
        {
            // Log if the level matches the configured level or if it's an ERROR level log
            return logLevel == _configuredLogLevel || logLevel == "ERROR" || logLevel == "WARNING";
        }

        // Helper method to format the log entry
        private static string FormatLogEntry(string message, string logLevel)
        {
            // Return the log entry with a timestamp, log level, and the message
            return $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} [{logLevel}] {message}";
        }

        // Asynchronous logging to avoid blocking the main thread
        public static async Task LogAsync(string message, string logLevel = "INFO")
        {
            // Skip logging if the log level doesn't match the configured log level
            if (!ShouldLog(logLevel))
            {
                return;
            }

            string logEntry = FormatLogEntry(message, logLevel);  // Prepare log entry

            try
            {
                // Ensure thread safety in asynchronous logging using Task.Run
                await Task.Run(() =>
                {
                    lock (_lock)
                    {
                        RotateLogFileIfNecessary();  // Check if log rotation is needed

                        // Append the log entry to the log file asynchronously
                        using (StreamWriter writer = new(_logFilePath, append: true))
                        {
                            writer.WriteLine(logEntry);
                        }
                    }
                });
            }
            catch (Exception ex)
            {
                // If writing to the log fails asynchronously, log the error to the console
                Console.WriteLine($"Failed to write log: {ex.Message}");
            }
        }
    }
}

