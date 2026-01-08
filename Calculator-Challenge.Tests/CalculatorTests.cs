using Services = Calculator_Challenge.Services;
using Calculator_Challenge.Exceptions;
using FluentAssertions;
using Xunit;

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
    public void Add_ValidInputs_ReturnsExpectedResult(string input, int expected)
    {
        var result = _calculator.Add(input);

        result.Should().Be(expected);
    }

    [Fact]
    public void Add_MoreThanTwoNumbers_ThrowsException()
    {
        var act = () => _calculator.Add("1,2,3");

        act.Should()
            .Throw<TooManyNumbersException>()
            .WithMessage("*maximum of 2 numbers*");
    }
}
