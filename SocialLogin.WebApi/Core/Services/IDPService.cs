namespace SocialLogin.WebApi.Core.Services;
internal class IDPService : IIDPService
{
    readonly HttpClient Client;
    readonly IOAuthService OAuthService;
    readonly IDPClientInfoOptions ClientIDPInfoOptions;
    readonly ILogger<IDPService> Logger;

    public IDPService(IHttpClientFactory httpClientFactory, IOAuthService oAuthService,
        IOptions<IDPClientInfoOptions> clientIDPInfoOptions,
        ILogger<IDPService> logger)
    {
        Client = httpClientFactory.CreateClient();
        OAuthService = oAuthService;
        ClientIDPInfoOptions = clientIDPInfoOptions.Value;
        Logger = logger;
    }

    public Task EndSession(string providerId, string token) 
    {
        return Task.CompletedTask;
    }

    public Task<string> GetAuthorizeRequestUri(string providerId, string state, string codeVerifier, string nonce)
    {
        string result = null;

        IDPClientInfo info = ClientIDPInfoOptions.IDPClients.FirstOrDefault(p => p.ProviderId == providerId);
        if(info != null)
        {
            string CodeChallenge;
            string CodeChallengeMethod;
            if(info.SupportsS256CodeChallengeMethod)
            {
                CodeChallenge = OAuthService.GetHash256CodeChallenge(codeVerifier);
                CodeChallengeMethod = OAuthService.CodeChallengeMethodSha256;
            }
            else
            {
                CodeChallenge = codeVerifier;
                CodeChallengeMethod = OAuthService.CodeChallengeMethodPlain;
            }

            AuthorizeRequestInfo RequestInfo = new(info.AuthorizeEndpoint,
                info.ClientId, info.RedirectUri, state, info.Scope, CodeChallenge,
                CodeChallengeMethod, nonce);

            result = OAuthService.BuildAuthorizeRequestUri(RequestInfo);
        }
        return Task.FromResult(result);

    }
    public async Task<IDPTokens> GetTokensAsync(string providerId, string authorizationCode,
        string codeVerifier, string nonce)
    {
        IDPTokens tokens = null;
        IDPClientInfo info = ClientIDPInfoOptions.IDPClients.FirstOrDefault(p => p.ProviderId == providerId);

        FormUrlEncodedContent requestBody = OAuthService.BuildTokenRequestBody(new TokenRequestInfo(
            authorizationCode, info.RedirectUri, info.ClientId, info.Scope,
            codeVerifier, info.ClientSecret));
        HttpResponseMessage response = await Client.PostAsync(info.TokenEndpoint, requestBody);
        JsonElement jsonContentResponse = await response.Content.ReadFromJsonAsync<JsonElement>();

        if(response.IsSuccessStatusCode)
        {
            if(jsonContentResponse.TryGetProperty("id_token", out JsonElement idTokenJson))
            {
                string idTokenToVerify = idTokenJson.ToString();
                // Requiere el paquete NuGet: System.IdentityModel.Tokens.Jwt
                JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
                JwtSecurityToken jwtToken = handler.ReadJwtToken(idTokenToVerify);
                string idTokenNonce = jwtToken.Claims.FirstOrDefault(c => c.Type == "nonce")?.Value;
                if(idTokenNonce != null && idTokenNonce == nonce)
                {
                    tokens = new()
                    {
                        IdToken = idTokenToVerify
                    };

                    if(jsonContentResponse.TryGetProperty("access_token", out JsonElement accessTokenJson))
                        tokens.AccessToken = accessTokenJson.ToString();
                }
            }
        }
        else
        {
            Logger.LogError("{content}", jsonContentResponse.GetRawText());
        }
        return tokens;
    }
}
