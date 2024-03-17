namespace SocialLogin.WebApi.Core.Interactors;
internal class AuthorizeInteractor : IAuthorizeInputPort
{
    readonly IAppClientService AppClientService;
    readonly IOAuthService OAuthService;
    readonly IIDPService IdpService;
    readonly IOAuthStateService StateService;

    public AuthorizeInteractor(IAppClientService appClientService, IOAuthService oAuthService, IIDPService idpService, IOAuthStateService stateService)
    {
        AppClientService = appClientService;
        OAuthService = oAuthService;
        IdpService = idpService;
        StateService = stateService;
    }

    public async Task<string> GetAuthorizeRequestRedirectUri(AppClientAuthorizeRequestInfo info)
    {
        AppClientService.ThrowIfNotExist(info.ClientId, info.RedirectUri);

        string state = OAuthService.GetState();
        StateInfo requestState = new StateInfo
        {
            CodeVerifier = OAuthService.GetCodeVerifier(),
            Nonce = OAuthService.GetNonce(),
            ProviderId = info.Scope.Substring(info.Scope.IndexOf("_") + 1),
            AppClientInfo = info
        };
        string requestUri = await IdpService.GetAuthorizeRequestUri(requestState.ProviderId, state, requestState.CodeVerifier, requestState.Nonce);
        if(requestUri == null) throw new UnauthorizedAccessException();
        await StateService.SetAsync(state, requestState);
        return requestUri;
    }
}
