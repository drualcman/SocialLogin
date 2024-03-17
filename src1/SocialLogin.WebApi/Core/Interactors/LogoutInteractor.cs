namespace SocialLogin.WebApi.Core.Interactors;
internal class LogoutInteractor : ILogoutInputPort
{
    readonly IRefreshTokenService RefreshTokenService;
    readonly IIDPService IDPService;

    public LogoutInteractor(IRefreshTokenService refreshTokenService, IIDPService iDPService)
    {
        RefreshTokenService = refreshTokenService;
        IDPService = iDPService;
    }

    public async Task LogoutAsync(UserTokensDto userTokens)
    {
        await RefreshTokenService.DeleteRefreshTokenAsync(userTokens.RefreshToken);
        await IDPService.EndSession(userTokens.AccessToken, "");
    }
}
