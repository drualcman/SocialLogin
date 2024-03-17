namespace SocialLogin.WebApi.Core.Services;
internal class OAuthStateService : IOAuthStateService
{
    readonly IMemoryCache Cache;

    public OAuthStateService(IMemoryCache cache) => Cache = cache;

    public Task<T> GetAsync<T>(string key)
    {
        Cache.TryGetValue(key, out T value);
        return Task.FromResult(value);
    }

    public Task RemoveAsync(string key)
    {
        Cache.Remove(key);
        return Task.CompletedTask;
    }

    public Task SetAsync<T>(string key, T value)
    {
        Cache.Set(key, value, DateTime.Now.AddMinutes(5));
        return Task.CompletedTask;
    }
}
