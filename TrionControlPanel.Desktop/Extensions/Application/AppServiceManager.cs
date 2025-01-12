﻿
using TrionControlPanel.Desktop.Extensions.Classes.Monitor;
using TrionControlPanel.Desktop.Extensions.Modules.Lists;

namespace TrionControlPanel.Desktop.Extensions.Application
{
    public class AppServiceManager
    {
        public async static Task InstallSPP(AppSettings appSettings)
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
                TrionLogger.Log($"An error occurred: {ex.Message}", "ERROR");
            }
        }
    }
}
