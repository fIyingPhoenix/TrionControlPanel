using MaterialSkin;
using MaterialSkin.Controls;
using System.Diagnostics;
using System.Globalization;
using TrionControlPanel.Desktop;
using TrionControlPanel.Desktop.Extensions.Application;
using TrionControlPanel.Desktop.Extensions.Classes;
using TrionControlPanel.Desktop.Extensions.Classes.Data.Form;
using TrionControlPanel.Desktop.Extensions.Classes.Monitor;
using TrionControlPanel.Desktop.Extensions.Classes.Network;
using TrionControlPanel.Desktop.Extensions.Database;
using TrionControlPanel.Desktop.Extensions.Modules;
using TrionControlPanel.Desktop.Extensions.Modules.Lists;
using TrionControlPanel.Desktop.Extensions.Notification;
using TrionControlPanel.Desktop.Properties;
using TrionControlPanelDesktop.Extensions.Modules;
using static TrionControlPanel.Desktop.Extensions.Modules.Enums;
using static TrionControlPanel.Desktop.Extensions.Notification.AlertBox;

namespace TrionControlPanelDesktop
{
    public partial class MainForm : MaterialForm
    {
        #region "Load Language"
        private Translator translator = new();



        private void SetLanguage()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(SetLanguage));
                return;
            }
            PopulateComboBoxes();
            #region "MainForm"  
            TLTHome.SetToolTip(BTNStartDatabase, translator.Translate("BTNStartDatabaseToolTip"));
            TLTHome.SetToolTip(BTNStartLogon, translator.Translate("BTNStartLogonToolTip"));
            TLTHome.SetToolTip(BTNStartWorld, translator.Translate("BTNStartWorldToolTip"));
            TLTHome.SetToolTip(InfoMachineResources, translator.Translate("InfoMachineResources"));
            TLTHome.SetToolTip(InfoLogonResorces, translator.Translate("InfoLogonResorces"));
            TLTHome.SetToolTip(InfoWorldResorces, translator.Translate("InfoWorldResorces"));
            BTNStartDatabase.Text = translator.Translate("BTNStartDatabaseTextOFF");
            BTNStartLogon.Text = translator.Translate("BTNStartLogonTextOFF");
            BTNStartWorld.Text = translator.Translate("BTNStartWorldTextOFF");
            BTNStartWebsite.Text = translator.Translate("BTNStartWebsiteTextOFF");
            NIcon.BalloonTipText = translator.Translate("NIconBalloonTipText");
            NIcon.BalloonTipTitle = translator.Translate("NIconBalloonTipTitle");
            NIcon.Text = translator.Translate("NIconBalloonTipTitle");
            TabSettings.Text = translator.Translate("TabSettingsTitle");
            TabHome.Text = translator.Translate("TabHomeTitle");
            TabDatabaseEditor.Text = translator.Translate("TabDatabaseEditorTitle");
            TabSPP.Text = translator.Translate("TabSPPTitle");
            TabNotification.Text = translator.Translate("TabNotification");
            TabDDNS.Text = translator.Translate("TabDDNS");
            TabDownloader.Text = translator.Translate("TabDownloader");
            Text = translator.Translate("TrionFormText");
            #endregion
            #region "Home Page"
            LBLCardMachineResourcesTitle.Text = translator.Translate("LBLCardMachineResourcesTitle").ToUpper(CultureInfo.InvariantCulture);
            LBLCardWorldResourcesTitle.Text = translator.Translate("LBLCardWorldResourcesTitle").ToUpper(CultureInfo.InvariantCulture);
            LBLCardLogonResourcesTitle.Text = translator.Translate("LBLCardLogonResourcesTitle").ToUpper(CultureInfo.InvariantCulture);
            LBLRAMTextMachineResources.Text = translator.Translate("LBLRAMTextMachineResources");
            LBLCPUTextMachineResources.Text = translator.Translate("LBLCPUTextMachineResources");
            LBLRAMTextWorldResources.Text = translator.Translate("LBLRAMTextMachineResources");
            LBLCPUTextWorldResources.Text = translator.Translate("LBLCPUTextMachineResources");
            LBLRAMTextLogonResources.Text = translator.Translate("LBLRAMTextMachineResources");
            LBLCPUTextLogonResources.Text = translator.Translate("LBLCPUTextMachineResources");
            LBLDatabaseProcessID.Text = translator.Translate("LBLProcessID");
            LBLWorldProcessID.Text = translator.Translate("LBLProcessID");
            LBLLogonProcessID.Text = translator.Translate("LBLProcessID");
            LBLUpTimeDatabase.Text = translator.Translate("LBLUpTime");
            LBLUpTimeWorld.Text = translator.Translate("LBLUpTime");
            LBLUpTimeLogon.Text = translator.Translate("LBLUpTime");
            #endregion
            #region "Datebase Edior"
            #region "Reamlist"
            TXTRealmID.Hint = translator.Translate("TXTRealmID");
            TXTRealmName.Hint = translator.Translate("TXTRealmName");
            TXTRealmAddress.Hint = translator.Translate("TXTRealmAddress");
            TXTRealmLocalAddress.Hint = translator.Translate("TXTRealmLocalAddress");
            TXTRealmSubnetMask.Hint = translator.Translate("TXTRealmSubnetMask");
            TXTRealmPort.Hint = translator.Translate("TXTRealmPort");
            TXTRealmGameBuild.Hint = translator.Translate("TXTRealmGameBuild");
            CBOXReamList.Hint = translator.Translate("CBOXReamList");
            TXTDomainName.Hint = translator.Translate("TXTDomainName");
            TXTInternIP.Hint = translator.Translate("TXTInternIP");
            TXTPublicIP.Hint = translator.Translate("TXTPublicIP");
            BTNOpenPublic.Text = translator.Translate("BTNOpenPublic");
            BTNOpenIntern.Text = translator.Translate("BTNOpenIntern");
            BTNEditRealmlistData.Text = translator.Translate("BTNEditRealmlistDataON");
            BTNCreateRealmList.Text = translator.Translate("BTNCreateRealmList");
            BTNDeleteRealmList.Text = translator.Translate("BTNDeleteRealmList");
            BTNForceRefresh.Text = translator.Translate("BTNForceRefresh");
            LBLCardRealmDataTitle.Text = translator.Translate("LBLCardRealmDataTitle").ToUpper(CultureInfo.InvariantCulture);
            LBLCardRealmOptionTitle.Text = translator.Translate("LBLCardRealmOptionTitle").ToUpper(CultureInfo.InvariantCulture);
            LBLCardRealmActionTitle.Text = translator.Translate("LBLCardRealmActionTitle").ToUpper(CultureInfo.InvariantCulture);
            TLTHome.SetToolTip(LBLCardRealmDataInfo, translator.Translate("LBLCardRealmDataInfo"));
            TLTHome.SetToolTip(LBLCardRealmOptionInfo, translator.Translate("LBLCardRealmOptionInfo"));
            TLTHome.SetToolTip(LBLCardRealmActionInfo, translator.Translate("LBLCardRealmActionInfo"));
            TabRealmList.Text = translator.Translate("TabRealmList");
            TabAccount.Text = translator.Translate("TabAccount");
            #endregion
            #region"Account Editor"
            TLTHome.SetToolTip(LBLCardAccountCreateInfo, translator.Translate("LBLCardAccountCreateInfo"));
            TLTHome.SetToolTip(LBLCardAccountAccessInfo, translator.Translate("LBLCardAccountAccessInfo"));
            TLTHome.SetToolTip(LBLCardPasswordResetInfo, translator.Translate("LBLCardPasswordResetInfo"));
            TXTBoxCreateUserUsername.Hint = translator.Translate("TXTBoxCreateUserUsername");
            TXTBoxCreateUserPassword.Hint = translator.Translate("TXTBoxCreateUserPassword");
            TXTBoxCreateUserEmail.Hint = translator.Translate("TXTBoxCreateUserEmail");
            CBOXAccountExpansion.Hint = translator.Translate("CBOXAccountExpansion");
            BTNAccountCreate.Text = translator.Translate("BTNAccountCreate");
            LBLCardAccountCreate.Text = translator.Translate("LBLCardAccountCreate").ToUpper(CultureInfo.InvariantCulture);
            LBLCardAccountAdmin.Text = translator.Translate("LBLCardAccountAdmin").ToUpper(CultureInfo.InvariantCulture);
            LBLCardAccountPassword.Text = translator.Translate("LBLCardAccountPassword").ToUpper(CultureInfo.InvariantCulture);
            TXTBoxGMUsername.Hint = translator.Translate("TXTBoxGMUsername");
            CBoxGMRealmSelect.Hint = translator.Translate("CBoxGMRealmSelect");
            CBOXAccountSecurityAccess.Hint = translator.Translate("CBOXAccountSecurityAccess");
            TXTBoxPassUsername.Hint = translator.Translate("TXTBoxPassUsername");
            TXTBoxPassPassword.Hint = translator.Translate("TXTBoxPassPassword");
            TXTBoxPassRePassword.Hint = translator.Translate("TXTBoxPassRePassword");
            BTNTBoxPassResset.Text = translator.Translate("BTNTBoxPassResset");
            BTNGMCreate.Text = translator.Translate("BTNGMCreate");
            TGLAccountShowPassword.Text = translator.Translate("TGLAccountShowPassword");
            #endregion
            #endregion
            #region"Settings"
            TabDatabase.Text = translator.Translate("TabDatabase");
            TabCustom.Text = translator.Translate("TabCustom");
            TabTrion.Text = translator.Translate("TabTrion");
            #region "Trion"
            BTNDownloadUpdates.Text = translator.Translate("BTNDownloadUpdatesOff");
            TGLServerCrashDetection.Text = translator.Translate("TGLServerCrashDetection");
            TGLServerStartup.Text = translator.Translate("TGLServerStartup");
            TGLRunTrionStartup.Text = translator.Translate("TGLRunTrionStartup");
            TGLHideConsole.Text = translator.Translate("TGLHideConsole");
            TGLNotificationSound.Text = translator.Translate("TGLNotificationSound");
            TGLStayInTray.Text = translator.Translate("TGLStayInTray");
            TGLAutoUpdateTrion.Text = translator.Translate("TGLAutoUpdateTrion");
            TGLAutoUpdateCore.Text = translator.Translate("TGLAutoUpdateCore");
            TGLAutoUpdateDatabase.Text = translator.Translate("TGLAutoUpdateDatabase");
            LBLCardCustomPreferencesTitle.Text = translator.Translate("LBLCardCustomPreferencesTitle").ToUpper(CultureInfo.InvariantCulture);
            LBLCardUpdateDashboardTitle.Text = translator.Translate("LBLCardUpdateDashboardTitle").ToUpper(CultureInfo.InvariantCulture);
            CBOXColorSelect.Hint = translator.Translate("CBOXColorSelect");
            CBOXLanguageSelect.Hint = translator.Translate("CBOXLanguageSelect");
            TLTHome.SetToolTip(LBLCardCustomPreferencesInfo, translator.Translate("LBLCardCustomPreferencesInfo"));
            TLTHome.SetToolTip(LBLCardUpdateDashboardInfo, translator.Translate("LBLCardUpdateDashboardInfo"));
            TXTSupporterKey.Hint = translator.Translate("TXTSupporterKeyHint");
            CBOXTrionIcon.Hint = translator.Translate("CBOXTrionIconHint");
            #endregion
            #region"Custom Emulators"
            TXTCustomDatabaseLocation.Hint = translator.Translate("TXTCustomDatabaseLocation");
            TXTCustomRepackLocation.Hint = translator.Translate("TXTCustomRepackLocation");
            LBLCardCustomEmulatorTitle.Text = translator.Translate("LBLCardCustomEmulatorTitle").ToUpper(CultureInfo.InvariantCulture);
            CBOXSelectedEmulators.Hint = translator.Translate("CBOXSelectedEmulators");
            BTNEmulatorLocation.Text = translator.Translate("BTNEmulatorLocation");
            BTNDatabaseLocation.Text = translator.Translate("BTNDatabaseLocation");
            TGLUseCustomServer.Text = translator.Translate("TGLUseCustomServer");
            LBLCardCustomNamesTitle.Text = translator.Translate("LBLCardCustomNamesTitle").ToUpper(CultureInfo.InvariantCulture);
            TXTCustomAuthName.Hint = translator.Translate("TXTCustomAuthName");
            TXTCustomWorldName.Hint = translator.Translate("TXTCustomWorldName");
            TXTCustomDatabaseName.Hint = translator.Translate("TxtCustomDatabaseName");
            TGLCustomNames.Text = translator.Translate("TGLCustomNames");
            BTNAscEmuWebsite.Text = translator.Translate("BTNAscEmuWebsite");
            BTNACoreWebsite.Text = translator.Translate("BTNACoreWebsite");
            BTNCMangosWebsite.Text = translator.Translate("BTNCMangosWebsite");
            BTNCypherWebsite.Text = translator.Translate("BTNCypherWebsite");
            BTNTrinityCoreWebsite.Text = translator.Translate("BTNTrinityCoreWebsite");
            BTNSkyFireWebsite.Text = translator.Translate("BTNSkyFireWebsite");
            TLTHome.SetToolTip(LBLCardCustomEmulatorInfo, translator.Translate("LBLCardCustomEmulatorInfo"));
            TLTHome.SetToolTip(LBLCardCustomNamesInfo, translator.Translate("LBLCardCustomNamesInfo"));
            #endregion
            #region"Database"
            LBLCardDatabaseCredencialsTitle.Text = translator.Translate("LBLCardDatabaseCredencialsTitle").ToUpper(CultureInfo.InvariantCulture);
            LBLCardTableNameTitle.Text = translator.Translate("LBLCardTableNameTitle").ToUpper(CultureInfo.InvariantCulture);
            LBLCardPreconfiguredDBTitle.Text = translator.Translate("LBLCardPreconfiguredDBTitle").ToUpper(CultureInfo.InvariantCulture);
            LBLCardDatabaseBCTitle.Text = translator.Translate("LBLCardDatabaseBCTitle").ToUpper(CultureInfo.InvariantCulture);
            TXTDatabaseHost.Hint = translator.Translate("TXTDatabaseHost");
            TXTDatabasePort.Hint = translator.Translate("TXTDatabasePorta");
            TXTDatabaseUser.Hint = translator.Translate("TXTDatabaseUser");
            TXTDatabasePassword.Hint = translator.Translate("TXTDatabasePassword");
            BTNTestConnection.Text = translator.Translate("BTNTestConnection");
            TXTAuthDatabase.Hint = translator.Translate("TXTAuthDatabase");
            TXTCharDatabase.Hint = translator.Translate("TXTCharDatabase");
            TXTWorldDatabase.Hint = translator.Translate("TXTWorldDatabase");
            BTNDeleteAuth.Text = translator.Translate("BTNDeleteAuth");
            BTNDeleteChar.Text = translator.Translate("BTNDeleteChar");
            BTNDeleteWorld.Text = translator.Translate("BTNDeleteWorld");
            TGLClassicDB.Text = translator.Translate("TGLClassicDB");
            TGLTbcDB.Text = translator.Translate("TGLTbcDB");
            TGLWotlkDB.Text = translator.Translate("TGLWotlkDB");
            TGLCataDB.Text = translator.Translate("TGLCataDB");
            TGLMopDB.Text = translator.Translate("TGLMopDB");
            TGLCustomDB.Text = translator.Translate("TGLCustomDB");
            TGLAuthBackup.Text = translator.Translate("TGLAuthBackup");
            TGLCharBackup.Text = translator.Translate("TGLCharBackup");
            TGLWorldBackup.Text = translator.Translate("TGLWorldBackup");
            BTNDatabaseBackup.Text = translator.Translate("BTNDatabaseBackup");
            BTNLoadBackup.Text = translator.Translate("BTNLoadBackup");
            BTNFixDatabase.Text = translator.Translate("BTNFixDatabase");
            TLTHome.SetToolTip(LBLCardDatabaseCredencialsInfo, translator.Translate("LBLCardDatabaseCredencialsInfo"));
            TLTHome.SetToolTip(LBLCardTableNameInfo, translator.Translate("LBLCardTableNameInfo"));
            TLTHome.SetToolTip(LBLCardPreconfiguredDBInfo, translator.Translate("LBLCardPreconfiguredDBInfo"));
            TLTHome.SetToolTip(LBLCardDatabaseBCInfo, translator.Translate("LBLCardDatabaseBCInfo"));
            #endregion
            #region"DDNS"
            TLTHome.SetToolTip(LBLCardDDNSSettingsInfo, translator.Translate("LBLCardDDNSSettingsInfo"));
            TLTHome.SetToolTip(LBLCardDDNSTimerInfos, translator.Translate("LBLCardDDNSTimerInfos"));
            LBLCardDDNSSettingsTitle.Text = translator.Translate("LBLCardDDNSSettingsTitle").ToUpper(CultureInfo.InvariantCulture);
            LBLCardDDNSTimerTitle.Text = translator.Translate("LBLCardDDNSTimerTitle").ToUpper(CultureInfo.InvariantCulture);
            CBOXDDNService.Hint = translator.Translate("CBOXDDNService");
            TXTDDNSDomain.Hint = translator.Translate("TXTDDNSDomain");
            TXTDDNSUsername.Hint = translator.Translate("TXTDDNSUsername");
            TXTDDNSPassword.Hint = translator.Translate("TXTDDNSPassword");
            TXTDDNSInterval.Hint = translator.Translate("TXTDDNSInterval");
            TGLDDNSRunOnStartup.Text = translator.Translate("TGLDDNSRunOnStartup");
            BTNDDNSServiceWebiste.Text = translator.Translate("BTNDDNSServiceWebiste");
            BTNDDNSTimerStart.Text = translator.Translate("BTNDDNSTimerStart");
            #endregion
            #endregion
            #region"SPP"
            LBLCardSPPVersionTitle.Text = translator.Translate("LBLCardSPPVersionTitle").ToUpper(CultureInfo.InvariantCulture);
            LBLCardSPPRunTitle.Text = translator.Translate("LBLCardSPPRunTitle").ToUpper(CultureInfo.InvariantCulture);
            BTNInstallSPP.Text = translator.Translate("BTNInstallSPP");
            BTNRepairSPP.Text = translator.Translate("BTNRepairSPP");
            BTNUninstallSPP.Text = translator.Translate("BTNUninstallSPP");
            TGLClassicLaunch.Text = translator.Translate("TGLClassicLaunch");
            TGLTBCLaunch.Text = translator.Translate("TGLTBCLaunch");
            TGLWotLKLaunch.Text = translator.Translate("TGLWotLKLaunch");
            TGLCataLaunch.Text = translator.Translate("TGLCataLaunch");
            TGLMoPLaunch.Text = translator.Translate("TGLMoPLaunch");
            CBOXSPPVersion.Hint = translator.Translate("CBOXSPPVersion");
            ChecCLASSICInstalled.Text = translator.Translate("CheckBoxInstalled");
            ChecTBCInstalled.Text = translator.Translate("CheckBoxInstalled");
            ChecWOTLKInstalled.Text = translator.Translate("CheckBoxInstalled");
            ChecCATAInstalled.Text = translator.Translate("CheckBoxInstalled");
            ChecMOPInstalled.Text = translator.Translate("CheckBoxInstalled");
            BTNWorldClassicStart.Text = translator.Translate("BTNWorldSeparateStart");
            BTNWorldTBCStart.Text = translator.Translate("BTNWorldSeparateStart");
            BTNWorldWotLKStart.Text = translator.Translate("BTNWorldSeparateStart");
            BTNWorldCataStart.Text = translator.Translate("BTNWorldSeparateStart");
            BTNWorldMoPStart.Text = translator.Translate("BTNWorldSeparateStart");
            BTNLogonClassicStart.Text = translator.Translate("BTNLogonSeparateStart");
            BTNLogonTBCStart.Text = translator.Translate("BTNLogonSeparateStart");
            BTNLogonWotLKStart.Text = translator.Translate("BTNLogonSeparateStart");
            BTNLogonCataStart.Text = translator.Translate("BTNLogonSeparateStart");
            BTNLogonMoPStart.Text = translator.Translate("BTNLogonSeparateStart");
            BTNShowSupport.Text = translator.Translate("BTNShowSupport");
            TLTHome.SetToolTip(LBLCardSPPversionInfo, translator.Translate("LBLCardSPPversionInfo"));
            TLTHome.SetToolTip(LBLCardElulatorsInfo, translator.Translate("LBLCardElulatorsInfo"));
            #endregion
            #region"Downloader"
            LBLDownloadSpeed.Text = translator.Translate("LBLDownloadSpeedIDLE");
            LBLInstallEmulatorTitle.Text = translator.Translate("LBLInstallEmulatorTitle");
            LBLServerFiles.Text = translator.Translate("LBLServerFilesIDLE");
            LBLLocalFiles.Text = translator.Translate("LBLLocalFilesIDLE");
            LBLFilesToBeDownloaded.Text = translator.Translate("LBLFilesToBeDownloadedIDLE");
            LBLFilesToBeRemoved.Text = translator.Translate("LBLFilesToBeRemovedIDLE");
            LBLFileName.Text = translator.Translate("LBLFileNameIDLE");
            LBLFileSize.Text = translator.Translate("LBLFileSizeIDLE");
            LBLCurrentDownload.Text = translator.Translate("LBLCurrentDownload");
            LBLTotalDownload.Text = translator.Translate("LBLTotalDownload");
            #endregion
        }
        #endregion
        #region "Load Settings"
        private void CBoxSelectItems()
        {
            switch (settings.SelectedCore)
            {
                case Cores.AzerothCore:
                    CBOXSelectedEmulators.SelectedItem = "AzerothCore";
                    break;
                case Cores.CMaNGOS:
                    CBOXSelectedEmulators.SelectedItem = "CMaNGOS";
                    break;
                case Cores.CypherCore:
                    CBOXSelectedEmulators.SelectedItem = "CypherCore";
                    break;
                case Cores.TrinityCore:
                    CBOXSelectedEmulators.SelectedItem = "TrinityCore";
                    break;
                case Cores.TrinityCore335:
                    CBOXSelectedEmulators.SelectedItem = "TrinityCore 3.3.5";
                    break;
                case Cores.TrinityCoreClassic:
                    CBOXSelectedEmulators.SelectedItem = "TrinityCore Classic";
                    break;
                case Cores.VMaNGOS:
                    CBOXSelectedEmulators.SelectedItem = "VMaNGOS";
                    break;
            }
            switch (settings.SelectedSPP)
            {
                case SPP.Classic:
                    CBOXSPPVersion.SelectedItem = translator.Translate("SPPver1");
                    break;
                case SPP.TheBurningCrusade:
                    CBOXSPPVersion.SelectedItem = translator.Translate("SPPver2");
                    break;
                case SPP.WrathOfTheLichKing:
                    CBOXSPPVersion.SelectedItem = translator.Translate("SPPver3");
                    break;
                case SPP.Cataclysm:
                    CBOXSPPVersion.SelectedItem = translator.Translate("SPPver4");
                    break;
                case SPP.MistOfPandaria:
                    CBOXSPPVersion.SelectedItem = translator.Translate("SPPver5");
                    break;
            }
            switch (settings.DDNSerivce)
            {
                case DDNSerivce.Afraid:
                    CBOXDDNService.SelectedItem = "freedns.afraid.org";
                    break;
                case DDNSerivce.AllInkl:
                    CBOXDDNService.SelectedItem = "all-inkl.com";
                    break;
                case DDNSerivce.Cloudflare:
                    CBOXDDNService.SelectedItem = "cloudflare.com";
                    break;
                case DDNSerivce.DuckDNS:
                    CBOXDDNService.SelectedItem = "duckdns.org";
                    break;
                case DDNSerivce.NoIP:
                    CBOXDDNService.SelectedItem = "noip.com";
                    break;
                case DDNSerivce.Dynu:
                    CBOXDDNService.SelectedItem = "dynu.com";
                    break;
                case DDNSerivce.dynDNS:
                    CBOXDDNService.SelectedItem = "dyn.com";
                    break;
                case DDNSerivce.Enom:
                    CBOXDDNService.SelectedItem = "enom.com";
                    break;
                case DDNSerivce.Freemyip:
                    CBOXDDNService.SelectedItem = "freemyip.com";
                    break;
                case DDNSerivce.OVH:
                    CBOXDDNService.SelectedItem = "ovhcloud.com";
                    break;
                case DDNSerivce.STRATO:
                    CBOXDDNService.SelectedItem = "strato.de";
                    break;
            }
        }
        private void LoadSettingsFromFile()
        {
            if (!File.Exists("Settings.json")) { Settings.CreatSettings("Settings.json"); }
            settings = Settings.LoadSettings("Settings.json");
        }
        private void LoadSettingsUI()
        {

            CBoxSelectItems();
            //Load Installed Emulators
            TGLClassicLaunch.Checked = settings.LaunchClassicCore;
            TGLTBCLaunch.Checked = settings.LaunchTBCCore;
            TGLWotLKLaunch.Checked = settings.LaunchWotLKCore;
            TGLCataLaunch.Checked = settings.LaunchCataCore;
            TGLMoPLaunch.Checked = settings.LaunchMoPCore;
            TGLUseCustomServer.Checked = settings.LaunchCustomCore;
            //Load Names
            TXTCustomAuthName.Text = settings.CustomLogonExeName;
            TXTCustomWorldName.Text = settings.CustomWorldExeName;
            TXTCustomDatabaseName.Text = settings.DBExeName;
            //Working Directory
            TXTCustomRepackLocation.Text = settings.CustomWorkingDirectory;
            TXTCustomDatabaseLocation.Text = settings.DBLocation;
            //Database Host Data
            TXTDatabaseHost.Text = settings.DBServerHost;
            TXTDatabasePort.Text = settings.DBServerPort;
            TXTDatabaseUser.Text = settings.DBServerUser;
            TXTDatabasePassword.Text = settings.DBServerPassword;
            //Databases Tabels
            TXTCharDatabase.Text = settings.CharactersDatabase;
            TXTAuthDatabase.Text = settings.AuthDatabase;
            TXTWorldDatabase.Text = settings.WorldDatabase;
            //ToggleButtons
            TGLAutoUpdateTrion.Checked = settings.AutoUpdateTrion;
            TGLAutoUpdateCore.Checked = settings.AutoUpdateCore;
            TGLHideConsole.Checked = settings.ConsolHide;
            TGLNotificationSound.Checked = settings.NotificationSound;
            TGLStayInTray.Checked = settings.StayInTray;
            TGLCustomNames.Checked = settings.CustomNames;
            TGLRunTrionStartup.Checked = settings.RunWithWindows;
            TGLServerCrashDetection.Checked = settings.ServerCrashDetection;
            TGLClassicLaunch.Checked = settings.LaunchClassicCore;
            TGLTBCLaunch.Checked = settings.LaunchTBCCore;
            TGLWotLKLaunch.Checked = settings.LaunchWotLKCore;
            TGLCataLaunch.Checked = settings.LaunchCataCore;
            TGLMoPLaunch.Checked = settings.LaunchMoPCore;
            TGLServerStartup.Checked = settings.RunServerWithWindows;
            TGLAutoUpdateDatabase.Checked = settings.AutoUpdateDatabase;
            //CheckBoxes
            ChecCLASSICInstalled.Checked = settings.ClassicInstalled;
            ChecTBCInstalled.Checked = settings.TBCInstalled;
            ChecWOTLKInstalled.Checked = settings.WotLKInstalled;
            ChecCATAInstalled.Checked = settings.CataInstalled;
            ChecMOPInstalled.Checked = settings.MOPInstalled;
            //DDNS
            TXTDDNSDomain.Text = settings.DDNSDomain;
            TXTDDNSUsername.Text = settings.DDNSUsername;
            TXTDDNSPassword.Text = settings.DDNSPassword;
            TXTDDNSInterval.Text = settings.DDNSInterval.ToString(CultureInfo.InvariantCulture);
            TGLDDNSRunOnStartup.Checked = settings.DDNSRunOnStartup;
            TimerDinamicDNS.Enabled = settings.DDNSRunOnStartup;
            TimerDinamicDNS.Interval = settings.DDNSInterval;
            //SupporterKet
            TXTSupporterKey.Text = settings.SupporterKey;
            //Load Trion Icon
            ChangeFormIcon(settings.TrionIcon);
        }
        private void LoadSkin()
        {
            materialSkinManager = MaterialSkinManager.Instance;
            // Set this to false to disable backcolor enforcing on non-materialSkin components
            // This HAS to be set before the AddFormToManage()
            materialSkinManager!.EnforceBackcolorOnAllComponents = true;

            // MaterialSkinManager properties
            materialSkinManager!.AddFormToManage(this);
            materialSkinManager!.Theme = MaterialSkinManager.Themes.TRION;
            switch (settings.TrionTheme)
            {
                case TrionTheme.TrionBlue:
                    materialSkinManager!.ColorScheme = new ColorScheme(
                        Primary.TrionBlue500,
                        Primary.TrionBlue300,
                        Primary.TrionBlue200,
                        Accent.TrionBlue900,
                        TextShade.WHITE);
                    TLTHome.BorderColor = Settings.ConvertToColor(Primary.TrionBlue500);
                    TLTHome.TitleColor = Settings.ConvertToColor(Primary.TrionBlue500);
                    break;
                case TrionTheme.Purple:
                    materialSkinManager!.ColorScheme = new ColorScheme(
                        Primary.DeepPurple500,
                        Primary.DeepPurple300,
                        Primary.DeepPurple200,
                        Accent.Purple700,
                        TextShade.WHITE);
                    TLTHome.BorderColor = Settings.ConvertToColor(Primary.DeepPurple500);
                    TLTHome.TitleColor = Settings.ConvertToColor(Primary.DeepPurple500);
                    break;
                case TrionTheme.Green:
                    materialSkinManager!.ColorScheme = new ColorScheme(
                        Primary.Green500,
                        Primary.Green300,
                        Primary.Green200,
                        Accent.Lime700,
                        TextShade.WHITE);
                    TLTHome.BorderColor = Settings.ConvertToColor(Primary.Green500);
                    TLTHome.TitleColor = Settings.ConvertToColor(Primary.Lime700);
                    break;
                case TrionTheme.Orange:
                    materialSkinManager!.ColorScheme = new ColorScheme(
                        Primary.Orange500,
                        Primary.Orange300,
                        Primary.Orange200,
                        Accent.Orange700,
                        TextShade.WHITE);
                    TLTHome.BorderColor = Settings.ConvertToColor(Primary.Orange500);
                    TLTHome.TitleColor = Settings.ConvertToColor(Primary.Orange500);
                    break;
                default:
                    materialSkinManager!.ColorScheme = new ColorScheme(
                        Primary.TrionBlue500,
                        Primary.TrionBlue300,
                        Primary.TrionBlue200,
                        Accent.TrionBlue900,
                        TextShade.WHITE);
                    TLTHome.BorderColor = Settings.ConvertToColor(Primary.TrionBlue500);
                    TLTHome.TitleColor = Settings.ConvertToColor(Primary.TrionBlue500);
                    break;
            }
            Invalidate(); // Marks the entire form for repaint
            Update();   // Forces the repaint immediately
            Refresh();
        }
        #endregion
        private AppSettings settings;
        private MaterialSkinManager? materialSkinManager;
        private LoadingScreen loadingForm = new();
        private int AppPageSize { get; } = 1;
        private int _worldCurrentPage { get; set; } = 1;
        private int _logonCurrentPage { get; set; } = 1;
        private bool _editRealmList { get; set; }
        static System.Threading.Timer TimerKeyPress { get; set; }

        private CancellationTokenSource _cancellationTokenSource;
        private CancellationToken _cancellationToken;

        #region"MainPage"
        //Loading...
        public MainForm()
        {
            DoubleBuffered = true;
            SetStyle(ControlStyles.AllPaintingInWmPaint |
                          ControlStyles.OptimizedDoubleBuffer |
                          ControlStyles.UserPaint, true);
            loadingForm.Show();
            Opacity = 0;
            WindowState = FormWindowState.Minimized;
            ShowInTaskbar = false;
            Invalidate();
            LoadSettingsFromFile();
            if (File.Exists("update.exe")) { File.Delete("update.exe"); }
            InitializeComponent();
            InitializeCustomUX();
        }


        private async void SetupDatabaseIfMissing()
        {
            if (settings.DBInstalled || FormData.UI.Form.InstallingEmulator)
            {
                if (!FormData.UI.Form.InstallingEmulator)
                {
                    BTNStartDatabase_Click(null!, null!);
                }
                return;
            }

            if (NetworkManager.IsOffline)
            {
                AlertBox.Show(translator.Translate("Database missing but cannot download in Offline Mode."), NotificationType.Error, settings);
                return;
            }

            AlertBox.Show(translator.Translate("InstallingMysqlDatabase"), NotificationType.Info, settings);
            await Task.Delay(1000);
            _cancellationTokenSource = new CancellationTokenSource();
            RefreshDownloader();
            CardLocalFiles.Visible = false;
            LBLFilesToBeRemoved.Text = translator.Translate("LBLInitDatabaseWaiting");
            LBLInstallEmulatorTitle.Text = translator.Translate("LBLInstallDatabaseTitle");
            Update();

            Action<bool> setDbInstallingStatus = (isInstalling) =>
            {
                FormData.UI.Form.InstallingEmulator = isInstalling;
                FormData.Infos.Install.Database = isInstalling;
            };

            LBLFilesToBeRemoved.Text = translator.Translate("LBLInitDatabaseWaiting");
            MainFormTabControler.SelectedTab = TabDownloader;
            await PerformInstallationAsync(
                installationName: "database",
                apiKeyIdentifier: "database",
                destinationFolder: "/database",
                title: translator.Translate("LBLInstallDatabaseTitle"),
                setInstallingStatus: setDbInstallingStatus,
                cancellationToken: _cancellationTokenSource.Token,
                deleteFilesAfterUnzip: false
            );
        }
        private async void MainForm_LoadAsync(object sender, EventArgs e)
        {
            LoadSettingsUI();
            GetAllLanguages();
            LoadSkin();
            LoadLangauge();
            PbarRAMMachineResources.Maximum = PerformanceMonitor.GetTotalRamInMB();
            
            // Perform connection check (parallelized and fast)
            await NetworkManager.GetAPIServer();

            if (NetworkManager.IsOffline)
            {
                SetOfflineState();
            }
            else
            {
                await UpdateSppVersion();
                await StartAutoUpdate();
            }

            loadingForm.Close();
            Opacity = 1;
            ShowInTaskbar = true;
            WindowState = FormWindowState.Normal;
            Refresh();
            SetupDatabaseIfMissing();
        }
        private async void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            AlertBox.Show(translator.Translate("AlerBoxShuttingDown"), NotificationType.Info, settings);
            await Settings.SaveSettings(settings, "Settings.json");
            await AppExecuteManager.StopDatabase();
            await AppExecuteManager.StopLogon();
            await AppExecuteManager.StopWorld();
            while (FormData.UI.Form.DBRunning && ServerMonitor.ServerRunningLogon() && ServerMonitor.ServerRunningWorld())
            {}
            Environment.Exit(0);
        }
        private void TimerPanelAnimation_Tick(object sender, EventArgs e)
        {
            if (FormData.UI.Version.Update.Trion)
            {
                PNLUpdateTrion.BorderColor = PNLUpdateTrion.BorderColor == Color.LimeGreen ? Color.Black : Color.LimeGreen;
                PNLUpdateTrion.Refresh();
                BTNDownloadUpdates.Text = translator.Translate("BTNDownloadUpdatesOn");
            }
            else
            {
                BTNDownloadUpdates.Text = translator.Translate("BTNDownloadUpdatesOff");
                PNLUpdateTrion.BorderColor = Color.Black;
                PNLUpdateTrion.Refresh();
            }
            if (FormData.UI.Version.Update.Database)
            {
                PNLUpdateDatabase.BorderColor = PNLUpdateDatabase.BorderColor == Color.LimeGreen ? Color.Black : Color.LimeGreen;
                PNLUpdateDatabase.Refresh();
                BTNDownloadUpdates.Text = translator.Translate("BTNDownloadUpdatesOn");
            }
            else
            {
                PNLUpdateDatabase.BorderColor = Color.Black;
                PNLUpdateDatabase.Refresh();
                BTNDownloadUpdates.Text = translator.Translate("BTNDownloadUpdatesOff");
            }
            if (FormData.UI.Version.Update.Classic)
            {
                PNLUpdateClassicSPP.BorderColor = PNLUpdateClassicSPP.BorderColor == Color.LimeGreen ? Color.Black : Color.LimeGreen;
                PNLUpdateClassicSPP.Refresh();
                BTNDownloadUpdates.Text = translator.Translate("BTNDownloadUpdatesOn");
            }
            else
            {
                PNLUpdateClassicSPP.BorderColor = Color.Black;
                PNLUpdateClassicSPP.Refresh();
                BTNDownloadUpdates.Text = translator.Translate("BTNDownloadUpdatesOff");
            }
            if (FormData.UI.Version.Update.TBC)
            {
                PNLUpdateTbcSPP.BorderColor = PNLUpdateTbcSPP.BorderColor == Color.LimeGreen ? Color.Black : Color.LimeGreen;
                PNLUpdateTbcSPP.Refresh();
                BTNDownloadUpdates.Text = translator.Translate("BTNDownloadUpdatesOn");
            }
            else
            {
                PNLUpdateTbcSPP.BorderColor = Color.Black;
                PNLUpdateTbcSPP.Refresh();
                BTNDownloadUpdates.Text = translator.Translate("BTNDownloadUpdatesOff");
            }
            if (FormData.UI.Version.Update.WotLK)
            {
                PNLUpdateWotlkSpp.BorderColor = PNLUpdateWotlkSpp.BorderColor == Color.LimeGreen ? Color.Black : Color.LimeGreen;
                PNLUpdateWotlkSpp.Refresh();
                BTNDownloadUpdates.Text = translator.Translate("BTNDownloadUpdatesOn");
            }
            else
            {
                PNLUpdateWotlkSpp.BorderColor = Color.Black;
                PNLUpdateWotlkSpp.Refresh();
                BTNDownloadUpdates.Text = translator.Translate("BTNDownloadUpdatesOff");
            }
            if (FormData.UI.Version.Update.Cata)
            {
                PNLUpdateWotlkSpp.BorderColor = PNLUpdateCataSPP.BorderColor == Color.LimeGreen ? Color.Black : Color.LimeGreen;
                PNLUpdateCataSPP.Refresh();
                BTNDownloadUpdates.Text = translator.Translate("BTNDownloadUpdatesOn");
            }
            else
            {
                PNLUpdateCataSPP.BorderColor = Color.Black;
                PNLUpdateCataSPP.Refresh();
                BTNDownloadUpdates.Text = translator.Translate("BTNDownloadUpdatesOff");
            }
            if (FormData.UI.Version.Update.Mop)
            {
                PNLUpdateMopSPP.BorderColor = PNLUpdateMopSPP.BorderColor == Color.LimeGreen ? Color.Black : Color.LimeGreen;
                PNLUpdateMopSPP.Refresh();
                BTNDownloadUpdates.Text = translator.Translate("BTNDownloadUpdatesOn");
            }
            else
            {
                PNLUpdateMopSPP.BorderColor = Color.Black;
                PNLUpdateMopSPP.Refresh();
                BTNDownloadUpdates.Text = translator.Translate("BTNDownloadUpdatesOff");
            }
        }
        private async void TimerWacher_Tick(object sender, EventArgs e)
        {
            EnableLaunchButtonInstalled();
            ToogleButtons();
            ResurceUsage();
            ProcessesIDUpdate();
            RunServerUpdate();
            await ServerMonitor.ServerRunningLogonAsync();
            await ServerMonitor.ServerRunningWorldAsync();
            await ServerMonitor.ServerRunningDatabaseAsync();

            if (settings.ServerCrashDetection)
            {
                await AppExecuteManager.CheckAndRestartServers(settings);
            }
        }
        private async void TimerUpdate_Tick(object sender, EventArgs e)
        {
            if (NetworkManager.IsOffline) return;

            await UpdateSppVersion();
            AppUpdateManager.CheckForUpdate(settings);
            await StartAutoUpdate();
        }
        private async void BTNStartDatabase_Click(object sender, EventArgs e)
        {
            var stopDatabase = false;
            Settings.CreateMySQLConfigFile(Directory.GetCurrentDirectory(), settings.DBLocation);
            SystemData.DatabaseStartTime = DateTime.Now;
            if (!FormData.UI.Form.DBRunning && !FormData.UI.Form.DBStarted)
            {
                string arg = $"--defaults-file=\"{Directory.GetCurrentDirectory()}/my.ini\" --console";
                await AppExecuteManager.StartDatabase(settings, arg);
            }
            else
            {

                stopDatabase = true;

                if (SystemData.GetWorldProcessesID().Count > 0 || SystemData.GetLogonProcessesID().Count > 0)
                {
                    var result = MaterialMessageBox.Show(
                        this,
                        translator.Translate("UnsafeMysqlTurnOffQuestionMbox"),
                        translator.Translate("UnsafeMysqlTurnOffQuestionMboxTitle"),
                        MessageBoxButtons.YesNo,
                        true,
                        FlexibleMaterialForm.ButtonsPosition.Center
                    );

                    if (result == DialogResult.No)
                    {
                        stopDatabase = false;
                    }
                }
            }

            if (stopDatabase)
            {

                await AppExecuteManager.StopDatabase();
            }
        }
        private async void BTNStartLogon_Click(object sender, EventArgs e)
        {
            if (!ServerMonitor.ServerStartedLogon() && !ServerMonitor.ServerRunningLogon())
            {
                await AppExecuteManager.StartLogon(settings);
            }
            else
            {
                await AppExecuteManager.StopLogon();
            }
        }
        private async void BTNStartWorld_Click(object sender, EventArgs e)
        {
            if (!ServerMonitor.ServerStartedWorld() && !ServerMonitor.ServerRunningWorld())
            {
                await AppExecuteManager.StartWorld(settings);
            }
            else
            {
                await AppExecuteManager.StopWorld();
            }
        }
        private void StartWorldTSMItem_Click(object sender, EventArgs e)
        {
            BTNStartWorld_Click(sender, e);
        }
        private void StartLogonTSMItem_Click(object sender, EventArgs e)
        {
            BTNStartLogon_Click(sender, e);
        }
        private void StartDatabaseTSMItem_Click(object sender, EventArgs e)
        {
            BTNStartDatabase_Click(sender, e);
        }
        private void ExitTSMItem_ClickAsync(object sender, EventArgs e)
        {
            NIcon.Dispose();
            Application.Exit();
        }
        private void OpenTSMItem_Click(object sender, EventArgs e)
        {
            NIcon.Visible = false;
            Show();
        }
        private async void MainFormTabControler_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (e.TabPage == TabDatabaseEditor && !FormData.UI.Form.DBRunning)
            {
                var result = MaterialMessageBox.Show(
                    this,
                    translator.Translate("DatabaseNotRunningErrorMbox"),
                    translator.Translate("MessageBoxTitleInfo"),
                    MessageBoxButtons.OKCancel,
                    true,
                    FlexibleMaterialForm.ButtonsPosition.Center
                );

                if (result == DialogResult.OK)
                {
                    BTNStartDatabase_Click(sender, e);
                    await LoadRealmList(); await LoadIPAdress();
                }
                else
                {
                    e.Cancel = true; // Prevent tab change
                }
            }
            else if (e.TabPage == TabDatabaseEditor && FormData.UI.Form.DBRunning)
            {
                await LoadRealmList(); await LoadIPAdress();
            }
            if (e.TabPage == TabDownloader && !FormData.UI.Form.InstallingEmulator)
            {
                e.Cancel = true; // Prevent tab change

            }
            if (e.TabPage == TabSettings)
            {
                // Do something if needed
            }
        }
        #endregion
        #region "HomePage"
        private void ProcessesIDUpdate()
        {
            var DatabaseProcessIDs = SystemData.GetTotalDatabaseProcessIDCount() > 0 ? string.Join(", ", SystemData.GetDatabaseProcessID().Select(p => p.ID)) : "0";
            LBLDatabaseProcessID.Text = $"{translator.Translate("LBLProcessID")}: {DatabaseProcessIDs}";
            //
            var WorldProcessIDs = SystemData.GetTotalWorldProcessIDCount() > 0 ? string.Join(", ", SystemData.GetWorldProcessesID().Select(p => p.ID)) : "0";
            LBLWorldProcessID.Text = $"{translator.Translate("LBLProcessID")}: {WorldProcessIDs}";
            //
            var LogonProcessIDs = SystemData.GetTotalLogonProcessIDCount() > 0 ? string.Join(", ", SystemData.GetLogonProcessesID().Select(p => p.ID)) : "0";
            LBLLogonProcessID.Text = $"{translator.Translate("LBLProcessID")}: {LogonProcessIDs}";

        }
        private void RunServerUpdate()
        {
            if (ServerMonitor.ServerStartedWorld() && ServerMonitor.ServerRunningWorld())
            {
                TimeSpan elapsedTime = DateTime.Now - SystemData.WorldStartTime;
                LBLUpTimeWorld.Text = $"{translator.Translate("LBLUpTime")}: {elapsedTime.Days}D : {elapsedTime.Hours}H : {elapsedTime.Minutes}M : {elapsedTime.Seconds}S";
                PNLWorldServerStatus.BorderColor = Color.LimeGreen;
                PNLWorldServerStatus.Refresh();
                PICWorldServerStatus.Image = Resources.cloud_online_64;
                BTNStartWorld.Text = translator.Translate("BTNStartWorldTextON");
            }
            else
            {
                LBLUpTimeWorld.Text = $"{translator.Translate("LBLUpTime")}:  0D : 0H : 0M : 0S";
                PNLWorldServerStatus.BorderColor = Color.DarkRed;
                PNLWorldServerStatus.Refresh();
                PICWorldServerStatus.Image = Resources.cloud_offline_64;
                BTNStartWorld.Text = translator.Translate("BTNStartWorldTextOFF");
            }
            if (ServerMonitor.ServerStartedLogon() && ServerMonitor.ServerRunningLogon())
            {
                TimeSpan elapsedTime = DateTime.Now - SystemData.LogonStartTime;
                LBLUpTimeLogon.Text = $"{translator.Translate("LBLUpTime")}: {elapsedTime.Days}D : {elapsedTime.Hours}H : {elapsedTime.Minutes}M : {elapsedTime.Seconds}S";
                PNLLogonServerStatus.BorderColor = Color.LimeGreen;
                PNLLogonServerStatus.Refresh();
                PICLogonServerStatus.Image = Resources.cloud_online_64;
                BTNStartLogon.Text = translator.Translate("BTNStartLogonTextON");
            }
            else
            {
                LBLUpTimeLogon.Text = $"{translator.Translate("LBLUpTime")}:  0D : 0H : 0M : 0S";
                PNLLogonServerStatus.BorderColor = Color.DarkRed;
                PNLLogonServerStatus.Refresh();
                PICLogonServerStatus.Image = Resources.cloud_offline_64;
                BTNStartLogon.Text = translator.Translate("BTNStartLogonTextOFF");
            }
            if (FormData.UI.Form.DBRunning && FormData.UI.Form.DBStarted)
            {
                TimeSpan elapsedTime = DateTime.Now - SystemData.DatabaseStartTime;
                LBLUpTimeDatabase.Text = $"{translator.Translate("LBLUpTime")}: {elapsedTime.Days}D : {elapsedTime.Hours}H : {elapsedTime.Minutes}M : {elapsedTime.Seconds}S";
                PNLDatanasServerStatus.BorderColor = Color.LimeGreen;
                PNLDatanasServerStatus.Refresh();
                PICDatabaseServerStatus.Image = Resources.cloud_online_64;
                BTNStartDatabase.Text = translator.Translate("BTNStartDatabaseTextON");
            }
            else
            {
                LBLUpTimeDatabase.Text = $"{translator.Translate("LBLUpTime")}:  0D : 0H : 0M : 0S";
                PNLDatanasServerStatus.BorderColor = Color.DarkRed;
                PNLDatanasServerStatus.Refresh();
                PICDatabaseServerStatus.Image = Resources.cloud_offline_64;
                BTNStartDatabase.Text = translator.Translate("BTNStartDatabaseTextOFF");
            }
        }
        private async void ResurceUsage()
        {
            PbarCPUMachineResources.Value = await Task.Run(() => PerformanceMonitor.GetCpuUtilizationPercentage());
            PbarRAMMachineResources.Value = await Task.Run(() => PerformanceMonitor.GetTotalRamInMB() - PerformanceMonitor.GetCurentPcRamUsage());
            PbarRAMLogonResources.Maximum = PbarRAMMachineResources.Value;
            PbarRAMWordResources.Maximum = PbarRAMMachineResources.Value;
            if (SystemData.GetTotalWorldProcessIDCount() > 0)
            {
                // Displaying the first page
                var worldProcess = SystemData.GetWorldProcessesIDPage(_worldCurrentPage, AppPageSize);
                //loger.WriteLine($"Page {currentPage}:");
                foreach (var process in worldProcess)
                {
                    if (process.ID == 0)
                    {
                        break;
                    }
                    PbarRAMWordResources.Value = await Task.Run(() => PerformanceMonitor.ApplicationRamUsage(process.ID));
                    PbarCPUWordResources.Value = await Task.Run(() => PerformanceMonitor.ApplicationCpuUsage(process.ID));
                }
            }
            if (SystemData.GetTotalLogonProcessIDCount() > 0)
            {
                //sameas up
                var logonProcess = SystemData.GetLogonProcessesIDPage(_logonCurrentPage, AppPageSize);
                //loget message here
                foreach (var process in logonProcess)
                {
                    if (process.ID == 0)
                    {
                        break;
                    }
                    PbarRAMLogonResources.Value = await Task.Run(() => PerformanceMonitor.ApplicationRamUsage(process.ID));
                    PbarCPULogonResources.Value = await Task.Run(() => PerformanceMonitor.ApplicationCpuUsage(process.ID));
                }
            }
        }
        #endregion
        #region "S.P.P.Page"
        private async void BTNInstallSPP_Click(object sender, EventArgs e)
        {
            MainFormTabControler.SelectedTab = TabDownloader;
            // Initialize the CancellationTokenSource and token
            RefreshDownloader();
            // Set UI state for the installation process
            FormData.UI.Form.InstallingEmulator = true;

            if (!FormData.UI.Form.DBRunning)
            {
                BTNStartDatabase_Click(sender, e);
                return;
            }

            try
            {
                switch (settings.SelectedSPP)
                {
                    case SPP.Classic:
                        if (!FormData.Infos.Install.Classic)
                        {
                            if (FormData.UI.Version.Online.Classic.Contains("N/A"))
                            {
                                AlertBox.Show(translator.Translate("AlerBoxEmulatorNotFound"), NotificationType.Info, settings);
                                FormData.UI.Form.InstallingEmulator = false;
                                return;
                            }

                            MainFormTabControler.SelectedTab = TabDownloader;
                            await InstallExpansionAsync("Classic", "/classic", settings.ClassicInstalled, v => FormData.Infos.Install.Classic = v);
                        }
                        else
                        {
                            AlertBox.Show(translator.Translate("AlerBoxEmulatorInstalled"), NotificationType.Info, settings);
                            FormData.UI.Form.InstallingEmulator = false;
                            return;
                        }

                        break;

                    case SPP.TheBurningCrusade:
                        if (!FormData.Infos.Install.TBC)
                        {
                            if (FormData.UI.Version.Online.TBC.Contains("N/A"))
                            {
                                AlertBox.Show(translator.Translate("AlerBoxEmulatorNotFound"), NotificationType.Info, settings);
                                FormData.UI.Form.InstallingEmulator = false;
                                return;
                            }

                            MainFormTabControler.SelectedTab = TabDownloader;
                            await InstallExpansionAsync("TBC", "/tbc", settings.TBCInstalled, v => FormData.Infos.Install.TBC = v);
                        }
                        else
                        {
                            AlertBox.Show(translator.Translate("AlerBoxEmulatorInstalled"), NotificationType.Info, settings);
                            FormData.UI.Form.InstallingEmulator = false;
                            return;
                        }
                        break;

                    case SPP.WrathOfTheLichKing:
                        if (!FormData.Infos.Install.WotLK)
                        {
                            if (FormData.UI.Version.Online.WotLK.Contains("N/A"))
                            {
                                AlertBox.Show(translator.Translate("AlerBoxEmulatorNotFound"), NotificationType.Info, settings);
                                FormData.UI.Form.InstallingEmulator = false;
                                return;
                            }

                            MainFormTabControler.SelectedTab = TabDownloader;
                            await InstallExpansionAsync("WotLK", "/wotlk", settings.WotLKInstalled, v => FormData.Infos.Install.WotLK = v);
                        }
                        else
                        {
                            AlertBox.Show(translator.Translate("AlerBoxEmulatorInstalled"), NotificationType.Info, settings);
                            FormData.UI.Form.InstallingEmulator = false;
                            return;
                        }
                        break;

                    case SPP.Cataclysm:
                        if (!FormData.Infos.Install.Cata)
                        {
                            if (FormData.UI.Version.Online.Cata.Contains("N/A"))
                            {
                                AlertBox.Show(translator.Translate("AlerBoxEmulatorNotFound"), NotificationType.Info, settings);
                                FormData.UI.Form.InstallingEmulator = false;
                                return;
                            }

                            MainFormTabControler.SelectedTab = TabDownloader;
                            await InstallExpansionAsync("Cata", "/cata", settings.CataInstalled, v => FormData.Infos.Install.Cata = v);
                        }
                        else
                        {
                            AlertBox.Show(translator.Translate("AlerBoxEmulatorInstalled"), NotificationType.Info, settings);
                            FormData.UI.Form.InstallingEmulator = false;
                            return;
                        }
                        break;

                    case SPP.MistOfPandaria:
                        if (!FormData.Infos.Install.Mop)
                        {
                            if (FormData.UI.Version.Online.Mop.Contains("N/A"))
                            {
                                AlertBox.Show(translator.Translate("AlerBoxEmulatorNotFound"), NotificationType.Info, settings);
                                FormData.UI.Form.InstallingEmulator = false;
                                return;
                            }

                            MainFormTabControler.SelectedTab = TabDownloader;
                            await InstallExpansionAsync("MoP", "/mop", settings.MOPInstalled, v => FormData.Infos.Install.Mop = v);
                        }
                        else
                        {
                            AlertBox.Show(translator.Translate("AlerBoxEmulatorInstalled"), NotificationType.Info, settings);
                            FormData.UI.Form.InstallingEmulator = false;
                            return;
                        }
                        break;

                    default:
                        AlertBox.Show(translator.Translate("AlerBoxFailedGettingEmulator"), NotificationType.Info, settings);
                        break;
                }

                AlertBox.Show(translator.Translate("SPPInstalationSucces"), NotificationType.Info, settings);
            }
            catch (Exception ex)
            {
                AlertBox.Show($"An error occurred: {ex.Message}", NotificationType.Error, settings);
            }
        }
        /// <summary>
        /// Handles the installation process for different expansions.
        /// </summary>
        private async Task PerformInstallationAsync(
            string installationName,
            string apiKeyIdentifier,
            string destinationFolder,
            string title,
            Action<bool> setInstallingStatus,
            CancellationToken cancellationToken,
            bool deleteFilesAfterUnzip = false,
            bool showRemoveFilesCard = false)
        {
            // Prepare the Downloader UI
            RefreshDownloader();
            CardLocalFiles.Visible = false;
            DLCardRemoweFiles.Visible = showRemoveFilesCard;
            LBLInstallEmulatorTitle.Text = title;
            Update();

            if (installationName.Contains("database"))
            {
                LBLFilesToBeRemoved.Text = translator.Translate("LBLInitDatabaseWaiting");
                DLCardRemoweFiles.Visible = true;
            }

            await Task.Delay(1000); // Allow UI to update

            setInstallingStatus(true);

            // Create progress handlers for UI updates
            var serverFilesProgress = new Progress<string>(message => LBLServerFiles.Text = string.Format(CultureInfo.InvariantCulture, translator.Translate("LBLServerFiles"), message));
            var downloadSpeedProgress = new Progress<double>(message => LBLDownloadSpeed.Text = string.Format(CultureInfo.InvariantCulture, translator.Translate("LBLDownloadSpeed"), $"{message:0.##} MB/s"));
            var downloadProgress = new Progress<double>(message => PBarCurrentDownlaod.Value = (int)message);

            // Get the list of files to download from the server
            var serverFiles = await Task.Run(() =>
                NetworkManager.GetServerFiles(Links.APIRequests.GetInstallFiles(apiKeyIdentifier, settings.SupporterKey), serverFilesProgress)
            );

            // Configure the total progress bar and labels
            // Each file has two steps: Download (1) and Unzip (1) = 2
            PBARTotalDownload.Maximum = serverFiles.Count * 2;
            LBLFilesToBeDownloaded.Text = string.Format(CultureInfo.InvariantCulture, translator.Translate("LBLFilesToBeDownloaded"), serverFiles.Count);

            // Download and Unzip each file sequentially
            foreach (var file in serverFiles)
            {
                if (cancellationToken.IsCancellationRequested) break;

                LBLFileName.Text = string.Format(CultureInfo.InvariantCulture, translator.Translate("LBLFileName"), $"{file.Name}");
                LBLFileSize.Text = string.Format(CultureInfo.InvariantCulture, translator.Translate("LBLFileSize"), $"{file.Size:0.##} MB");

                // Download step
                LBLCurrentDownload.Text = translator.Translate("LBLCurrentDownload");
                await FileManager.DownloadFileAsync(file, destinationFolder, installationName, settings.SupporterKey, cancellationToken, downloadProgress, null, downloadSpeedProgress);
                PBARTotalDownload.Value++;

                if (cancellationToken.IsCancellationRequested) break;

                // Unzip step
                LBLCurrentDownload.Text = translator.Translate("LBLCurrenInstall");
                await FileManager.UnzipFileAsync(file, destinationFolder, cancellationToken, downloadProgress, null, downloadSpeedProgress);
                PBARTotalDownload.Value++;
            }

            if (deleteFilesAfterUnzip && !cancellationToken.IsCancellationRequested)
            {
                await FileManager.DeleteInstallFiles(serverFiles, destinationFolder);
            }

            InstallFinished();
            setInstallingStatus(false);
        }
        private async void BTNRepairSPP_Click(object sender, EventArgs e)
        {
            // Initialize the CancellationTokenSource and token
            _cancellationTokenSource = new CancellationTokenSource();
            _cancellationToken = _cancellationTokenSource.Token;

            // Set UI state for the repair process
            FormData.UI.Form.InstallingEmulator = true;

            try
            {
                switch (settings.SelectedSPP)
                {
                    case SPP.Classic:
                        await RepairExpansionAsync("Classic", "/classic", settings.ClassicInstalled);
                        break;

                    case SPP.TheBurningCrusade:
                        await RepairExpansionAsync("TBC", "/tbc", settings.TBCInstalled);
                        break;

                    case SPP.WrathOfTheLichKing:
                        await RepairExpansionAsync("WotLK", "/wotlk", settings.WotLKInstalled);
                        break;

                    case SPP.Cataclysm:
                        await RepairExpansionAsync("Cata", "/cata", settings.CataInstalled);
                        break;

                    case SPP.MistOfPandaria:
                        await RepairExpansionAsync("MoP", "/mop", settings.MOPInstalled);
                        break;

                    default:
                        AlertBox.Show(translator.Translate("AlerBoxFailedGettingEmulator"), NotificationType.Info, settings);
                        break;
                }

                AlertBox.Show(translator.Translate("SPPRepairSuccess"), NotificationType.Info, settings);
            }
            catch (Exception ex)
            {
                AlertBox.Show($"An error occurred: {ex.Message}", NotificationType.Error, settings);
            }
            finally
            {
                FormData.UI.Form.InstallingEmulator = false;
            }
        }
        private async Task InstallExpansionAsync(string expansionName, string folderPath, bool isInstalled, Action<bool> setInstallStatus)
        {
            if (isInstalled)
            {
                AlertBox.Show(string.Format(CultureInfo.InvariantCulture, translator.Translate("AlerBoxEmulatroInstalled"), $"{expansionName} Emulator"), NotificationType.Info, settings);
                return;
            }
            _cancellationTokenSource = new CancellationTokenSource();

            // The action here simply wraps the passed-in delegate
            Action<bool> setExpansionInstallingStatus = (isInstalling) =>
            {
                FormData.UI.Form.InstallingEmulator = isInstalling;
                setInstallStatus(isInstalling); // Call the original delegate
            };
            TrionLogger.Log($"installationName {expansionName}, apiKeyIdentifier: {expansionName.ToLower()}, destinationFolder: {folderPath}," +
                $"title: {string.Format(CultureInfo.InvariantCulture, translator.Translate("LBLInstallEmulatorTitle"), $"{expansionName}")}, ");

            await PerformInstallationAsync(
                installationName: expansionName,
                apiKeyIdentifier: expansionName.ToLower(),
                destinationFolder: folderPath,
                title: string.Format(CultureInfo.InvariantCulture, translator.Translate("LBLInstallEmulatorTitle"), $"{expansionName}"),
                setInstallingStatus: setExpansionInstallingStatus,
                cancellationToken: _cancellationTokenSource.Token,
                deleteFilesAfterUnzip: true, // Expansions delete the zips after install
                showRemoveFilesCard: true
            );
        }
        /// <summary>
        /// Repairs an existing expansion by checking for missing or corrupt files and redownloading them.
        /// </summary>
        private async Task RepairExpansionAsync(string expansionName, string folderPath, bool isInstalled)
        {
            if (!isInstalled)
            {
                AlertBox.Show(string.Format(CultureInfo.InvariantCulture, translator.Translate("AlerBoxEmulatroNotInstalled"), $"{expansionName} Emulator"), NotificationType.Info, settings);
                FormData.UI.Form.InstallingEmulator = false;
                return;
            }
            DLCardRemoweFiles.Visible = true;
            LBLInstallEmulatorTitle.Text = string.Format(CultureInfo.InvariantCulture, translator.Translate("LBLRepairEmulatorTitle"), $"{expansionName} Emulator");

            MainFormTabControler.SelectedTab = TabDownloader;
            await Task.Delay(1000);

            // Create progress handlers to ensure UI updates happen on the UI thread
            var serverFilesProgress = new Progress<string>(message => LBLServerFiles.Text = string.Format(CultureInfo.InvariantCulture, translator.Translate("LBLServerFiles"), message));
            var localFilesProgress = new Progress<string>(message => LBLLocalFiles.Text = string.Format(CultureInfo.InvariantCulture, translator.Translate("LBLLocalFiles"), message));
            var filesToBeDeletedProgress = new Progress<string>(message => LBLFilesToBeRemoved.Text = string.Format(CultureInfo.InvariantCulture, translator.Translate("LBLFilesToBeRemoved"), message));
            var filesToBeDownloadedProgress = new Progress<string>(message => LBLFilesToBeDownloaded.Text = string.Format(CultureInfo.InvariantCulture, translator.Translate("LBLFilesToBeDownloaded"), message));
            var downloadSpeedProgress = new Progress<double>(message => LBLDownloadSpeed.Text = string.Format(CultureInfo.InvariantCulture, translator.Translate("LBLDownloadSpeed"), $"{message:0.##} MB/s"));
            var downloadProgress = new Progress<double>(message => PBarCurrentDownlaod.Value = (int)message);

            // Run file and server tasks concurrently
            var serverFilesTask = Task.Run(() => NetworkManager.GetServerFiles(Links.APIRequests.GetReapirFiles(expansionName.ToLower(), settings.SupporterKey), serverFilesProgress));
            var localFilesTask = Task.Run(() => FileManager.ProcessFilesAsync(Links.Install.WotLK, localFilesProgress));

            await Task.WhenAll(serverFilesTask, localFilesTask);

            // Retrieve results
            var serverFiles = await serverFilesTask;
            var localFiles = await localFilesTask;

            // Compare files and get missing/corrupt ones
            var (missingFiles, filesToDelete) = await FileManager.CompareFiles(serverFiles, localFiles, folderPath, filesToBeDeletedProgress, filesToBeDownloadedProgress);

            PBARTotalDownload.Maximum = missingFiles.Count;

            // **Download missing/corrupt files one-by-one**
            foreach (var file in missingFiles)
            {
                LBLFileName.Text = string.Format(CultureInfo.InvariantCulture, translator.Translate("LBLFileName"), $"{file.Name}");
                LBLFileSize.Text = string.Format(CultureInfo.InvariantCulture, translator.Translate("LBLFileSize"), $"{file.Size:0.##} MB");

                await FileManager.DownloadFileAsync(file, folderPath, expansionName, settings.SupporterKey, _cancellationToken, downloadProgress, null, downloadSpeedProgress);
                PBARTotalDownload.Value++;
            }

            AlertBox.Show($"{expansionName} Repair Completed!", NotificationType.Success, settings);
        }
        // Uninstalls the selected SPP based on the provided app settings.
        private async void BTNUninstallSPP_Click(object sender, EventArgs e)
        {
            AlertBox.Show(translator.Translate("SPPUninstall"), NotificationType.Info, settings);
            switch (settings.SelectedSPP)
            {
                case Enums.SPP.Classic:
                    await AppServiceManager.StartUninstall(settings.ClassicWorkingDirectory);
                    settings.ClassicInstalled = false;
                    settings.LaunchClassicCore = false;
                    break;
                case Enums.SPP.TheBurningCrusade:
                    await AppServiceManager.StartUninstall(settings.TBCWorkingDirectory);
                    settings.TBCInstalled = false;
                    settings.LaunchTBCCore = false;
                    break;
                case Enums.SPP.WrathOfTheLichKing:
                    await AppServiceManager.StartUninstall(settings.WotLKWorkingDirectory);
                    settings.WotLKInstalled = false;
                    settings.LaunchWotLKCore = false;
                    break;
                case Enums.SPP.Cataclysm:
                    await AppServiceManager.StartUninstall(settings.CataWorkingDirectory);
                    settings.CataInstalled = false;
                    settings.LaunchCataCore = false;
                    break;
                case Enums.SPP.MistOfPandaria:
                    await AppServiceManager.StartUninstall(settings.MopWorkingDirectory);
                    settings.MOPInstalled = false;
                    settings.LaunchMoPCore = false;
                    break;
            }

        }
        private void CBOXSPPVersion_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CBOXSPPVersion.SelectedItem!.ToString() == translator.Translate("SPPver1"))
            {
                settings.SelectedSPP = SPP.Classic;
            }
            if (CBOXSPPVersion.SelectedItem.ToString() == translator.Translate("SPPver2"))
            {
                settings.SelectedSPP = SPP.TheBurningCrusade;
            }
            if (CBOXSPPVersion.SelectedItem.ToString() == translator.Translate("SPPver3"))
            {
                settings.SelectedSPP = SPP.WrathOfTheLichKing;
            }
            if (CBOXSPPVersion.SelectedItem.ToString() == translator.Translate("SPPver4"))
            {
                settings.SelectedSPP = SPP.Cataclysm;
            }
            if (CBOXSPPVersion.SelectedItem.ToString() == translator.Translate("SPPver5"))
            {
                settings.SelectedSPP = SPP.MistOfPandaria;
            }
        }
        private async void BTNWorldClassicStart_Click(object sender, EventArgs e)
        {
            await AppExecuteManager.StartWorldSeparate(
                settings.ClassicWorldExeLoc,
                settings.ClassicWorkingDirectory,
                settings.ClassicWorldExeName,
                settings.ConsolHide
                );
        }
        private async void BTNLogonClassicStart_Click(object sender, EventArgs e)
        {
            await AppExecuteManager.StartLogonSeparate(
                settings.ClassicLogonExeLoc,
                settings.ClassicWorkingDirectory,
                settings.ClassicLogonExeName,
                settings.ConsolHide
                );
        }
        private async void BTNWorldTBCStart_Click(object sender, EventArgs e)
        {
            await AppExecuteManager.StartWorldSeparate(
                settings.TBCWorldExeLoc,
                settings.TBCWorkingDirectory,
                settings.TBCWorldExeName,
                settings.ConsolHide
                );
        }
        private async void BTNLogonTBCStart_Click(object sender, EventArgs e)
        {
            await AppExecuteManager.StartLogonSeparate(
                settings.TBCLogonExeLoc,
                settings.TBCWorkingDirectory,
                settings.TBCLogonExeName,
                settings.ConsolHide
                );
        }
        private async void BTNWorldWotLKStart_Click(object sender, EventArgs e)
        {
            await AppExecuteManager.StartWorldSeparate(
                settings.WotLKWorldExeLoc,
                settings.WotLKWorkingDirectory,
                settings.WotLKWorldExeName,
                settings.ConsolHide
                );
        }
        private async void BTNLogonWotLKStart_Click(object sender, EventArgs e)
        {
            await AppExecuteManager.StartLogonSeparate(
                settings.WotLKLogonExeLoc,
                settings.WotLKWorkingDirectory,
                settings.WotLKLogonExeName,
                settings.ConsolHide
                );
        }
        private async void BTNWorldCataStart_Click(object sender, EventArgs e)
        {
            await AppExecuteManager.StartWorldSeparate(
                settings.CataWorldExeLoc,
                settings.CataWorkingDirectory,
                settings.CataWorldExeName,
                settings.ConsolHide
                );
        }
        private async void BTNLogonCataStart_Click(object sender, EventArgs e)
        {
            await AppExecuteManager.StartLogonSeparate(
                settings.CataLogonExeLoc,
                settings.CataWorkingDirectory,
                settings.CataLogonExeName,
                settings.ConsolHide
                );
        }
        private async void BTNWorldMoPStart_Click(object sender, EventArgs e)
        {
            await AppExecuteManager.StartWorldSeparate(
                settings.MopWorldExeLoc,
                settings.MopWorkingDirectory,
                settings.MopWorldExeName,
                settings.ConsolHide
                );
        }
        private async void BTNLogonMoPStart_Click(object sender, EventArgs e)
        {
            await AppExecuteManager.StartLogonSeparate(
                settings.MopLogonExeLoc,
                settings.MopWorkingDirectory,
                settings.MopLogonExeName,
                settings.ConsolHide
                );
        }
        private void EnableLaunchButtonInstalled()
        {
            TGLClassicLaunch.Enabled = ChecCLASSICInstalled.Checked;
            TGLTBCLaunch.Enabled = ChecTBCInstalled.Checked;
            TGLWotLKLaunch.Enabled = ChecWOTLKInstalled.Checked;
            TGLCataLaunch.Enabled = ChecCATAInstalled.Checked;
            TGLMoPLaunch.Enabled = ChecMOPInstalled.Checked;
        }
        private void TGLClassicLaunch_CheckedChanged(object sender, EventArgs e)
        {
            settings.LaunchClassicCore = TGLClassicLaunch.Checked;
        }
        private void TGLTBCLaunch_CheckedChanged(object sender, EventArgs e)
        {
            settings.LaunchTBCCore = TGLTBCLaunch.Checked;
        }

        private void TGLWotLKLaunch_CheckedChanged(object sender, EventArgs e)
        {
            settings.LaunchWotLKCore = TGLWotLKLaunch.Checked;
        }

        private void TGLCataLaunch_CheckedChanged(object sender, EventArgs e)
        {
            settings.LaunchCataCore = TGLCataLaunch.Checked;
        }

        private void TGLMoPLaunch_CheckedChanged(object sender, EventArgs e)
        {
            settings.LaunchMoPCore = TGLMoPLaunch.Checked;
        }
        #endregion
        #region"DatabaseEditor"
        #region"Realmlist"
        private void CBOXReamList_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectRealmList();
        }
        private async Task LoadRealmList()
        {
            CBoxGMRealmSelect.Items.Clear();
            CBOXReamList.Items.Clear();
            CBoxGMRealmSelect.Items.Add("All");
            CBoxGMRealmSelect.SelectedIndex = 0;
            try
            {
                if (settings.SelectedCore == Cores.TrinityCore ||
                    settings.SelectedCore == Cores.TrinityCore335 ||
                    settings.SelectedCore == Cores.TrinityCoreClassic ||
                    settings.SelectedCore == Cores.AzerothCore ||
                    settings.SelectedCore == Cores.CypherCore)
                {
                    var RealmLists = await RealmListManager.GetRealmListsAsync<Realmlist.TrinityBased>(settings);
                    foreach (var RealmList in RealmLists)
                    {
                        CBoxGMRealmSelect.Items.Add(RealmList.ID);
                        CBOXReamList.Items.Add(RealmList.Name);
                        if (CBOXReamList.Items.Count > 0) { CBOXReamList.SelectedIndex = 0; }
                        if (CBoxGMRealmSelect.Items.Count > 0) { CBoxGMRealmSelect.SelectedIndex = 0; }
                        TrionLogger.Log($"Realm List Loaded {RealmList.Name}");
                    }
                }
                if (settings.SelectedCore == Cores.CMaNGOS ||
                    settings.SelectedCore == Cores.VMaNGOS)
                {
                    var RealmLists = await RealmListManager.GetRealmListsAsync<Realmlist.MangosBased>(settings);
                    foreach (var RealmList in RealmLists)
                    {
                        CBoxGMRealmSelect.Items.Add(RealmList.ID);
                        CBOXReamList.Items.Add(RealmList.Name);
                        if (CBoxGMRealmSelect.Items.Count > 0) { CBoxGMRealmSelect.SelectedIndex = 0; }
                        if (CBOXReamList.Items.Count > 0) { CBOXReamList.SelectedIndex = 0; }
                        TrionLogger.Log($"Realm List Loaded {RealmList.Name}");
                    }
                }
            }
            catch (Exception ex)
            {
                TrionLogger.Log(ex.Message, "ERROR");
            }
        }
        private async void BTNCreateRealmList_Click(object sender, EventArgs e)
        {
            if (_editRealmList == true)
            {
                await CreateRealmListData();
                BTNCreateRealmList.Text = translator.Translate("BTNCreateRealmlistDataON");
                BTNCreateRealmList.Refresh();
                TXTRealmName.ReadOnly = true;
                TXTRealmLocalAddress.ReadOnly = true;
                TXTRealmSubnetMask.ReadOnly = true;
                TXTRealmPort.ReadOnly = true;
                TXTRealmGameBuild.ReadOnly = true;
                TXTRealmAddress.ReadOnly = true;
                _editRealmList = false;
                await LoadRealmList();

            }
            else if (_editRealmList == false)
            {
                TXTRealmName.Text = string.Empty;
                TXTRealmID.Text = string.Empty;
                TXTRealmLocalAddress.Text = string.Empty;
                TXTRealmPort.Text = string.Empty;
                TXTRealmAddress.Text = string.Empty;
                BTNCreateRealmList.Text = translator.Translate("BTNCreateRealmlistDataOFF");
                BTNCreateRealmList.Refresh();
                TXTRealmName.ReadOnly = false;
                TXTRealmLocalAddress.ReadOnly = false;
                TXTRealmSubnetMask.ReadOnly = false;
                TXTRealmPort.ReadOnly = false;
                TXTRealmGameBuild.ReadOnly = false;
                TXTRealmAddress.ReadOnly = false;
                _editRealmList = true;
            }
        }

        private async void BTNDeleteRealmList_Click(object sender, EventArgs e)
        {
            var id = Convert.ToInt32(TXTRealmID.Text, CultureInfo.InvariantCulture);
            await RealmListManager.DeleteRealmListAsync(settings, id);
            await LoadRealmList();
        }
        private async void BTNOpenPublic_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(settings.DDNSDomain) && !string.IsNullOrEmpty(TXTPublicIP.Text) || TXTPublicIP.Text != "0.0.0.0")
            {
                await RealmListManager.UpdateRealmListAddressAsync(int.Parse(TXTRealmID!.Text, CultureInfo.InvariantCulture), TXTPublicIP.Text, settings);
                await LoadRealmList();
                return;
            }
            else
            {
                MaterialMessageBox.Show("");
            }
            if (!string.IsNullOrEmpty(settings.DDNSDomain) && NetworkManager.IsDomainName(settings.DDNSDomain))
            {
                await RealmListManager.UpdateRealmListAddressAsync(int.Parse(TXTRealmID!.Text, CultureInfo.InvariantCulture), settings.DDNSDomain, settings);
                await LoadRealmList();
                return;
            }
            else
            {
                MaterialMessageBox.Show("");
            }
            
        }
        private async void BTNForceRefresh_Click(object sender, EventArgs e)
        {
            await LoadRealmList();
        }
        private void BTNReviveIP_Click(object sender, EventArgs e)
        {
            TXTPublicIP.PasswordChar = TXTPublicIP.PasswordChar == '⛊' ? '\0' : '⛊';
        }
        private async void SelectRealmList()
        {
            if (settings.SelectedCore == Cores.TrinityCore ||
                settings.SelectedCore == Cores.TrinityCore335 ||
                settings.SelectedCore == Cores.TrinityCoreClassic ||
                settings.SelectedCore == Cores.AzerothCore ||
                settings.SelectedCore == Cores.CypherCore)
            {
                var realmLists = await RealmListManager.GetRealmListsAsync<Realmlist.TrinityBased>(settings);
                var SearchList = realmLists.Find(obj =>
                obj.Name.ToString(CultureInfo.InvariantCulture) == CBOXReamList.SelectedItem!.ToString());
                TXTRealmID.Text = SearchList!.ID.ToString(CultureInfo.InvariantCulture);
                TXTRealmName.Text = SearchList!.Name;
                TXTRealmLocalAddress.Text = SearchList!.LocalAddress;
                TXTRealmSubnetMask.Text = SearchList!.LocalSubnetMask;
                TXTRealmPort.Text = SearchList!.Port.ToString(CultureInfo.InvariantCulture);
                TXTRealmGameBuild.Text = SearchList!.GameBuild.ToString(CultureInfo.InvariantCulture);
                TXTRealmAddress.Text = SearchList!.Address;
                TXTRealmAddress.Hint = translator.Translate("TXTRealmAddress");
                TXTRealmName.Enabled = true;
                TXTRealmLocalAddress.Enabled = true;
                TXTRealmSubnetMask.Enabled = true;
                TXTRealmPort.Enabled = true;
                TXTRealmGameBuild.Enabled = true;
                TXTRealmAddress.Enabled = true;
            }
            if (settings.SelectedCore == Cores.CMaNGOS ||
                settings.SelectedCore == Cores.VMaNGOS)
            {
                var realmLists = await RealmListManager.GetRealmListsAsync<Realmlist.MangosBased>(settings);
                var SearchList = realmLists.Find(obj =>
                obj.Name.ToString(CultureInfo.InvariantCulture) == CBOXReamList.SelectedItem!.ToString());
                TXTRealmName.Text = SearchList!.Name;
                TXTRealmLocalAddress.Text = "N/A";
                TXTRealmSubnetMask.Text = "N/A";
                TXTRealmPort.Text = SearchList!.Port.ToString(CultureInfo.InvariantCulture);
                TXTRealmGameBuild.Text = SearchList!.GameBuild.ToString(CultureInfo.InvariantCulture);
                TXTRealmAddress.Text = SearchList!.Address;
                TXTRealmAddress.Hint = translator.Translate("TXTRealmAddress");
                TXTRealmName.Enabled = true;
                TXTRealmLocalAddress.Enabled = false;
                TXTRealmSubnetMask.Enabled = false;
                TXTRealmPort.Enabled = true;
                TXTRealmGameBuild.Enabled = true;
                TXTRealmAddress.Enabled = true;
            }
        }
        private async Task LoadIPAdress()
        {
            TXTInternIP.Text = NetworkManager.GetInternalIpAddress();
            TXTPublicIP.Text = await NetworkManager.GetExternalIpAddress(Links.APIRequests.GetExternalIPv4());
        }
        private async Task SaveRealmListData()
        {

            if (settings.SelectedCore == Cores.TrinityCore ||
                settings.SelectedCore == Cores.TrinityCore335 ||
                settings.SelectedCore == Cores.TrinityCoreClassic ||
                settings.SelectedCore == Cores.AzerothCore ||
                settings.SelectedCore == Cores.CypherCore)
            {
                await AccessManager.SaveData(SqlQueryManager.UpdateRealmList(settings.SelectedCore), new
                {
                    ID = TXTRealmID.Text,
                    Name = TXTRealmName.Text,
                    Address = TXTRealmAddress.Text,
                    LocalAddress = TXTRealmLocalAddress.Text,
                    LocalSubnetMask = TXTRealmSubnetMask.Text,
                    Port = TXTRealmPort.Text,
                    GameBuild = TXTRealmGameBuild.Text
                }, AccessManager.ConnectionString(settings, settings.AuthDatabase));
            }
            if (settings.SelectedCore == Cores.CMaNGOS ||
                settings.SelectedCore == Cores.VMaNGOS)
            {
                await AccessManager.SaveData(SqlQueryManager.UpdateRealmList(settings.SelectedCore), new
                {
                    ID = TXTRealmID.Text,
                    Name = TXTRealmName.Text,
                    Address = TXTRealmAddress.Text,
                    Port = TXTRealmPort.Text,
                    GameBuild = TXTRealmGameBuild.Text
                }, AccessManager.ConnectionString(settings, settings.AuthDatabase));
            }
        }
        private async Task CreateRealmListData()
        {

            if (settings.SelectedCore == Cores.TrinityCore ||
                settings.SelectedCore == Cores.TrinityCore335 ||
                settings.SelectedCore == Cores.TrinityCoreClassic ||
                settings.SelectedCore == Cores.AzerothCore ||
                settings.SelectedCore == Cores.CypherCore)
            {
                await AccessManager.SaveData(SqlQueryManager.CreateRealmList(settings.SelectedCore), new
                {
                    Name = TXTRealmName.Text,
                    Address = TXTRealmAddress.Text,
                    LocalAddress = TXTRealmLocalAddress.Text,
                    LocalSubnetMask = TXTRealmSubnetMask.Text,
                    Port = TXTRealmPort.Text,
                    GameBuild = TXTRealmGameBuild.Text
                }, AccessManager.ConnectionString(settings, settings.AuthDatabase));
            }
            if (settings.SelectedCore == Cores.CMaNGOS ||
                settings.SelectedCore == Cores.VMaNGOS)
            {
                await AccessManager.SaveData(SqlQueryManager.UpdateRealmList(settings.SelectedCore), new
                {
                    Name = TXTRealmName.Text,
                    Address = TXTRealmAddress.Text,
                    Port = TXTRealmPort.Text,
                    GameBuild = TXTRealmGameBuild.Text
                }, AccessManager.ConnectionString(settings, settings.AuthDatabase));
            }
        }

        private async void BTNEditRealmlistData_Click(object sender, EventArgs e)
        {
            if (_editRealmList == true)
            {
                await SaveRealmListData();
                BTNEditRealmlistData.Text = translator.Translate("BTNEditRealmlistDataON");
                BTNEditRealmlistData.Refresh();
                TXTRealmName.ReadOnly = true;
                TXTRealmLocalAddress.ReadOnly = true;
                TXTRealmSubnetMask.ReadOnly = true;
                TXTRealmPort.ReadOnly = true;
                TXTRealmGameBuild.ReadOnly = true;
                TXTRealmAddress.ReadOnly = true;
                _editRealmList = false;
            }
            else if (_editRealmList == false)
            {
                BTNEditRealmlistData.Text = translator.Translate("BTNEditRealmlistDataOFF");
                BTNEditRealmlistData.Refresh();
                TXTRealmName.ReadOnly = false;
                TXTRealmLocalAddress.ReadOnly = false;
                TXTRealmSubnetMask.ReadOnly = false;
                TXTRealmPort.ReadOnly = false;
                TXTRealmGameBuild.ReadOnly = false;
                TXTRealmAddress.ReadOnly = false;
                _editRealmList = true;
            }
        }
        #endregion
        #region "Account"

        private async void BTNAccountCreate_Click(object sender, EventArgs e)
        {
            var result = await AccountManager.CreateAccount(TXTBoxCreateUserUsername.Text, TXTBoxCreateUserPassword.Text, TXTBoxCreateUserEmail.Text, settings);
            if (result == AccountOpResult.Ok)
            {
                AlertBox.Show(translator.Translate("AccountSuccessCreate"), NotificationType.Info, settings);
            }
            else
            {
                AlertBox.Show(translator.Translate("AccountFaildCreate"), NotificationType.Info, settings);
            }
            TXTBoxCreateUserUsername.Text = string.Empty;
            TXTBoxCreateUserPassword.Text = string.Empty;
            TXTBoxCreateUserEmail.Text = string.Empty;
        }
        #endregion
        #endregion
        #region"Dynamic DNS"
        private void BTNDDNSTimerStart_Click(object sender, EventArgs e)
        {
            TimerWacher.Enabled = true;
        }
        private void TImerDinamicDNS_Tick(object sender, EventArgs e)
        {
            if (settings.IPAddress != TXTPublicIP.Text)
            {
                settings.IPAddress = TXTPublicIP.Text;
                NetworkManager.UpdateDNSIP(settings);
            }
        }
        private void BTNDDNSServiceWebiste_Click(object sender, EventArgs e)
        {
            Process.Start("explorer.exe", Links.DDNSWebsits(settings.DDNSerivce));
        }
        #endregion
        #region "Settings Page"
        #region"CustomRepack"
        private void TGLUseCustomServer_CheckedChanged(object sender, EventArgs e)
        {
            settings.LaunchCustomCore = TGLUseCustomServer.Checked;
        }
        private void TGLCustomNames_CheckedChanged(object sender, EventArgs e)
        {
            settings.CustomNames = TGLCustomNames.Checked;
            TXTCustomWorldName.ReadOnly = !settings.CustomNames;
            TXTCustomDatabaseName.ReadOnly = !settings.CustomNames;
            TXTCustomAuthName.ReadOnly = !settings.CustomNames;
        }
        #endregion
        #region"Trion"
        private async Task UpdateSppVersion()
        {
            AppUpdateManager.GetSPPVersionOffline(settings);
            await AppUpdateManager.GetSPPVersionOnline(settings);
            LBLTrionVersion.Text = string.Format(CultureInfo.InvariantCulture, translator.Translate("LBLTrionVersion"), FormData.UI.Version.Local.Trion, FormData.UI.Version.Online.Trion);
            LBLDBVersion.Text = string.Format(CultureInfo.InvariantCulture, translator.Translate("LBLDBVersion"), FormData.UI.Version.Local.Database, FormData.UI.Version.Online.Database);
            LBLClassicVersion.Text = string.Format(CultureInfo.InvariantCulture, translator.Translate("LBLClassicVersion"), FormData.UI.Version.Local.Classic, FormData.UI.Version.Online.Classic);
            LBLTBCVersion.Text = string.Format(CultureInfo.InvariantCulture, translator.Translate("LBLTBCVersion"), FormData.UI.Version.Local.TBC, FormData.UI.Version.Online.TBC);
            LBLWotLKVersion.Text = string.Format(CultureInfo.InvariantCulture, translator.Translate("LBLWotLKVersion"), FormData.UI.Version.Local.WotLK, FormData.UI.Version.Online.WotLK);
            LBLCataVersion.Text = string.Format(CultureInfo.InvariantCulture, translator.Translate("LBLCataVersion"), FormData.UI.Version.Local.Cata, FormData.UI.Version.Online.Cata);
            LBLMoPVersion.Text = string.Format(CultureInfo.InvariantCulture, translator.Translate("LBLMoPVersion"), FormData.UI.Version.Local.Mop, FormData.UI.Version.Online.Mop);
        }
        private void RefreshDownloader()
        {
            CardLocalFiles.Visible = true;
            LBLCurrentDownload.Text = translator.Translate("LBLCurrentDownload");
            LBLServerFiles.Text = translator.Translate("LBLServerFilesIDLE");
            LBLLocalFiles.Text = translator.Translate("LBLLocalFilesIDLE");
            LBLFilesToBeDownloaded.Text = translator.Translate("LBLFilesToBeDownloadedIDLE");
            LBLFilesToBeRemoved.Text = translator.Translate("LBLFilesToBeRemovedIDLE");
            LBLFileName.Text = translator.Translate("LBLFileNameIDLE");
            LBLFileSize.Text = translator.Translate("LBLFileSizeIDLE");
            INITSpinner.Visible = false;
            PBarCurrentDownlaod.Value = 0;
            PBARTotalDownload.Value = 0;
        }
        private void CBOXTrionIcon_SelectedIndexChanged(object sender, EventArgs e)
        {
            settings.TrionIcon = CBOXTrionIcon.SelectedItem!.ToString()!;
            ChangeFormIcon(settings.TrionIcon);
        }
        private void BTNReviveSupporterKey_Click(object sender, EventArgs e)
        {
            TXTSupporterKey.PasswordChar = TXTSupporterKey.PasswordChar == '⛊' ? '\0' : '⛊';
        }
        private async Task StartAutoUpdate()
        {
            if (FormData.UI.Version.Update.Trion && settings.AutoUpdateTrion)
            {
                await InstallExpansionAsync("trion", "", settings.ClassicInstalled, v => FormData.Infos.Install.Trion = v);
            }
            if (FormData.UI.Version.Update.Database && settings.AutoUpdateTrion)
            {
                if (FormData.UI.Form.DBRunning)
                {
                    await AppExecuteManager.StopDatabase();
                }
                await RepairExpansionAsync("database", "/database", settings.DBInstalled);
            }
            if (FormData.UI.Version.Update.Classic && settings.AutoUpdateCore)
            {
                if (FormData.UI.Form.ClassicLogonRunning || FormData.UI.Form.ClassicWorldRunning)
                {
                    await AppExecuteManager.StopLogon();
                    await AppExecuteManager.StopWorld();
                }
                await RepairExpansionAsync("classic", "/classic", settings.ClassicInstalled);
            }
            if (FormData.UI.Version.Update.TBC && settings.AutoUpdateCore)
            {
                if (FormData.UI.Form.TBCLogonRunning || FormData.UI.Form.TBCWorldRunning)
                {
                    await AppExecuteManager.StopLogon();
                    await AppExecuteManager.StopWorld();
                }
                await RepairExpansionAsync("tbc", "/tbc", settings.TBCInstalled);
            }
            if (FormData.UI.Version.Update.WotLK && settings.AutoUpdateCore)
            {
                if (FormData.UI.Form.WotLKLogonRunning || FormData.UI.Form.WotLKWorldRunning)
                {
                    await AppExecuteManager.StopLogon();
                    await AppExecuteManager.StopWorld();
                }
                await RepairExpansionAsync("wotlk", "/wotlk", settings.WotLKInstalled);
            }
            if (FormData.UI.Version.Update.Cata && settings.AutoUpdateCore)
            {
                if (FormData.UI.Form.CataLogonRunning || FormData.UI.Form.CataWorldRunning)
                {
                    await AppExecuteManager.StopLogon();
                    await AppExecuteManager.StopWorld();
                }
                await RepairExpansionAsync("cata", "/cata", settings.CataInstalled);
            }
            if (FormData.UI.Version.Update.Mop && settings.AutoUpdateCore)
            {
                if (FormData.UI.Form.MOPLogonRunning || FormData.UI.Form.MOPWorldRunning)
                {
                    await AppExecuteManager.StopLogon();
                    await AppExecuteManager.StopWorld();
                }
                await RepairExpansionAsync("mop", "/mop", settings.CataInstalled);
            }
        }
        private void CBOXColorSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (CBOXColorSelect.SelectedItem)
            {
                case "TrionBlue":
                    settings.TrionTheme = TrionTheme.TrionBlue;
                    LoadSkin();
                    break;
                case "Purple":
                    settings.TrionTheme = TrionTheme.Purple;
                    LoadSkin();
                    break;
                case "Orange":
                    settings.TrionTheme = TrionTheme.Orange;
                    LoadSkin();
                    break;
                case "Green":
                    settings.TrionTheme = TrionTheme.Green;
                    LoadSkin();
                    break;
                default:
                    // Default to TrionBlue or keep current
                    break;
            }

        }
        private void CBOXLanguageSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            settings.TrionLanguage = CBOXLanguageSelect.SelectedItem!.ToString() ?? "en";
            LoadLangauge();
            Refresh();
        }
        private void TGLServerCrashDetection_CheckedChanged(object sender, EventArgs e)
        {
            settings.ServerCrashDetection = TGLServerCrashDetection.Checked;
        }
        private void TGLServerStartup_CheckedChanged(object sender, EventArgs e)
        {
            settings.RunServerWithWindows = TGLServerStartup.Checked;
        }
        private void TGLRunTrionStartup_CheckedChanged(object sender, EventArgs e)
        {
            settings.RunWithWindows = TGLRunTrionStartup.Checked;
        }
        private void TGLHideConsole_CheckedChanged(object sender, EventArgs e)
        {
            settings.ConsolHide = TGLHideConsole.Checked;
        }
        private void TGLNotificationSound_CheckedChanged(object sender, EventArgs e)
        {
            settings.NotificationSound = TGLNotificationSound.Checked;
        }
        private void TGLStayInTray_CheckedChanged(object sender, EventArgs e)
        {
            settings.StayInTray = TGLStayInTray.Checked;
        }
        private void TGLAutoUpdateTrion_CheckedChanged(object sender, EventArgs e)
        {
            settings.AutoUpdateTrion = TGLAutoUpdateTrion.Checked;
        }
        private void TGLAutoUpdateCore_CheckedChanged(object sender, EventArgs e)
        {
            settings.AutoUpdateCore = TGLAutoUpdateCore.Checked;
        }
        private void TGLAutoUpdateDatabase_CheckedChanged(object sender, EventArgs e)
        {
            settings.AutoUpdateDatabase = TGLAutoUpdateDatabase.Checked;
        }
        #endregion
        #region "Database"

        private async void BTNFixDatabase_Click(object sender, EventArgs e)
        {
            string Database = Links.Install.Database.Replace("/", @"\");
            string SQLLocation = $@"{Database}\extra\initSTDDatabase.sql";
            string folderPath = $@"{Database}\data";
            await FileManager.DeleteFolderAsync(folderPath);
            await AppExecuteManager.ApplicationStart(settings.DBExeLoc, settings.DBWorkingDir, "initialize MySQL", true, $"--initialize-insecure --init-file=\"{SQLLocation}\" --console");
        }
        private void HandleDatabaseToggle(string dbPrefix, CheckBox toggled)
        {
            // Disable textboxes for all preset options, enable for custom
            bool isCustom = (dbPrefix == "custom");
            TXTAuthDatabase.ReadOnly = !isCustom;
            TXTCharDatabase.ReadOnly = !isCustom;
            TXTWorldDatabase.ReadOnly = !isCustom;

            // Uncheck all toggles except the one that triggered this
            foreach (var toggle in new[] { TGLClassicDB, TGLTbcDB, TGLWotlkDB, TGLCataDB, TGLMopDB, TGLCustomDB })
            {
                if (toggle != toggled)
                    toggle.Checked = false;
            }

            if (!isCustom)
            {
                settings.AuthDatabase = $"{dbPrefix}_auth";
                settings.CharactersDatabase = $"{dbPrefix}_characters";
                settings.WorldDatabase = $"{dbPrefix}_world";
                LoadSettingsUI();
            }
        }

        // Hook this up to each CheckedChanged event
        private void TGLClassicDB_CheckedChanged(object sender, EventArgs e) => HandleDatabaseToggle("classic", TGLClassicDB);
        private void TGLTbcDB_CheckedChanged(object sender, EventArgs e) => HandleDatabaseToggle("tbc", TGLTbcDB);
        private void TGLWotlkDB_CheckedChanged(object sender, EventArgs e) => HandleDatabaseToggle("wotlk", TGLWotlkDB);
        private void TGLCataDB_CheckedChanged(object sender, EventArgs e) => HandleDatabaseToggle("cata", TGLCataDB);
        private void TGLMopDB_CheckedChanged(object sender, EventArgs e) => HandleDatabaseToggle("mop", TGLMopDB);
        private void TGLCustomDB_CheckedChanged(object sender, EventArgs e) => HandleDatabaseToggle("custom", TGLCustomDB);
        #endregion
        #endregion
        #region"Downloader"
        private async void InstallFinished()
        {
            // 2. Create progress handlers for UI updates
            var serverFilesProgress = new Progress<string>(message => LBLServerFiles.Text = string.Format(CultureInfo.InvariantCulture, translator.Translate("LBLServerFiles"), message));
            var downloadSpeedProgress = new Progress<double>(message => LBLDownloadSpeed.Text = string.Format(CultureInfo.InvariantCulture, translator.Translate("LBLDownloadSpeed"), $"{message:0.##} MB/s"));
            var downloadProgress = new Progress<double>(message => PBarCurrentDownlaod.Value = (int)message);
            _cancellationTokenSource = new CancellationTokenSource();
            _cancellationToken = _cancellationTokenSource.Token;
            if (FormData.Infos.Install.Classic)
            {

                string classic = Links.Install.Classic.Replace("/", @"\");
                await AccessManager.ExecuteSqlFileAsync($"{classic}/database/full/classicDB.sql", AccessManager.ConnectionString(settings), _cancellationToken, downloadProgress, null, downloadSpeedProgress);
                settings.ClassicInstalled = true;
                settings.LaunchClassicCore = true;
                settings.ClassicWorkingDirectory = classic;
                settings.ClassicLogonExeLoc = FileManager.GetExecutableLocation(classic, "realmd");
                settings.ClassicLogonExeName = "realmd";
                settings.ClassicWorldExeLoc = FileManager.GetExecutableLocation(classic, "mangosd");
                settings.ClassicWorldExeName = "mangosd";
                settings.ClassicLogonName = "WoW Classic Logon";
                settings.ClassicWorldName = "WoW Classic World";
                settings.SelectedCore = Cores.CMaNGOS;
                Directory.CreateDirectory("logs/classic");
            }
            if (FormData.Infos.Install.TBC)
            {
                string TBC = Links.Install.TBC.Replace("/", @"\");
                settings.TBCInstalled = true;
                settings.LaunchTBCCore = true;
                settings.TBCWorkingDirectory = TBC;
                settings.TBCLogonExeLoc = FileManager.GetExecutableLocation(TBC, "realmd");
                settings.TBCLogonExeName = "realmd";
                settings.TBCWorldExeLoc = FileManager.GetExecutableLocation(TBC, "mangosd");
                settings.TBCWorldExeName = "mangosd";
                settings.TBCLogonName = "The Burning Crusade Logon";
                settings.TBCWorldName = "The Burning Crusade World";
                await AccessManager.ExecuteSqlFileAsync($"{TBC}/database/full/tbcDB.sql", AccessManager.ConnectionString(settings), _cancellationToken, downloadProgress, null, downloadSpeedProgress);
                settings.SelectedCore = Cores.CMaNGOS;
                Directory.CreateDirectory("logs/tbc");
            }
            if (FormData.Infos.Install.WotLK)
            {
                TrionLogger.Log("Finishing WotLK Installation");
                string WotLK = Links.Install.WotLK.Replace("/", @"\");
                settings.WotLKInstalled = true;
                settings.LaunchWotLKCore = true;
                settings.WotLKWorkingDirectory = WotLK;
                settings.WotLKLogonExeLoc = FileManager.GetExecutableLocation(WotLK, "authserver");
                settings.WotLKLogonExeName = "authserver";
                settings.WotLKWorldExeLoc = FileManager.GetExecutableLocation(WotLK, "worldserver");
                settings.WotLKLogonName = "Wrath of the Lich King Logon";
                settings.WotLKWorldName = "Wrath of the Lich King World";
                settings.SelectedCore = Cores.AzerothCore;
                Directory.CreateDirectory("logs/wotlk");
            }
            if (FormData.Infos.Install.Cata)
            {
                string cata = Links.Install.Cata.Replace("/", @"\");
                settings.CataInstalled = true;
                settings.LaunchCataCore = true;
                settings.CataWorkingDirectory = cata;
                settings.CataLogonExeLoc = FileManager.GetExecutableLocation(cata, "authserver");
                settings.CataLogonExeName = "authserver";
                settings.CataWorldExeLoc = FileManager.GetExecutableLocation(cata, "worldserver");
                settings.CataWorldExeName = "worldserver";
                settings.CataLogonName = "Cataclysm Logon";
                settings.CataWorldName = "Cataclysm World";
                settings.SelectedCore = Cores.TrinityCore;
                Directory.CreateDirectory("logs/cata");
            }
            if (FormData.Infos.Install.Mop)
            {
                string Mop = Links.Install.Mop.Replace("/", @"\");
                settings.MOPInstalled = true;
                settings.LaunchMoPCore = true;
                settings.MopWorkingDirectory = Mop;
                settings.MopLogonExeLoc = FileManager.GetExecutableLocation(Mop, "authserver");
                settings.MopLogonExeName = "authserver";
                settings.MopWorldExeLoc = FileManager.GetExecutableLocation(Mop, "authserver");
                settings.MopWorldExeName = "worldserver";
                settings.MoPLogonName = "Mists of Pandaria Logon";
                settings.MoPWorldName = "Mists of Pandaria World";
                settings.SelectedCore = Cores.TrinityCore;
                Directory.CreateDirectory("logs/mop");
            }
            if (FormData.Infos.Install.Database == true)
            {
                string Database = Links.Install.Database.Replace("/", @"\");
                settings.DBInstalled = true;
                settings.DBLocation = $@"{Database}";
                settings.DBWorkingDir = $@"{Database}\bin";
                settings.DBExeLoc = FileManager.GetExecutableLocation($@"{Database}\bin", "mysqld");
                settings.DBExeName = "mysqld";
                Settings.CreateMySQLConfigFile(Directory.GetCurrentDirectory(), Database);
                string SQLLocation = $@"{Database}\extra\initSTDDatabase.sql";
                await Task.Delay(1000);
                var initID = await AppExecuteManager.ApplicationStart(settings.DBExeLoc, settings.DBWorkingDir, "initialize MySQL", true, $"--initialize-insecure --init-file=\"{SQLLocation}\" --console");
                INITSpinner.Visible = true;
                LBLFilesToBeRemoved.Text = translator.Translate("LBLInitDatabaseInit");
                while (await Task.Run(() => ServerMonitor.IsApplicationRunning(initID)))
                {
                    INITSpinner.Visible = true;
                    LBLFilesToBeRemoved.Text = translator.Translate("LBLInitDatabaseInit");
                }
                File.Delete($"{settings.DBWorkingDir}/mysql-8.4.5-winx64.zip");
                BTNStartDatabase_Click(null!, null!);
                Directory.CreateDirectory("logs/database");
            }
            if (FormData.Infos.Install.Trion == true)
            {
                Process.Start("update.exe");
                Environment.Exit(0);
            }

            if (MainFormTabControler.SelectedTab == TabDownloader)
                MainFormTabControler.SelectedTab = TabHome;
            LoadSettingsUI();
        }
        #endregion

        private void TXTOnlyNumbers(object sender, KeyPressEventArgs e)
        {
            // Allow control characters like Backspace
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Ignore the input if it's not a digit
            }
        }
        private void TXTBox_TextChanged(object sender, EventArgs e)
        {
            // Stop the timer if it's running
            TimerKeyPress?.Dispose();
            // Start a new timer
            TimerKeyPress = new(SaveDataFormTextbox, null, 1000, Timeout.Infinite);
        }
        private void SaveDataFormTextbox(object state)
        {
            settings.DDNSDomain = TXTDDNSDomain.Text;
            settings.DDNSPassword = TXTDDNSPassword.Text;
            settings.DDNSInterval = int.Parse(TXTDDNSInterval.Text, CultureInfo.InvariantCulture);
            settings.DDNSUsername = TXTDDNSUsername.Text;
            settings.SupporterKey = TXTSupporterKey.Text;
            settings.AuthDatabase = TXTAuthDatabase.Text;
            settings.CharactersDatabase = TXTCharDatabase.Text;
            settings.WorldDatabase = TXTWorldDatabase.Text;
            settings.DBServerHost = TXTDatabaseHost.Text;
            settings.DBServerPassword = TXTDatabasePassword.Text;
            settings.DBServerPort = TXTDatabasePort.Text;
            settings.DBServerUser = TXTDatabaseUser.Text;
            settings.CustomWorldExeName = TXTCustomWorldName.Text;
            settings.CustomLogonExeName = TXTCustomAuthName.Text;
            settings.DBExeName = TXTCustomDatabaseName.Text;
        }
        private void CBOXSelectedEmulators_SelectedIndexChanged(object sender, EventArgs e)
        {
            settings.SelectedCore = CBOXSelectedEmulators.SelectedItem?.ToString() switch
            {
                "AzerothCore" => Cores.AzerothCore,
                "CMaNGOS" => Cores.CMaNGOS,
                "CypherCore" => Cores.CypherCore,
                "TrinityCore" => Cores.TrinityCore,
                "TrinityCore Classic" => Cores.TrinityCoreClassic,
                "VMaNGOS" => Cores.VMaNGOS,
                _ => settings.SelectedCore // keep current if unknown
            };

        }
        private void BTNEmulatorLocation_Click(object sender, EventArgs e)
        {
            string? path = settings.SelectedCore switch
            {

            };

            if (string.IsNullOrWhiteSpace(path) || !Directory.Exists(path))
            {
                AlertBox.Show("Emulator folder not found.", NotificationType.Info, settings);
                return;
            }

            Process.Start(new ProcessStartInfo
            {
                FileName = path,
                UseShellExecute = true,
                Verb = "open"
            });
        }

        private void BTNDatabaseLocation_Click(object sender, EventArgs e)
        {
            string path = settings.DBLocation;
            if (string.IsNullOrWhiteSpace(path) || !Directory.Exists(path))
            {
                AlertBox.Show("Database folder not found.", NotificationType.Info, settings);
                return;
            }

            Process.Start(new ProcessStartInfo
            {
                FileName = path,
                UseShellExecute = true,
                Verb = "open"
            });
        }

        private async void BTNOpenIntern_Click(object sender, EventArgs e)
        {
            TXTRealmAddress.Text = TXTInternIP.Text;
            await SaveRealmListData();
        }

        private async void BTNGMCreate_Click(object sender, EventArgs e)
        {
            int userID = await AccountManager.GetUserID(TXTBoxGMUsername.Text, settings);
            int gmLevel = CBOXAccountSecurityAccess.SelectedIndex;
            int RealmId = CBoxGMRealmSelect.SelectedItem?.ToString() switch
            {
                "ALL" => -1,
                string s when int.TryParse(s, out var id) => id,
                _ => 0
            };
            var result = await AccountManager.SetGMLevel(userID, gmLevel, RealmId, settings);
            if (result == AccountOpResult.GMSet)
            {
                AlertBox.Show("Set", NotificationType.Info, settings);
            }
            else
            {
                AlertBox.Show("Faild", NotificationType.Info, settings);
            }
            TXTBoxGMUsername.Text = string.Empty;
        }
        private void BTNShowSupport_Click(object sender, EventArgs e)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = "https://flying-phoenix.dev/support.php",
                UseShellExecute = true
            });
        }
    }
}