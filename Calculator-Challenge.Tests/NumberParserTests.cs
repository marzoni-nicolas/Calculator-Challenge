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
    [InlineData("4,-3", new[] { 4, -3 })]
    public void Parse_SupportsNewlineAndCommaDelimiters(string input, int[] expected)
    {
        var result = _parser.Parse(input);

        result.Should().Equal(expected);
    }

    [Theory]
    [InlineData("//#\n2#5", new[] { 2, 5 })]
    [InlineData("//,\n3,4", new[] { 3, 4 })]
    public void Parse_SupportsSingleCharacterCustomDelimter(string input, int[] expected)
    {
        var parser = new NumberParser();

        var result = parser.Parse(input);

        result.Should().Equal(expected);
    }

    [Fact]
    public void Parse_CustomDelimiterStillSupportsDefaultDelimiters()
    {
        var parser = new NumberParser();

        var result = parser.Parse("//,\n2,ff,100");

        result.Should().Equal(2, 0, 100);
    }

    [Theory]
    [InlineData("//[***]\n11***22***33", new[] { 11, 22, 33 })]
    [InlineData("//[&]\n11&22&33", new[] { 11, 22, 33 })]
    [InlineData("//[,]\n11,22,33", new[] { 11, 22, 33 })]
    public void Parse_SupportsCustomDelimiterOfAnyLength(string input, int[] expected)
    {
        var parser = new NumberParser();

        var result = parser.Parse(input);

        result.Should().Equal(expected);
    }

    [Theory]
    [InlineData("//[***]\n***", new[] { 0 ,0 })]
    [InlineData("//[##]\n####", new[] { 0, 0, 0 })]
    [InlineData("//[,,]\n,,", new[] { 0, 0 })]
    public void Parse_SupportsCustomDelimiterOfAnyLength_WithoutNumbers(string input, int[] expected)
    {
        var parser = new NumberParser();

        var result = parser.Parse(input);

        result.Should().Equal(expected);
    }

    [Theory]
    [InlineData("//[***][#]\n11***22#33", new[] { 11, 22, 33 })]
    [InlineData("//[***][#][&]\n1***2&3", new[] { 1, 2, 3 })]
    public void Parse_SupportsMultipleCustomDelimiterOfAnyLength(string input, int[] expected)
    {
        var parser = new NumberParser();

        var result = parser.Parse(input);

        result.Should().Equal(expected);
    }

}
