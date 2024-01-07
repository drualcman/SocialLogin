namespace SocialLogin.WebApi.Core.Presenters;
internal class LoginPresenter : ILoginOutputPort
{
    readonly IAccessTokenService AccessTokenService;
    readonly IRefreshTokenService RefreshTokenService;

    public LoginPresenter(IAccessTokenService accessTokenService, IRefreshTokenService refreshTokenService)
    {
        AccessTokenService = accessTokenService;
        RefreshTokenService = refreshTokenService;
    }

    public UserTokensDto UserTokens { get; private set; }

    public async Task HandleUserEntityAsync(UserEntity user)
    {
        string accessToken = await AccessTokenService.GetNewUserAccessTokenAsync(user);
        string refreshToken = await RefreshTokenService.GetRefreshTokenForAccessTokenAsync(accessToken);
        UserTokens = new UserTokensDto(accessToken, refreshToken);
    }
}
