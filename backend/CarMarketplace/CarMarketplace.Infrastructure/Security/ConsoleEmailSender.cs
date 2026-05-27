using CarMarketplace.Application.Authorization.Helpers;
using Microsoft.Extensions.Logging;

namespace CarMarketplace.Infrastructure.Security;

internal class ConsoleEmailSender(ILogger<ConsoleEmailSender> logger) : IEmailSender
{
    public Task SendPasswordResetEmailAsync(string email, string resetToken, CancellationToken token = default)
    {
        logger.LogInformation("[EMAIL] Password reset for {Email}: {Token}", email, resetToken);

        return Task.CompletedTask;
    }
}
