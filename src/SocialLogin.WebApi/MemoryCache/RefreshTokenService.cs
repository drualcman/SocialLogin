namespace SocialLogin.WebApi.MemoryCache;
internal class RefreshTokenService : IRefreshTokenService
{
    readonly JwtOptions Options;
    readonly IMemoryCache Cache;

    public RefreshTokenService(IOptions<JwtOptions> options, IMemoryCache cache)
    {
        Options = options.Value;
        Cache = cache;
    }

    public Task DeleteRefreshTokenAsync(string refreshToken)
    {
        Cache.Remove(refreshToken);
        return Task.CompletedTask;
    }
    public Task<string> GetRefreshTokenForAccessTokenAsync(string accessToken)
    {
        string refreshToken = GenerateToken();
        RefreshTokenInfo refreshTokenInfo = new(accessToken, DateTime.UtcNow.AddMinutes(Options.RefreshTokenExpireInMinutes));
        Cache.Set(refreshToken, refreshTokenInfo, DateTime.UtcNow.AddMinutes(Options.RefreshTokenExpireInMinutes + 5));
        return Task.FromResult(refreshToken);
    }
    public Task ThrowIfUnableToRotateRefreshTokenAsync(string refreshToken, string accessToken)
    {
        if(Cache.TryGetValue(refreshToken, out RefreshTokenInfo refreshTokenInfo))
        {
            Cache.Remove(refreshToken);
            if(refreshTokenInfo.AccessToken != accessToken)
                throw new RefreshTokenCompromisedException();

            if(refreshTokenInfo.RefreshTokenExpiresAt < DateTime.UtcNow)
                throw new RefreshTokenExpiredException();
        }
        else
            throw new RefreshTokenNotFoundException();
        return Task.CompletedTask;
    }

    static string GenerateToken()
    {
        byte[] buffer = new byte[75];
        using RandomNumberGenerator rng = RandomNumberGenerator.Create();
        rng.GetNonZeroBytes(buffer);
        return Convert.ToBase64String(buffer);
    }
}
