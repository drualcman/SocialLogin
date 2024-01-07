namespace SocialLogin.WebApi.MemoryCache;
internal class RefreshTokenInfo
{
    public string AccessToken { get; }
    public DateTime RefreshTokenExpiresAt { get; }

    public RefreshTokenInfo(string accessToken, DateTime refreshTokenExpiresAt)
    {
        AccessToken = accessToken;
        RefreshTokenExpiresAt = refreshTokenExpiresAt;
    }
}
