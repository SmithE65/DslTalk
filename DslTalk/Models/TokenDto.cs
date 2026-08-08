namespace DslTalk.Models;

public sealed record TokenDto(
    string Type,
    string Lexeme,
    string? Value,
    int Start,
    int Length);
