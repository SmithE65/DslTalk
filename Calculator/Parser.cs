namespace Calculator;

public class Parser(IEnumerable<Token> tokens)
{
    private readonly List<Token> _tokens = tokens.ToList();

    private int _current = 0;

    public Expression Parse()
    {
        return ParseExpression();
    }

    private Expression ParseExpression()
    {
        return ParseTerm();
    }

    /// <summary>
    /// Parses a term expression, which can be a factor or a series of factors combined with addition or subtraction operators.
    /// </summary>
    /// <returns>The parsed term expression.</returns>
    private Expression ParseTerm()
    {
        Expression expression = ParseFactor();

        while (Match(TokenType.Plus, TokenType.Minus))
        {
            Token operatorToken = Previous();
            Expression right = ParseFactor();
            expression = new BinaryExpression(expression, operatorToken, right);
        }

        return expression;
    }

    /// <summary>
    /// Parses a factor expression, which can be a primary expression or a multiplication/division of primary expressions.
    /// </summary>
    /// <returns>The parsed factor expression.</returns>
    private Expression ParseFactor()
    {
        Expression expression = ParsePrimary();

        while (Match(TokenType.Multiply, TokenType.Divide))
        {
            Token operatorToken = Previous();
            Expression right = ParsePrimary();
            expression = new BinaryExpression(expression, operatorToken, right);
        }

        return expression;
    }

    /// <summary>
    /// Parses a primary expression, which can be a literal or a grouped expression.
    /// If the next token is a literal, it creates a LiteralExpression.
    /// If the next token is a left parenthesis, it parses the expression inside the parentheses and creates a GroupingExpression.
    /// If neither case applies, it throws an exception indicating that an expression was expected.
    /// </summary>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    private Expression ParsePrimary()
    {
        if (Match(TokenType.Plus, TokenType.Minus))
        {
            Token operatorToken = Previous();
            Expression right = ParsePrimary();
            return new UnaryExpression(operatorToken, right);
        }

        if (Match(TokenType.Literal))
        {
            return new LiteralExpression(Previous());
        }

        if (Match(TokenType.LeftParen))
        {
            Expression expression = ParseExpression();
            Consume(TokenType.RightParen, "Expect ')' after expression.");
            return expression;
        }

        throw new ParseException("Expect expression.", Peek());
    }

    /// <summary>
    /// Checks if the next token is of any of the given types. If it is, consumes the token and returns true. Otherwise, returns false.
    /// </summary>
    /// <param name="types"></param>
    /// <returns></returns>
    private bool Match(params TokenType[] types)
    {
        foreach (TokenType type in types)
        {
            if (Check(type))
            {
                Advance();
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// Checks if the next token is of the given type without consuming it. Returns true if it is, false otherwise.
    /// </summary>
    /// <param name="type">The type of the token to check.</param>
    /// <returns>True if the next token is of the given type, false otherwise.</returns>
    private bool Check(TokenType type)
    {
        if (IsAtEnd()) return false;
        return Peek().Type == type;
    }

    /// <summary>
    /// Advances the current token index and returns the previous token.
    /// </summary>
    /// <returns>The previous token.</returns>
    private Token Advance()
    {
        if (!IsAtEnd()) _current++;
        return Previous();
    }

    /// <summary>
    /// Checks if the parser has reached the end of the token list.
    /// </summary>
    /// <returns>True if the parser has reached the end of the token list, false otherwise.</returns>
    private bool IsAtEnd()
    {
        return Peek().Type == TokenType.EOF;
    }

    /// <summary>
    /// Returns the current token without consuming it.
    /// </summary>
    /// <returns>The current token.</returns>
    private Token Peek()
    {
        return _tokens[_current];
    }

    /// <summary>
    /// Returns the previous token without consuming it.
    /// </summary>
    /// <returns>The previous token.</returns>
    private Token Previous()
    {
        return _tokens[_current - 1];
    }

    /// <summary>
    /// Consumes the next token if it is of the given type. If it is not, throws an exception with the given message.
    /// </summary>
    /// <param name="type">The type of the token to consume.</param>
    /// <param name="message">The message for the exception if the token is not of the given type.</param>
    /// <returns>The consumed token.</returns>
    /// <exception cref="Exception"></exception>
    private Token Consume(TokenType type, string message)
    {
        if (Check(type))
        {
            return Advance();
        }

        throw new Exception(message);
    }
}
