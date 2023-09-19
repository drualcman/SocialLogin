namespace SocialLogin.Client.Blazor.Interfaces;
public interface IAuthorizeService
{
    ExternalIDPInfo[] IDPs { get; }
    Task AuthorizeAsync(string providerId, ScopeAction scope, string returnUri);
}
