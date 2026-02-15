// =============================================================================
// SystemData.cs
// Purpose: Manages system data related to process IDs for database, world, and logon processes
// Dependencies: ProcessID model, ConcurrentDictionary
// Step 7 of IMPROVEMENTS2.md - Replace lock-based collections with ConcurrentCollections
// =============================================================================

using System.Collections.Concurrent;
using System.Collections.ObjectModel;
using TrionControlPanel.Desktop.Extensions.Modules.Lists;

namespace TrionControlPanel.Desktop.Extensions.Classes.Data.Form
{
    /// <summary>
    /// Manages system data related to process IDs for database, world, and logon processes.
    /// Provides thread-safe CRUD operations for tracking server processes using ConcurrentDictionary.
    /// </summary>
    public class SystemData
    {
        #region Properties - Start Times
        // ─────────────────────────────────────────────────────────────────────

        /// <summary>
        /// Gets or sets the start time of the database process.
        /// </summary>
        public static DateTime DatabaseStartTime { get; set; }

        /// <summary>
        /// Gets or sets the start time of the world process.
        /// </summary>
        public static DateTime WorldStartTime { get; set; }

        /// <summary>
        /// Gets or sets the start time of the logon process.
        /// </summary>
        public static DateTime LogonStartTime { get; set; }

        #endregion

        #region Fields - Process Collections
        // ─────────────────────────────────────────────────────────────────────

        /// <summary>
        /// Thread-safe dictionary to store database process IDs, keyed by process ID.
        /// </summary>
        private static readonly ConcurrentDictionary<int, ProcessID> _databaseProcesses = new();

        /// <summary>
        /// Thread-safe dictionary to store world process IDs, keyed by process ID.
        /// </summary>
        private static readonly ConcurrentDictionary<int, ProcessID> _worldProcesses = new();

        /// <summary>
        /// Thread-safe dictionary to store logon process IDs, keyed by process ID.
        /// </summary>
        private static readonly ConcurrentDictionary<int, ProcessID> _logonProcesses = new();

        #endregion

        #region Public Methods - Database Process ID CRUD
        // ─────────────────────────────────────────────────────────────────────

        /// <summary>
        /// Adds a process ID to the database process tracking.
        /// </summary>
        /// <param name="processID">The process ID to add.</param>
        public static void AddToDatabaseProcessID(ProcessID processID)
        {
            _databaseProcesses.TryAdd(processID.ID, processID);
        }

        /// <summary>
        /// Removes a process ID from the database process tracking.
        /// </summary>
        /// <param name="processID">The process ID to remove.</param>
        /// <returns>True if the item was removed, false otherwise.</returns>
        public static bool RemoveFromDatabaseProcessID(ProcessID processID)
        {
            return _databaseProcesses.TryRemove(processID.ID, out _);
        }

        /// <summary>
        /// Clears all process IDs from the database process tracking.
        /// </summary>
        public static void CleanDatabaseProcessID()
        {
            _databaseProcesses.Clear();
        }

        /// <summary>
        /// Retrieves a read-only collection of database process IDs.
        /// </summary>
        /// <returns>A read-only snapshot of database process IDs.</returns>
        public static ReadOnlyCollection<ProcessID> GetDatabaseProcessID()
        {
            return _databaseProcesses.Values.ToList().AsReadOnly();
        }

        /// <summary>
        /// Retrieves a paginated list of database process IDs.
        /// </summary>
        /// <param name="pageNumber">The page number (1-based).</param>
        /// <param name="pageSize">The number of items per page.</param>
        /// <returns>A list of process IDs for the specified page.</returns>
        public static List<ProcessID> GetDatabaseProcessIDPage(int pageNumber, int pageSize)
        {
            return _databaseProcesses.Values
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }

        /// <summary>
        /// Retrieves the total count of database process IDs.
        /// </summary>
        /// <returns>The count of database process IDs.</returns>
        public static int GetTotalDatabaseProcessIDCount()
        {
            return _databaseProcesses.Count;
        }

        #endregion

        #region Public Methods - World Process ID CRUD
        // ─────────────────────────────────────────────────────────────────────

        /// <summary>
        /// Adds a process ID to the world process tracking.
        /// </summary>
        /// <param name="processID">The process ID to add.</param>
        public static void AddToWorldProcessesID(ProcessID processID)
        {
            _worldProcesses.TryAdd(processID.ID, processID);
        }

        /// <summary>
        /// Removes a process ID from the world process tracking.
        /// </summary>
        /// <param name="processID">The process ID to remove.</param>
        /// <returns>True if the item was removed, false otherwise.</returns>
        public static bool RemoveFromWorldProcessesID(ProcessID processID)
        {
            return _worldProcesses.TryRemove(processID.ID, out _);
        }

        /// <summary>
        /// Retrieves a read-only collection of world process IDs.
        /// </summary>
        /// <returns>A read-only snapshot of world process IDs.</returns>
        public static ReadOnlyCollection<ProcessID> GetWorldProcessesID()
        {
            return _worldProcesses.Values.ToList().AsReadOnly();
        }

        /// <summary>
        /// Retrieves a paginated list of world process IDs.
        /// </summary>
        /// <param name="pageNumber">The page number (1-based).</param>
        /// <param name="pageSize">The number of items per page.</param>
        /// <returns>A list of process IDs for the specified page.</returns>
        public static List<ProcessID> GetWorldProcessesIDPage(int pageNumber, int pageSize)
        {
            return _worldProcesses.Values
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }

        /// <summary>
        /// Retrieves the total count of world process IDs.
        /// </summary>
        /// <returns>The count of world process IDs.</returns>
        public static int GetTotalWorldProcessIDCount()
        {
            return _worldProcesses.Count;
        }

        /// <summary>
        /// Clears all tracked world server process IDs.
        /// </summary>
        public static void ClearWorldProcessIds()
        {
            _worldProcesses.Clear();
        }

        #endregion

        #region Public Methods - Logon Process ID CRUD
        // ─────────────────────────────────────────────────────────────────────

        /// <summary>
        /// Adds a process ID to the logon process tracking.
        /// </summary>
        /// <param name="processID">The process ID to add.</param>
        public static void AddToLogonProcessesID(ProcessID processID)
        {
            _logonProcesses.TryAdd(processID.ID, processID);
        }

        /// <summary>
        /// Clears all process IDs from the logon process tracking.
        /// </summary>
        public static void CleanLogonProcessID()
        {
            _logonProcesses.Clear();
        }

        /// <summary>
        /// Removes a process ID from the logon process tracking.
        /// </summary>
        /// <param name="processID">The process ID to remove.</param>
        /// <returns>True if the item was removed, false otherwise.</returns>
        public static bool RemoveFromLogonProcessesID(ProcessID processID)
        {
            return _logonProcesses.TryRemove(processID.ID, out _);
        }

        /// <summary>
        /// Retrieves a read-only collection of logon process IDs.
        /// </summary>
        /// <returns>A read-only snapshot of logon process IDs.</returns>
        public static ReadOnlyCollection<ProcessID> GetLogonProcessesID()
        {
            return _logonProcesses.Values.ToList().AsReadOnly();
        }

        /// <summary>
        /// Retrieves a paginated list of logon process IDs.
        /// </summary>
        /// <param name="pageNumber">The page number (1-based).</param>
        /// <param name="pageSize">The number of items per page.</param>
        /// <returns>A list of process IDs for the specified page.</returns>
        public static List<ProcessID> GetLogonProcessesIDPage(int pageNumber, int pageSize)
        {
            return _logonProcesses.Values
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }

        /// <summary>
        /// Retrieves the total count of logon process IDs.
        /// </summary>
        /// <returns>The count of logon process IDs.</returns>
        public static int GetTotalLogonProcessIDCount()
        {
            return _logonProcesses.Count;
        }

        #endregion
    }
}
