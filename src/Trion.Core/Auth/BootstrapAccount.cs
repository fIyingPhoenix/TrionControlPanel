using Trion.Core.Abstractions.Settings;

namespace Trion.Core.Auth;

public static class BootstrapAccount
{
    private const string UsernameKey     = "bootstrap_username";
    private const string PasswordHashKey = "bootstrap_password_hash";

    public const string DefaultUsername = "admin";

    /// <summary>
    /// Ensures a bootstrap (Trion-native Owner) account exists in settings.
    /// Called once at application startup before the web host begins accepting requests.
    /// </summary>
    public static async Task EnsureCreatedAsync(
        ISettingsRepository settings,
        string? initialPassword = null,
        CancellationToken ct = default)
    {
        var existing = await settings.GetAsync<string>(UsernameKey, ct);
        if (existing is not null)
            return;

        var password = initialPassword ?? GenerateTemporaryPassword();
        var hash     = BCrypt.Net.BCrypt.HashPassword(password, workFactor: 12);

        await settings.SetAsync(UsernameKey, DefaultUsername, ct);
        await settings.SetAsync(PasswordHashKey, hash, ct);

        // Write the temporary password to a first-run file so the operator can retrieve it
        var firstRunPath = Path.Combine(AppContext.BaseDirectory, "FIRST_RUN_PASSWORD.txt");
        await File.WriteAllTextAsync(firstRunPath,
            $"Trion bootstrap account created.\r\n" +
            $"Username: {DefaultUsername}\r\n" +
            $"Password: {password}\r\n" +
            $"Delete this file after logging in.\r\n", ct);
    }

    public static async Task<bool> ValidateAsync(
        ISettingsRepository settings,
        string username,
        string password,
        CancellationToken ct = default)
    {
        var storedUsername = await settings.GetAsync<string>(UsernameKey, ct);
        if (storedUsername is null || !storedUsername.Equals(username, StringComparison.Ordinal))
            return false;

        var storedHash = await settings.GetAsync<string>(PasswordHashKey, ct);
        if (storedHash is null)
            return false;

        return BCrypt.Net.BCrypt.Verify(password, storedHash);
    }

    private static string GenerateTemporaryPassword()
    {
        const string chars = "ABCDEFGHJKLMNPQRSTUVWXYZabcdefghjkmnpqrstuvwxyz23456789!@#$";
        return System.Security.Cryptography.RandomNumberGenerator.GetString(chars, 16);
    }
}
