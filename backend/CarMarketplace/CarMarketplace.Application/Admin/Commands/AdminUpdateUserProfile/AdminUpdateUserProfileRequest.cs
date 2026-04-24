using CarMarketplace.Application.Common.Abstractions;
using CarMarketplace.Application.Users.DTOs;

namespace CarMarketplace.Application.Admin.Commands.AdminUpdateUserProfile;

public record AdminUpdateUserProfileRequest(
    Guid UserId,
    string FirstName,
    string LastName) : ICommand<UserResponse>;
