using CarMarketplace.Domain.Exceptions;

namespace CarMarketplace.Domain.Common.Exceptions;

public sealed class EmptyOrNullCurrency()
    : DomainException("Currency cannot be empty or null.");