using CarMarketplace.Application.Common.Interfaces;
using CarMarketplace.Application.Contacts.DTOs;
using CarMarketplace.Application.Contacts.Repositories;
using CarMarketplace.Application.Listings.Repositories;
using CarMarketplace.Application.Listings.Searchers;
using CarMarketplace.Domain.ContactReveals;
using MediatR;

namespace CarMarketplace.Application.Listings.Commands.RevealListingContacts;

internal class RevealListingContactsHandler(
    IListingSearcher listingSearcher,
    IContactRepository contactRepository,
    IContactRevealRepository contactRevealRepository,
    ICurrentUserProvider currentUserProvider)
    : IRequestHandler<RevealListingContactsRequest, IReadOnlyList<ContactResponse>>
{
    public async Task<IReadOnlyList<ContactResponse>> Handle(RevealListingContactsRequest request, CancellationToken token)
    {
        var listing = await listingSearcher.FindByIdAsync(request.ListingId, token);
        var viewerId = currentUserProvider.GetUserId();

        // Register reveals if not already revealed (skip if viewer is the seller)
        var alreadyRevealed = await contactRevealRepository.ExistsAsync(listing.Id, viewerId, token);
        if (!alreadyRevealed && viewerId != listing.SellerId)
            foreach (var contactId in listing.ContactIds)
            {
                var reveal = new ContactReveal(listing.Id, viewerId, contactId);
                await contactRevealRepository.AddAsync(reveal, token);
            }

        // Return contact data
        var contacts = await contactRepository.GetByIdsAsync(listing.ContactIds, token);

        return contacts.Select(ContactResponse.FromEntity).ToList();
    }
}
