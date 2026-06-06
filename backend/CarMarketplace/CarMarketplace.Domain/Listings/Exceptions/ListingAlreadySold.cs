using CarMarketplace.Domain.Exceptions;

namespace CarMarketplace.Domain.Listings.Exceptions;

public class ListingAlreadySold()
    : DomainException("Listing is already marked as sold.");
