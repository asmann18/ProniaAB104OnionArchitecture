using Pronia.Domain.Entities.Common;

namespace Pronia.Domain.Entities;

public class Tag:BaseNamebleEntity
{
    public ICollection<ProductTag> ProductTags { get; set; }
}
