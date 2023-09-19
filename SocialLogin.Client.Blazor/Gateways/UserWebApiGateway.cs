namespace SocialLogin.Client.Blazor.Gateways;
internal class UserWebApiGateway : IUserWebApiGateway
{
    readonly UserEndpointsOptions Options;
    readonly HttpClient Client;

    public UserWebApiGateway(IOptions<UserEndpointsOptions> options, HttpClient client)
    {
        Options = options.Value;
        Client = client;
        Client.BaseAddress = new Uri(Options.WebApiBaseAddress);
    }

    public async Task LogoutAsync(UserTokensDto userTokens) =>
        await Client.PostAsJsonAsync(Options.Logout, userTokens);

    public async Task<UserTokensDto> RefreshTokenAsync(UserTokensDto tokens)
    {
        HttpResponseMessage response = await Client.PostAsJsonAsync(Options.Refresh, tokens);
        return await response.Content.ReadFromJsonAsync<UserTokensDto>();
    }
}
