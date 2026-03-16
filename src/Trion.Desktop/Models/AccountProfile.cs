namespace Trion.Desktop.Models;

/// <summary>
/// Full user profile returned by the Trion backend after successful login.
/// </summary>
public sealed class AccountProfile
{
    public int     Id        { get; init; }
    public string  Username  { get; init; } = "";
    public string  Email     { get; init; } = "";

    /// <summary>super_admin | admin | supporter_legend | supporter_champion | supporter_guardian | supporter | user</summary>
    public string  Role      { get; init; } = "user";

    public bool    IsActive  { get; init; }
    public bool    IsBanned  { get; init; }

    public string? LastLogin { get; init; }
    public string? CreatedAt { get; init; }

    /// <summary>64-character API key (fp_supp_… prefix).</summary>
    public string  ApiKey    { get; init; } = "";

    /// <summary>free | support | guardian | champion | legend</summary>
    public string  ApiTier   { get; init; } = "free";
}
