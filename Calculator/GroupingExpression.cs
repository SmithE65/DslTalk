namespace Calculator;

public class GroupingExpression(Expression expression) : Expression(expression.Token)
{
    public Expression Expression { get; } = expression;
}
