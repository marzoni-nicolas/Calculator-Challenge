using Calculator_Challenge.Exceptions;
using Calculator_Challenge.Services;
using FluentAssertions;
using System.ComponentModel.DataAnnotations;

namespace Calculator_Challenge.Tests;

/// <summary>
/// Simple integration tests to try few inputs without running all of them from the terminal.
/// </summary>
public class ApplicationServiceTests
{
    private readonly IApplicationService _applicationService = new ApplicationService(new NumberParser(),
                                                                                      new NumberListValidator(),
                                                                                      new Calculator());

    [Theory]
    [InlineData("//[,,]\n4,,5", 9)]
    [InlineData("//[,]\n3,7", 10)]
    [InlineData("//,\n1,2", 3)]
    [InlineData("//[##]\n1##", 1)]
    [InlineData("//[***]\n***", 0)]
    [InlineData("//[##]\n####", 0)]
    [InlineData("1,\n2", 3)]
    [InlineData("5,tytyt", 5)]
    [InlineData("//[***][#]\n11***22#33", 66)]
    [InlineData("//[***][#][&]\n1***2&3", 6)]

    public void Calling_Calculate_WithValidInput_ShouldSucceed(string input, int expected)
    {
        var result = _applicationService.Calculate(input);

        result.Should().Be(expected);
    }

    [Fact]
    public void Calling_Calculate_WithNegatives_ThrowsExceptionWithAllNegatives()
    {
        var act = () => _applicationService.Calculate("-2,4");

        act.Should()
            .Throw<NegativeNumbersNotAllowedException>()
            .WithMessage("*-2*");
    }
}
