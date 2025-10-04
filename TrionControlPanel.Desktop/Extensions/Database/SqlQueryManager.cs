using System.Collections.Generic;
using System.Collections.Immutable;
using static TrionControlPanel.Desktop.Extensions.Modules.Enums;

namespace TrionControlPanel.Desktop.Extensions.Database
{
    public static class SqlQueryManager
    {
        #region Constant fragments
        private const string BaseRealmUpdate =
            "UPDATE `realmlist` " +
            "SET `name` = @Name, `address` = @Address, `port` = @Port, `gamebuild` = @GameBuild ";

        private const string BaseRealmInsert =
            "INSERT INTO `realmlist` (`name`, `address`, `port`, `gamebuild`) " +
            "VALUES (@Name, @Address, @Port, @GameBuild)";

        private const string BaseAccountInsert =
            "INSERT INTO `account` (`username`, `email`, `joindate`) " +
            "VALUES (@Username, @Email, @JoinDate)";

        private static readonly ImmutableDictionary<Cores, string> RealmListQueries =
            new Dictionary<Cores, string>
            {
                [Cores.AzerothCore] = "SELECT * FROM `realmlist` LIMIT 10;",
                [Cores.CMaNGOS] = "SELECT * FROM `realmlist` LIMIT 10;",
                [Cores.CypherCore] = "SELECT * FROM `realmlist` LIMIT 10;",
                [Cores.TrinityCore335] = "SELECT * FROM `realmlist` LIMIT 10;",
                [Cores.TrinityCore] = "SELECT * FROM `realmlist` LIMIT 10;",
                [Cores.TrinityCoreClassic] = "SELECT * FROM `realmlist` LIMIT 10;",
                [Cores.VMaNGOS] = "SELECT * FROM `realmlist` LIMIT 10;"
            }.ToImmutableDictionary();
        #endregion

        #region Public API
        public static string UpdateRealmList(Cores core) => core switch
        {
            Cores.AzerothCore =>
                BaseRealmUpdate + ", `localAddress` = @LocalAddress, `localSubnetMask` = @LocalSubnetMask WHERE `id` = @ID;",

            Cores.CMaNGOS or Cores.VMaNGOS =>
                "UPDATE `realmlist` SET `name` = '@Name', `address` = '@Address', `port` = @Port, " +
                "`realmbuilds` = @GameBuild WHERE `id` = @ID;",

            Cores.CypherCore or Cores.TrinityCore or Cores.TrinityCore335 or Cores.TrinityCoreClassic =>
                BaseRealmUpdate + ", `localAddress` = '@LocalAddress', `localSubnetMask` = '@LocalSubnetMask' WHERE `id` = @ID;",

            _ => string.Empty
        };

        public static string GetRealmList(Cores core) =>
            RealmListQueries.TryGetValue(core, out var q) ? q : string.Empty;

        public static string GetUserID(Cores core) =>
            "SELECT `id` FROM `account` WHERE `username` = @Username";

        public static string GetUserByUsername(Cores core) =>
            "SELECT `username` FROM `account` WHERE `username` = @Username";

        public static string GetEmailByEmail(Cores core) =>
            "SELECT `email` FROM `account` WHERE `email` = @Email";

        public static string UpdateRealmListAddress(Cores core) =>
            "UPDATE `realmlist` SET `address` = @Address WHERE `id` = @ID";

        public static string DeleteRealmList(Cores core) =>
            "DELETE FROM `realmlist` WHERE `id` = @ID";

        public static string CreateAccount(Cores core) => core switch
        {
            Cores.AzerothCore =>
                "INSERT INTO `account` (`username`, `salt`, `verifier`, `email`, `reg_mail`, `joindate`) " +
                "VALUES (@Username, @Salt, @Verifier, @Email, @RegMail, @JoinDate)",

            Cores.CMaNGOS =>
                "INSERT INTO `account` (`username`, `v`, `s`, `email`, `joindate`) " +
                "VALUES (@Username, @Verifier, @Salt, @Email, @JoinDate)",

            Cores.CypherCore =>
                "INSERT INTO `account` (`username`, `salt`, `verifier`, `email`, `joindate`) " +
                "VALUES (@Username, @Salt, @Verifier, @Email, @JoinDate)",

            Cores.TrinityCore or Cores.TrinityCore335 or Cores.TrinityCoreClassic =>
                "INSERT INTO `account` (`username`, `sha_pass_hash`, `email`, `joindate`) " +
                "VALUES (@Username, @ShaPassHash, @Email, @JoinDate)",

            Cores.VMaNGOS =>
                "INSERT INTO `account` (`username`, `v`, `s`, `email`, `joindate`) " +
                "VALUES (@Username, @Verifier, @Salt, @Email, @JoinDate)",

            _ => string.Empty
        };

        public static string CreateRealmList(Cores core) => core switch
        {
            Cores.AzerothCore or Cores.CypherCore or Cores.TrinityCore or
            Cores.TrinityCore335 or Cores.TrinityCoreClassic =>
                "INSERT INTO `realmlist` (`name`, `address`, `localAddress`, `localSubnetMask`, `port`, `gamebuild`) " +
                "VALUES (@Name, @Address, @LocalAddress, @LocalSubnetMask, @Port, @GameBuild)",

            Cores.CMaNGOS =>
                "INSERT INTO `realmlist` (`name`, `address`, `port`, `realmbuilds`) " +
                "VALUES (@Name, @Address, @Port, @GameBuild)",

            Cores.VMaNGOS =>
                "INSERT INTO `realmlist` (`name`, `address`, `port`, `gamebuild`) " +
                "VALUES (@Name, @Address, @Port, @GameBuild)",

            _ => string.Empty
        };
        public static string GrantGmLevel(Cores core) => core switch
        {
            // AzerothCore / CypherCore / TrinityCore (modern)
            Cores.AzerothCore or Cores.CypherCore or Cores.TrinityCore or
            Cores.TrinityCore335 or Cores.TrinityCoreClassic =>
                "INSERT INTO `account_access` (`id`, `gmlevel`, `RealmID`) " +
                "VALUES (@AccountId, @GmLevel, @RealmID) " +
                "ON DUPLICATE KEY UPDATE `gmlevel` = @GmLevel;",

            // CMaNGOS / VMaNGOS
            Cores.CMaNGOS or Cores.VMaNGOS =>
                "INSERT INTO `account` (`username`, `v`, `s`, `email`, `joindate`, `gmlevel`) " +
                "VALUES (@Username, @Verifier, @Salt, @Email, @JoinDate, @GmLevel) " +
                "ON DUPLICATE KEY UPDATE `gmlevel` = @GmLevel;",

            _ => string.Empty
        };
        #endregion
    }
}