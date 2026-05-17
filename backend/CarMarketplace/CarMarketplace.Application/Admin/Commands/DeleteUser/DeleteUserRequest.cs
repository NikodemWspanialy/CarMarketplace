using CarMarketplace.Application.Common.Abstractions;
using MediatR;

namespace CarMarketplace.Application.Admin.Commands.DeleteUser;

public record DeleteUserRequest(Guid UserId) : ICommand<Unit>;
