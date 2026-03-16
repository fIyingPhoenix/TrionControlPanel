namespace Trion.Desktop.Infrastructure.Constants;

/// <summary>
/// SQL executed once by <c>MySqlSetupService</c> immediately after
/// <c>mysqld --initialize-insecure</c> creates the blank data directory.
///
/// At this point root has no password (insecure init), so mysql.exe connects
/// as root without a password and runs every statement in order:
///   1. Secure the root account with a strong password.
///   2. Drop / recreate the 'phoenix' application user.
///   3. Create all expansion databases (Classic → MoP).
///   4. Grant phoenix full ownership of every database.
/// </summary>
internal static class MySqlInitSql
{
    /// <summary>
    /// Password assigned to <c>root@localhost</c> during first-run setup.
    /// Used by <see cref="DatabaseService.PingAsync"/> to verify MySQL connectivity
    /// with known-good credentials regardless of the application user configuration.
    /// </summary>
    public const string RootPassword = "FlyingPhoenix";

    /// <summary>
    /// Full first-run initialization script.
    /// Run via <c>mysql.exe --user=root --host=127.0.0.1 --port=3306 --protocol=TCP</c>
    /// with this text piped to stdin.
    /// </summary>
    public const string FirstRunSetup = """
        -- ── Root account ──────────────────────────────────────────────────────
        ALTER USER 'root'@'localhost' IDENTIFIED WITH 'caching_sha2_password' BY 'FlyingPhoenix';

        -- ── Application user ───────────────────────────────────────────────────
        DROP USER IF EXISTS 'phoenix'@'localhost';
        CREATE USER 'phoenix'@'localhost'
            IDENTIFIED WITH 'caching_sha2_password' BY 'phoenix'
            WITH MAX_QUERIES_PER_HOUR 0
                 MAX_CONNECTIONS_PER_HOUR 0
                 MAX_UPDATES_PER_HOUR 0;
        GRANT ALL PRIVILEGES ON *.* TO 'phoenix'@'localhost' WITH GRANT OPTION;

        -- ── Classic databases ──────────────────────────────────────────────────
        CREATE DATABASE `classic_world`      DEFAULT CHARACTER SET UTF8MB4 COLLATE utf8mb4_unicode_ci;
        CREATE DATABASE `classic_characters` DEFAULT CHARACTER SET UTF8MB4 COLLATE utf8mb4_unicode_ci;
        CREATE DATABASE `classic_auth`       DEFAULT CHARACTER SET UTF8MB4 COLLATE utf8mb4_unicode_ci;
        CREATE DATABASE `classic_logs`       DEFAULT CHARACTER SET UTF8MB4 COLLATE utf8mb4_general_ci;

        GRANT ALL PRIVILEGES ON `classic_world`      .* TO 'phoenix'@'localhost' WITH GRANT OPTION;
        GRANT ALL PRIVILEGES ON `classic_characters` .* TO 'phoenix'@'localhost' WITH GRANT OPTION;
        GRANT ALL PRIVILEGES ON `classic_auth`       .* TO 'phoenix'@'localhost' WITH GRANT OPTION;
        GRANT ALL PRIVILEGES ON `classic_logs`       .* TO 'phoenix'@'localhost' WITH GRANT OPTION;

        -- ── TBC databases ──────────────────────────────────────────────────────
        CREATE DATABASE `tbc_world`      DEFAULT CHARACTER SET UTF8MB4 COLLATE utf8mb4_unicode_ci;
        CREATE DATABASE `tbc_characters` DEFAULT CHARACTER SET UTF8MB4 COLLATE utf8mb4_unicode_ci;
        CREATE DATABASE `tbc_auth`       DEFAULT CHARACTER SET UTF8MB4 COLLATE utf8mb4_unicode_ci;
        CREATE DATABASE `tbc_logs`       DEFAULT CHARACTER SET UTF8MB4 COLLATE utf8mb4_general_ci;

        GRANT ALL PRIVILEGES ON `tbc_world`      .* TO 'phoenix'@'localhost' WITH GRANT OPTION;
        GRANT ALL PRIVILEGES ON `tbc_characters` .* TO 'phoenix'@'localhost' WITH GRANT OPTION;
        GRANT ALL PRIVILEGES ON `tbc_auth`       .* TO 'phoenix'@'localhost' WITH GRANT OPTION;
        GRANT ALL PRIVILEGES ON `tbc_logs`       .* TO 'phoenix'@'localhost' WITH GRANT OPTION;

        -- ── WotLK databases ────────────────────────────────────────────────────
        CREATE DATABASE `wotlk_world`      DEFAULT CHARACTER SET UTF8MB4 COLLATE utf8mb4_unicode_ci;
        CREATE DATABASE `wotlk_characters` DEFAULT CHARACTER SET UTF8MB4 COLLATE utf8mb4_unicode_ci;
        CREATE DATABASE `wotlk_auth`       DEFAULT CHARACTER SET UTF8MB4 COLLATE utf8mb4_unicode_ci;
        CREATE DATABASE `wotlk_playerbots` DEFAULT CHARACTER SET UTF8MB4 COLLATE utf8mb4_general_ci;

        GRANT ALL PRIVILEGES ON `wotlk_world`      .* TO 'phoenix'@'localhost' WITH GRANT OPTION;
        GRANT ALL PRIVILEGES ON `wotlk_characters` .* TO 'phoenix'@'localhost' WITH GRANT OPTION;
        GRANT ALL PRIVILEGES ON `wotlk_auth`       .* TO 'phoenix'@'localhost' WITH GRANT OPTION;
        GRANT ALL PRIVILEGES ON `wotlk_playerbots` .* TO 'phoenix'@'localhost' WITH GRANT OPTION;

        -- ── Cataclysm databases ────────────────────────────────────────────────
        CREATE DATABASE `cata_world`      DEFAULT CHARACTER SET UTF8MB4 COLLATE utf8mb4_unicode_ci;
        CREATE DATABASE `cata_characters` DEFAULT CHARACTER SET UTF8MB4 COLLATE utf8mb4_unicode_ci;
        CREATE DATABASE `cata_auth`       DEFAULT CHARACTER SET UTF8MB4 COLLATE utf8mb4_unicode_ci;

        GRANT ALL PRIVILEGES ON `cata_world`      .* TO 'phoenix'@'localhost' WITH GRANT OPTION;
        GRANT ALL PRIVILEGES ON `cata_characters` .* TO 'phoenix'@'localhost' WITH GRANT OPTION;
        GRANT ALL PRIVILEGES ON `cata_auth`       .* TO 'phoenix'@'localhost' WITH GRANT OPTION;

        -- ── MoP databases ──────────────────────────────────────────────────────
        CREATE DATABASE `mop_world`      DEFAULT CHARACTER SET UTF8MB4 COLLATE utf8mb4_unicode_ci;
        CREATE DATABASE `mop_characters` DEFAULT CHARACTER SET UTF8MB4 COLLATE utf8mb4_unicode_ci;
        CREATE DATABASE `mop_auth`       DEFAULT CHARACTER SET UTF8MB4 COLLATE utf8mb4_unicode_ci;

        GRANT ALL PRIVILEGES ON `mop_world`      .* TO 'phoenix'@'localhost' WITH GRANT OPTION;
        GRANT ALL PRIVILEGES ON `mop_characters` .* TO 'phoenix'@'localhost' WITH GRANT OPTION;
        GRANT ALL PRIVILEGES ON `mop_auth`       .* TO 'phoenix'@'localhost' WITH GRANT OPTION;

        -- ── Custom core databases ─────────────────────────────────────────────
        CREATE DATABASE `custom_world`      DEFAULT CHARACTER SET UTF8MB4 COLLATE utf8mb4_unicode_ci;
        CREATE DATABASE `custom_characters` DEFAULT CHARACTER SET UTF8MB4 COLLATE utf8mb4_unicode_ci;
        CREATE DATABASE `custom_auth`       DEFAULT CHARACTER SET UTF8MB4 COLLATE utf8mb4_unicode_ci;

        GRANT ALL PRIVILEGES ON `custom_world`      .* TO 'phoenix'@'localhost' WITH GRANT OPTION;
        GRANT ALL PRIVILEGES ON `custom_characters` .* TO 'phoenix'@'localhost' WITH GRANT OPTION;
        GRANT ALL PRIVILEGES ON `custom_auth`       .* TO 'phoenix'@'localhost' WITH GRANT OPTION;

        -- ── Flush ──────────────────────────────────────────────────────────────
        FLUSH PRIVILEGES;
        """;
}
