using CarMarketplace.Application.Common.Abstractions;
using MediatR;

namespace CarMarketplace.Application.Authorization.Commands.ForgotPassword;

public record ForgotPasswordRequest(string Email) : ICommand<Unit>;
