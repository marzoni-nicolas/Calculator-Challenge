using Services = Calculator_Challenge.Services;
using FluentAssertions;

namespace Calculator.Tests;

public class CalculatorTests
{
    private readonly Services.ICalculator _calculator = new Services.Calculator();

    [Theory]
    [InlineData("20", 20)]
    [InlineData("1,5000", 5001)]
    [InlineData("4,-3", 1)]
    [InlineData("", 0)]
    [InlineData(",", 0)]
    [InlineData("5,tytyt", 5)]
    [InlineData("1,2,3,4,5,6,7,8,9,10,11,12", 78)]
    [InlineData("1,2,3,6", 12)]
    public void Add_ValidInputs_ReturnsExpectedResult(string input, int expected)
    {
        var result = _calculator.Add(input);

        result.Should().Be(expected);
    }
}
