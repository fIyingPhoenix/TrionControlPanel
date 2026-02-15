// =============================================================================
// MainForm.Settings.cs
// Purpose: Handles loading and applying application settings to the UI
// Related UI: All settings controls, theme selection, DDNS configuration
// Dependencies: Settings, AppSettings, MaterialSkinManager
// =============================================================================

using MaterialSkin;
using System.Globalization;
using TrionControlPanel.Desktop.Extensions.Application;
using TrionControlPanel.Desktop.Extensions.Classes;
using TrionControlPanel.Desktop.Extensions.Modules.Lists;
using static TrionControlPanel.Desktop.Extensions.Modules.Enums;

namespace TrionControlPanelDesktop
{
    /// <summary>
    /// Partial class containing settings loading and UI binding methods.
    /// Handles the persistence and restoration of application state.
    /// </summary>
    public partial class MainForm
    {
        #region Settings File Operations
        // ─────────────────────────────────────────────────────────────────────

        /// <summary>
        /// Loads settings from the JSON file on disk.
        /// Creates a default settings file if one doesn't exist.
        /// </summary>
        /// <remarks>
        /// Called during form construction before InitializeComponent().
        /// The settings object is used throughout the application lifecycle.
        /// </remarks>
        private void LoadSettingsFromFile()
        {
            if (!File.Exists("Settings.json"))
            {
                Settings.CreateSettings("Settings.json");
            }
            settings = Settings.LoadSettings("Settings.json");

            // Initialize ServerController with loaded settings (Step 8)
            _serverController = new ServerController(settings);
        }

        #endregion

        #region Settings UI Binding
        // ─────────────────────────────────────────────────────────────────────

        /// <summary>
        /// Applies all loaded settings to their corresponding UI controls.
        /// This includes toggles, text fields, combo boxes, and checkboxes.
        /// </summary>
        /// <remarks>
        /// Call this after loading settings from file or after programmatic changes
        /// to ensure the UI reflects the current settings state.
        /// </remarks>
        private void LoadSettingsUI()
        {
            // Select correct items in combo boxes based on enum values
            CBoxSelectItems();

            // ── Expansion Launch Toggles ──
            TGLClassicLaunch.Checked = settings.LaunchClassicCore;
            TGLTBCLaunch.Checked = settings.LaunchTBCCore;
            TGLWotLKLaunch.Checked = settings.LaunchWotLKCore;
            TGLCataLaunch.Checked = settings.LaunchCataCore;
            TGLMoPLaunch.Checked = settings.LaunchMoPCore;
            TGLUseCustomServer.Checked = settings.LaunchCustomCore;

            // ── Custom Executable Names ──
            TXTCustomAuthName.Text = settings.CustomLogonExeName;
            TXTCustomWorldName.Text = settings.CustomWorldExeName;
            TXTCustomDatabaseName.Text = settings.DBExeName;

            // ── Working Directories ──
            TXTCustomRepackLocation.Text = settings.CustomWorkingDirectory;
            TXTCustomDatabaseLocation.Text = settings.DBLocation;

            // ── Database Connection Settings ──
            TXTDatabaseHost.Text = settings.DBServerHost;
            TXTDatabasePort.Text = settings.DBServerPort;
            TXTDatabaseUser.Text = settings.DBServerUser;
            TXTDatabasePassword.Text = settings.DBServerPassword;

            // ── Database Table Names ──
            TXTCharDatabase.Text = settings.CharactersDatabase;
            TXTAuthDatabase.Text = settings.AuthDatabase;
            TXTWorldDatabase.Text = settings.WorldDatabase;

            // ── Application Behavior Toggles ──
            TGLAutoUpdateTrion.Checked = settings.AutoUpdateTrion;
            TGLAutoUpdateCore.Checked = settings.AutoUpdateCore;
            TGLHideConsole.Checked = settings.ConsoleHide;
            TGLNotificationSound.Checked = settings.NotificationSound;
            TGLStayInTray.Checked = settings.StayInTray;
            TGLCustomNames.Checked = settings.CustomNames;
            TGLRunTrionStartup.Checked = settings.RunWithWindows;
            TGLServerCrashDetection.Checked = settings.ServerCrashDetection;
            TGLServerStartup.Checked = settings.RunServerWithWindows;
            TGLAutoUpdateDatabase.Checked = settings.AutoUpdateDatabase;

            // ── Expansion Launch Toggles (duplicated in settings for safety) ──
            TGLClassicLaunch.Checked = settings.LaunchClassicCore;
            TGLTBCLaunch.Checked = settings.LaunchTBCCore;
            TGLWotLKLaunch.Checked = settings.LaunchWotLKCore;
            TGLCataLaunch.Checked = settings.LaunchCataCore;
            TGLMoPLaunch.Checked = settings.LaunchMoPCore;

            // ── Installed Expansion Checkboxes ──
            ChecCLASSICInstalled.Checked = settings.ClassicInstalled;
            ChecTBCInstalled.Checked = settings.TBCInstalled;
            ChecWOTLKInstalled.Checked = settings.WotLKInstalled;
            ChecCATAInstalled.Checked = settings.CataInstalled;
            ChecMOPInstalled.Checked = settings.MOPInstalled;

            // ── DDNS Configuration ──
            TXTDDNSDomain.Text = settings.DDNSDomain;
            TXTDDNSUsername.Text = settings.DDNSUsername;
            TXTDDNSPassword.Text = settings.DDNSPassword;
            TXTDDNSInterval.Text = settings.DDNSInterval.ToString(CultureInfo.InvariantCulture);
            TGLDDNSRunOnStartup.Checked = settings.DDNSRunOnStartup;
            TimerDinamicDNS.Enabled = settings.DDNSRunOnStartup;
            TimerDinamicDNS.Interval = settings.DDNSInterval;

            // ── Supporter Key ──
            TXTSupporterKey.Text = settings.SupporterKey;

            // ── Form Icon ──
            ChangeFormIcon(settings.TrionIcon);
        }

        /// <summary>
        /// Sets the selected items in combo boxes based on enum values from settings.
        /// Maps enum values to their display strings.
        /// </summary>
        private void CBoxSelectItems()
        {
            // ── Emulator Core Selection ──
            CBOXSelectedEmulators.SelectedItem = settings.SelectedCore switch
            {
                Cores.AzerothCore => "AzerothCore",
                Cores.CMaNGOS => "CMaNGOS",
                Cores.CypherCore => "CypherCore",
                Cores.TrinityCore => "TrinityCore",
                Cores.TrinityCore335 => "TrinityCore 3.3.5",
                Cores.TrinityCoreClassic => "TrinityCore Classic",
                Cores.VMaNGOS => "VMaNGOS",
                _ => CBOXSelectedEmulators.SelectedItem
            };

            // ── SPP Version Selection (uses translated strings) ──
            CBOXSPPVersion.SelectedItem = settings.SelectedSPP switch
            {
                SPP.Classic => translator.Translate("SPPver1"),
                SPP.TheBurningCrusade => translator.Translate("SPPver2"),
                SPP.WrathOfTheLichKing => translator.Translate("SPPver3"),
                SPP.Cataclysm => translator.Translate("SPPver4"),
                SPP.MistsOfPandaria => translator.Translate("SPPver5"),
                _ => CBOXSPPVersion.SelectedItem
            };

            // ── DDNS Service Provider Selection ──
            CBOXDDNService.SelectedItem = settings.DDNSService switch
            {
                DDNSService.Afraid => "freedns.afraid.org",
                DDNSService.AllInkl => "all-inkl.com",
                DDNSService.Cloudflare => "cloudflare.com",
                DDNSService.DuckDNS => "duckdns.org",
                DDNSService.NoIP => "noip.com",
                DDNSService.Dynu => "dynu.com",
                DDNSService.DynDNS => "dyn.com",
                DDNSService.Enom => "enom.com",
                DDNSService.Freemyip => "freemyip.com",
                DDNSService.OVH => "ovhcloud.com",
                DDNSService.STRATO => "strato.de",
                _ => CBOXDDNService.SelectedItem
            };
        }

        #endregion

        #region Theme/Skin Loading
        // ─────────────────────────────────────────────────────────────────────

        /// <summary>
        /// Applies the selected color theme to the MaterialSkin manager.
        /// Updates the form's visual appearance including colors and tooltip styling.
        /// </summary>
        /// <remarks>
        /// The theme affects all MaterialSkin controls on the form.
        /// Available themes: TrionBlue (default), Purple, Green, Orange.
        /// </remarks>
        private void LoadSkin()
        {
            // Get or create the MaterialSkinManager singleton
            materialSkinManager = MaterialSkinManager.Instance;

            // Enable backcolor enforcement for consistent theming
            // Must be set before AddFormToManage()
            materialSkinManager!.EnforceBackcolorOnAllComponents = true;

            // Register this form with the skin manager
            materialSkinManager!.AddFormToManage(this);
            materialSkinManager!.Theme = MaterialSkinManager.Themes.TRION;

            // Apply the selected color scheme
            switch (settings.TrionTheme)
            {
                case TrionTheme.TrionBlue:
                    ApplyColorScheme(
                        Primary.TrionBlue500,
                        Primary.TrionBlue300,
                        Primary.TrionBlue200,
                        Accent.TrionBlue900,
                        Primary.TrionBlue500);
                    break;

                case TrionTheme.Purple:
                    ApplyColorScheme(
                        Primary.DeepPurple500,
                        Primary.DeepPurple300,
                        Primary.DeepPurple200,
                        Accent.Purple700,
                        Primary.DeepPurple500);
                    break;

                case TrionTheme.Green:
                    ApplyColorScheme(
                        Primary.Green500,
                        Primary.Green300,
                        Primary.Green200,
                        Accent.Lime700,
                        Primary.Green500,
                        Primary.Lime700); // Different title color
                    break;

                case TrionTheme.Orange:
                    ApplyColorScheme(
                        Primary.Orange500,
                        Primary.Orange300,
                        Primary.Orange200,
                        Accent.Orange700,
                        Primary.Orange500);
                    break;

                default:
                    // Default to TrionBlue
                    ApplyColorScheme(
                        Primary.TrionBlue500,
                        Primary.TrionBlue300,
                        Primary.TrionBlue200,
                        Accent.TrionBlue900,
                        Primary.TrionBlue500);
                    break;
            }

            // Force immediate visual update
            Invalidate();
            Update();
            Refresh();
        }

        /// <summary>
        /// Helper method to apply a color scheme to the MaterialSkinManager and tooltip.
        /// </summary>
        /// <param name="primary">Primary color (main UI elements)</param>
        /// <param name="primaryDark">Dark variant of primary</param>
        /// <param name="primaryLight">Light variant of primary</param>
        /// <param name="accent">Accent color for highlights</param>
        /// <param name="borderColor">Tooltip border color</param>
        /// <param name="titleColor">Tooltip title color (optional, defaults to borderColor)</param>
        private void ApplyColorScheme(
            Primary primary,
            Primary primaryDark,
            Primary primaryLight,
            Accent accent,
            Primary borderColor,
            Primary? titleColor = null)
        {
            materialSkinManager!.ColorScheme = new ColorScheme(
                primary,
                primaryDark,
                primaryLight,
                accent,
                TextShade.WHITE);

            TLTHome.BorderColor = Settings.ConvertToColor(borderColor);
            TLTHome.TitleColor = Settings.ConvertToColor(titleColor ?? borderColor);
        }

        #endregion
    }
}
