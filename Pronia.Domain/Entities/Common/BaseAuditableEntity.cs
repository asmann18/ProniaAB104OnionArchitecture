namespace Pronia.Domain.Entities.Common;

public class BaseAuditableEntity:BaseEntity
{
    public string Name { get; set; }
    public string CreatedBy { get; set; }
    public DateTime CreatedTime { get; set; }
    public string? ModifiedBy { get; set; }
    public DateTime? ModifiedTime { get; set; }
    public bool IsDeleted { get; set; } = false;

}
