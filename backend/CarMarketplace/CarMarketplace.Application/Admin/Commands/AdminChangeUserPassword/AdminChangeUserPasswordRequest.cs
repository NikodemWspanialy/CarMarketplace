using CarMarketplace.Application.Common.Abstractions;
using MediatR;

namespace CarMarketplace.Application.Admin.Commands.AdminChangeUserPassword;

public record AdminChangeUserPasswordRequest(
    Guid UserId,
    string NewPassword) : ICommand<Unit>;
