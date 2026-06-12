using CarMarketplace.Application.Listings.Repositories;
using CarMarketplace.Application.Listings.Searchers;
using MediatR;

namespace CarMarketplace.Application.Admin.Commands.RemoveListingFeature;

internal class RemoveListingFeatureHandler(
    IListingSearcher listingSearcher,
    IListingRepository listingRepository)
    : IRequestHandler<RemoveListingFeatureRequest, Unit>
{
    public async Task<Unit> Handle(RemoveListingFeatureRequest request, CancellationToken token)
    {
        var listing = await listingSearcher.FindByIdAsync(request.ListingId, token);

        listing.RemoveFeature();
        await listingRepository.UpdateAsync(listing, token);

        return Unit.Value;
    }
}
