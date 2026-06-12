using CarMarketplace.Application.Listings.Helpers;
using CarMarketplace.Application.Listings.Repositories;
using CarMarketplace.Application.Listings.Searchers;
using MediatR;

namespace CarMarketplace.Application.Listings.Commands.DeactivateListing;

internal class DeactivateListingHandler(
    IListingSearcher listingSearcher,
    IListingSellerGuard listingSellerGuard,
    IListingRepository listingRepository)
    : IRequestHandler<DeactivateListingRequest, Unit>
{
    public async Task<Unit> Handle(DeactivateListingRequest request, CancellationToken token)
    {
        var listing = await listingSearcher.FindByIdAsync(request.Id, token);
        listingSellerGuard.EnsureCanMutate(listing.SellerId);

        listing.Deactivate();
        await listingRepository.UpdateAsync(listing, token);

        return Unit.Value;
    }
}
