---
inclusion: always
---

# Integration Tests

## Project
- `CarMarketplace.IntegrationTests` — references API and Infrastructure
- Namespace: `CarMarketplace.IntegrationTests`

## Stack
- xUnit 2.9 — test framework
- Microsoft.AspNetCore.Mvc.Testing — `WebApplicationFactory<Program>` for in-process API hosting
- Testcontainers.PostgreSql — real PostgreSQL container per test class
- Respawn — fast database reset between tests
- FluentAssertions — readable assertions
- Bogus — test data generation

## Architecture
- Tests send real HTTP requests through the full pipeline (controller → MediatR → handler → EF → PostgreSQL)
- No in-memory database fakes — always real PostgreSQL via Docker
- External services (e.g. `IEmailSender`) are mocked in the factory

## File Structure
```
IntegrationTests/
├── Common/
│   ├── IntegrationTestBases/
│   │   ├── IntegrationTestBase.cs
│   │   ├── IntegrationTestBaseWithUserLogin.cs
│   │   └── IntegrationTestBaseWithAdminLogin.cs
│   ├── CarMarketplaceApiFactory.cs
│   └── JwtTokenGenerator.cs
└── {Feature}/
    └── {Feature}Tests.cs
```

## Key Classes

### CarMarketplaceApiFactory
- Inherits `WebApplicationFactory<Program>`, implements `IAsyncLifetime`
- Starts PostgreSQL container, applies migrations on `InitializeAsync`
- Swaps `DbContextOptions<CarMarketplaceDbContext>` to point at container
- Shared per test class via `IClassFixture<CarMarketplaceApiFactory>`

### IntegrationTestBase
- Abstract base class for all integration tests
- Implements `IClassFixture<CarMarketplaceApiFactory>` + `IAsyncLifetime`
- Resets database via Respawn before each test
- Provides `HttpClient`, `CreateDbContext()`, `Authenticate()`, `AuthenticateAsAdmin()`
- Provides `SendAsync<TResponse>(IRequest<TResponse>)` and `SendAsync(IRequest)` — sends commands/queries directly via MediatR (bypasses controller/middleware)

### IntegrationTestBaseWithUserLogin
- Extends `IntegrationTestBase`
- Registers a user and authenticates as User role on `InitializeAsync`
- Provides `UserId` (Guid) and `UserEmail` (string)

### IntegrationTestBaseWithAdminLogin
- Extends `IntegrationTestBase`
- Registers a user, promotes to Admin, and authenticates as Admin role on `InitializeAsync`
- Provides `AdminId` (Guid) and `AdminEmail` (string)

### JwtTokenGenerator
- Static helper generating tokens matching `appsettings.json` JWT config
- Used by `Authenticate()` / `AuthenticateAsAdmin()` in base class

## Conventions
- One test class per feature/endpoint group
- Test method naming: `{Action}_With{Condition}_Returns{Expected}` or `Should_{Behavior}`
- Always use Arrange/Act/Assert pattern
- Use `Client` (HttpClient) for full pipeline tests (controller → handler → DB)
- Use `SendAsync(command)` for direct MediatR dispatch (bypasses HTTP layer) — useful for data setup or testing business logic in isolation
- Use `CreateDbContext()` only for assertions (verifying DB state), not for setup via EF
- Setup data through API calls, `SendAsync`, or direct DB inserts when API path is too complex
- Primary constructor for injecting `CarMarketplaceApiFactory`

## Running Tests
- Requires Docker running locally
- `dotnet test CarMarketplace.IntegrationTests`
