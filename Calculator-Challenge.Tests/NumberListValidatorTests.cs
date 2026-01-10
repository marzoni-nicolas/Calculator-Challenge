using Calculator_Challenge.Exceptions;
using Calculator_Challenge.Options;
using Calculator_Challenge.Services;
using FluentAssertions;
using NSubstitute;

namespace Calculator_Challenge.Tests;


public class NumberListValidatorTests
{
    private readonly INumberListValidator _validator;
    private ICalculatorOptions _calculatorOptions;
    public NumberListValidatorTests()
    {
        _calculatorOptions = Substitute.For<ICalculatorOptions>();

        _calculatorOptions.AlternateDelimiter.Returns('\n');
        _calculatorOptions.MaxAllowedValue.Returns(1000);
        _calculatorOptions.DenyNegativeNumbers.Returns(true);

        _validator = new NumberListValidator(_calculatorOptions);
    }

    [Fact]
    public void Validate_WhenNegativeNumbersExist_ThrowsExceptionWithAllNegatives()
    {
        var numbers = new[] { 1, -2, 3, -5 };

        var act = () => _validator.Validate(numbers);

        act.Should()
            .Throw<NegativeNumbersNotAllowedException>()
            .WithMessage("*-2, -5*");
    }

    [Fact]
    public void Validate_WhenNoNegatives_DoesNotThrow()
    {
        var numbers = new[] { 0, 1, 2, 3 };

        var act = () => _validator.Validate(numbers);

        act.Should().NotThrow();
    }

    [Fact]
    public void Validate_WhenDenyNegativeNumbersIsFalse_DoesNotThrow()
    {
        _calculatorOptions.DenyNegativeNumbers.Returns(false);

        var parserWithCustomOptions = new NumberParser(_calculatorOptions);

        var result = parserWithCustomOptions.Parse("1,-11");

        result.Should().Equal([1, -11]);
    }
}
