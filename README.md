# 🚗 CarMarketplace API

![.NET](https://img.shields.io/badge/.NET-10-512BD4?logo=dotnet&logoColor=white)
![C#](https://img.shields.io/badge/C%23-13-239120?logo=csharp&logoColor=white)
![ASP.NET Core](https://img.shields.io/badge/ASP.NET_Core-Web_API-5C2D91?logo=dotnet&logoColor=white)
![PostgreSQL](https://img.shields.io/badge/PostgreSQL-16-4169E1?logo=postgresql&logoColor=white)
![DDD](https://img.shields.io/badge/DDD-Domain_Driven_Design-orange)
![CQRS](https://img.shields.io/badge/CQRS-MediatR-red)
![Docker](https://img.shields.io/badge/Docker-Testcontainers-2496ED?logo=docker&logoColor=white)
![Kiro](https://img.shields.io/badge/AI-Kiro-7C3AED)

## About the Project

A portfolio backend project showcasing modern .NET development practices - Clean Architecture, DDD, and CQRS applied to a car marketplace domain.

Sellers can publish car offers with photos, pricing history, and contact details. Buyers browse listings, reveal seller contacts, and view listing statistics. Admins moderate users, manage bans, and feature premium listings.

### ✨ Key Features

- Custom authentication (JWT, BCrypt) without ASP.NET Identity, with roles (User/Admin), password reset, and account management
- Creating and editing car sale listings with photos, price history, and featuring mechanism
- Browsing, filtering, and pagination of active offers with full lifecycle (active/sold/deactivated/archived)
- Seller contact management as a separate domain (CRUD, attachment to listings)
- Contact reveal system - interested buyers can reveal seller's contact details
- Tracking who viewed a listing and who revealed seller's contact information
- Banning and unbanning users with reason, duration, and full ban history audit trail
- Admin panel for user moderation (deletion, role changes, profile editing) and listing featuring

## 🛠 Tech Stack

| Category | Details |
|----------|---------|
| Runtime & Framework | .NET 10, C# 13, ASP.NET Core Web API |
| Architecture | Clean Architecture, DDD, CQRS, Modular Monolith |
| Database | PostgreSQL + Entity Framework Core 10 (Npgsql), Fluent API only |
| Security | JWT Bearer (HMAC-SHA256), BCrypt.Net-Next, custom implementation (no Identity) |
| API | Swagger/OpenAPI (Swashbuckle), Rate Limiting, CORS, GlobalExceptionMiddleware |
| Infrastructure | Docker Compose, Linux containers |

## 🏛 Patterns & Practices

| Pattern | Usage |
|---------|-------|
| MediatR | Command/query dispatching + pipeline behaviors |
| FluentValidation | Input validation |
| Repository Pattern | Data access abstraction |
| Unit of Work | Transaction management |
| Aggregate Root | Transactional boundary in the domain |
| Value Objects | Domain primitives (e.g. Money) |
| Soft Delete | IsDeleted flag with explicit filtering |
| Ownership Guard | Resource ownership validation |
| Factory Pattern | Entity and value object creation |
| Domain Validators | Business rules requiring database access |

## 🧪 Testing Stack

| Tool | Role |
|------|------|
| xUnit | Test framework |
| WebApplicationFactory | In-process API hosting |
| Testcontainers | PostgreSQL in Docker per test run |
| Respawn | Fast DB reset between tests |
| FluentAssertions | Readable assertions |
| Bogus | Test data generation |

Two test layers:
- Integration tests via MediatR dispatch
- API tests via HTTP

## 🏗 Architecture

Clean Architecture + Domain-Driven Design + CQRS pattern, organized as a Modular Monolith.

```text
CarMarketplace.Domain
├── Aggregates
├── Entities
├── Value Objects
├── Enums
├── Domain Exceptions
└── IAggregateRoot

CarMarketplace.Application
├── Commands / Queries
├── Handlers
├── DTOs
├── FluentValidation
├── Factories
├── Searchers
├── Guards
├── Domain Validators
└── Pipeline Behaviors

CarMarketplace.Infrastructure
├── EF Core (Fluent API)
├── Repositories
├── JWT Provider
├── Password Hasher
├── Email Service
└── UnitOfWork

CarMarketplace.API
├── Controllers
├── GlobalExceptionMiddleware
├── Rate Limiting
├── CORS
├── Auth Policies
└── Swagger

CarMarketplace.IntegrationTests
CarMarketplace.API.tests
CarMarketplace.Tests.Shared
```

### Project References

```text
Domain (no dependencies)
  ↑
Application → Domain
  ↑
Infrastructure → Application, Domain
  ↑
API → Application, Infrastructure
```

Test projects reference only what they need:
- `IntegrationTests` → API, Infrastructure, Tests.Shared
- `API.tests` → API, Infrastructure
- `Tests.Shared` → Application

## 🎯 Design Decisions

### 🔐 Custom Authentication

Full control over users, JWT tokens, and password hashing (BCrypt). User remains a domain aggregate instead of a framework entity.

### 🔄 Pipeline Behaviors

Validation, logging, and UnitOfWork are implemented as MediatR middleware. Handlers contain only business logic.

### 🌐 GlobalExceptionMiddleware

Centralized mapping of domain and infrastructure exceptions into standardized HTTP responses. No try-catch blocks in controllers.

### 🧩 Aggregate Roots

Aggregates encapsulate business rules and consistency boundaries. Child entities are modified only through aggregate roots.

### ✅ Two-Level Validation

- FluentValidation → input validation
- Domain Validators → business rules requiring database access

### 🐘 Real Database Testing

Testcontainers spins up PostgreSQL containers and Respawn resets data between tests. No in-memory database providers.

### 🤖 AI & Agent Hooks & Steering Files

Kiro AI assisted with maintaining conventions, documentation sync, and code scaffolding through steering files and automated hooks — helping maintain consistency across the codebase while the architecture and business logic were designed and implemented manually.
Conventions and architectural documentation stored in `.kiro/steering/` remain synchronized with the codebase and act as living documentation.

## 📋 Prerequisites

- .NET 10 SDK
- Docker (required for PostgreSQL and tests)

## 🚀 Getting Started

```bash
# Start PostgreSQL
docker run -d --name car-postgres \
  -e POSTGRES_USER=postgres \
  -e POSTGRES_PASSWORD=postgres \
  -e POSTGRES_DB=car \
  -p 5432:5432 postgres:16-alpine

# Restore dependencies
dotnet restore

# Apply migrations
cd CarMarketplace.API
dotnet ef database update

# Run the API
dotnet run
```

## 🧪 Running Tests

Docker must be running (Testcontainers spins up PostgreSQL automatically).

```bash
# All tests
dotnet test

# Integration tests only
dotnet test CarMarketplace.IntegrationTests

# API tests only
dotnet test CarMarketplace.API.tests
```

## 📡 API Overview

| Area | Endpoints | Auth |
|------|-----------|------|
| Auth | register, login, forgot/reset password, refresh, logout | Public (rate-limited) |
| Cars | CRUD + photos (batch upload, ordering, primary) + price update | Owner or Admin |
| Listings | Create, status transitions, contact attach/detach, reveal, stats | Owner or Admin |
| Contacts | CRUD, attachment to listings | Owner |
| User | Profile, password, email, delete account | Authenticated |
| Admin | User management, ban/unban, promote/demote, feature listings | Admin only |

Full endpoint list available in Swagger at `/swagger`.
