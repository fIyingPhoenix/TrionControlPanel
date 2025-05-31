using Microsoft.AspNetCore.HttpOverrides;
using TrionControlPanel.API.Classes;
using TrionControlPanel.API.Classes.Database;
using MudBlazor.Services;
using TrionControlPanel.API.Components;

namespace TrionControlPanel.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Razor Components (Blazor) with interactive server mode
            builder.Services.AddRazorComponents()
                            .AddInteractiveServerComponents();

            // MudBlazor
            builder.Services.AddMudServices();

            // Add standard services
            builder.Services.AddMemoryCache();
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // App-specific services
            builder.Services.AddSingleton<DatabaseManager>();
            builder.Services.AddSingleton<AccessManager>();
            builder.Services.AddSingleton<SqlQueryManager>();
            builder.Services.AddSingleton<Network>();

            var app = builder.Build();

            // Forward headers if behind a proxy (like Nginx)
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            // Swagger in all environments
            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            // Add Anti-forgery middleware (required for interactive Razor components)
            app.UseAntiforgery();

            app.MapControllers();

            app.MapStaticAssets();

            // Razor Components entry point
            app.MapRazorComponents<App>()
               .AddInteractiveServerRenderMode();

            app.Run();
        }
    }
}
