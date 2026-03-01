namespace Trion.Core.Services.Emulator;

public sealed record ExtractProgressEvent(
    string StepName,
    int    PercentComplete,   // 0–100
    string? Message  = null,
    bool    IsError  = false);
