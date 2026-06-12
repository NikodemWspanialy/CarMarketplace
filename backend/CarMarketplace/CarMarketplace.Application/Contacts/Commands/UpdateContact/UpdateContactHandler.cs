using CarMarketplace.Application.Contacts.DTOs;
using CarMarketplace.Application.Contacts.Helpers;
using CarMarketplace.Application.Contacts.Repositories;
using CarMarketplace.Application.Contacts.Searchers;
using CarMarketplace.Domain.Contacts;
using MediatR;

namespace CarMarketplace.Application.Contacts.Commands.UpdateContact;

internal class UpdateContactHandler(
    IContactSearcher contactSearcher,
    IContactSellerGuard contactSellerGuard,
    IContactRepository contactRepository)
    : IRequestHandler<UpdateContactRequest, ContactResponse>
{
    public async Task<ContactResponse> Handle(UpdateContactRequest request, CancellationToken token)
    {
        var contact = await contactSearcher.FindByIdAsync(request.Id, token);
        contactSellerGuard.EnsureCanMutate(contact.SellerId);

        contact.Update(
            request.Type,
            new ContactDetails(request.PhoneNumber, request.CountryCode, request.EmailAddress, request.Username),
            request.Label);

        await contactRepository.UpdateAsync(contact, token);

        return ContactResponse.FromEntity(contact);
    }
}
