using CarMarketplace.Domain.Exceptions;

namespace CarMarketplace.Domain.Contacts.Exceptions;

public class InvalidContactDetails(string message)
    : DomainException(message);
