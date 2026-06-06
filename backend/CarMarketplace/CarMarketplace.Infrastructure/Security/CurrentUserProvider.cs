using System.Security.Claims;
using CarMarketplace.Application.Common.Interfaces;
using CarMarketplace.Domain.Users;
using Microsoft.AspNetCore.Http;

namespace CarMarketplace.Infrastructure.Security;

internal class CurrentUserProvider(IHttpContextAccessor httpContextAccessor) : ICurrentUserProvider
{
    public Guid GetUserId()
    {
        var claim = httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)
            ?? throw new UnauthorizedAccessException("User is not authenticated.");

        return Guid.Parse(claim.Value);
    }

    public Guid? GetUserIdOrNull()
    {
        var claim = httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier);

        return claim is not null && Guid.TryParse(claim.Value, out var userId) ? userId : null;
    }

    public UserRole GetUserRole()
    {
        var claim = httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Role)
            ?? throw new UnauthorizedAccessException("User is not authenticated.");

        return UserRoleMapper.Map(claim.Value);
    }
}
