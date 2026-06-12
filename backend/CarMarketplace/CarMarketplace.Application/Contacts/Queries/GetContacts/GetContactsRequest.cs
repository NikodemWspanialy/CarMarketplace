using CarMarketplace.Application.Common.Abstractions;
using CarMarketplace.Application.Contacts.DTOs;

namespace CarMarketplace.Application.Contacts.Queries.GetContacts;

public record GetContactsRequest : IQuery<IReadOnlyList<ContactResponse>>;
