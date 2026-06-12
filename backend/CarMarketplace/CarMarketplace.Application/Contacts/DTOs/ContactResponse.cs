using CarMarketplace.Domain.Contacts;

namespace CarMarketplace.Application.Contacts.DTOs;

public record ContactResponse(
    Guid Id,
    ContactType Type,
    string? PhoneNumber,
    string? CountryCode,
    string? EmailAddress,
    string? Username,
    string? Label)
{
    public static ContactResponse FromEntity(Contact contact) =>
        new(contact.Id,
            contact.Type,
            contact.Details.PhoneNumber,
            contact.Details.CountryCode,
            contact.Details.EmailAddress,
            contact.Details.Username,
            contact.Label);
}
