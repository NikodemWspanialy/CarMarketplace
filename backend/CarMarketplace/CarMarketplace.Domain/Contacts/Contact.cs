using CarMarketplace.Domain.Abstractions;

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
}
