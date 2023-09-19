namespace SocialLogin.WebApi.Middlewares;
internal class InvalidAuthorizationCodeExceptionHandler
{
    public static ProblemDetails Handle(RefreshTokenNotFoundException ex, IMembershipMessageLocalizer localizer)
    {
        return new ProblemDetails().FromHttp400BadRequest(
            localizer[MessageKeys.InvalidAuthorizationCodeExceptionMessage],
            nameof(InvalidAuthorizationCodeException),
            null);
    }
}
