namespace TrionControlPanel.API.Classes.Database
{
    public class SqlQueryManager
    {
        public static string SELECT_SUPPORT_KEY = "SELECT COUNT(`Key`) FROM `SupporterKey` WHERE `Key` = @Key";
    }
}
