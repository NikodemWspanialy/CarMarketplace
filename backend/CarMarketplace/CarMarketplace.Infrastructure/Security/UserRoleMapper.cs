using CarMarketplace.Domain.Users;

namespace CarMarketplace.Infrastructure.Security;

internal static class UserRoleMapper
{
    public static UserRole Map(string role) =>
        Enum.TryParse<UserRole>(role, ignoreCase: true, out var result)
            ? result
            : UserRole.User;
}
