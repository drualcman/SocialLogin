namespace SocialLogin.WebApi.Options;
public class AppClientInfoOptions
{
    public const string SectionKey = "OAth2:AppClients";
    public IEnumerable<AppClientInfo> AppClients { get; set; }

}
