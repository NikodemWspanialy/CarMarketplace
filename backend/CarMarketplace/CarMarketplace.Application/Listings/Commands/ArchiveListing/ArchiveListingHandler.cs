using CarMarketplace.Application.Listings.Helpers;
using CarMarketplace.Application.Listings.Repositories;
using CarMarketplace.Application.Listings.Searchers;
using MediatR;

namespace CarMarketplace.Application.Listings.Commands.ArchiveListing;

internal class ArchiveListingHandler(
    IListingSearcher listingSearcher,
    IListingSellerGuard listingSellerGuard,
    IListingRepository listingRepository)
    : IRequestHandler<ArchiveListingRequest, Unit>
{
    public async Task<Unit> Handle(ArchiveListingRequest request, CancellationToken token)
    {
        var listing = await listingSearcher.FindByIdAsync(request.Id, token);
        listingSellerGuard.EnsureCanMutate(listing.SellerId);

        listing.Archive();
        await listingRepository.UpdateAsync(listing, token);

        return Unit.Value;
    }
}
