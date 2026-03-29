using CarMarketplace.Domain.Cars;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarMarketplace.Infrastructure.Persistence.Configurations;

internal sealed class CarPhotoConfiguration : IEntityTypeConfiguration<CarPhoto>
{
    public void Configure(EntityTypeBuilder<CarPhoto> builder)
    {
        builder.ToTable("car_photos");
        builder.HasKey(x => x.Id);

        // Id
        builder.Property(x => x.Id).ValueGeneratedNever();

        // CarId
        builder.Property(x => x.CarId).IsRequired();

        // Url
        builder.Property(x => x.Url)
            .IsRequired()
            .HasMaxLength(1000);

        // Is primary
        builder.Property(x => x.IsPrimary).IsRequired();

        // Order
        builder.Property(x => x.Order).IsRequired();

        // Is deleted
        builder.Property(x => x.IsDeleted).IsRequired();

        // Indexes
        builder.HasIndex(x => x.CarId);
    }
}