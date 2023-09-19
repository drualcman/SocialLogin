namespace SocialLogin.WebApi.Middlewares;
internal class RefreshTokenNotFoundExceptionHandler
{
    public static ProblemDetails Handle(RefreshTokenNotFoundException ex, IMembershipMessageLocalizer localizer)
    {
        return new ProblemDetails().FromHttp400BadRequest(
            localizer[MessageKeys.RefreshTokenNotFoundExceptionMessage],
            nameof(RefreshTokenNotFoundException),
            null);
    }
}
