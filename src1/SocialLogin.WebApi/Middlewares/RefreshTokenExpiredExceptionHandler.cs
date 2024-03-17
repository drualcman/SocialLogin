namespace SocialLogin.WebApi.Middlewares;
internal class RefreshTokenExpiredExceptionHandler
{
    public static ProblemDetails Handle(RefreshTokenExpiredException ex, IMembershipMessageLocalizer localizer)
    {
        return new ProblemDetails().FromHttp400BadRequest(
            localizer[MessageKeys.RefreshTokenExpiredExceptionMessage],
            nameof(RefreshTokenExpiredException),
            null);
    }
}
