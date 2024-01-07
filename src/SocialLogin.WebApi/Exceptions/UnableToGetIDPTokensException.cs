namespace SocialLogin.WebApi.Exceptions;
public class UnableToGetIDPTokensException : Exception
{
    public UnableToGetIDPTokensException()
    {
    }

    public UnableToGetIDPTokensException(string message) : base(message)
    {
    }

    public UnableToGetIDPTokensException(string message, Exception innerException) : base(message, innerException)
    {
    }
}
