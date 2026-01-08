using Calculator_Challenge.Exceptions;

namespace Calculator_Challenge.Services;

public interface ICalculator
{
    int Add(string input);
}

public sealed class Calculator : ICalculator
{
    public int Add(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            return 0;
        }

        // Keeping this simple as first iteration. "Add" method could accept two arguments a int and b int, so we just do a+b.
        var parts = input.Split(',');

        if (parts.Length > 2)
        {
            throw new TooManyNumbersException(parts.Length);
        }

        return parts.Sum(ParseNumber);
    }

    /// <summary>
    /// Parse string into a number. Returns the numeric representation of the string or 0 if the string is an invalid int.
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    private static int ParseNumber(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return 0;
        }

        return int.TryParse(value, out var number)
            ? number
            : 0;
    }
}