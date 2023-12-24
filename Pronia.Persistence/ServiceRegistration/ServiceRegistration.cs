using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pronia.Application.Abstractions.Repositories;
using Pronia.Application.Abstractions.Services;
using Pronia.Domain.Entities;
using Pronia.Persistence.Contexts;
using Pronia.Persistence.Implementations.Repositories;
using Pronia.Persistence.Implementations.Services;
using Pronia.Persistence.Interceptors;

namespace Pronia.Persistence.ServiceRegistration;

public static class ServiceRegistration
{

    public static ModelBuilder AddBaseAuditableEntityQueryFilter(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>().HasQueryFilter(x => !x.IsDeleted);
        return modelBuilder;
    }

    public static IServiceCollection AddPersistenceServices(this IServiceCollection services,IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("Default")));

        //Repositories
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<ITagRepository, TagRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();


        //Services
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<ITagService, TagService>();
        services.AddScoped<IProductService, ProductService>();


        //Interceptors
        services.AddScoped<BaseEntityInterceptor>();

        return services;
    }
}
