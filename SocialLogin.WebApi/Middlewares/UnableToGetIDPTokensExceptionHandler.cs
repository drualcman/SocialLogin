namespace SocialLogin.WebApi.Middlewares;
internal class UnableToGetIDPTokensExceptionHandler
{
    public static ProblemDetails Handle(RefreshTokenNotFoundException ex, IMembershipMessageLocalizer localizer)
    {
        return new ProblemDetails().FromHttp400BadRequest(
            localizer[MessageKeys.UnableToGetIDPTokensExceptionMessage],
            nameof(UnableToGetIDPTokensException),
            null);
    }
}
