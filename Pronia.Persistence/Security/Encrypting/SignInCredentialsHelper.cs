using Microsoft.IdentityModel.Tokens;

namespace Pronia.Persistence.Security.Encrypting;

public static class SignInCredentialsHelper
{
    public static SigningCredentials CreateSigninCredentianals(SecurityKey securityKey)
    {
       return  new(securityKey, SecurityAlgorithms.HmacSha512Signature);
    }
}
