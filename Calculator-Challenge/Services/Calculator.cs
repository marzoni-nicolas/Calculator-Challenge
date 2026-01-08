namespace Calculator_Challenge.Services;

public interface ICalculator
{
    int Add(IReadOnlyCollection<int> numbers);
}

public sealed class Calculator : ICalculator
{
    /// <summary>
    /// Computes the sum of all <paramref name="numbers"/>
    /// </summary>
    /// <param name="numbers"></param>
    /// <returns></returns>
    public int Add(IReadOnlyCollection<int> numbers)
    {
        if (numbers?.Any() != true)
        {
            return 0;
        }

        return numbers.Sum();
    }
}