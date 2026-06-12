using CarMarketplace.Application.Listings.Repositories;
using CarMarketplace.Domain.ListingViews;
using CarMarketplace.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CarMarketplace.Infrastructure.Repositories;

public class ListingViewRepository(CarMarketplaceDbContext dbContext) : IListingViewRepository
{
    public async Task AddAsync(ListingView view, CancellationToken token = default) =>
        await dbContext.ListingViews.AddAsync(view, token);

    public async Task<bool> ExistsRecentViewAsync(Guid listingId, Guid? viewerId, TimeSpan window, CancellationToken token = default)
    {
        if (viewerId is null)
            return false;

        var cutoff = DateTime.UtcNow - window;

        return await dbContext.ListingViews
            .AnyAsync(x => x.ListingId == listingId && x.ViewerId == viewerId && x.ViewedAt >= cutoff, token);
    }

    public async Task<int> CountByListingIdAsync(Guid listingId, CancellationToken token = default) =>
        await dbContext.ListingViews.CountAsync(x => x.ListingId == listingId, token);
}
