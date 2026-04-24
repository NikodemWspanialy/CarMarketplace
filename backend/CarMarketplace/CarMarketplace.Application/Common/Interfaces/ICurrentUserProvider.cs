using CarMarketplace.Domain.Users;

namespace CarMarketplace.Application.Common.Interfaces;

public interface ICurrentUserProvider
{
    Guid GetUserId();
    UserRole GetUserRole();
}
