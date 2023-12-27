using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Pronia.Application.Abstractions.Helper;
using Pronia.Application.DTOs.TokenDtos;
using Pronia.Domain.Entities;
using Pronia.Persistence.Security.Encrypting;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Pronia.Persistence.Security.JWT;


public class JWTHelper : ITokenHelper
{
    private readonly IConfiguration _configuration;
    private readonly TokenOptionDto _tokenOptions;
    private readonly DateTime _expiresAt;
    public JWTHelper(IConfiguration configuration)
    {
        _configuration = configuration;
        _tokenOptions = _configuration.GetSection("TokenOptions").Get<TokenOptionDto>();
        _expiresAt = DateTime.UtcNow.AddMinutes(_tokenOptions.TokenExpiration);
    }

    public AccessToken CreateToken(List<Claim> claims)
    {
        JwtHeader jwtHeader = JwtHeaderCreate();
        JwtPayload jwtPayload = JwtPayloadCreate(claims);
        JwtSecurityToken jwtSecurityToken = new(jwtHeader, jwtPayload);

        return AccessTokenCreate(jwtSecurityToken);
         
    }

    private AccessToken AccessTokenCreate(JwtSecurityToken jwtSecurityToken)
    {
        JwtSecurityTokenHandler jwtSecurityTokenHandler = new();
        return new()
        {
            Token = jwtSecurityTokenHandler.WriteToken(jwtSecurityToken),
            ExpiredDate = _expiresAt
        };
    }

    private JwtPayload JwtPayloadCreate(List<Claim> claims)
    {
        return new(
            issuer: _tokenOptions.Issuer,
            audience: _tokenOptions.Audience,
            claims: claims,
            notBefore: DateTime.UtcNow,
            expires: _expiresAt
            );
    }

    private static JwtHeader JwtHeaderCreate()
    {
        SecurityKey securityKey = SecurityKeyHelper.CreateSecurityKey("salam");
        SigningCredentials signingCredentials = SignInCredentialsHelper.CreateSigninCredentianals(securityKey);
        JwtHeader jwtHeader = new(signingCredentials);
        return jwtHeader;
    }
}
