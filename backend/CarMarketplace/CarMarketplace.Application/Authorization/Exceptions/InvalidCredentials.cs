using CarMarketplace.Domain.Exceptions;

namespace CarMarketplace.Application.Authorization.Exceptions;

public class InvalidCredentials() : DomainException("Invalid Credentials.");