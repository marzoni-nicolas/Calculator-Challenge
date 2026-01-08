using Calculator_Challenge.Services;
using FluentAssertions;

namespace Calculator_Challenge.Tests;

public class NumberParserTests
{
    private readonly INumberParser _parser = new NumberParser();

    [Theory]
    [InlineData("1,2,3", new[] { 1, 2, 3 })]
    [InlineData("5,tytyt", new[] { 5, 0 })]
    [InlineData(",", new[] { 0, 0 })]
    [InlineData("", new[] { 0 })]
    [InlineData("-2,1", new[] { -2, 1 })]
    [InlineData("-2,1000", new[] { -2, 1000 })]
    [InlineData("-2,1001", new[] { -2, 0 })]
    public void Parse_Input_ReturnsNormalizedIntegers(string input, int[] expected)
    {
        var result = _parser.Parse(input);

        result.Should().Equal(expected);
    }

    [Theory]
    [InlineData("1\n2,3", new[] { 1, 2, 3 })]
    [InlineData("1,\n2", new[] { 1, 0, 2 })]
    [InlineData("\n", new[] { 0 })]
    [InlineData("4\n-3", new[] { 4, -3 })]
    public void Parse_SupportsNewlineAndCommaDelimiters(string input, int[] expected)
    {
        var result = _parser.Parse(input);

        result.Should().Equal(expected);
    }
}
