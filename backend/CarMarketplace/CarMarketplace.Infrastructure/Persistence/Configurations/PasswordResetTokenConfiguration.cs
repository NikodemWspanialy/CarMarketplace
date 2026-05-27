using CarMarketplace.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarMarketplace.Infrastructure.Persistence.Configurations;

internal class PasswordResetTokenConfiguration : IEntityTypeConfiguration<PasswordResetToken>
{
    public void Configure(EntityTypeBuilder<PasswordResetToken> builder)
    {
        builder.ToTable("password_reset_tokens");
        builder.HasKey(x => x.Id);

        // Id
        builder.Property(x => x.Id).ValueGeneratedNever();

        // UserId
        builder.Property(x => x.UserId).IsRequired();

        // Token
        builder.Property(x => x.Token)
            .IsRequired()
            .HasMaxLength(128);

        // CreatedAt
        builder.Property(x => x.CreatedAt).IsRequired();

        // ExpiresAt
        builder.Property(x => x.ExpiresAt).IsRequired();

        // IsUsed
        builder.Property(x => x.IsUsed).IsRequired();

        // Ignore computed property
        builder.Ignore(x => x.IsValid);

        // Indexes
        builder.HasIndex(x => x.Token).IsUnique();
        builder.HasIndex(x => x.UserId);
    }
}
