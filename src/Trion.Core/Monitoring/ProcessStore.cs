using System.Collections.Concurrent;
using System.Text.Json;

namespace Trion.Core.Monitoring;

public sealed class ProcessStore
{
    private readonly string _path;
    private readonly string _tmpPath;
    private readonly string _bakPath;
    private readonly ConcurrentDictionary<string, int> _cache;
    private readonly Lock _saveLock = new();

    public ProcessStore(string? fileName = null)
    {
        _path    = fileName ?? Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "Trion", "process-store.json");
        _tmpPath = _path + ".tmp";
        _bakPath = _path + ".bak";

        Directory.CreateDirectory(Path.GetDirectoryName(_path)!);
        _cache = Load();
    }

    public void Add(string name, int pid) { _cache[name] = pid; Save(); }
    public void Remove(string name)       { _cache.TryRemove(name, out _); Save(); }
    public bool TryGetPid(string name, out int pid) => _cache.TryGetValue(name, out pid);
    public IReadOnlyDictionary<string, int> GetAll() => _cache;

    public static bool IsAlive(int pid)
    {
        try { using var p = System.Diagnostics.Process.GetProcessById(pid); return true; }
        catch { return false; }
    }

    private void Save()
    {
        lock (_saveLock)
        {
            // Write to temp file first so a crash mid-write doesn't corrupt the main file
            File.WriteAllText(_tmpPath, JsonSerializer.Serialize(_cache));
            File.Replace(_tmpPath, _path, _bakPath);

            // Clean up backup after successful replace
            if (File.Exists(_bakPath))
                File.Delete(_bakPath);
        }
    }

    private ConcurrentDictionary<string, int> Load()
    {
        if (!File.Exists(_path)) return [];
        try
        {
            var raw = File.ReadAllText(_path);
            return new(JsonSerializer.Deserialize<Dictionary<string, int>>(raw) ?? []);
        }
        catch { return []; }
    }
}
