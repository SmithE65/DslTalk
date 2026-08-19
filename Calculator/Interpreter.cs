namespace Calculator;

public class Interpreter(Expression expression)
{
    private readonly Expression _expression = expression;

    public double Interpret()
    {
        return Evaluate(_expression);
    }

    private static double Evaluate(Expression expression)
    {
        switch (expression)
        {
            case UnaryExpression unary:
                double operand = Evaluate(unary.Right);
                return unary.Token.Type switch
                {
                    TokenType.Plus => operand,
                    TokenType.Minus => -operand,
                    _ => throw new Exception($"Unknown operator: {unary.Token.GetLexeme()}")
                };
            case LiteralExpression literal:
                return literal.Value;
            case BinaryExpression binary:
                double left = Evaluate(binary.Left);
                double right = Evaluate(binary.Right);
                return binary.Token.Type switch
                {
                    TokenType.Plus => left + right,
                    TokenType.Minus => left - right,
                    TokenType.Multiply => left * right,
                    TokenType.Divide => left / right,
                    _ => throw new Exception($"Unknown operator: {binary.Token.GetLexeme()}")
                };
            case GroupingExpression grouping:
                return Evaluate(grouping.Expression);
            default:
                throw new Exception("Unknown expression type.");
        }
    }
}
