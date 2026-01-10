using Calculator_Challenge.Exceptions;
using Calculator_Challenge.Options;

namespace Calculator_Challenge.Services;

public interface INumberListValidator
{
    void Validate(IReadOnlyList<int> numbers);
}


public sealed class NumberListValidator : INumberListValidator
{
    private readonly ICalculatorOptions _options;

    public NumberListValidator(ICalculatorOptions options)
    {
        _options = options;
    }

    /// <summary>
    /// When <see cref="CalculatorOptions.DenyNegativeNumbers"/> equals true, throw a <see cref="NegativeNumbersNotAllowedException"/> if any value is negative. Otherwise skips the validation.
    /// </summary>
    /// <param name="numbers"></param>
    /// <exception cref="NegativeNumbersNotAllowedException"></exception>
    public void Validate(IReadOnlyList<int> numbers)
    {
        if (!_options.DenyNegativeNumbers)
        {
            return;
        }

        var negatives = numbers.Where(n => n < 0).ToList();

        if (negatives.Any())
        {
            throw new NegativeNumbersNotAllowedException(negatives);
        }
    }
}
