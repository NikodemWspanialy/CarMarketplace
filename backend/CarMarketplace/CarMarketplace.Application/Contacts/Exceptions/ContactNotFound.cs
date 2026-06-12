using CarMarketplace.Domain.Exceptions;

namespace CarMarketplace.Application.Contacts.Exceptions;

public class ContactNotFound(Guid id)
    : DomainException($"Contact with id '{id}' was not found.");
