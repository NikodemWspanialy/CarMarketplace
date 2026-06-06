using CarMarketplace.Application.Contacts.Searchers;
using CarMarketplace.Application.Listings.Helpers;
using CarMarketplace.Application.Listings.Repositories;
using CarMarketplace.Application.Listings.Searchers;
using MediatR;

namespace CarMarketplace.Application.Listings.Commands.AttachListingContact;

internal class AttachListingContactHandler(
    IListingSearcher listingSearcher,
    IListingSellerGuard listingSellerGuard,
    IContactSearcher contactSearcher,
    IListingRepository listingRepository)
    : IRequestHandler<AttachListingContactRequest, Unit>
{
    public async Task<Unit> Handle(AttachListingContactRequest request, CancellationToken token)
    {
        var listing = await listingSearcher.FindByIdAsync(request.ListingId, token);
        listingSellerGuard.EnsureCanMutate(listing.SellerId);

        // Validate contact exists
        await contactSearcher.FindByIdAsync(request.ContactId, token);

        listing.AttachContact(request.ContactId);
        await listingRepository.UpdateAsync(listing, token);

        return Unit.Value;
    }
}
