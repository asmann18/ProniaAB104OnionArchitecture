using Pronia.Domain.Entities.Common;

namespace Pronia.Domain.Entities;

public class Product:BaseAuditableEntity
{
    public decimal Price { get; set; }
    public string Description { get; set; }
    public byte Rating { get; set; }

    public string SKU { get; set; }
    public int CategoryId { get; set; }
    public Category Category { get; set; }
    public int TagId { get; set; }
    public Tag Tag { get; set; }
}
