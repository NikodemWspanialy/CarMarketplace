using CarMarketplace.Application.Contacts.Commands.CreateContact;
using CarMarketplace.Domain.Contacts;

namespace CarMarketplace.Application.Contacts.Factories;

internal interface IContactFactory
{
    Contact Create(CreateContactRequest request, Guid sellerId);
}

internal class ContactFactory : IContactFactory
{
    public Contact Create(CreateContactRequest request, Guid sellerId) =>
        new Contact(
            sellerId,
            request.Type,
            new ContactDetails(request.PhoneNumber, request.CountryCode, request.EmailAddress, request.Username),
            request.Label);
}
