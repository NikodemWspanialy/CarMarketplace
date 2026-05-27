using CarMarketplace.Application.Common.Abstractions;
using MediatR;

namespace CarMarketplace.Application.Admin.Commands.UnbanUser;

public record UnbanUserRequest(Guid UserId, string? Reason = null) : ICommand<Unit>;
