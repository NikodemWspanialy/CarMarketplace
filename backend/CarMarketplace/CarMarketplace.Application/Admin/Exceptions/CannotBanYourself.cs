using CarMarketplace.Domain.Exceptions;

namespace CarMarketplace.Application.Admin.Exceptions;

public class CannotBanYourself() : DomainException("Cannot ban yourself.");
