namespace Calculator;

public class ScanException(string message, int lineNumber) : Exception(message)
{
    public int LineNumber { get; } = lineNumber;
}
