namespace SocialLogin.Client.Entities.Interfaces;
public interface ITokensRepository
{
    Task SaveTokensAsync(UserTokensDto userTokensDto);
    Task<UserTokensDto> GetTokensAsync();
    Task RemoveTokensAsync();
}
