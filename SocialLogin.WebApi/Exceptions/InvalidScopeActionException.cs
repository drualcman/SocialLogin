namespace SocialLogin.WebApi.Exceptions;
public class InvalidScopeActionException : Exception
{
    public InvalidScopeActionException()
    {
    }

    public InvalidScopeActionException(string message) : base(message)
    {
    }

    public InvalidScopeActionException(string message, Exception innerException) : base(message, innerException)
    {
    }
}
