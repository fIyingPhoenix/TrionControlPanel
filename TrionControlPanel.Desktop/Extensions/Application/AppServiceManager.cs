using TrionControlPanel.Desktop.Extensions.Classes.Monitor;

namespace TrionControlPanel.Desktop.Extensions.Application
{
    // Manages services related to the application, such as repairing and uninstalling SPPs.
    public class AppServiceManager
    {

        // Helper method to start the uninstallation process for the specified directory.
        public async static Task StartUninstall(string targetDirectory)
        {
            try
            {
                // Delete all files in the target directory.
                string[] files = Directory.GetFiles(targetDirectory);
                foreach (string file in files)
                {
                    await Task.Run(() => File.Delete(file));
                }

                // Delete all directories in the target directory.
                string[] directories = Directory.GetDirectories(targetDirectory);
                foreach (string directory in directories)
                {
                    await Task.Run(() => Directory.Delete(directory, true));
                }
            }
            catch (Exception ex)
            {
                TrionLogger.Log($"An error occurred: {ex.Message}", "ERROR");
            }
        }
    }
}
