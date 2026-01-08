using Calculator_Challenge.Exceptions;

namespace Calculator_Challenge.Services;

public interface INumberListValidator
{
    void Validate(IReadOnlyList<int> numbers);
}


public sealed class NumberListValidator : INumberListValidator
{
    /// <summary>
    /// Throw a <see cref="NegativeNumbersNotAllowedException"/> if any value is negative.
    /// </summary>
    /// <param name="numbers"></param>
    /// <exception cref="NegativeNumbersNotAllowedException"></exception>
    public void Validate(IReadOnlyList<int> numbers)
    {
        var negatives = numbers.Where(n => n < 0).ToList();

        if (negatives.Any())
        {
            throw new NegativeNumbersNotAllowedException(negatives);
        }
    }
}
