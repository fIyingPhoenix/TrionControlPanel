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
using TrionControlPanel.Desktop;

namespace TrionControlPanelDesktop
{
    public partial class MainForm : MaterialForm
    {
        #region "Load Language"
        private Translator _translator = new();
        private void PopulateComboBoxes()
        {
            CBOXSPPVersion.Items.Clear();
            CBOXSPPVersion.Items.Add(_translator.Translate("SPPver1"));
            CBOXSPPVersion.Items.Add(_translator.Translate("SPPver2"));
            CBOXSPPVersion.Items.Add(_translator.Translate("SPPver3"));
            CBOXSPPVersion.Items.Add(_translator.Translate("SPPver4"));
            CBOXSPPVersion.Items.Add(_translator.Translate("SPPver5"));
            CBOXSPPVersion.SelectedIndex = (int)_settings.SelectedSPP;
            CBOXAccountExpansion.Items.Clear();
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
            CBOXAccountSecurityAccess.Items.Clear();
            CBOXAccountSecurityAccess.Items.Add(_translator.Translate("AccountAccessLvL0"));
            CBOXAccountSecurityAccess.Items.Add(_translator.Translate("AccountAccessLvL1"));
            CBOXAccountSecurityAccess.Items.Add(_translator.Translate("AccountAccessLvL2"));
            CBOXAccountSecurityAccess.Items.Add(_translator.Translate("AccountAccessLvL3"));
            CBOXAccountSecurityAccess.Items.Add(_translator.Translate("AccountAccessLvL4"));
            CBOXAccountSecurityAccess.SelectedIndex = 0;
            CBOXTrionIcon.Items.Clear();
            foreach (var key in ImageListIcons.Images.Keys)
            {
                CBOXTrionIcon.Items.Add(key!);
            }
            CBOXTrionIcon.SelectedItem = _settings.TrionIcon;
            CBoxSelectItems();
        }
        private void LoadLangauge()
        {
            _translator.LoadLanguage(_settings.TrionLanguage);
            SetLanguage();
        }
        private void GetAllLanguages()
        {
            CBOXLanguageSelect.Items.AddRange([.. Translator.GetAvailableLanguages()]);
            CBOXLanguageSelect.SelectedItem = _settings.TrionLanguage;
            CBOXColorSelect.Items.AddRange(System.Enum.GetValues(typeof(Enums.TrionTheme)).Cast<Enums.TrionTheme>().Select(e => e.ToString()).ToArray());
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
            CBOXTrionIcon.Hint = _translator.Translate("CBOXTrionIconHint");
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
        private void LoadSettingsFromFile()
        {
            if (!File.Exists("Settings.json")) { Settings.CreatSettings("Settings.json"); }
            _settings = Settings.LoadSettings("Settings.json");
        }
        private void LoadSettingsUI()
        {

            CBoxSelectItems();
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
            TGLClassicLaunch.Checked = _settings.LaunchClassicCore;
            TGLTBCLaunch.Checked = _settings.LaunchTBCCore;
            TGLWotLKLaunch.Checked = _settings.LaunchWotLKCore;
            TGLCataLaunch.Checked = _settings.LaunchCataCore;
            TGLMoPLaunch.Checked = _settings.LaunchMoPCore;
            TGLServerStartup.Checked = _settings.RunServerWithWindows;
            TGLAutoUpdateDatabase.Checked = _settings.AutoUpdateDatabase;
            //CheckBoxes
            ChecCLASSICInstalled.Checked = _settings.ClassicInstalled;
            ChecTBCInstalled.Checked = _settings.TBCInstalled;
            ChecWOTLKInstalled.Checked = _settings.WotLKInstalled;
            ChecCATAInstalled.Checked = _settings.CataInstalled;
            ChecMOPInstalled.Checked = _settings.MOPInstalled;
            //DDNS
            TXTDDNSDomain.Text = _settings.DDNSDomain;
            TXTDDNSUsername.Text = _settings.DDNSUsername;
            TXTDDNSPassword.Text = _settings.DDNSPassword;
            TXTDDNSInterval.Text = _settings.DDNSInterval.ToString(CultureInfo.InvariantCulture);
            TGLDDNSRunOnStartup.Checked = _settings.DDNSRunOnStartup;
            TimerDinamicDNS.Enabled = _settings.DDNSRunOnStartup;
            TimerDinamicDNS.Interval = _settings.DDNSInterval;
            //SupporterKet
            TXTSupporterKey.Text = _settings.SupporterKey;
            //Load Trion Icon
            ChangeFormIcon(_settings.TrionIcon);
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
        private AppSettings _settings;
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
        private async void SetupDatabaseIfMissing()
        {
            if (!_settings.DBInstalled && !FormData.UI.Form.InstallingEmulator)
            {
                refreshDownloader();
                LBLFilesToBeRemoved.Text = _translator.Translate("LBLInitDatabaseWaiting");
                LBLInstallEmulatorTitle.Text = _translator.Translate("LBLInstallDatabaseTitle");
                Update();

                _cancellationTokenSource = new CancellationTokenSource();
                _cancellationToken = _cancellationTokenSource.Token;

                FormData.UI.Form.InstallingEmulator = true;

                // Create progress reporting for UI updates
                var serverFilesProgress = new Progress<string>(message => LBLServerFiles.Text = string.Format(CultureInfo.InvariantCulture, _translator.Translate("LBLServerFiles"), message));
                var localFilesProgress = new Progress<string>(message => LBLLocalFiles.Text = string.Format(CultureInfo.InvariantCulture, _translator.Translate("LBLLocalFiles"), message));
                var filesToBeDeletedProgress = new Progress<string>();
                var filesToBeDownloadedProgress = new Progress<string>(message => LBLFilesToBeDownloaded.Text = string.Format(CultureInfo.InvariantCulture, _translator.Translate("LBLFilesToBeDownloaded"), message));
                var downloadSpeedProgress = new Progress<double>(message => LBLDownloadSpeed.Text = string.Format(CultureInfo.InvariantCulture, _translator.Translate("LBLDownloadSpeed"), $"{message:0.##} MB/s"));
                var downloadProgress = new Progress<double>(message => PBarCurrentDownlaod.Value = (int)message);

                MainFormTabControler.SelectedTab = TabDownloader;
                await Task.Delay(1000);
                FormData.Infos.Install.Database = true;
                // Run file and server tasks concurrently on background threads
                var serverFilesDatabaseTask = Task.Run(() => NetworkManager.GetServerFiles(Links.APIRequests.GetServerFiles("database", _settings.SupporterKey), serverFilesProgress));
                var localFilesDatabaseTask = Task.Run(() => FileManager.ProcessFilesAsync(Links.Install.Database, localFilesProgress));
                // Wait for both tasks to complete before continuing
                await Task.WhenAll(serverFilesDatabaseTask, localFilesDatabaseTask);
                // Now both tasks are complete, retrieve their results
                var ServerFilesDatabase = await serverFilesDatabaseTask;
                var LocalFilesDatabase = await localFilesDatabaseTask;
                // Compare files and get missing ones
                var (missingFilesDatabase, filesToDeleteDatabase) = await FileManager.CompareFiles(ServerFilesDatabase, LocalFilesDatabase, "/database", filesToBeDeletedProgress, filesToBeDownloadedProgress);
                PBARTotalDownload.Maximum = missingFilesDatabase.Count;
                // **Download missing files one-by-one**
                foreach (var file in missingFilesDatabase)
                {
                    LBLFileName.Text = string.Format(CultureInfo.InvariantCulture, _translator.Translate("LBLFileName"), $"{file.Name}");
                    LBLFileSize.Text = string.Format(CultureInfo.InvariantCulture, _translator.Translate("LBLFileSize"), $"{file.Size} MB");

                    await FileManager.DownloadFileAsync(file, "/database", _cancellationToken, downloadProgress, null, downloadSpeedProgress);
                    PBARTotalDownload.Value++;
                }

                InstallFinished();

                FormData.UI.Form.InstallingEmulator = false;
                FormData.Infos.Install.Database = false;
            }
            else
            {
                BTNStartDatabase_Click(null!, null!);
            }
        }

        private async void MainForm_LoadAsync(object sender, EventArgs e)
        {
            LoadSettingsUI();
            GetAllLanguages();
            LoadSkin();
            LoadLangauge();
            PbarRAMMachineResources.Maximum = PerformanceMonitor.GetTotalRamInMB();

            await NetworkManager.GetAPIServer();
            await UpdateSppVersion();
            SetupDatabaseIfMissing();
            loadingForm.Close();
            Opacity = 1;
            ShowInTaskbar = true;
            WindowState = FormWindowState.Normal;
            Refresh();
        }
        private async void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            await AppExecuteMenager.StopDatabase();
            await AppExecuteMenager.StopLogon();
            await AppExecuteMenager.StopWorld();
            await Settings.SaveSettings(_settings, "Settings.json");
        }
        private void TimerPanelAnimation_Tick(object sender, EventArgs e)
        {
            if (FormData.UI.Version.Update.Trion)
            {
                PNLUpdateTrion.BorderColor = PNLUpdateTrion.BorderColor == Color.LimeGreen ? Color.Black : Color.LimeGreen;
                PNLUpdateTrion.Refresh();
                BTNDownloadUpdates.Text = _translator.Translate("BTNDownloadUpdatesOn");
            }
            else
            {
                BTNDownloadUpdates.Text = _translator.Translate("BTNDownloadUpdatesOff");
                PNLUpdateTrion.BorderColor = Color.Black;
                PNLUpdateTrion.Refresh();
            }
            if (FormData.UI.Version.Update.Database)
            {
                PNLUpdateDatabase.BorderColor = PNLUpdateDatabase.BorderColor == Color.LimeGreen ? Color.Black : Color.LimeGreen;
                PNLUpdateDatabase.Refresh();
                BTNDownloadUpdates.Text = _translator.Translate("BTNDownloadUpdatesOn");
            }
            else
            {
                PNLUpdateDatabase.BorderColor = Color.Black;
                PNLUpdateDatabase.Refresh();
                BTNDownloadUpdates.Text = _translator.Translate("BTNDownloadUpdatesOff");
            }
            if (FormData.UI.Version.Update.Classic)
            {
                PNLUpdateClassicSPP.BorderColor = PNLUpdateClassicSPP.BorderColor == Color.LimeGreen ? Color.Black : Color.LimeGreen;
                PNLUpdateClassicSPP.Refresh();
                BTNDownloadUpdates.Text = _translator.Translate("BTNDownloadUpdatesOn");
            }
            else
            {
                PNLUpdateClassicSPP.BorderColor = Color.Black;
                PNLUpdateClassicSPP.Refresh();
                BTNDownloadUpdates.Text = _translator.Translate("BTNDownloadUpdatesOff");
            }
            if (FormData.UI.Version.Update.TBC)
            {
                PNLUpdateTbcSPP.BorderColor = PNLUpdateTbcSPP.BorderColor == Color.LimeGreen ? Color.Black : Color.LimeGreen;
                PNLUpdateTbcSPP.Refresh();
                BTNDownloadUpdates.Text = _translator.Translate("BTNDownloadUpdatesOn");
            }
            else
            {
                PNLUpdateTbcSPP.BorderColor = Color.Black;
                PNLUpdateTbcSPP.Refresh();
                BTNDownloadUpdates.Text = _translator.Translate("BTNDownloadUpdatesOff");
            }
            if (FormData.UI.Version.Update.WotLK)
            {
                PNLUpdateWotlkSpp.BorderColor = PNLUpdateWotlkSpp.BorderColor == Color.LimeGreen ? Color.Black : Color.LimeGreen;
                PNLUpdateWotlkSpp.Refresh();
                BTNDownloadUpdates.Text = _translator.Translate("BTNDownloadUpdatesOn");
            }
            else
            {
                PNLUpdateWotlkSpp.BorderColor = Color.Black;
                PNLUpdateWotlkSpp.Refresh();
                BTNDownloadUpdates.Text = _translator.Translate("BTNDownloadUpdatesOff");
            }
            if (FormData.UI.Version.Update.Cata)
            {
                PNLUpdateWotlkSpp.BorderColor = PNLUpdateCataSPP.BorderColor == Color.LimeGreen ? Color.Black : Color.LimeGreen;
                PNLUpdateCataSPP.Refresh();
                BTNDownloadUpdates.Text = _translator.Translate("BTNDownloadUpdatesOn");
            }
            else
            {
                PNLUpdateCataSPP.BorderColor = Color.Black;
                PNLUpdateCataSPP.Refresh();
                BTNDownloadUpdates.Text = _translator.Translate("BTNDownloadUpdatesOff");
            }
            if (FormData.UI.Version.Update.Mop)
            {
                PNLUpdateMopSPP.BorderColor = PNLUpdateMopSPP.BorderColor == Color.LimeGreen ? Color.Black : Color.LimeGreen;
                PNLUpdateMopSPP.Refresh();
                BTNDownloadUpdates.Text = _translator.Translate("BTNDownloadUpdatesOn");
            }
            else
            {
                PNLUpdateMopSPP.BorderColor = Color.Black;
                PNLUpdateMopSPP.Refresh();
                BTNDownloadUpdates.Text = _translator.Translate("BTNDownloadUpdatesOff");
            }
        }
        private async void TimerWacher_Tick(object sender, EventArgs e)
        {
            ToogleButtons();
            ResurceUsage();
            ProcessesIDUpdate();
            RunServerUpdate();
            await ServerMonitor.ServerRunningLogonAsync();
            await ServerMonitor.ServerRunningWorldAsync();
            await ServerMonitor.ServerRunningDatabaseAsync();
        }
        private async void TimerUpdate_Tick(object sender, EventArgs e)
        {
            await UpdateSppVersion();
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
            if (!ServerMonitor.ServerStartedLogon() && !ServerMonitor.ServerRunningLogon())
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
            if (!ServerMonitor.ServerStartedWorld() && !ServerMonitor.ServerRunningWorld())
            {
                await AppExecuteMenager.StartWorld(_settings);
            }
            else
            {
                await AppExecuteMenager.StopWorld();
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
                    _translator.Translate("DatabaseNotRunningErrorMbox"),
                    _translator.Translate("MessageBoxTitleInfo"),
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
            LBLDatabaseProcessID.Text = $"{_translator.Translate("LBLProcessID")}: {DatabaseProcessIDs}";
            //
            var WorldProcessIDs = SystemData.GetTotalWorldProcessIDCount() > 0 ? string.Join(", ", SystemData.GetWorldProcessesID().Select(p => p.ID)) : "0";
            LBLWorldProcessID.Text = $"{_translator.Translate("LBLProcessID")}: {WorldProcessIDs}";
            //
            var LogonProcessIDs = SystemData.GetTotalLogonProcessIDCount() > 0 ? string.Join(", ", SystemData.GetLogonProcessesID().Select(p => p.ID)) : "0";
            LBLLogonProcessID.Text = $"{_translator.Translate("LBLProcessID")}: {LogonProcessIDs}";

        }
        private void RunServerUpdate()
        {
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

            try
            {
                switch (_settings.SelectedSPP)
                {
                    case SPP.Classic:
                        await InstallExpansionAsync("Classic", "/classic", _settings.ClassicInstalled, v => FormData.Infos.Install.Classic = v, Links.Install.Classic);
                        break;

                    case SPP.TheBurningCrusade:
                        await InstallExpansionAsync("TBC", "/tbc", _settings.TBCInstalled, v => FormData.Infos.Install.TBC = v, Links.Install.TBC);
                        break;

                    case SPP.WrathOfTheLichKing:
                        await InstallExpansionAsync("WotLK", "/wotlk", _settings.WotLKInstalled, v => FormData.Infos.Install.WotLK = v, Links.Install.WotLK);
                        break;

                    case SPP.Cataclysm:
                        await InstallExpansionAsync("Cata", "/cata", _settings.CataInstalled, v => FormData.Infos.Install.Cata = v, Links.Install.Cata);
                        break;

                    case SPP.MistOfPandaria:
                        await InstallExpansionAsync("MoP", "/mop", _settings.MOPInstalled, v => FormData.Infos.Install.Mop = v, Links.Install.Mop);
                        break;

                    default:
                        AlertBox.Show(_translator.Translate("AlerBoxFaildGettingEmulatro"), NotificationType.Info, _settings);
                        break;
                }

                AlertBox.Show(_translator.Translate("SPPInstalationSucces"), NotificationType.Info, _settings);
            }
            catch (Exception ex)
            {
                AlertBox.Show($"An error occurred: {ex.Message}", NotificationType.Error, _settings);
            }
        }
        /// <summary>
        /// Handles the installation process for different expansions.
        /// </summary>
        private async Task InstallExpansionAsync(string expansionName, string folderPath, bool isInstalled, Action<bool> setInstallStatus, string InstalationLocation)
        {
            DLCardRemoweFiles.Visible = true;
            Update();

            LBLInstallEmulatorTitle.Text = string.Format(CultureInfo.InvariantCulture, _translator.Translate("LBLInstallEmulatorTitle"), $"{expansionName}");
            refreshDownloader();
            if (isInstalled)
            {
                AlertBox.Show(string.Format(CultureInfo.InvariantCulture, _translator.Translate("AlerBoxEmulatroInstalled"), $"{expansionName} Emulator"), NotificationType.Info, _settings);
                FormData.UI.Form.InstallingEmulator = false;
                return;
            }

            MainFormTabControler.SelectedTab = TabDownloader;
            await Task.Delay(1000);

            setInstallStatus(true);

            // Create progress handlers to ensure UI updates happen on the UI thread
            var serverFilesProgress = new Progress<string>(message => LBLServerFiles.Text = string.Format(CultureInfo.InvariantCulture, _translator.Translate("LBLServerFiles"), message));
            var localFilesProgress = new Progress<string>(message =>LBLLocalFiles.Text = string.Format(CultureInfo.InvariantCulture, _translator.Translate("LBLLocalFiles"), message));
            var filesToBeDeletedProgress = new Progress<string>(message => LBLFilesToBeRemoved.Text = string.Format(CultureInfo.InvariantCulture, _translator.Translate("LBLFilesToBeRemoved"), message));
            var filesToBeDownloadedProgress = new Progress<string>(message => LBLFilesToBeDownloaded.Text = string.Format(CultureInfo.InvariantCulture, _translator.Translate("LBLFilesToBeDownloaded"), message));
            var downloadSpeedProgress = new Progress<double>(message => LBLDownloadSpeed.Text = string.Format(CultureInfo.InvariantCulture, _translator.Translate("LBLDownloadSpeed"), $"{message:0.##} MB/s"));
            var downloadProgress = new Progress<double>(message => PBarCurrentDownlaod.Value = (int)message);

            // Run file and server tasks concurrently
            var serverFilesTask = Task.Run(() => NetworkManager.GetServerFiles(Links.APIRequests.GetServerFiles(expansionName.ToLower(), _settings.SupporterKey), serverFilesProgress));
            var localFilesTask = Task.Run(() => FileManager.ProcessFilesAsync(InstalationLocation, localFilesProgress));

            await Task.WhenAll(serverFilesTask, localFilesTask);

            // Retrieve results
            var serverFiles = await serverFilesTask;
            var localFiles = await localFilesTask;

            // Compare files and get missing ones
            var (missingFiles, filesToDelete) = await FileManager.CompareFiles(serverFiles, localFiles, folderPath, filesToBeDeletedProgress, filesToBeDownloadedProgress);

            PBARTotalDownload.Maximum = missingFiles.Count;

            // **Download missing files one-by-one**
            foreach (var file in missingFiles)
            {
                LBLFileName.Text = string.Format(CultureInfo.InvariantCulture, _translator.Translate("LBLFileName"), $"{file.Name}");
                LBLFileSize.Text = string.Format(CultureInfo.InvariantCulture, _translator.Translate("LBLFileSize"), $"{file.Size} MB");

                await FileManager.DownloadFileAsync(file, folderPath, _cancellationToken, downloadProgress, null, downloadSpeedProgress);

                PBARTotalDownload.Value++;
            }

            await FileManager.DeleteFiles(filesToDelete);

            // Installation Finished!
            InstallFinished();
            FormData.UI.Form.InstallingEmulator = false;
            setInstallStatus(false);
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
                switch (_settings.SelectedSPP)
                {
                    case SPP.Classic:
                        await RepairExpansionAsync("Classic", "/classic", _settings.ClassicInstalled);
                        break;

                    case SPP.TheBurningCrusade:
                        await RepairExpansionAsync("TBC", "/tbc", _settings.TBCInstalled);
                        break;

                    case SPP.WrathOfTheLichKing:
                        await RepairExpansionAsync("WotLK", "/wotlk", _settings.WotLKInstalled);
                        break;

                    case SPP.Cataclysm:
                        await RepairExpansionAsync("Cata", "/cata", _settings.CataInstalled);
                        break;

                    case SPP.MistOfPandaria:
                        await RepairExpansionAsync("MoP", "/mop", _settings.MOPInstalled);
                        break;

                    default:
                        AlertBox.Show(_translator.Translate("AlerBoxFailedGettingEmulator"), NotificationType.Info, _settings);
                        break;
                }

                AlertBox.Show(_translator.Translate("SPPRepairSuccess"), NotificationType.Info, _settings);
            }
            catch (Exception ex)
            {
                AlertBox.Show($"An error occurred: {ex.Message}", NotificationType.Error, _settings);
            }
            finally
            {
                FormData.UI.Form.InstallingEmulator = false;
            }
        }
        /// <summary>
        /// Repairs an existing expansion by checking for missing or corrupt files and redownloading them.
        /// </summary>
        private async Task RepairExpansionAsync(string expansionName, string folderPath, bool isInstalled)
        {
            if (!isInstalled)
            {
                AlertBox.Show(string.Format(CultureInfo.InvariantCulture, _translator.Translate("AlerBoxEmulatroNotInstalled"), $"{expansionName} Emulator"), NotificationType.Info, _settings);
                FormData.UI.Form.InstallingEmulator = false;
                return;
            }
            DLCardRemoweFiles.Visible = true;
            LBLInstallEmulatorTitle.Text = string.Format(CultureInfo.InvariantCulture, _translator.Translate("LBLRepairEmulatorTitle"), $"{expansionName} Emulator");

            MainFormTabControler.SelectedTab = TabDownloader;
            await Task.Delay(1000);

            // Create progress handlers to ensure UI updates happen on the UI thread
            var serverFilesProgress = new Progress<string>(message => LBLServerFiles.Text = string.Format(CultureInfo.InvariantCulture, _translator.Translate("LBLServerFiles"), message));
            var localFilesProgress = new Progress<string>(message => LBLLocalFiles.Text = string.Format(CultureInfo.InvariantCulture, _translator.Translate("LBLLocalFiles"), message));
            var filesToBeDeletedProgress = new Progress<string>(message => LBLFilesToBeRemoved.Text = string.Format(CultureInfo.InvariantCulture, _translator.Translate("LBLFilesToBeRemoved"), message));
            var filesToBeDownloadedProgress = new Progress<string>(message => LBLFilesToBeDownloaded.Text = string.Format(CultureInfo.InvariantCulture, _translator.Translate("LBLFilesToBeDownloaded"), message));
            var downloadSpeedProgress = new Progress<double>(message => LBLDownloadSpeed.Text = string.Format(CultureInfo.InvariantCulture, _translator.Translate("LBLDownloadSpeed"), $"{message:0.##} MB/s"));
            var downloadProgress = new Progress<double>(message => PBarCurrentDownlaod.Value = (int)message);

            // Run file and server tasks concurrently
            var serverFilesTask = Task.Run(() => NetworkManager.GetServerFiles(Links.APIRequests.GetServerFiles(expansionName.ToLower(), _settings.SupporterKey), serverFilesProgress));
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
                LBLFileName.Text = string.Format(CultureInfo.InvariantCulture, _translator.Translate("LBLFileName"), $"{file.Name}");
                LBLFileSize.Text = string.Format(CultureInfo.InvariantCulture, _translator.Translate("LBLFileSize"), $"{file.Size} MB");

                //await FileManager.DownloadFileAsync(file, folderPath, _cancellationToken, downloadProgress, null, downloadSpeedProgress);

                PBARTotalDownload.Value++;
            }

            AlertBox.Show($"{expansionName} Repair Completed!", NotificationType.Success, _settings);
        }
        // Uninstalls the selected SPP based on the provided app settings.
        private async void BTNUninstallSPP_Click(object sender, EventArgs e)
        {
            AlertBox.Show(_translator.Translate("SPPUninstall"), NotificationType.Info, _settings);
            switch (_settings.SelectedSPP)
            {
                case Enums.SPP.Classic:
                    await AppServiceManager.StartUninstall(_settings.ClassicWorkingDirectory);
                    _settings.ClassicInstalled = false;
                    _settings.LaunchClassicCore = false;
                    break;
                case Enums.SPP.TheBurningCrusade:
                    await AppServiceManager.StartUninstall(_settings.TBCWorkingDirectory);
                    _settings.TBCInstalled = false;
                    _settings.LaunchTBCCore = false;
                    break;
                case Enums.SPP.WrathOfTheLichKing:
                    await AppServiceManager.StartUninstall(_settings.WotLKWorkingDirectory);
                    _settings.WotLKInstalled = false;
                    _settings.LaunchWotLKCore = false;
                    break;
                case Enums.SPP.Cataclysm:
                    await AppServiceManager.StartUninstall(_settings.CataWorkingDirectory);
                    _settings.CataInstalled = false;
                    _settings.LaunchCataCore = false;
                    break;
                case Enums.SPP.MistOfPandaria:
                    await AppServiceManager.StartUninstall(_settings.MopWorkingDirectory);
                    _settings.MOPInstalled = false;
                    _settings.LaunchMoPCore = false;
                    break;
            }

        }
        private void CBOXSPPVersion_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CBOXSPPVersion.SelectedItem!.ToString() == _translator.Translate("SPPver1"))
            {
                _settings.SelectedSPP = SPP.Classic;
            }
            if (CBOXSPPVersion.SelectedItem.ToString() == _translator.Translate("SPPver2"))
            {
                _settings.SelectedSPP = SPP.TheBurningCrusade;
            }
            if (CBOXSPPVersion.SelectedItem.ToString() == _translator.Translate("SPPver3"))
            {
                _settings.SelectedSPP = SPP.WrathOfTheLichKing;
            }
            if (CBOXSPPVersion.SelectedItem.ToString() == _translator.Translate("SPPver4"))
            {
                _settings.SelectedSPP = SPP.Cataclysm;
            }
            if (CBOXSPPVersion.SelectedItem.ToString() == _translator.Translate("SPPver5"))
            {
                _settings.SelectedSPP = SPP.MistOfPandaria;
            }
        }
        private async void BTNWorldClassicStart_Click(object sender, EventArgs e)
        {
            await AppExecuteMenager.StartWorldSeparate(
                _settings.ClassicWorldExeLoc,
                _settings.ClassicWorkingDirectory,
                _settings.ClassicWorldExeName,
                _settings.ConsolHide
                );
        }
        private async void BTNLogonClassicStart_Click(object sender, EventArgs e)
        {
            await AppExecuteMenager.StartLogonSeparate(
                _settings.ClassicLogonExeLoc,
                _settings.ClassicWorkingDirectory,
                _settings.ClassicLogonExeName,
                _settings.ConsolHide
                );
        }
        private async void BTNWorldTBCStart_Click(object sender, EventArgs e)
        {
            await AppExecuteMenager.StartWorldSeparate(
                _settings.TBCWorldExeLoc,
                _settings.TBCWorkingDirectory,
                _settings.TBCWorldExeName,
                _settings.ConsolHide
                );
        }
        private async void BTNLogonTBCStart_Click(object sender, EventArgs e)
        {
            await AppExecuteMenager.StartLogonSeparate(
                _settings.TBCLogonExeLoc,
                _settings.TBCWorkingDirectory,
                _settings.TBCLogonExeName,
                _settings.ConsolHide
                );
        }
        private async void BTNWorldWotLKStart_Click(object sender, EventArgs e)
        {
            await AppExecuteMenager.StartWorldSeparate(
                _settings.WotLKWorldExeLoc,
                _settings.WotLKWorkingDirectory,
                _settings.WotLKWorldExeName,
                _settings.ConsolHide
                );
        }
        private async void BTNLogonWotLKStart_Click(object sender, EventArgs e)
        {
            await AppExecuteMenager.StartLogonSeparate(
                _settings.WotLKLogonExeLoc,
                _settings.WotLKWorkingDirectory,
                _settings.WotLKLogonExeName,
                _settings.ConsolHide
                );
        }
        private async void BTNWorldCataStart_Click(object sender, EventArgs e)
        {
            await AppExecuteMenager.StartWorldSeparate(
                _settings.CataWorldExeLoc,
                _settings.CataWorkingDirectory,
                _settings.CataWorldExeName,
                _settings.ConsolHide
                );
        }
        private async void BTNLogonCataStart_Click(object sender, EventArgs e)
        {
            await AppExecuteMenager.StartLogonSeparate(
                _settings.CataLogonExeLoc,
                _settings.CataWorkingDirectory,
                _settings.CataLogonExeName,
                _settings.ConsolHide
                );
        }
        private async void BTNWorldMoPStart_Click(object sender, EventArgs e)
        {
            await AppExecuteMenager.StartWorldSeparate(
                _settings.MopWorldExeLoc,
                _settings.MopWorkingDirectory,
                _settings.MopWorldExeName,
                _settings.ConsolHide
                );
        }
        private async void BTNLogonMoPStart_Click(object sender, EventArgs e)
        {
            await AppExecuteMenager.StartLogonSeparate(
                _settings.MopLogonExeLoc,
                _settings.MopWorkingDirectory,
                _settings.MopLogonExeName,
                _settings.ConsolHide
                );
        }
        private void TGLClassicLaunch_CheckedChanged(object sender, EventArgs e)
        {
            _settings.LaunchClassicCore = TGLClassicLaunch.Checked;
        }
        private void TGLTBCLaunch_CheckedChanged(object sender, EventArgs e)
        {
            _settings.LaunchTBCCore = TGLTBCLaunch.Checked;
        }

        private void TGLWotLKLaunch_CheckedChanged(object sender, EventArgs e)
        {
            _settings.LaunchWotLKCore = TGLWotLKLaunch.Checked;
        }

        private void TGLCataLaunch_CheckedChanged(object sender, EventArgs e)
        {
            _settings.LaunchCataCore = TGLCataLaunch.Checked;
        }

        private void TGLMoPLaunch_CheckedChanged(object sender, EventArgs e)
        {
            _settings.LaunchMoPCore = TGLMoPLaunch.Checked;
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
                if (_settings.SelectedCore == Cores.AscEmu)
                {
                    var RealmLists = await RealmListMenager.GetRealmLists<Realmlist.AscemuBased>(_settings);
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
                    var RealmLists = await RealmListMenager.GetRealmLists<Realmlist.TrinityBased>(_settings);
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
                    var RealmLists = await RealmListMenager.GetRealmLists<Realmlist.MangosBased>(_settings);
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
                await RealmListMenager.UpdateTealmListAddress(int.Parse(TXTRealmID!.Text, CultureInfo.InvariantCulture), TXTPublicIP.Text, _settings);
                return;
            }
            else
            {
                MaterialMessageBox.Show("");
            }
            if (!string.IsNullOrEmpty(_settings.DDNSDomain) && NetworkManager.IsDomainName(_settings.DDNSDomain))
            {
                await RealmListMenager.UpdateTealmListAddress(int.Parse(TXTRealmID!.Text, CultureInfo.InvariantCulture), _settings.DDNSDomain, _settings);
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
                var realmLists = await RealmListMenager.GetRealmLists<Realmlist.AscemuBased>(_settings);
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
                var realmLists = await RealmListMenager.GetRealmLists<Realmlist.TrinityBased>(_settings);
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
                var realmLists = await RealmListMenager.GetRealmLists<Realmlist.MangosBased>(_settings);
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
        private void refreshDownloader()
        {
            LBLServerFiles.Text = _translator.Translate("LBLServerFilesIDLE");
            LBLLocalFiles.Text = _translator.Translate("LBLLocalFilesIDLE");
            LBLFilesToBeDownloaded.Text = _translator.Translate("LBLFilesToBeDownloadedIDLE");
            LBLFilesToBeRemoved.Text = _translator.Translate("LBLFilesToBeRemovedIDLE");
            LBLFileName.Text = _translator.Translate("LBLFileNameIDLE");
            LBLFileSize.Text = _translator.Translate("LBLFileSizeIDLE");
            PBarCurrentDownlaod.Value = 0;
            PBARTotalDownload.Value = 0;
        }
        private void CBOXTrionIcon_SelectedIndexChanged(object sender, EventArgs e)
        {
            _settings.TrionIcon = CBOXTrionIcon.SelectedItem!.ToString()!;
            ChangeFormIcon(_settings.TrionIcon);
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
            LoadLangauge();
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
            if (FormData.Infos.Install.Classic)
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
            if (FormData.Infos.Install.TBC)
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
            if (FormData.Infos.Install.WotLK)
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
            if (FormData.Infos.Install.Cata)
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
            if (FormData.Infos.Install.Mop)
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
            if (FormData.Infos.Install.Database == true)
            {
                string Database = Links.Install.Database.Replace("/", @"\");
                _settings.DBInstalled = true;
                _settings.DBLocation = $@"{Database}";
                _settings.DBWorkingDir = $@"{Database}\bin";
                _settings.DBExeLoc = FileManager.GetExecutableLocation($@"{Database}\bin", "mysqld");
                _settings.DBExeName = "mysqld";
                Settings.CreateMySQLConfigFile(Directory.GetCurrentDirectory(), Database);
                string SQLLocation = $@"{Database}\extra\initDatabase.sql";
                await Task.Delay(1000);
                var initID = await AppExecuteMenager.ApplicationStart(_settings.DBExeLoc, _settings.DBWorkingDir, "initialize MySQL", true, $"--initialize-insecure --init-file=\"{SQLLocation}\" --console");
                INITSpinner.Visible = true;
                LBLFilesToBeRemoved.Text = _translator.Translate("LBLInitDatabaseInit");
                while (await Task.Run(() => ServerMonitor.IsApplicationRunning(initID)))
                {
                    INITSpinner.Visible = true;
                    LBLFilesToBeRemoved.Text = _translator.Translate("LBLInitDatabaseInit");
                }
            }
            if (FormData.Infos.Install.Trion == true)
            {
                Process.Start("update.exe");
                Environment.Exit(0);
            }

            if (MainFormTabControler.SelectedTab == TabDownloader)
                MainFormTabControler.SelectedTab = TabHome;
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

    }
}