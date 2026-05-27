using CarMarketplace.Application.Common.Abstractions;
using CarMarketplace.Application.Common.DTOs;
using CarMarketplace.Application.Users.DTOs;

namespace CarMarketplace.Application.Admin.Queries.GetUsers;

public record GetUsersRequest(int PageNumber, int PageSize) : IQuery<ListResponse<UserResponse>>;
