namespace SocialLogin.WebApi.Core.Interactors;
internal class TokenInteractor : ITokenInputPort
{
    readonly IOAuthStateService StateService;
    readonly IOAuthService OAuthService;
    readonly IUserManagerService UserManagerService;
    readonly ILoginOutputPort LoginOutputPort;

    public TokenInteractor(IOAuthStateService stateService, IOAuthService oAuthService, IUserManagerService userManagerService, ILoginOutputPort loginOutputPort)
    {
        StateService = stateService;
        OAuthService = oAuthService;
        UserManagerService = userManagerService;
        LoginOutputPort = loginOutputPort;
    }

    public async Task HandleTokenRequestAsync(TokenRequestInfo requestInfo)
    {
        UserEntity userEntity = null;
        StateInfo stateInfo = await StateService.GetAsync<StateInfo>(requestInfo.Code);
        if(stateInfo == null) throw new InvalidAuthorizationCodeException();
        if(stateInfo.AppClientInfo.RedirectUri != requestInfo.RedirectUri) throw new InvalidRedirectUriException();
        if(stateInfo.AppClientInfo.ClientId != requestInfo.ClientId) throw new InvalidClientIdException();
        if(stateInfo.AppClientInfo.Scope != requestInfo.Scope) throw new InvalidScopeException();
        if(stateInfo.AppClientInfo.CodeChallenge != OAuthService.GetHash256CodeChallenge(requestInfo.CodeVerifier)) throw new InvalidCodeVerifierException();

        string action = requestInfo.Scope[..requestInfo.Scope.IndexOf("_")]?.ToLower();

        JwtSecurityToken identityToken = new JwtSecurityTokenHandler().ReadJwtToken(stateInfo.Tokens.IdToken);
        string firstName = identityToken.Claims.FirstOrDefault(c => c.Type == "given_name")?.Value;
        string lastName = identityToken.Claims.FirstOrDefault(c => c.Type == "family_name")?.Value;
        string name = identityToken.Claims.FirstOrDefault(c => c.Type == "name")?.Value;
        string sub = identityToken.Claims.FirstOrDefault(c => c.Type == "sub")?.Value;
        string email = identityToken.Claims.FirstOrDefault(c => c.Type == "email")?.Value;

        if(string.IsNullOrEmpty(email)) email = sub;
        if(string.IsNullOrEmpty(firstName) && string.IsNullOrEmpty(lastName)) firstName = name ?? email;

        ExternalUserEntity externalUserIdentity = new ExternalUserEntity(email, firstName, lastName, stateInfo.ProviderId, sub);

        switch(action)
        {
            case "register":
                await UserManagerService.ThrowIfUnableToRegisterExternalUserAsync(externalUserIdentity);
                userEntity = await UserManagerService.ThrowIfUnableToGetUserByExternalCredentialsAsync(
                    new ExternalUserCredentials(externalUserIdentity.LoginProvider, externalUserIdentity.ProviderUserId, stateInfo.Tokens));
                break;
            case "login":
                userEntity = await UserManagerService.ThrowIfUnableToGetUserByExternalCredentialsAsync(
                    new ExternalUserCredentials(externalUserIdentity.LoginProvider, externalUserIdentity.ProviderUserId, stateInfo.Tokens));
                break;
            default:
                throw new InvalidScopeActionException();
        }
        if (userEntity is not null && userEntity.Claims is not null)
            userEntity.Claims = new List<Claim>(userEntity.Claims) { new Claim("nonce", stateInfo.AppClientInfo.Nonce) };
        else
            userEntity.Claims = new List<Claim>() { new Claim("nonce", stateInfo.AppClientInfo.Nonce) };

        await LoginOutputPort.HandleUserEntityAsync(userEntity);
    }
}
