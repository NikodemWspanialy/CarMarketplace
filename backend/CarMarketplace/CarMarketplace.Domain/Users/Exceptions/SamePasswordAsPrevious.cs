using CarMarketplace.Domain.Exceptions;

namespace CarMarketplace.Domain.Users.Exceptions;

public class SamePasswordAsPrevious()
    : DomainException("New password must be different from old password.");