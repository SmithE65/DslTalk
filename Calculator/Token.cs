namespace Calculator;

public class Token(TokenType tokenType, string source, int start, int length, int lineNumber)
{
    public TokenType Type { get; } = tokenType;
    public int Start { get; } = start;
    public int Length { get; } = length;
    public int LineNumber { get; } = lineNumber;

    public string GetLexeme() => source.Substring(Start, Length);
}
