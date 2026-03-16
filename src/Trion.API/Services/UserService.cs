using Trion.API.Data;
using Trion.API.Models;

namespace Trion.API.Services;

public interface IUserService
{
    /// <summary>Verifies credentials and updates LastLogin. Returns null on failure.</summary>
    Task<User?> ValidateCredentialsAsync(string email, string password);
    Task<User?> GetByIdAsync(int id);
}

public sealed class UserService(TrionDbAccess db) : IUserService
{
    public async Task<User?> ValidateCredentialsAsync(string email, string password)
    {
        var user = await db.QuerySingleAsync<User>(TrionSql.GetUserByEmail, new { Email = email });

        if (user is null)                                             return null;
        if (!BCrypt.Net.BCrypt.Verify(password, user.PasswordHash)) return null;
        if (!user.IsActive || user.IsBanned)                         return null;

        await db.ExecuteAsync(TrionSql.UpdateLastLogin, new { user.ID });
        return user;
    }

    public Task<User?> GetByIdAsync(int id)
        => db.QuerySingleAsync<User>(TrionSql.GetUserById, new { Id = id });
}
