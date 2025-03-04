namespace TrionControlPanel.API.Classes.Database
{
    // SqlQueryManager class holds all the SQL queries used in database operations.
    // This class centralizes the queries to avoid scattering raw SQL across the codebase.
    public class SqlQueryManager
    {
        // SQL query to check if a supporter key exists in the 'SupporterKey' table.
        // It returns the count of matching rows for a given key.
        public static string SELECT_SUPPORT_KEY = "SELECT COUNT(`Key`) FROM `SupporterKey` WHERE `Key` = @Key";
    }
}
