using CarMarketplace.Domain.Abstractions;
using CarMarketplace.Domain.Contacts.Exceptions;

namespace CarMarketplace.Domain.Contacts;

public class Contact : IAggregateRoot
{
    public Guid Id { get; private set; }

    public Guid SellerId { get; private set; }

    public ContactType Type { get; private set; }

    public ContactDetails Details { get; private set; }

    public string? Label { get; private set; }

    public bool IsDeleted { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public DateTime? UpdatedAt { get; private set; }

    // EF Core
    private Contact() { }

    public Contact(Guid sellerId, ContactType type, ContactDetails details, string? label)
    {
        ValidateDetails(type, details);

        Id = Guid.NewGuid();
        SellerId = sellerId;
        Type = type;
        Details = details;
        Label = label;
        IsDeleted = false;
        CreatedAt = DateTime.UtcNow;
    }

    public void Update(ContactType type, ContactDetails details, string? label)
    {
        if (IsDeleted)
            throw new ContactAlreadyDeleted();

        ValidateDetails(type, details);

        Type = type;
        Details = details;
        Label = label;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Delete()
    {
        if (IsDeleted)
            throw new ContactAlreadyDeleted();

        IsDeleted = true;
        UpdatedAt = DateTime.UtcNow;
    }

    private static void ValidateDetails(ContactType type, ContactDetails details)
    {
        switch (type)
        {
            case ContactType.Phone:
            case ContactType.WhatsApp:
                if (string.IsNullOrWhiteSpace(details.PhoneNumber))
                    throw new InvalidContactDetails($"PhoneNumber is required for {type}.");
                break;

            case ContactType.Email:
                if (string.IsNullOrWhiteSpace(details.EmailAddress))
                    throw new InvalidContactDetails("EmailAddress is required for Email.");
                break;

            case ContactType.Telegram:
                if (string.IsNullOrWhiteSpace(details.Username))
                    throw new InvalidContactDetails("Username is required for Telegram.");
                break;
        }
    }
}
