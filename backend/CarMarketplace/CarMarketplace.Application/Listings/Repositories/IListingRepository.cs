using CarMarketplace.Domain.Listings;

namespace CarMarketplace.Application.Listings.Repositories;

public interface IListingRepository
{
    Task<Listing?> GetByIdAsync(Guid id, CancellationToken token = default);

    Task<Listing?> GetByCarIdActiveAsync(Guid carId, CancellationToken token = default);

    Task<Listing> AddAsync(Listing listing, CancellationToken token = default);

    Task UpdateAsync(Listing listing, CancellationToken token = default);

    Task<(IReadOnlyList<Listing> Listings, int TotalCount)> GetPagedActiveAsync(
        int pageNumber,
        int pageSize,
        CancellationToken token = default);
}
