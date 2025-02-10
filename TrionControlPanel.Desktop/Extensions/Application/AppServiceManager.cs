
using System.Diagnostics;
using System.Reflection;
using System.Text.RegularExpressions;
using TrionControlPanel.Desktop.Extensions.Classes;
using TrionControlPanel.Desktop.Extensions.Classes.Data.Form;
using TrionControlPanel.Desktop.Extensions.Classes.Monitor;
using TrionControlPanel.Desktop.Extensions.Modules.Lists;
using TrionControlPanelDesktop.Extensions.Modules;

namespace TrionControlPanel.Desktop.Extensions.Application
{
    public class AppServiceManager
    {
        public static async Task GetAPIServer()
        {
            if (await NetworkManager.IsWebsiteOnlineAsync($"{Links.MainHost}/Trion/GetWebsitePing")) { Links.APIServer = Links.MainHost; }
            if (await NetworkManager.IsWebsiteOnlineAsync($"{Links.BackupHost}/Trion/GetWebsitePing")) { Links.APIServer = Links.BackupHost; }
            else { Links.APIServer = Links.MainHost; }
        }
        public async static Task<bool> InstallSPP(AppSettings appSettings)
        {
            switch (appSettings.SelectedSPP)
            {
                case Modules.Enums.SPP.Classic:
                    await Task.Delay(100);
                    //
                    return false;

                case Modules.Enums.SPP.TheBurningCrusade:
                    return false;
                   
                case Modules.Enums.SPP.WrathOfTheLichKing:
                    //
                    return false;
                    
                case Modules.Enums.SPP.Cataclysm:
                    //
                    return false;
                    
                case Modules.Enums.SPP.MistOfPandaria:
                    //
                    return false;
            }
            return false;
        }

        public static async Task<List<FileList>> GetEmulatorInstallationList(AppSettings appSettings)
        {
            return [];
        }
        public async static Task RepairSPP(AppSettings appSettings)
        {
            switch (appSettings.SelectedSPP)
            {
                case Modules.Enums.SPP.Classic:
                    await Task.Delay(1000);
                    break;
                case Modules.Enums.SPP.TheBurningCrusade:
                    //
                    break;
                case Modules.Enums.SPP.WrathOfTheLichKing:
                    //
                    break;
                case Modules.Enums.SPP.Cataclysm:
                    //
                    break;
                case Modules.Enums.SPP.MistOfPandaria:
                    //
                    break;
            }
        }
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
        private async static Task StartUninstall(string targetDirectory)
        {
            try
            {
                // Delete all files
                string[] files = Directory.GetFiles(targetDirectory);
                foreach (string file in files)
                {
                    await Task.Run(() => File.Delete(file));
                }

                // Delete all directories
                string[] directories = Directory.GetDirectories(targetDirectory);
                foreach (string directory in directories)
                {
                    await Task.Run(() => Directory.Delete(directory, true));
                }
            }
            catch (Exception ex)
            {
                await TrionLogger.Log($"An error occurred: {ex.Message}", "ERROR");
            }
        }
    }
}
