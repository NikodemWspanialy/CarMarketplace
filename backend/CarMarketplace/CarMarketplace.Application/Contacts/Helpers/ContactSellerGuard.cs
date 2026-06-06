using CarMarketplace.Application.Common.Interfaces;
using CarMarketplace.Domain.Users;

namespace CarMarketplace.Application.Contacts.Helpers;

internal interface IContactSellerGuard
{
    void EnsureCanMutate(Guid sellerId);
}

internal class ContactSellerGuard(
    ICurrentUserProvider currentUserProvider) : IContactSellerGuard
{
    public void EnsureCanMutate(Guid sellerId)
    {
        var role = currentUserProvider.GetUserRole();
        if (role == UserRole.Admin)
            return;

        var userId = currentUserProvider.GetUserId();
        if (sellerId != userId)
            throw new UnauthorizedAccessException("You are not the owner of this contact.");
    }
}
