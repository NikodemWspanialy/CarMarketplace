using CarMarketplace.Domain.Exceptions;

namespace CarMarketplace.Domain.Listings.Exceptions;

public class ListingAlreadyDeleted()
    : DomainException("Listing is already deleted.");
