namespace TrionControlPanel.API.Classes.Database
{
    public class DatabaseManager
    {
        private readonly AccessManager _accessManager;
        public DatabaseManager (AccessManager accessManager)
        {
            _accessManager = accessManager;
        }
        public async Task<bool> GetKeyVerified (string SupporterKey)
        {
            return await _accessManager.LoadDataType<bool, dynamic>(SqlQueryManager.SELECT_SUPPORT_KEY, new { Key = SupporterKey });

        }
    }
}
