using Services = Calculator_Challenge.Services;
using FluentAssertions;

namespace Calculator_Challenge.Tests;

public class CalculatorTests
{
    private readonly Services.ICalculator _calculator = new Services.Calculator();

    [Theory]
    [InlineData(new int[] { 20 }, 20)]
    [InlineData(new int[] { 1, 5000 }, 5001)]
    [InlineData(new int[] { 4, -3 }, 1)]
    [InlineData(new int[] { }, 0)]
    [InlineData(new int[] { 0 }, 0)]
    [InlineData(new int[] { 5, 0 }, 5)]
    [InlineData(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 }, 78)]
    [InlineData(new int[] { 1, 2, 3, 6 }, 12)]
    public void Add_ValidInputs_ReturnsExpectedResult(IReadOnlyCollection<int> input, int expected)
    {
        var result = _calculator.Add(input);

        result.Should().Be(expected);
    }
}
