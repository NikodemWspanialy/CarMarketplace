namespace CarMarketplace.Application.Common.Interfaces;

public interface ICurrentUserProvider
{
    Guid GetUserId();
}
