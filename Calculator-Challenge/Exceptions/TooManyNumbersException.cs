namespace Calculator_Challenge.Exceptions;

public sealed class TooManyNumbersException : Exception
{
    public TooManyNumbersException(int count)
        : base($"A maximum of 2 numbers is allowed. Received {count}.")
    {
    }
}