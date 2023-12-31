﻿namespace SocialLogin.WebApi.Shared.Dtos;
public class UserTokensDto
{
    public string AccessToken { get; }
    public string RefreshToken { get; }

    public UserTokensDto(string accessToken, string refreshToken)
    {
        AccessToken = accessToken;
        RefreshToken = refreshToken;
    }
}
