using Pronia.Application.Abstractions.Repositories;
using Pronia.Domain.Entities;
using Pronia.Persistence.Contexts;

namespace Pronia.Persistence.Implementations.Repositories;

public class CategoryRepository:Repository<Category>,ICategoryRepository
{
    public CategoryRepository(AppDbContext context): base(context)
    {
        
    }
}
