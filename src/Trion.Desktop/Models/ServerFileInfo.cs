using System.Text.Json.Serialization;

namespace Trion.Desktop.Models;

/// <summary>Entry in the server repair manifest returned by /Trion/RepairSPP.</summary>
public sealed class ServerFileInfo
{
    [JsonPropertyName("name")] public string Name { get; init; } = "";
    [JsonPropertyName("size")] public long   Size { get; init; }
    [JsonPropertyName("hash")] public string Hash { get; init; } = "";
}
