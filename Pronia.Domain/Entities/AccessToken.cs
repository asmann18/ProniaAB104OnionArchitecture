namespace Pronia.Domain.Entities;

public class AccessToken
{
    public string Token { get; set; }
    public DateTime ExpiredDate { get; set; }
}
