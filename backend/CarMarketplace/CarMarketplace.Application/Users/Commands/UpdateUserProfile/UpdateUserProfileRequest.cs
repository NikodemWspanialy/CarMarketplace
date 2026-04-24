using CarMarketplace.Application.Common.Abstractions;
using CarMarketplace.Application.Users.DTOs;

namespace CarMarketplace.Application.Users.Commands.UpdateUserProfile;

public record UpdateUserProfileRequest(
    string FirstName,
    string LastName) : ICommand<UserResponse>;
