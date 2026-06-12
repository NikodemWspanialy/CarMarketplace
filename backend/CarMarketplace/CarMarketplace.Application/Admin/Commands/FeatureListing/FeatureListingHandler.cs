using CarMarketplace.Application.Listings.Repositories;
using CarMarketplace.Application.Listings.Searchers;
using MediatR;

namespace CarMarketplace.Application.Admin.Commands.FeatureListing;

internal class FeatureListingHandler(
    IListingSearcher listingSearcher,
    IListingRepository listingRepository)
    : IRequestHandler<FeatureListingRequest, Unit>
{
    public async Task<Unit> Handle(FeatureListingRequest request, CancellationToken token)
    {
        var listing = await listingSearcher.FindByIdAsync(request.ListingId, token);

        listing.Feature(request.Until);
        await listingRepository.UpdateAsync(listing, token);

        return Unit.Value;
    }
}
