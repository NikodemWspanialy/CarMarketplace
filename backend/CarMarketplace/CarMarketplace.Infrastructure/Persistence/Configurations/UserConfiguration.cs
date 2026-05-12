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

        // Indexes
        builder.HasIndex(x => x.Email).IsUnique();
    }
}
