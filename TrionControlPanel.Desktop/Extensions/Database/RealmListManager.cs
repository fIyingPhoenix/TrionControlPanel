using TrionControlPanel.Desktop.Extensions.Classes.Monitor;
using TrionControlPanel.Desktop.Extensions.Modules.Lists;
using static TrionControlPanel.Desktop.Extensions.Modules.Enums;

namespace TrionControlPanel.Desktop.Extensions.Database
{
    public class RealmListManager
    {
        // Helper method to handle AscEmu check to avoid repeating the check in every method
        private static bool IsAscEmu(AppSettings Settings)
        {
            if (Settings.SelectedCore == Cores.AscEmu)
            {
                
                TrionLogger.Log("AscEmu does not use a database for storing the realmlist address.", "ERROR");
                return true;
            }
            return false;
        }

        // Get the list of realms
        public static async Task<List<T>> GetRealmLists<T>(AppSettings Settings)
        {
            return await AccessManager.LoadDataList<T, dynamic>(
                SqlQueryManager.GetRealmList(Settings.SelectedCore),
                new { },
                AccessManager.ConnectionString(Settings, Settings.AuthDatabase)
            );
        }

        // Update the realm list address
        public static async Task<RealmListOpResult> UpdateRealmListAddress(int ID, string Address, AppSettings Settings)
        {
            // Check if AscEmu, if so, return early
            if (IsAscEmu(Settings)) return RealmListOpResult.BadEmulator;

            try
            {
                // Perform the update
                await AccessManager.SaveData(SqlQueryManager.UpdateRealmList(Settings.SelectedCore), new
                {
                    ID,
                    Address,
                }, AccessManager.ConnectionString(Settings, Settings.AuthDatabase));
                TrionLogger.Log($"Realm list address updated: ID:{ID} Emulator: {Settings.SelectedCore} Address: {Address}");
                return RealmListOpResult.Success;
            }
            catch (Exception ex)
            {
                TrionLogger.Log($"Error updating realm list address for ID: {ID}. Error: {ex.Message}", "ERROR");
                return RealmListOpResult.DatabaseInternalError;
            }
        }

        // Create a new realm list
        public static async Task<RealmListOpResult> CreateRealmList(AppSettings Settings, string Name, string Address, string LocalAddress, string SubnetMask, int Port, int Gamebuild)
        {
            // Check if AscEmu, if so, return early
            if (IsAscEmu(Settings)) return RealmListOpResult.BadEmulator;

            try
            {
                // Create the realm list
                await AccessManager.SaveData(SqlQueryManager.CreateAccount(Settings.SelectedCore), new
                {
                    Name,
                    Address,
                    LocalAddress,
                    SubnetMask,
                    Port,
                    Gamebuild
                }, AccessManager.ConnectionString(Settings, Settings.AuthDatabase));
                TrionLogger.Log($"Realm list created: Name:{Name} Emulator: {Settings.SelectedCore} Address: {Address}");
                return RealmListOpResult.Success;
            }
            catch (Exception ex)
            {
                TrionLogger.Log($"Error creating realm list for Name: {Name}. Error: {ex.Message}", "ERROR");
                return RealmListOpResult.DatabaseInternalError;
            }
        }

        // Delete a realm list by ID
        public static async Task<RealmListOpResult> DeleteRealmList(AppSettings Settings, int ID)
        {
            // Check if AscEmu, if so, return early
            if (IsAscEmu(Settings)) return RealmListOpResult.BadEmulator;

            try
            {
                // Perform the deletion
                await AccessManager.SaveData(SqlQueryManager.DeleteRealmList(Settings.SelectedCore), new
                {
                    ID
                }, AccessManager.ConnectionString(Settings, Settings.AuthDatabase));
                TrionLogger.Log($"Realm list deleted: ID:{ID} Emulator: {Settings.SelectedCore}");
                return RealmListOpResult.Success;
            }
            catch (Exception ex)
            {
                TrionLogger.Log($"Error deleting realm list for ID: {ID}. Error: {ex.Message}", "ERROR");
                return RealmListOpResult.DatabaseInternalError;
            }
        }
    }
}
