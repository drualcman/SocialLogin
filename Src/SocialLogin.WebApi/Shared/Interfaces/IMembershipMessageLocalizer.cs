namespace SocialLogin.WebApi.Shared.Interfaces;
public interface IMembershipMessageLocalizer
{
    string this[string key] { get; }

}
