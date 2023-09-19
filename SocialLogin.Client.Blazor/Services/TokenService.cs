namespace SocialLogin.Client.Blazor.Services;
internal class TokenService
{
    readonly IMembershipMessageLocalizer Localizer;
    readonly IOAuthStateService AuthStateService;
    readonly IOptions<AppOptions> AppOptions;
    readonly IOAuthService AuthService;
    readonly HttpClient Client;

    public TokenService(IMembershipMessageLocalizer localizer, IOAuthStateService authStateService, IOptions<AppOptions> appOptions, IOAuthService authService, HttpClient client)
    {
        Localizer = localizer;
        AuthStateService = authStateService;
        AppOptions = appOptions;
        AuthService = authService;
        Client = client;
    }

    public async Task<TokenServiceResponse> GetTokensAsync(string state, string code)
    {
        TokenServiceResponse result;

        if(string.IsNullOrWhiteSpace(state) || string.IsNullOrWhiteSpace(code))
            throw new Exception(Localizer[MessageKeys.MissingAuthorizeCallbackParametersMessage]);

        StateInfo stateInfo = await AuthStateService.GetAsync<StateInfo>(state);

        if(stateInfo is null)
            throw new Exception(Localizer[MessageKeys.InvalidStateValueMessage]);

        FormUrlEncodedContent requestBody = AuthService.BuildTokenRequestBody(
            new TokenRequestInfo(code, AppOptions.Value.RedirectUri, AppOptions.Value.ClientId, stateInfo.Scope, stateInfo.CodeVerifier, null));

        HttpResponseMessage response = await Client.PostAsync(AppOptions.Value.TokenEndpoint, requestBody);

        UserTokensDto tokens = await response.Content.ReadFromJsonAsync<UserTokensDto>();

        JwtSecurityToken jwt = new JwtSecurityTokenHandler().ReadJwtToken(tokens.AccessToken);
        string tokenNonce = jwt.Claims.FirstOrDefault(c => c.Type == "nonce").Value;

        if(string.IsNullOrEmpty(tokenNonce) || tokenNonce != stateInfo.Nonce)
            throw new Exception(Localizer[MessageKeys.InvalidNonceValueMessage]);

        result = new()
        {
            Tokens = tokens,
            ReturnUri = stateInfo.ReturnUri,
            Scope = stateInfo.Scope
        };

        await AuthStateService.RemoveAsync(state);

        return result;
    }
}
