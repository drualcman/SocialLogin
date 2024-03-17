namespace SocialLogin.WebApi.ValueObjects;
public class IDPClientInfo
{
    public string ProviderId { get; set; }
    public string AuthorizeEndpoint { get; set; }
    public string EndSessionEndpoint { get; set; }
    public string TokenEndpoint { get; set; }
    public string ClientId { get; set; }
    public string ClientSecret { get; set; }
    public string RedirectUri { get; set; }
    public bool SupportsS256CodeChallengeMethod { get; set; }
    public string Scope { get; set; }
    public string Iss { get; set; }
    public string Aud{ get; set; }
}
