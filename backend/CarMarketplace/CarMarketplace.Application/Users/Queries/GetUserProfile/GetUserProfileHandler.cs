using CarMarketplace.Application.Common.Interfaces;
using CarMarketplace.Application.Users.DTOs;
using CarMarketplace.Application.Users.Searchers;
using MediatR;

namespace CarMarketplace.Application.Users.Queries.GetUserProfile;

internal class GetUserProfileHandler(
    ICurrentUserProvider currentUserProvider,
    IUserSearcher userSearcher) : IRequestHandler<GetUserProfileRequest, UserResponse>
{
    public async Task<UserResponse> Handle(GetUserProfileRequest request, CancellationToken token)
    {
        var userId = currentUserProvider.GetUserId();
        var user = await userSearcher.FindByIdAsync(userId, token);

        return UserResponse.FromEntity(user);
    }
}
