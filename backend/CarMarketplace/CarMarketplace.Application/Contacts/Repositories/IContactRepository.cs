using CarMarketplace.Domain.Contacts;

namespace CarMarketplace.Application.Contacts.Repositories;

public interface IContactRepository
{
    Task<Contact?> GetByIdAsync(Guid id, CancellationToken token = default);

    Task<IReadOnlyList<Contact>> GetBySellerIdAsync(Guid sellerId, CancellationToken token = default);

    Task<IReadOnlyList<Contact>> GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken token = default);

    Task<Contact> AddAsync(Contact contact, CancellationToken token = default);

    Task UpdateAsync(Contact contact, CancellationToken token = default);

    Task<int> CountBySellerIdAsync(Guid sellerId, CancellationToken token = default);
}
