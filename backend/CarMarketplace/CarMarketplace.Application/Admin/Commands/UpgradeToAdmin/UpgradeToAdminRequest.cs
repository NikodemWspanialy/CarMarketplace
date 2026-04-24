using CarMarketplace.Application.Common.Abstractions;
using MediatR;

namespace CarMarketplace.Application.Admin.Commands.UpgradeToAdmin;

public record UpgradeToAdminRequest(Guid UserId) : ICommand<Unit>;
