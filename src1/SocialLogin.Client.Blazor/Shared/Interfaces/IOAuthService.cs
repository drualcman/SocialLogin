namespace SocialLogin.Client.Blazor.Shared.Interfaces;
public interface IOAuthService
{
    string CodeChallengeMethodSha256 => "S256";
    string CodeChallengeMethodPlain => "plain";

    string GetCodeVerifier();
    string GetHash256CodeChallenge(string codeVerifier);
    string GetNonce();
    string GetState();
    string BuildAuthorizeRequestUri(AuthorizeRequestInfo info);
    FormUrlEncodedContent BuildTokenRequestBody(TokenRequestInfo info);

    TokenRequestInfo GetTokenRequestInfoFromRequestBody(Dictionary<string, string> requestBody);
}

