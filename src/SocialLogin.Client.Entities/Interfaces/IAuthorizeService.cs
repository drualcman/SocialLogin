namespace SocialLogin.Client.Entities.Interfaces;
public interface IAuthorizeService
{
    ExternalIDPInfo[] IDPs { get; }
    Task AuthorizeAsync(string providerId, ScopeAction scope, string returnUri);
}
