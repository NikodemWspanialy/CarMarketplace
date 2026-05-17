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

## Child Entities
None

## Value Objects
None

## Actions
- `ChangePassword` — update password hash (validates not same as previous, not empty)
- `UpdateProfile` — change firstName, lastName
- `PromoteToAdmin` — elevate role to Admin
- `DemoteToUser` — lower role to User
- `Delete` — soft-delete the user (sets IsDeleted = true)

## Business Rules
- New users default to `UserRole.User`
- Soft delete via `IsDeleted` flag (global query filter excludes deleted users)
- Cannot delete if already deleted
- Cannot change password to the same hash
- Cannot promote if already Admin
- Cannot demote if already User
- Email must be unique (enforced at DB level via unique index)

## Authorization
- Profile operations require authenticated user (own profile)
- User can delete own account (`DELETE /api/user/delete-account`)
- Admin can delete any user (`DELETE /api/admin/delete-user/{id}`)
- Role changes (promote/demote) restricted to Admin via `[Authorize(Policy = "AdminOnly")]`
