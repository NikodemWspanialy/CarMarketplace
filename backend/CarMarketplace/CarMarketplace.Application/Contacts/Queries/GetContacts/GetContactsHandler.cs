using CarMarketplace.Application.Common.Interfaces;
using CarMarketplace.Application.Contacts.DTOs;
using CarMarketplace.Application.Contacts.Repositories;
using MediatR;

namespace CarMarketplace.Application.Contacts.Queries.GetContacts;

internal class GetContactsHandler(
    IContactRepository contactRepository,
    ICurrentUserProvider currentUserProvider)
    : IRequestHandler<GetContactsRequest, IReadOnlyList<ContactResponse>>
{
    public async Task<IReadOnlyList<ContactResponse>> Handle(GetContactsRequest request, CancellationToken token)
    {
        var sellerId = currentUserProvider.GetUserId();
        var contacts = await contactRepository.GetBySellerIdAsync(sellerId, token);

        return contacts.Select(ContactResponse.FromEntity).ToList();
    }
}
