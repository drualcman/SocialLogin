namespace SocialLogin.Client.Entities.Interfaces;
public interface IUserWebApiGateway
{
    Task<UserTokensDto> RefreshTokenAsync(UserTokensDto tokens);
    Task LogoutAsync(UserTokensDto userTokens);
}
