using System.Collections.Concurrent;
using System.IO;
using System.Text.Json;
using System.Threading;

namespace Trion.Core.Monitoring;

public sealed class ProcessStore
{
    private readonly string _path;
    private readonly ConcurrentDictionary<string, int> _cache;
    private readonly ReaderWriterLockSlim _lock = new();

    public ProcessStore(string? fileName = null)
    {
        _path = fileName ?? Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "Trion", "process-store.json");
        Directory.CreateDirectory(Path.GetDirectoryName(_path)!);
        _cache = Load();
    }

    public void Add(string name, int pid) { _cache[name] = pid; Save(); }
    public void Remove(string name) { _cache.TryRemove(name, out _); Save(); }
    public bool TryGetPid(string name, out int pid) => _cache.TryGetValue(name, out pid);
    public IReadOnlyDictionary<string, int> GetAll() => _cache;

    public static bool IsAlive(int pid) { try { using var p = System.Diagnostics.Process.GetProcessById(pid); return true; } catch { return false; } }

    private void Save() { _lock.EnterWriteLock(); try { File.WriteAllText(_path, JsonSerializer.Serialize(_cache)); } finally { _lock.ExitWriteLock(); } }
    private ConcurrentDictionary<string, int> Load()
    {
        _lock.EnterReadLock();
        try
        {
            if (!File.Exists(_path)) return new();
            var raw = File.ReadAllText(_path);
            return new(JsonSerializer.Deserialize<Dictionary<string, int>>(raw) ?? new());
        }
        finally { _lock.ExitReadLock(); }
    }
}