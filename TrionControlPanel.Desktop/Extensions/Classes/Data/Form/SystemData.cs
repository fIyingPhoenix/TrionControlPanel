
using System.Collections.ObjectModel;
using TrionControlPanel.Desktop.Extensions.Modules.Lists;
namespace TrionControlPanel.Desktop.Extensions.Classes.Data.Form
{
    public class SystemData
    {
        private static readonly object _databaseLock = new();
        private static readonly object _worldLock = new();
        private static readonly object _logonLock = new();

        public static DateTime DatabaseStartTime { get; set; }
        public static DateTime WorldStartTime { get; set; }
        public static DateTime LogonStartTime { get; set; }

        private static List<ProcessID> _databaseProcessID = [];
        private static List<ProcessID> _worldProcessesID = [];
        private static List<ProcessID> _logonProcessesID = [];

        #region "Database Process ID CRUD"
        public static void AddToDatabaseProcessID(ProcessID processID)
        {
            lock (_databaseLock)
            {
                _databaseProcessID.Add(processID);
            }
        }
        public static bool RemoveFromDatabaseProcessID(ProcessID processID)
        {
            lock (_databaseLock)
            {
                return _databaseProcessID.Remove(processID);
            }
        }
        public static void CleanDatabaseProcessID()
        {
            lock (_databaseLock) { _databaseProcessID.Clear(); }
        }
        public static ReadOnlyCollection<ProcessID> GetDatabaseProcessID()
        {
            lock (_databaseLock)
            {
                return _databaseProcessID.AsReadOnly();
            }
        }
        // Pagination method
        public static List<ProcessID> GetDatabaseProcessIDPage(int pageNumber, int pageSize)
        {
            lock (_databaseLock)
            {
                return _databaseProcessID
                    .Skip((pageNumber - 1) * pageSize) // Skip items from previous pages
                    .Take(pageSize)                   // Take the desired number of items
                    .ToList();
            }
        }
        public static int GetTotalDatabaseProcessIDCount()
        {
            lock (_databaseLock)
            {
                return _databaseProcessID.Count;
            }
        }
        #endregion
        #region "World Process ID CRUD"
        public static void AddToWorldProcessesID(ProcessID processID)
        {
            lock (_worldLock)
            {
                _worldProcessesID.Add(processID);
            }
        }
        public static bool RemoveFromWorldProcessesID(ProcessID processID)
        {
            lock (_worldLock)
            {
                return _worldProcessesID.Remove(processID);
            }
        }
        public static ReadOnlyCollection<ProcessID> GetWorldProcessesID()
        {
            lock (_worldLock)
            {
                return _worldProcessesID.AsReadOnly();
            }
        }
        // Pagination method
        public static List<ProcessID> GetWorldProcessesIDPage(int pageNumber, int pageSize)
        {
            lock (_worldLock)
            {
                return _worldProcessesID
                    .Skip((pageNumber - 1) * pageSize) // Skip items from previous pages
                    .Take(pageSize)                   // Take the desired number of items
                    .ToList();
            }
        }
        public static int GetTotalWorldProcessIDCount()
        {
            lock (_worldLock)
            {
                return _worldProcessesID.Count;
            }
        }
        public static void CleanWolrdProcessID()
        {
            lock (_worldLock) { _worldProcessesID.Clear(); }
        }
        #endregion
        #region "Logon Process ID CRUD"
        public static void AddToLogonProcessesID(ProcessID processID)
        {
            lock (_logonLock)
            {
                _logonProcessesID.Add(processID);
            }
        }
        public static void CleanLogonProcessID()
        {
            lock (_logonLock) { _logonProcessesID.Clear(); }
        }
        public static bool RemoveFromLogonProcessesID(ProcessID processID)
        {
            lock (_logonLock)
            {
                return _logonProcessesID.Remove(processID);
            }
        }
        public static ReadOnlyCollection<ProcessID> GetLogonProcessesID()
        {
            lock (_logonLock)
            {
                return _logonProcessesID.AsReadOnly();
            }
        }
        // Pagination method
        public static List<ProcessID> GetLogonProcessesIDPage(int pageNumber, int pageSize)
        {
            lock (_logonLock)
            {
                return _logonProcessesID
                    .Skip((pageNumber - 1) * pageSize) // Skip items from previous pages
                    .Take(pageSize)                   // Take the desired number of items
                    .ToList();
            }
        }
        public static int GetTotalLogonProcessIDCount()
        {
            lock (_logonLock)
            {
                return _logonProcessesID.Count;
            }
        }
        #endregion
    }
}
