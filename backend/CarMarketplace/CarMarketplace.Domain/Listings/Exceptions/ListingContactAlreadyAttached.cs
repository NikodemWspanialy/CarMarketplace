using CarMarketplace.Domain.Exceptions;

namespace CarMarketplace.Domain.Listings.Exceptions;

public class ListingContactAlreadyAttached()
    : DomainException("Contact is already attached to this listing.");
