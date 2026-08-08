namespace Calculator;

public class BinaryExpression(Expression left, Token operatorToken, Expression right) : Expression(operatorToken)
{
    public Expression Left { get; } = left;
    public Expression Right { get; } = right;
}
