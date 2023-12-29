﻿using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Pronia.Infrastructure.Security.Encrypting;

public static class SecurityKeyHelper
{
    public static SecurityKey CreateSecurityKey(string securitykey)
    {
        return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securitykey));
    }

}
