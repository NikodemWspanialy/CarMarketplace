using CarMarketplace.Domain.Exceptions;

namespace CarMarketplace.Application.Authorization.Exceptions;

public class EmailAlreadyTaken(string email) : DomainException($"Email '{email}' is already taken'");