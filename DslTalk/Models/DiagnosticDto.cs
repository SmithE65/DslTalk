namespace DslTalk.Models;

public sealed record DiagnosticDto(
    string Phase,
    string Message,
    int? Start,
    int Length);
