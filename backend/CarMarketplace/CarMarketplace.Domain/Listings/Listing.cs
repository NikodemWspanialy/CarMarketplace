using CarMarketplace.Domain.Abstractions;

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
    private Listing()
    {
    }

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
}