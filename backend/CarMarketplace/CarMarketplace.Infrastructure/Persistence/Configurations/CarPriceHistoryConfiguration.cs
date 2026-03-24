using CarMarketplace.Domain.Cars;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarMarketplace.Infrastructure.Persistence.Configurations;

internal class CarPriceHistoryConfiguration : IEntityTypeConfiguration<CarPriceHistory>
{
    public void Configure(EntityTypeBuilder<CarPriceHistory> builder)
    {
        builder.ToTable("car_price_history");
        builder.HasKey(x => x.Id);

        // Id
        builder.Property(x => x.Id).ValueGeneratedNever();

        // CarId
        builder.Property(x => x.CarId).IsRequired();

        // ChangedAt
        builder.Property(x => x.ChangedAt).IsRequired();

        // Price
        builder.OwnsOne(x => x.Price, money =>
        {
            money.Property(p => p.Amount)
                .HasColumnName("price_amount")
                .HasPrecision(18, 2)
                .IsRequired();

            money.Property(p => p.Currency)
                .HasColumnName("price_currency")
                .HasMaxLength(3)
                .IsRequired();
        });

        // Indexes
        builder.HasIndex(x => x.CarId);
        builder.HasIndex(x => x.ChangedAt);
    }
}