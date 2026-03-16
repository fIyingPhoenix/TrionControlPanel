using System.Text.Json.Serialization;

namespace Trion.Desktop.Models;

public sealed class EmulatorVersions
{
    [JsonPropertyName("classicSPP")] public string Classic { get; init; } = "—";
    [JsonPropertyName("tbcSPP")]     public string Tbc     { get; init; } = "—";
    [JsonPropertyName("wotlkSPP")]   public string Wotlk   { get; init; } = "—";
    [JsonPropertyName("cataSPP")]    public string Cata    { get; init; } = "—";
    [JsonPropertyName("mopSPP")]     public string Mop     { get; init; } = "—";
}
