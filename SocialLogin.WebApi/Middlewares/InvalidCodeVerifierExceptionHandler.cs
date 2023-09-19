namespace SocialLogin.WebApi.Middlewares;
internal class InvalidCodeVerifierExceptionHandler
{
    public static ProblemDetails Handle(RefreshTokenNotFoundException ex, IMembershipMessageLocalizer localizer)
    {
        return new ProblemDetails().FromHttp400BadRequest(
            localizer[MessageKeys.InvalidCodeVerifierExceptionMessage],
            nameof(InvalidCodeVerifierException),
            null);
    }
}
