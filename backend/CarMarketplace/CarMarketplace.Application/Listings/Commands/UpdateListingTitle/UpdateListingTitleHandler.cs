using CarMarketplace.Application.Listings.Helpers;
using CarMarketplace.Application.Listings.Repositories;
using CarMarketplace.Application.Listings.Searchers;
using MediatR;

namespace CarMarketplace.Application.Listings.Commands.UpdateListingTitle;

internal class UpdateListingTitleHandler(
    IListingSearcher listingSearcher,
    IListingSellerGuard listingSellerGuard,
    IListingRepository listingRepository)
    : IRequestHandler<UpdateListingTitleRequest, Unit>
{
    public async Task<Unit> Handle(UpdateListingTitleRequest request, CancellationToken token)
    {
        var listing = await listingSearcher.FindByIdAsync(request.Id, token);
        listingSellerGuard.EnsureCanMutate(listing.SellerId);

        listing.UpdateTitle(request.Title);
        await listingRepository.UpdateAsync(listing, token);

        return Unit.Value;
    }
}
