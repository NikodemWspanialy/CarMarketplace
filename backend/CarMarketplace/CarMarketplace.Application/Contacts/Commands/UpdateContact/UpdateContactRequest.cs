using CarMarketplace.Application.Common.Abstractions;
using CarMarketplace.Application.Contacts.DTOs;
using CarMarketplace.Domain.Contacts;

namespace CarMarketplace.Application.Contacts.Commands.UpdateContact;

public record UpdateContactRequest(
    Guid Id,
    ContactType Type,
    string? PhoneNumber,
    string? CountryCode,
    string? EmailAddress,
    string? Username,
    string? Label) : ICommand<ContactResponse>;
