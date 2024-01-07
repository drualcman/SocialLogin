namespace SocialLogin.Client.Blazor.Repositories;
internal class TokensRepository : ITokensRepository
{
    const string SessionKey = "asp";
    readonly IJSRuntime JsRuntime;

    public TokensRepository(IJSRuntime jsRuntime) => JsRuntime = jsRuntime;

    public async Task<UserTokensDto> GetTokensAsync()
    {
        UserTokensDto storedTokens = default;
        try
        {
            string value = await JsRuntime.InvokeAsync<string>("localStorage.getItem", SessionKey);
            if(value is not null)
            {
                string serializedTokens = Encoding.UTF8.GetString(Convert.FromBase64String(value));
                storedTokens = JsonSerializer.Deserialize<UserTokensDto>(serializedTokens);
            }
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
        return storedTokens;
    }

    public async Task RemoveTokensAsync() =>
        await JsRuntime.InvokeVoidAsync("localStorage.removeItem", SessionKey);

    public async Task SaveTokensAsync(UserTokensDto userTokensDto)
    {
        string serializedTokens = JsonSerializer.Serialize(userTokensDto);
        string value = Convert.ToBase64String(Encoding.UTF8.GetBytes(serializedTokens));
        await JsRuntime.InvokeVoidAsync("localStorage.setItem", SessionKey, value);
    }
}
