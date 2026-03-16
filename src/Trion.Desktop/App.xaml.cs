using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Windows;
using Trion.Core.Logging;
using Trion.Desktop.Infrastructure.Extensions;
using Trion.Desktop.Models;
using Trion.Desktop.Services;
using Trion.Desktop.ViewModels;
using Trion.Desktop.Views;
#pragma warning disable CA2000 // services are disposed by the host

namespace Trion.Desktop;

public partial class App : Application
{
    public static IHost AppHost { get; private set; } = null!;

    protected override async void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        // Prevent WPF from shutting down when the login window closes before
        // the main window has been shown.  We call Shutdown() explicitly in OnExit.
        ShutdownMode = ShutdownMode.OnExplicitShutdown;

        AppHost = Host.CreateDefaultBuilder()
            .ConfigureLogging(logging =>
            {
                logging.ClearProviders();
                logging.AddTrionLogger();
            })
            .ConfigureServices((_, services) =>
            {
                // Core services
                services.AddSingleton<ISettingsService, SettingsService>();
                services.AddSingleton<IThemeService, ThemeService>();
                services.AddSingleton<ILocalizationService, LocalizationService>();
                services.AddSingleton<INavigationService, NavigationService>();
                services.AddSingleton<INotificationService, NotificationService>();
                services.AddSingleton<IMySqlSetupService, MySqlSetupService>();
                services.AddSingleton<IApiKeyService, ApiKeyService>();
                services.AddSingleton<IAccountService, AccountService>();
                services.AddSingleton<INewsService, NewsService>();
                services.AddSingleton<ISupportersService, SupportersService>();
                services.AddSingleton<IEmulatorInstallerService, EmulatorInstallerService>();
                services.AddSingleton<INetworkService, NetworkService>();
                services.AddSingleton<IDdnsUpdateService, DdnsUpdateService>();
                services.AddSingleton<IDatabaseService, DatabaseService>();

                // System metrics — singleton shared between hosted service lifecycle and DashboardViewModel
                services.AddSingleton<SystemMetricsService>();
                services.AddSingleton<ISystemMetricsService>(sp => sp.GetRequiredService<SystemMetricsService>());
                services.AddHostedService(sp => sp.GetRequiredService<SystemMetricsService>());

                // Register all ViewModels, Views, and Services via extension
                services.RegisterViewModels();
                services.RegisterViews();
                services.RegisterTrayService();
            })
            .Build();

        await AppHost.StartAsync();

        var logger = AppHost.Services.GetRequiredService<ILogger<App>>();
        logger.LogInformation("Application started. OS: {OS}  CLR: {CLR}", Environment.OSVersion, Environment.Version);

        // Load settings and apply theme/language
        var settings = AppHost.Services.GetRequiredService<ISettingsService>();
        settings.Load();
        logger.LogInformation("Settings loaded. Theme: {Theme}  Lang: {Lang}", settings.Current.Theme, settings.Current.Language);

        // Initialise the API key system: load cached guest key or register a new install
        var apiKeys = AppHost.Services.GetRequiredService<IApiKeyService>();
        _ = Task.Run(async () =>
        {
            try   { await apiKeys.InitialiseAsync(); }
            catch (Exception ex) { logger.LogException(ex, "App.ApiKeyInit"); }
        });

        var theme = AppHost.Services.GetRequiredService<IThemeService>();
        theme.SetTheme(settings.Current.Theme);

        var loc = AppHost.Services.GetRequiredService<ILocalizationService>();
        loc.LoadLanguage(settings.Current.Language);

        // Show login window if the user has no saved session and hasn't chosen guest mode
        var account = AppHost.Services.GetRequiredService<IAccountService>();
        if (!account.IsLoggedIn && !account.IsGuest)
        {
            var loginWindow = AppHost.Services.GetRequiredService<LoginWindow>();
            loginWindow.ShowDialog();   // blocks until user logs in or skips
        }

        var tray = AppHost.Services.GetRequiredService<ITrayService>();

        var window = AppHost.Services.GetRequiredService<MainWindow>();
        tray.Initialize(window);
        window.Closed += (_, _) => Shutdown();

        // Keep the tray/window icon in sync with the active theme accent colour
        theme.AccentChanged += color =>
            tray.SetAccentColor(System.Drawing.Color.FromArgb(color.A, color.R, color.G, color.B));

        // Apply the current accent immediately (theme was already set above)
        if (Application.Current.Resources["Accent.Primary"] is System.Windows.Media.SolidColorBrush accentBrush)
        {
            var c = accentBrush.Color;
            tray.SetAccentColor(System.Drawing.Color.FromArgb(c.A, c.R, c.G, c.B));
        }
        window.Show();

        // MySQL detection / installation runs on a background thread after the
        // window is visible.  Progress is broadcast via IMySqlSetupService.ProgressChanged;
        // DashboardViewModel subscribes and shows the banner + event log entries.
        var mysql = AppHost.Services.GetRequiredService<IMySqlSetupService>();
        _ = Task.Run(async () =>
        {
            try   { await mysql.RunStartupCheckAsync(); }
            catch (Exception ex)
            { logger.LogException(ex, "App.MySqlStartup"); }
        });
    }

    protected override async void OnExit(ExitEventArgs e)
    {
        // Dispose the tray icon immediately so it doesn't linger in the taskbar
        AppHost.Services.GetRequiredService<ITrayService>().Dispose();

        var logger = AppHost.Services.GetRequiredService<ILogger<App>>();
        logger.LogInformation("Application shutdown initiated.");

        // Gracefully stop MySQL before tearing down the host
        var mysql = AppHost.Services.GetRequiredService<IMySqlSetupService>();
        try { await mysql.StopAsync(); }
        catch (Exception ex) { logger.LogException(ex, "App.OnExit.StopMySQL"); }

        var settings = AppHost.Services.GetRequiredService<ISettingsService>();

        // If the user didn't choose "Remember me", wipe the session token so
        // they are prompted to sign in again on next launch (drops to guest mode).
        var cfg = settings.Current;
        if (!string.IsNullOrEmpty(cfg.AccountToken) && !cfg.RememberMe)
        {
            cfg.AccountToken    = "";
            cfg.LoginSkipped    = true;   // return to guest on next launch
        }

        settings.Save();
        logger.LogInformation("Settings saved. Shutting down host.");

        await AppHost.StopAsync();
        AppHost.Dispose();
        base.OnExit(e);
    }
}
