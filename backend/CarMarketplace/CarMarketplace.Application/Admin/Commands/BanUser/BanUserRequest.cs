using CarMarketplace.Application.Common.Abstractions;
using MediatR;

namespace CarMarketplace.Application.Admin.Commands.BanUser;

public record BanUserRequest(Guid UserId, string Reason, DateTime? ExpiresAt = null) : ICommand<Unit>;
