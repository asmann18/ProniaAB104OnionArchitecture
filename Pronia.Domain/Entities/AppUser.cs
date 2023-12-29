using Microsoft.AspNetCore.Identity;

namespace Pronia.Domain.Entities;

public class AppUser:IdentityUser
{
    public string Fullname { get; set; } = null!;
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiredAt { get; set; }
}
