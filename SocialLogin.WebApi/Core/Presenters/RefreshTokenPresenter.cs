namespace SocialLogin.WebApi.Core.Presenters;
internal class RefreshTokenPresenter : IRefreshTokenOutputPort
{
    readonly IAccessTokenService AccessTokenService;
    readonly IRefreshTokenService RefreshTokenService;

    public RefreshTokenPresenter(IAccessTokenService accessTokenService, IRefreshTokenService refreshTokenService)
    {
        AccessTokenService = accessTokenService;
        RefreshTokenService = refreshTokenService;
    }

    public UserTokensDto UserTokens { get; private set; }

    public async Task HandleAccessTokenAsync(string accessToken)
    {
        string userToken = await AccessTokenService.RotateAccessTokenAsync(accessToken);
        string refreshToken = await RefreshTokenService.GetRefreshTokenForAccessTokenAsync(userToken);
        UserTokens = new UserTokensDto(userToken, refreshToken);
    }
}
