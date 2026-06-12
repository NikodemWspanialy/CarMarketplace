using CarMarketplace.Application.Contacts.Helpers;
using CarMarketplace.Application.Contacts.Repositories;
using CarMarketplace.Application.Contacts.Searchers;
using MediatR;

namespace CarMarketplace.Application.Contacts.Commands.DeleteContact;

internal class DeleteContactHandler(
    IContactSearcher contactSearcher,
    IContactSellerGuard contactSellerGuard,
    IContactRepository contactRepository)
    : IRequestHandler<DeleteContactRequest, Unit>
{
    public async Task<Unit> Handle(DeleteContactRequest request, CancellationToken token)
    {
        var contact = await contactSearcher.FindByIdAsync(request.Id, token);
        contactSellerGuard.EnsureCanMutate(contact.SellerId);

        contact.Delete();

        await contactRepository.UpdateAsync(contact, token);

        return Unit.Value;
    }
}
