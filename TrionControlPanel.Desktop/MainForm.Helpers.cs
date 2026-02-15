using TrionControlPanel.Desktop.Extensions.Application;
using TrionControlPanel.Desktop.Extensions.Classes.Data.Form;
using TrionControlPanel.Desktop.Extensions.Classes.Monitor;
using TrionControlPanel.Desktop.Extensions.Classes;
using TrionControlPanel.Desktop.Extensions.Modules;
using TrionControlPanelDesktop.Extensions.Modules;

namespace TrionControlPanelDesktop
{
    public partial class MainForm
    {
        private void PopulateComboBoxes()
        {
            CBOXSPPVersion.Items.Clear();
            CBOXSPPVersion.Items.Add(translator.Translate("SPPver0"));
            CBOXSPPVersion.Items.Add(translator.Translate("SPPver1"));
            CBOXSPPVersion.Items.Add(translator.Translate("SPPver2"));
            CBOXSPPVersion.Items.Add(translator.Translate("SPPver3"));
            CBOXSPPVersion.Items.Add(translator.Translate("SPPver4"));
            CBOXSPPVersion.Items.Add(translator.Translate("SPPver5"));
            CBOXSPPVersion.SelectedIndex = (int)settings.SelectedSPP;
            CBOXAccountExpansion.Items.Clear();
            CBOXAccountExpansion.Items.Add(translator.Translate("AccountExpansion0"));
            CBOXAccountExpansion.Items.Add(translator.Translate("AccountExpansion1"));
            CBOXAccountExpansion.Items.Add(translator.Translate("AccountExpansion2"));
            CBOXAccountExpansion.Items.Add(translator.Translate("AccountExpansion3"));
            CBOXAccountExpansion.Items.Add(translator.Translate("AccountExpansion4"));
            CBOXAccountExpansion.Items.Add(translator.Translate("AccountExpansion5"));
            CBOXAccountExpansion.Items.Add(translator.Translate("AccountExpansion6"));
            CBOXAccountExpansion.Items.Add(translator.Translate("AccountExpansion7"));
            CBOXAccountExpansion.Items.Add(translator.Translate("AccountExpansion8"));
            CBOXAccountExpansion.Items.Add(translator.Translate("AccountExpansion9"));
            CBOXAccountExpansion.SelectedIndex = 2;
            CBOXAccountSecurityAccess.Items.Clear();
            CBOXAccountSecurityAccess.Items.Add(translator.Translate("AccountAccessLvL0"));
            CBOXAccountSecurityAccess.Items.Add(translator.Translate("AccountAccessLvL1"));
            CBOXAccountSecurityAccess.Items.Add(translator.Translate("AccountAccessLvL2"));
            CBOXAccountSecurityAccess.Items.Add(translator.Translate("AccountAccessLvL3"));
            CBOXAccountSecurityAccess.Items.Add(translator.Translate("AccountAccessLvL4"));
            CBOXAccountSecurityAccess.SelectedIndex = 0;
            CBOXTrionIcon.Items.Clear();
            foreach (var key in ImageListIcons.Images.Keys)
            {
                CBOXTrionIcon.Items.Add(key!);
            }
            CBOXTrionIcon.SelectedItem = settings.TrionIcon;
            CBoxSelectItems();
        }

        private void LoadLangauge()
        {
            translator.LoadLanguage(settings.TrionLanguage);

            // Initialize LocalizationService if not already created (Step 4)
            if (_localization == null)
            {
                _localization = new LocalizationService(translator);
                InitializeLocalizationBindings();
            }

            SetLanguage();
        }

        private void GetAllLanguages()
        {
            CBOXLanguageSelect.Items.AddRange([.. Translator.GetAvailableLanguages()]);
            CBOXLanguageSelect.SelectedItem = settings.TrionLanguage;
            CBOXColorSelect.Items.AddRange(Enum.GetValues(typeof(Enums.TrionTheme)).Cast<Enums.TrionTheme>().Select(e => e.ToString()).ToArray());
            CBOXColorSelect.SelectedItem = settings.TrionTheme.ToString();
        }

        private void ChangeFormIcon(string imageName)
        {
            if (ImageListIcons.Images.ContainsKey(imageName))
            {
                using (Bitmap bitmap = new(ImageListIcons.Images[imageName]!))
                {
                    IntPtr hIcon = bitmap.GetHicon(); // Convert to icon
                    Icon = Icon.FromHandle(hIcon); // Apply icon
                    NIcon.Icon = Icon;
                }
            }
            else
            {
                //if the image is not found in the image list, load a default icon
                using (Bitmap bitmap = new(ImageListIcons.Images["Trion New Logo"]!))
                {
                    IntPtr hIcon = bitmap.GetHicon();
                    Icon = Icon.FromHandle(hIcon);
                    NIcon.Icon = Icon;
                }
            }
        }

        private void ToogleButtons()
        {
            BTNInstallSPP.Enabled = !FormData.UI.Form.InstallingEmulator;
            BTNUninstallSPP.Enabled = !FormData.UI.Form.InstallingEmulator;
            BTNRepairSPP.Enabled = !FormData.UI.Form.InstallingEmulator;
        }

        public void InitializeCustomUX()
        {
            // Start All Servers Menu Item
            var startAllItem = new ToolStripMenuItem("Start All Servers");
            startAllItem.Click += async (s, e) => await StartAllServers();
            CMSNotify.Items.Insert(0, startAllItem); // Add to top

            // Monitor ID Switching
            LBLWorldProcessID.Cursor = Cursors.Hand;
            LBLWorldProcessID.Click += (s, e) => 
            {
                _worldCurrentPage++;
                if (_worldCurrentPage > SystemData.GetTotalWorldProcessIDCount()) _worldCurrentPage = 1;
                ProcessesIDUpdate(); // Refresh label immediately
                ResurceUsage(); // Refresh stats
            };

            LBLLogonProcessID.Cursor = Cursors.Hand;
            LBLLogonProcessID.Click += (s, e) => 
            {
                _logonCurrentPage++;
                if (_logonCurrentPage > SystemData.GetTotalLogonProcessIDCount()) _logonCurrentPage = 1;
                ProcessesIDUpdate();
                ResurceUsage();
            };
            
            // Add tooltips
            if (TLTHome != null)
            {
                TLTHome.SetToolTip(LBLWorldProcessID, "Click to cycle through monitored World Process IDs");
                TLTHome.SetToolTip(LBLLogonProcessID, "Click to cycle through monitored Logon Process IDs");
            }
        }

        private async Task StartAllServers()
        {
            if (!FormData.UI.Form.DBRunning && !FormData.UI.Form.DBStarted)
            {
                Settings.CreateMySQLConfigFile(Directory.GetCurrentDirectory(), settings.DBLocation);
                SystemData.DatabaseStartTime = DateTime.Now;
                string arg = "--defaults-file=\"" + Directory.GetCurrentDirectory() + "/my.ini\" --console";
                await AppExecuteManager.StartDatabase(settings, arg);
            }

            if (!ServerMonitor.ServerStartedLogon())
            {
                await AppExecuteManager.StartLogon(settings);
            }

            if (!ServerMonitor.ServerStartedWorld())
            {
                await AppExecuteManager.StartWorld(settings);
            }
        }
    }
}