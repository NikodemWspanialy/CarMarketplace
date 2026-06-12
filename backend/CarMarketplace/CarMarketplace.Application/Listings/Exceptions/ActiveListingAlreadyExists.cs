using CarMarketplace.Domain.Exceptions;

namespace CarMarketplace.Application.Listings.Exceptions;

public class ActiveListingAlreadyExists(Guid carId)
    : DomainException($"An active listing already exists for car '{carId}'.");
