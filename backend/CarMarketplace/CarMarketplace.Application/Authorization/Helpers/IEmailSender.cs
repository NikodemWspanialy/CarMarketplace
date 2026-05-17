namespace CarMarketplace.Application.Authorization.Helpers;

public interface IEmailSender
{
    Task SendPasswordResetEmailAsync(string email, string resetToken, CancellationToken token = default);
}
