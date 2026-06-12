using CarMarketplace.Application.Listings.Repositories;
using CarMarketplace.Domain.ContactReveals;
using CarMarketplace.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CarMarketplace.Infrastructure.Repositories;

public class ContactRevealRepository(CarMarketplaceDbContext dbContext) : IContactRevealRepository
{
    public async Task AddAsync(ContactReveal reveal, CancellationToken token = default) =>
        await dbContext.ContactReveals.AddAsync(reveal, token);

    public async Task<bool> ExistsAsync(Guid listingId, Guid viewerId, CancellationToken token = default) =>
        await dbContext.ContactReveals
            .AnyAsync(x => x.ListingId == listingId && x.ViewerId == viewerId, token);

    public async Task<int> CountByListingIdAsync(Guid listingId, CancellationToken token = default) =>
        await dbContext.ContactReveals.CountAsync(x => x.ListingId == listingId, token);
}
