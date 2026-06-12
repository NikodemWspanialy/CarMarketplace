using CarMarketplace.Application.Common.Interfaces;
using Microsoft.AspNetCore.Http;

namespace CarMarketplace.Infrastructure.Security;

internal class ClientInfoProvider(IHttpContextAccessor httpContextAccessor) : IClientInfoProvider
{
    public string? GetIpAddress() =>
        httpContextAccessor.HttpContext?.Connection.RemoteIpAddress?.ToString();
}