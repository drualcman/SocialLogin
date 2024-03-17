namespace SocialLogin.Client.Blazor.Options;
public class UserEndpointsOptions
{
    public const string SectionKey = "UserEndpoints";
    public string WebApiBaseAddress { get; set; }
    public string Register { get; set; } = MembershipEndpoints.Register;
    public string Login { get; set; } = MembershipEndpoints.Login;
    public string Logout { get; set; } = MembershipEndpoints.Logout;
    public string Refresh { get; set; } = MembershipEndpoints.Refresh;

}
