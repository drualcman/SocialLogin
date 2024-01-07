namespace SocialLogin.WebApi.Interfaces.Services;
public interface IIDPService
{
    Task<string> GetAuthorizeRequestUri(string providerId, string state, string codeVerifier, string nonce);
    Task<IDPTokens> GetTokensAsync(string providerId, string authorizacionCode, string codeVerifier, string nonce);
    Task EndSession(string providerId, string token);
}
