namespace SocialLogin.WebApi.ValueObjects;
public class StateInfo
{
    public string CodeVerifier { get; set; }
    public string Nonce { get; set; }
    public string ProviderId { get; set; }
    public AppClientAuthorizeRequestInfo AppClientInfo { get; set; }
    public IDPTokens Tokens { get; set; }
}
