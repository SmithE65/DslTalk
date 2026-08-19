namespace Calculator;

public class UnaryExpression(Token operatorToken, Expression right) : Expression(operatorToken)
{
    public Expression Right { get; } = right;
}