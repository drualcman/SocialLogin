namespace SocialLogin.WebApi.Exceptions;
public class RefreshTokenCompromisedException : Exception
{
    public RefreshTokenCompromisedException()
    {
    }

    public RefreshTokenCompromisedException(string message) : base(message)
    {
    }

    public RefreshTokenCompromisedException(string message, Exception innerException) : base(message, innerException)
    {
    }
}
