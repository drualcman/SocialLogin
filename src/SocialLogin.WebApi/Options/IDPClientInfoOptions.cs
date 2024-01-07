namespace SocialLogin.WebApi.Options;
public class IDPClientInfoOptions
{
    public const string SectionKey = "OAth2:IDPClients";
    public IEnumerable<IDPClientInfo> IDPClients { get; set; }

}
