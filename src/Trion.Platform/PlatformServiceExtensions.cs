using System.Runtime.InteropServices;
using Microsoft.Extensions.DependencyInjection;
using Trion.Core.Abstractions.Monitoring;
using Trion.Core.Abstractions.Settings;
using Trion.Core.Agent;
using Trion.Platform.Shared;

namespace Trion.Platform;

public static class PlatformServiceExtensions
{
    /// <summary>
    /// Single point of OS detection. Registers all platform-specific
    /// implementations against their Core interfaces.
    /// This is the ONLY place in the solution where RuntimeInformation.IsOSPlatform() is called.
    /// </summary>
    public static IServiceCollection AddPlatformServices(this IServiceCollection services)
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            services.AddSingleton<Windows.WindowsDiskReader>();
            services.AddSingleton<Windows.WindowsNetworkReader>();
            services.AddSingleton<IMachineMetricsProvider, Windows.WindowsMetricsProvider>();
            services.AddSingleton<ISecretStore, Windows.WindowsSecretStore>();
        }
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            services.AddSingleton<Linux.LinuxDiskReader>();
            services.AddSingleton<Linux.LinuxNetworkReader>();
            services.AddSingleton<Linux.LinuxRamReader>();
            services.AddSingleton<IMachineMetricsProvider, Linux.LinuxMetricsProvider>();
            services.AddSingleton<ISecretStore, Linux.LinuxSecretStore>();
        }
        else
        {
            throw new PlatformNotSupportedException(
                "Trion Control Panel supports Windows and Linux only.");
        }

        // AgentClient: platform-agnostic IPC wrapper (stub until Milestone 5)
        services.AddSingleton<IAgentClient, AgentClient>();

        return services;
    }
}
