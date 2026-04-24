using System.Security.Claims;
using CarMarketplace.Application.Common.Interfaces;
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
}
