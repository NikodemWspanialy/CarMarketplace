using CarMarketplace.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarMarketplace.Infrastructure.Persistence.Configurations;

internal class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users");
        builder.HasKey(x => x.Id);

        // Id
        builder.Property(x => x.Id).ValueGeneratedNever();

        // Email
        builder.Property(x => x.Email)
            .IsRequired()
            .HasMaxLength(256);

        // PasswordHash
        builder.Property(x => x.PasswordHash)
            .IsRequired()
            .HasMaxLength(512);

        // FirstName
        builder.Property(x => x.FirstName)
            .IsRequired()
            .HasMaxLength(100);

        // LastName
        builder.Property(x => x.LastName)
            .IsRequired()
            .HasMaxLength(100);

        // Role enum
        builder.Property(x => x.Role).HasConversion<int>().IsRequired();

        // CreatedAt
        builder.Property(x => x.CreatedAt).IsRequired();

        // Soft delete
        builder.Property(x => x.IsDeleted).IsRequired();

        // Active ban (owned value object)
        builder.OwnsOne(x => x.ActiveBan, ban =>
        {
            ban.Property(b => b.Reason)
                .HasColumnName("ban_reason")
                .HasMaxLength(500);

            ban.Property(b => b.BannedAt)
                .HasColumnName("ban_banned_at");

            ban.Property(b => b.ExpiresAt)
                .HasColumnName("ban_expires_at");
        });

        // Ban history
        builder.HasMany(x => x.BanHistory)
            .WithOne()
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        // Ignore computed property
        builder.Ignore(x => x.IsBanned);

        // Indexes
        builder.HasIndex(x => x.Email).IsUnique();
    }
}
