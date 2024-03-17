namespace SocialLogin.WebApi.Middlewares;
internal class InvalidScopeExceptionHandler
{
    public static ProblemDetails Handle(RefreshTokenNotFoundException ex, IMembershipMessageLocalizer localizer)
    {
        return new ProblemDetails().FromHttp400BadRequest(
            localizer[MessageKeys.InvalidScopeExceptionMessage],
            nameof(InvalidScopeException),
            null);
    }
}
