namespace SocialLogin.Client.HttpHandlers.HttpMessageHandlers;
internal class BearerTokenHandler : DelegatingHandler
{
    readonly IAuthenticationStateProvider StateProvider;

    public BearerTokenHandler(IAuthenticationStateProvider stateProvider) => StateProvider = stateProvider;

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        UserTokensDto storedTokens = await StateProvider.GetUserTokensAsync();
        if(storedTokens is not null && !string.IsNullOrEmpty(storedTokens.AccessToken))
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", storedTokens.AccessToken);
        return await base.SendAsync(request, cancellationToken);
    }
}
