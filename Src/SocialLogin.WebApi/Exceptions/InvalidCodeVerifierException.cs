namespace SocialLogin.WebApi.Exceptions;
public class InvalidCodeVerifierException : Exception
{
    public InvalidCodeVerifierException()
    {
    }

    public InvalidCodeVerifierException(string message) : base(message)
    {
    }

    public InvalidCodeVerifierException(string message, Exception innerException) : base(message, innerException)
    {
    }
}
