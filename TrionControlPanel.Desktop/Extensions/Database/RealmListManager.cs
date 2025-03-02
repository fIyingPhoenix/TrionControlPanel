
using TrionControlPanel.Desktop.Extensions.Classes.Monitor;
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
        public static async Task<RealmListOpResult> UpdateTealmListAddress(int ID, string Address, AppSettings Settings)
        {
            if (Settings.SelectedCore == Cores.AscEmu)
            {
                TrionLogger.Log("AscEmu does not use a database for storing the realmlist address.", "ERROR");
                return RealmListOpResult.BadEmulator;
            }
            try
            {
                await AccessManager.SaveData(SqlQueryManager.UpdateRealmListAddress(Settings.SelectedCore), new
                {
                    ID,
                    Address,
                }, AccessManager.ConnectionString(Settings, Settings.AuthDatabase));
                TrionLogger.Log($"Address update for ID: {ID} Emulatro: {Settings.SelectedCore} Address:{Address}");
                return RealmListOpResult.Ok;
                

            } catch (Exception ex)
            {
               TrionLogger.Log(ex.Message, "ERROR");
                return RealmListOpResult.DBInternalError;
            }
        }

        public static async Task<RealmListOpResult> CreateRealmList(AppSettings Settings, string Name, string Address, string LocalAddress, string SubnetMask, int Port, int Gamebuild)
        {
            if (Settings.SelectedCore == Cores.AscEmu)
            {
                TrionLogger.Log("AscEmu does not use a database for storing the realmlist address.", "ERROR");
                return RealmListOpResult.BadEmulator;
            }
            try
            {
                await AccessManager.SaveData(SqlQueryManager.CreateRealmList(Settings.SelectedCore), new
                {
                   Name,
                   Address,
                   LocalAddress,
                   SubnetMask,
                   Port,
                   Gamebuild
                }, AccessManager.ConnectionString(Settings, Settings.AuthDatabase));
                TrionLogger.Log($"Realmilst Created: {Name} Emulatro: {Settings.SelectedCore} Address:{Address}");
                return RealmListOpResult.Ok;
            }
            catch (Exception ex)
            {
                TrionLogger.Log(ex.Message, "ERROR");
                return RealmListOpResult.DBInternalError;
            }
        }
        public static async Task<RealmListOpResult> DeleteRealmList(AppSettings Settings, int ID)
        {
            if (Settings.SelectedCore == Cores.AscEmu)
            {
                TrionLogger.Log("AscEmu does not use a database for storing the realmlist address.", "ERROR");
                return RealmListOpResult.BadEmulator;
            }
            try
            {
                await AccessManager.SaveData(SqlQueryManager.DeleteRealmList(Settings.SelectedCore), new
                {
                    ID
                }, AccessManager.ConnectionString(Settings, Settings.AuthDatabase));
                TrionLogger.Log($"Realmilst Deleted: {ID} Emulatro: {Settings.SelectedCore}");
                return RealmListOpResult.Ok;
            }
            catch (Exception ex)
            {
                TrionLogger.Log(ex.Message, "ERROR");
                return RealmListOpResult.DBInternalError;
            }
        }
    }
}
