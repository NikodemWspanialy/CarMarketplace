using CarMarketplace.Application.Common.Interfaces;
using CarMarketplace.Application.Users.DTOs;
using CarMarketplace.Application.Users.Repositories;
using CarMarketplace.Application.Users.Searchers;
using MediatR;

namespace CarMarketplace.Application.Users.Commands.UpdateUserProfile;

internal class UpdateUserProfileHandler(
    ICurrentUserProvider currentUserProvider,
    IUserSearcher userSearcher,
    IUserRepository userRepository) : IRequestHandler<UpdateUserProfileRequest, UserResponse>
{
    public async Task<UserResponse> Handle(UpdateUserProfileRequest request, CancellationToken token)
    {
        var userId = currentUserProvider.GetUserId();
        var user = await userSearcher.FindByIdAsync(userId, token);

        user.UpdateProfile(request.FirstName, request.LastName);

        await userRepository.UpdateUserAsync(user, token);

        return UserResponse.FromEntity(user);
    }
}
