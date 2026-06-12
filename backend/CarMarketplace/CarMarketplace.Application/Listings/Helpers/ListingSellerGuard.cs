using CarMarketplace.Application.Common.Interfaces;
using CarMarketplace.Domain.Users;

namespace CarMarketplace.Application.Listings.Helpers;

internal interface IListingSellerGuard
{
    void EnsureCanMutate(Guid sellerId);
}

internal class ListingSellerGuard(
    ICurrentUserProvider currentUserProvider) : IListingSellerGuard
{
    public void EnsureCanMutate(Guid sellerId)
    {
        var role = currentUserProvider.GetUserRole();
        if (role == UserRole.Admin)
            return;

        var userId = currentUserProvider.GetUserId();
        if (sellerId != userId)
            throw new UnauthorizedAccessException("You are not the owner of this listing.");
    }
}
