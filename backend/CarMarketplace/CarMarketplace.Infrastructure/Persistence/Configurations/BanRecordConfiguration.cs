using CarMarketplace.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarMarketplace.Infrastructure.Persistence.Configurations;

internal class BanRecordConfiguration : IEntityTypeConfiguration<BanRecord>
{
    public void Configure(EntityTypeBuilder<BanRecord> builder)
    {
        builder.ToTable("ban_records");
        builder.HasKey(x => x.Id);

        // Id
        builder.Property(x => x.Id).ValueGeneratedNever();

        // UserId
        builder.Property(x => x.UserId).IsRequired();

        // Reason
        builder.Property(x => x.Reason)
            .IsRequired()
            .HasMaxLength(500);

        // BannedAt
        builder.Property(x => x.BannedAt).IsRequired();

        // ExpiresAt
        builder.Property(x => x.ExpiresAt);

        // UnbannedAt
        builder.Property(x => x.UnbannedAt);

        // UnbanReason
        builder.Property(x => x.UnbanReason).HasMaxLength(500);

        // BannedByAdminId
        builder.Property(x => x.BannedByAdminId).IsRequired();

        // UnbannedByAdminId
        builder.Property(x => x.UnbannedByAdminId);

        // Indexes
        builder.HasIndex(x => x.UserId);
    }
}
