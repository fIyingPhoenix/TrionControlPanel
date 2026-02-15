// =============================================================================
// MainForm.Language.cs
// Purpose: Handles UI localization and translation for all form controls
// Related UI: All labels, buttons, tooltips, and hints across the application
// Dependencies: Translator, LocalizationService, Settings
// Step 4 of IMPROVEMENTS.md - Extract Localization Logic
// =============================================================================

using System.Globalization;
using TrionControlPanel.Desktop.Extensions.Classes;

namespace TrionControlPanelDesktop
{
    /// <summary>
    /// Partial class containing language/localization related methods.
    /// Responsible for setting translated text on all UI controls.
    /// </summary>
    /// <remarks>
    /// This file uses two approaches for localization:
    /// 1. Traditional: Direct translator.Translate() calls in SetLanguage_*() methods
    /// 2. Binding-based: LocalizationService with BindText/BindHint/BindTooltip (Step 4)
    ///
    /// The binding approach is preferred for new code as it:
    /// - Reduces repetitive code
    /// - Allows easy key updates for dynamic text
    /// - Centralizes all bindings in one place
    /// </remarks>
    public partial class MainForm
    {
        #region Localization Bindings (Step 4)
        // ─────────────────────────────────────────────────────────────────────

        /// <summary>
        /// Initializes all control bindings for the LocalizationService.
        /// Call this once when the form is created to set up the binding system.
        /// </summary>
        /// <remarks>
        /// This method demonstrates the binding approach. Controls bound here
        /// will be automatically updated when _localization.ApplyAll() is called.
        /// </remarks>
        private void InitializeLocalizationBindings()
        {
            if (_localization == null) return;

            // ── Main Form Elements ──
            _localization
                // Tooltips for main action buttons
                .BindTooltip(BTNStartDatabase, "BTNStartDatabaseToolTip", TLTHome)
                .BindTooltip(BTNStartLogon, "BTNStartLogonToolTip", TLTHome)
                .BindTooltip(BTNStartWorld, "BTNStartWorldToolTip", TLTHome)
                .BindTooltip(InfoMachineResources, "InfoMachineResources", TLTHome)
                .BindTooltip(InfoLogonResorces, "InfoLogonResorces", TLTHome)
                .BindTooltip(InfoWorldResorces, "InfoWorldResorces", TLTHome);

            // ── Home Page - Resource Cards (Uppercase Titles) ──
            _localization
                .BindText(LBLCardMachineResourcesTitle, "LBLCardMachineResourcesTitle", toUpper: true)
                .BindText(LBLCardWorldResourcesTitle, "LBLCardWorldResourcesTitle", toUpper: true)
                .BindText(LBLCardLogonResourcesTitle, "LBLCardLogonResourcesTitle", toUpper: true);

            // ── Home Page - Resource Labels ──
            _localization
                .BindText(LBLRAMTextMachineResources, "LBLRAMTextMachineResources")
                .BindText(LBLCPUTextMachineResources, "LBLCPUTextMachineResources")
                .BindText(LBLRAMTextWorldResources, "LBLRAMTextMachineResources")
                .BindText(LBLCPUTextWorldResources, "LBLCPUTextMachineResources")
                .BindText(LBLRAMTextLogonResources, "LBLRAMTextMachineResources")
                .BindText(LBLCPUTextLogonResources, "LBLCPUTextMachineResources");

            // ── Process and Uptime Labels ──
            _localization
                .BindText(LBLDatabaseProcessID, "LBLProcessID")
                .BindText(LBLWorldProcessID, "LBLProcessID")
                .BindText(LBLLogonProcessID, "LBLProcessID")
                .BindText(LBLUpTimeDatabase, "LBLUpTime")
                .BindText(LBLUpTimeWorld, "LBLUpTime")
                .BindText(LBLUpTimeLogon, "LBLUpTime");

            // ── Database Editor - RealmList Tab Hints ──
            _localization
                .BindHint(TXTRealmID, "TXTRealmID")
                .BindHint(TXTRealmName, "TXTRealmName")
                .BindHint(TXTRealmAddress, "TXTRealmAddress")
                .BindHint(TXTRealmLocalAddress, "TXTRealmLocalAddress")
                .BindHint(TXTRealmSubnetMask, "TXTRealmSubnetMask")
                .BindHint(TXTRealmPort, "TXTRealmPort")
                .BindHint(TXTRealmGameBuild, "TXTRealmGameBuild")
                .BindHint(CBOXReamList, "CBOXReamList")
                .BindHint(TXTDomainName, "TXTDomainName")
                .BindHint(TXTInternIP, "TXTInternIP")
                .BindHint(TXTPublicIP, "TXTPublicIP");

            // ── Database Editor - RealmList Card Titles ──
            _localization
                .BindText(LBLCardRealmDataTitle, "LBLCardRealmDataTitle", toUpper: true)
                .BindText(LBLCardRealmOptionTitle, "LBLCardRealmOptionTitle", toUpper: true)
                .BindText(LBLCardRealmActionTitle, "LBLCardRealmActionTitle", toUpper: true)
                .BindTooltip(LBLCardRealmDataInfo, "LBLCardRealmDataInfo", TLTHome)
                .BindTooltip(LBLCardRealmOptionInfo, "LBLCardRealmOptionInfo", TLTHome)
                .BindTooltip(LBLCardRealmActionInfo, "LBLCardRealmActionInfo", TLTHome);

            // ── Database Editor - Account Tab ──
            _localization
                .BindHint(TXTBoxCreateUserUsername, "TXTBoxCreateUserUsername")
                .BindHint(TXTBoxCreateUserPassword, "TXTBoxCreateUserPassword")
                .BindHint(TXTBoxCreateUserEmail, "TXTBoxCreateUserEmail")
                .BindHint(CBOXAccountExpansion, "CBOXAccountExpansion")
                .BindHint(TXTBoxGMUsername, "TXTBoxGMUsername")
                .BindHint(CBoxGMRealmSelect, "CBoxGMRealmSelect")
                .BindHint(CBOXAccountSecurityAccess, "CBOXAccountSecurityAccess")
                .BindHint(TXTBoxPassUsername, "TXTBoxPassUsername")
                .BindHint(TXTBoxPassPassword, "TXTBoxPassPassword")
                .BindHint(TXTBoxPassRePassword, "TXTBoxPassRePassword")
                .BindText(LBLCardAccountCreate, "LBLCardAccountCreate", toUpper: true)
                .BindText(LBLCardAccountAdmin, "LBLCardAccountAdmin", toUpper: true)
                .BindText(LBLCardAccountPassword, "LBLCardAccountPassword", toUpper: true)
                .BindTooltip(LBLCardAccountCreateInfo, "LBLCardAccountCreateInfo", TLTHome)
                .BindTooltip(LBLCardAccountAccessInfo, "LBLCardAccountAccessInfo", TLTHome)
                .BindTooltip(LBLCardPasswordResetInfo, "LBLCardPasswordResetInfo", TLTHome);

            // ── Settings - Database Tab ──
            _localization
                .BindText(LBLCardDatabaseCredencialsTitle, "LBLCardDatabaseCredencialsTitle", toUpper: true)
                .BindText(LBLCardTableNameTitle, "LBLCardTableNameTitle", toUpper: true)
                .BindText(LBLCardPreconfiguredDBTitle, "LBLCardPreconfiguredDBTitle", toUpper: true)
                .BindText(LBLCardDatabaseBCTitle, "LBLCardDatabaseBCTitle", toUpper: true)
                .BindHint(TXTDatabaseHost, "TXTDatabaseHost")
                .BindHint(TXTDatabasePort, "TXTDatabasePorta")
                .BindHint(TXTDatabaseUser, "TXTDatabaseUser")
                .BindHint(TXTDatabasePassword, "TXTDatabasePassword")
                .BindHint(TXTAuthDatabase, "TXTAuthDatabase")
                .BindHint(TXTCharDatabase, "TXTCharDatabase")
                .BindHint(TXTWorldDatabase, "TXTWorldDatabase")
                .BindTooltip(LBLCardDatabaseCredencialsInfo, "LBLCardDatabaseCredencialsInfo", TLTHome)
                .BindTooltip(LBLCardTableNameInfo, "LBLCardTableNameInfo", TLTHome)
                .BindTooltip(LBLCardPreconfiguredDBInfo, "LBLCardPreconfiguredDBInfo", TLTHome)
                .BindTooltip(LBLCardDatabaseBCInfo, "LBLCardDatabaseBCInfo", TLTHome);

            // ── Settings - Custom Emulator Tab ──
            _localization
                .BindText(LBLCardCustomEmulatorTitle, "LBLCardCustomEmulatorTitle", toUpper: true)
                .BindText(LBLCardCustomNamesTitle, "LBLCardCustomNamesTitle", toUpper: true)
                .BindHint(TXTCustomDatabaseLocation, "TXTCustomDatabaseLocation")
                .BindHint(TXTCustomRepackLocation, "TXTCustomRepackLocation")
                .BindHint(CBOXSelectedEmulators, "CBOXSelectedEmulators")
                .BindHint(TXTCustomAuthName, "TXTCustomAuthName")
                .BindHint(TXTCustomWorldName, "TXTCustomWorldName")
                .BindHint(TXTCustomDatabaseName, "TxtCustomDatabaseName")
                .BindTooltip(LBLCardCustomEmulatorInfo, "LBLCardCustomEmulatorInfo", TLTHome)
                .BindTooltip(LBLCardCustomNamesInfo, "LBLCardCustomNamesInfo", TLTHome);

            // ── Settings - Trion Tab ──
            _localization
                .BindText(LBLCardCustomPreferencesTitle, "LBLCardCustomPreferencesTitle", toUpper: true)
                .BindText(LBLCardUpdateDashboardTitle, "LBLCardUpdateDashboardTitle", toUpper: true)
                .BindHint(CBOXColorSelect, "CBOXColorSelect")
                .BindHint(CBOXLanguageSelect, "CBOXLanguageSelect")
                .BindHint(TXTSupporterKey, "TXTSupporterKeyHint")
                .BindHint(CBOXTrionIcon, "CBOXTrionIconHint")
                .BindTooltip(LBLCardCustomPreferencesInfo, "LBLCardCustomPreferencesInfo", TLTHome)
                .BindTooltip(LBLCardUpdateDashboardInfo, "LBLCardUpdateDashboardInfo", TLTHome);

            // ── DDNS Settings ──
            _localization
                .BindText(LBLCardDDNSSettingsTitle, "LBLCardDDNSSettingsTitle", toUpper: true)
                .BindText(LBLCardDDNSTimerTitle, "LBLCardDDNSTimerTitle", toUpper: true)
                .BindHint(CBOXDDNService, "CBOXDDNService")
                .BindHint(TXTDDNSDomain, "TXTDDNSDomain")
                .BindHint(TXTDDNSUsername, "TXTDDNSUsername")
                .BindHint(TXTDDNSPassword, "TXTDDNSPassword")
                .BindHint(TXTDDNSInterval, "TXTDDNSInterval")
                .BindTooltip(LBLCardDDNSSettingsInfo, "LBLCardDDNSSettingsInfo", TLTHome)
                .BindTooltip(LBLCardDDNSTimerInfos, "LBLCardDDNSTimerInfos", TLTHome);

            // ── SPP Page - Card Titles ──
            _localization
                .BindText(LBLCardSPPVersionTitle, "LBLCardSPPVersionTitle", toUpper: true)
                .BindText(LBLCardSPPRunTitle, "LBLCardSPPRunTitle", toUpper: true)
                .BindHint(CBOXSPPVersion, "CBOXSPPVersion")
                .BindTooltip(LBLCardSPPversionInfo, "LBLCardSPPversionInfo", TLTHome)
                .BindTooltip(LBLCardElulatorsInfo, "LBLCardElulatorsInfo", TLTHome);
        }

        #endregion

        #region Language Setup
        // ─────────────────────────────────────────────────────────────────────

        /// <summary>
        /// Applies translated text to all UI controls based on the current language setting.
        /// This method is called when the application starts or when the language is changed.
        /// </summary>
        /// <remarks>
        /// The translations are loaded from JSON language files located in the Languages folder.
        /// Each control's text/hint is mapped to a key in the language file.
        /// </remarks>
        private void SetLanguage()
        {
            // Ensure UI updates happen on the UI thread
            if (InvokeRequired)
            {
                Invoke(new Action(SetLanguage));
                return;
            }

            // Populate combo boxes with translated items
            PopulateComboBoxes();

            // Apply all bound translations via LocalizationService (Step 4)
            // This handles: tooltips, uppercase card titles, hints, etc.
            _localization?.ApplyAll();

            // Apply translations to each UI section
            // Note: Some controls are now handled by _localization.ApplyAll() above,
            // but we keep these methods for button text and other dynamic elements
            SetLanguage_MainForm();
            SetLanguage_HomePage();
            SetLanguage_DatabaseEditor();
            SetLanguage_Settings();
            SetLanguage_SPP();
            SetLanguage_Downloader();
        }

        /// <summary>
        /// Sets translations for the main form elements (title, tabs, tray icon).
        /// </summary>
        private void SetLanguage_MainForm()
        {
            // Tooltips for main action buttons
            TLTHome.SetToolTip(BTNStartDatabase, translator.Translate("BTNStartDatabaseToolTip"));
            TLTHome.SetToolTip(BTNStartLogon, translator.Translate("BTNStartLogonToolTip"));
            TLTHome.SetToolTip(BTNStartWorld, translator.Translate("BTNStartWorldToolTip"));
            TLTHome.SetToolTip(InfoMachineResources, translator.Translate("InfoMachineResources"));
            TLTHome.SetToolTip(InfoLogonResorces, translator.Translate("InfoLogonResorces"));
            TLTHome.SetToolTip(InfoWorldResorces, translator.Translate("InfoWorldResorces"));

            // Main action button text (default OFF state)
            BTNStartDatabase.Text = translator.Translate("BTNStartDatabaseTextOFF");
            BTNStartLogon.Text = translator.Translate("BTNStartLogonTextOFF");
            BTNStartWorld.Text = translator.Translate("BTNStartWorldTextOFF");
            BTNStartWebsite.Text = translator.Translate("BTNStartWebsiteTextOFF");

            // System tray notification settings
            NIcon.BalloonTipText = translator.Translate("NIconBalloonTipText");
            NIcon.BalloonTipTitle = translator.Translate("NIconBalloonTipTitle");
            NIcon.Text = translator.Translate("NIconBalloonTipTitle");

            // Tab page titles
            TabSettings.Text = translator.Translate("TabSettingsTitle");
            TabHome.Text = translator.Translate("TabHomeTitle");
            TabDatabaseEditor.Text = translator.Translate("TabDatabaseEditorTitle");
            TabSPP.Text = translator.Translate("TabSPPTitle");
            TabNotification.Text = translator.Translate("TabNotification");
            TabDDNS.Text = translator.Translate("TabDDNS");
            TabDownloader.Text = translator.Translate("TabDownloader");

            // Main form title
            Text = translator.Translate("TrionFormText");
        }

        /// <summary>
        /// Sets translations for the Home page (resource monitoring cards).
        /// </summary>
        private void SetLanguage_HomePage()
        {
            // Card titles (uppercase for visual emphasis)
            LBLCardMachineResourcesTitle.Text = translator.Translate("LBLCardMachineResourcesTitle").ToUpper(CultureInfo.InvariantCulture);
            LBLCardWorldResourcesTitle.Text = translator.Translate("LBLCardWorldResourcesTitle").ToUpper(CultureInfo.InvariantCulture);
            LBLCardLogonResourcesTitle.Text = translator.Translate("LBLCardLogonResourcesTitle").ToUpper(CultureInfo.InvariantCulture);

            // Resource labels (RAM/CPU)
            LBLRAMTextMachineResources.Text = translator.Translate("LBLRAMTextMachineResources");
            LBLCPUTextMachineResources.Text = translator.Translate("LBLCPUTextMachineResources");
            LBLRAMTextWorldResources.Text = translator.Translate("LBLRAMTextMachineResources");
            LBLCPUTextWorldResources.Text = translator.Translate("LBLCPUTextMachineResources");
            LBLRAMTextLogonResources.Text = translator.Translate("LBLRAMTextMachineResources");
            LBLCPUTextLogonResources.Text = translator.Translate("LBLCPUTextMachineResources");

            // Process ID labels
            LBLDatabaseProcessID.Text = translator.Translate("LBLProcessID");
            LBLWorldProcessID.Text = translator.Translate("LBLProcessID");
            LBLLogonProcessID.Text = translator.Translate("LBLProcessID");

            // Uptime labels
            LBLUpTimeDatabase.Text = translator.Translate("LBLUpTime");
            LBLUpTimeWorld.Text = translator.Translate("LBLUpTime");
            LBLUpTimeLogon.Text = translator.Translate("LBLUpTime");
        }

        /// <summary>
        /// Sets translations for the Database Editor page (RealmList and Account tabs).
        /// </summary>
        private void SetLanguage_DatabaseEditor()
        {
            // ── RealmList Tab ──
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

            // RealmList action buttons
            BTNOpenPublic.Text = translator.Translate("BTNOpenPublic");
            BTNOpenIntern.Text = translator.Translate("BTNOpenIntern");
            BTNEditRealmlistData.Text = translator.Translate("BTNEditRealmlistDataON");
            BTNCreateRealmList.Text = translator.Translate("BTNCreateRealmList");
            BTNDeleteRealmList.Text = translator.Translate("BTNDeleteRealmList");
            BTNForceRefresh.Text = translator.Translate("BTNForceRefresh");

            // Card titles and tooltips
            LBLCardRealmDataTitle.Text = translator.Translate("LBLCardRealmDataTitle").ToUpper(CultureInfo.InvariantCulture);
            LBLCardRealmOptionTitle.Text = translator.Translate("LBLCardRealmOptionTitle").ToUpper(CultureInfo.InvariantCulture);
            LBLCardRealmActionTitle.Text = translator.Translate("LBLCardRealmActionTitle").ToUpper(CultureInfo.InvariantCulture);
            TLTHome.SetToolTip(LBLCardRealmDataInfo, translator.Translate("LBLCardRealmDataInfo"));
            TLTHome.SetToolTip(LBLCardRealmOptionInfo, translator.Translate("LBLCardRealmOptionInfo"));
            TLTHome.SetToolTip(LBLCardRealmActionInfo, translator.Translate("LBLCardRealmActionInfo"));

            // Tab names
            TabRealmList.Text = translator.Translate("TabRealmList");
            TabAccount.Text = translator.Translate("TabAccount");

            // ── Account Editor Tab ──
            TLTHome.SetToolTip(LBLCardAccountCreateInfo, translator.Translate("LBLCardAccountCreateInfo"));
            TLTHome.SetToolTip(LBLCardAccountAccessInfo, translator.Translate("LBLCardAccountAccessInfo"));
            TLTHome.SetToolTip(LBLCardPasswordResetInfo, translator.Translate("LBLCardPasswordResetInfo"));

            // Account creation fields
            TXTBoxCreateUserUsername.Hint = translator.Translate("TXTBoxCreateUserUsername");
            TXTBoxCreateUserPassword.Hint = translator.Translate("TXTBoxCreateUserPassword");
            TXTBoxCreateUserEmail.Hint = translator.Translate("TXTBoxCreateUserEmail");
            CBOXAccountExpansion.Hint = translator.Translate("CBOXAccountExpansion");
            BTNAccountCreate.Text = translator.Translate("BTNAccountCreate");

            // Card titles
            LBLCardAccountCreate.Text = translator.Translate("LBLCardAccountCreate").ToUpper(CultureInfo.InvariantCulture);
            LBLCardAccountAdmin.Text = translator.Translate("LBLCardAccountAdmin").ToUpper(CultureInfo.InvariantCulture);
            LBLCardAccountPassword.Text = translator.Translate("LBLCardAccountPassword").ToUpper(CultureInfo.InvariantCulture);

            // GM level fields
            TXTBoxGMUsername.Hint = translator.Translate("TXTBoxGMUsername");
            CBoxGMRealmSelect.Hint = translator.Translate("CBoxGMRealmSelect");
            CBOXAccountSecurityAccess.Hint = translator.Translate("CBOXAccountSecurityAccess");

            // Password reset fields
            TXTBoxPassUsername.Hint = translator.Translate("TXTBoxPassUsername");
            TXTBoxPassPassword.Hint = translator.Translate("TXTBoxPassPassword");
            TXTBoxPassRePassword.Hint = translator.Translate("TXTBoxPassRePassword");
            BTNTBoxPassResset.Text = translator.Translate("BTNTBoxPassResset");
            BTNGMCreate.Text = translator.Translate("BTNGMCreate");
            TGLAccountShowPassword.Text = translator.Translate("TGLAccountShowPassword");
        }

        /// <summary>
        /// Sets translations for the Settings page (Database, Custom Emulators, Trion, DDNS tabs).
        /// </summary>
        private void SetLanguage_Settings()
        {
            // Tab names
            TabDatabase.Text = translator.Translate("TabDatabase");
            TabCustom.Text = translator.Translate("TabCustom");
            TabTrion.Text = translator.Translate("TabTrion");

            // ── Trion Settings ──
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

            // ── Custom Emulators Settings ──
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

            // Emulator website buttons
            BTNAscEmuWebsite.Text = translator.Translate("BTNAscEmuWebsite");
            BTNACoreWebsite.Text = translator.Translate("BTNACoreWebsite");
            BTNCMangosWebsite.Text = translator.Translate("BTNCMangosWebsite");
            BTNCypherWebsite.Text = translator.Translate("BTNCypherWebsite");
            BTNTrinityCoreWebsite.Text = translator.Translate("BTNTrinityCoreWebsite");
            BTNSkyFireWebsite.Text = translator.Translate("BTNSkyFireWebsite");
            TLTHome.SetToolTip(LBLCardCustomEmulatorInfo, translator.Translate("LBLCardCustomEmulatorInfo"));
            TLTHome.SetToolTip(LBLCardCustomNamesInfo, translator.Translate("LBLCardCustomNamesInfo"));

            // ── Database Settings ──
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

            // Database toggle buttons for each expansion
            TGLClassicDB.Text = translator.Translate("TGLClassicDB");
            TGLTbcDB.Text = translator.Translate("TGLTbcDB");
            TGLWotlkDB.Text = translator.Translate("TGLWotlkDB");
            TGLCataDB.Text = translator.Translate("TGLCataDB");
            TGLMopDB.Text = translator.Translate("TGLMopDB");
            TGLCustomDB.Text = translator.Translate("TGLCustomDB");

            // Backup options
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

            // ── DDNS Settings ──
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
        }

        /// <summary>
        /// Sets translations for the SPP (Single Player Project) page.
        /// </summary>
        private void SetLanguage_SPP()
        {
            // Card titles
            LBLCardSPPVersionTitle.Text = translator.Translate("LBLCardSPPVersionTitle").ToUpper(CultureInfo.InvariantCulture);
            LBLCardSPPRunTitle.Text = translator.Translate("LBLCardSPPRunTitle").ToUpper(CultureInfo.InvariantCulture);

            // Main action buttons
            BTNInstallSPP.Text = translator.Translate("BTNInstallSPP");
            BTNRepairSPP.Text = translator.Translate("BTNRepairSPP");
            BTNUninstallSPP.Text = translator.Translate("BTNUninstallSPP");

            // Launch toggles for each expansion
            TGLClassicLaunch.Text = translator.Translate("TGLClassicLaunch");
            TGLTBCLaunch.Text = translator.Translate("TGLTBCLaunch");
            TGLWotLKLaunch.Text = translator.Translate("TGLWotLKLaunch");
            TGLCataLaunch.Text = translator.Translate("TGLCataLaunch");
            TGLMoPLaunch.Text = translator.Translate("TGLMoPLaunch");

            // Version selector
            CBOXSPPVersion.Hint = translator.Translate("CBOXSPPVersion");

            // Installed checkboxes
            ChecCLASSICInstalled.Text = translator.Translate("CheckBoxInstalled");
            ChecTBCInstalled.Text = translator.Translate("CheckBoxInstalled");
            ChecWOTLKInstalled.Text = translator.Translate("CheckBoxInstalled");
            ChecCATAInstalled.Text = translator.Translate("CheckBoxInstalled");
            ChecMOPInstalled.Text = translator.Translate("CheckBoxInstalled");

            // Individual world server start buttons
            BTNWorldClassicStart.Text = translator.Translate("BTNWorldSeparateStart");
            BTNWorldTBCStart.Text = translator.Translate("BTNWorldSeparateStart");
            BTNWorldWotLKStart.Text = translator.Translate("BTNWorldSeparateStart");
            BTNWorldCataStart.Text = translator.Translate("BTNWorldSeparateStart");
            BTNWorldMoPStart.Text = translator.Translate("BTNWorldSeparateStart");

            // Individual logon server start buttons
            BTNLogonClassicStart.Text = translator.Translate("BTNLogonSeparateStart");
            BTNLogonTBCStart.Text = translator.Translate("BTNLogonSeparateStart");
            BTNLogonWotLKStart.Text = translator.Translate("BTNLogonSeparateStart");
            BTNLogonCataStart.Text = translator.Translate("BTNLogonSeparateStart");
            BTNLogonMoPStart.Text = translator.Translate("BTNLogonSeparateStart");

            // Support button and tooltips
            BTNShowSupport.Text = translator.Translate("BTNShowSupport");
            TLTHome.SetToolTip(LBLCardSPPversionInfo, translator.Translate("LBLCardSPPversionInfo"));
            TLTHome.SetToolTip(LBLCardElulatorsInfo, translator.Translate("LBLCardElulatorsInfo"));
        }

        /// <summary>
        /// Sets translations for the Downloader page (file download progress display).
        /// </summary>
        private void SetLanguage_Downloader()
        {
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
        }

        #endregion
    }
}
