using CarMarketplace.Application.Common.DTOs;
using CarMarketplace.Application.Users.DTOs;
using CarMarketplace.Application.Users.Repositories;
using MediatR;

namespace CarMarketplace.Application.Admin.Queries.GetUsers;

internal class GetUsersHandler(IUserRepository userRepository) 
    : IRequestHandler<GetUsersRequest, ListResponse<UserResponse>>
{
    public async Task<ListResponse<UserResponse>> Handle(GetUsersRequest request, CancellationToken token)
    {
        var result = await userRepository.GetPagedAsync(request.PageNumber, request.PageSize, token);
        var items = result.Users.Select(UserResponse.FromEntity).ToList();

        return new ListResponse<UserResponse>(items, result.TotalCount, request.PageNumber, request.PageSize);
    }
}
