using CarMarketplace.Application.Authorization.Helpers;
using CarMarketplace.Application.Users.Repositories;
using MediatR;

namespace CarMarketplace.Application.Authorization.Commands.ForgotPassword;

internal class ForgotPasswordHandler(
    IUserRepository userRepository,
    IPasswordResetTokenGenerator tokenGenerator,
    IEmailSender emailSender) : IRequestHandler<ForgotPasswordRequest, Unit>
{
    public async Task<Unit> Handle(ForgotPasswordRequest request, CancellationToken token)
    {
        var user = await userRepository.GetUserByEmailAsync(request.Email, token);

        // Always return success to not reveal if email exists
        if (user is null)
            return Unit.Value;

        var resetToken = await tokenGenerator.GenerateAsync(user.Id, token);
        await emailSender.SendPasswordResetEmailAsync(request.Email, resetToken, token);

        return Unit.Value;
    }
}
