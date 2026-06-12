using CarMarketplace.Application.Common.Interfaces;
using CarMarketplace.Application.Listings.Repositories;
using CarMarketplace.Application.Listings.Searchers;
using CarMarketplace.Domain.ListingViews;
using MediatR;

namespace CarMarketplace.Application.Listings.Commands.RegisterListingView;

internal class RegisterListingViewHandler(
    IListingSearcher listingSearcher,
    IListingViewRepository listingViewRepository,
    ICurrentUserProvider currentUserProvider,
    IClientInfoProvider clientInfoProvider)
    : IRequestHandler<RegisterListingViewRequest, Unit>
{
    public async Task<Unit> Handle(RegisterListingViewRequest request, CancellationToken token)
    {
        var listing = await listingSearcher.FindByIdAsync(request.ListingId, token);
        var viewerId = currentUserProvider.GetUserIdOrNull();

        var hasRecentView = await listingViewRepository.ExistsRecentViewAsync(
            listing.Id, viewerId, TimeSpan.FromHours(24), token);

        if (hasRecentView) return Unit.Value;
        
        var ipAddress = clientInfoProvider.GetIpAddress();
        var view = new ListingView(listing.Id, viewerId, ipAddress);
        await listingViewRepository.AddAsync(view, token);

        return Unit.Value;
    }
}
