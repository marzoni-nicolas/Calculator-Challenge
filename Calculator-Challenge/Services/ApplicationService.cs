using System.Linq;

namespace Calculator_Challenge.Services;

public interface IApplicationService
{
    /// <summary>
    /// Parse, validate and calculate
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    (int total, IReadOnlyCollection<int> numbers, char separator) Calculate(string input);
}

public sealed class ApplicationService : IApplicationService
{
    private readonly INumberParser _parser;
    private readonly INumberListValidator _validator;
    private readonly ICalculator _calculator;

    public ApplicationService(
        INumberParser parser,
        INumberListValidator validator,
        ICalculator calculator)
    {
        _parser = parser;
        _validator = validator;
        _calculator = calculator;
    }

    public (int total, IReadOnlyCollection<int> numbers, char separator) Calculate(string input)
    {
        var numbers = _parser.Parse(input);

        _validator.Validate(numbers);

        return (_calculator.Add(numbers), numbers, '+');
    }
}

