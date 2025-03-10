
using TrionControlPanel.Desktop.Extensions.Modules;

namespace TrionControlPanel.Desktop.Extensions.Database
{
    public class SqlQueryManager
    {
        // Returns an SQL query to update the realm list based on the selected core
        public static string UpdateRealmList(Enums.Cores SelectedCore)
        {
            return SelectedCore switch
            {
                Enums.Cores.AscEmu => "UPDATE `realms` SET `password` = @Password WHERE `id` = @ID;",
                Enums.Cores.AzerothCore => "UPDATE `realmlist` SET `name` = @Name, `address` = @Address, `localAddress` = @LocalAddress, `localSubnetMask` = @LocalSubnetMask, `port` = @Port, `gamebuild`= @GameBuild  WHERE `id` = @ID;",
                Enums.Cores.CMaNGOS => "UPDATE `realmlist` SET `name` = '@Name', `address` = '@Address', `port` = @Port, `realmbuilds` = @GameBuild WHERE `id` = @ID;",
                Enums.Cores.CypherCore => "UPDATE `realmlist` SET `name` = '@Name', `address` = '@Address', `localAddress` = '@LocalAddress', `localSubnetMask` = '@LocalSubnetMask', `port` = @Port, `gamebuild`= @GameBuild WHERE `id` = @ID;",
                Enums.Cores.TrinityCore335 => $"UPDATE `realmlist` SET `name` = '@Name', `address` = '@Address', `localAddress` = '@LocalAddress', `localSubnetMask` = '@LocalSubnetMask', `port` = @Port, `gamebuild`= @GameBuild WHERE `id` = @ID;",
                Enums.Cores.TrinityCore => "UPDATE `realmlist` SET `name` = '@Name', `address` = '@Address', `localAddress` = '@LocalAddress', `localSubnetMask` = '@LocalSubnetMask', `port` = @Port, `gamebuild`= @GameBuild WHERE `id` = @ID;",
                Enums.Cores.TrinityCoreClassic => $"UPDATE `realmlist` SET `name` = '@Name', `address` = '@Address', `localAddress` = '@LocalAddress', `localSubnetMask` = '@LocalSubnetMask', `port` = @Port, `gamebuild`= @GameBuild WHERE `id` = @ID;",
                Enums.Cores.VMaNGOS => "UPDATE `realmlist` SET `name` = '@Name', `address` = '@Address', `port` = @Port, `realmbuilds` = @GameBuild WHERE `id` = @ID;",
                _ => ""
            };
        }
        // Returns an SQL query to get the realm list based on the selected core
        public static string GetRealmList(Enums.Cores SelectedCore)
        {
            return SelectedCore switch
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
        // Returns an SQL query to get a user ID based on the selected core
        public static string GetUserID(Enums.Cores SelectedCore)
        {
            switch (SelectedCore)
            {
                case Enums.Cores.AscEmu:
                    return "SELECT id FROM accounts WHERE username = @Username";
                case Enums.Cores.AzerothCore:
                    return "SELECT id FROM account WHERE username = @Username";
                case Enums.Cores.CMaNGOS:
                    return "SELECT id FROM account WHERE username = @Username";
                case Enums.Cores.CypherCore:
                    return "SELECT id FROM account WHERE username = @Username";
                case Enums.Cores.TrinityCore335:
                    return "SELECT id FROM account WHERE username = @Username";
                case Enums.Cores.TrinityCore:
                    return "SELECT id FROM account WHERE username = @Username";
                case Enums.Cores.TrinityCoreClassic:
                    return "SELECT id FROM account WHERE username = @Username";
                case Enums.Cores.VMaNGOS:
                    return "SELECT id FROM account WHERE username = @Username";
                default:
                    return "";
            };
        }
        // Returns an SQL query to create a user account based on the selected core
        public static string CreateAccount(Enums.Cores SelectedCore)
        {
            switch (SelectedCore)
            {
                case Enums.Cores.AscEmu:
                    return "INSERT INTO accounts(acc_name, encrypted_password, email, joindate) VALUES (@Username, @EncryptedPassword, @Email, @JoinDate)";
                case Enums.Cores.AzerothCore:
                    return "INSERT INTO account(username, salt, verifier, email, reg_mail, joindate) VALUES(@Username, @Salt, @Verifier, @Email, @RegMail, @JoinDate)";
                case Enums.Cores.CMaNGOS:
                    return "INSERT INTO `account` (`username`, `v`, `s`, `email`, `joindate`, `expansion`) VALUES (@Username, @Verifier, @Salt, @Email, @JoinDate, @Expansion)";
                case Enums.Cores.CypherCore:
                    return "INSERT INTO account(username, salt, verifier, email, joindate) VALUES (@Username, @Salt, @Verifier, @Email, @JoinDate)";
                case Enums.Cores.TrinityCore335:
                    return "INSERT INTO account(username, sha_pass_hash, email, joindate) VALUES (@Username, @ShaPassHash, @Email, @JoinDate)";
                case Enums.Cores.TrinityCore:
                    return "INSERT INTO account(username, sha_pass_hash, email, joindate) VALUES (@Username, @ShaPassHash, @Email, @JoinDate)";
                case Enums.Cores.TrinityCoreClassic:
                    return "INSERT INTO account(username, sha_pass_hash, email, joindate) VALUES (@Username, @ShaPassHash, @Email, @JoinDate)";
                case Enums.Cores.VMaNGOS:
                    return "INSERT INTO `account` (`username`,`v`, `s`, `email`, `joindate`, `expansion`) VALUES (@Username, @GMLevel, @Verifier, @Salt, @Email, @JoinDate, @Expansion)";
                default:
                    return "";
            }
        }
        // Returns an SQL query to fetch a user by username
        public static string GetUserByUsername(Enums.Cores SelectedCore)
        {
            switch (SelectedCore)
            {
                case Enums.Cores.AscEmu:
                    return "SELECT username FROM accounts WHERE username = @Username";
                case Enums.Cores.AzerothCore:
                    return "SELECT username FROM account WHERE username = @Username";
                case Enums.Cores.CMaNGOS:
                    return "SELECT username FROM account WHERE username = @Username";
                case Enums.Cores.CypherCore:
                    return "SELECT username FROM account WHERE username = @Username";
                case Enums.Cores.TrinityCore335:
                    return "SELECT username FROM account WHERE username = @Username";
                case Enums.Cores.TrinityCore:
                    return "SELECT username FROM account WHERE username = @Username";
                case Enums.Cores.TrinityCoreClassic:
                    return "SELECT username FROM account WHERE username = @Username";
                case Enums.Cores.VMaNGOS:
                    return "SELECT username FROM account WHERE username = @Username";
                default:
                    return "";
            };
        }
        // Returns an SQL query to fetch an email by email
        public static string GetEmailByEmail(Enums.Cores SelectedCore)
        {
            switch (SelectedCore)
            {
                case Enums.Cores.AscEmu:
                    return "SELECT email FROM accounts WHERE email = @Email";
                case Enums.Cores.AzerothCore:
                    return "SELECT email FROM account WHERE email = @Email";
                case Enums.Cores.CMaNGOS:
                    return "SELECT email FROM account WHERE email = @Email";
                case Enums.Cores.CypherCore:
                    return "SELECT email FROM account WHERE email = @Email";
                case Enums.Cores.TrinityCore335:
                    return "SELECT email FROM account WHERE email = @Email";
                case Enums.Cores.TrinityCore:
                    return "SELECT email FROM account WHERE email = @Email";
                case Enums.Cores.TrinityCoreClassic:
                    return "SELECT email FROM account WHERE email = @Email";
                case Enums.Cores.VMaNGOS:
                    return "SELECT email FROM account WHERE email = @Email";
                default:
                    return "";
            };
        }
        // Returns an SQL query to update a realm list's address based on the selected core
        public static string UpdateRealmListAddress(Enums.Cores SelectedCore) 
        {
            switch (SelectedCore)
            {
                case Enums.Cores.AzerothCore:
                    return "UPDATE `realmlist` SET `address` = @Address WHERE `id` = @ID";
                case Enums.Cores.CMaNGOS:
                    return "UPDATE `realmlist` SET `address` = @Address WHERE `id` = @ID";
                case Enums.Cores.CypherCore:
                    return "UPDATE `realmlist` SET `address` = @Address WHERE `id` = @ID";
                case Enums.Cores.TrinityCore335:
                    return "UPDATE `realmlist` SET `address` = @Address WHERE `id` = @ID";
                case Enums.Cores.TrinityCore:
                    return "UPDATE `realmlist` SET `address` = @Address WHERE `id` = @ID";
                case Enums.Cores.TrinityCoreClassic:
                    return "UPDATE `realmlist` SET `address` = @Address WHERE `id` = @ID";
                case Enums.Cores.VMaNGOS:
                    return "UPDATE `realmlist` SET `address` = @Address WHERE `id` = @ID";
                default:
                    return "";
            };

        }
        // Returns an SQL query to create a realm list entry
        public static string CreateRealmList(Enums.Cores SelectedCore)
        {
            switch (SelectedCore)
            {
                case Enums.Cores.AzerothCore:
                    return "INSERT INTO `realmlist` ( `name`, `address`, `localAddress`, `localSubnetMask`, `port`, `gamebuild`) VALUES (@Name,@Address,@LocalAddress, @LocalSubnetMask, @Port, @Gamebuild)";
                case Enums.Cores.CMaNGOS:
                    return "INSERT INTO `realmlist` ( `name`, `address`, `port`, `gamebuild`) VALUES (@Name,@Address,@LocalAddress, @LocalSubnetMask, @Port ,@Gamebuild)";
                case Enums.Cores.CypherCore:
                    return "INSERT INTO `realmlist` ( `name`, `address`, `localAddress`, `localSubnetMask`, `port`, `gamebuild`) VALUES (@Name,@Address,@LocalAddress, @LocalSubnetMask, @Port, @Gamebuild)";
                case Enums.Cores.TrinityCore335:
                    return "INSERT INTO `realmlist` ( `name`, `address`, `localAddress`, `localSubnetMask`, `port`, `gamebuild`) VALUES (@Name,@Address,@LocalAddress, @LocalSubnetMask, @Port, @Gamebuild )";
                case Enums.Cores.TrinityCore:
                    return "INSERT INTO `realmlist` ( `name`, `address`, `localAddress`, `localSubnetMask`, `port`, `gamebuild`) VALUES (@Name,@Address,@LocalAddress, @LocalSubnetMask, @Port, @Gamebuild )";
                case Enums.Cores.TrinityCoreClassic:
                    return "INSERT INTO `realmlist` ( `name`, `address`, `localAddress`, `localSubnetMask`, `port`, `gamebuild`) VALUES (@Name,@Address,@LocalAddress, @LocalSubnetMask, @Port, @Gamebuild )";
                case Enums.Cores.VMaNGOS:
                    return "INSERT INTO `realmlist` ( `name`, `address`, `port`, `gamebuild`) VALUES (@Name,@Address ,@Port, @Gamebuild)";
                default:
                    return "";
            };
        }
        // Returns an SQL query to delete a realm list entry
        public static string DeleteRealmList(Enums.Cores SelectedCore)
        {
            switch (SelectedCore)
            {
                case Enums.Cores.AzerothCore:
                    return "DELETE FROM `realmlist` WHERE `ID`= @ID";
                case Enums.Cores.CMaNGOS:
                    return "DELETE FROM `realmlist` WHERE `ID`= @ID";
                case Enums.Cores.CypherCore:
                    return "DELETE FROM `realmlist` WHERE `ID`= @ID";
                case Enums.Cores.TrinityCore335:
                    return "DELETE FROM `realmlist` WHERE `ID`= @ID";
                case Enums.Cores.TrinityCore:
                    return "DELETE FROM `realmlist` WHERE `ID`= @ID";
                case Enums.Cores.TrinityCoreClassic:
                    return "DELETE FROM `realmlist` WHERE `ID`= @ID";
                case Enums.Cores.VMaNGOS:
                    return "DELETE FROM `realmlist` WHERE `ID`= @ID";
                default:
                    return "";
            };
        }
    }
}
