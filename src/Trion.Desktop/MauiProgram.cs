using Microsoft.Extensions.Logging;
using MudBlazor.Services;
using Trion.Core.Logging;

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

            // Add MudBlazor services
            builder.Services.AddMudServices();

            builder.Services.AddMauiBlazorWebView();

#if DEBUG
    		builder.Services.AddBlazorWebViewDeveloperTools();
    		builder.Logging.AddDebug();
#endif
            builder.Services.AddSingleton(new LoggerOptions
            {
                Folder = Path.Combine(FileSystem.AppDataDirectory, "Logs"),
                WriteToConsole = true,   
                WriteToFile = true,   
                ShowInfo = true,
                ShowSuccess = true,
                ShowWarning = true,
                ShowError = true
            });

            builder.Services.AddTrionLogger();

            return builder.Build();
        }
    }
}
