---
inclusion: manual
---

# User Aggregate

## Purpose
Represents a platform user account with authentication and role management.

## Aggregate Root
`User` implements `IAggregateRoot`

### Properties
- `Id` (Guid)
- `Email` (string)
- `PasswordHash` (string)
- `FirstName` (string)
- `LastName` (string)
- `Role` (UserRole enum)
- `CreatedAt` (DateTime)
- `IsDeleted` (bool)
- `ActiveBan` (ActiveBan? value object)
- `BanHistory` (List\<BanRecord\>)
- `IsBanned` (computed, not persisted)

## Child Entities
- `BanRecord` — ban history entry (Id, UserId, BannedByAdminId, Reason, BannedAt, ExpiresAt?, UnbannedAt?, UnbannedByAdminId?, UnbanReason?)

## Value Objects
- `ActiveBan` (Reason, BannedAt, ExpiresAt?) — current ban state, null if not banned

## Actions
- `ChangePassword` — update password hash (validates not same as previous, not empty)
- `ChangeEmail` — update email (validates not same as current)
- `UpdateProfile` — change firstName, lastName
- `PromoteToAdmin` — elevate role to Admin
- `DemoteToUser` — lower role to User
- `Delete` — soft-delete the user (sets IsDeleted = true)
- `Ban(reason, expiresAt?)` — set ActiveBan + add BanRecord
- `Unban(reason?)` — clear ActiveBan, mark BanRecord as unbanned

## Business Rules
- New users default to `UserRole.User`
- Soft delete via `IsDeleted` flag (global query filter excludes deleted users)
- Cannot delete if already deleted
- Cannot change password to the same hash
- Cannot promote if already Admin
- Cannot demote if already User
- Cannot ban if already banned (non-expired)
- Cannot unban if not banned
- Ban with null ExpiresAt is permanent
- `IsBanned` = ActiveBan is not null and not expired
- Email must be unique (enforced at DB level via unique index)

## Authorization
- Profile operations require authenticated user (own profile)
- User can delete own account (`DELETE /api/user/delete-account`)
- Admin can delete any user (`DELETE /api/admin/delete-user/{id}`)
- Role changes (promote/demote) restricted to Admin via `[Authorize(Policy = "AdminOnly")]`
