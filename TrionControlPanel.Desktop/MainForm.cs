using MaterialSkin;
using MaterialSkin.Controls;
using System.Globalization;
using TrionControlPanel.Desktop.Properties;
using TrionControlPanel.Desktop.Extensions.Classes;
using TrionControlPanel.Desktop.Extensions.Classes.Data.Form;
using TrionControlPanel.Desktop.Extensions.Classes.Monitor;
using TrionControlPanel.Desktop.Extensions.Modules;
using TrionControlPanel.Desktop.Extensions.Modules.Lists;
using TrionControlPanelDesktop.Extensions.Modules;
using static TrionControlPanel.Desktop.Extensions.Modules.Enums;
using TrionControlPanel.Desktop.Extensions.Application;
using TrionControlPanel.Desktop.Extensions.Database;
using System.Diagnostics;
using static TrionControlPanel.Desktop.Extensions.Notification.AlertBox;
using TrionControlPanel.Desktop.Extensions.Notification;
using static TrionControlPanel.Desktop.Extensions.Classes.Data.Form.FormData;
using System.Windows.Forms;
using K4os.Compression.LZ4.Internal;
using MetroFramework.Controls;

namespace TrionControlPanelDesktop
{
    public partial class MainForm : MaterialForm
    {
        // Additional properties and fields
        private AppSettings _settings;
        private MaterialSkinManager? materialSkinManager;
        private int AppPageSize { get; } = 1;
        private int _worldCurrentPage { get; set; } = 1;
        private int _logonCurrentPage { get; set; } = 1;
        private bool _editRealmList { get; set; }
        static System.Threading.Timer TimerKeyPress { get; set; }

        private CancellationTokenSource _cancellationTokenSource;
        private CancellationToken _cancellationToken;

        #region "Load Language"
        private Translator _translator = new();

        private void PopulateComboBoxes()
        {
            // Populate SPP Version ComboBox
            CBOXSPPVersion.Items.Clear();
            CBOXSPPVersion.Items.Add(_translator.Translate("SPPver1"));
            CBOXSPPVersion.Items.Add(_translator.Translate("SPPver2"));
            CBOXSPPVersion.Items.Add(_translator.Translate("SPPver3"));
            CBOXSPPVersion.Items.Add(_translator.Translate("SPPver4"));
            CBOXSPPVersion.Items.Add(_translator.Translate("SPPver5"));
            CBOXSPPVersion.SelectedIndex = (int)_settings.SelectedSPP;

            // Populate Account Expansion ComboBox
            CBOXAccountExpansion.Items.Clear();
            for (int i = 0; i < 10; i++)
            {
                CBOXAccountExpansion.Items.Add(_translator.Translate($"AccountExpansion{i}"));
            }
            CBOXAccountExpansion.SelectedIndex = 2;

            // Populate Account Security Access ComboBox
            CBOXAccountSecurityAccess.Items.Clear();
            for (int i = 0; i < 5; i++)
            {
                CBOXAccountSecurityAccess.Items.Add(_translator.Translate($"AccountAccessLvL{i}"));
            }
            CBOXAccountSecurityAccess.SelectedIndex = 0;

            // Call method to populate other combo boxes or selections
            CBoxSelectItems();
        }

        private void LoadLanguage()
        {
            // Load selected language and apply it
            _translator.LoadLanguage(_settings.TrionLanguage);
            SetLanguage();
        }

        private void GetAllLanguages()
        {
            // Populate language selection combo box with available languages
            CBOXLanguageSelect.Items.AddRange([.. Translator.GetAvailableLanguages()]);
            CBOXLanguageSelect.SelectedItem = _settings.TrionLanguage;

            // Populate color theme combo box with available themes
            CBOXColorSelect.Items.AddRange(Enum.GetValues(typeof(Enums.TrionTheme))
                .Cast<Enums.TrionTheme>().Select(e => e.ToString()).ToArray());
            CBOXColorSelect.SelectedItem = _settings.TrionTheme.ToString();
        }

        private void SetLanguage()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(SetLanguage));
                return;
            }
            PopulateComboBoxes();

            #region "MainForm"  
            TLTHome.SetToolTip(BTNStartDatabase, _translator.Translate("BTNStartDatabaseToolTip"));
            TLTHome.SetToolTip(BTNStartLogon, _translator.Translate("BTNStartLogonToolTip"));
            TLTHome.SetToolTip(BTNStartWorld, _translator.Translate("BTNStartWorldToolTip"));
            TLTHome.SetToolTip(InfoMachineResources, _translator.Translate("InfoMachineResources"));
            TLTHome.SetToolTip(InfoLogonResorces, _translator.Translate("InfoLogonResorces"));
            TLTHome.SetToolTip(InfoWorldResorces, _translator.Translate("InfoWorldResorces"));
            BTNStartDatabase.Text = _translator.Translate("BTNStartDatabaseTextOFF");
            BTNStartLogon.Text = _translator.Translate("BTNStartLogonTextOFF");
            BTNStartWorld.Text = _translator.Translate("BTNStartWorldTextOFF");
            BTNStartWebsite.Text = _translator.Translate("BTNStartWebsiteTextOFF");
            NIcon.BalloonTipText = _translator.Translate("NIconBalloonTipText");
            NIcon.BalloonTipTitle = _translator.Translate("NIconBalloonTipTitle");
            NIcon.Text = _translator.Translate("NIconBalloonTipTitle");
            TabSettings.Text = _translator.Translate("TabSettingsTitle");
            TabHome.Text = _translator.Translate("TabHomeTitle");
            TabDatabaseEditor.Text = _translator.Translate("TabDatabaseEditorTitle");
            TabSPP.Text = _translator.Translate("TabSPPTitle");
            TabNotification.Text = _translator.Translate("TabNotification");
            TabDDNS.Text = _translator.Translate("TabDDNS");
            TabDownloader.Text = _translator.Translate("TabDownloader");
            Text = _translator.Translate("TrionFormText");
            #endregion

            #region "Home Page"
            LBLCardMachineResourcesTitle.Text = _translator.Translate("LBLCardMachineResourcesTitle").ToUpper(CultureInfo.InvariantCulture);
            LBLCardWorldResourcesTitle.Text = _translator.Translate("LBLCardWorldResourcesTitle").ToUpper(CultureInfo.InvariantCulture);
            LBLCardLogonResourcesTitle.Text = _translator.Translate("LBLCardLogonResourcesTitle").ToUpper(CultureInfo.InvariantCulture);
            LBLRAMTextMachineResources.Text = _translator.Translate("LBLRAMTextMachineResources");
            LBLCPUTextMachineResources.Text = _translator.Translate("LBLCPUTextMachineResources");
            LBLRAMTextWorldResources.Text = _translator.Translate("LBLRAMTextMachineResources");
            LBLCPUTextWorldResources.Text = _translator.Translate("LBLCPUTextMachineResources");
            LBLRAMTextLogonResources.Text = _translator.Translate("LBLRAMTextMachineResources");
            LBLCPUTextLogonResources.Text = _translator.Translate("LBLCPUTextMachineResources");
            LBLDatabaseProcessID.Text = _translator.Translate("LBLProcessID");
            LBLWorldProcessID.Text = _translator.Translate("LBLProcessID");
            LBLLogonProcessID.Text = _translator.Translate("LBLProcessID");
            LBLUpTimeDatabase.Text = _translator.Translate("LBLUpTime");
            LBLUpTimeWorld.Text = _translator.Translate("LBLUpTime");
            LBLUpTimeLogon.Text = _translator.Translate("LBLUpTime");
            #endregion
            
            #region "Datebase Edior"
            #region "Reamlist"
            TXTRealmID.Hint = _translator.Translate("TXTRealmID");
            TXTRealmName.Hint = _translator.Translate("TXTRealmName");
            TXTRealmAddress.Hint = _translator.Translate("TXTRealmAddress");
            TXTRealmLocalAddress.Hint = _translator.Translate("TXTRealmLocalAddress");
            TXTRealmSubnetMask.Hint = _translator.Translate("TXTRealmSubnetMask");
            TXTRealmPort.Hint = _translator.Translate("TXTRealmPort");
            TXTRealmGameBuild.Hint = _translator.Translate("TXTRealmGameBuild");
            CBOXReamList.Hint = _translator.Translate("CBOXReamList");
            TXTDomainName.Hint = _translator.Translate("TXTDomainName");
            TXTInternIP.Hint = _translator.Translate("TXTInternIP");
            TXTPublicIP.Hint = _translator.Translate("TXTPublicIP");
            BTNOpenPublic.Text = _translator.Translate("BTNOpenPublic");
            BTNOpenIntern.Text = _translator.Translate("BTNOpenIntern");
            BTNEditRealmlistData.Text = _translator.Translate("BTNEditRealmlistDataON");
            BTNCreateRealmList.Text = _translator.Translate("BTNCreateRealmList");
            BTNDeleteRealmList.Text = _translator.Translate("BTNDeleteRealmList");
            BTNForceRefresh.Text = _translator.Translate("BTNForceRefresh");
            LBLCardRealmDataTitle.Text = _translator.Translate("LBLCardRealmDataTitle").ToUpper(CultureInfo.InvariantCulture);
            LBLCardRealmOptionTitle.Text = _translator.Translate("LBLCardRealmOptionTitle").ToUpper(CultureInfo.InvariantCulture);
            LBLCardRealmActionTitle.Text = _translator.Translate("LBLCardRealmActionTitle").ToUpper(CultureInfo.InvariantCulture);
            TLTHome.SetToolTip(LBLCardRealmDataInfo, _translator.Translate("LBLCardRealmDataInfo"));
            TLTHome.SetToolTip(LBLCardRealmOptionInfo, _translator.Translate("LBLCardRealmOptionInfo"));
            TLTHome.SetToolTip(LBLCardRealmActionInfo, _translator.Translate("LBLCardRealmActionInfo"));
            TabRealmList.Text = _translator.Translate("TabRealmList");
            TabAccount.Text = _translator.Translate("TabAccount");
            #endregion
            #region"Account Editor"
            TLTHome.SetToolTip(LBLCardAccountCreateInfo, _translator.Translate("LBLCardAccountCreateInfo"));
            TLTHome.SetToolTip(LBLCardAccountAccessInfo, _translator.Translate("LBLCardAccountAccessInfo"));
            TLTHome.SetToolTip(LBLCardPasswordResetInfo, _translator.Translate("LBLCardPasswordResetInfo"));
            TXTBoxCreateUserUsername.Hint = _translator.Translate("TXTBoxCreateUserUsername");
            TXTBoxCreateUserPassword.Hint = _translator.Translate("TXTBoxCreateUserPassword");
            TXTBoxCreateUserEmail.Hint = _translator.Translate("TXTBoxCreateUserEmail");
            CBOXAccountExpansion.Hint = _translator.Translate("CBOXAccountExpansion");
            BTNAccountCreate.Text = _translator.Translate("BTNAccountCreate");
            LBLCardAccountCreate.Text = _translator.Translate("LBLCardAccountCreate").ToUpper(CultureInfo.InvariantCulture);
            LBLCardAccountAdmin.Text = _translator.Translate("LBLCardAccountAdmin").ToUpper(CultureInfo.InvariantCulture);
            LBLCardAccountPassword.Text = _translator.Translate("LBLCardAccountPassword").ToUpper(CultureInfo.InvariantCulture);
            TXTBoxGMUsername.Hint = _translator.Translate("TXTBoxGMUsername");
            CBoxGMRealmSelect.Hint = _translator.Translate("CBoxGMRealmSelect");
            CBOXAccountSecurityAccess.Hint = _translator.Translate("CBOXAccountSecurityAccess");
            TXTBoxPassUsername.Hint = _translator.Translate("TXTBoxPassUsername");
            TXTBoxPassPassword.Hint = _translator.Translate("TXTBoxPassPassword");
            TXTBoxPassRePassword.Hint = _translator.Translate("TXTBoxPassRePassword");
            BTNTBoxPassResset.Text = _translator.Translate("BTNTBoxPassResset");
            BTNGMCreate.Text = _translator.Translate("BTNGMCreate");
            TGLAccountShowPassword.Text = _translator.Translate("TGLAccountShowPassword");
            #endregion
            #endregion
            
            #region"Settings"
            TabDatabase.Text = _translator.Translate("TabDatabase");
            TabCustom.Text = _translator.Translate("TabCustom");
            TabTrion.Text = _translator.Translate("TabTrion");
            #region "Trion"
            BTNDownloadUpdates.Text = _translator.Translate("BTNDownloadUpdatesOff");
            TGLServerCrashDetection.Text = _translator.Translate("TGLServerCrashDetection");
            TGLServerStartup.Text = _translator.Translate("TGLServerStartup");
            TGLRunTrionStartup.Text = _translator.Translate("TGLRunTrionStartup");
            TGLHideConsole.Text = _translator.Translate("TGLHideConsole");
            TGLNotificationSound.Text = _translator.Translate("TGLNotificationSound");
            TGLStayInTray.Text = _translator.Translate("TGLStayInTray");
            TGLAutoUpdateTrion.Text = _translator.Translate("TGLAutoUpdateTrion");
            TGLAutoUpdateCore.Text = _translator.Translate("TGLAutoUpdateCore");
            TGLAutoUpdateDatabase.Text = _translator.Translate("TGLAutoUpdateDatabase");
            LBLCardCustomPreferencesTitle.Text = _translator.Translate("LBLCardCustomPreferencesTitle").ToUpper(CultureInfo.InvariantCulture);
            LBLCardUpdateDashboardTitle.Text = _translator.Translate("LBLCardUpdateDashboardTitle").ToUpper(CultureInfo.InvariantCulture);
            CBOXColorSelect.Hint = _translator.Translate("CBOXColorSelect");
            CBOXLanguageSelect.Hint = _translator.Translate("CBOXLanguageSelect");
            TLTHome.SetToolTip(LBLCardCustomPreferencesInfo, _translator.Translate("LBLCardCustomPreferencesInfo"));
            TLTHome.SetToolTip(LBLCardUpdateDashboardInfo, _translator.Translate("LBLCardUpdateDashboardInfo"));
            TXTSupporterKey.Hint = _translator.Translate("TXTSupporterKeyHint");
            #endregion
            #region"Custom Emulators"
            TXTCustomDatabaseLocation.Hint = _translator.Translate("TXTCustomDatabaseLocation");
            TXTCustomRepackLocation.Hint = _translator.Translate("TXTCustomRepackLocation");
            LBLCardCustomEmulatorTitle.Text = _translator.Translate("LBLCardCustomEmulatorTitle").ToUpper(CultureInfo.InvariantCulture);
            CBOXSelectedEmulators.Hint = _translator.Translate("CBOXSelectedEmulators");
            BTNEmulatorLocation.Text = _translator.Translate("BTNEmulatorLocation");
            BTNDatabaseLocation.Text = _translator.Translate("BTNDatabaseLocation");
            TGLUseCustomServer.Text = _translator.Translate("TGLUseCustomServer");
            LBLCardCustomNamesTitle.Text = _translator.Translate("LBLCardCustomNamesTitle").ToUpper(CultureInfo.InvariantCulture);
            TXTCustomAuthName.Hint = _translator.Translate("TXTCustomAuthName");
            TXTCustomWorldName.Hint = _translator.Translate("TXTCustomWorldName");
            TXTCustomDatabaseName.Hint = _translator.Translate("TxtCustomDatabaseName");
            TGLCustomNames.Text = _translator.Translate("TGLCustomNames");
            BTNAscEmuWebsite.Text = _translator.Translate("BTNAscEmuWebsite");
            BTNACoreWebsite.Text = _translator.Translate("BTNACoreWebsite");
            BTNCMangosWebsite.Text = _translator.Translate("BTNCMangosWebsite");
            BTNCypherWebsite.Text = _translator.Translate("BTNCypherWebsite");
            BTNTrinityCoreWebsite.Text = _translator.Translate("BTNTrinityCoreWebsite");
            BTNSkyFireWebsite.Text = _translator.Translate("BTNSkyFireWebsite");
            TLTHome.SetToolTip(LBLCardCustomEmulatorInfo, _translator.Translate("LBLCardCustomEmulatorInfo"));
            TLTHome.SetToolTip(LBLCardCustomNamesInfo, _translator.Translate("LBLCardCustomNamesInfo"));
            #endregion
            #region"Database"
            LBLCardDatabaseCredencialsTitle.Text = _translator.Translate("LBLCardDatabaseCredencialsTitle").ToUpper(CultureInfo.InvariantCulture);
            LBLCardTableNameTitle.Text = _translator.Translate("LBLCardTableNameTitle").ToUpper(CultureInfo.InvariantCulture);
            LBLCardPreconfiguredDBTitle.Text = _translator.Translate("LBLCardPreconfiguredDBTitle").ToUpper(CultureInfo.InvariantCulture);
            LBLCardDatabaseBCTitle.Text = _translator.Translate("LBLCardDatabaseBCTitle").ToUpper(CultureInfo.InvariantCulture);
            TXTDatabaseHost.Hint = _translator.Translate("TXTDatabaseHost");
            TXTDatabasePort.Hint = _translator.Translate("TXTDatabasePorta");
            TXTDatabaseUser.Hint = _translator.Translate("TXTDatabaseUser");
            TXTDatabasePassword.Hint = _translator.Translate("TXTDatabasePassword");
            BTNTestConnection.Text = _translator.Translate("BTNTestConnection");
            TXTAuthDatabase.Hint = _translator.Translate("TXTAuthDatabase");
            TXTCharDatabase.Hint = _translator.Translate("TXTCharDatabase");
            TXTWorldDatabase.Hint = _translator.Translate("TXTWorldDatabase");
            BTNDeleteAuth.Text = _translator.Translate("BTNDeleteAuth");
            BTNDeleteChar.Text = _translator.Translate("BTNDeleteChar");
            BTNDeleteWorld.Text = _translator.Translate("BTNDeleteWorld");
            TGLClassicDB.Text = _translator.Translate("TGLClassicDB");
            TGLTbcDB.Text = _translator.Translate("TGLTbcDB");
            TGLWotlkDB.Text = _translator.Translate("TGLWotlkDB");
            TGLCataDB.Text = _translator.Translate("TGLCataDB");
            TGLMopDB.Text = _translator.Translate("TGLMopDB");
            TGLCustomDB.Text = _translator.Translate("TGLCustomDB");
            TGLAuthBackup.Text = _translator.Translate("TGLAuthBackup");
            TGLCharBackup.Text = _translator.Translate("TGLCharBackup");
            TGLWorldBackup.Text = _translator.Translate("TGLWorldBackup");
            BTNDatabaseBackup.Text = _translator.Translate("BTNDatabaseBackup");
            BTNLoadBackup.Text = _translator.Translate("BTNLoadBackup");
            BTNFixDatabase.Text = _translator.Translate("BTNFixDatabase");
            TLTHome.SetToolTip(LBLCardDatabaseCredencialsInfo, _translator.Translate("LBLCardDatabaseCredencialsInfo"));
            TLTHome.SetToolTip(LBLCardTableNameInfo, _translator.Translate("LBLCardTableNameInfo"));
            TLTHome.SetToolTip(LBLCardPreconfiguredDBInfo, _translator.Translate("LBLCardPreconfiguredDBInfo"));
            TLTHome.SetToolTip(LBLCardDatabaseBCInfo, _translator.Translate("LBLCardDatabaseBCInfo"));
            #endregion
            #region"DDNS"
            TLTHome.SetToolTip(LBLCardDDNSSettingsInfo, _translator.Translate("LBLCardDDNSSettingsInfo"));
            TLTHome.SetToolTip(LBLCardDDNSTimerInfos, _translator.Translate("LBLCardDDNSTimerInfos"));
            LBLCardDDNSSettingsTitle.Text = _translator.Translate("LBLCardDDNSSettingsTitle").ToUpper(CultureInfo.InvariantCulture);
            LBLCardDDNSTimerTitle.Text = _translator.Translate("LBLCardDDNSTimerTitle").ToUpper(CultureInfo.InvariantCulture);
            CBOXDDNService.Hint = _translator.Translate("CBOXDDNService");
            TXTDDNSDomain.Hint = _translator.Translate("TXTDDNSDomain");
            TXTDDNSUsername.Hint = _translator.Translate("TXTDDNSUsername");
            TXTDDNSPassword.Hint = _translator.Translate("TXTDDNSPassword");
            TXTDDNSInterval.Hint = _translator.Translate("TXTDDNSInterval");
            TGLDDNSRunOnStartup.Text = _translator.Translate("TGLDDNSRunOnStartup");
            BTNDDNSServiceWebiste.Text = _translator.Translate("BTNDDNSServiceWebiste");
            BTNDDNSTimerStart.Text = _translator.Translate("BTNDDNSTimerStart");
            #endregion
            #endregion
            
            #region"SPP"
            LBLCardSPPVersionTitle.Text = _translator.Translate("LBLCardSPPVersionTitle").ToUpper(CultureInfo.InvariantCulture);
            LBLCardSPPRunTitle.Text = _translator.Translate("LBLCardSPPRunTitle").ToUpper(CultureInfo.InvariantCulture);
            BTNInstallSPP.Text = _translator.Translate("BTNInstallSPP");
            BTNRepairSPP.Text = _translator.Translate("BTNRepairSPP");
            BTNUninstallSPP.Text = _translator.Translate("BTNUninstallSPP");
            TGLClassicLaunch.Text = _translator.Translate("TGLClassicLaunch");
            TGLTBCLaunch.Text = _translator.Translate("TGLTBCLaunch");
            TGLWotLKLaunch.Text = _translator.Translate("TGLWotLKLaunch");
            TGLCataLaunch.Text = _translator.Translate("TGLCataLaunch");
            TGLMoPLaunch.Text = _translator.Translate("TGLMoPLaunch");
            CBOXSPPVersion.Hint = _translator.Translate("CBOXSPPVersion");
            ChecCLASSICInstalled.Text = _translator.Translate("CheckBoxInstalled");
            ChecTBCInstalled.Text = _translator.Translate("CheckBoxInstalled");
            ChecWOTLKInstalled.Text = _translator.Translate("CheckBoxInstalled");
            ChecCATAInstalled.Text = _translator.Translate("CheckBoxInstalled");
            ChecMOPInstalled.Text = _translator.Translate("CheckBoxInstalled");
            BTNWorldClassicStart.Text = _translator.Translate("BTNWorldSeparateStart");
            BTNWorldTBCStart.Text = _translator.Translate("BTNWorldSeparateStart");
            BTNWorldWotLKStart.Text = _translator.Translate("BTNWorldSeparateStart");
            BTNWorldCataStart.Text = _translator.Translate("BTNWorldSeparateStart");
            BTNWorldMoPStart.Text = _translator.Translate("BTNWorldSeparateStart");
            BTNLogonClassicStart.Text = _translator.Translate("BTNLogonSeparateStart");
            BTNLogonTBCStart.Text = _translator.Translate("BTNLogonSeparateStart");
            BTNLogonWotLKStart.Text = _translator.Translate("BTNLogonSeparateStart");
            BTNLogonCataStart.Text = _translator.Translate("BTNLogonSeparateStart");
            BTNLogonMoPStart.Text = _translator.Translate("BTNLogonSeparateStart");
            TLTHome.SetToolTip(LBLCardSPPversionInfo, _translator.Translate("LBLCardSPPversionInfo"));
            TLTHome.SetToolTip(LBLCardElulatorsInfo, _translator.Translate("LBLCardElulatorsInfo"));
            #endregion
            
            #region"Downloader"
            LBLDownloadSpeed.Text = _translator.Translate("LBLDownloadSpeedIDLE");
            LBLInstallEmulatorTitle.Text = _translator.Translate("LBLInstallEmulatorTitle");
            LBLServerFiles.Text = _translator.Translate("LBLServerFilesIDLE");
            LBLLocalFiles.Text = _translator.Translate("LBLLocalFilesIDLE");
            LBLFilesToBeDownloaded.Text = _translator.Translate("LBLFilesToBeDownloadedIDLE");
            LBLFilesToBeRemoved.Text = _translator.Translate("LBLFilesToBeRemovedIDLE");
            LBLFileName.Text = _translator.Translate("LBLFileNameIDLE");
            LBLFileSize.Text = _translator.Translate("LBLFileSizeIDLE");
            LBLCurrentDownload.Text = _translator.Translate("LBLCurrentDownload");
            LBLTotalDownload.Text = _translator.Translate("LBLTotalDownload");
            #endregion
        }
        #endregion

        #region "Load Settings"
        private void CBoxSelectItems()
        {
            // Set selected emulator based on selected core
            switch (_settings.SelectedCore)
            {
                case Cores.AscEmu:
                    CBOXSelectedEmulators.SelectedItem = "AscEmu";
                    break;
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

            // Set selected SPP version based on selected SPP
            switch (_settings.SelectedSPP)
            {
                case SPP.Classic:
                    CBOXSPPVersion.SelectedItem = _translator.Translate("SPPver1");
                    break;
                case SPP.TheBurningCrusade:
                    CBOXSPPVersion.SelectedItem = _translator.Translate("SPPver2");
                    break;
                case SPP.WrathOfTheLichKing:
                    CBOXSPPVersion.SelectedItem = _translator.Translate("SPPver3");
                    break;
                case SPP.Cataclysm:
                    CBOXSPPVersion.SelectedItem = _translator.Translate("SPPver4");
                    break;
                case SPP.MistOfPandaria:
                    CBOXSPPVersion.SelectedItem = _translator.Translate("SPPver5");
                    break;
            }

            // Set DDNS service selection based on selected service
            switch (_settings.DDNSerivce)
            {
                case DDNSService.Afraid:
                    CBOXDDNService.SelectedItem = "freedns.afraid.org";
                    break;
                case DDNSService.AllInkl:
                    CBOXDDNService.SelectedItem = "all-inkl.com";
                    break;
                case DDNSService.Cloudflare:
                    CBOXDDNService.SelectedItem = "cloudflare.com";
                    break;
                case DDNSService.DuckDNS:
                    CBOXDDNService.SelectedItem = "duckdns.org";
                    break;
                case DDNSService.NoIP:
                    CBOXDDNService.SelectedItem = "noip.com";
                    break;
                case DDNSService.Dynu:
                    CBOXDDNService.SelectedItem = "dynu.com";
                    break;
                case DDNSService.DynDNS:
                    CBOXDDNService.SelectedItem = "dyn.com";
                    break;
                case DDNSService.Enom:
                    CBOXDDNService.SelectedItem = "enom.com";
                    break;
                case DDNSService.Freemyip:
                    CBOXDDNService.SelectedItem = "freemyip.com";
                    break;
                case DDNSService.OVH:
                    CBOXDDNService.SelectedItem = "ovhcloud.com";
                    break;
                case DDNSService.STRATO:
                    CBOXDDNService.SelectedItem = "strato.de";
                    break;
            }
        }

        private void LoadSettings()
        {
            // Create or reset logs file
            if (!File.Exists("Trion.logs"))
            {
                File.Create("Trion.logs").Close();
            }
            else
            {
                File.Delete("Trion.logs");
                File.Create("Trion.logs").Close();
            }

            // Load settings from JSON or create new settings if file doesn't exist
            if (!File.Exists("Settings.json"))
            {
                Settings.CreatSettings("Settings.json");
            }
            _settings = Settings.LoadSettings("Settings.json");
        }

        private void LoadSettingsUI()
        {
            // Load items into ComboBoxes and UI elements based on loaded settings
            CBoxSelectItems();

            // Load emulator launch settings
            TGLClassicLaunch.Checked = _settings.LaunchClassicCore;
            TGLTBCLaunch.Checked = _settings.LaunchTBCCore;
            TGLWotLKLaunch.Checked = _settings.LaunchWotLKCore;
            TGLCataLaunch.Checked = _settings.LaunchCataCore;
            TGLMoPLaunch.Checked = _settings.LaunchMoPCore;
            TGLUseCustomServer.Checked = _settings.LaunchCustomCore;

            // Load custom executable names
            TXTCustomAuthName.Text = _settings.CustomLogonExeName;
            TXTCustomWorldName.Text = _settings.CustomWorldExeName;
            TXTCustomDatabaseName.Text = _settings.DBExeName;

            // Load working directories
            TXTCustomRepackLocation.Text = _settings.CustomWorkingDirectory;
            TXTCustomDatabaseLocation.Text = _settings.DBLocation;

            // Load database connection settings
            TXTDatabaseHost.Text = _settings.DBServerHost;
            TXTDatabasePort.Text = _settings.DBServerPort;
            TXTDatabaseUser.Text = _settings.DBServerUser;
            TXTDatabasePassword.Text = _settings.DBServerPassword;

            // Load database names for different categories
            TXTCharDatabase.Text = _settings.CharactersDatabase;
            TXTAuthDatabase.Text = _settings.AuthDatabase;
            TXTWorldDatabase.Text = _settings.WorldDatabase;

            // Load toggle buttons for various settings
            TGLAutoUpdateTrion.Checked = _settings.AutoUpdateTrion;
            TGLAutoUpdateCore.Checked = _settings.AutoUpdateCore;
            TGLHideConsole.Checked = _settings.ConsolHide;
            TGLNotificationSound.Checked = _settings.NotificationSound;
            TGLStayInTray.Checked = _settings.StayInTray;
            TGLCustomNames.Checked = _settings.CustomNames;
            TGLRunTrionStartup.Checked = _settings.RunWithWindows;
            TGLServerCrashDetection.Checked = _settings.ServerCrashDetection;

            // Load additional emulator launch settings
            TGLServerStartup.Checked = _settings.RunServerWithWindows;
            TGLAutoUpdateDatabase.Checked = _settings.AutoUpdateDatabase;

            // Load installation status checkboxes
            ChecCLASSICInstalled.Checked = _settings.ClassicInstalled;
            ChecTBCInstalled.Checked = _settings.TBCInstalled;
            ChecWOTLKInstalled.Checked = _settings.WotLKInstalled;
            ChecCATAInstalled.Checked = _settings.CataInstalled;
            ChecMOPInstalled.Checked = _settings.MOPInstalled;

            // Load DDNS settings
            TXTDDNSDomain.Text = _settings.DDNSDomain;
            TXTDDNSUsername.Text = _settings.DDNSUsername;
            TXTDDNSPassword.Text = _settings.DDNSPassword;
            TXTDDNSInterval.Text = _settings.DDNSInterval.ToString(CultureInfo.InvariantCulture);
            TGLDDNSRunOnStartup.Checked = _settings.DDNSRunOnStartup;
            TimerDinamicDNS.Enabled = _settings.DDNSRunOnStartup;
            TimerDinamicDNS.Interval = _settings.DDNSInterval;

            // Load supporter key
            TXTSupporterKey.Text = _settings.SupporterKey;
        }

        private void LoadSkin()
        {
            // Initialize material skin manager and set the theme
            materialSkinManager = MaterialSkinManager.Instance;

            // Enforce backcolor on all components
            materialSkinManager!.EnforceBackcolorOnAllComponents = true;

            // Add the form to the material skin manager
            materialSkinManager!.AddFormToManage(this);
            materialSkinManager!.Theme = MaterialSkinManager.Themes.TRION;

            // Apply the selected theme's color scheme
            switch (_settings.TrionTheme)
            {
                case TrionTheme.TrionBlue:
                    materialSkinManager!.ColorScheme = new ColorScheme(
                        Primary.TrionBlue500,
                        Primary.TrionBlue300,
                        Primary.TrionBlue200,
                        Accent.TrionBlue900,
                        TextShade.WHITE
                    );
                    TLTHome.BorderColor = Settings.ConvertToColor(Primary.TrionBlue500);
                    TLTHome.TitleColor = Settings.ConvertToColor(Primary.TrionBlue500);
                    break;
                case TrionTheme.Purple:
                    materialSkinManager!.ColorScheme = new ColorScheme(
                        Primary.DeepPurple500,
                        Primary.DeepPurple300,
                        Primary.DeepPurple200,
                        Accent.Purple700,
                        TextShade.WHITE
                    );
                    TLTHome.BorderColor = Settings.ConvertToColor(Primary.DeepPurple500);
                    TLTHome.TitleColor = Settings.ConvertToColor(Primary.DeepPurple500);
                    break;
                case TrionTheme.Green:
                    materialSkinManager!.ColorScheme = new ColorScheme(
                        Primary.Green500,
                        Primary.Green300,
                        Primary.Green200,
                        Accent.Lime700,
                        TextShade.WHITE
                    );
                    TLTHome.BorderColor = Settings.ConvertToColor(Primary.Green500);
                    TLTHome.TitleColor = Settings.ConvertToColor(Primary.Lime700);
                    break;
                case TrionTheme.Orange:
                    materialSkinManager!.ColorScheme = new ColorScheme(
                        Primary.Orange500,
                        Primary.Orange300,
                        Primary.Orange200,
                        Accent.Orange700,
                        TextShade.WHITE
                    );
                    TLTHome.BorderColor = Settings.ConvertToColor(Primary.Orange500);
                    TLTHome.TitleColor = Settings.ConvertToColor(Primary.Orange500);
                    break;
                default:
                    materialSkinManager!.ColorScheme = new ColorScheme(
                        Primary.TrionBlue500,
                        Primary.TrionBlue300,
                        Primary.TrionBlue200,
                        Accent.TrionBlue900,
                        TextShade.WHITE
                    );
                    TLTHome.BorderColor = Settings.ConvertToColor(Primary.TrionBlue500);
                    TLTHome.TitleColor = Settings.ConvertToColor(Primary.TrionBlue500);
                    break;
            }

            // Force a UI update to reflect changes
            Invalidate();
            Update();
            Refresh();
        }
        #endregion

        #region "MainPage"

        // Main Form Constructor
        public MainForm()
        {
            // Load settings and initialize components
            LoadSettings();
            InitializeComponent();

            // Delete the update executable if it exists
            if (File.Exists("update.exe"))
            {
                File.Delete("update.exe");
            }
        }

        // Toggle button states based on installation status
        private void ToogleButtons()
        {
            bool isInstallingEmulator = FormData.UI.Form.InstallingEmulator;
            BTNInstallSPP.Enabled = !isInstallingEmulator;
            BTNUninstallSPP.Enabled = !isInstallingEmulator;
            BTNRepairSPP.Enabled = !isInstallingEmulator;
        }

        // Setup database if missing and handle its installation
        private async void SetupDatabaseIfMissing()
        {
            // If DB is not installed and not currently installing
            if (!_settings.DBInstalled && !FormData.UI.Form.InstallingEmulator)
            {
                // Initialize cancellation token for async tasks
                _cancellationTokenSource = new CancellationTokenSource();
                _cancellationToken = _cancellationTokenSource.Token;

                // Set UI state to indicate installation process is starting
                FormData.UI.Form.InstallingEmulator = true;

                // Initialize progress reporting for UI updates
                var serverFilesProgress = new Progress<string>(message => LBLServerFiles.Text = $"Online Files: {message}");
                var localFilesProgress = new Progress<string>(message => LBLLocalFiles.Text = $"Local Files: {message}");
                var downloadSpeed = new Progress<double>(message => LBLDownloadSpeed.Text = $"Speed: {message:0.##} MB/s");
                var downloadProgress = new Progress<double>(message => PBarCurrentDownlaod.Value = (int)message);
                var filesToBeDeleted = new Progress<string>(message => LBLFilesToBeRemoved.Text = $"Files to be removed: {message}");
                var filesToBeDownloaded = new Progress<string>(message => LBLFilesToBeDownloaded.Text = $"Files to be downloaded: {message}");

                // Switch to downloader tab
                MainFormTabControler.SelectedTab = TabDownloader;
                await Task.Delay(1000);
                Infos.Install.Database = true;

                // Run server and local file tasks concurrently
                var serverFilesDatabaseTask = Task.Run(() => NetworkManager.GetServerFiles(Links.APIRequests.GetServerFiles("database", _settings.SupporterKey), serverFilesProgress));
                var localFilesDatabaseTask = Task.Run(() => FileManager.ProcessFilesAsync(Links.Install.Database, localFilesProgress));

                // Wait for both tasks to complete
                await Task.WhenAll(serverFilesDatabaseTask, localFilesDatabaseTask);

                // Retrieve results from both tasks
                var serverFilesDatabase = await serverFilesDatabaseTask;
                var localFilesDatabase = await localFilesDatabaseTask;

                // Compare files and get missing ones
                var (missingFilesDatabase, filesToDeleteDatabase) = await FileManager.CompareFiles(serverFilesDatabase, localFilesDatabase, "/database", filesToBeDeleted, filesToBeDownloaded);

                PBARTotalDownload.Maximum = missingFilesDatabase.Count;

                // Download missing files one-by-one
                foreach (var file in missingFilesDatabase)
                {
                    LBLFileName.Text = $"File Name: {file.Name}";
                    LBLFileSize.Text = $"File Size: {file.Size} Kb";

                    // Directly await the download without Task.Run
                    await FileManager.DownloadFileAsync(file, "/database", _cancellationToken, downloadProgress, null, downloadSpeed);
                    PBARTotalDownload.Value++;
                }

                // Finalize installation
                InstallFinished();
                FormData.UI.Form.InstallingEmulator = false;
                Infos.Install.Database = false;
            }
        }

        // Main Form Load event handler
        private async void MainForm_LoadAsync(object sender, EventArgs e)
        {
            LoadSettingsUI();
            GetAllLanguages();
            LoadSkin();
            LoadLanguage();
            PbarRAMMachineResources.Maximum = PerformanceMonitor.GetTotalRamInMB();

            await NetworkManager.GetAPIServer();
            await UpdateSppVersion();
            SetupDatabaseIfMissing();
        }

        // Save settings on form closing
        private async void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            await Settings.SaveSettings(_settings, "Settings.json");
        }

        // Timer event to animate update panel borders
        private void TimerPanelAnimation_Tick(object sender, EventArgs e)
        {
            UpdatePanelBordersAndButtonText();
        }

        // Timer event to update various UI components
        private void TimerWacher_Tick(object sender, EventArgs e)
        {
            ToogleButtons();
            ResurceUsage();
            ProcessesIDUpdate();
            RunServerUpdate();
        }

        // Timer event to update SPP version
        private async void TimerUpdate_Tick(object sender, EventArgs e)
        {
            await UpdateSppVersion();
        }

        // Start or stop the database based on current status
        private async void BTNStartDatabase_Click(object sender, EventArgs e)
        {
            Settings.CreateMySQLConfigFile(Directory.GetCurrentDirectory(), _settings.DBLocation);
            SystemData.DatabaseStartTime = DateTime.Now;

            if (!FormData.UI.Form.DBRunning && !FormData.UI.Form.DBStarted)
            {
                string arg = $"--defaults-file=\"{Directory.GetCurrentDirectory()}/my.ini\" --console";
                await AppExecuteMenager.StartDatabase(_settings, arg);
            }
            else
            {
                await AppExecuteMenager.StopDatabase();
            }
        }

        // Start or stop the logon service based on current status
        private async void BTNStartLogon_Click(object sender, EventArgs e)
        {
            if (!ServerMonitor.ServerStartedLogon() && !ServerMonitor.ServerRunningLogon())
            {
                await AppExecuteMenager.StartLogon(_settings);
            }
            else
            {
                await AppExecuteMenager.StopLogon();
            }
        }

        // Start or stop the world service based on current status
        private async void BTNStartWorld_Click(object sender, EventArgs e)
        {
            if (!ServerMonitor.ServerStartedWorld() && !ServerMonitor.ServerRunningWorld())
            {
                await AppExecuteMenager.StartWorld(_settings);
            }
            else
            {
                await AppExecuteMenager.StopWorld();
            }
        }

        // Start world service from system tray menu
        private void StartWorldTSMItem_Click(object sender, EventArgs e)
        {
            BTNStartWorld_Click(sender, e);
        }

        // Start logon service from system tray menu
        private void StartLogonTSMItem_Click(object sender, EventArgs e)
        {
            BTNStartLogon_Click(sender, e);
        }

        // Start database service from system tray menu
        private void StartDatabaseTSMItem_Click(object sender, EventArgs e)
        {
            BTNStartDatabase_Click(sender, e);
        }

        // Exit application from system tray menu
        private void ExitTSMItem_ClickAsync(object sender, EventArgs e)
        {
            NIcon.Dispose();
            Application.Exit();
        }

        // Open main form from system tray menu
        private void OpenTSMItem_Click(object sender, EventArgs e)
        {
            NIcon.Visible = false;
            Show();
        }

        // Handle tab selection change for different tabs
        private async void MainFormTabControler_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (e.TabPage == TabDatabaseEditor && !FormData.UI.Form.DBRunning)
            {
                var result = MaterialMessageBox.Show(
                    this,
                    _translator.Translate("DatabaseNotRunningErrorMbox"),
                    _translator.Translate("MessageBoxTitleInfo"),
                    MessageBoxButtons.OKCancel,
                    true,
                    FlexibleMaterialForm.ButtonsPosition.Center
                );

                if (result == DialogResult.OK)
                {
                    BTNStartDatabase_Click(sender, e);
                    await LoadRealmList();
                    await LoadIPAdress();
                }
                else
                {
                    e.Cancel = true; // Prevent tab change
                }
            }

            if (e.TabPage == TabDownloader && !FormData.UI.Form.InstallingEmulator)
            {
                e.Cancel = true; // Prevent tab change
            }
        }

        // Helper method to update panel borders and button text
        private void UpdatePanelBordersAndButtonText()
        {
            UpdatePanelBorder(PNLUpdateTrion, FormData.UI.Version.Update.Trion, "BTNDownloadUpdatesOn", "BTNDownloadUpdatesOff");
            UpdatePanelBorder(PNLUpdateDatabase, FormData.UI.Version.Update.Database, "BTNDownloadUpdatesOn", "BTNDownloadUpdatesOff");
            UpdatePanelBorder(PNLUpdateClassicSPP, FormData.UI.Version.Update.Classic, "BTNDownloadUpdatesOn", "BTNDownloadUpdatesOff");
            UpdatePanelBorder(PNLUpdateTbcSPP, FormData.UI.Version.Update.TBC, "BTNDownloadUpdatesOn", "BTNDownloadUpdatesOff");
            UpdatePanelBorder(PNLUpdateWotlkSpp, FormData.UI.Version.Update.WotLK, "BTNDownloadUpdatesOn", "BTNDownloadUpdatesOff");
            UpdatePanelBorder(PNLUpdateCataSPP, FormData.UI.Version.Update.Cata, "BTNDownloadUpdatesOn", "BTNDownloadUpdatesOff");
            UpdatePanelBorder(PNLUpdateMopSPP, FormData.UI.Version.Update.Mop, "BTNDownloadUpdatesOn", "BTNDownloadUpdatesOff");
        }

        // Helper method to update individual panel borders and button text based on update status
        private void UpdatePanelBorder(MetroPanel panel, bool isUpdateAvailable, string updateOnText, string updateOffText)
        {
            if (isUpdateAvailable)
            {
                panel.BorderColor = panel.BorderColor == Color.LimeGreen ? Color.Black : Color.LimeGreen;
                BTNDownloadUpdates.Text = _translator.Translate(updateOnText);
            }
            else
            {
                panel.BorderColor = Color.Black;
                BTNDownloadUpdates.Text = _translator.Translate(updateOffText);
            }
        }
        #endregion

        #region "HomePage"
        private void ProcessesIDUpdate()
        {
            // Fetch and join the process IDs for database, world, and logon processes
            var DatabaseProcessIDs = SystemData.GetTotalDatabaseProcessIDCount() > 0
                ? string.Join(", ", SystemData.GetDatabaseProcessID().Select(p => p.ID))
                : "0";  // If no IDs, return "0"
            LBLDatabaseProcessID.Text = $"{_translator.Translate("LBLProcessID")}: {DatabaseProcessIDs}";

            var WorldProcessIDs = SystemData.GetTotalWorldProcessIDCount() > 0
                ? string.Join(", ", SystemData.GetWorldProcessesID().Select(p => p.ID))
                : "0";  // If no IDs, return "0"
            LBLWorldProcessID.Text = $"{_translator.Translate("LBLProcessID")}: {WorldProcessIDs}";

            var LogonProcessIDs = SystemData.GetTotalLogonProcessIDCount() > 0
                ? string.Join(", ", SystemData.GetLogonProcessesID().Select(p => p.ID))
                : "0";  // If no IDs, return "0"
            LBLLogonProcessID.Text = $"{_translator.Translate("LBLProcessID")}: {LogonProcessIDs}";
        }

        private void RunServerUpdate()
        {
            // Update the status of the World Server
            if (ServerMonitor.ServerStartedWorld() && ServerMonitor.ServerRunningWorld())
            {
                TimeSpan elapsedTime = DateTime.Now - SystemData.WorldStartTime;
                LBLUpTimeWorld.Text = $"{_translator.Translate("LBLUpTime")}: {elapsedTime.Days}D : {elapsedTime.Hours}H : {elapsedTime.Minutes}M : {elapsedTime.Seconds}S";
                PNLWorldServerStatus.BorderColor = Color.LimeGreen;
                PNLWorldServerStatus.Refresh();
                PICWorldServerStatus.Image = Resources.cloud_online_64;
                BTNStartWorld.Text = _translator.Translate("BTNStartWorldTextON");
            }
            else
            {
                LBLUpTimeWorld.Text = $"{_translator.Translate("LBLUpTime")}:  0D : 0H : 0M : 0S";
                PNLWorldServerStatus.BorderColor = Color.DarkRed;
                PNLWorldServerStatus.Refresh();
                PICWorldServerStatus.Image = Resources.cloud_offline_64;
                BTNStartWorld.Text = _translator.Translate("BTNStartWorldTextOFF");
            }

            // Update the status of the Logon Server
            if (ServerMonitor.ServerStartedLogon() && ServerMonitor.ServerRunningLogon())
            {
                TimeSpan elapsedTime = DateTime.Now - SystemData.LogonStartTime;
                LBLUpTimeLogon.Text = $"{_translator.Translate("LBLUpTime")}: {elapsedTime.Days}D : {elapsedTime.Hours}H : {elapsedTime.Minutes}M : {elapsedTime.Seconds}S";
                PNLLogonServerStatus.BorderColor = Color.LimeGreen;
                PNLLogonServerStatus.Refresh();
                PICLogonServerStatus.Image = Resources.cloud_online_64;
                BTNStartLogon.Text = _translator.Translate("BTNStartLogonTextON");
            }
            else
            {
                LBLUpTimeLogon.Text = $"{_translator.Translate("LBLUpTime")}:  0D : 0H : 0M : 0S";
                PNLLogonServerStatus.BorderColor = Color.DarkRed;
                PNLLogonServerStatus.Refresh();
                PICLogonServerStatus.Image = Resources.cloud_offline_64;
                BTNStartLogon.Text = _translator.Translate("BTNStartLogonTextOFF");
            }

            // Update the status of the Database Server
            if (FormData.UI.Form.DBRunning && FormData.UI.Form.DBStarted)
            {
                TimeSpan elapsedTime = DateTime.Now - SystemData.DatabaseStartTime;
                LBLUpTimeDatabase.Text = $"{_translator.Translate("LBLUpTime")}: {elapsedTime.Days}D : {elapsedTime.Hours}H : {elapsedTime.Minutes}M : {elapsedTime.Seconds}S";
                PNLDatanasServerStatus.BorderColor = Color.LimeGreen;
                PNLDatanasServerStatus.Refresh();
                PICDatabaseServerStatus.Image = Resources.cloud_online_64;
                BTNStartDatabase.Text = _translator.Translate("BTNStartDatabaseTextON");
            }
            else
            {
                LBLUpTimeDatabase.Text = $"{_translator.Translate("LBLUpTime")}:  0D : 0H : 0M : 0S";
                PNLDatanasServerStatus.BorderColor = Color.DarkRed;
                PNLDatanasServerStatus.Refresh();
                PICDatabaseServerStatus.Image = Resources.cloud_offline_64;
                BTNStartDatabase.Text = _translator.Translate("BTNStartDatabaseTextOFF");
            }
        }

        private async void ResurceUsage()
        {
            // Set the CPU and RAM resource usage for the machine
            PbarCPUMachineResources.Value = await Task.Run(() => PerformanceMonitor.GetCpuUtilizationPercentage());
            PbarRAMMachineResources.Value = await Task.Run(() => PerformanceMonitor.GetTotalRamInMB() - PerformanceMonitor.GetCurentPcRamUsage());
            PbarRAMLogonResources.Maximum = PbarRAMMachineResources.Value;
            PbarRAMWordResources.Maximum = PbarRAMMachineResources.Value;

            // Update resource usage for World processes
            if (SystemData.GetTotalWorldProcessIDCount() > 0)
            {
                var worldProcess = SystemData.GetWorldProcessesIDPage(_worldCurrentPage, AppPageSize);
                foreach (var process in worldProcess)
                {
                    PbarRAMWordResources.Value = await Task.Run(() => PerformanceMonitor.ApplicationRamUsage(process.ID));
                    PbarCPUWordResources.Value = await Task.Run(() => PerformanceMonitor.ApplicationCpuUsage(process.ID));
                }
            }

            // Update resource usage for Logon processes
            if (SystemData.GetTotalLogonProcessIDCount() > 0)
            {
                var logonProcess = SystemData.GetLogonProcessesIDPage(_logonCurrentPage, AppPageSize);
                foreach (var process in logonProcess)
                {
                    PbarRAMLogonResources.Value = await Task.Run(() => PerformanceMonitor.ApplicationRamUsage(process.ID));
                    PbarCPULogonResources.Value = await Task.Run(() => PerformanceMonitor.ApplicationCpuUsage(process.ID));
                }
            }
        }
        #endregion

        #region "S.P.P.Page"

        private async void BTNInstallSPP_Click(object sender, EventArgs e)
        {
            // Initialize the CancellationTokenSource and token
            _cancellationTokenSource = new CancellationTokenSource();
            _cancellationToken = _cancellationTokenSource.Token;

            // Set UI state for the installation process
            FormData.UI.Form.InstallingEmulator = true;

            // Create progress reporting for UI updates
            var serverFilesProgress = new Progress<string>(message => LBLServerFiles.Text = $"Online Files: {message}");
            var localFilesProgress = new Progress<string>(message => LBLLocalFiles.Text = $"Local Files: {message}");
            var downloadSpeed = new Progress<double>(message => LBLDownloadSpeed.Text = $"Speed: {message:0.##} MB/s");
            var downloadProgress = new Progress<double>(message => PBarCurrentDownlaod.Value = (int)message);
            var filesToBeDeleted = new Progress<string>(message => LBLFilesToBeRemoved.Text = $"Files to be removed: {message}");
            var filesToBeDownloaded = new Progress<string>(message => LBLFilesToBeDownloaded.Text = $"Files to be downlaoded: {message}");

            try
            {
                // Switch on the selected SPP
                switch (_settings.SelectedSPP)
                {
                    case SPP.Classic:
                        LBLInstallEmulatorTitle.Text = string.Format(CultureInfo.InvariantCulture, _translator.Translate("LBLInstallEmulatorTitle"), "Classic Emulator");
                        break;

                    case SPP.WrathOfTheLichKing:
                        LBLInstallEmulatorTitle.Text = string.Format(CultureInfo.InvariantCulture, _translator.Translate("LBLInstallEmulatorTitle"), "WotLK Emulator");
                        if (_settings.WotLKInstalled == false)
                        {
                            AlertBox.Show(string.Format(CultureInfo.InvariantCulture, _translator.Translate("AlerBoxEmulatroInstalled"), "WotLK Emulator"), NotificationType.Info, _settings);
                            FormData.UI.Form.InstallingEmulator = false;
                            break;
                        }
                        MainFormTabControler.SelectedTab = TabDownloader;
                        await Task.Delay(1000);
                        FormData.Infos.Install.WotLK = true;

                        // Run file and server tasks concurrently on background threads
                        var serverFilesWotlkTask = Task.Run(() => NetworkManager.GetServerFiles(Links.APIRequests.GetServerFiles("wotlk", _settings.SupporterKey), serverFilesProgress));
                        var localFilesWotlkTask = Task.Run(() => FileManager.ProcessFilesAsync(Links.Install.WotLK, localFilesProgress));
                        await Task.WhenAll(serverFilesWotlkTask, localFilesWotlkTask);

                        // Retrieve results after both tasks complete
                        var serverFilesWotlk = await serverFilesWotlkTask;
                        var localFilesWotlk = await localFilesWotlkTask;

                        // Compare files and get missing ones
                        var (missingFilesWotlk, filesToDeleteWotlk) = await FileManager.CompareFiles(serverFilesWotlk, localFilesWotlk, "/wotlk", filesToBeDeleted, filesToBeDownloaded);
                        PBARTotalDownload.Maximum = missingFilesWotlk.Count;

                        // Download missing files one-by-one
                        foreach (var file in missingFilesWotlk)
                        {
                            BeginInvoke(new Action(() =>
                            {
                                LBLFileName.Text = $"File Name: {file.Name}";
                                LBLFileSize.Text = $"File Size: {file.Size} Kb";
                            }));

                            await FileManager.DownloadFileAsync(file, "/wotlk", _cancellationToken, downloadProgress, null, downloadSpeed);
                            PBARTotalDownload.Value++;
                        }

                        await FileManager.DeleteFiles(filesToDeleteWotlk);

                        // Installation finished!
                        InstallFinished();
                        FormData.UI.Form.InstallingEmulator = false;
                        FormData.Infos.Install.WotLK = false;
                        break;

                    default:
                        BeginInvoke(new Action(() =>
                        {
                            AlertBox.Show(_translator.Translate("AlerBoxFaildGettingEmulatro"), NotificationType.Info, _settings);
                        }));
                        break;
                }

                // Notify installation success
                AlertBox.Show(_translator.Translate("SPPInstalationSucces"), NotificationType.Info, _settings);

            }
            catch (Exception ex)
            {
                // Handle any exceptions and ensure UI is updated correctly
                BeginInvoke(new Action(() =>
                {
                    AlertBox.Show($"An error occurred: {ex.Message}", NotificationType.Error, _settings);
                }));
            }
        }

        // Repair SPP
        private async void BTNRepairSPP_Click(object sender, EventArgs e)
        {
            AlertBox.Show(_translator.Translate("SPPRepair"), NotificationType.Info, _settings);
            await AppServiceManager.RepairSPP(_settings);
        }

        // Uninstall SPP
        private async void BTNUninstallSPP_Click(object sender, EventArgs e)
        {
            AlertBox.Show(_translator.Translate("SPPUninstall"), NotificationType.Info, _settings);
            await AppServiceManager.UninstallSPP(_settings);
        }

        // Handle SPP version selection
        private void CBOXSPPVersion_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedVersion = CBOXSPPVersion.SelectedItem!.ToString();
            if (selectedVersion == _translator.Translate("SPPver1"))
                _settings.SelectedSPP = SPP.Classic;
            else if (selectedVersion == _translator.Translate("SPPver2"))
                _settings.SelectedSPP = SPP.TheBurningCrusade;
            else if (selectedVersion == _translator.Translate("SPPver3"))
                _settings.SelectedSPP = SPP.WrathOfTheLichKing;
            else if (selectedVersion == _translator.Translate("SPPver4"))
                _settings.SelectedSPP = SPP.Cataclysm;
            else if (selectedVersion == _translator.Translate("SPPver5"))
                _settings.SelectedSPP = SPP.MistOfPandaria;
        }

        // Start World or Logon for Classic, TBC, WotLK, Cata, MoP
        private async void StartSPPExecutable_Click(object sender, EventArgs e)
        {
            var button = sender as Button;
            if (button == null) return;

            // Determine which world/logon button was clicked and call the helper method
            switch (button.Name)
            {
                case "BTNWorldClassicStart":
                    await AppExecuteMenager.StartWorldOrLogonExecutable(_settings.ClassicWorldExeLoc, _settings.ClassicWorkingDirectory, _settings.ClassicWorldExeName, _settings.ConsolHide, true);
                    break;
                case "BTNLogonClassicStart":
                    await AppExecuteMenager.StartWorldOrLogonExecutable(_settings.ClassicLogonExeLoc, _settings.ClassicWorkingDirectory, _settings.ClassicLogonExeName, _settings.ConsolHide, false);
                    break;
                case "BTNWorldTBCStart":
                    await AppExecuteMenager.StartWorldOrLogonExecutable(_settings.TBCWorldExeLoc, _settings.TBCWorkingDirectory, _settings.TBCWorldExeName, _settings.ConsolHide, true);
                    break;
                case "BTNLogonTBCStart":
                    await AppExecuteMenager.StartWorldOrLogonExecutable(_settings.TBCLogonExeLoc, _settings.TBCWorkingDirectory, _settings.TBCLogonExeName, _settings.ConsolHide, false);
                    break;
                case "BTNWorldWotLKStart":
                    await AppExecuteMenager.StartWorldOrLogonExecutable(_settings.WotLKWorldExeLoc, _settings.WotLKWorkingDirectory, _settings.WotLKWorldExeName, _settings.ConsolHide, true);
                    break;
                case "BTNLogonWotLKStart":
                    await AppExecuteMenager.StartWorldOrLogonExecutable(_settings.WotLKLogonExeLoc, _settings.WotLKWorkingDirectory, _settings.WotLKLogonExeName, _settings.ConsolHide, false);
                    break;
                case "BTNWorldCataStart":
                    await AppExecuteMenager.StartWorldOrLogonExecutable(_settings.CataWorldExeLoc, _settings.CataWorkingDirectory, _settings.CataWorldExeName, _settings.ConsolHide, true);
                    break;
                case "BTNLogonCataStart":
                    await AppExecuteMenager.StartWorldOrLogonExecutable(_settings.CataLogonExeLoc, _settings.CataWorkingDirectory, _settings.CataLogonExeName, _settings.ConsolHide, false);
                    break;
                case "BTNWorldMoPStart":
                    await AppExecuteMenager.StartWorldOrLogonExecutable(_settings.MopWorldExeLoc, _settings.MopWorkingDirectory, _settings.MopWorldExeName, _settings.ConsolHide, true);
                    break;
                case "BTNLogonMoPStart":
                    await AppExecuteMenager.StartWorldOrLogonExecutable(_settings.MopLogonExeLoc, _settings.MopWorkingDirectory, _settings.MopLogonExeName, _settings.ConsolHide, false);
                    break;
            }
        }


        // Toggle settings for Classic, TBC, WotLK, Cata, MoP launches
        private void ToggleSPPCoreLaunch_CheckedChanged(object sender, EventArgs e)
        {
            var toggle = sender as MaterialSwitch;
            if (toggle == null) return;

            // Use a switch statement or individual if checks to handle each toggle button
            switch (toggle.Name)
            {
                case "TGLClassicLaunch":
                    _settings.LaunchClassicCore = toggle.Checked;
                    break;
                case "TGLTBCLaunch":
                    _settings.LaunchTBCCore = toggle.Checked;
                    break;
                case "TGLWotLKLaunch":
                    _settings.LaunchWotLKCore = toggle.Checked;
                    break;
                case "TGLCataLaunch":
                    _settings.LaunchCataCore = toggle.Checked;
                    break;
                case "TGLMoPLaunch":
                    _settings.LaunchMoPCore = toggle.Checked;
                    break;
            }
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
            CBoxGMRealmSelect.Items.Add("All");
            CBoxGMRealmSelect.SelectedIndex = 0;
            try
            {
                if (_settings.SelectedCore == Cores.AscEmu)
                {
                    var RealmLists = await RealmListManager.GetRealmLists<Realmlist.AscemuBased>(_settings);
                    foreach (var RealmList in RealmLists)
                    {
                        CBOXReamList.Items.Add(RealmList.ID);
                        CBoxGMRealmSelect.Items.Add(RealmList.ID);
                        if (CBOXReamList.Items.Count > 0) { CBOXReamList.SelectedIndex = 0; }
                        if (CBoxGMRealmSelect.Items.Count > 0) { CBoxGMRealmSelect.SelectedIndex = 0; }
                        TrionLogger.Log($"Realm List Loaded {RealmList.ID}");
                    }
                }
                if (_settings.SelectedCore == Cores.TrinityCore ||
                    _settings.SelectedCore == Cores.TrinityCore335 ||
                    _settings.SelectedCore == Cores.TrinityCoreClassic ||
                    _settings.SelectedCore == Cores.AzerothCore ||
                    _settings.SelectedCore == Cores.CypherCore)
                {
                    var RealmLists = await RealmListManager.GetRealmLists<Realmlist.TrinityBased>(_settings);
                    foreach (var RealmList in RealmLists)
                    {
                        CBoxGMRealmSelect.Items.Add(RealmList.ID);
                        CBOXReamList.Items.Add(RealmList.Name);
                        if (CBOXReamList.Items.Count > 0) { CBOXReamList.SelectedIndex = 0; }
                        if (CBoxGMRealmSelect.Items.Count > 0) { CBoxGMRealmSelect.SelectedIndex = 0; }
                        TrionLogger.Log($"Realm List Loaded {RealmList.Name}");
                    }
                }
                if (_settings.SelectedCore == Cores.CMaNGOS ||
                    _settings.SelectedCore == Cores.VMaNGOS)
                {
                    var RealmLists = await RealmListManager.GetRealmLists<Realmlist.MangosBased>(_settings);
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
        private void BTNCreateRealmList_Click(object sender, EventArgs e)
        {

        }

        private void BTNDeleteRealmList_Click(object sender, EventArgs e)
        {

        }
        private async void BTNOpenPublic_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_settings.DDNSDomain) && !string.IsNullOrEmpty(TXTPublicIP.Text) || TXTPublicIP.Text != "0.0.0.0")
            {
                await RealmListManager.UpdateRealmListAddress(int.Parse(TXTRealmID!.Text, CultureInfo.InvariantCulture), TXTPublicIP.Text, _settings);
                return;
            }
            else
            {
                MaterialMessageBox.Show("");
            }
            if (!string.IsNullOrEmpty(_settings.DDNSDomain) && NetworkManager.IsDomainName(_settings.DDNSDomain))
            {
                await RealmListManager.UpdateRealmListAddress(int.Parse(TXTRealmID!.Text, CultureInfo.InvariantCulture), _settings.DDNSDomain, _settings);
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
            if (_settings.SelectedCore == Cores.AscEmu)
            {
                var realmLists = await RealmListManager.GetRealmLists<Realmlist.AscemuBased>(_settings);
                var SearchList = realmLists.Find(obj =>
                obj.ID.ToString(CultureInfo.InvariantCulture) == CBOXReamList.SelectedItem!.ToString());
                TXTRealmID.Text = SearchList!.ID.ToString(CultureInfo.InvariantCulture);
                TXTRealmName.Text = "N/A";
                TXTRealmLocalAddress.Text = "N/A";
                TXTRealmSubnetMask.Text = "N/A";
                TXTRealmPort.Text = "N/A";
                TXTRealmGameBuild.Text = "N/A";
                TXTRealmAddress.Text = SearchList.Password;
                TXTRealmAddress.Hint = _translator.Translate("TXTBoxCreateUserPassword");
                TXTRealmAddress.Enabled = true;
                TXTRealmName.Enabled = false;
                TXTRealmLocalAddress.Enabled = false;
                TXTRealmSubnetMask.Enabled = false;
                TXTRealmPort.Enabled = false;
                TXTRealmGameBuild.Enabled = false;
            }
            if (_settings.SelectedCore == Cores.TrinityCore ||
                _settings.SelectedCore == Cores.TrinityCore335 ||
                _settings.SelectedCore == Cores.TrinityCoreClassic ||
                _settings.SelectedCore == Cores.AzerothCore ||
                _settings.SelectedCore == Cores.CypherCore)
            {
                var realmLists = await RealmListManager.GetRealmLists<Realmlist.TrinityBased>(_settings);
                var SearchList = realmLists.Find(obj =>
                obj.Name.ToString(CultureInfo.InvariantCulture) == CBOXReamList.SelectedItem!.ToString());
                TXTRealmID.Text = SearchList!.ID.ToString(CultureInfo.InvariantCulture);
                TXTRealmName.Text = SearchList!.Name;
                TXTRealmLocalAddress.Text = SearchList!.LocalAddress;
                TXTRealmSubnetMask.Text = SearchList!.LocalSubnetMask;
                TXTRealmPort.Text = SearchList!.Port.ToString(CultureInfo.InvariantCulture);
                TXTRealmGameBuild.Text = SearchList!.GameBuild.ToString(CultureInfo.InvariantCulture);
                TXTRealmAddress.Text = SearchList!.Address;
                TXTRealmAddress.Hint = _translator.Translate("TXTRealmAddress");
                TXTRealmName.Enabled = true;
                TXTRealmLocalAddress.Enabled = true;
                TXTRealmSubnetMask.Enabled = true;
                TXTRealmPort.Enabled = true;
                TXTRealmGameBuild.Enabled = true;
                TXTRealmAddress.Enabled = true;
            }
            if (_settings.SelectedCore == Cores.CMaNGOS ||
                _settings.SelectedCore == Cores.VMaNGOS)
            {
                var realmLists = await RealmListManager.GetRealmLists<Realmlist.MangosBased>(_settings);
                var SearchList = realmLists.Find(obj =>
                obj.Name.ToString(CultureInfo.InvariantCulture) == CBOXReamList.SelectedItem!.ToString());
                TXTRealmName.Text = SearchList!.Name;
                TXTRealmLocalAddress.Text = "N/A";
                TXTRealmSubnetMask.Text = "N/A";
                TXTRealmPort.Text = SearchList!.Port.ToString(CultureInfo.InvariantCulture);
                TXTRealmGameBuild.Text = SearchList!.GameBuild.ToString(CultureInfo.InvariantCulture);
                TXTRealmAddress.Text = SearchList!.Address;
                TXTRealmAddress.Hint = _translator.Translate("TXTRealmAddress");
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

            if (_settings.SelectedCore == Cores.TrinityCore ||
                _settings.SelectedCore == Cores.TrinityCore335 ||
                _settings.SelectedCore == Cores.TrinityCoreClassic ||
                _settings.SelectedCore == Cores.AzerothCore ||
                _settings.SelectedCore == Cores.CypherCore)
            {
                await AccessManager.SaveData(SqlQueryManager.UpdateRealmList(_settings.SelectedCore), new
                {
                    Name = TXTRealmName.Text,
                    Address = TXTRealmAddress.Text,
                    LocalAddress = TXTRealmLocalAddress.Text,
                    LocalSubnetMask = TXTRealmSubnetMask.Text,
                    Port = TXTRealmPort.Text,
                    GameBuild = TXTRealmGameBuild.Text
                }, AccessManager.ConnectionString(_settings, _settings.AuthDatabase));
            }
            if (_settings.SelectedCore == Cores.CMaNGOS ||
                _settings.SelectedCore == Cores.VMaNGOS)
            {
                await AccessManager.SaveData(SqlQueryManager.UpdateRealmList(_settings.SelectedCore), new
                {
                    Name = TXTRealmName.Text,
                    Address = TXTRealmAddress.Text,
                    Port = TXTRealmPort.Text,
                    GameBuild = TXTRealmGameBuild.Text
                }, AccessManager.ConnectionString(_settings, _settings.AuthDatabase));
            }
            if (_settings.SelectedCore == Cores.AscEmu)
            {
                await AccessManager.SaveData(SqlQueryManager.UpdateRealmList(_settings.SelectedCore), new
                {
                    Password = TXTRealmAddress.Text,
                }, AccessManager.ConnectionString(_settings, _settings.AuthDatabase));
            }
        }
        private async void BTNEditRealmlistData_Click(object sender, EventArgs e)
        {
            if (_editRealmList == true)
            {
                await SaveRealmListData();
                BTNEditRealmlistData.Text = _translator.Translate("BTNEditRealmlistDataON");
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
                BTNEditRealmlistData.Text = _translator.Translate("BTNEditRealmlistDataOFF");
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
        #endregion
        #region"Dynamic DNS"
        private void BTNDDNSTimerStart_Click(object sender, EventArgs e)
        {
            TimerWacher.Enabled = true;
        }
        private void TImerDinamicDNS_Tick(object sender, EventArgs e)
        {
            if (_settings.IPAddress != TXTPublicIP.Text)
            {
                _settings.IPAddress = TXTPublicIP.Text;
                NetworkManager.UpdateDNSIP(_settings);
            }
        }
        private void BTNDDNSServiceWebiste_Click(object sender, EventArgs e)
        {
            Process.Start("explorer.exe", Links.DDNSWebsits(_settings.DDNSerivce));
        }
        #endregion
        #region "Settings Page"
        #region"CustomRepack"
        private void TGLUseCustomServer_CheckedChanged(object sender, EventArgs e)
        {
            _settings.LaunchCustomCore = TGLUseCustomServer.Checked;
        }
        private void TGLCustomNames_CheckedChanged(object sender, EventArgs e)
        {
            _settings.CustomNames = TGLCustomNames.Checked;
            TXTCustomWorldName.ReadOnly = !_settings.CustomNames;
            TXTCustomDatabaseName.ReadOnly = !_settings.CustomNames;
            TXTCustomAuthName.ReadOnly = !_settings.CustomNames;
        }
        #endregion
        #region"Trion"
        private async Task UpdateSppVersion()
        {
            AppUpdateManager.GetSPPVersionOffline(_settings);
            await AppUpdateManager.GetSPPVersionOnline(_settings);
            LBLTrionVersion.Text = string.Format(CultureInfo.InvariantCulture, _translator.Translate("LBLTrionVersion"), FormData.UI.Version.Local.Trion, FormData.UI.Version.Online.Trion);
            LBLDBVersion.Text = string.Format(CultureInfo.InvariantCulture, _translator.Translate("LBLDBVersion"), FormData.UI.Version.Local.Database, FormData.UI.Version.Online.Database);
            LBLClassicVersion.Text = string.Format(CultureInfo.InvariantCulture, _translator.Translate("LBLClassicVersion"), FormData.UI.Version.Local.Classic, FormData.UI.Version.Online.Classic);
            LBLTBCVersion.Text = string.Format(CultureInfo.InvariantCulture, _translator.Translate("LBLTBCVersion"), FormData.UI.Version.Local.TBC, FormData.UI.Version.Online.TBC);
            LBLWotLKVersion.Text = string.Format(CultureInfo.InvariantCulture, _translator.Translate("LBLWotLKVersion"), FormData.UI.Version.Local.WotLK, FormData.UI.Version.Online.WotLK);
            LBLCataVersion.Text = string.Format(CultureInfo.InvariantCulture, _translator.Translate("LBLCataVersion"), FormData.UI.Version.Local.Cata, FormData.UI.Version.Online.Cata);
            LBLMoPVersion.Text = string.Format(CultureInfo.InvariantCulture, _translator.Translate("LBLMoPVersion"), FormData.UI.Version.Local.Mop, FormData.UI.Version.Online.Mop);
        }
        private void BTNReviveSupporterKey_Click(object sender, EventArgs e)
        {
            TXTSupporterKey.PasswordChar = TXTSupporterKey.PasswordChar == '⛊' ? '\0' : '⛊';
        }
        private async Task StartAutoUpdate(AppSettings Settings)
        {
            if (FormData.UI.Version.Update.Trion && _settings.AutoUpdateTrion)
            {

            }
            if (FormData.UI.Version.Update.Database && _settings.AutoUpdateTrion)
            {

            }
            if (FormData.UI.Version.Update.Classic && _settings.AutoUpdateTrion)
            {

            }
            if (FormData.UI.Version.Update.TBC && _settings.AutoUpdateTrion)
            {

            }
            if (FormData.UI.Version.Update.WotLK && _settings.AutoUpdateTrion)
            {

            }
            if (FormData.UI.Version.Update.Cata && _settings.AutoUpdateTrion)
            {

            }
            if (FormData.UI.Version.Update.Mop && _settings.AutoUpdateTrion)
            {

            }
        }
        private void CBOXColorSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (CBOXColorSelect.SelectedItem)
            {
                case "TrionBlue":
                    _settings.TrionTheme = TrionTheme.TrionBlue;
                    LoadSkin();
                    break;
                case "Purple":
                    _settings.TrionTheme = TrionTheme.Purple;
                    LoadSkin();
                    break;
                case "Orange":
                    _settings.TrionTheme = TrionTheme.Orange;
                    LoadSkin();
                    break;
                case "Green":
                    _settings.TrionTheme = TrionTheme.Green;
                    LoadSkin();
                    break;
            }

        }
        private void CBOXLanguageSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            _settings.TrionLanguage = CBOXLanguageSelect.SelectedItem!.ToString() ?? "en";
            LoadLanguage();
            Refresh();
        }
        private void TGLServerCrashDetection_CheckedChanged(object sender, EventArgs e)
        {
            _settings.ServerCrashDetection = TGLServerCrashDetection.Checked;
        }
        private void TGLServerStartup_CheckedChanged(object sender, EventArgs e)
        {
            _settings.RunServerWithWindows = TGLServerStartup.Checked;
        }
        private void TGLRunTrionStartup_CheckedChanged(object sender, EventArgs e)
        {
            _settings.RunWithWindows = TGLRunTrionStartup.Checked;
        }
        private void TGLHideConsole_CheckedChanged(object sender, EventArgs e)
        {
            _settings.ConsolHide = TGLHideConsole.Checked;
        }
        private void TGLNotificationSound_CheckedChanged(object sender, EventArgs e)
        {
            _settings.NotificationSound = TGLNotificationSound.Checked;
        }
        private void TGLStayInTray_CheckedChanged(object sender, EventArgs e)
        {
            _settings.StayInTray = TGLStayInTray.Checked;
        }
        private void TGLAutoUpdateTrion_CheckedChanged(object sender, EventArgs e)
        {
            _settings.AutoUpdateTrion = TGLAutoUpdateTrion.Checked;
        }
        private void TGLAutoUpdateCore_CheckedChanged(object sender, EventArgs e)
        {
            _settings.AutoUpdateCore = TGLAutoUpdateCore.Checked;
        }
        private void TGLAutoUpdateDatabase_CheckedChanged(object sender, EventArgs e)
        {
            _settings.AutoUpdateDatabase = TGLAutoUpdateDatabase.Checked;
        }
        #endregion
        #endregion
        #region"Downloader"
        private async void InstallFinished()
        {
            if (Infos.Install.Classic)
            {
                string classic = Links.Install.Classic.Replace("/", @"\");
                _settings.ClassicInstalled = true;
                _settings.LaunchClassicCore = true;
                _settings.ClassicWorkingDirectory = classic;
                _settings.ClassicLogonExeLoc = FileManager.GetExecutableLocation(classic, "realmd");
                _settings.ClassicLogonExeName = "realmd";
                _settings.ClassicWorldExeLoc = FileManager.GetExecutableLocation(classic, "mangosd");
                _settings.ClassicWorldExeName = "mangosd";
                _settings.ClassicLogonName = "WoW Classic Logon";
                _settings.ClassicWorldName = "WoW Classic World";
            }
            if (Infos.Install.TBC)
            {
                string TBC = Links.Install.TBC.Replace("/", @"\");
                _settings.TBCInstalled = true;
                _settings.LaunchTBCCore = true;
                _settings.TBCWorkingDirectory = TBC;
                _settings.TBCLogonExeLoc = FileManager.GetExecutableLocation(TBC, "realmd");
                _settings.TBCLogonExeName = "realmd";
                _settings.TBCWorldExeLoc = FileManager.GetExecutableLocation(TBC, "mangosd");
                _settings.TBCWorldExeName = "mangosd";
                _settings.TBCLogonName = "The Burning Crusade Logon";
                _settings.TBCWorldName = "The Burning Crusade World";
            }
            if (Infos.Install.WotLK)
            {
                string WotLK = Links.Install.WotLK.Replace("/", @"\");
                _settings.WotLKInstalled = true;
                _settings.LaunchWotLKCore = true;
                _settings.WotLKWorkingDirectory = WotLK;
                _settings.WotLKLogonExeLoc = FileManager.GetExecutableLocation(WotLK, "authserver");
                _settings.WotLKLogonExeName = "authserver";
                _settings.WotLKWorldExeLoc = FileManager.GetExecutableLocation(WotLK, "worldserver");
                _settings.WotLKLogonName = "Wrath of the Lich King Logon";
                _settings.WotLKWorldName = "Wrath of the Lich King World";
            }
            if (Infos.Install.Cata)
            {
                string cata = Links.Install.Cata.Replace("/", @"\");
                _settings.CataInstalled = true;
                _settings.LaunchCataCore = true;
                _settings.CataWorkingDirectory = cata;
                _settings.CataLogonExeLoc = FileManager.GetExecutableLocation(cata, "authserver");
                _settings.CataLogonExeName = "authserver";
                _settings.CataWorldExeLoc = FileManager.GetExecutableLocation(cata, "worldserver");
                _settings.CataWorldExeName = "worldserver";
                _settings.CataLogonName = "Cataclysm Logon";
                _settings.CataWorldName = "Cataclysm World";
            }
            if (Infos.Install.Mop)
            {
                string Mop = Links.Install.Mop.Replace("/", @"\");
                _settings.MOPInstalled = true;
                _settings.LaunchMoPCore = true;
                _settings.MopWorkingDirectory = Mop;
                _settings.MopLogonExeLoc = FileManager.GetExecutableLocation(Mop, "authserver");
                _settings.MopLogonExeName = "authserver";
                _settings.MopWorldExeLoc = FileManager.GetExecutableLocation(Mop, "authserver");
                _settings.MopWorldExeName = "worldserver";
                _settings.MoPLogonName = "Mists of Pandaria Logon";
                _settings.MoPWorldName = "Mists of Pandaria World";
            }
            if (Infos.Install.Database == true)
            {
                string Database = Links.Install.Database.Replace("/", @"\");
                _settings.DBInstalled = true;
                _settings.DBLocation = $@"{Database}";
                _settings.DBWorkingDir = $@"{Database}\bin";
                _settings.DBExeLoc = FileManager.GetExecutableLocation($@"{Database}\bin", "mysqld");
                _settings.DBExeName = "mysqld";
                Settings.CreateMySQLConfigFile(Directory.GetCurrentDirectory(), Database);

                string SQLLocation = $@"{Database}\extra\initDatabase.sql";
                await AppExecuteMenager.ApplicationStart(_settings.DBExeLoc, _settings.DBWorkingDir, "initialize MySQL", true, $"--initialize-insecure --init-file=\"{SQLLocation}\" --console");
            }
            if (Infos.Install.Trion == true)
            {
                Process.Start("updater.exe");
                Environment.Exit(0);
            }
            
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
            _settings.DDNSDomain = TXTDDNSDomain.Text;
            _settings.DDNSPassword = TXTDDNSPassword.Text;
            _settings.DDNSInterval = int.Parse(TXTDDNSInterval.Text, CultureInfo.InvariantCulture);
            _settings.DDNSUsername = TXTDDNSUsername.Text;
            _settings.SupporterKey = TXTSupporterKey.Text;
            _settings.AuthDatabase = TXTAuthDatabase.Text;
            _settings.CharactersDatabase = TXTCharDatabase.Text;
            _settings.WorldDatabase = TXTWorldDatabase.Text;
            _settings.DBServerHost = TXTDatabaseHost.Text;
            _settings.DBServerPassword = TXTDatabasePassword.Text;
            _settings.DBServerPort = TXTDatabasePort.Text;
            _settings.DBServerUser = TXTDatabaseUser.Text;
            _settings.CustomWorldExeName = TXTCustomWorldName.Text;
            _settings.CustomLogonExeName = TXTCustomAuthName.Text;
            _settings.DBExeName = TXTCustomDatabaseName.Text;
        }
        private void TimerNotification_Tick(object sender, EventArgs e)
        {
            if (NotificationData.Message != null) {
                var Time = DateTime.Now;
                // Add a new row to the DataTable
                DGVNotifications.Rows.Add(DGVNotifications.Rows.Count + 1.ToString(), NotificationData.Message.ToString(), Time.ToString("ddd, dd MMM yyyy h: mm"));
                NotificationData.Message = null!;
                // Refresh the DataGridView to reflect the changes
                DGVNotifications.Refresh();

            }
        }
    }
}