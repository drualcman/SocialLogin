namespace SocialLogin.WebApi.Middlewares;
internal class InvalidScopeActionExceptionHandler
{
    public static ProblemDetails Handle(RefreshTokenNotFoundException ex, IMembershipMessageLocalizer localizer)
    {
        return new ProblemDetails().FromHttp400BadRequest(
            localizer[MessageKeys.InvalidScopeActionExceptionMessage],
            nameof(InvalidScopeActionException),
            null);
    }
}
