using SocialLogin.Client.Entities.ValueObjects;

namespace SocialLogin.Client.Blazor.Options;
public class AppOptions
{
    public const string SectionKey = "appoptions";
    public string AuthorizationEndpoint { get; set; }
    public string ClientId { get; set; }
    public string RedirectUri { get; set; }
    public string TokenEndpoint { get; set; }
    public ExternalIDPInfo[] IDPs { get; set; }
}
