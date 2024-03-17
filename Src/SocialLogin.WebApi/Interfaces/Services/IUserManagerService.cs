namespace SocialLogin.WebApi.Interfaces.Services;
public interface IUserManagerService
{
    Task<UserEntity> GetUserByExternalCredentialsAsync(ExternalUserCredentials user);
    async Task<UserEntity> ThrowIfUnableToGetUserByExternalCredentialsAsync(ExternalUserCredentials userCredentials)
    {
        UserEntity user = await GetUserByExternalCredentialsAsync(userCredentials);
        if (user == default)
            throw new LoginUserException();
        return user;
    }
}
