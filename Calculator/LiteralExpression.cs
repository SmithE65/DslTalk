namespace Calculator;

public class LiteralExpression(Token literalToken) : Expression(literalToken)
{
    public double Value { get; } = double.Parse(literalToken.GetLexeme());
}
