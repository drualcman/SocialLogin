namespace SocialLogin.Client.Blazor.AuthenticationStateProviders;
internal class JWTAuthenticationStateProvider : AuthenticationStateProvider,
    IAuthenticationStateProvider
{
    readonly IUserWebApiGateway UserWebApiGateway;
    readonly ITokensRepository TokenRepository;

    public JWTAuthenticationStateProvider(IUserWebApiGateway userWebApiGateway, ITokensRepository tokenRepository)
    {
        UserWebApiGateway = userWebApiGateway;
        TokenRepository = tokenRepository;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        ClaimsIdentity identity = new ClaimsIdentity();
        UserTokensDto storedTokens = await GetUserTokensAsync();
        if(storedTokens is not null)
        {
            JwtSecurityToken token = GetToken(storedTokens.AccessToken);
            identity = new ClaimsIdentity(token.Claims, "Bearer");
        }
        return new AuthenticationState(new ClaimsPrincipal(identity));
    }

    private JwtSecurityToken GetToken(string accessToken) =>
        new JwtSecurityTokenHandler().ReadJwtToken(accessToken);

    public async Task<UserTokensDto> GetUserTokensAsync()
    {
        UserTokensDto storedTokens = await TokenRepository.GetTokensAsync();
        if(storedTokens is not null)
        {
            JwtSecurityToken token = GetToken(storedTokens.AccessToken);
            if(token.ValidTo <= DateTime.UtcNow)
            {
                try
                {
                    UserTokensDto newTokens = await UserWebApiGateway.RefreshTokenAsync(storedTokens);
                    await LoginAsync(newTokens);
                    storedTokens = newTokens;
                    Console.WriteLine("Access token actualizado.");
                }
                catch(Exception ex)
                {
                    storedTokens = default;
                    Console.WriteLine(ex.Message);
                    await LogoutAsync();
                }
            }
        }
        return storedTokens;
    }

    public async Task LoginAsync(UserTokensDto userTokensDto)
    {
        await TokenRepository.SaveTokensAsync(userTokensDto);
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }

    public async Task LogoutAsync()
    {
        await TokenRepository.RemoveTokensAsync();
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }
}
