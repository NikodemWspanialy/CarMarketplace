using CarMarketplace.Domain.Exceptions;

namespace CarMarketplace.Domain.Cars.Exceptions;

public class SamePriceAsCurrent()
    : DomainException("New price must be different from the current price.");