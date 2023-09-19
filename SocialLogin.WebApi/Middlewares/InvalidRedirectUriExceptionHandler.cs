namespace SocialLogin.WebApi.Middlewares;
internal class InvalidRedirectUriExceptionHandler
{
    public static ProblemDetails Handle(RefreshTokenNotFoundException ex, IMembershipMessageLocalizer localizer)
    {
        return new ProblemDetails().FromHttp400BadRequest(
            localizer[MessageKeys.InvalidRedirectUriExceptionMessage],
            nameof(InvalidRedirectUriException),
            null);
    }
}
