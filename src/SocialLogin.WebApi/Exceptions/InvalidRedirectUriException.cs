namespace SocialLogin.WebApi.Exceptions;
public class InvalidRedirectUriException : Exception
{
    public InvalidRedirectUriException()
    {
    }

    public InvalidRedirectUriException(string message) : base(message)
    {
    }

    public InvalidRedirectUriException(string message, Exception innerException) : base(message, innerException)
    {
    }
}
