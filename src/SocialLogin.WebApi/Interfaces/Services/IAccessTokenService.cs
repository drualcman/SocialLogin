namespace SocialLogin.WebApi.Interfaces.Services;
public interface IAccessTokenService
{
    Task<string> GetNewUserAccessTokenAsync(UserEntity user);
    Task<string> RotateAccessTokenAsync(string accessTokenToRorate);
}
