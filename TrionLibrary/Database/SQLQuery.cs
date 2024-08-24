using TrionLibrary.Models;

namespace TrionLibrary.Database
{
    public class SQLQuery
    {
        public static string SaveRealm()
        {
            return Setting.Setting.List.SelectedCore switch
            {
                Enums.Cores.AscEmu => "UPDATE `realms` SET `password` = @Password, `status_change_time` = @StatusChangeTime WHERE `id` = @ID;",
                Enums.Cores.AzerothCore => "UPDATE `realmlist` SET `name` = @Name, `address` = @Address, `localAddress` = @LocalAddress, `localSubnetMask` = @LocalSubnetMask, `port` = @Port, `icon` = @Icon, `flag` = @Flag, `timezone` = @Timezone WHERE `id` = @ID;",
                Enums.Cores.CMaNGOS => "UPDATE `realmlist` SET `name` = '@Name', `address` = '@Address', `port` = @Port, `icon` = @Icon, `realmflags` = @Realmflags, `timezone` = @Timezone, `allowedSecurityLevel` = @AllowedSecurityLevel WHERE `id` = @ID;",
                Enums.Cores.CypherCore => "UPDATE `realmlist` SET `name` = '@Name', `address` = '@Address', `localAddress` = '@LocalAddress', `localSubnetMask` = '@LocalSubnetMask', `port` = @Port, `icon` = @Icon, `flag` = @Flag, `timezone` = @Timezone WHERE `id` = @ID;",
                Enums.Cores.TrinityCore335 => $"UPDATE `realmlist` SET `name` = '@Name', `address` = '@Address', `localAddress` = '@LocalAddress', `localSubnetMask` = '@LocalSubnetMask', `port` = @Port, `icon` = @Icon, `flag` = @Flag, `timezone` = @Timezone WHERE `id` = @ID;",
                Enums.Cores.TrinityCore => "UPDATE `realmlist` SET `name` = '@Name', `address` = '@Address', `localAddress` = '@LocalAddress', `localSubnetMask` = '@LocalSubnetMask', `port` = @Port, `icon` = @Icon, `flag` = @Flag, `timezone` = @Timezone WHERE `id` = @ID;",
                Enums.Cores.TrinityCoreClassic => $"UPDATE `realmlist` SET `name` = '@Name', `address` = '@Address', `localAddress` = '@LocalAddress', `localSubnetMask` = '@LocalSubnetMask', `port` = @Port, `icon` = @Icon, `flag` = @Flag, `timezone` = @Timezone WHERE `id` = @ID;",
                Enums.Cores.VMaNGOS => "UPDATE `realmlist` SET `name` = '@Name', `address` = '@Address', `port` = @Port, `icon` = @Icon, `realmflags` = @Realmflags, `timezone` = @Timezone WHERE `id` = @ID;",
                _ => ""
            };
        }
        public static string LoadRealm()
        {
            return Setting.Setting.List.SelectedCore switch
            {
                Enums.Cores.AscEmu => $"SELECT * FROM `realms` LIMIT 10;",
                Enums.Cores.AzerothCore => $"SELECT * FROM `realmlist` LIMIT 10;",
                Enums.Cores.CMaNGOS => $"SELECT * FROM `realmlist` LIMIT 10;",
                Enums.Cores.CypherCore => $"SELECT * FROM `realmlist` LIMIT 10;",
                Enums.Cores.TrinityCore335 => $"SELECT * FROM `realmlist` LIMIT 10;",
                Enums.Cores.TrinityCore => $"SELECT * FROM `realmlist` LIMIT 10;",
                Enums.Cores.TrinityCoreClassic => $"SELECT * FROM `realmlist` LIMIT 10;",
                Enums.Cores.VMaNGOS => $"SELECT * FROM `realmlist` LIMIT 10;",
                _ => ""
            };
        }
        public static string AccountCreate()
        {
            return Setting.Setting.List.SelectedCore switch
            {
                Enums.Cores.AscEmu => $"",
                Enums.Cores.AzerothCore => $"INSERT INTO account(username, salt, verifier, email, reg_mail, joindate) VALUES(@Username, @Salt, @Verifier, @Email, @RegMail, @JoinDate)",
                Enums.Cores.CMaNGOS => $"",
                Enums.Cores.CypherCore => $"",
                Enums.Cores.TrinityCore335 => $"",
                Enums.Cores.TrinityCore => $"",
                Enums.Cores.TrinityCoreClassic => $"",
                Enums.Cores.VMaNGOS => $"",
                _ => ""
            };
        }
    }
}
