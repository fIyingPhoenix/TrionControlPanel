
namespace TrionControlPanel.Desktop.Extensions.Classes.Monitor
{
    public class TrionLogger
    {
        public static async Task Log(string message, string logLevel = "INFO")
        {
            string logEntry = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} [{logLevel}] {message}";
            try
            {
                using (StreamWriter writer = new("logs.tcp", append: true))
                {
                   await writer.WriteLineAsync(logEntry);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to write log: {ex.Message}");
            }
        }
    }
}
