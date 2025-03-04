namespace TrionControlPanel.API.Classes.Database
{
    // DatabaseManager class handles interactions with the database through the AccessManager
    public class DatabaseManager
    {
        private readonly AccessManager _accessManager;

        // Constructor injects an instance of AccessManager to interact with the database
        public DatabaseManager(AccessManager accessManager)
        {
            _accessManager = accessManager;
        }

        // Method to verify a Supporter Key by querying the database
        // Returns true if the key is verified, otherwise false
        public async Task<bool> GetKeyVerified(string supporterKey)
        {
            // Log the action for traceability (optional but helpful in debugging)
            TrionLogger.Log($"Verifying supporter key: {supporterKey}", "INFO");

            // Execute the SQL query to check the key in the database, using the AccessManager
            // Uses dynamic parameters to pass the SupporterKey to the SQL query
            var result = await _accessManager.LoadDataType<bool, dynamic>(
                SqlQueryManager.SELECT_SUPPORT_KEY, new { Key = supporterKey });

            // Return the result from the database query
            return result;
        }
    }
}
