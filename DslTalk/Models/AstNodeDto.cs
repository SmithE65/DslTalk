namespace DslTalk.Models;

public sealed record AstNodeDto(
    string Kind,
    string? Label,
    IReadOnlyList<AspPropertyNode> Properties,
    IReadOnlyList<AstNodeDto?> Children);
