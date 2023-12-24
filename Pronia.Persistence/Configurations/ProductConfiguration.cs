using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pronia.Domain.Entities;

namespace Pronia.Persistence.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasIndex(x=>x.Name).IsUnique();
        builder.Property(x=>x.Name).HasMaxLength(64);
        builder.Property(x=>x.SKU).IsRequired().HasMaxLength(32);
        builder.Property(x => x.Price).IsRequired().HasColumnType("decimal(6,2)");
        builder.Property(x => x.Description).IsRequired().HasMaxLength(2000);

    }
}
