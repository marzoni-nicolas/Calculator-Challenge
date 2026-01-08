
using System;

namespace Calculator_Challenge.Services;


public interface INumberParser 
{
    IReadOnlyList<int> Parse(string input);
}

public sealed class NumberParser : INumberParser
{
    private static readonly char[] delimiters = { ',', '\n' };

    /// <summary>
    /// Parse the input string into list of integers. Empty values and invalid integers are parsed as zero.
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public IReadOnlyList<int> Parse(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            return new[] { 0};
        }
                
    var parts = input.Split(delimiters);

        var numbers = parts
            .Select(ParseNumber)
            .ToList();

        return numbers;
    }

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
