using Pronia.Domain.Entities;
using System.Security.Claims;

namespace Pronia.Application.Abstractions.Helper;

public interface ITokenHelper
{
    AccessToken CreateToken(List<Claim> claims);
}
