using CarMarketplace.Application.Common.Interfaces;
using CarMarketplace.Domain.Users;

namespace CarMarketplace.Application.Cars.Helpers;

internal interface ICarSellerGuard
{
    void EnsureCanMutate(Guid sellerId);
}

internal class CarSellerGuard(
    ICurrentUserProvider currentUserProvider) : ICarSellerGuard
{
    public void EnsureCanMutate(Guid sellerId)
    {
        var role = currentUserProvider.GetUserRole();
        if (role == UserRole.Admin)
            return;

        var userId = currentUserProvider.GetUserId();
        if (sellerId != userId)
            throw new UnauthorizedAccessException("You are not the owner of this car.");
    }
}