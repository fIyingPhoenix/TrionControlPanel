
namespace TrionControlPanel.API.Classes
{ // TrionLogger class for logging messages to a file.
    public class TrionLogger
    {
        private static readonly object _lock = new(); // Lock object for thread safety.
        private static string _logFilePath = "logs/trion/Trion.logs"; // Default log file path.

        // Sets the log file path.
        public static void SetLogFilePath(string logFilePath)
        {
            _logFilePath = logFilePath;
        }

        // Logs a message with a specified log level to a file.
        public static void Log(string message, string logLevel = "INFO")
        {
            // Format the log entry with the current date and time, log level, and message.
            string logEntry = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} [{logLevel}] {message}";
            try
            {
                // Ensure thread safety using a lock.
                lock (_lock)
                {
                    // Ensure the directory exists.
                    string directory = Path.GetDirectoryName(_logFilePath);
                    if (!Directory.Exists(directory))
                    {
                        Directory.CreateDirectory(directory);
                    }

                    // Write the log entry to the log file, appending to the file if it already exists.
                    using (StreamWriter writer = new(_logFilePath, append: true))
                    {
                        writer.WriteLine(logEntry);
                    }
                }
            }
            catch (Exception ex)
            {
                // If writing to the log file fails, write the error message to the console.
                Console.WriteLine($"Failed to write log: {ex.Message}");
            }
        }
    }
}
