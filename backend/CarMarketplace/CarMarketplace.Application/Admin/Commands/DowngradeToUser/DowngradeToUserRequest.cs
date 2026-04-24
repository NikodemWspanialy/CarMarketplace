using CarMarketplace.Application.Common.Abstractions;
using MediatR;

namespace CarMarketplace.Application.Admin.Commands.DowngradeToUser;

public record DowngradeToUserRequest(Guid UserId) : ICommand<Unit>;
