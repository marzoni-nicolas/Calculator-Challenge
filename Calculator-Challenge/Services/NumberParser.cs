
using System;
using System.Text.RegularExpressions;

namespace Calculator_Challenge.Services;


public interface INumberParser 
{
    IReadOnlyList<int> Parse(string input);
}

public sealed class NumberParser : INumberParser
{
    private static readonly IList<string> _defaultDelimiters = new[] { ",", "\n" };
    private static readonly string _regexDelimiterPattern = @"^//(\[(.+?)\]|(.))\n";

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

        var delimiters = new List<string>(_defaultDelimiters);
        var numbersSection = input;

        if (HasCustomDelimiter(input))
        {
            delimiters = [GetCustomDelimiter(input, out int delimiterHeaderLenght)];
            numbersSection = RemoveDelimiterHeader(input, delimiterHeaderLenght);
        }

        var parts = numbersSection.Split(delimiters.ToArray(), StringSplitOptions.None);

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

        if (!int.TryParse(value, out var number))
            return 0;

        if (number > 1000)
            return 0;

        return number;
    }

    private static bool HasCustomDelimiter(string input)
        => input.StartsWith("//");

    /// <summary>
    /// Detect and extract a custom delimiter definition, if present.
    /// Supported formats at this stage:
    /// 1) //;\n1;2 - Single-char delimiter without brackets from requirement #6.
    /// 2) //[***]\n1***2 - Multi-char delimiter within brackets.
    /// </summary>
    /// <param name="input"></param>
    /// <param name="delimiterHeaderLenght"></param>
    /// <returns></returns>
    private static string GetCustomDelimiter(string input, out int delimiterHeaderLenght)
    {
        var match = Regex.Match(input, _regexDelimiterPattern);

        if (match.Success)
        {
            // Group 2 → bracketed delimiter (any length)
            // Group 3 → single-character delimiter
            var delimiter = match.Groups[2].Success
                ? match.Groups[2].Value
                : match.Groups[3].Value;

            // Remove the delimiter declaration line before parsing numbers
            delimiterHeaderLenght = match.Length;

            return delimiter;
        }

        delimiterHeaderLenght = 0;
        return string.Empty;
    }

    private static string RemoveDelimiterHeader(string input, int delimiterHeaderLength)
        => input.Substring(delimiterHeaderLength);
}
