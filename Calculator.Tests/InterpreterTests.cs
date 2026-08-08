namespace Calculator.Tests;

public class InterpreterTests
{
    [Theory]
    [InlineData("1 + 2", 3)]
    [InlineData("2 * 3", 6)]
    [InlineData("4 / 2", 2)]
    [InlineData("5 - 3", 2)]
    [InlineData("1 + 2 * 3", 7)]
    [InlineData("(1 + 2) * 3", 9)]
    [InlineData("3 + 4 * (2 - 1)", 7)]
    public void Interpret_ValidExpressions_ReturnsExpectedResults(string input, double expected)
    {
        var scanner = new Scanner(input);
        var tokens = scanner.Scan();
        var parser = new Parser(tokens);
        var expression = parser.Parse();
        var interpreter = new Interpreter(expression);
        var result = interpreter.Interpret();
        Assert.Equal(expected, result);
    }
}
