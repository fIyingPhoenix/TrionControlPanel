using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Options;
using MudBlazor.Services;
using Trion.Core.Logging;
using Trion.Core.Monitoring;
using Trion.UI.Localization; 
using Trion.Web.Components;

var builder = WebApplication.CreateBuilder(args);

// --- Trion Core Configuration ---
builder.Services.Configure<LoggerOptions>(
    builder.Configuration.GetSection("Logging:Trion"));
builder.Services.AddTrionLogger();

builder.Services.Configure<ProcessMonitorOptions>(
    builder.Configuration.GetSection("Monitoring"));
builder.Services.AddSingleton<IMachineMetricsProvider, MachineMetricsProvider>();
builder.Services.AddSingleton<ProcessStore>();
builder.Services.AddHostedService<ProcessMonitor>();

// --- UI & Framework Services ---
builder.Services.AddMudServices();
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// --- Localization (from Trion.UI.Localization) ---
builder.Services.AddLocalization(options => options.ResourcesPath = "Localization/Resources");
builder.Services.AddSingleton<GlobalLocalizer>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers(); // required for CultureController

// match the list in LanguageSelector:
var supportedCultures = new[] { "en", "de", "ro" };

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    options.DefaultRequestCulture = new RequestCulture("en");
    options.AddSupportedCultures(supportedCultures);
    options.AddSupportedUICultures(supportedCultures);

    // Use a cookie named "trion_lang" for saving selected language
    var cookieProvider = new CookieRequestCultureProvider
    {
        CookieName = "trion_lang"
    };
    // Make the cookie provider highest priority
    options.RequestCultureProviders.Insert(0, cookieProvider);
});

// --- Build the App ---
var app = builder.Build();

// --- Middleware Order is Important ---
// Localization BEFORE routing
var locOptions = app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>();
app.UseRequestLocalization(locOptions.Value);

// Error handling
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

// Standard ASP.NET middleware
app.UseHttpsRedirection();
app.UseAntiforgery();
app.MapStaticAssets();

// --- Controllers for culture switching ---
app.MapControllers();

// --- Razor Components ---
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
