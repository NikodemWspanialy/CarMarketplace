using CarMarketplace.Application.Users.DTOs;
using CarMarketplace.Application.Users.Searchers;
using MediatR;

namespace CarMarketplace.Application.Users.Queries.GetUserById;

internal class GetUserByIdHandler(
    IUserSearcher userSearcher) : IRequestHandler<GetUserByIdRequest, UserResponse>
{
    public async Task<UserResponse> Handle(GetUserByIdRequest request, CancellationToken token)
    {
        var user = await userSearcher.FindByIdAsync(request.Id, token);

        return UserResponse.FromEntity(user);
    }
}
