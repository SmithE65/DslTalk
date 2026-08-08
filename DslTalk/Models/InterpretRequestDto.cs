namespace DslTalk.Models;

public class InterpretRequestDto
{
    public required string DslText { get; set; }
    public string? Input { get; set; }
}
