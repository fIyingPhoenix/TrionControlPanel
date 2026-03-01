namespace Trion.Core.Auth;

public sealed class EmulatorAuthOptions
{
    /// <summary>
    /// Number of failed login attempts before the account is locked out.
    /// </summary>
    public int MaxFailedAttempts { get; set; } = 5;

    /// <summary>
    /// How long the lockout lasts after hitting MaxFailedAttempts.
    /// </summary>
    public TimeSpan LockoutDuration { get; set; } = TimeSpan.FromMinutes(15);
}
