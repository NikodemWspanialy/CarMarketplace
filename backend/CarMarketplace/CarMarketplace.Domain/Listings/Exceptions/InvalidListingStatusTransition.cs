using CarMarketplace.Domain.Exceptions;

namespace CarMarketplace.Domain.Listings.Exceptions;

public class InvalidListingStatusTransition(ListingStatus from, ListingStatus to)
    : DomainException($"Cannot transition listing from {from} to {to}.");
