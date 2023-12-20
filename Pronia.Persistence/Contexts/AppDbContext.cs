using Microsoft.EntityFrameworkCore;
using Pronia.Domain.Entities;
using System.Reflection;

namespace Pronia.Persistence.Contexts;


public class AppDbContext:DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Tag> Tags { get; set; }
}
