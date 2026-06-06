using CarMarketplace.Domain.Listings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarMarketplace.Infrastructure.Persistence.Configurations;

internal class ListingConfiguration : IEntityTypeConfiguration<Listing>
{
    public void Configure(EntityTypeBuilder<Listing> builder)
    {
        builder.ToTable("listings");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).ValueGeneratedNever();
        builder.Property(x => x.CarId).IsRequired();
        builder.Property(x => x.SellerId).IsRequired();
        builder.Property(x => x.Title).IsRequired().HasMaxLength(200);
        builder.Property(x => x.Status).HasConversion<int>().IsRequired();
        builder.Property(x => x.IsFeatured).IsRequired();
        builder.Property(x => x.FeaturedUntil);
        builder.Property(x => x.CreatedAt).IsRequired();
        builder.Property(x => x.UpdatedAt);
        builder.Property(x => x.ExpiresAt);
        builder.Property(x => x.IsDeleted).IsRequired();

        builder.PrimitiveCollection(x => x.ContactIds);

        builder.HasQueryFilter(x => !x.IsDeleted);

        builder.HasIndex(x => x.CarId)
            .IsUnique()
            .HasFilter("\"Status\" = 1");

        builder.HasIndex(x => x.SellerId);
        builder.HasIndex(x => x.CreatedAt);
    }
}
