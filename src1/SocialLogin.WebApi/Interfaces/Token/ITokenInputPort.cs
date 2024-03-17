namespace SocialLogin.WebApi.Interfaces.Token;
public interface ITokenInputPort
{
    Task HandleTokenRequestAsync(TokenRequestInfo requestInfo);
}
