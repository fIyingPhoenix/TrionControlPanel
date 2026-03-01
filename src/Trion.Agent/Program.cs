using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Trion.Agent;
using Trion.Agent.Handlers;
using Trion.Agent.IPC;
using Trion.Agent.Security;

// ── Integrity check must run before any other code ───────────────────────────
AgentIntegrityGuard.Check();

// ── Host ──────────────────────────────────────────────────────────────────────
var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((ctx, services) =>
    {
        services.Configure<AgentOptions>(ctx.Configuration.GetSection("Agent"));

        // Security
        services.AddSingleton<HmacValidator>();
        services.AddSingleton<CommandAllowlist>();

        // Command handlers
        services.AddSingleton<ProcessLaunchHandler>();
        services.AddSingleton<ServiceControlHandler>();
        services.AddSingleton<ProcessKillHandler>();

        // IPC
        services.AddSingleton<CommandDispatcher>();
        services.AddHostedService<AgentPipeServer>();
    })
    .Build();

await host.RunAsync();
