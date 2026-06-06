using CarMarketplace.Application.Listings.DTOs;
using CarMarketplace.Application.Listings.Helpers;
using CarMarketplace.Application.Listings.Repositories;
using CarMarketplace.Application.Listings.Searchers;
using MediatR;

namespace CarMarketplace.Application.Listings.Queries.GetListingStats;

internal class GetListingStatsHandler(
    IListingSearcher listingSearcher,
    IListingSellerGuard listingSellerGuard,
    IListingViewRepository listingViewRepository,
    IContactRevealRepository contactRevealRepository)
    : IRequestHandler<GetListingStatsRequest, ListingStatsResponse>
{
    public async Task<ListingStatsResponse> Handle(GetListingStatsRequest request, CancellationToken token)
    {
        var listing = await listingSearcher.FindByIdAsync(request.Id, token);
        listingSellerGuard.EnsureCanMutate(listing.SellerId);

        var viewCount = await listingViewRepository.CountByListingIdAsync(listing.Id, token);
        var revealCount = await contactRevealRepository.CountByListingIdAsync(listing.Id, token);

        return new ListingStatsResponse(viewCount, revealCount);
    }
}
