namespace SocialLogin.Client.Blazor.Interfaces;
public interface IAuthenticationStateProvider
{
    Task<AuthenticationState> GetAuthenticationStateAsync();
    Task LoginAsync(UserTokensDto userTokensDto);
    Task LogoutAsync();
    Task<UserTokensDto> GetUserTokensAsync();
}
