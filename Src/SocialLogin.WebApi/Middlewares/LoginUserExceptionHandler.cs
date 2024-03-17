namespace SocialLogin.WebApi.Middlewares;
internal class LoginUserExceptionHandler
{
    public static ProblemDetails Handle(LoginUserException ex, IMembershipMessageLocalizer localizer)
    {
        return new ProblemDetails().FromHttp400BadRequest(
            localizer[MessageKeys.LoginUserExceptionMessage],
            nameof(LoginUserException),
            null);
    }
}
