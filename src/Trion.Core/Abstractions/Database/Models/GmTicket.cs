namespace Trion.Core.Abstractions.Database.Models;

public sealed record GmTicket(
    int Id,
    string AuthorName,
    string Text,
    DateTimeOffset CreatedAt,
    bool Closed,
    string? AssignedTo = null);
