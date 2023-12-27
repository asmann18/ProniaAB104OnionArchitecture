namespace Pronia.Application.DTOs.AppUserDtos;

public record AppUserLoginDto
{
    public string EmailOrUsername { get; init; }
    public string Password { get; init; }
}
