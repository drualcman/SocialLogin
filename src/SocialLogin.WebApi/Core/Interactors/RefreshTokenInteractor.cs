namespace SocialLogin.WebApi.Core.Interactors;
internal class RefreshTokenInteractor : IRefreshTokenInputPort
{
    readonly IRefreshTokenService RefreshTokenService;
    readonly IRefreshTokenOutputPort OutputPort;

    public RefreshTokenInteractor(IRefreshTokenService refreshTokenService, IRefreshTokenOutputPort outputPort)
    {
        RefreshTokenService = refreshTokenService;
        OutputPort = outputPort;
    }

    public async Task RefreshTokenAsync(UserTokensDto userTokensDto)
    {
        await RefreshTokenService.ThrowIfUnableToRotateRefreshTokenAsync(userTokensDto.RefreshToken, userTokensDto.AccessToken);
        await OutputPort.HandleAccessTokenAsync(userTokensDto.AccessToken);
    }
}
