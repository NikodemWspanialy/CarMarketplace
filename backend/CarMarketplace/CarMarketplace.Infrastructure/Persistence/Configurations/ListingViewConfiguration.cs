using CarMarketplace.Domain.ListingViews;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarMarketplace.Infrastructure.Persistence.Configurations;

internal class ListingViewConfiguration : IEntityTypeConfiguration<ListingView>
{
    public void Configure(EntityTypeBuilder<ListingView> builder)
    {
        builder.ToTable("listing_views");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).ValueGeneratedNever();
        builder.Property(x => x.ListingId).IsRequired();
        builder.Property(x => x.ViewerId);
        builder.Property(x => x.ViewedAt).IsRequired();
        builder.Property(x => x.IpAddress).HasMaxLength(45);

        builder.HasIndex(x => new { x.ListingId, x.ViewerId, x.ViewedAt });
    }
}
