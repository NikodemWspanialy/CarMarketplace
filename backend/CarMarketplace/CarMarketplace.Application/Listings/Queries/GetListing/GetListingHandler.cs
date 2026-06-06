using CarMarketplace.Application.Common.Interfaces;
using CarMarketplace.Application.Contacts.DTOs;
using CarMarketplace.Application.Contacts.Repositories;
using CarMarketplace.Application.Listings.DTOs;
using CarMarketplace.Application.Listings.Repositories;
using CarMarketplace.Application.Listings.Searchers;
using CarMarketplace.Domain.ListingViews;
using MediatR;

namespace CarMarketplace.Application.Listings.Queries.GetListing;

internal class GetListingHandler(
    IListingSearcher listingSearcher,
    IContactRepository contactRepository,
    IListingViewRepository listingViewRepository,
    ICurrentUserProvider currentUserProvider)
    : IRequestHandler<GetListingRequest, ListingDetailsResponse>
{
    public async Task<ListingDetailsResponse> Handle(GetListingRequest request, CancellationToken token)
    {
        var listing = await listingSearcher.FindByIdAsync(request.Id, token);

        // Load contacts
        var contacts = await contactRepository.GetByIdsAsync(listing.ContactIds, token);
        var contactDtos = contacts.Select(ContactResponse.FromEntity).ToList();

        // Register view with 24h dedup
        Guid? viewerId = null;
        try { viewerId = currentUserProvider.GetUserId(); } catch { /* anonymous */ }

        var hasRecentView = await listingViewRepository.ExistsRecentViewAsync(
            listing.Id, viewerId, TimeSpan.FromHours(24), token);

        if (!hasRecentView)
        {
            var view = new ListingView(listing.Id, viewerId, null);
            await listingViewRepository.AddAsync(view, token);
        }

        return ListingDetailsResponse.FromEntity(listing, contactDtos);
    }
}
