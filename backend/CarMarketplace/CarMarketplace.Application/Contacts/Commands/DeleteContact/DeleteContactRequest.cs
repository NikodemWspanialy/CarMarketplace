using CarMarketplace.Application.Common.Abstractions;
using MediatR;

namespace CarMarketplace.Application.Contacts.Commands.DeleteContact;

public record DeleteContactRequest(Guid Id) : ICommand<Unit>;
