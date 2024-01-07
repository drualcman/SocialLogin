namespace SocialLogin.WebApi.Shared.ValueObject;
public class TokenRequestInfo
{
    public string Code { get; }
    public string RedirectUri { get; }
    public string ClientId { get; }
    public string Scope { get; }
    public string CodeVerifier { get; }
    public string ClientSecret { get; }

    public TokenRequestInfo(string code, string redirectUri, string clientId, string scope,
        string codeVerifier, string clientSecret)
    {
        Code = code;
        RedirectUri = redirectUri;
        ClientId = clientId;
        Scope = scope;
        CodeVerifier = codeVerifier;
        ClientSecret = clientSecret;
    }
}
