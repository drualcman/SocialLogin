namespace SocialLogin.WebApi.Middlewares;
internal class RefreshTokenCompromisedExceptionHandler
{
    public static ProblemDetails Handle(RefreshTokenCompromisedException ex, IMembershipMessageLocalizer localizer)
    {
        return new ProblemDetails().FromHttp400BadRequest(
            localizer[MessageKeys.RefreshTokenCompromisedExceptionMessage],
            nameof(RefreshTokenCompromisedException),
            null);
    }
}
