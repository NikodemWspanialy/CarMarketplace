using CarMarketplace.Domain.Exceptions;

namespace CarMarketplace.Domain.Users.Exceptions;

public sealed class InvalidPassword() 
    : DomainException("Invalid password.");