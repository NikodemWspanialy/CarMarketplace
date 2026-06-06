using CarMarketplace.Application.Contacts.Exceptions;
using CarMarketplace.Application.Contacts.Repositories;
using CarMarketplace.Domain.Contacts;

namespace CarMarketplace.Application.Contacts.Searchers;

internal interface IContactSearcher
{
    Task<Contact> FindByIdAsync(Guid id, CancellationToken token = default);
}

internal class ContactSearcher(IContactRepository contactRepository) : IContactSearcher
{
    public async Task<Contact> FindByIdAsync(Guid id, CancellationToken token = default) =>
        await contactRepository.GetByIdAsync(id, token) ?? throw new ContactNotFound(id);
}
