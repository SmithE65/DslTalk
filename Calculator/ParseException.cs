namespace Calculator;

public class ParseException(string message, Token token) : Exception(message)
{
    public Token Token { get; } = token;
}
