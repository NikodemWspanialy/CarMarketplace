using CarMarketplace.Domain.Exceptions;

namespace CarMarketplace.Domain.Contacts.Exceptions;

public class ContactAlreadyDeleted()
    : DomainException("Contact is already deleted.");
