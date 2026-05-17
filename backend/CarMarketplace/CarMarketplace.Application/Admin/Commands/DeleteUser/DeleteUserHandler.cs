using CarMarketplace.Application.Users.Repositories;
using CarMarketplace.Application.Users.Searchers;
using MediatR;

namespace CarMarketplace.Application.Admin.Commands.DeleteUser;

internal class DeleteUserHandler(
    IUserSearcher userSearcher,
    IUserRepository userRepository) : IRequestHandler<DeleteUserRequest, Unit>
{
    public async Task<Unit> Handle(DeleteUserRequest request, CancellationToken token)
    {
        var user = await userSearcher.FindByIdAsync(request.UserId, token);

        user.Delete();
        await userRepository.UpdateUserAsync(user, token);

        return Unit.Value;
    }
}
