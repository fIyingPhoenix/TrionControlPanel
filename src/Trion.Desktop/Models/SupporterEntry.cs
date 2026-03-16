using System.Text.Json.Serialization;

namespace Trion.Desktop.Models;

public sealed record SupporterEntry(
    [property: JsonPropertyName("username")]       string Username,
    [property: JsonPropertyName("role")]           string Role,
    [property: JsonPropertyName("tier")]           string Tier,
    [property: JsonPropertyName("tierName")]       string TierName,
    [property: JsonPropertyName("tierColor")]      string TierColor,
    [property: JsonPropertyName("supporterSince")] string SupporterSince
);
