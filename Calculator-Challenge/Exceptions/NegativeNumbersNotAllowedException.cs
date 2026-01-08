namespace Calculator_Challenge.Exceptions;

public sealed class NegativeNumbersNotAllowedException : Exception
{
    public NegativeNumbersNotAllowedException(IEnumerable<int> negatives)
        : base($"Negative numbers are not allowed: {string.Join(", ", negatives)}")
    {
    }
}
