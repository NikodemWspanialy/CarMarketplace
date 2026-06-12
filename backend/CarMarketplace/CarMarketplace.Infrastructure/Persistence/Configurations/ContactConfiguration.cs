using CarMarketplace.Domain.Contacts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarMarketplace.Infrastructure.Persistence.Configurations;

internal class ContactConfiguration : IEntityTypeConfiguration<Contact>
{
    public void Configure(EntityTypeBuilder<Contact> builder)
    {
        builder.ToTable("contacts");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).ValueGeneratedNever();
        builder.Property(x => x.SellerId).IsRequired();
        builder.Property(x => x.Type).HasConversion<int>().IsRequired();
        builder.Property(x => x.Label).HasMaxLength(100);
        builder.Property(x => x.IsDeleted).IsRequired();
        builder.Property(x => x.CreatedAt).IsRequired();
        builder.Property(x => x.UpdatedAt);

        builder.OwnsOne(x => x.Details, details =>
        {
            details.Property(d => d.PhoneNumber)
                .HasColumnName("phone_number")
                .HasMaxLength(20);

            details.Property(d => d.CountryCode)
                .HasColumnName("country_code")
                .HasMaxLength(5);

            details.Property(d => d.EmailAddress)
                .HasColumnName("email_address")
                .HasMaxLength(256);

            details.Property(d => d.Username)
                .HasColumnName("username")
                .HasMaxLength(100);
        });

        builder.HasQueryFilter(x => !x.IsDeleted);

        builder.HasIndex(x => x.SellerId);
    }
}
