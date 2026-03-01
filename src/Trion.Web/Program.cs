using System.Security.Cryptography;
using System.Threading.RateLimiting;
using Microsoft.Data.Sqlite;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MudBlazor.Services;
using Trion.Core.Abstractions.Database;
using Trion.Core.Abstractions.Monitoring;
using Trion.Core.Abstractions.Services;
using Trion.Core.Abstractions.Settings;
using Trion.Core.Agent;
using Trion.Core.Auth;
using Trion.Core.Database;
using Trion.Core.Logging;
using Trion.Core.Monitoring;
using Trion.Core.Services.Ddns;
using Trion.Core.Services.Emulator;
using Trion.Core.Services.Features;
using Trion.Core.Services.Orchestration;
using Trion.Core.Services.Software;
using Trion.Core.Settings;
using Trion.Platform;
using Trion.UI.Localization;
using Trion.Web.Components;
using Trion.Web.Middleware;

var builder = WebApplication.CreateBuilder(args);

// ── Logging ────────────────────────────────────────────────────────────────
builder.Logging.ClearProviders();
builder.Logging.AddTrionLogger();
builder.Services.Configure<LoggerOptions>(
    builder.Configuration.GetSection("Logging:Trion"));

// ── Platform services (IMachineMetricsProvider, ISecretStore, IAgentClient) ─
builder.Services.AddPlatformServices();
builder.Services.Configure<AgentClientOptions>(builder.Configuration.GetSection("Agent"));

// ── Settings database ─────────────────────────────────────────────────────
var dbPath = Path.Combine(AppContext.BaseDirectory, "settings.db");
builder.Services.AddSingleton<ISettingsRepository>(
    new SettingsRepository(dbPath));
builder.Services.AddSingleton(
    new RefreshTokenStore($"Data Source={dbPath};"));

// ── Auth services ──────────────────────────────────────────────────────────
builder.Services.Configure<EmulatorAuthOptions>(
    builder.Configuration.GetSection("Auth"));
builder.Services.Configure<EmulatorConnectionOptions>(
    builder.Configuration.GetSection("Emulators"));
builder.Services.AddSingleton<IEmulatorRepositoryFactory, EmulatorRepositoryFactory>();
builder.Services.AddSingleton<AuditLogger>(sp =>
    new AuditLogger(Path.Combine(AppContext.BaseDirectory, "Audit")));
builder.Services.AddSingleton<TokenService>();
builder.Services.AddSingleton<EmulatorAuthService>();
builder.Services.AddSingleton<RoleMappingService>();
builder.Services.AddMemoryCache();
builder.Services.AddTransient<AuditMiddleware>();

// ── Monitoring pipeline ────────────────────────────────────────────────────
builder.Services
    .AddOptions<ProcessMonitorOptions>()
    .BindConfiguration("Monitoring")
    .Validate(
        o => o.RefreshInterval >= TimeSpan.FromSeconds(1),
        "Monitoring:RefreshInterval must be at least 1 second.")
    .ValidateOnStart();

builder.Services.AddSingleton<MetricsChannelAccessor>();
builder.Services.AddScoped<IMetricsChannelReader>(
    sp => sp.GetRequiredService<MetricsChannelAccessor>().CreateReader());
builder.Services.AddHostedService<MachineMetricsWorker>();
builder.Services.AddHostedService<ProcessMonitor>();

// ── Feature gate ───────────────────────────────────────────────────────────
builder.Services.AddSingleton<IFeatureGate, DefaultFeatureGate>();

// ── Emulator installer & third-party software ──────────────────────────────
builder.Services.AddSingleton<EmulatorInstaller>();
builder.Services.AddSingleton<ClientDataExtractor>();
builder.Services.AddSingleton<ThirdPartySoftwareInstaller>(sp =>
    new ThirdPartySoftwareInstaller(
        sp.GetRequiredService<IAgentClient>(),
        new HttpClient(),
        sp.GetRequiredService<AuditLogger>(),
        sp.GetRequiredService<ILogger<ThirdPartySoftwareInstaller>>()));

// ── Server Orchestration ───────────────────────────────────────────────────
// Register as singleton so the same instance is resolved via IEmulatorOrchestrator
// and also registered as IHostedService for the background health-check loop.
builder.Services.AddSingleton<EmulatorOrchestrator>();
builder.Services.AddSingleton<IEmulatorOrchestrator>(
    sp => sp.GetRequiredService<EmulatorOrchestrator>());
builder.Services.AddHostedService(
    sp => sp.GetRequiredService<EmulatorOrchestrator>());

// ── DDNS ───────────────────────────────────────────────────────────────────
builder.Services.Configure<DdnsOptions>(builder.Configuration.GetSection("Ddns"));
builder.Services.AddSingleton<IDdnsUpdater>(sp =>
    new DdnsUpdater(
        new HttpClient(),
        sp.GetRequiredService<IOptions<DdnsOptions>>(),
        sp.GetRequiredService<ISettingsRepository>(),
        sp.GetRequiredService<ILogger<DdnsUpdater>>()));
builder.Services.AddHostedService<DdnsPollingWorker>();

// ── JWT Authentication ─────────────────────────────────────────────────────
builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer           = true,
            ValidIssuer              = "trion",
            ValidateAudience         = false,
            ValidateLifetime         = true,
            ValidateIssuerSigningKey = true,
            // Key is resolved lazily from TokenService on first request
            IssuerSigningKeyResolver = (token, secToken, kid, tvp) =>
            {
                // Resolved via service locator — acceptable here since this runs per-request
                // and the alternative (circular DI) is worse.
                // The resolver is set on the configured options object before the app starts.
                return [];  // replaced in app.Use below
            }
        };
    });

// ── Authorization policies ─────────────────────────────────────────────────
builder.Services.AddAuthorizationBuilder()
    .AddPolicy("RequireGmLevel1", p => p.RequireClaim("gm_level", "1", "2", "3"))
    .AddPolicy("RequireGmLevel2", p => p.RequireClaim("gm_level", "2", "3"))
    .AddPolicy("RequireGmLevel3", p => p.RequireClaim("gm_level", "3"));

// ── Rate limiting ──────────────────────────────────────────────────────────
builder.Services.AddRateLimiter(options =>
{
    options.AddFixedWindowLimiter("auth", cfg =>
    {
        cfg.PermitLimit = 10;
        cfg.Window      = TimeSpan.FromMinutes(1);
        cfg.QueueLimit  = 0;
        cfg.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
    });
});

// ── UI & Framework services ────────────────────────────────────────────────
builder.Services.AddMudServices();
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// ── Localization ───────────────────────────────────────────────────────────
builder.Services.AddLocalization(options => options.ResourcesPath = "Localization/Resources");
builder.Services.AddSingleton<GlobalLocalizer>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers();

var supportedCultures = new[] { "en", "de", "ro" };
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    options.DefaultRequestCulture = new RequestCulture("en");
    options.AddSupportedCultures(supportedCultures);
    options.AddSupportedUICultures(supportedCultures);
    options.RequestCultureProviders.Insert(0, new CookieRequestCultureProvider
    {
        CookieName = "trion_lang"
    });
});

// ── Build ──────────────────────────────────────────────────────────────────
var app = builder.Build();

// ── Run database migrations ────────────────────────────────────────────────
{
    await using var startupConn = new SqliteConnection($"Data Source={dbPath};");
    await startupConn.OpenAsync();
    await SettingsMigrations.RunAsync(startupConn);
}

// ── Ensure bootstrap admin account exists ──────────────────────────────────
{
    var settings = app.Services.GetRequiredService<ISettingsRepository>();
    await BootstrapAccount.EnsureCreatedAsync(settings);
}

// ── Patch JWT key resolver with the real RSA public key from TokenService ──
var tokenSvc = app.Services.GetRequiredService<TokenService>();
var jwtOpts  = app.Services.GetRequiredService<IOptionsMonitor<JwtBearerOptions>>()
                            .Get(JwtBearerDefaults.AuthenticationScheme);
jwtOpts.TokenValidationParameters.IssuerSigningKeyResolver = (_, _, _, _) =>
{
    var rsa = RSA.Create();
    rsa.ImportSubjectPublicKeyInfo(
        Convert.FromBase64String(
            tokenSvc.GetPublicKeyPem()
                    .Replace("-----BEGIN PUBLIC KEY-----", string.Empty)
                    .Replace("-----END PUBLIC KEY-----", string.Empty)
                    .Replace("\n", string.Empty)
                    .Trim()),
        out _);
    return [new RsaSecurityKey(rsa)];
};

// ── Middleware pipeline (order matters) ────────────────────────────────────
var locOptions = app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>();
app.UseRequestLocalization(locOptions.Value);

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseRateLimiter();
app.UseAuthentication();
app.UseAuthorization();
app.UseAntiforgery();
app.UseMiddleware<AuditMiddleware>();
app.MapStaticAssets();

// ── API endpoints ──────────────────────────────────────────────────────────
app.MapControllers();
Trion.Web.Api.MetricsEndpoints.MapMetricsEndpoints(app);
Trion.Web.Api.AuthEndpoints.MapAuthEndpoints(app);
Trion.Web.Api.ServerControlEndpoints.MapServerControlEndpoints(app);

// ── Blazor ─────────────────────────────────────────────────────────────────
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
