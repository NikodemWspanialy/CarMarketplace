using CarMarketplace.Application.Common.Abstractions;
using MediatR;

namespace CarMarketplace.Application.Users.Commands.ChangeEmail;

public record ChangeEmailRequest(string NewEmail) : ICommand<Unit>;
