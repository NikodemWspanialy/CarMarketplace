using CarMarketplace.Domain.ListingViews;

namespace CarMarketplace.Application.Listings.Repositories;

public interface IListingViewRepository
{
    Task AddAsync(ListingView view, CancellationToken token = default);

    Task<bool> ExistsRecentViewAsync(Guid listingId, Guid? viewerId, TimeSpan window, CancellationToken token = default);

    Task<int> CountByListingIdAsync(Guid listingId, CancellationToken token = default);
}
