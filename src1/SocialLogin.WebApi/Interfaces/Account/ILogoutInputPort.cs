namespace SocialLogin.WebApi.Interfaces.Account;
public interface ILogoutInputPort
{
    Task LogoutAsync(UserTokensDto userTokens);
}
