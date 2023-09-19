namespace SocialLogin.WebApi.Exceptions;
public class InvalidAuthorizationCodeException : Exception
{
    public InvalidAuthorizationCodeException()
    {
    }

    public InvalidAuthorizationCodeException(string message) : base(message)
    {
    }

    public InvalidAuthorizationCodeException(string message, Exception innerException) : base(message, innerException)
    {
    }
}
