using CarMarketplace.Application.Common.Abstractions;
using CarMarketplace.Application.Users.DTOs;

namespace CarMarketplace.Application.Users.Queries.GetUserProfile;

public record GetUserProfileRequest : IQuery<UserResponse>;