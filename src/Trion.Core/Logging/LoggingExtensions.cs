using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Trion.Core.Logging;

public static class LoggingExtensions
{
    /// <summary>
    /// Registers <see cref="TrionLogger"/> as the active <see cref="ILoggerProvider"/>.
    /// Call from <c>ILoggingBuilder</c> inside <c>Host.CreateDefaultBuilder</c>.
    /// </summary>
    public static ILoggingBuilder AddTrionLogger(this ILoggingBuilder builder)
    {
        builder.Services.AddSingleton<TrionLogger>();
        builder.Services.AddSingleton<ILoggerProvider>(sp => sp.GetRequiredService<TrionLogger>());
        return builder;
    }
}
