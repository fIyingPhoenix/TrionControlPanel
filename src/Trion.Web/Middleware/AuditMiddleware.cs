using System.Security.Claims;
using Trion.Core.Logging;

namespace Trion.Web.Middleware;

public sealed class AuditMiddleware : IMiddleware
{
    private readonly AuditLogger _audit;

    public AuditMiddleware(AuditLogger audit)
    {
        _audit = audit;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        await next(context);

        // Only audit mutating API calls after the response is finalized
        var method = context.Request.Method;
        var path   = context.Request.Path.Value ?? string.Empty;

        if (!path.StartsWith("/api/", StringComparison.OrdinalIgnoreCase))
            return;

        if (method is not ("POST" or "PUT" or "PATCH" or "DELETE"))
            return;

        context.Response.OnCompleted(() =>
        {
            var username = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var ip       = GetClientIp(context);
            var status   = context.Response.StatusCode.ToString();

            _audit.Record(new AuditEvent(
                Timestamp: DateTimeOffset.UtcNow,
                Username:  username,
                IpAddress: ip,
                Action:    $"{method} {path}",
                Result:    status));

            return Task.CompletedTask;
        });
    }

    private static string GetClientIp(HttpContext context)
    {
        // Honour X-Forwarded-For when behind Nginx reverse proxy
        var forwarded = context.Request.Headers["X-Forwarded-For"].FirstOrDefault();
        if (!string.IsNullOrEmpty(forwarded))
            return forwarded.Split(',')[0].Trim();

        return context.Connection.RemoteIpAddress?.ToString() ?? "unknown";
    }
}
