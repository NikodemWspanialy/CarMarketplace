using CarMarketplace.Domain.Exceptions;

namespace CarMarketplace.Domain.Users.Exceptions;

public class SameEmailAsCurrent()
    : DomainException("New email is the same as the current one.");
