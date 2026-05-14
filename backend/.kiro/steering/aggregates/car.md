---
inclusion: manual
---

# Car Aggregate

## Purpose
Represents a car offered for sale — its technical details, price, and photos.

## Aggregate Root
`Car` implements `IAggregateRoot`

### Properties
- `Id` (Guid)
- `SellerId` (Guid)
- `Brand` (string)
- `Model` (string)
- `Year` (int)
- `Price` (Money)
- `Mileage` (int)
- `FuelType` (FuelType enum)
- `Description` (string?)
- `Photos` (List\<CarPhoto\>)
- `PriceHistory` (List\<CarPriceHistory\>)
- `CreatedAt` (DateTime)
- `UpdatedAt` (DateTime?)
- `IsDeleted` (bool)

## Child Entities
- `CarPhoto` — photo attached to the car (Id, CarId, Url, IsPrimary, Order, IsDeleted)
- `CarPriceHistory` — historical price record (Id, CarId, Price, ChangedAt)

## Value Objects
- `Money` (Amount, Currency) — used for Price

## Actions
- `UpdateDetails` — change brand, model, year, mileage, fuelType, description
- `UpdatePrice` — change price, records history
- `Delete` — soft-delete the car
- `AddPhoto` — add a single photo (via factory)
- `AddPhotos` — add multiple photos in batch
- `DeletePhoto` — soft-delete a photo by ID
- `SetPrimaryPhoto` — designate one photo as thumbnail
- `UpdatePhotosOrder` — reorder photos

## Business Rules
- Soft delete via `IsDeleted` flag
- Price cannot be negative
- Price update rejected if same as current
- Cannot modify a deleted car
- Max 20 non-deleted photos per car
- At most one photo can be primary at any time
- Setting a new primary unsets the previous one
- Deleting primary does NOT auto-promote another photo
- Only the owner (SellerId) or admin can mutate (enforced by `ICarSellerGuard`)

## Authorization
- Write operations require `[Authorize]`
- `ICarSellerGuard.EnsureCanMutate(car.SellerId)` — checks current user is owner or admin
