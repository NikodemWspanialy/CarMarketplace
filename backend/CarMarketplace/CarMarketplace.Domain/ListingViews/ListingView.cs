using CarMarketplace.Domain.Abstractions;

namespace CarMarketplace.Domain.ListingViews;

public class ListingView : IEntity
{
    public Guid Id { get; private set; }

    public Guid ListingId { get; private set; }

    public Guid? ViewerId { get; private set; }

    public DateTime ViewedAt { get; private set; }

    public string? IpAddress { get; private set; }

    // EF Core
    private ListingView() { }

    public ListingView(Guid listingId, Guid? viewerId, string? ipAddress)
    {
        Id = Guid.NewGuid();
        ListingId = listingId;
        ViewerId = viewerId;
        ViewedAt = DateTime.UtcNow;
        IpAddress = ipAddress;
    }
}
