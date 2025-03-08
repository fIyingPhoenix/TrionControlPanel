
using System.Collections.ObjectModel;
using TrionControlPanel.Desktop.Extensions.Modules.Lists;
namespace TrionControlPanel.Desktop.Extensions.Classes.Data.Form
{
    // Manages system data related to process IDs for database, world, and logon processes.
    public class SystemData
    {
        private static readonly object _databaseLock = new(); // Lock object for database process ID operations.
        private static readonly object _worldLock = new(); // Lock object for world process ID operations.
        private static readonly object _logonLock = new(); // Lock object for logon process ID operations.

        public static DateTime DatabaseStartTime { get; set; } // Stores the start time of the database process.
        public static DateTime WorldStartTime { get; set; } // Stores the start time of the world process.
        public static DateTime LogonStartTime { get; set; } // Stores the start time of the logon process.

        private static List<ProcessID> _databaseProcessID = new(); // List to store database process IDs.
        private static List<ProcessID> _worldProcessesID = new(); // List to store world process IDs.
        private static List<ProcessID> _logonProcessesID = new(); // List to store logon process IDs.

        #region "Database Process ID CRUD"
        // Adds a process ID to the database process ID list.
        public static void AddToDatabaseProcessID(ProcessID processID)
        {
            lock (_databaseLock)
            {
                _databaseProcessID.Add(processID);
            }
        }

        // Removes a process ID from the database process ID list.
        public static bool RemoveFromDatabaseProcessID(ProcessID processID)
        {
            lock (_databaseLock)
            {
                return _databaseProcessID.Remove(processID);
            }
        }

        // Clears all process IDs from the database process ID list.
        public static void CleanDatabaseProcessID()
        {
            lock (_databaseLock) { _databaseProcessID.Clear(); }
        }

        // Retrieves a read-only collection of database process IDs.
        public static ReadOnlyCollection<ProcessID> GetDatabaseProcessID()
        {
            lock (_databaseLock)
            {
                return _databaseProcessID.AsReadOnly();
            }
        }

        // Retrieves a paginated list of database process IDs.
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

        // Retrieves the total count of database process IDs.
        public static int GetTotalDatabaseProcessIDCount()
        {
            lock (_databaseLock)
            {
                return _databaseProcessID.Count;
            }
        }
        #endregion

        #region "World Process ID CRUD"
        // Adds a process ID to the world process ID list.
        public static void AddToWorldProcessesID(ProcessID processID)
        {
            lock (_worldLock)
            {
                _worldProcessesID.Add(processID);
            }
        }

        // Removes a process ID from the world process ID list.
        public static bool RemoveFromWorldProcessesID(ProcessID processID)
        {
            lock (_worldLock)
            {
                return _worldProcessesID.Remove(processID);
            }
        }

        // Retrieves a read-only collection of world process IDs.
        public static ReadOnlyCollection<ProcessID> GetWorldProcessesID()
        {
            lock (_worldLock)
            {
                return _worldProcessesID.AsReadOnly();
            }
        }

        // Retrieves a paginated list of world process IDs.
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

        // Retrieves the total count of world process IDs.
        public static int GetTotalWorldProcessIDCount()
        {
            lock (_worldLock)
            {
                return _worldProcessesID.Count;
            }
        }

        // Clears all process IDs from the world process ID list.
        public static void CleanWolrdProcessID()
        {
            lock (_worldLock) { _worldProcessesID.Clear(); }
        }
        #endregion

        #region "Logon Process ID CRUD"
        // Adds a process ID to the logon process ID list.
        public static void AddToLogonProcessesID(ProcessID processID)
        {
            lock (_logonLock)
            {
                _logonProcessesID.Add(processID);
            }
        }

        // Clears all process IDs from the logon process ID list.
        public static void CleanLogonProcessID()
        {
            lock (_logonLock) { _logonProcessesID.Clear(); }
        }

        // Removes a process ID from the logon process ID list.
        public static bool RemoveFromLogonProcessesID(ProcessID processID)
        {
            lock (_logonLock)
            {
                return _logonProcessesID.Remove(processID);
            }
        }

        // Retrieves a read-only collection of logon process IDs.
        public static ReadOnlyCollection<ProcessID> GetLogonProcessesID()
        {
            lock (_logonLock)
            {
                return _logonProcessesID.AsReadOnly();
            }
        }

        // Retrieves a paginated list of logon process IDs.
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

        // Retrieves the total count of logon process IDs.
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
