namespace SocialLogin.WebApi.Interfaces.Token;
public interface IRefreshTokenOutputPort
{
    UserTokensDto UserTokens { get; }
    Task HandleAccessTokenAsync(string accessToken);
}
