namespace SocialLogin.Client.Blazor.Services;
internal class AuthorizeService : IAuthorizeService
{
    readonly IOptions<AppOptions> AppOptions;
    readonly IOAuthStateService StateService;
    readonly IOAuthService OAuthService;
    readonly NavigationManager NavigationManager;

    public AuthorizeService(IOptions<AppOptions> appOptions, IOAuthStateService stateService, IOAuthService oAuthService, NavigationManager navigationManager)
    {
        AppOptions = appOptions;
        StateService = stateService;
        OAuthService = oAuthService;
        NavigationManager = navigationManager;
    }

    public ExternalIDPInfo[] IDPs => AppOptions.Value.IDPs;

    public async Task AuthorizeAsync(string providerId, ScopeAction action, string returnUri)
    {
        StateInfo stateInfo = new StateInfo(
            OAuthService.GetState(),
            OAuthService.GetCodeVerifier(),
            OAuthService.GetNonce(),
            $"{action}_{providerId}",
            returnUri);

        AuthorizeRequestInfo requestData = new AuthorizeRequestInfo(
            AppOptions.Value.AuthorizationEndpoint,
            AppOptions.Value.ClientId,
            AppOptions.Value.RedirectUri,
            stateInfo.State,
            stateInfo.Scope,
            OAuthService.GetHash256CodeChallenge(stateInfo.CodeVerifier),
            OAuthService.CodeChallengeMethodSha256,
            stateInfo.Nonce);

        await StateService.SetAsync(stateInfo.State, stateInfo);

        NavigationManager.NavigateTo(OAuthService.BuildAuthorizeRequestUri(requestData), true);
    }
}
