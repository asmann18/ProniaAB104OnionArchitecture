using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pronia.Application.Abstractions.Repositories;
using Pronia.Application.Abstractions.Services;
using Pronia.Persistence.Contexts;
using Pronia.Persistence.Implementations.Repositories;
using Pronia.Persistence.Implementations.Services;

namespace Pronia.Persistence.ServiceRegistration;

public static class ServiceRegistration
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services,IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("Default")));

        //Repositories
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<ITagRepository, TagRepository>();


        //Services
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<ITagService, TagService>();
        return services;
    }
}
