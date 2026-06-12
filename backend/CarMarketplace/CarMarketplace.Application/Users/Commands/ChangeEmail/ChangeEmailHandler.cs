using CarMarketplace.Application.Common.Interfaces;
using CarMarketplace.Application.Users.Repositories;
using CarMarketplace.Application.Users.Searchers;
using CarMarketplace.Application.Users.Validators;
using MediatR;

namespace CarMarketplace.Application.Users.Commands.ChangeEmail;

internal class ChangeEmailHandler(
    ICurrentUserProvider currentUserProvider,
    IUserSearcher userSearcher,
    IChangeEmailValidator changeEmailValidator,
    IUserRepository userRepository) : IRequestHandler<ChangeEmailRequest, Unit>
{
    public async Task<Unit> Handle(ChangeEmailRequest request, CancellationToken token)
    {
        var userId = currentUserProvider.GetUserId();

        await changeEmailValidator.ValidateEmailNotTaken(request.NewEmail, token);

        var user = await userSearcher.FindByIdAsync(userId, token);
        
        user.ChangeEmail(request.NewEmail);
        await userRepository.UpdateUserAsync(user, token);

        return Unit.Value;
    }
}
