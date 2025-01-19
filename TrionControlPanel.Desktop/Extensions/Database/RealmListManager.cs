
using TrionControlPanel.Desktop.Extensions.Modules.Lists;
using static TrionControlPanel.Desktop.Extensions.Modules.Enums;

namespace TrionControlPanel.Desktop.Extensions.Database
{
    public class RealmListMenager
    {
        public static async Task<List<T>> GetRealmLists<T>(AppSettings Settings)
        {
            return await AccessManager.LodaDataList<T, dynamic>(
                SqlQueryManager.GetRealmList(Settings.SelectedCore),
                new { },
                AccessManager.ConnectionString(Settings, Settings.AuthDatabase)
            );
        }

        public static async Task<RealmListOpResult> SaveRealmList(
        int ID, 
        string? Name, 
        string? Address,
        string? LocalAddress, 
        string? LocalSubnetMask, 
        int? Port, 
        int? GameBuild,
        AppSettings Settings
        )
        {
            if(Settings.SelectedCore == Cores.AscEmu)
            {
                await AccessManager.SaveData(SqlQueryManager.SaveRealmList(Cores.AscEmu), new
                {
                    ID = ID,
                    Password = Address

                }, AccessManager.ConnectionString(Settings, Settings.AuthDatabase));
                return RealmListOpResult.Ok;
            }
            if (Settings.SelectedCore == Cores.VMaNGOS || Settings.SelectedCore == Cores.CMaNGOS)
            {
                await AccessManager.SaveData(SqlQueryManager.SaveRealmList(Cores.AscEmu), new
                {
                    ID,
                    Name,
                    Address,
                    Port,
                    GameBuild,

                }, AccessManager.ConnectionString(Settings, Settings.AuthDatabase));
                return RealmListOpResult.Ok;
            }
            if (Settings.SelectedCore == Cores.TrinityCore || Settings.SelectedCore == Cores.TrinityCore335 ||
                Settings.SelectedCore == Cores.TrinityCoreClassic || Settings.SelectedCore == Cores.CypherCore ||
                Settings.SelectedCore == Cores.AzerothCore
                )
            {
                await AccessManager.SaveData(SqlQueryManager.SaveRealmList(Cores.AscEmu), new
                {
                    ID,
                    Name,
                    Address,
                    LocalAddress,
                    LocalSubnetMask,
                    Port,
                    GameBuild,

                }, AccessManager.ConnectionString(Settings, Settings.AuthDatabase));
                return RealmListOpResult.Ok;
            }
            return RealmListOpResult.BadLink;
        }
    }
}
