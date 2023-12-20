using Pronia.Application.Abstractions.Repositories;
using Pronia.Domain.Entities;
using Pronia.Persistence.Contexts;

namespace Pronia.Persistence.Implementations.Repositories;

public class TagRepository:Repository<Tag>,ITagRepository
{
    public TagRepository(AppDbContext context):base(context)
    {
        
    }
}
