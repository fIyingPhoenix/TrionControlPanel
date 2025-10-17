using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Trion.Core.Logging;

public static class LoggerExtensions
{
    public static IServiceCollection AddTrionLogger(
        this IServiceCollection services,
        Action<LoggerOptions>? configure = null)
    {
        if (configure != null) services.Configure(configure);
        return services.AddSingleton(sp =>
        {
            var opts = sp.GetRequiredService<IOptions<LoggerOptions>>().Value;
            return new TrionLogger(opts);
        });
    }
}