namespace Pronia.Application.DTOs.TokenDtos;

public record TokenDto
{
    public string Token { get; init; }
    public DateTime  ExpiredDate { get; init; }
}
