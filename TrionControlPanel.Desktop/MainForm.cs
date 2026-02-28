// =============================================================================
// MainForm.cs
// Purpose: Core form initialization, fields, and main lifecycle methods
// This is the main entry point - see other MainForm.*.cs files for features:
//   - MainForm.Language.cs      : UI localization
//   - MainForm.Settings.cs      : Settings loading and skin management
//   - MainForm.ServerControl.cs : Server start/stop logic
//   - MainForm.Monitoring.cs    : Resource usage and process tracking
//   - MainForm.DatabaseEditor.cs: RealmList and Account management
//   - MainForm.SPPInstaller.cs  : Install/Repair/Uninstall expansions
//   - MainForm.DDNS.cs          : Dynamic DNS updates
//   - MainForm.SettingsEvents.cs: Settings control event handlers
//   - MainForm.Events.cs        : Event subscription and handlers (Step 13)
//   - MainForm.Helpers.cs       : Helper methods and custom UX
//   - MainForm.Offline.cs       : Offline mode handling
// =============================================================================

using MaterialSkin;
using MaterialSkin.Controls;
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
using TrionControlPanelDesktop.Extensions.Modules;
using static TrionControlPanel.Desktop.Extensions.Notification.AlertBox;

namespace TrionControlPanelDesktop
{
    /// <summary>
    /// Main application form for Trion Control Panel.
    /// Manages WoW emulator server instances, database connections, and SPP installations.
    /// </summary>
    public partial class MainForm : MaterialForm
    {
        #region Fields
        // ─────────────────────────────────────────────────────────────────────

        /// <summary>Translator instance for UI localization.</summary>
        private Translator translator = new();

        /// <summary>Localization service for binding controls to translations (Step 4).</summary>
        private LocalizationService? _localization;

        /// <summary>Centralized server controller for all server operations (Step 8).</summary>
        private ServerController? _serverController;

        /// <summary>Application settings loaded from Settings.json.</summary>
        private AppSettings settings;

        /// <summary>MaterialSkin manager for theming.</summary>
        private MaterialSkinManager? materialSkinManager;

        /// <summary>Loading screen shown during initialization.</summary>
        private LoadingScreen loadingForm = new();

        /// <summary>Number of process IDs to show per page when cycling.</summary>
        private int AppPageSize { get; } = 1;

        /// <summary>Current page for world process ID display.</summary>
        private int _worldCurrentPage { get; set; } = 1;

        /// <summary>Current page for logon process ID display.</summary>
        private int _logonCurrentPage { get; set; } = 1;

        /// <summary>Tracks whether realm list editing is active.</summary>
        private bool _editRealmList { get; set; }

        /// <summary>Timer for debouncing text input saves.</summary>
        private static System.Threading.Timer? TimerKeyPress { get; set; }

        /// <summary>Cancellation token source for async operations.</summary>
        private CancellationTokenSource _cancellationTokenSource;

        /// <summary>Cancellation token for async operations.</summary>
        private CancellationToken _cancellationToken;

        #endregion

        #region Constructor
        // ─────────────────────────────────────────────────────────────────────

        /// <summary>
        /// Initializes the main form.
        /// Loads settings, shows loading screen, and sets up double buffering.
        /// </summary>
        public MainForm()
        {
            TrionLogger.LogAppLifecycle("MainForm initializing");

            // Enable double buffering for smoother rendering
            DoubleBuffered = true;
            SetStyle(ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.UserPaint, true);

            // Show loading screen while initializing
            loadingForm.Show();
            Opacity = 0;
            WindowState = FormWindowState.Minimized;
            ShowInTaskbar = false;
            Invalidate();

            // Load settings before InitializeComponent (needed for skin)
            LoadSettingsFromFile();

            // Clean up any leftover update executable
            if (File.Exists("update.exe"))
            {
                File.Delete("update.exe");
                TrionLogger.Info("Cleaned up leftover update.exe");
            }

            InitializeComponent();
            InitializeCustomUX();

            // Subscribe to application-wide events (Step 13)
            SubscribeToAppEvents();
        }

        #endregion

        #region Form Lifecycle Events
        // ─────────────────────────────────────────────────────────────────────

        /// <summary>
        /// Main form load event. Completes initialization after UI is ready.
        /// </summary>
        private async void MainForm_LoadAsync(object sender, EventArgs e)
        {
            // Apply settings to UI controls
            LoadSettingsUI();
            GetAllLanguages();
            LoadSkin();
            LoadLangauge();

            // Set RAM progress bar maximum to total system RAM
            PbarRAMMachineResources.Maximum = PerformanceMonitor.GetTotalRamInMB();

            // Check network connectivity
            await NetworkManager.GetAPIServer();

            // Auto-detect previously installed components
            await CheckAndMarkInstalled();

            if (NetworkManager.IsOffline)
            {
                SetOfflineState();
            }
            else
            {
                await UpdateSppVersion();
                await StartAutoUpdate();
            }

            // Hide loading screen and show main form
            loadingForm.Close();
            Opacity = 1;
            ShowInTaskbar = true;
            WindowState = FormWindowState.Normal;
            Refresh();

            TrionLogger.LogAppLifecycle("MainForm loaded", NetworkManager.IsOffline ? "Offline mode" : "Online mode");

            // Start database if needed
            SetupDatabaseIfMissing();
        }

        /// <summary>
        /// Form closing event. Saves settings and stops all servers gracefully.
        /// </summary>
        private async void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Cancel the close event to perform cleanup first
            e.Cancel = true;

            TrionLogger.LogAppLifecycle("Shutting down", "Stopping all servers");
            AlertBox.Show(translator.Translate("AlerBoxShuttingDown"), NotificationType.Info, settings);

            // Save settings
            await Settings.SaveSettings(settings, "Settings.json");

            // Stop all running servers
            await AppExecuteManager.StopDatabase();
            await AppExecuteManager.StopLogon();
            await AppExecuteManager.StopWorld();

            // Wait for servers to stop (with timeout to prevent hanging)
            int maxWaitMs = 30000;
            int elapsed = 0;
            while ((FormData.UI.Form.DBRunning ||
                    ServerMonitor.ServerRunningLogon() ||
                    ServerMonitor.ServerRunningWorld()) &&
                   elapsed < maxWaitMs)
            {
                await Task.Delay(500);
                elapsed += 500;
            }

            // Unsubscribe from application events to prevent memory leaks (Step 13)
            UnsubscribeFromAppEvents();

            TrionLogger.LogAppLifecycle("Shutdown complete");
            Environment.Exit(0);
        }

        #endregion

        #region Database Setup
        // ─────────────────────────────────────────────────────────────────────

        /// <summary>
        /// Checks if database is installed and starts it, or initiates installation.
        /// </summary>
        private async void SetupDatabaseIfMissing()
        {
            // If already installed or currently installing, just start it
            if (settings.DBInstalled || FormData.UI.Form.InstallingEmulator)
            {
                if (!FormData.UI.Form.InstallingEmulator)
                {
                    BTNStartDatabase_Click(null!, null!);
                }
                return;
            }

            // Can't install in offline mode
            if (NetworkManager.IsOffline)
            {
                TrionLogger.Warning("Database not installed but cannot download in offline mode");
                AlertBox.Show(
                    translator.Translate("Database missing but cannot download in Offline Mode."),
                    NotificationType.Error, settings);
                return;
            }

            // Start database installation
            TrionLogger.Info("Database not installed, starting automatic installation");
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
                deleteFilesAfterUnzip: false);
        }

        #endregion

        #region Tab Control Navigation
        // ─────────────────────────────────────────────────────────────────────

        /// <summary>
        /// Handles tab selection changes with validation.
        /// </summary>
        private async void MainFormTabControler_Selecting(object sender, TabControlCancelEventArgs e)
        {
            // Database Editor tab requires database to be running
            if (e.TabPage == TabDatabaseEditor && !FormData.UI.Form.DBRunning)
            {
                var result = MaterialMessageBox.Show(
                    this,
                    translator.Translate("DatabaseNotRunningErrorMbox"),
                    translator.Translate("MessageBoxTitleInfo"),
                    MessageBoxButtons.OKCancel,
                    true,
                    FlexibleMaterialForm.ButtonsPosition.Center);

                if (result == DialogResult.OK)
                {
                    BTNStartDatabase_Click(sender, e);
                    await LoadRealmList();
                    await LoadIPAdress();
                }
                else
                {
                    e.Cancel = true;
                }
            }
            else if (e.TabPage == TabDatabaseEditor && FormData.UI.Form.DBRunning)
            {
                await LoadRealmList();
                await LoadIPAdress();
            }

            // Downloader tab is only accessible during installation
            if (e.TabPage == TabDownloader && !FormData.UI.Form.InstallingEmulator)
            {
                e.Cancel = true;
            }
        }

        #endregion

        #region Installation Completion
        // ─────────────────────────────────────────────────────────────────────

        /// <summary>
        /// Finalizes installation by configuring settings for the installed component.
        /// Called after PerformInstallationAsync completes.
        /// </summary>
        private async void InstallFinished()
        {
            // Progress reporters for SQL import
            var downloadSpeedProgress = new Progress<double>(message =>
                LBLDownloadSpeed.Text = string.Format(
                    System.Globalization.CultureInfo.InvariantCulture,
                    translator.Translate("LBLDownloadSpeed"), $"{message:0.##} MB/s"));
            var downloadProgress = new Progress<double>(message =>
                PBarCurrentDownlaod.Value = (int)message);

            _cancellationTokenSource = new CancellationTokenSource();
            _cancellationToken = _cancellationTokenSource.Token;

            // Configure each expansion that was just installed
            if (FormData.Infos.Install.Classic)
            {
                await ConfigureClassicInstallation(downloadProgress, downloadSpeedProgress);
            }
            if (FormData.Infos.Install.TBC)
            {
                await ConfigureTBCInstallation(downloadProgress, downloadSpeedProgress);
            }
            if (FormData.Infos.Install.WotLK)
            {
                ConfigureWotLKInstallation();
            }
            if (FormData.Infos.Install.Cata)
            {
                ConfigureCataInstallation();
            }
            if (FormData.Infos.Install.Mop)
            {
                ConfigureMoPInstallation();
            }
            if (FormData.Infos.Install.Database)
            {
                await ConfigureDatabaseInstallation(downloadProgress);
            }
            if (FormData.Infos.Install.Trion)
            {
                // Trion update - launch updater and exit
                System.Diagnostics.Process.Start("update.exe");
                Environment.Exit(0);
            }

            // Return to home tab
            if (MainFormTabControler.SelectedTab == TabDownloader)
            {
                MainFormTabControler.SelectedTab = TabHome;
            }

            LoadSettingsUI();
        }

        #region Expansion Configuration Methods
        // ─────────────────────────────────────────────────────────────────────
         
        private async Task ConfigureClassicInstallation(
            IProgress<double> downloadProgress,
            IProgress<double> downloadSpeedProgress)
        {
            string classic = Links.Install.Classic.Replace("/", @"\");

            await AccessManager.ExecuteSqlFileAsync(
                $"{classic}/database/full/classicDB.sql",
                AccessManager.ConnectionString(settings),
                _cancellationToken, downloadProgress, null, downloadSpeedProgress);

            settings.ClassicInstalled = true;
            settings.LaunchClassicCore = true;
            settings.ClassicWorkingDirectory = classic;
            settings.ClassicLogonExeLoc = FileManager.GetExecutableLocation(classic, "realmd");
            settings.ClassicLogonExeName = "realmd";
            settings.ClassicWorldExeLoc = FileManager.GetExecutableLocation(classic, "mangosd");
            settings.ClassicWorldExeName = "mangosd";
            settings.ClassicLogonName = "WoW Classic Logon";
            settings.ClassicWorldName = "WoW Classic World";
            settings.SelectedCore = Enums.Cores.CMaNGOS;
            Directory.CreateDirectory("logs/classic");
        }

        private async Task ConfigureTBCInstallation(
            IProgress<double> downloadProgress,
            IProgress<double> downloadSpeedProgress)
        {
            string tbc = Links.Install.TBC.Replace("/", @"\");

            settings.TBCInstalled = true;
            settings.LaunchTBCCore = true;
            settings.TBCWorkingDirectory = tbc;
            settings.TBCLogonExeLoc = FileManager.GetExecutableLocation(tbc, "realmd");
            settings.TBCLogonExeName = "realmd";
            settings.TBCWorldExeLoc = FileManager.GetExecutableLocation(tbc, "mangosd");
            settings.TBCWorldExeName = "mangosd";
            settings.TBCLogonName = "The Burning Crusade Logon";
            settings.TBCWorldName = "The Burning Crusade World";

            await AccessManager.ExecuteSqlFileAsync(
                $"{tbc}/database/full/tbcDB.sql",
                AccessManager.ConnectionString(settings),
                _cancellationToken, downloadProgress, null, downloadSpeedProgress);

            settings.SelectedCore = Enums.Cores.CMaNGOS;
            Directory.CreateDirectory("logs/tbc");
        }

        private void ConfigureWotLKInstallation()
        {
            TrionLogger.Log("Finishing WotLK Installation");
            string wotlk = Links.Install.WotLK.Replace("/", @"\");

            settings.WotLKInstalled = true;
            settings.LaunchWotLKCore = true;
            settings.WotLKWorkingDirectory = wotlk;
            settings.WotLKLogonExeLoc = FileManager.GetExecutableLocation(wotlk, "authserver");
            settings.WotLKLogonExeName = "authserver";
            settings.WotLKWorldExeLoc = FileManager.GetExecutableLocation(wotlk, "worldserver");
            settings.WotLKLogonName = "Wrath of the Lich King Logon";
            settings.WotLKWorldName = "Wrath of the Lich King World";
            settings.SelectedCore = Enums.Cores.AzerothCore;
            Directory.CreateDirectory("logs/wotlk");
        }

        private void ConfigureCataInstallation()
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
            settings.SelectedCore = Enums.Cores.TrinityCore;
            Directory.CreateDirectory("logs/cata");
        }

        private void ConfigureMoPInstallation()
        {
            string mop = Links.Install.Mop.Replace("/", @"\");

            settings.MOPInstalled = true;
            settings.LaunchMoPCore = true;
            settings.MopWorkingDirectory = mop;
            settings.MopLogonExeLoc = FileManager.GetExecutableLocation(mop, "authserver");
            settings.MopLogonExeName = "authserver";
            settings.MopWorldExeLoc = FileManager.GetExecutableLocation(mop, "authserver");
            settings.MopWorldExeName = "worldserver";
            settings.MoPLogonName = "Mists of Pandaria Logon";
            settings.MoPWorldName = "Mists of Pandaria World";
            settings.SelectedCore = Enums.Cores.TrinityCore;
            Directory.CreateDirectory("logs/mop");
        }

        private async Task ConfigureDatabaseInstallation(IProgress<double> downloadProgress)
        {
            string database = Links.Install.Database.Replace("/", @"\");

            settings.DBInstalled = true;
            settings.DBLocation = database;
            settings.DBWorkingDir = $@"{database}\bin";
            settings.DBExeLoc = FileManager.GetExecutableLocation($@"{database}\bin", "mysqld");
            settings.DBExeName = "mysqld";

            Settings.CreateMySQLConfigFile(Directory.GetCurrentDirectory(), database);
            string sqlLocation = $@"{database}\extra\initSTDDatabase.sql";

            await Task.Delay(1000);

            // Initialize MySQL with standard database
            var initID = await AppExecuteManager.ApplicationStart(
                settings.DBExeLoc,
                settings.DBWorkingDir,
                "initialize MySQL",
                true,
                $"--initialize-insecure --init-file=\"{sqlLocation}\" --console");

            INITSpinner.Visible = true;
            LBLFilesToBeRemoved.Text = translator.Translate("LBLInitDatabaseInit");

            // Wait for initialization to complete
            while (await Task.Run(() => ServerMonitor.IsApplicationRunning(initID)))
            {
                INITSpinner.Visible = true;
                LBLFilesToBeRemoved.Text = translator.Translate("LBLInitDatabaseInit");
            }

            // Clean up downloaded archive
            File.Delete($"{settings.DBWorkingDir}/mysql-8.4.5-winx64.zip");

            // Start database server
            BTNStartDatabase_Click(null!, null!);
            Directory.CreateDirectory("logs/database");
        }

        #endregion

        #endregion

    }
}
