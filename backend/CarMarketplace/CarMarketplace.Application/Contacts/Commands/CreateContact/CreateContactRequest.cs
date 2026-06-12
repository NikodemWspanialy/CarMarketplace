using CarMarketplace.Application.Common.Abstractions;
using CarMarketplace.Domain.Contacts;

namespace CarMarketplace.Application.Contacts.Commands.CreateContact;

public record CreateContactRequest(
    ContactType Type,
    string? PhoneNumber,
    string? CountryCode,
    string? EmailAddress,
    string? Username,
    string? Label) : ICommand<Guid>;
