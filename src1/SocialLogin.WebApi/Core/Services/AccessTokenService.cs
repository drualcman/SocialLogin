namespace SocialLogin.WebApi.Core.Services;
internal class AccessTokenService : IAccessTokenService
{
    readonly JwtOptions JwtOptions;

    public AccessTokenService(IOptions<JwtOptions> jwtOptions) => JwtOptions = jwtOptions.Value;

    public Task<string> GetNewUserAccessTokenAsync(UserEntity user) =>
        Task.FromResult(GetAccessToken(GetUserClaims(user)));
    public Task<string> RotateAccessTokenAsync(string accessTokenToRorate) =>
        Task.FromResult(GetAccessToken(GetUserClaimsFromAccessToken(accessTokenToRorate)));

    string GetAccessToken(List<Claim> userClaims)
    {
        byte[] key = Encoding.UTF8.GetBytes(JwtOptions.SecurityKey);
        SymmetricSecurityKey secret = new SymmetricSecurityKey(key);
        SigningCredentials signingCredentials = new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        JwtSecurityToken tokenOptions = new JwtSecurityToken(
            issuer: JwtOptions.ValidIssuer,
            audience: JwtOptions.ValidAudience,
            claims: userClaims,
            expires: DateTime.Now.AddMinutes(JwtOptions.ExpireInMinutes),
            signingCredentials: signingCredentials);
        return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
    }

    static List<Claim> GetUserClaims(UserEntity user)
    {
        List<Claim> claims = new()
        {
            new Claim(ClaimTypes.Name, user.Email),
            new Claim("FullName", user.FullName)
        };
        if(user.Claims != null)
            claims.AddRange(user.Claims);
        return claims;
    }

    static List<Claim> GetUserClaimsFromAccessToken(string accessToken) =>
        new JwtSecurityTokenHandler()
        .ReadJwtToken(accessToken)
        .Claims.ToList();
}
