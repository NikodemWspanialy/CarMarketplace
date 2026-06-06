using CarMarketplace.Application.Common.Interfaces;
using CarMarketplace.Domain.Users;

namespace CarMarketplace.IntegrationTests.Common;

public class FakeCurrentUserProvider : ICurrentUserProvider
{
    public Guid UserId { get; set; }
    public UserRole Role { get; set; } = UserRole.User;

    public Guid GetUserId() => UserId;
    public Guid? GetUserIdOrNull() => UserId == Guid.Empty ? null : UserId;
    public UserRole GetUserRole() => Role;
}
