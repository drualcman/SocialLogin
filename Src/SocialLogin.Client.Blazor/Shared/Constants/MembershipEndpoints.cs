namespace SocialLogin.Client.Blazor.Shared.Constants;
public class MembershipEndpoints
{
    public const string Register = "/user/register";
    public const string Login = "/user/login";
    public const string Logout = "/user/logout";
    public const string Refresh = "/user/refresh-token";
    public const string Authorize = "/oauth2/authorize";
    public const string AuthorizeCallback = "/oauth2/authorizecallback";
    public const string Token = "/oauth2/token";
}
