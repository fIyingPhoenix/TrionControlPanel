namespace Trion.Desktop.Models;

/// <summary>
/// Represents a single row in the auth database <c>realmlist</c> table.
/// </summary>
public sealed class RealmEntry
{
    public int    Id              { get; set; }
    public string Name            { get; set; } = string.Empty;
    public string Address         { get; set; } = string.Empty;
    public string LocalAddress    { get; set; } = string.Empty;
    public string LocalSubnetMask { get; set; } = "255.255.255.0";
    public int    Port            { get; set; } = 8085;
    public int    GameBuild       { get; set; } = 12340;

    public override string ToString() => $"[{Id}] {Name}  ({Address}:{Port})";
}
