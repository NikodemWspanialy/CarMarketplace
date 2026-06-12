using CarMarketplace.Application.Contacts.DTOs;
using CarMarketplace.Application.Contacts.Repositories;
using CarMarketplace.Application.Listings.DTOs;
using CarMarketplace.Application.Listings.Searchers;
using MediatR;

namespace CarMarketplace.Application.Listings.Queries.GetListing;

internal class GetListingHandler(
    IListingSearcher listingSearcher,
    IContactRepository contactRepository)
    : IRequestHandler<GetListingRequest, ListingDetailsResponse>
{
    public async Task<ListingDetailsResponse> Handle(GetListingRequest request, CancellationToken token)
    {
        var listing = await listingSearcher.FindByIdAsync(request.Id, token);

        var contacts = await contactRepository.GetByIdsAsync(listing.ContactIds, token);
        var contactDtos = contacts.Select(ContactResponse.FromEntity).ToList();

        return ListingDetailsResponse.FromEntity(listing, contactDtos);
    }
}
