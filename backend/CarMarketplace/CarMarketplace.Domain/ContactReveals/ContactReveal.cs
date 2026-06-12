using CarMarketplace.Domain.Abstractions;

namespace CarMarketplace.Domain.ContactReveals;

public class ContactReveal : IEntity
{
    public Guid Id { get; private set; }

    public Guid ListingId { get; private set; }

    public Guid ViewerId { get; private set; }

    public Guid ContactId { get; private set; }

    public DateTime RevealedAt { get; private set; }

    // EF Core
    private ContactReveal() { }

    public ContactReveal(Guid listingId, Guid viewerId, Guid contactId)
    {
        Id = Guid.NewGuid();
        ListingId = listingId;
        ViewerId = viewerId;
        ContactId = contactId;
        RevealedAt = DateTime.UtcNow;
    }
}
