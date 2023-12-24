using Pronia.Application.Abstractions.Repositories;
using Pronia.Domain.Entities;
using Pronia.Persistence.Contexts;

namespace Pronia.Persistence.Implementations.Repositories;

public class ProductRepository:Repository<Product>, IProductRepository
{
    public ProductRepository(AppDbContext context): base(context)
    {
        
    }
}
