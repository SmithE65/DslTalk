namespace DslTalk.Models;

public sealed record InterpretResultDto(
    string DslText,
    string? InputText,
    IReadOnlyList<TokenDto> Tokens,
    AstNodeDto? Ast,
    string? ResultText,
    DiagnosticDto? Diagnostic);
