using CarMarketplace.Application.Common.Interfaces;
using CarMarketplace.Application.Contacts.Factories;
using CarMarketplace.Application.Contacts.Repositories;
using CarMarketplace.Application.Contacts.Validators;
using MediatR;

namespace CarMarketplace.Application.Contacts.Commands.CreateContact;

internal class CreateContactHandler(
    IContactFactory contactFactory,
    IContactRepository contactRepository,
    ICurrentUserProvider currentUserProvider,
    ICreateContactValidator createContactValidator)
    : IRequestHandler<CreateContactRequest, Guid>
{
    public async Task<Guid> Handle(CreateContactRequest request, CancellationToken token)
    {
        var sellerId = currentUserProvider.GetUserId();

        await createContactValidator.ValidateContactLimitAsync(sellerId, token);

        var contact = contactFactory.Create(request, sellerId);
        await contactRepository.AddAsync(contact, token);

        return contact.Id;
    }
}
