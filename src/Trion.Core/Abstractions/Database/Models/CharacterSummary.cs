namespace Trion.Core.Abstractions.Database.Models;

public sealed record CharacterSummary(
    int Id,
    string Name,
    int Level,
    int Race,
    int Class,
    int AccountId);
