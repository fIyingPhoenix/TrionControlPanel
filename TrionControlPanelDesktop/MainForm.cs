using MaterialSkin;
using MaterialSkin.Controls;
using System.Globalization;
using TrionControlPanel.Desktop.Extensions.Classes;
using TrionControlPanel.Desktop.Extensions.Classes.Data.Form;
using TrionControlPanel.Desktop.Extensions.Classes.Monitor;
using TrionControlPanel.Desktop.Extensions.Modules.Lists;
using TrionControlPanelDesktop.Extensions.Classes;
using TrionControlPanelDesktop.Extensions.Modules;
using Windows.ApplicationModel.Activation;
using static TrionControlPanelDesktop.Extensions.Modules.Enums;


namespace TrionControlPanelDesktop
{
    public partial class MainForm : MaterialForm
    {
        #region "Load Language"
        private Translator _translator = new();

        private void PopulateComboBoxes()
        {
            CBOXSPPVersion.Items.Add(_translator.Translate("SPPver1"));
            CBOXSPPVersion.Items.Add(_translator.Translate("SPPver2"));
            CBOXSPPVersion.Items.Add(_translator.Translate("SPPver3"));
            CBOXSPPVersion.Items.Add(_translator.Translate("SPPver4"));
            CBOXSPPVersion.Items.Add(_translator.Translate("SPPver5"));
            CBOXSPPVersion.SelectedIndex = 2;
            CBOXAccountExpansion.Items.Add(_translator.Translate("AccountExpansion0"));
            CBOXAccountExpansion.Items.Add(_translator.Translate("AccountExpansion1"));
            CBOXAccountExpansion.Items.Add(_translator.Translate("AccountExpansion2"));
            CBOXAccountExpansion.Items.Add(_translator.Translate("AccountExpansion3"));
            CBOXAccountExpansion.Items.Add(_translator.Translate("AccountExpansion4"));
            CBOXAccountExpansion.Items.Add(_translator.Translate("AccountExpansion5"));
            CBOXAccountExpansion.Items.Add(_translator.Translate("AccountExpansion6"));
            CBOXAccountExpansion.Items.Add(_translator.Translate("AccountExpansion7"));
            CBOXAccountExpansion.Items.Add(_translator.Translate("AccountExpansion8"));
            CBOXAccountExpansion.Items.Add(_translator.Translate("AccountExpansion9"));
            CBOXAccountExpansion.SelectedIndex = 2;
            CBOXAccountSecurityAccess.Items.Add(_translator.Translate("AccountAccessLvL0"));
            CBOXAccountSecurityAccess.Items.Add(_translator.Translate("AccountAccessLvL1"));
            CBOXAccountSecurityAccess.Items.Add(_translator.Translate("AccountAccessLvL2"));
            CBOXAccountSecurityAccess.Items.Add(_translator.Translate("AccountAccessLvL3"));
            CBOXAccountSecurityAccess.Items.Add(_translator.Translate("AccountAccessLvL4"));
            CBOXAccountSecurityAccess.SelectedIndex = 0;
            CBoxSelectItems();
            CBOXLanguageSelect.Items.AddRange([.. Translator.GetAvailableLanguages()]);
            CBOXLanguageSelect.SelectedItem = _settings.TrionLanguage;
            CBOXColorSelect.Items.AddRange(Enum.GetValues(typeof(Enums.TrionTheme)).Cast<Enums.TrionTheme>().Select(e => e.ToString()).ToArray());
            CBOXColorSelect.SelectedItem = _settings.TrionTheme.ToString();
        }
        private void LoadLangauge()
        {
            _translator.LoadLanguage(_settings.TrionLanguage);
            SetLanguage();
        }
        private void SetLanguage()
        {
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
            NIcon.BalloonTipText = _translator.Translate("NIconBalloonTipText");
            NIcon.BalloonTipTitle = _translator.Translate("NIconBalloonTipTitle");
            NIcon.Text = _translator.Translate("NIconBalloonTipTitle");
            TabSettings.Text = _translator.Translate("TabSettingsTitle");
            TabHome.Text = _translator.Translate("TabHomeTitle");
            TabDatabaseEditor.Text = _translator.Translate("TabDatabaseEditorTitle");
            TabSPP.Text = _translator.Translate("TabSPPTitle");
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
            #region "Trion"
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
            LBLTrionVersion.Text = string.Format(_translator.Translate("LBLTrionVersion"), "1", "1");
            LBLDBVersion.Text = string.Format(_translator.Translate("LBLDBVersion"), "1", "1");
            LBLClassicVersion.Text = string.Format(_translator.Translate("LBLClassicVersion"), "1", "1");
            LBLTBCVersion.Text = string.Format(_translator.Translate("LBLTBCVersion"), "1", "1");
            LBLWotLKVersion.Text = string.Format(_translator.Translate("LBLWotLKVersion"), "1", "1");
            LBLCataVersion.Text = string.Format(_translator.Translate("LBLCataVersion"), "1", "1");
            LBLMoPVersion.Text = string.Format(_translator.Translate("LBLMoPVersion"), "1", "1");
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
            TLTHome.SetToolTip(LBLCardSPPversionInfo, _translator.Translate("LBLCardSPPversionInfo"));
            TLTHome.SetToolTip(LBLCardElulatorsInfo, _translator.Translate("LBLCardElulatorsInfo"));
            TLTHome.SetToolTip(LBLCardSPPInstaledInfo, _translator.Translate("LBLCardSPPInstaledInfo"));
            #endregion
        }
        #endregion
        #region "Load / Save Settings"
        private void CBoxSelectItems()
        {
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
                    CBOXSelectedEmulators.SelectedItem = "CypherCore";
                    break;
                case Cores.TrinityCoreClassic:
                    CBOXSelectedEmulators.SelectedItem = "TrinityCore Classic";
                    break;
                case Cores.VMaNGOS:
                    CBOXSelectedEmulators.SelectedItem = "VMaNGOS";
                    break;
            }
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
            switch (_settings.DDNSerivce)
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
        private void LoadSettings()
        {
            if (!File.Exists("Settings.json")) { Settings.CreatSettings("Settings.json"); }
            _settings = Settings.LoadSettings("Settings.json");

            //Load Installed Emulators
            TGLClassicLaunch.Checked = _settings.LaunchClassicCore;
            TGLTBCLaunch.Checked = _settings.LaunchTBCCore;
            TGLWotLKLaunch.Checked = _settings.LaunchWotLKCore;
            TGLCataLaunch.Checked = _settings.LaunchCataCore;
            TGLMoPLaunch.Checked = _settings.LaunchMoPCore;
            TGLUseCustomServer.Checked = _settings.LaunchCustomCore;
            //Load Names
            TXTCustomAuthName.Text = _settings.CustomLogonExeName;
            TXTCustomWorldName.Text = _settings.CustomWorldExeName;
            TXTCustomDatabaseName.Text = _settings.DBExeName;
            //Working Directory
            TXTCustomRepackLocation.Text = _settings.CustomWorkingDirectory;
            TXTCustomDatabaseLocation.Text = _settings.DBLocation;
            //Database Host Data
            TXTDatabaseHost.Text = _settings.DBServerHost;
            TXTDatabasePort.Text = _settings.DBServerPort;
            TXTDatabaseUser.Text = _settings.DBServerUser;
            TXTDatabasePassword.Text = _settings.DBServerPassword;
            //Databases Tabels
            TXTCharDatabase.Text = _settings.CharactersDatabase;
            TXTAuthDatabase.Text = _settings.AuthDatabase;
            TXTWorldDatabase.Text = _settings.WorldDatabase;
            //ToggleButtons
            TGLAutoUpdateTrion.Checked = _settings.AutoUpdateTrion;
            TGLAutoUpdateCore.Checked = _settings.AutoUpdateCore;
            TGLHideConsole.Checked = _settings.ConsolHide;
            TGLNotificationSound.Checked = _settings.NotificationSound;
            TGLStayInTray.Checked = _settings.StayInTray;
            TGLCustomNames.Checked = _settings.CustomNames;
            TGLRunTrionStartup.Checked = _settings.RunWithWindows;
            TGLServerCrashDetection.Checked = _settings.ServerCrashDetection;
            //DDNS
            TXTDDNSDomain.Text = _settings.DDNSDomain;
            TXTDDNSUsername.Text = _settings.DDNSUsername;
            TXTDDNSPassword.Text = _settings.DDNSPassword;
            TXTDDNSInterval.Text = _settings.DDNSInterval.ToString();
            TGLDDNSRunOnStartup.Checked = _settings.DDNSRunOnStartup;

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
            switch (_settings.TrionTheme)
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
                    TLTHome.TitleColor = Settings.ConvertToColor(Primary.Green500);
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
        private AppSettings _settings;
        private MaterialSkinManager? materialSkinManager;
        private int _appPageSize { get; } = 1;
        private int _worldCurrentPage = 1;
        private int _logonCurrentPage = 1;
        #region"MainPage"
        //Loading...
        public MainForm()
        {
            InitializeComponent();
            LoadSettings();
            LoadSkin();
            //Initialize LoadLanguage 
            LoadLangauge();
            //Initialize MaterialSkinManager
            //Check if an update has ben Dowloaded and Delete it!
            if (File.Exists("setup.exe")) { File.Delete("setup.exe"); }

        }
        private void MainForm_LoadAsync(object sender, EventArgs e)
        {
            //Set max ram to progressbar
            PbarRAMMachineResources.Maximum = PerformanceMonitor.GetTotalRamInMB();
        }

        private void TimerWacher_Tick(object sender, EventArgs e)
        {
            ResurceUsage();
            RuntimeMonitoring();
        }
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
        private async void BTNStartLogon_Click(object sender, EventArgs e)
        {
            if(!ServerMonitor.ServerStartedLogon() &&  !ServerMonitor.ServerRunningLogon())
            { 
                await AppExecuteMenager.StartLogon(_settings);
            }
            else
            {
                await AppExecuteMenager.StopLogon();
            }
        }
        private async void BTNStartWorld_Click(object sender, EventArgs e)
        {
            if(!ServerMonitor.ServerStartedWorld() && !ServerMonitor.ServerRunningWorld())
            {
                await AppExecuteMenager.StartWorld(_settings);
            }
            else
            {
                await AppExecuteMenager.StopWorld();
            }
        }
        private void TabDatabaseEditor_Click(object sender, EventArgs e)
        {
            if(FormData.UI.Form.DBRunning)
            {
                //refresh data in database
            }
            else
            {
                MaterialMessageBox.Show(_translator.Translate("DatabaseNotRunningErrorMbox"), true, FlexibleMaterialForm.ButtonsPosition.Center, MessageBoxButtons.YesNo)
            }
        }
        #endregion
        #region "HomePage"
        private async void RuntimeMonitoring()
        {
            LBLDatabaseProcessID.Text = $"{_translator.Translate("LBLProcessID")}: {string.Join(", ", SystemData.GetDatabaseProcessID().Select(p => p.ID))}";
            LBLWorldProcessID.Text = $"{_translator.Translate("LBLProcessID")}: {string.Join(", ", SystemData.GetWorldProcessesID().Select(p => p.ID))}";
            LBLLogonProcessID.Text = $"{_translator.Translate("LBLProcessID")}: {string.Join(", ", SystemData.GetLogonProcessesID().Select(p => p.ID))}";
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
                var worldProcess = SystemData.GetWorldProcessesIDPage(_worldCurrentPage, _appPageSize);
                //loger.WriteLine($"Page {currentPage}:");
                foreach (var process in worldProcess)
                {
                    PbarRAMWordResources.Value = await Task.Run(() => PerformanceMonitor.ApplicationRamUsage(process.ID));
                    PbarCPUWordResources.Value = await Task.Run(() => PerformanceMonitor.ApplicationRamUsage(process.ID));
                }
            }
            if (SystemData.GetTotalLogonProcessIDCount() > 0)
            {
                //sameas up
                var logonProcess = SystemData.GetLogonProcessesIDPage(_logonCurrentPage, _appPageSize);
                //loget message here
                foreach (var process in logonProcess)
                {
                    PbarRAMLogonResources.Value = await Task.Run(() => PerformanceMonitor.ApplicationRamUsage(process.ID));
                    PbarCPULogonResources.Value = await Task.Run(() => PerformanceMonitor.ApplicationCpuUsage(process.ID));
                }
            }
        }
        #endregion
        #region "Settings Page"

        private void CBOXColorSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (CBOXColorSelect.SelectedItem) {
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

        #endregion
        void LoadData()
        {

        }
        //Closing running processes when trion die.

        private void StartWorldTSMItem_Click(object sender, EventArgs e)
        {
        }
        private void StartLogonTSMItem_Click(object sender, EventArgs e)
        {
        }
        private void StartDatabaseTSMItem_Click(object sender, EventArgs e)
        {
        }
        private void BTNStartMySQL_Click(object sender, EventArgs e)
        {
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
        private void TXTOnlyNumbers(object sender, KeyPressEventArgs e)
        {
            // Allow control characters like Backspace
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Ignore the input if it's not a digit
            }
        }

    }
}