using CarMarketplace.Domain.Exceptions;

namespace CarMarketplace.Application.Admin.Exceptions;

public class CannotDemoteYourself() : DomainException("Cannot demote yourself.");
