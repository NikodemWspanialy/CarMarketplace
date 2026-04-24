using CarMarketplace.Application.Users.DTOs;
using CarMarketplace.Application.Users.Repositories;
using CarMarketplace.Application.Users.Searchers;
using MediatR;

namespace CarMarketplace.Application.Admin.Commands.AdminUpdateUserProfile;

internal class AdminUpdateUserProfileHandler(
    IUserSearcher userSearcher,
    IUserRepository userRepository) : IRequestHandler<AdminUpdateUserProfileRequest, UserResponse>
{
    public async Task<UserResponse> Handle(AdminUpdateUserProfileRequest request, CancellationToken token)
    {
        var user = await userSearcher.FindByIdAsync(request.UserId, token);

        user.UpdateProfile(request.FirstName, request.LastName);

        await userRepository.UpdateUserAsync(user, token);

        return UserResponse.FromEntity(user);
    }
}