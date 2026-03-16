using Microsoft.AspNetCore.Mvc;
using Trion.API.Services;

namespace Trion.API.Endpoints;

public static class NewsEndpoints
{
    public static WebApplication MapNewsEndpoints(this WebApplication app)
    {
        app.MapGet("/Trion/news", GetNews)
           .WithTags("News")
           .CacheOutput(p => p.Expire(TimeSpan.FromMinutes(5)));

        return app;
    }

    // ── GET /Trion/news?limit=6 ───────────────────────────────────────────────

    private static async Task<IResult> GetNews(
        [FromQuery] int  limit,
        INewsService     news)
    {
        limit = Math.Clamp(limit <= 0 ? 6 : limit, 1, 50);

        var items = await news.GetNewsAsync(limit);

        return Results.Ok(items.Select(n => new
        {
            id          = n.ID,
            title       = n.Title,
            summary     = n.Summary,
            category    = n.Category,
            publishedAt = n.PublishedAt.ToString("yyyy-MM-dd"),
            url         = n.Url,
        }));
    }
}
