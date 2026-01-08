using Calculator_Challenge.Exceptions;
using Calculator_Challenge.Services;
using FluentAssertions;

namespace Calculator_Challenge.Tests;


public class NumberListValidatorTests
{
    private readonly INumberListValidator _validator = new NumberListValidator();

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
}
