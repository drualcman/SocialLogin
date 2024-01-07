namespace SocialLogin.WebApi.Interfaces.Token;
public interface IRefreshTokenInputPort
{
    Task RefreshTokenAsync(UserTokensDto userTokensDto);
}
