namespace SocialLogin.WebApi.Entities;
public class ExternalUserCredentials
{
    public string LoginProvider { get; }
    public string ProviderUserId { get; }
    public IDPTokens Tokens { get; }

    public ExternalUserCredentials(string loginProvider, string providerUserId, IDPTokens tokens)
    {
        LoginProvider = loginProvider;
        ProviderUserId = providerUserId;
        Tokens = tokens;
    }
}
