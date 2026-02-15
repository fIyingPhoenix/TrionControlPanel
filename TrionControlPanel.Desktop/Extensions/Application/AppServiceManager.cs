using TrionControlPanel.Desktop.Extensions.Classes.Monitor;

namespace TrionControlPanel.Desktop.Extensions.Application
{
    // Manages services related to the application, such as repairing and uninstalling SPPs.
    public class AppServiceManager
    {

        // Helper method to start the uninstallation process for the specified directory.
        public async static Task StartUninstall(string targetDirectory)
        {
            TrionLogger.Info($"Starting uninstall: {targetDirectory}");
            try
            {
                await Task.Run(() =>
                {
                    // Delete all files in the target directory.
                    string[] files = Directory.GetFiles(targetDirectory);
                    foreach (string file in files)
                    {
                        File.Delete(file);
                    }

                    // Delete all directories in the target directory.
                    string[] directories = Directory.GetDirectories(targetDirectory);
                    foreach (string directory in directories)
                    {
                        Directory.Delete(directory, true);
                    }
                });
                TrionLogger.Info($"Uninstall completed: {targetDirectory}");
            }
            catch (Exception ex)
            {
                TrionLogger.LogException(ex, "Uninstall");
            }
        }
    }
}
