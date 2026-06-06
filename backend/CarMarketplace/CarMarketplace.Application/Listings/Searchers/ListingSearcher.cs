using CarMarketplace.Application.Listings.Exceptions;
using CarMarketplace.Application.Listings.Repositories;
using CarMarketplace.Domain.Listings;

namespace CarMarketplace.Application.Listings.Searchers;

internal interface IListingSearcher
{
    Task<Listing> FindByIdAsync(Guid id, CancellationToken token = default);
}

internal class ListingSearcher(IListingRepository listingRepository) : IListingSearcher
{
    public async Task<Listing> FindByIdAsync(Guid id, CancellationToken token = default) =>
        await listingRepository.GetByIdAsync(id, token) ?? throw new ListingNotFound(id);
}
