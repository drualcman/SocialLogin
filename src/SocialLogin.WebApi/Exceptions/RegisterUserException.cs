using SocialLogin.WebApi.Shared.ValueObject;
using SocialLogin.WebApi.ValueObjects;

namespace SocialLogin.WebApi.Exceptions;
public class RegisterUserException : Exception
{
    public IEnumerable<MembershipError> Errors { get; }
    public RegisterUserException()
    {
    }

    public RegisterUserException(string message) : base(message)
    {
    }

    public RegisterUserException(string message, Exception innerException) : base(message, innerException)
    {
    }

    public RegisterUserException(IEnumerable<MembershipError> errors) : base(string.Join("\n", errors))
    {
        Errors = errors;
    }
}
