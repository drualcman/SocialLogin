namespace SocialLogin.WebApi.Interfaces.Authorize;
public interface IAuthorizeCallbackInputPort
{
    Task<string> HandleCallback(string state, string code);
}
