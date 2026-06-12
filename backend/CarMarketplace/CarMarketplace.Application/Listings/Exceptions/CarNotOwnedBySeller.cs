using CarMarketplace.Domain.Exceptions;

namespace CarMarketplace.Application.Listings.Exceptions;

public class CarNotOwnedBySeller()
    : DomainException("The car does not belong to the current seller.");
