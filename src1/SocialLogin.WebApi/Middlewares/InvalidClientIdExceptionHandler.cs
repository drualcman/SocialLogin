namespace SocialLogin.WebApi.Middlewares;
internal class InvalidClientIdExceptionHandler
{
    public static ProblemDetails Handle(RefreshTokenNotFoundException ex, IMembershipMessageLocalizer localizer)
    {
        return new ProblemDetails().FromHttp400BadRequest(
            localizer[MessageKeys.InvalidClientIdExceptionMessage],
            nameof(InvalidClientIdException),
            null);
    }
}
