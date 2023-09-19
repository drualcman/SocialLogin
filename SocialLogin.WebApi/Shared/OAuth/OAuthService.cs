namespace SocialLogin.WebApi.Shared.OAuth;
internal class OAuthService : IOAuthService
{
    public string BuildAuthorizeRequestUri(AuthorizeRequestInfo info)
    {
        StringBuilder SB = new StringBuilder($"{info.AuthorizeEndpoint}?");
        SB.Append($"response_type=code&");
        SB.Append($"client_id={info.ClientId}&");
        SB.Append($"redirect_uri={info.RedirectUri}&");
        SB.Append($"state={info.State}&");
        SB.Append($"scope={info.Scope}&");
        SB.Append($"code_challenge={info.CodeChallenge}&");
        SB.Append($"code_challenge_method={info.CodeChallengeMethod}&");
        SB.Append($"nonce={info.Nonce}");
        return SB.ToString();
    }

    public FormUrlEncodedContent BuildTokenRequestBody(TokenRequestInfo info)
    {
        Dictionary<string, string> BodyData =
            new()
            {
                {"grant_type", "authorization_code" },
                {"code", info.Code },
                {"redirect_uri", info.RedirectUri },
                {"client_id", info.ClientId},
                {"scope", info.Scope},
                {"code_verifier", info.CodeVerifier }
            };

        if(!string.IsNullOrWhiteSpace(info.ClientSecret))
            BodyData.Add("client_secret", info.ClientSecret);

        return new FormUrlEncodedContent(BodyData);
    }

    public string GetCodeVerifier()
    {
        // https://www.oauth.com/oauth2-servers/pkce/authorization-request/
        const string PossibleChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789-._~";
        StringBuilder SB = new StringBuilder();
        int MaxIndex = PossibleChars.Length;
        var RandomGenerator = new Random();
        // Code Verifier debe tener una longitud de 43 a 128.
        int Length = RandomGenerator.Next(43, 129);
        for(int i = 0; i < Length; i++)
        {
            SB.Append(PossibleChars[RandomGenerator.Next(MaxIndex)]);
        }
        return SB.ToString();
    }

    public string GetHash256CodeChallenge(string codeVerifier)
    {
        byte[] ChallengeBytes =
            SHA256.HashData(Encoding.UTF8.GetBytes(codeVerifier));
        return Base64UrlEncoder.Encode(ChallengeBytes);
    }

    public string GetNonce() => GetRandomString(12);
    public string GetState() => GetRandomString(32);

    public TokenRequestInfo GetTokenRequestInfoFromRequestBody(Dictionary<string, string> requestBody)
    {
        requestBody.TryGetValue("code", out string Code);
        requestBody.TryGetValue("redirect_uri", out string RedirectUri);
        requestBody.TryGetValue("client_id", out string ClientId);
        requestBody.TryGetValue("scope", out string Scope);
        requestBody.TryGetValue("code_verifier", out string CodeVerifier);
        requestBody.TryGetValue("client_secret", out string ClientSecret);

        return new TokenRequestInfo(Code, RedirectUri, ClientId, Scope, CodeVerifier, ClientSecret);
    }


    #region Helpers
    static string GetRandomString(int length)
    {
        // 3 bytes generan 4 caracteres base64
        // 75 bytes generan 100 caracteres

        double NumBytes = Math.Ceiling(3 * length / 4d);
        byte[] Buffer = new byte[(int)NumBytes];
        using RandomNumberGenerator Rng = RandomNumberGenerator.Create();
        Rng.GetNonZeroBytes(Buffer);

        // Requiere paquete Microsoft.IdentityModel.Tokens
        return Base64UrlEncoder.Encode(Buffer);
    }
    #endregion

}
