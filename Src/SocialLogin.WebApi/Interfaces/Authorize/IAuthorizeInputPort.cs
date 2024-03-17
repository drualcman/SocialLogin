namespace SocialLogin.WebApi.Interfaces.Authorize;
public interface IAuthorizeInputPort
{
    Task<string> GetAuthorizeRequestRedirectUri(AppClientAuthorizeRequestInfo info);
}
