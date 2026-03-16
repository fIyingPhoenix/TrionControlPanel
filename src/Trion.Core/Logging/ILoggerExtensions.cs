using Microsoft.Extensions.Logging;

namespace Trion.Core.Logging;

/// <summary>
/// Convenience extensions on <see cref="ILogger"/> that mirror the old
/// <c>ITrionLogger.LogException</c> signature so call-sites need minimal changes.
/// </summary>
public static class ILoggerExtensions
{
    /// <summary>
    /// Logs <paramref name="exception"/> at Error level, optionally prefixed with a
    /// <paramref name="context"/> string (e.g. the calling method or service name).
    /// </summary>
    public static void LogException(this ILogger logger, Exception exception, string context = "")
    {
        if (string.IsNullOrEmpty(context))
            logger.LogError(exception, "{Message}", exception.Message);
        else
            logger.LogError(exception, "[{Context}] {Message}", context, exception.Message);
    }
}
