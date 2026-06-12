using CarMarketplace.Application.Listings.Helpers;
using CarMarketplace.Application.Listings.Repositories;
using CarMarketplace.Application.Listings.Searchers;
using MediatR;

namespace CarMarketplace.Application.Listings.Commands.DeleteListing;

internal class DeleteListingHandler(
    IListingSearcher listingSearcher,
    IListingSellerGuard listingSellerGuard,
    IListingRepository listingRepository)
    : IRequestHandler<DeleteListingRequest, Unit>
{
    public async Task<Unit> Handle(DeleteListingRequest request, CancellationToken token)
    {
        var listing = await listingSearcher.FindByIdAsync(request.Id, token);
        listingSellerGuard.EnsureCanMutate(listing.SellerId);

        listing.Delete();
        await listingRepository.UpdateAsync(listing, token);

        return Unit.Value;
    }
}
