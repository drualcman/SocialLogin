namespace SocialLogin.Client.Blazor.Interfaces;
internal interface ITokensRepository
{
    Task SaveTokensAsync(UserTokensDto userTokensDto);
    Task<UserTokensDto> GetTokensAsync();
    Task RemoveTokensAsync();
}
