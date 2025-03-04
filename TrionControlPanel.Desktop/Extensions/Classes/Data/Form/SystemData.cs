using System.Collections.ObjectModel;
using TrionControlPanel.Desktop.Extensions.Classes.Monitor;
using TrionControlPanel.Desktop.Extensions.Modules.Lists;

namespace TrionControlPanel.Desktop.Extensions.Classes.Data.Form
{
    public class SystemData
    {
        // Locks to ensure thread-safe access to process IDs
        private static readonly object _databaseLock = new();
        private static readonly object _worldLock = new();
        private static readonly object _logonLock = new();

        // Start time for different components (Database, World, Logon)
        public static DateTime DatabaseStartTime { get; set; }
        public static DateTime WorldStartTime { get; set; }
        public static DateTime LogonStartTime { get; set; }

        // Process ID lists for each component
        private static List<ProcessID> _databaseProcessID = new();
        private static List<ProcessID> _worldProcessesID = new();
        private static List<ProcessID> _logonProcessesID = new();


        #region "Database Process ID CRUD"

        public static void AddToDatabaseProcessID(ProcessID processID)
        {
            try
            {
                lock (_databaseLock)
                {
                    _databaseProcessID.Add(processID);
                    TrionLogger.Log($"Added ProcessID {processID} to Database Process list.", "INFO");
                }
            }
            catch (Exception ex)
            {
                TrionLogger.Log($"Error occurred while adding ProcessID to Database Process list: {ex.Message}", "ERROR");
            }
        }

        public static bool RemoveFromDatabaseProcessID(ProcessID processID)
        {
            try
            {
                lock (_databaseLock)
                {
                    bool result = _databaseProcessID.Remove(processID);
                    if (result)
                        TrionLogger.Log($"Removed ProcessID {processID} from Database Process list.", "INFO");
                    else
                        TrionLogger.Log($"ProcessID {processID} not found in Database Process list.", "WARNING");
                    return result;
                }
            }
            catch (Exception ex)
            {
                TrionLogger.Log($"Error occurred while removing ProcessID from Database Process list: {ex.Message}", "ERROR");
                return false;
            }
        }

        public static void CleanDatabaseProcessID()
        {
            try
            {
                lock (_databaseLock)
                {
                    _databaseProcessID.Clear();
                    TrionLogger.Log("Cleared all ProcessIDs from Database Process list.", "INFO");
                }
            }
            catch (Exception ex)
            {
                TrionLogger.Log($"Error occurred while clearing the Database Process list: {ex.Message}", "ERROR");
            }
        }

        public static ReadOnlyCollection<ProcessID> GetDatabaseProcessID()
        {
            try
            {
                lock (_databaseLock)
                {
                    TrionLogger.Log("Fetched Database Process list.", "INFO");
                    return _databaseProcessID.AsReadOnly();
                }
            }
            catch (Exception ex)
            {
                TrionLogger.Log($"Error occurred while fetching Database Process list: {ex.Message}", "ERROR");
                return new ReadOnlyCollection<ProcessID>(new List<ProcessID>());
            }
        }

        public static List<ProcessID> GetDatabaseProcessIDPage(int pageNumber, int pageSize)
        {
            try
            {
                lock (_databaseLock)
                {
                    var result = _databaseProcessID
                        .Skip((pageNumber - 1) * pageSize) // Skip items from previous pages
                        .Take(pageSize)                    // Take the number of items per page
                        .ToList();
                    TrionLogger.Log($"Fetched page {pageNumber} of Database Process list with {result.Count} entries.", "INFO");
                    return result;
                }
            }
            catch (Exception ex)
            {
                TrionLogger.Log($"Error occurred while fetching a page of Database Process list: {ex.Message}", "ERROR");
                return new List<ProcessID>();
            }
        }

        public static int GetTotalDatabaseProcessIDCount()
        {
            try
            {
                lock (_databaseLock)
                {
                    int count = _databaseProcessID.Count;
                    TrionLogger.Log($"Total ProcessID count in Database Process list: {count}", "INFO");
                    return count;
                }
            }
            catch (Exception ex)
            {
                TrionLogger.Log($"Error occurred while counting Database Process IDs: {ex.Message}", "ERROR");
                return 0;
            }
        }

        #endregion

        #region "World Process ID CRUD"

        public static void AddToWorldProcessesID(ProcessID processID)
        {
            try
            {
                lock (_worldLock)
                {
                    _worldProcessesID.Add(processID);
                    TrionLogger.Log($"Added ProcessID {processID} to World Process list.", "INFO");
                }
            }
            catch (Exception ex)
            {
                TrionLogger.Log($"Error occurred while adding ProcessID to World Process list: {ex.Message}", "ERROR");
            }
        }

        public static bool RemoveFromWorldProcessesID(ProcessID processID)
        {
            try
            {
                lock (_worldLock)
                {
                    bool result = _worldProcessesID.Remove(processID);
                    if (result)
                        TrionLogger.Log($"Removed ProcessID {processID} from World Process list.", "INFO");
                    else
                        TrionLogger.Log($"ProcessID {processID} not found in World Process list.", "WARNING");
                    return result;
                }
            }
            catch (Exception ex)
            {
                TrionLogger.Log($"Error occurred while removing ProcessID from World Process list: {ex.Message}", "ERROR");
                return false;
            }
        }

        public static ReadOnlyCollection<ProcessID> GetWorldProcessesID()
        {
            try
            {
                lock (_worldLock)
                {
                    TrionLogger.Log("Fetched World Process list.", "INFO");
                    return _worldProcessesID.AsReadOnly();
                }
            }
            catch (Exception ex)
            {
                TrionLogger.Log($"Error occurred while fetching World Process list: {ex.Message}", "ERROR");
                return new ReadOnlyCollection<ProcessID>(new List<ProcessID>());
            }
        }

        public static List<ProcessID> GetWorldProcessesIDPage(int pageNumber, int pageSize)
        {
            try
            {
                lock (_worldLock)
                {
                    var result = _worldProcessesID
                        .Skip((pageNumber - 1) * pageSize) // Skip items from previous pages
                        .Take(pageSize)                    // Take the number of items per page
                        .ToList();
                    TrionLogger.Log($"Fetched page {pageNumber} of World Process list with {result.Count} entries.", "INFO");
                    return result;
                }
            }
            catch (Exception ex)
            {
                TrionLogger.Log($"Error occurred while fetching a page of World Process list: {ex.Message}", "ERROR");
                return new List<ProcessID>();
            }
        }

        public static int GetTotalWorldProcessIDCount()
        {
            try
            {
                lock (_worldLock)
                {
                    int count = _worldProcessesID.Count;
                    TrionLogger.Log($"Total ProcessID count in World Process list: {count}", "INFO");
                    return count;
                }
            }
            catch (Exception ex)
            {
                TrionLogger.Log($"Error occurred while counting World Process IDs: {ex.Message}", "ERROR");
                return 0;
            }
        }

        public static void CleanWorldProcessID()
        {
            try
            {
                lock (_worldLock)
                {
                    _worldProcessesID.Clear();
                    TrionLogger.Log("Cleared all ProcessIDs from World Process list.", "INFO");
                }
            }
            catch (Exception ex)
            {
                TrionLogger.Log($"Error occurred while clearing the World Process list: {ex.Message}", "ERROR");
            }
        }

        #endregion

        #region "Logon Process ID CRUD"

        public static void AddToLogonProcessesID(ProcessID processID)
        {
            try
            {
                lock (_logonLock)
                {
                    _logonProcessesID.Add(processID);
                    TrionLogger.Log($"Added ProcessID {processID} to Logon Process list.", "INFO");
                }
            }
            catch (Exception ex)
            {
                TrionLogger.Log($"Error occurred while adding ProcessID to Logon Process list: {ex.Message}", "ERROR");
            }
        }

        public static bool RemoveFromLogonProcessesID(ProcessID processID)
        {
            try
            {
                lock (_logonLock)
                {
                    bool result = _logonProcessesID.Remove(processID);
                    if (result)
                        TrionLogger.Log($"Removed ProcessID {processID} from Logon Process list.", "INFO");
                    else
                        TrionLogger.Log($"ProcessID {processID} not found in Logon Process list.", "WARNING");
                    return result;
                }
            }
            catch (Exception ex)
            {
                TrionLogger.Log($"Error occurred while removing ProcessID from Logon Process list: {ex.Message}", "ERROR");
                return false;
            }
        }

        public static void CleanLogonProcessID()
        {
            try
            {
                lock (_logonLock)
                {
                    _logonProcessesID.Clear();
                    TrionLogger.Log("Cleared all ProcessIDs from Logon Process list.", "INFO");
                }
            }
            catch (Exception ex)
            {
                TrionLogger.Log($"Error occurred while clearing the Logon Process list: {ex.Message}", "ERROR");
            }
        }

        public static ReadOnlyCollection<ProcessID> GetLogonProcessesID()
        {
            try
            {
                lock (_logonLock)
                {
                    TrionLogger.Log("Fetched Logon Process list.", "INFO");
                    return _logonProcessesID.AsReadOnly();
                }
            }
            catch (Exception ex)
            {
                TrionLogger.Log($"Error occurred while fetching Logon Process list: {ex.Message}", "ERROR");
                return new ReadOnlyCollection<ProcessID>(new List<ProcessID>());
            }
        }

        public static List<ProcessID> GetLogonProcessesIDPage(int pageNumber, int pageSize)
        {
            try
            {
                lock (_logonLock)
                {
                    var result = _logonProcessesID
                        .Skip((pageNumber - 1) * pageSize) // Skip items from previous pages
                        .Take(pageSize)                    // Take the number of items per page
                        .ToList();
                    TrionLogger.Log($"Fetched page {pageNumber} of Logon Process list with {result.Count} entries.", "INFO");
                    return result;
                }
            }
            catch (Exception ex)
            {
                TrionLogger.Log($"Error occurred while fetching a page of Logon Process list: {ex.Message}", "ERROR");
                return new List<ProcessID>();
            }
        }

        public static int GetTotalLogonProcessIDCount()
        {
            try
            {
                lock (_logonLock)
                {
                    int count = _logonProcessesID.Count;
                    TrionLogger.Log($"Total ProcessID count in Logon Process list: {count}", "INFO");
                    return count;
                }
            }
            catch (Exception ex)
            {
                TrionLogger.Log($"Error occurred while counting Logon Process IDs: {ex.Message}", "ERROR");
                return 0;
            }
        }

        #endregion
    }
}
