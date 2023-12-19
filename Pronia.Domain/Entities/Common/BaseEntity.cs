namespace Pronia.Domain.Entities.Common;

public class BaseEntity
{
    public int Id { get; set; }

    public string CreatedBy { get; set; }
    public DateTime CreatedTime { get; set; }
    public string? ModifiedBy { get; set; }
    public DateTime? ModifiedTime { get; set; }

    public BaseEntity()
    {
        CreatedBy = "Asiman";
        CreatedTime= DateTime.Now;
    }
}
