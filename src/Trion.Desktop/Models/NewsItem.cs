using System.Text.Json.Serialization;

namespace Trion.Desktop.Models;

public sealed class NewsItem
{
    [JsonPropertyName("id")]
    public int    Id          { get; init; }

    [JsonPropertyName("title")]
    public string Title       { get; init; } = "";

    [JsonPropertyName("summary")]
    public string Summary     { get; init; } = "";

    [JsonPropertyName("category")]
    public string Category    { get; init; } = "";

    [JsonPropertyName("published_at")]
    public string PublishedAt { get; init; } = "";

    [JsonPropertyName("url")]
    public string? Url        { get; init; }
}
