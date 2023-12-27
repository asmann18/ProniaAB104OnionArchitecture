namespace Pronia.Application.DTOs.AppUserDtos;

public record AppUserGetDto
{
    public string Fullname { get; init; }
    public string Username { get; init; }
    public string Email { get; init; }

    public string Password { get; init; }
}
