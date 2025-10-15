using MudBlazor.Services;
using Trion.Core.Logging;
using Trion.Core.Monitoring;
using Trion.Web.Components;

var builder = WebApplication.CreateBuilder(args);

// bind options from appsettings.json
builder.Services.Configure<LoggerOptions>(
    builder.Configuration.GetSection("Logging:Trion"));

// register singleton logger
builder.Services.AddTrionLogger();

// Add MudBlazor services
builder.Services.AddMudServices();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.Configure<ProcessMonitorOptions>(
    builder.Configuration.GetSection("Monitoring"));
builder.Services.AddSingleton<IMachineMetricsProvider, MachineMetricsProvider>();
builder.Services.AddSingleton<ProcessStore>();
builder.Services.AddHostedService<ProcessMonitor>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
