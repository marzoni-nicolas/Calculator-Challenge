using Calculator_Challenge.Services;
using FluentAssertions;

namespace Calculator.Tests;

public class NumberParserTests
{
    private readonly INumberParser _parser = new NumberParser();

    [Theory]
    [InlineData("1,2,3", new[] { 1, 2, 3 })]
    [InlineData("5,tytyt", new[] { 5, 0 })]
    [InlineData(",", new[] { 0, 0 })]
    [InlineData("", new[] { 0 })]
    [InlineData("-2,1", new[] { -2, 1 })]
    public void Parse_Input_ReturnsNormalizedIntegers(string input, int[] expected)
    {
        var result = _parser.Parse(input);

        result.Should().Equal(expected);
    }
}
