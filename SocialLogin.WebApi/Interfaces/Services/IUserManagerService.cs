namespace SocialLogin.WebApi.Interfaces.Services;
public interface IUserManagerService
{
    Task<IEnumerable<MembershipError>> RegisterExternalUserAsync(ExternalUserEntity user);
    Task<UserEntity> GetUserByExternalCredentialsAsync(ExternalUserCredentials user);
    async Task ThrowIfUnableToRegisterExternalUserAsync(ExternalUserEntity user)
    {
        IEnumerable<MembershipError> errors = await RegisterExternalUserAsync(user);
        if (errors is not null && errors.Any())
            throw new RegisterUserException(errors);
    }
    async Task<UserEntity> ThrowIfUnableToGetUserByExternalCredentialsAsync(ExternalUserCredentials userCredentials)
    {
        UserEntity user = await GetUserByExternalCredentialsAsync(userCredentials);
        if (user == default)
            throw new LoginUserException();
        return user;
    }
}
