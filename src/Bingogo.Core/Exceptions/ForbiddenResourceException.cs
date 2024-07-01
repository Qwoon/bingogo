namespace Bingogo.Core.Exceptions;

public class ForbiddenResourceException : Exception
{
    public ForbiddenResourceException(string message) : base(message) { }
}

