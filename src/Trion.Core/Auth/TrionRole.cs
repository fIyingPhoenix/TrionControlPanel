namespace Trion.Core.Auth;

/// <summary>
/// Trion-internal roles, mapped from emulator GM levels.
/// GM level 0 = denied. Level 1–3 grant increasing Trion access.
/// </summary>
public enum TrionRole
{
    None      = 0,
    Observer  = 1,   // read-only: view metrics, sessions
    Moderator = 2,   // + kick sessions, close tickets, manage bans
    Owner     = 3    // full access including account management
}
