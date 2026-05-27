using CarMarketplace.Application.Common.Abstractions;
using MediatR;

namespace CarMarketplace.Application.Authorization.Commands.ResetPassword;

public record ResetPasswordRequest(string Token, string NewPassword) : ICommand<Unit>;
