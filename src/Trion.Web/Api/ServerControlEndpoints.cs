using Microsoft.AspNetCore.Http.HttpResults;
using Trion.Core.Abstractions.Services;

namespace Trion.Web.Api;

public static class ServerControlEndpoints
{
    public static IEndpointRouteBuilder MapServerControlEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/server")
            .WithTags("Server Control")
            .RequireAuthorization("RequireGmLevel2");

        // POST /api/server/{id}/start  — body: EmulatorProfile JSON
        group.MapPost("/{id}/start", StartServer)
            .WithName("StartServer")
            .WithSummary("Registers and starts an emulator profile.")
            .Accepts<EmulatorProfile>("application/json");

        // POST /api/server/{id}/stop
        group.MapPost("/{id}/stop", StopServer)
            .WithName("StopServer")
            .WithSummary("Stops a running server profile.");

        // POST /api/server/{id}/restart
        group.MapPost("/{id}/restart", RestartServer)
            .WithName("RestartServer")
            .WithSummary("Restarts a server profile (uses the last known EmulatorProfile).");

        // GET /api/server/{id}/status
        group.MapGet("/{id}/status", GetStatus)
            .WithName("GetServerStatus")
            .WithSummary("Returns the orchestrator status for a specific profile.")
            .Produces<OrchestratorStatus>(StatusCodes.Status200OK);

        // GET /api/server/statuses
        group.MapGet("/statuses", GetAllStatuses)
            .WithName("GetAllServerStatuses")
            .WithSummary("Returns orchestrator statuses for all registered profiles.")
            .Produces<IReadOnlyList<OrchestratorStatus>>(StatusCodes.Status200OK);

        return app;
    }

    private static async Task<Ok> StartServer(
        string id,
        EmulatorProfile profile,
        IEmulatorOrchestrator orchestrator,
        CancellationToken ct)
    {
        await orchestrator.StartAsync(profile, ct);
        return TypedResults.Ok();
    }

    private static async Task<Ok> StopServer(
        string id,
        IEmulatorOrchestrator orchestrator,
        CancellationToken ct)
    {
        await orchestrator.StopAsync(id, ct);
        return TypedResults.Ok();
    }

    private static async Task<Ok> RestartServer(
        string id,
        IEmulatorOrchestrator orchestrator,
        CancellationToken ct)
    {
        await orchestrator.RestartAsync(id, ct);
        return TypedResults.Ok();
    }

    private static async Task<Ok<OrchestratorStatus>> GetStatus(
        string id,
        IEmulatorOrchestrator orchestrator)
    {
        var status = await orchestrator.GetStatusAsync(id);
        return TypedResults.Ok(status);
    }

    private static Ok<IReadOnlyList<OrchestratorStatus>> GetAllStatuses(
        IEmulatorOrchestrator orchestrator) =>
        TypedResults.Ok(orchestrator.GetAllStatuses());
}
