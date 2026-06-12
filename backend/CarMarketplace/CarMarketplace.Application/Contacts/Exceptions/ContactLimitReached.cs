using CarMarketplace.Domain.Exceptions;

namespace CarMarketplace.Application.Contacts.Exceptions;

public class ContactLimitReached()
    : DomainException("Maximum of 5 contacts allowed per seller.");
