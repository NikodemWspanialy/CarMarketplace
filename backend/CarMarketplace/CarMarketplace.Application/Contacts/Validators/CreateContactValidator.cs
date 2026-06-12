using CarMarketplace.Application.Contacts.Exceptions;
using CarMarketplace.Application.Contacts.Repositories;

namespace CarMarketplace.Application.Contacts.Validators;

internal interface ICreateContactValidator
{
    Task ValidateContactLimitAsync(Guid sellerId, CancellationToken token = default);
}

internal class CreateContactValidator(IContactRepository contactRepository) : ICreateContactValidator
{
    public async Task ValidateContactLimitAsync(Guid sellerId, CancellationToken token = default)
    {
        var count = await contactRepository.CountBySellerIdAsync(sellerId, token);
        if (count >= 5)
            throw new ContactLimitReached();
    }
}
