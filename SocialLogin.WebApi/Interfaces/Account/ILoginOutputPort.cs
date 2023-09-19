namespace SocialLogin.WebApi.Interfaces.Account;
public interface ILoginOutputPort
{
    UserTokensDto UserTokens { get; }
    Task HandleUserEntityAsync(UserEntity user);
}
