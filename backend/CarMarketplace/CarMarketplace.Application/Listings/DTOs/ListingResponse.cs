using CarMarketplace.Domain.Listings;

namespace CarMarketplace.Application.Listings.DTOs;

public record ListingResponse(
    Guid Id,
    Guid CarId,
    string Title,
    ListingStatus Status,
    bool IsFeatured,
    DateTime CreatedAt)
{
    public static ListingResponse FromEntity(Listing listing) =>
        new(listing.Id,
            listing.CarId,
            listing.Title,
            listing.Status,
            listing.IsFeatured,
            listing.CreatedAt);
}
