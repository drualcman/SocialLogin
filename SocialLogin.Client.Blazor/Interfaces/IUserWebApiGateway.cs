namespace SocialLogin.Client.Blazor.Interfaces;
internal interface IUserWebApiGateway
{
    Task<UserTokensDto> RefreshTokenAsync(UserTokensDto tokens);
    Task LogoutAsync(UserTokensDto userTokens);
}
