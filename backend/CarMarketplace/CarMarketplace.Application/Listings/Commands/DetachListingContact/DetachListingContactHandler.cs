using CarMarketplace.Application.Listings.Helpers;
using CarMarketplace.Application.Listings.Repositories;
using CarMarketplace.Application.Listings.Searchers;
using MediatR;

namespace CarMarketplace.Application.Listings.Commands.DetachListingContact;

internal class DetachListingContactHandler(
    IListingSearcher listingSearcher,
    IListingSellerGuard listingSellerGuard,
    IListingRepository listingRepository)
    : IRequestHandler<DetachListingContactRequest, Unit>
{
    public async Task<Unit> Handle(DetachListingContactRequest request, CancellationToken token)
    {
        var listing = await listingSearcher.FindByIdAsync(request.ListingId, token);
        listingSellerGuard.EnsureCanMutate(listing.SellerId);

        listing.DetachContact(request.ContactId);
        await listingRepository.UpdateAsync(listing, token);

        return Unit.Value;
    }
}
