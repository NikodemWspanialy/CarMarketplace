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

## Child Entities
None

## Value Objects
None

## Actions
- `ChangePassword` — update password hash (validates not same as previous, not empty)
- `UpdateProfile` — change firstName, lastName
- `PromoteToAdmin` — elevate role to Admin
- `DemoteToUser` — lower role to User

## Business Rules
- New users default to `UserRole.User`
- Cannot change password to the same hash
- Cannot promote if already Admin
- Cannot demote if already User
- Email must be unique (enforced at DB level via unique index)

## Authorization
- Profile operations require authenticated user (own profile)
- Role changes (promote/demote) restricted to Admin via `[Authorize(Policy = "AdminOnly")]`
