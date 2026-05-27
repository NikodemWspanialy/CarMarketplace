using CarMarketplace.Application.Common.Abstractions;
using MediatR;

namespace CarMarketplace.Application.Users.Commands.DeleteAccount;

public record DeleteAccountRequest : ICommand<Unit>;
