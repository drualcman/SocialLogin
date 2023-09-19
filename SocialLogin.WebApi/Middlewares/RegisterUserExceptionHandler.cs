namespace SocialLogin.WebApi.Middlewares;
internal class RegisterUserExceptionHandler
{
    public static ProblemDetails Handle(RegisterUserException ex, IMembershipMessageLocalizer localizer)
    {
        return new ProblemDetails().FromHttp400BadRequest(
            localizer[MessageKeys.RegisterUserExceptionMessage],
            nameof(RegisterUserException),
            ex.Errors);
    }
}
