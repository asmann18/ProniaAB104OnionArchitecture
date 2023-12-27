using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Pronia.Domain.Entities;
using Pronia.Persistence.Interceptors;
using Pronia.Persistence.ServiceRegistration;
using System.Reflection;

namespace Pronia.Persistence.Contexts;


public class AppDbContext:IdentityDbContext<AppUser>
{
    private readonly BaseEntityInterceptor _baseInterceptor;
    public AppDbContext(DbContextOptions<AppDbContext> options, BaseEntityInterceptor baseInterceptor) : base(options)
    {
        _baseInterceptor = baseInterceptor;
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
        modelBuilder.AddBaseAuditableEntityQueryFilter();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.AddInterceptors(_baseInterceptor);
    }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductTag> ProductTags { get; set; }



}
