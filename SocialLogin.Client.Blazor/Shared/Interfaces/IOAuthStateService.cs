namespace SocialLogin.Client.Blazor.Shared.Interfaces;
public interface IOAuthStateService
{
    Task SetAsync<T>(string key, T value);
    Task<T> GetAsync<T>(string key);
    Task RemoveAsync(string key);
}

