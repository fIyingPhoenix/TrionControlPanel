
namespace TrionControlPanel.Desktop.Extensions.Classes.Monitor
{
    public class TrionLogger
    {
        public static void Log(string message, string logLevel = "INFO")
        {
            string logEntry = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} [{logLevel}] {message}";
            try
            {
                
                using (StreamWriter writer = new("Trion.logs", append: true))
                {
                    writer.WriteLine(logEntry);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to write log: {ex.Message}");
            }
        }
    }
}
