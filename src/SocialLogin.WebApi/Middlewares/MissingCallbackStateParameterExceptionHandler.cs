namespace SocialLogin.WebApi.Middlewares;
internal class MissingCallbackStateParameterExceptionHandler
{
    public static ProblemDetails Handle(RefreshTokenNotFoundException ex, IMembershipMessageLocalizer localizer)
    {
        return new ProblemDetails().FromHttp400BadRequest(
            localizer[MessageKeys.UnableToGetIDPTokensExceptionMessage],
            nameof(MissingCallbackStateParameterException),
            null);
    }
}
