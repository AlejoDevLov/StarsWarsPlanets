namespace StarsWarsPlanets.Exceptions;

internal class InvalidUserChoiceException : Exception
{
    public InvalidUserChoiceException()
    {
    }

    public InvalidUserChoiceException(string? message) : base(message)
    {
    }

    public InvalidUserChoiceException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

}
