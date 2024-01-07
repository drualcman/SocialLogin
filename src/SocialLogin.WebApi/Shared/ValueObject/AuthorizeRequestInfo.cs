namespace SocialLogin.WebApi.Shared.ValueObject;
public class AuthorizeRequestInfo
{
    public string AuthorizeEndpoint { get; }
    public string ClientId { get; }
    public string RedirectUri { get; }
    public string State { get; }
    public string Scope { get; }
    public string CodeChallenge { get; }
    public string CodeChallengeMethod { get; }
    public string Nonce { get; }

    public AuthorizeRequestInfo(string authorizeEndpoint, string clientId, string redirectUri,
        string state, string scope, string codeChallenge, string codeChallengeMethod, string nonce)
    {
        AuthorizeEndpoint = authorizeEndpoint;
        ClientId = clientId;
        RedirectUri = redirectUri;
        State = state;
        Scope = scope;
        CodeChallenge = codeChallenge;
        CodeChallengeMethod = codeChallengeMethod;
        Nonce = nonce;
    }
}

