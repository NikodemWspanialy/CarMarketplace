using CarMarketplace.Application.Common.Abstractions;

namespace CarMarketplace.Application.Listings.Commands.CreateListing;

public record CreateListingRequest(
    Guid CarId,
    string Title,
    List<Guid> ContactIds) : ICommand<Guid>;
