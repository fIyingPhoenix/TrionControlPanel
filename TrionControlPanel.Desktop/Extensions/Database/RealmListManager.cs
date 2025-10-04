
using TrionControlPanel.Desktop.Extensions.Classes.Monitor;
using TrionControlPanel.Desktop.Extensions.Modules.Lists;
using static TrionControlPanel.Desktop.Extensions.Modules.Enums;

namespace TrionControlPanel.Desktop.Extensions.Database
{
    public static class RealmListManager  
    {
        /*----------------------------------------------------------
         *  READ
         *--------------------------------------------------------*/
        public static Task<List<T>> GetRealmListsAsync<T>(AppSettings settings)
            => AccessManager.LodaDataList<T, dynamic>(
                SqlQueryManager.GetRealmList(settings.SelectedCore),
                new { },
                AccessManager.ConnectionString(settings, settings.AuthDatabase));

        /*----------------------------------------------------------
         *  UPDATE
         *--------------------------------------------------------*/
        public static Task<RealmListOpResult> UpdateRealmListAddressAsync(
            int id, string address, AppSettings settings)
            => ExecuteAsync(
                SqlQueryManager.UpdateRealmListAddress(settings.SelectedCore),
                new { ID = id, Address = address },
                settings,
                $"Address updated  ID:{id}  Core:{settings.SelectedCore}  Address:{address}");

        /*----------------------------------------------------------
         *  CREATE
         *--------------------------------------------------------*/
        public static Task<RealmListOpResult> CreateRealmListAsync(
            AppSettings settings,
            string name,
            string address,
            string localAddress,
            string subnetMask,
            int port,
            int gameBuild)
            => ExecuteAsync(
                SqlQueryManager.CreateRealmList(settings.SelectedCore),
                new
                {
                    Name = name,
                    Address = address,
                    LocalAddress = localAddress,
                    LocalSubnetMask = subnetMask,
                    Port = port,
                    GameBuild = gameBuild
                },
                settings,
                $"Realm created  Name:{name}  Core:{settings.SelectedCore}  Address:{address}");

        /*----------------------------------------------------------
         *  DELETE
         *--------------------------------------------------------*/
        public static Task<RealmListOpResult> DeleteRealmListAsync(
            AppSettings settings, int id)
            => ExecuteAsync(
                SqlQueryManager.DeleteRealmList(settings.SelectedCore),
                new { ID = id },
                settings,
                $"Realm deleted  ID:{id}  Core:{settings.SelectedCore}");

        /*----------------------------------------------------------
         *  Private helper
         *--------------------------------------------------------*/
        private static async Task<RealmListOpResult> ExecuteAsync(
            string sql,
            object parameters,
            AppSettings settings,
            string successMessage)
        {
            try
            {
                await AccessManager.SaveData(
                    sql,
                    parameters,
                    AccessManager.ConnectionString(settings, settings.AuthDatabase));

                TrionLogger.Log(successMessage);
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