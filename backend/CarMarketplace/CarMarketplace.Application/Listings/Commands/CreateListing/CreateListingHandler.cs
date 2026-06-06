using CarMarketplace.Application.Cars.Searchers;
using CarMarketplace.Application.Common.Interfaces;
using CarMarketplace.Application.Contacts.Repositories;
using CarMarketplace.Application.Listings.Exceptions;
using CarMarketplace.Application.Listings.Factories;
using CarMarketplace.Application.Listings.Repositories;
using MediatR;

namespace CarMarketplace.Application.Listings.Commands.CreateListing;

internal class CreateListingHandler(
    ICarSearcher carSearcher,
    IListingRepository listingRepository,
    IContactRepository contactRepository,
    IListingFactory listingFactory,
    ICurrentUserProvider currentUserProvider)
    : IRequestHandler<CreateListingRequest, Guid>
{
    public async Task<Guid> Handle(CreateListingRequest request, CancellationToken token)
    {
        var sellerId = currentUserProvider.GetUserId();

        // Validate car exists
        await carSearcher.FindByIdAsync(request.CarId, token);

        // Validate no active listing for this car
        var existing = await listingRepository.GetByCarIdActiveAsync(request.CarId, token);
        if (existing is not null)
            throw new ActiveListingAlreadyExists(request.CarId);

        // Validate contacts belong to seller
        var contacts = await contactRepository.GetByIdsAsync(request.ContactIds, token);
        if (contacts.Count != request.ContactIds.Count || contacts.Any(c => c.SellerId != sellerId))
            throw new ContactsNotOwnedBySeller();

        var listing = listingFactory.Create(request, sellerId);
        await listingRepository.AddAsync(listing, token);

        return listing.Id;
    }
}
