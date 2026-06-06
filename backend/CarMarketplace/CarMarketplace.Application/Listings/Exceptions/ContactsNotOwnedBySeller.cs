using CarMarketplace.Domain.Exceptions;

namespace CarMarketplace.Application.Listings.Exceptions;

public class ContactsNotOwnedBySeller()
    : DomainException("One or more contacts do not belong to the current seller.");
