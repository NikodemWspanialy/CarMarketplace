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
- Each aggregate has a steering file in `.kiro/steering/aggregates/{name}.md` documenting actions, business rules, and authorization
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
- Pipeline Behaviors: `ValidationBehavior`, `LoggerBehavior`, `UnitOfWorkBehavior`
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
- Only Aggregate Roots get a `DbSet` in DbContext — child entities are accessed through the root's navigation properties
- Standalone entities (not aggregates, not children) may also get a `DbSet` when they need to be queried independently (e.g., `PasswordResetToken`)
- A configuration class must be defined for every entity persisted to its own table (regardless of DbSet)
- Child entity relationships defined via `HasMany` with strongly-typed navigation in the Aggregate Root's configuration
- Repositories implement interfaces from Application
- Infrastructure exceptions inherit from `InfrastructureException`
- UnitOfWork as pipeline behavior
- Security: `CurrentUserProvider`, `UserRoleMapper`, password hasher, JWT provider in `Security/`

### API
- Controllers in `Controllers/` folder
- `GlobalExceptionMiddleware` catches `DomainException` and `InfrastructureException`

### Integration Tests (`CarMarketplace.IntegrationTests`)
- Real PostgreSQL via Testcontainers — no in-memory fakes
- `WebApplicationFactory<Program>` hosts app for DI container access
- Respawn resets DB between tests (DELETE, not DROP — fast)
- Tests use `SendAsync` (MediatR dispatch) — no HTTP calls, no auth layer
- Base classes in `Common/IntegrationTestBases/`:
  - `IntegrationTestBase` — Respawn, `TestData` (DbContext), `SendAsync<T>()`, `Faker`, `SetCurrentUser()`, virtual `SeedAsync()` hook
  - `IntegrationTestBaseWithUserLogin` — overrides `SeedAsync` to register user and set current user context
  - `IntegrationTestBaseWithAdminLogin` — overrides `SeedAsync` to register + promote admin and set current user context
- `FakeCurrentUserProvider` — replaces `ICurrentUserProvider` in DI, set via `SetCurrentUser(userId, role)`
- `TestData` — `CarMarketplaceDbContext` property for read-only DB assertions
- Test files grouped by feature: `{Feature}/{Feature}Tests.cs`
- Conventions: Arrange/Act/Assert, primary constructors, one test class per command/query
- API-level tests (HTTP, auth, routing) will be a separate project
- Requires Docker running locally

### Tests.Shared (`CarMarketplace.Tests.Shared`)
- Shared test utilities referenced by all test projects
- `Builders/Builder.cs` — abstract `Builder<T>` base class with `Faker` and abstract `Build()`
- `Builders/{Entity}/Create{Entity}RequestBuilder` — fluent builders inheriting `Builder<T>` with Bogus defaults

See `naming-conventions.md` for all naming patterns (commands, queries, handlers, repositories, etc.).
