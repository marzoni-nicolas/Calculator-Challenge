namespace Calculator_Challenge.Services;

public interface ICalculator
{
    int Add(IReadOnlyCollection<int> numbers);
}

public sealed class Calculator : ICalculator
{
    public int Add(IReadOnlyCollection<int> numbers)
    {
        if (numbers?.Any() != true)
        {
            return 0;
        }

        return numbers.Sum();
    }
}