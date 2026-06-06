using CarMarketplace.Domain.Abstractions;
using CarMarketplace.Domain.Listings.Exceptions;

namespace CarMarketplace.Domain.Listings;

public class Listing : IAggregateRoot
{
    public Guid Id { get; private set; }

    public Guid CarId { get; private set; }

    public Guid SellerId { get; private set; }

    public string Title { get; private set; }

    public ListingStatus Status { get; private set; }

    public bool IsFeatured { get; private set; }

    public DateTime? FeaturedUntil { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public DateTime? UpdatedAt { get; private set; }

    public DateTime? ExpiresAt { get; private set; }

    public bool IsDeleted { get; private set; }

    public List<Guid> ContactIds { get; private set; }

    // EF Core
    private Listing() { }

    public Listing(Guid carId, Guid sellerId, string title)
    {
        Id = Guid.NewGuid();
        CarId = carId;
        SellerId = sellerId;
        Title = title;
        Status = ListingStatus.Active;
        IsFeatured = false;
        CreatedAt = DateTime.UtcNow;
        IsDeleted = false;
        ContactIds = [];
    }

    public void MarkAsSold()
    {
        if (Status == ListingStatus.Sold)
            throw new ListingAlreadySold();

        if (Status != ListingStatus.Active)
            throw new InvalidListingStatusTransition(Status, ListingStatus.Sold);

        Status = ListingStatus.Sold;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Archive()
    {
        if (Status is not (ListingStatus.Active or ListingStatus.Sold or ListingStatus.Deactivated))
            throw new InvalidListingStatusTransition(Status, ListingStatus.Archived);

        Status = ListingStatus.Archived;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Deactivate()
    {
        if (Status != ListingStatus.Active)
            throw new InvalidListingStatusTransition(Status, ListingStatus.Deactivated);

        Status = ListingStatus.Deactivated;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Reactivate()
    {
        if (Status != ListingStatus.Deactivated)
            throw new InvalidListingStatusTransition(Status, ListingStatus.Active);

        Status = ListingStatus.Active;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Delete()
    {
        if (IsDeleted)
            throw new ListingAlreadyDeleted();

        IsDeleted = true;
        UpdatedAt = DateTime.UtcNow;
    }

    public void AttachContact(Guid contactId)
    {
        if (ContactIds.Contains(contactId))
            throw new ListingContactAlreadyAttached();

        ContactIds.Add(contactId);
        UpdatedAt = DateTime.UtcNow;
    }

    public void DetachContact(Guid contactId)
    {
        if (!ContactIds.Contains(contactId))
            throw new ListingContactNotAttached();

        ContactIds.Remove(contactId);
        UpdatedAt = DateTime.UtcNow;
    }

    public void UpdateTitle(string title)
    {
        Title = title;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Feature(DateTime until)
    {
        IsFeatured = true;
        FeaturedUntil = until;
        UpdatedAt = DateTime.UtcNow;
    }

    public void RemoveFeature()
    {
        IsFeatured = false;
        FeaturedUntil = null;
        UpdatedAt = DateTime.UtcNow;
    }

    public void SetExpiration(DateTime expiresAt)
    {
        ExpiresAt = expiresAt;
        UpdatedAt = DateTime.UtcNow;
    }
}
