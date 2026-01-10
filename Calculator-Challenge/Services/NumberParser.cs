
using Calculator_Challenge.Options;
using System;
using System.Text.RegularExpressions;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Calculator_Challenge.Services;


public interface INumberParser 
{
    IReadOnlyList<int> Parse(string input);
}

public sealed class NumberParser : INumberParser
{
    private readonly IList<string> _defaultDelimiters;

    // This regex captures:
    // 1) One or more delimiters wrapped in brackets: [*][!!][r9r]
    // 2) OR a single-character delimiter: ;
    private static readonly string _regexDelimiterPattern = @"^//(\[(.+?)\])+\n|^//(.)\n";

    private static readonly string _delimiterHeaderStarsWithString = "//";

    private readonly ICalculatorOptions _options;

    public NumberParser(ICalculatorOptions options)
    {
        _options = options;

        _defaultDelimiters = new List<string>
        {
            ",",
            _options.AlternateDelimiter.ToString()
        };
    }

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
            delimiters = GetCustomDelimiters(input, out int delimiterHeaderLenght);
            numbersSection = RemoveDelimiterHeader(input, delimiterHeaderLenght);
        }

        var parts = numbersSection.Split(delimiters.ToArray(), StringSplitOptions.None);

        var numbers = parts
            .Select(ParseNumber)
            .ToList();

        return numbers;
    }

    private int ParseNumber(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return 0;
        }

        if (!int.TryParse(value, out var number))
            return 0;

        if (number > _options.MaxAllowedValue)
            return 0;

        return number;
    }

    private static bool HasCustomDelimiter(string input)
        => input.StartsWith(_delimiterHeaderStarsWithString);

    /// <summary>
    /// Detect and extract a custom delimiters definition, if present.
    /// Supported formats at this stage:
    /// 1) //;\n1;2 - Single-char delimiter without brackets from requirement #6.
    /// 2) //[***]\n1***2 - Multi-char delimiter within brackets.
    /// 3) //[***][#]\n1***2#3 - Multiple multi-char delimiter within brackets.
    /// </summary>
    /// <param name="input"></param>
    /// <param name="delimiterHeaderLenght"></param>
    /// <returns></returns>
    private static List<string> GetCustomDelimiters(string input, out int delimiterHeaderLenght)
    {
        var customDelimiters = new List<string>();
        
        var match = Regex.Match(input, _regexDelimiterPattern);

        if (!match.Success)
        {
            // Not defined as a requirement, but an empty header should not happen.

            delimiterHeaderLenght = 0;
            return [string.Empty];
        }

        // Extract all bracketed delimiters, if present
        var bracketMatches = Regex.Matches(match.Value, @"\[(.+?)\]");

        if (bracketMatches.Count > 0)
        {
            // Brackets delimiter (any length)
            foreach (Match bracketMatch in bracketMatches)
            {
                customDelimiters.Add(bracketMatch.Groups[1].Value);
            }
        }
        else
        {
            // No brackets delimiter (Single-char)
            customDelimiters.Add(match.Groups[3].Value);
        }

        // Returns the delimiter header length so that can be removed before parsing numbers
        delimiterHeaderLenght = match.Length;

        return customDelimiters;
    }

    private static string RemoveDelimiterHeader(string input, int delimiterHeaderLength)
        => input.Substring(delimiterHeaderLength);
}
