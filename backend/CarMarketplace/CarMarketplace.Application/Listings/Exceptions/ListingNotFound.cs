using CarMarketplace.Domain.Exceptions;

namespace CarMarketplace.Application.Listings.Exceptions;

public class ListingNotFound(Guid id)
    : DomainException($"Listing with id '{id}' was not found.");
