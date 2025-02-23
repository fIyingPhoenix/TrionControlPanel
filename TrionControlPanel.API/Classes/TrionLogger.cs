
namespace TrionControlPanel.API.Classes
{
    public class TrionLogger
    {
        public static async Task Log(string message, string logLevel = "INFO")
        {
            string logEntry = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} [{logLevel}] {message}";
            try
            {
                
                using (StreamWriter writer = new("Trion.logs", append: true))
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
