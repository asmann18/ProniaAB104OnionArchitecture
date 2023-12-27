namespace Pronia.Application.DTOs.AppUserDtos;

public record AppUserRegisterDto
{
    public string Fullname { get; init; }
    public string Username { get; init; }
    public string Email { get; init; }

    public string Password { get; init; }
}
