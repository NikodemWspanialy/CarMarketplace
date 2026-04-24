using CarMarketplace.Application.Common.Abstractions;
using MediatR;

namespace CarMarketplace.Application.Users.Commands.ChangePassword;

public record ChangePasswordRequest(
    string OldPassword,
    string NewPassword) : ICommand<Unit>;
