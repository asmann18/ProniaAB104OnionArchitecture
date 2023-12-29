using Microsoft.IdentityModel.Tokens;

namespace Pronia.Infrastructure.Security.Encrypting;

public static class SignInCredentialsHelper
{
    public static SigningCredentials CreateSigninCredentianals(SecurityKey securityKey)
    {
       return  new(securityKey, SecurityAlgorithms.HmacSha256);
    }
}
