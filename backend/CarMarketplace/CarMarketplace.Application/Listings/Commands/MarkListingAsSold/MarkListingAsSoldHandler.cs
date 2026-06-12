using CarMarketplace.Application.Listings.Helpers;
using CarMarketplace.Application.Listings.Repositories;
using CarMarketplace.Application.Listings.Searchers;
using MediatR;

namespace CarMarketplace.Application.Listings.Commands.MarkListingAsSold;

internal class MarkListingAsSoldHandler(
    IListingSearcher listingSearcher,
    IListingSellerGuard listingSellerGuard,
    IListingRepository listingRepository)
    : IRequestHandler<MarkListingAsSoldRequest, Unit>
{
    public async Task<Unit> Handle(MarkListingAsSoldRequest request, CancellationToken token)
    {
        var listing = await listingSearcher.FindByIdAsync(request.Id, token);
        listingSellerGuard.EnsureCanMutate(listing.SellerId);

        listing.MarkAsSold();
        await listingRepository.UpdateAsync(listing, token);

        return Unit.Value;
    }
}
