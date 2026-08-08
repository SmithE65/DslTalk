namespace Calculator.Tests;

public class ScannerTests
{
    [Theory]
    [InlineData("+", TokenType.Plus)]
    [InlineData("-", TokenType.Minus)]
    [InlineData("*", TokenType.Multiply)]
    [InlineData("/", TokenType.Divide)]
    [InlineData("(", TokenType.LeftParen)]
    [InlineData(")", TokenType.RightParen)]
    [InlineData("123", TokenType.Literal)]
    public void Scan_SingleTokenInputs_MatchTokenType(string input, TokenType expectedTokenType)
    {
        var sut = new Scanner(input);

        var tokens = sut.Scan();

        Assert.Equal(2, tokens.Count());
        Assert.Equal(expectedTokenType, tokens.First().Type);
    }

    [Fact]
    public void Scan_ComplexExpression_MatchTokenTypes()
    {
        var input = "3 + 4 * (2 - 1)";
        var expectedTokenTypes = new List<TokenType>
        {
            TokenType.Literal,
            TokenType.Plus,
            TokenType.Literal,
            TokenType.Multiply,
            TokenType.LeftParen,
            TokenType.Literal,
            TokenType.Minus,
            TokenType.Literal,
            TokenType.RightParen,
            TokenType.EOF
        };
        var sut = new Scanner(input);
        var tokens = sut.Scan();
        Assert.Equal(expectedTokenTypes.Count, tokens.Count());
        for (int i = 0; i < expectedTokenTypes.Count; i++)
        {
            Assert.Equal(expectedTokenTypes[i], tokens.ElementAt(i).Type);
        }
    }
}
