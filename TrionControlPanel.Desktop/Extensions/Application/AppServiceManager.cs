using TrionControlPanel.Desktop.Extensions.Classes.Monitor;
using TrionControlPanel.Desktop.Extensions.Modules.Lists;

namespace TrionControlPanel.Desktop.Extensions.Application
{
    // Manages services related to the application, such as repairing and uninstalling SPPs.
    public class AppServiceManager
    {
        // Repairs the selected SPP based on the provided app settings.
        public async static Task RepairSPP(AppSettings appSettings)
        {
            switch (appSettings.SelectedSPP)
            {
                case Modules.Enums.SPP.Classic:
                    await Task.Delay(1000); // Simulate repair delay for Classic SPP.
                    break;
                case Modules.Enums.SPP.TheBurningCrusade:
                    // Add repair logic for The Burning Crusade SPP.
                    break;
                case Modules.Enums.SPP.WrathOfTheLichKing:
                    // Add repair logic for Wrath of the Lich King SPP.
                    break;
                case Modules.Enums.SPP.Cataclysm:
                    // Add repair logic for Cataclysm SPP.
                    break;
                case Modules.Enums.SPP.MistOfPandaria:
                    // Add repair logic for Mist of Pandaria SPP.
                    break;
            }
        }

        // Uninstalls the selected SPP based on the provided app settings.
        public async static Task UninstallSPP(AppSettings appSettings)
        {
            switch (appSettings.SelectedSPP)
            {
                case Modules.Enums.SPP.Classic:
                    await StartUninstall(appSettings.ClassicWorkingDirectory);
                    break;
                case Modules.Enums.SPP.TheBurningCrusade:
                    await StartUninstall(appSettings.TBCWorkingDirectory);
                    break;
                case Modules.Enums.SPP.WrathOfTheLichKing:
                    await StartUninstall(appSettings.WotLKWorkingDirectory);
                    break;
                case Modules.Enums.SPP.Cataclysm:
                    await StartUninstall(appSettings.CataWorkingDirectory);
                    break;
                case Modules.Enums.SPP.MistOfPandaria:
                    await StartUninstall(appSettings.MopWorkingDirectory);
                    break;
            }
        }

        // Helper method to start the uninstallation process for the specified directory.
        private async static Task StartUninstall(string targetDirectory)
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
