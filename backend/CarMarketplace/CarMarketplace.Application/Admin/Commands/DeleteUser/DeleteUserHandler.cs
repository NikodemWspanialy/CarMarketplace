using CarMarketplace.Application.Users.Helpers;
using MediatR;

namespace CarMarketplace.Application.Admin.Commands.DeleteUser;

internal class DeleteUserHandler(
    IUserService userService) : IRequestHandler<DeleteUserRequest, Unit>
{
    public async Task<Unit> Handle(DeleteUserRequest request, CancellationToken token)
    {
        await userService.DeleteAsync(request.UserId, token);

        return Unit.Value;
    }
}
