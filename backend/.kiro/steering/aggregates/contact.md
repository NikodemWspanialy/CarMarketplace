---
inclusion: manual
---

# Contact Aggregate

## Purpose
Represents seller's contact details — managed independently from listings. A seller creates contacts once and attaches them to multiple listings by reference (Id).

## Aggregate Root
`Contact` implements `IAggregateRoot`

### Properties
- `Id` (Guid)
- `SellerId` (Guid) — reference to the User who owns the contact
- `Type` (ContactType enum)
- `Details` (ContactDetails value object)
- `Label` (string?) — optional user-defined label
- `IsDeleted` (bool)
- `CreatedAt` (DateTime)
- `UpdatedAt` (DateTime?)

## Child Entities
None

## Value Objects
- `ContactDetails` (PhoneNumber?, CountryCode?, EmailAddress?, Username?) — conditional fields based on type

## Enums
- `ContactType` — Phone (1), Email (2), WhatsApp (3), Telegram (4), Other (5)

## References
- `SellerId` → `User` aggregate

## Actions
- `Create` (constructor) — create contact with type-specific validation
- `Update` — change type, details, and label (validates not deleted, then validates details)
- `Delete` — soft-delete the contact

## Business Rules
- Soft delete via `IsDeleted` flag
- Cannot update or delete if already deleted (`ContactAlreadyDeleted`)
- Type-specific details validation (domain level):
  - Phone / WhatsApp: `PhoneNumber` required
  - Email: `EmailAddress` required
  - Telegram: `Username` required
  - Other: no mandatory fields
- Max 5 contacts per seller (`ContactLimitReached` — Application layer, validated before creation)
- Validation done via `ICreateContactValidator.ValidateContactLimitAsync()`

## Authorization
- Write operations require `[Authorize]`
- `IContactSellerGuard.EnsureCanMutate(contact.SellerId)` — checks current user is owner or admin
- GetContacts returns contacts for the authenticated seller
