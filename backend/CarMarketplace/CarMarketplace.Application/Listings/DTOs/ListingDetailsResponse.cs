using CarMarketplace.Application.Contacts.DTOs;
using CarMarketplace.Domain.Listings;

namespace CarMarketplace.Application.Listings.DTOs;

public record ListingDetailsResponse(
    Guid Id,
    Guid CarId,
    Guid SellerId,
    string Title,
    ListingStatus Status,
    bool IsFeatured,
    DateTime? FeaturedUntil,
    DateTime CreatedAt,
    DateTime? UpdatedAt,
    DateTime? ExpiresAt,
    IReadOnlyList<ContactResponse> Contacts)
{
    public static ListingDetailsResponse FromEntity(Listing listing, IReadOnlyList<ContactResponse> contacts) =>
        new(listing.Id,
            listing.CarId,
            listing.SellerId,
            listing.Title,
            listing.Status,
            listing.IsFeatured,
            listing.FeaturedUntil,
            listing.CreatedAt,
            listing.UpdatedAt,
            listing.ExpiresAt,
            contacts);
}
