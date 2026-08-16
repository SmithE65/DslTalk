namespace Calculator;

public class Scanner(string source)
{
    private int _start = 0;
    private int _current = 0;
    private int _line = 1;
    private int _lineChar = 0;

    private readonly string _source = source;
    private readonly List<Token> _tokens = [];

    public IEnumerable<Token> Scan()
    {
        while (!IsEoF())
        {
            _start = _current;
            ScanNextToken();
        }

        _tokens.Add(new Token(TokenType.EOF, _source, _source.Length, 0, _line));
        return _tokens;
    }

    private bool IsEoF() => _current >= _source.Length;

    private char Advance()
    {
        _lineChar++;
        return _source[_current++];
    }

    private char Peek() => IsEoF() ? '\0' : _source[_current];

    private char PeekNext() => (_current + 1) >= _source.Length ? '\0' : _source[_current + 1];

    private void AddToken(TokenType tokenType)
    {
        _tokens.Add(new Token(tokenType, _source, _start, _current - _start, _line));
    }

    private void ScanNextToken()
    {
        char c = Advance();

        switch (c)
        {
            case '+':
                AddToken(TokenType.Plus);
                break;
            case '-':
                AddToken(TokenType.Minus);
                break;
            case '*':
                AddToken(TokenType.Multiply);
                break;
            case '/':
                AddToken(TokenType.Divide);
                break;
            case '(':
                AddToken(TokenType.LeftParen);
                break;
            case ')':
                AddToken(TokenType.RightParen);
                break;
            default:
                if (char.IsDigit(c))
                {
                    ScanNumber();
                }
                else if (!char.IsWhiteSpace(c))
                {
                    throw new ScanException($"{_line}:{_lineChar} Unexpected character: {c}", _line);
                }
                else if (c == '\n')
                {
                    _line++;
                    _lineChar = 0;
                }
                break;
        }
    }

    private void ScanNumber()
    {
        while (char.IsDigit(Peek()))
        {
            Advance();
        }
        if (Peek() == '.' && char.IsDigit(PeekNext()))
        {
            Advance();
            while (char.IsDigit(Peek()))
            {
                Advance();
            }
        }
        AddToken(TokenType.Literal);
    }


}
