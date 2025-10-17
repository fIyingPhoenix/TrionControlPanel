using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MudBlazor.Services;
using System.Globalization;
using Trion.Core.Logging;
using Trion.Core.Monitoring;
using Trion.UI.Localization;

namespace Trion.Desktop
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder.UseMauiApp<App>()
                   .ConfigureFonts(fonts =>
                   {
                       fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                   });

            builder.Services.AddMudServices();
            builder.Services.AddMauiBlazorWebView();

#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
            builder.Logging.AddDebug();
#endif

            /* JSON config */
            builder.Configuration.AddJsonFile("appsettings.json", optional: true);

            /* Logger */
            builder.Services.AddOptions<LoggerOptions>()
                            .Bind(builder.Configuration.GetSection("Logging:Trion"));
            builder.Services.AddTrionLogger();

            /* Monitoring */
            builder.Services.AddOptions<ProcessMonitorOptions>()
                            .Bind(builder.Configuration.GetSection("Monitoring"));
            builder.Services.AddSingleton<IMachineMetricsProvider, MachineMetricsProvider>();
            builder.Services.AddSingleton<ProcessStore>();
            builder.Services.AddHostedService<ProcessMonitor>();

            /* Localization */
            builder.Services.AddLocalization(options => options.ResourcesPath = "Localization/Resources");
            builder.Services.AddSingleton<GlobalLocalizer>();

            // Load the last selected language (saved by LanguageSelector)
            var savedLang = Preferences.Get("trion_lang", "en");
            var culture = new CultureInfo(savedLang);

            CultureInfo.CurrentCulture = culture;
            CultureInfo.CurrentUICulture = culture;
            CultureInfo.DefaultThreadCurrentCulture = culture;
            CultureInfo.DefaultThreadCurrentUICulture = culture;

            return builder.Build();
        }
    }
}
