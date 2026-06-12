using CarMarketplace.Domain.Exceptions;

namespace CarMarketplace.Domain.Listings.Exceptions;

public class ListingContactNotAttached()
    : DomainException("Contact is not attached to this listing.");
