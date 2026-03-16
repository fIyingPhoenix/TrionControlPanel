using Trion.API.Data;

namespace Trion.API.Endpoints;

public static class InfoEndpoints
{
    public static WebApplication MapInfoEndpoints(this WebApplication app)
    {
        var g = app.MapGroup("/Trion").WithTags("Info");

        g.MapGet("updates",    GetUpdates).CacheOutput(p => p.Expire(TimeSpan.FromMinutes(10)));
        g.MapGet("downloads",  GetDownloads).CacheOutput(p => p.Expire(TimeSpan.FromMinutes(5)));
        g.MapGet("supporters", GetSupporters).CacheOutput(p => p.Expire(TimeSpan.FromMinutes(10)));

        return app;
    }

    // ── GET /Trion/updates ────────────────────────────────────────────────────

    private static IResult GetUpdates(IConfiguration cfg) =>
        Results.Ok(new
        {
            version      = cfg["trion:Version:Default"]  ?? "",
            releaseUrl   = cfg["trion:ReleaseUrl"]       ?? "",
            changelogUrl = cfg["trion:ChangelogUrl"]     ?? "",
        });

    // ── GET /Trion/downloads ──────────────────────────────────────────────────

    private static async Task<IResult> GetDownloads(TrionDbAccess db)
    {
        var items = await db.QueryAsync<dynamic>(TrionSql.GetDownloads);
        return Results.Ok(items);
    }

    // ── GET /Trion/supporters ─────────────────────────────────────────────────

    private static async Task<IResult> GetSupporters(TrionDbAccess db)
    {
        var rows = await db.QueryAsync<dynamic>(TrionSql.GetSupporters);
        return Results.Ok(rows);
    }
}
