using CarMarketplace.Domain.ContactReveals;

namespace CarMarketplace.Application.Listings.Repositories;

public interface IContactRevealRepository
{
    Task AddAsync(ContactReveal reveal, CancellationToken token = default);

    Task<bool> ExistsAsync(Guid listingId, Guid viewerId, CancellationToken token = default);

    Task<int> CountByListingIdAsync(Guid listingId, CancellationToken token = default);
}
