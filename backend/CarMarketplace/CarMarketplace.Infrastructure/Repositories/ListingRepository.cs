using CarMarketplace.Application.Listings.Repositories;
using CarMarketplace.Domain.Listings;
using CarMarketplace.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CarMarketplace.Infrastructure.Repositories;

public class ListingRepository(CarMarketplaceDbContext dbContext) : IListingRepository
{
    public async Task<Listing?> GetByIdAsync(Guid id, CancellationToken token = default) =>
        await dbContext.Listings.FirstOrDefaultAsync(x => x.Id == id, token);

    public async Task<Listing?> GetByCarIdActiveAsync(Guid carId, CancellationToken token = default) =>
        await dbContext.Listings
            .FirstOrDefaultAsync(x => x.CarId == carId && x.Status == ListingStatus.Active, token);

    public async Task<Listing> AddAsync(Listing listing, CancellationToken token = default)
    {
        await dbContext.Listings.AddAsync(listing, token);

        return listing;
    }

    public Task UpdateAsync(Listing listing, CancellationToken token = default)
    {
        dbContext.Listings.Update(listing);

        return Task.CompletedTask;
    }

    public async Task<(IReadOnlyList<Listing> Listings, int TotalCount)> GetPagedActiveAsync(
        int pageNumber,
        int pageSize,
        CancellationToken token = default)
    {
        var query = dbContext.Listings
            .AsNoTracking()
            .Where(x => x.Status == ListingStatus.Active);

        var totalCount = await query.CountAsync(token);

        var listings = await query
            .OrderByDescending(x => x.IsFeatured)
            .ThenByDescending(x => x.CreatedAt)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(token);

        return (listings, totalCount);
    }
}
