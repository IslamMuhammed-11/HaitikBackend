<div align="center">

<h1>🚚 Haitik Backend</h1>

<p>
  <strong>Enterprise-grade logistics coordination API for government shipment operations</strong><br/>
  Built with <strong>ASP.NET Core 10</strong> · <strong>Clean Architecture</strong> · <strong>CQRS</strong> · <strong>SignalR</strong> · <strong>Hangfire</strong>
</p>

<p>
  <img src="https://img.shields.io/badge/.NET-10.0-512BD4?style=for-the-badge&logo=dotnet&logoColor=white" />
  <img src="https://img.shields.io/badge/EF_Core-10.0-512BD4?style=for-the-badge&logo=microsoft&logoColor=white" />
  <img src="https://img.shields.io/badge/SQL_Server-2022-CC2927?style=for-the-badge&logo=microsoftsqlserver&logoColor=white" />
  <img src="https://img.shields.io/badge/SignalR-Real--Time-00C4CC?style=for-the-badge" />
  <img src="https://img.shields.io/badge/Hangfire-Background_Jobs-005571?style=for-the-badge" />
  <img src="https://img.shields.io/badge/Architecture-Clean_|_CQRS-FF6B35?style=for-the-badge" />
</p>

</div>

---

## 📖 What Is This?

**Haitik** is a backend-only **logistics coordination platform** built for government shipment operations. It powers three separate clients — a government employee dashboard, a delivery admin dashboard, and a driver mobile app — from a single, unified API.

The entire system is state-machine driven. Every order passes through a strict lifecycle, and critical transitions (delivery confirmation, return confirmation) are protected by **OTP verification sent via SMS**.

> The backend is intentionally built as a **modular monolith** — clean internal boundaries, shared domain model, no microservices complexity.

---

## ✨ Feature Highlights

| Feature | Description |
|---|---|
| 🔐 **JWT + Refresh Token Auth** | Role-scoped access with token rotation |
| 📦 **Order Lifecycle Management** | Strict state machine: `Pending → ReceivedPackage → Delivering → Delivered` |
| 📤 **Bulk Order Upload** | Row-level validation with rejection reporting, processed off the request thread |
| 🧭 **Smart Driver Assignment** | Manual by admin OR automatic by geo-zone with first-accept-wins + fallback |
| 🔑 **OTP-Gated Delivery & Returns** | Hashed, expirable, attempt-limited OTP shared across delivery and return flows |
| 📍 **Live Driver Tracking** | Real-time location streaming via SignalR to admins |
| 🌍 **Public Tracking Link** | Tokenized, rate-limited read-only tracking for end customers |
| 📸 **Proof of Delivery (POD)** | Photo upload with metadata, linked to the order lifecycle |
| 🔔 **Async Notifications** | Non-blocking SMS & push, retried by background workers |
| 📊 **Admin Reporting** | Aggregated performance, revenue, and driver load reports via CQRS read models |
| 🕵️ **Full Audit Trail** | Every status change is recorded in `OrderStatusHistory` |
| ⚙️ **Hangfire Dashboard** | Visibility into all background job queues and retries |

---

## 🏛️ Architecture

Haitik is structured as a **4-layer Clean Architecture** solution with strict dependency rules — outer layers depend on inner ones, never the reverse.

```
┌─────────────────────────────────────────────────────────┐
│                    HaitikBackend.API                    │  ← Controllers, Hubs, Middleware
│              (ASP.NET Core · SignalR · Swagger)         │
├─────────────────────────────────────────────────────────┤
│               HaitikBackend.Application                 │  ← CQRS Commands/Queries, Behaviors
│       (MediatR · FluentValidation · AutoMapper)         │
├─────────────────────────────────────────────────────────┤
│               HaitikBackend.Infrastructure              │  ← EF Core, Hangfire, Services
│  (SQL Server · NetTopologySuite · BCrypt · Hangfire)    │
├─────────────────────────────────────────────────────────┤
│                  HaitikBackend.Domain                   │  ← Entities, Value Objects, Events
│           (Pure C# · Zero external dependencies)        │
└─────────────────────────────────────────────────────────┘
```

### Layer Responsibilities

| Layer | Responsibility |
|---|---|
| **Domain** | Business rules, entity behavior, domain events, value objects, state machine |
| **Application** | Use cases as CQRS commands/queries, pipeline behaviors, interfaces |
| **Infrastructure** | EF Core persistence, Hangfire jobs, OTP, file storage, assignment engine, notifications |
| **API** | HTTP routing, SignalR hubs, global exception middleware, Swagger |

---

## 🧬 Domain Model

The domain is rich and behavior-driven. Entities encapsulate their own rules — no anemic models here.

```
User ──────────────────── Driver ─────────┬─── DriverLocationPing
  │                         │             │
  └── GovernmentAgency      │             └─── OrderDriverAssignment
         │                  │
         └─── Order ────────┘
               │
               ├── OrderStatusHistory   (full audit trail)
               ├── OtpCode              (hashed, per-purpose, expirable)
               ├── DeliveryProof        (POD photo + metadata)
               ├── Return               (mirrored OTP flow)
               └── BulkUploadBatch ─── BulkUploadRejectedRow
```

### Key Domain Behaviors

**`Order` (the core aggregate)**
- Factory method `Order.Create(...)` — no public constructors
- `UpdateLocation()` blocked unless status is `Pending`
- `AssignDriver()` raises a `DriverAssignedEvent` domain event
- `ProofDelivery()` raises `DeliveryProofWasUploadedEvent`
- `RequestToReturn()` raises `ReturnRequestCreatedEvent`
- `_ChangeOrderStatus()` validates transitions via `CheckOrderTransitionsEligibility`

**`OtpCode`**
- Stored **hashed** — plaintext never persisted
- Per-purpose (`Delivery` / `Return`)
- Configurable expiry and `MaximumAttempts = 10`
- `RecordFailedAttempt()` increments counter and returns typed errors

**`Driver`**
- `PingLocation()` creates or updates a live `DriverLocationPing`
- `MaximumOrdersPerDay` configurable, validated in domain
- Online/Offline state transitions with guard clauses

---

## ⚙️ MediatR Pipeline

Every command and query flows through a 3-stage pipeline:

```
Request
   │
   ▼
LoggingBehavior      ← Logs request entry/exit
   │
   ▼
PerformanceBehavior  ← Warns if handler exceeds threshold
   │
   ▼
ValidationBehavior   ← Runs FluentValidation, fails fast on errors
   │
   ▼
Handler              ← Your actual business logic
```

---

## 🗺️ API Reference

### 🔑 Auth
```
POST   /api/auth/login
POST   /api/auth/refresh-token
POST   /api/auth/logout
```

### 📦 Orders
```
POST   /api/orders                              ← Create single order
POST   /api/orders/bulk-upload                  ← Upload file (async processing)
GET    /api/orders/bulk-upload/{batchId}/report ← Download rejection report
GET    /api/orders                              ← List orders (scoped by role)
GET    /api/orders/{id}                         ← Order detail
PUT    /api/orders/{id}/address                 ← Edit address (Pending state only)
GET    /api/orders/history                      ← Full status audit log
```

### 🧭 Assignment
```
POST   /api/orders/{id}/assign                  ← Manual admin assignment
POST   /api/orders/{id}/assignment/override     ← Override existing assignment
GET    /api/drivers/available?zone={zoneId}     ← Query eligible drivers
```

### 🚗 Driver App
```
GET    /api/driver/orders/offered               ← View offered orders
POST   /api/driver/orders/{id}/accept           ← Accept (first-accept-wins)
POST   /api/driver/orders/{id}/reject           ← Reject offer
POST   /api/driver/location                     ← Location ping
POST   /api/driver/orders/{id}/pod              ← Upload proof of delivery
POST   /api/driver/orders/{id}/delivery/request-otp
POST   /api/driver/orders/{id}/delivery/verify-otp
POST   /api/driver/orders/{id}/return/request-otp
POST   /api/driver/orders/{id}/return/verify-otp
```

### 🔄 Returns
```
POST   /api/returns/{id}/approve
POST   /api/returns/{id}/reject
```

### 📊 Reports
```
GET    /api/reports/performance
GET    /api/reports/revenue
GET    /api/reports/driver-load
```

### 🌍 Public Tracking
```
GET    /api/track/{token}                       ← Read-only, rate-limited, tokenized
```

### ⚡ SignalR Hubs
```
/hubs/driver-tracking     ← Driver sends location → admin group receives live updates
```

---

## 🔒 Security Design

| Concern | Implementation |
|---|---|
| Authentication | JWT access tokens + refresh token rotation |
| OTP storage | BCrypt-hashed, never stored in plaintext |
| Brute force protection | `AttemptCount` per OTP code with hard `MaximumAttempts` cap |
| Race conditions | `RowVersion` optimistic concurrency on `Order` entity |
| Public tracking | Short-lived tokenized URLs, rate-limited endpoint |
| HTTPS | Enforced via `UseHttpsRedirection()` |

---

## 🔧 Background Jobs (Hangfire)

All heavy or time-sensitive operations are offloaded to Hangfire, keeping HTTP responses fast:

| Job | Trigger |
|---|---|
| `AutoAssignmentCommand` | Enqueued immediately after order is created |
| `FallbackCheckCommand` | Scheduled after assignment window expires (no driver accepted) |
| SMS OTP dispatch | Enqueued on `request-otp` calls |
| Notification retries | Automatic via Hangfire retry policies |

The Hangfire dashboard is exposed at `/hangfire` for operational visibility.

---

## 🗄️ Tech Stack

| Technology | Version | Purpose |
|---|---|---|
| ASP.NET Core | 10.0 | Web API framework |
| Entity Framework Core | 10.0 | ORM |
| SQL Server | — | Primary database |
| NetTopologySuite | 2.6.0 | Geospatial queries for driver zones |
| MediatR | 14.2.0 | CQRS + domain event dispatch |
| FluentValidation | 12.1.1 | Input validation pipeline |
| AutoMapper | 16.2.0 | DTO mapping |
| Hangfire | 1.8.24 | Background job scheduling |
| SignalR | (built-in) | Real-time driver tracking |
| BCrypt.Net | 4.2.0 | OTP & password hashing |
| libphonenumber-csharp | 9.0.1 | Phone number validation |
| Swashbuckle | 10.2.3 | Swagger / OpenAPI documentation |

---

## 🚀 Getting Started

### Prerequisites
- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)
- SQL Server (local or Docker)

### 1. Clone the repo
```bash
git clone https://github.com/YOUR_USERNAME/HaitikBackend.git
cd HaitikBackend
```

### 2. Configure the connection string

Update the connection string in `appsettings.json` or `Infrastructure/DepandencyInjection.cs`:
```
Server=.;Database=HaitikDB;User Id=sa;Password=YOUR_PASSWORD;TrustServerCertificate=True;
```

### 3. Apply migrations
```bash
dotnet ef database update --project HaitikBackend.Infrastructure --startup-project HaitikBackend
```

### 4. Run the API
```bash
dotnet run --project HaitikBackend
```

### 5. Explore
- **Swagger UI** → `https://localhost:{PORT}/swagger`
- **Hangfire Dashboard** → `https://localhost:{PORT}/hangfire`

---

## 👥 User Roles

| Role | Capabilities |
|---|---|
| **Government Employee** | Create orders, view/manage orders scoped to their agency, edit address (Pending only) |
| **Delivery Admin** | Assign/override drivers, monitor live tracking, approve/reject returns, view reports |
| **Driver** | Accept/reject offers, ping location, upload POD, complete OTP delivery & return flows |
| **End Customer** | No account — receives SMS OTP, accesses public tracking link via token |

---

## 📁 Project Structure

```
HaitikBackend/
├── HaitikBackend/                    # API layer
│   ├── Controllers/                  # HTTP endpoint controllers
│   ├── Hubs/                         # SignalR hubs (DriverTrackingHub)
│   ├── Middleware/                   # Global exception handler
│   ├── Extensions/                   # Service/app extension methods
│   └── Program.cs
│
├── HaitikBackend.Application/        # Application layer
│   ├── Features/                     # CQRS Commands & Queries by feature
│   │   ├── Auth/
│   │   ├── Orders/
│   │   ├── Drivers/
│   │   ├── OrderDriverAssignments/
│   │   ├── Otp/
│   │   ├── Return/
│   │   ├── BulkUploadBatch/
│   │   ├── DeliveryProofs/
│   │   └── DriverLocationPings/
│   ├── Behaviors/                    # Logging, Performance, Validation pipelines
│   ├── Common/Interfaces/            # Port definitions (no infrastructure leaking in)
│   └── Services/
│
├── HaitikBackend.Domain/             # Domain layer (zero external dependencies)
│   ├── Entities/                     # Rich domain entities
│   ├── ValueObjects/                 # GeoLocation, Area
│   ├── Enums/                        # OrderStatus, DriverStatus, OtpPurpose, etc.
│   ├── DomainEvents/                 # Events raised by aggregates
│   ├── Errors/                       # Typed error definitions
│   └── Common/Results/               # Result<T> pattern
│
└── HaitikBackend.Infrastructure/     # Infrastructure layer
    ├── Presistence/                  # EF Core DbContext, Fluent API configs, repos
    ├── Services/                     # OTP, FileStorage, Assignment, Notification, etc.
    ├── BackgroundJobs/               # Hangfire job implementations
    └── DepandencyInjection.cs
```

---

## 🧩 Design Patterns

- **Clean Architecture** — dependency inversion at every layer boundary
- **CQRS** — commands (write) and queries (read) fully separated via MediatR
- **Domain Events** — aggregates raise events, handlers react after `SaveChangesAsync`
- **Result Pattern** — `Result<T>` for explicit error handling without exceptions
- **Repository + Unit of Work** — persistence abstracted behind interfaces
- **Factory Methods** — entities constructed only through static `Create(...)` methods
- **Pipeline Behaviors** — cross-cutting concerns (logging, validation, perf) in MediatR pipeline
- **Optimistic Concurrency** — `RowVersion` on `Order` to prevent race conditions on assignment

---

<div align="center">
  <sub>Built with ❤️ by Eslam · Powered by .NET 10</sub>
</div>
