using CarMarketplace.Application.Common.Abstractions;
using CarMarketplace.Application.Users.DTOs;

namespace CarMarketplace.Application.Users.Queries.GetUserById;

public record GetUserByIdRequest(Guid Id) : IQuery<UserResponse>;