namespace Trion.API.Models;

public sealed class NewsItem
{
    public int      ID          { get; init; }
    public string   Title       { get; init; } = "";
    public string   Summary     { get; init; } = "";
    public string   Category    { get; init; } = "";
    public DateTime PublishedAt { get; init; }
    public string?  Url         { get; init; }
}
