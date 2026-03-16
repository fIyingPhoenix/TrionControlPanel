namespace Trion.API.Data;

internal static class TrionSql
{
    // ── Users ─────────────────────────────────────────────────────────────────

    internal const string GetUserByEmail = """
        SELECT ID, Username, Email, PasswordHash, Role, IsActive, IsBanned,
               ApiKey, ApiTier, LastLogin, CreatedAt
        FROM   Users
        WHERE  Email = @Email
        LIMIT  1
        """;

    internal const string GetUserById = """
        SELECT ID, Username, Email, PasswordHash, Role, IsActive, IsBanned,
               ApiKey, ApiTier, LastLogin, CreatedAt
        FROM   Users
        WHERE  ID = @Id
        LIMIT  1
        """;

    internal const string UpdateLastLogin = """
        UPDATE Users SET LastLogin = UTC_TIMESTAMP() WHERE ID = @Id
        """;

    // ── Supporter keys ────────────────────────────────────────────────────────

    internal const string GetKeyExists = """
        SELECT EXISTS(SELECT 1 FROM SupporterKey WHERE ApiKey = @ApiKey)
        """;

    internal const string InsertSupporterKey = """
        INSERT INTO SupporterKey (ApiKey, UID) VALUES (@ApiKey, @UID)
        """;

    // ── Supporters ────────────────────────────────────────────────────────────

    internal const string GetSupporters = """
        SELECT u.Username, u.ApiTier,
               DATE_FORMAT(u.CreatedAt, '%Y-%m-%d') AS JoinedAt
        FROM   Users u
        INNER  JOIN SupporterKey sk ON sk.UID = u.ID
        WHERE  u.IsActive = 1 AND u.IsBanned = 0
        ORDER  BY FIELD(u.ApiTier, 'legend','champion','guardian','support'),
                  u.Username ASC
        """;

    // ── News ──────────────────────────────────────────────────────────────────

    internal const string GetNews = """
        SELECT ID, Title, Summary, Category, PublishedAt, Url
        FROM   News
        WHERE  IsPublished = 1
        ORDER  BY PublishedAt DESC
        LIMIT  @Limit
        """;

    // ── Downloads ─────────────────────────────────────────────────────────────

    internal const string GetDownloads = """
        SELECT Name, Version, Description, Url, UpdatedAt
        FROM   Downloads
        WHERE  IsAvailable = 1
        ORDER  BY SortOrder ASC
        """;
}
