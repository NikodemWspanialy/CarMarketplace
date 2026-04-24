---
inclusion: always
---

# CarMarketplace Project Architecture

## Architectural Style
- Clean Architecture + DDD + CQRS
- Modular Monolith — single deployment, logical modules

## Layers (from inside out)

### Domain (no external dependencies)
- Entities for example: `Car` (Aggregate Root), `CarPhoto`, `CarPriceHistory`, `User`
- Aggregate Roots implement `IAggregateRoot` interface
- Value Objects (for example: `Money`)
- Enums for example: `FuelType`, `UserRole`
- Domain exceptions inherit from `DomainException`
- Soft delete via `IsDeleted` flag
- File structure: `Domain/{EntityNamePlural}/{EntityName}.cs`
  - Example: `Domain/Cars/Car.cs`, `Domain/Users/User.cs`

### Application
- NEVER return Domain entities from handlers — always map to a `{EntityName}Response` DTO defined in Application layer
- Commands and Queries (CQRS) — `ICommand<T>` → write operations, `IQuery<T>` → read operations
- Exceptions inherit from `DomainException`
- Pipeline Behaviors: `LoggerBehavior`, `UnitOfWorkBehavior`
- File structure: `Application/{EntityNamePlural}/`
  - `Commands/` — write operations (create, update, delete)
  - `Queries/` — read operations
  - `DTOs/` — data transfer objects
  - `Exceptions/` — application-level exceptions
  - `Helpers/` — seller guards, utility interfaces
  - `Searchers/` — entity searchers (find by id or throw)
  - `Validators/` — FluentValidation validators
  - `Factories/` — entity and value object factories
  - `Repositories/` — repository interfaces
  - Not all subdirectories are required — create only what's needed
- Shared code in `Application/Common/`:
  - `Abstractions/` — `ICommand`, `IQuery` interfaces
  - `Behaviors/` — `LoggerBehavior`, `UnitOfWorkBehavior`
  - `Interfaces/` — `IUnitOfWork`, `ICurrentUserProvider`
- Admin operations in `Application/Admin/` — commands restricted to admin role

### Infrastructure
- Fluent API configurations in `Persistence/Configurations/`
- `OwnsOne` for Value Objects
- A configuration class must be defined for every entity that has a DbSet in DbContext
- Repositories implement interfaces from Application
- Infrastructure exceptions inherit from `InfrastructureException`
- UnitOfWork as pipeline behavior
- Security: `CurrentUserProvider`, `UserRoleMapper`, password hasher, JWT provider in `Security/`

### API
- Controllers in `Controllers/` folder
- `GlobalExceptionMiddleware` catches `DomainException` and `InfrastructureException`

See `naming-conventions.md` for all naming patterns (commands, queries, handlers, repositories, etc.).
