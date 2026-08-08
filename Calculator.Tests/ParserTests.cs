namespace Calculator.Tests;

public class ParserTests
{
    [Fact]
    public void ParsesLiteralExpression()
    {
        const string input = "42";
        var scanner = new Scanner(input);
        var tokens = scanner.Scan();
        var parser = new Parser(tokens);
        var expression = parser.Parse();
        Assert.NotNull(expression);
        var literalExpression = Assert.IsType<LiteralExpression>(expression);
        Assert.Equal(42, literalExpression.Value);
    }

    [Fact]
    public void ParsesBinaryExpression()
    {
        const string input = "1 + 2";
        var scanner = new Scanner(input);
        var tokens = scanner.Scan();
        var parser = new Parser(tokens);
        var expression = parser.Parse();
        Assert.NotNull(expression);
        var binaryExpression = Assert.IsType<BinaryExpression>(expression);
        var left = Assert.IsType<LiteralExpression>(binaryExpression.Left);
        Assert.Equal(1, left.Value);
        var right = Assert.IsType<LiteralExpression>(binaryExpression.Right);
        Assert.Equal(2, right.Value);
    }

    [Fact]
    public void HandlesPrecedenceCorrectly()
    {
        const string input = "1 + 2 * 3";
        var scanner = new Scanner(input);
        var tokens = scanner.Scan();
        var parser = new Parser(tokens);
        var expression = parser.Parse();
        Assert.NotNull(expression);

        // The top-level expression should be a BinaryExpression for '+'
        var topLevelExpression = Assert.IsType<BinaryExpression>(expression);
        var left = Assert.IsType<LiteralExpression>(topLevelExpression.Left);
        Assert.Equal(1, left.Value);

        // The right side should be a BinaryExpression for '*'
        var right = Assert.IsType<BinaryExpression>(topLevelExpression.Right);
        left = Assert.IsType<LiteralExpression>(right.Left);
        Assert.Equal(2, left.Value);
        var rightLiteral = Assert.IsType<LiteralExpression>(right.Right);
        Assert.Equal(3, rightLiteral.Value);
    }

    [Fact]
    public void ParsesGroupingExpression()
    {
        const string input = "(1 + 2) * 3";
        var scanner = new Scanner(input);
        var tokens = scanner.Scan();
        var parser = new Parser(tokens);
        var expression = parser.Parse();
        Assert.NotNull(expression);
        // The top-level expression should be a BinaryExpression for '*'
        var topLevelExpression = Assert.IsType<BinaryExpression>(expression);
        var leftGrouping = Assert.IsType<GroupingExpression>(topLevelExpression.Left);

        // Inside the grouping, we should have a BinaryExpression for '+'
        var innerExpression = Assert.IsType<BinaryExpression>(leftGrouping.Expression);
        var left = Assert.IsType<LiteralExpression>(innerExpression.Left);
        Assert.Equal(1, left.Value);
        var right = Assert.IsType<LiteralExpression>(innerExpression.Right);
        Assert.Equal(2, right.Value);
        // The right side of the top-level expression should be a LiteralExpression for '3'
        var rightLiteral = Assert.IsType<LiteralExpression>(topLevelExpression.Right);
        Assert.Equal(3, rightLiteral.Value);
    }

    [Fact]
    public void ParsesComplexInputs()
    {
        const string input = "3 + 4 * (2 - 1)";
        var scanner = new Scanner(input);
        var tokens = scanner.Scan();
        var parser = new Parser(tokens);
        var expression = parser.Parse();

        Assert.NotNull(expression);
        
        // 3 + ...
        var topLevelExpression = Assert.IsType<BinaryExpression>(expression);
        var left = Assert.IsType<LiteralExpression>(topLevelExpression.Left);
        Assert.Equal(3, left.Value);
        var right = Assert.IsType<BinaryExpression>(topLevelExpression.Right);

        // 4 * ...
        topLevelExpression = right;
        left = Assert.IsType<LiteralExpression>(topLevelExpression.Left);
        Assert.Equal(4, left.Value);
        var group = Assert.IsType<GroupingExpression>(topLevelExpression.Right);

        // 2 - 1
        topLevelExpression = Assert.IsType<BinaryExpression>(group.Expression);
        left = Assert.IsType<LiteralExpression>(topLevelExpression.Left);
        Assert.Equal(2, left.Value);
        var rightLiteral = Assert.IsType<LiteralExpression>(topLevelExpression.Right);
        Assert.Equal(1, rightLiteral.Value);
    }

    [Fact]
    public void Parse_ThrowsOnUnexpectedToken()
    {
        const string input = "1 + * 2";
        var scanner = new Scanner(input);
        var tokens = scanner.Scan();
        var parser = new Parser(tokens);
        var exception = Assert.Throws<ParseException>(() => parser.Parse());
        Assert.Equal(TokenType.Multiply, exception.Token.Type);
    }
}
