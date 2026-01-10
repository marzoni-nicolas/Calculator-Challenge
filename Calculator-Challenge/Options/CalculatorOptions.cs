namespace Calculator_Challenge.Options;

public interface ICalculatorOptions
{
    char AlternateDelimiter { get; init; }
    bool DenyNegativeNumbers { get; init; }
    int MaxAllowedValue { get; init; }
}

public sealed class CalculatorOptions : ICalculatorOptions
{
    /// <summary>
    /// Alternate delimiter for newline support
    /// </summary>
    public char AlternateDelimiter { get; init; } = '\n';

    /// <summary>
    /// Toggle negative number validation
    /// </summary>
    public bool DenyNegativeNumbers { get; init; } = true;

    /// <summary>
    /// Upper bound for valid numbers
    /// </summary>
    public int MaxAllowedValue { get; init; } = 1000;
}

