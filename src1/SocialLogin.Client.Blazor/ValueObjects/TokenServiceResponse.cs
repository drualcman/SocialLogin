namespace SocialLogin.Client.Blazor.ValueObjects;
internal class TokenServiceResponse
{
    public UserTokensDto Tokens { get; set; }
    public string Scope { get; set; }
    public string ReturnUri { get; set; }
}
