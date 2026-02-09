using CarMarketplace.Domain.Cars;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarMarketplace.Infrastructure.Persistence.Configurations;

public class CarConfiguration : IEntityTypeConfiguration<Car>
{
    public void Configure(EntityTypeBuilder<Car> builder)
    {
        builder.ToTable("cars");
        builder.HasKey(x => x.Id);

        // Id
        builder.Property(x => x.Id).ValueGeneratedNever();

        // Brand
        builder.Property(x => x.Brand).IsRequired().HasMaxLength(100);

        // Model
        builder.Property(x => x.Model).IsRequired().HasMaxLength(100);

        // Year
        builder.Property(x => x.Year).IsRequired();

        // Price
        builder.Property(x => x.Price).IsRequired().HasPrecision(18, 2);

        // Mileage
        builder.Property(x => x.Mileage).IsRequired();

        // FuelType enum
        builder.Property(x => x.FuelType).HasConversion<int>().IsRequired();

        // Description
        builder.Property(x => x.Description).HasMaxLength(2000);

        // Dates
        builder.Property(x => x.CreatedAt).IsRequired();
        builder.Property(x => x.UpdatedAt);

        // Soft delete
        builder.Property(x => x.IsDeleted).IsRequired();

        // Global filter
        builder.HasQueryFilter(x => !x.IsDeleted);

        // Indexes
        builder.HasIndex(x => x.Brand);
        builder.HasIndex(x => x.Model);
        builder.HasIndex(x => x.Price);
        builder.HasIndex(x => x.CreatedAt);
    }
}