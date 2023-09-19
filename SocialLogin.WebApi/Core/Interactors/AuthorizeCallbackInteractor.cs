namespace SocialLogin.WebApi.Core.Interactors;
internal class AuthorizeCallbackInteractor : IAuthorizeCallbackInputPort
{
    readonly IOAuthStateService StateService;
    readonly IIDPService IdpService;

    public AuthorizeCallbackInteractor(IOAuthStateService stateService, IIDPService idpService)
    {
        StateService = stateService;
        IdpService = idpService;
    }

    public async Task<string> HandleCallback(string state, string code)
    {
        StateInfo stateInfo = await StateService.GetAsync<StateInfo>(state);
        if(stateInfo == null) throw new MissingCallbackStateParameterException();
        IDPTokens tokens = await IdpService.GetTokensAsync(stateInfo.ProviderId, code, stateInfo.CodeVerifier, stateInfo.Nonce);
        if(tokens == null) throw new UnableToGetIDPTokensException();
        stateInfo.Tokens = tokens;       //como se trabaja en memoria es un objecto que se maneja por referencia, por eso no hace falta volver a guardar
        string redirectUri = string.Format("{0}?state={1}&code={2}", stateInfo.AppClientInfo.RedirectUri, stateInfo.AppClientInfo.State, state);
        return redirectUri;
    }
}
