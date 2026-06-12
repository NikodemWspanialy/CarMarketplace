using CarMarketplace.Application.Contacts.Repositories;
using CarMarketplace.Domain.Contacts;
using CarMarketplace.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CarMarketplace.Infrastructure.Repositories;

public class ContactRepository(CarMarketplaceDbContext dbContext) : IContactRepository
{
    public async Task<Contact?> GetByIdAsync(Guid id, CancellationToken token = default) =>
        await dbContext.Contacts.FirstOrDefaultAsync(x => x.Id == id, token);

    public async Task<IReadOnlyList<Contact>> GetBySellerIdAsync(Guid sellerId, CancellationToken token = default) =>
        await dbContext.Contacts
            .AsNoTracking()
            .Where(x => x.SellerId == sellerId)
            .ToListAsync(token);

    public async Task<IReadOnlyList<Contact>> GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken token = default) =>
        await dbContext.Contacts
            .Where(x => ids.Contains(x.Id))
            .ToListAsync(token);

    public async Task<Contact> AddAsync(Contact contact, CancellationToken token = default)
    {
        await dbContext.Contacts.AddAsync(contact, token);

        return contact;
    }

    public Task UpdateAsync(Contact contact, CancellationToken token = default)
    {
        dbContext.Contacts.Update(contact);

        return Task.CompletedTask;
    }

    public async Task<int> CountBySellerIdAsync(Guid sellerId, CancellationToken token = default) =>
        await dbContext.Contacts.CountAsync(x => x.SellerId == sellerId, token);
}
