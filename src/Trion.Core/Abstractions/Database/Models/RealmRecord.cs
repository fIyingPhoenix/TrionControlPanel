namespace Trion.Core.Abstractions.Database.Models;

public sealed record RealmRecord(
    int Id,
    string Name,
    string Address,
    int Port,
    bool Online,
    int Population = 0);
