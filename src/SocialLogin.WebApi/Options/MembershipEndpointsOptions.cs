namespace SocialLogin.WebApi.Options;
public class MembershipEndpointsOptions
{
    public const string SectionKey = "MembershipEndpoints";
    public string Logout { get; set; } = MembershipEndpoints.Logout;
    public string Refresh { get; set; } = MembershipEndpoints.Refresh;
    public string Authorize { get; set; } = MembershipEndpoints.Authorize;
    public string AuthorizeCallback { get; set; } = MembershipEndpoints.AuthorizeCallback;
    public string Token { get; set; } = MembershipEndpoints.Token;
}
