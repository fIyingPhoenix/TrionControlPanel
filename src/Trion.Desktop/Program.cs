using Microsoft.Extensions.DependencyInjection;
using MudBlazor.Services;
using Photino.Blazor;
using Trion.Desktop.Components;

namespace Trion.Desktop
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            var appBuilder = PhotinoBlazorAppBuilder.CreateDefault(args);

            appBuilder.Services.AddLogging();
            // de default "app" name of the file doesn't seems to work don't know why
            appBuilder.RootComponents.Add<Appli>("app"); 
            // Add MudBlazor services
            appBuilder.Services.AddMudServices();

            var app = appBuilder.Build();

            app.MainWindow.SetTitle("Photino Blazor Sample");

            AppDomain.CurrentDomain.UnhandledException += (sender, error) =>
            {
                app.MainWindow.ShowMessage("Fatal Exception", error.ExceptionObject.ToString());
            };

            app.Run();
        }
    }
}