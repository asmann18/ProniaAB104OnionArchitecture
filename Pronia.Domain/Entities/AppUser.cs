using Microsoft.AspNetCore.Identity;

namespace Pronia.Domain.Entities;

public class AppUser:IdentityUser
{
    public string Fullname { get; set; }
}
