using CarMarketplace.Domain.ContactReveals;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarMarketplace.Infrastructure.Persistence.Configurations;

internal class ContactRevealConfiguration : IEntityTypeConfiguration<ContactReveal>
{
    public void Configure(EntityTypeBuilder<ContactReveal> builder)
    {
        builder.ToTable("contact_reveals");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).ValueGeneratedNever();
        builder.Property(x => x.ListingId).IsRequired();
        builder.Property(x => x.ViewerId).IsRequired();
        builder.Property(x => x.ContactId).IsRequired();
        builder.Property(x => x.RevealedAt).IsRequired();

        builder.HasIndex(x => new { x.ListingId, x.ViewerId });
    }
}
