---
inclusion: manual
---

# Listing Aggregate

## Purpose
Represents a car sale listing — aggregates a car, seller info, contact details, and premium features. This is what buyers browse on the platform.

## Aggregate Root
`Listing` implements `IAggregateRoot`

### Properties
- `Id` (Guid)
- `CarId` (Guid) — reference to the Car being listed
- `SellerId` (Guid) — reference to the User who owns the listing
- `Title` (string)
- `Status` (ListingStatus enum)
- `IsFeatured` (bool)
- `FeaturedUntil` (DateTime?)
- `CreatedAt` (DateTime)
- `UpdatedAt` (DateTime?)
- `ExpiresAt` (DateTime?)
- `IsDeleted` (bool)
- `ContactIds` (List\<Guid\>) — references to Contact entities attached to this listing

## Child Entities
None — ContactIds stored as List\<Guid\> (references, not owned entities)

## Value Objects
None

## Enums
- `ListingStatus` — Active (1), Sold (2), Archived (3), Deactivated (4)

## References
- `CarId` → `Car` aggregate
- `SellerId` → `User` aggregate
- `ContactIds` → `Contact` aggregate (many)

## Actions
- `MarkAsSold` — transition status to Sold (only from Active)
- `Archive` — transition status to Archived (from Active, Sold, or Deactivated)
- `Deactivate` — transition status to Deactivated (only from Active)
- `Reactivate` — transition status back to Active (only from Deactivated)
- `Delete` — soft-delete the listing
- `AttachContact` — add a contact ID to the listing
- `DetachContact` — remove a contact ID from the listing
- `UpdateTitle` — change the listing title
- `Feature` — mark as featured with expiration date
- `RemoveFeature` — unmark as featured
- `SetExpiration` — set listing expiration date

## Business Rules
- Soft delete via `IsDeleted` flag
- Cannot delete if already deleted (`ListingAlreadyDeleted`)
- Cannot mark as sold if already sold (`ListingAlreadySold`)
- Status transitions enforced — invalid transitions throw `InvalidListingStatusTransition`:
  - Sold: only from Active
  - Archived: from Active, Sold, or Deactivated
  - Deactivated: only from Active
  - Reactivate (→ Active): only from Deactivated
- Cannot attach a contact that is already attached (`ListingContactAlreadyAttached`)
- Cannot detach a contact that is not attached (`ListingContactNotAttached`)
- Only one active listing per car (`ActiveListingAlreadyExists` — Application layer)
- Car must belong to the seller creating the listing (`CarNotOwnedBySeller` — Application layer)
- All attached contacts must belong to the seller (`ContactsNotOwnedBySeller` — Application layer)
- At least one contact is required on creation (FluentValidation)
- ContactIds must be unique (FluentValidation)
- Title max length: 200 characters (FluentValidation)
- GetListing registers a view with 24h deduplication per viewer
- RevealListingContacts records a ContactReveal per contact (skip if viewer is the seller)

## Supporting Entities (not aggregates, have own DbSet)
- `ListingView` (IEntity) — tracks listing page views (ListingId, ViewerId?, ViewedAt, IpAddress)
- `ContactReveal` (IEntity) — tracks contact reveal events (ListingId, ViewerId, ContactId, RevealedAt)

## Authorization
- Write operations require `[Authorize]`
- `IListingSellerGuard.EnsureCanMutate(listing.SellerId)` — checks current user is owner or admin
- GetListing is public (supports anonymous viewers via `GetUserIdOrNull()`)
- GetListingStats is owner/admin only (uses seller guard)
- RevealListingContacts requires authenticated user
