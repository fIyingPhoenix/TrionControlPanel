using Microsoft.AspNetCore.Http.HttpResults;
using Trion.Core.Monitoring;

namespace Trion.Web.Api;

public static class MetricsEndpoints
{
    public static IEndpointRouteBuilder MapMetricsEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/metrics")
            .WithTags("Metrics");
        // TODO (M3): add .RequireAuthorization("RequireGmLevel1") once auth is wired

        group.MapGet("/machine", GetMachineMetrics)
            .WithName("GetMachineMetrics")
            .WithSummary("Returns the latest machine metrics snapshot.")
            .Produces<MachineMetrics>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status401Unauthorized);

        group.MapGet("/processes", GetProcessMetrics)
            .WithName("GetProcessMetrics")
            .WithSummary("Returns the latest tracked process metrics.")
            .Produces<ProcessMetrics[]>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status401Unauthorized);

        return app;
    }

    private static Results<Ok<MachineMetrics>, NoContent> GetMachineMetrics(
        MetricsChannelAccessor accessor)
    {
        var last = accessor.LastMachine;
        return last is not null
            ? TypedResults.Ok(last)
            : TypedResults.NoContent();
    }

    private static Results<Ok<ProcessMetrics[]>, NoContent> GetProcessMetrics(
        MetricsChannelAccessor accessor)
    {
        var last = accessor.LastProcess;
        return last is not null
            ? TypedResults.Ok(last)
            : TypedResults.NoContent();
    }
}
