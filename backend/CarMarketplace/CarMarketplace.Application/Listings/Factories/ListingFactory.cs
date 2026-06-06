using CarMarketplace.Application.Listings.Commands.CreateListing;
using CarMarketplace.Domain.Listings;

namespace CarMarketplace.Application.Listings.Factories;

internal interface IListingFactory
{
    Listing Create(CreateListingRequest request, Guid sellerId);
}

internal class ListingFactory : IListingFactory
{
    public Listing Create(CreateListingRequest request, Guid sellerId)
    {
        var listing = new Listing(request.CarId, sellerId, request.Title);

        foreach (var contactId in request.ContactIds)
            listing.AttachContact(contactId);

        return listing;
    }
}
