using CarMarketplace.Application.Common.Interfaces;

namespace CarMarketplace.IntegrationTests.Common;

public class FakeClientInfoProvider : IClientInfoProvider
{
    public string? IpAddress { get; set; } = "127.0.0.1";

    public string? GetIpAddress() => IpAddress;
}