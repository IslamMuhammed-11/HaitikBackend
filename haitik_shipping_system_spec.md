# Haitik Shipping System — Backend Specification

**Version:** 0.1  
**Source:** SRS Summary (Backend Architecture Analysis)  
**Architecture Style:** Modular Monolith  
**Primary Stack:** ASP.NET Core Web API, EF Core, SQL Server, CQRS, MediatR, FluentValidation, JWT/Refresh Tokens, SignalR, Background Jobs

---

## 1. Purpose

Build a backend-only logistics coordination platform for government shipment operations. The system supports three active clients:

- Government employee dashboard
- Delivery admin dashboard
- Driver mobile app

The end customer does not have an application. They only interact through SMS and a public browser tracking link.

The backend must manage shipment lifecycle, assignment, driver operations, OTP-gated delivery/return confirmation, notifications, tracking, bulk upload, and reporting.

---

## 2. Product Scope

### In Scope
- Authentication and role-based access control
- Order creation and order lifecycle management
- Bulk order upload with row-level validation and rejection reporting
- Manual and automatic driver assignment
- Driver accept/reject flow
- Driver location ingestion and live tracking
- POD upload
- OTP generation, SMS dispatch, and OTP verification for delivery and return
- Notification logging and retry handling
- Public tracking via tokenized read-only endpoint
- Administrative reporting and audit history

### Out of Scope
- Consumer shipping application
- Routing engine / route optimization
- Full navigation engine
- Microservices architecture
- ERP integration
- COD payment processing
- Offline map implementation unless explicitly required later
- Any UI-specific frontend implementation beyond API support

---

## 3. System Overview

The platform is a strict state-driven shipment backend with two OTP-protected transitions:

- Delivery confirmation
- Return confirmation

The shared database and unified business logic mean all three clients rely on the same domain model and service layer. The backend should therefore be built as a modular monolith with clear internal boundaries.

The core business flow is:

1. Authenticate user
2. Create single order or import bulk orders
3. Assign driver manually or automatically
4. Driver accepts or rejects the order
5. Driver shares live location while active
6. Driver uploads proof of delivery
7. System sends OTP to customer by SMS
8. Driver verifies customer-provided OTP
9. Order reaches final delivered state
10. Returns follow the same OTP-protected pattern

---

## 4. User Roles

### 4.1 Government Employee
- Creates orders
- Views orders scoped to their organization
- Edits order address only when allowed by the current state
- Views order history and tracking data

### 4.2 Delivery Admin
- Manually assigns orders to drivers
- Oversees auto-assignment behavior
- Monitors drivers and orders in real time
- Approves or rejects returns
- Accesses reports

### 4.3 Driver
- Receives offers for available orders
- Accepts or rejects assignments
- Sends location updates
- Uploads POD photos
- Requests and verifies OTP for delivery and return flow

### 4.4 End Customer
- No account
- Receives OTP by SMS
- Can open a public tracking page using a token
- Provides OTP verbally to the driver

---

## 5. Core Domain Rules

### 5.1 Order Lifecycle
The order must follow a strict state machine. A minimum lifecycle is:

- Pending
- Received Package
- Delivering
- Delivered

If reverse flow is activated, the return flow must use the same OTP subsystem with a purpose flag.

### 5.2 Address Editing Rule
The order address may only be edited while the order is in an allowed state. This must be a domain rule, not duplicated in multiple handlers.

### 5.3 OTP Rules
- OTP must be generated per purpose
- OTP must be stored hashed
- OTP must expire
- OTP must enforce maximum verification attempts
- OTP verification must be rate-limited
- OTP must not be reusable after success or expiry

### 5.4 Assignment Rules
The assignment engine must support:
- Manual assignment by admin
- Automatic assignment to drivers in the correct geo-zone
- First-accept-wins behavior
- Timeout fallback to the driver with the fewest active orders in the zone

### 5.5 Driver Eligibility Rules
A driver must only receive offers if eligible based on:
- Availability
- Geo-zone match
- Daily order cap
- Existing active workload

### 5.6 Tracking Rules
- Driver location must be recorded while a task is active
- Admin must see live driver locations
- Customer must see read-only tracking information via token

### 5.7 Notification Rules
- SMS and push notifications must be non-blocking
- Notification sending must be retried through background processing
- All notification attempts must be logged

---

## 6. Functional Requirements

### Authentication and Access
- Users must log in using JWT access tokens and refresh tokens
- Access must be restricted by role
- Role-based authorization must be enforced on every endpoint

### Orders
- Government employees can create single orders
- Government employees can view and manage orders within their scope
- Orders must support state history tracking
- Address update must be blocked when the current state disallows it

### Bulk Upload
- The system must accept a bulk file upload for orders
- Each row must be validated independently
- Valid rows must create orders
- Invalid rows must be captured in a rejection report
- Processing should support large files without blocking the request thread

### Assignment
- Admins must be able to assign drivers manually
- The system must support automatic assignment by zone
- Auto-assignment must send offers to eligible drivers
- The first driver to accept must win the assignment
- If no driver accepts in time, fallback rules must apply

### Driver Operations
- Drivers must be able to view offered orders
- Drivers must be able to accept or reject an offer
- Drivers must send location updates periodically
- Drivers must upload POD photos

### Delivery Flow
- Driver uploads POD
- System requests OTP send to the customer by SMS
- Driver enters the customer-provided OTP
- System verifies OTP
- If OTP is valid, order transitions to delivered
- If OTP is invalid, retry must be allowed up to the configured limit

### Return Flow
- Return flow must mirror delivery OTP behavior
- Return OTP must be issued and verified using the same OTP subsystem
- Admin approval is required where the business flow demands it

### Notifications
- SMS and push messages must be triggered at key lifecycle points
- Message delivery must not block API responses
- Failures must be logged and retried

### Tracking
- Admin must be able to view live driver tracking on a map
- Public customers must be able to track order status through a tokenized endpoint

### Reporting
- Admin must be able to view aggregated reports
- Reports must support performance, revenue, driver load, and order counts
- Reports should use read models or direct queries, not the write model

---

## 7. Non-Functional Requirements

### Security
- Use HTTPS only
- Hash OTP codes before storing
- Protect OTP verification from brute force
- Use refresh token rotation
- Keep public tracking endpoints tokenized and rate-limited

### Reliability
- SMS and push operations must be retried asynchronously
- Bulk upload processing must survive partial row failure
- Auto-assignment must prevent double-accept race conditions

### Performance
- Live tracking updates should be efficient and scalable
- Reporting queries must not degrade write performance
- High-frequency location updates should not bloat the primary history tables unnecessarily

### Maintainability
- The order state machine must be centralized in the domain layer
- Assignment logic must be isolated in its own module
- OTP logic must be shared between delivery and return flows
- Notification sending must be abstracted behind interfaces

### Observability
- Audit order status changes
- Log notification provider responses
- Persist assignment offers and outcomes
- Log OTP attempts and verification results

---

## 8. Suggested Modules

1. Identity & Access
2. Orders
3. Bulk Upload
4. Assignment Engine
5. Driver Operations
6. Real-Time Tracking
7. Delivery Workflow
8. Returns Workflow
9. Notifications
10. Public Customer Tracking
11. Reporting
12. Audit / Order History

---

## 9. Data Model

### Users
Shared identity table for all authenticated users.

### Roles / UserRoles
Role-based authorization for:
- Government Employee
- Delivery Admin
- Driver

### GovernmentEntities
Represents the organization that owns the order demand.

### GovernmentEmployees
Links users to their government entity.

### DeliveryAdmins
Links users to admin identity.

### Drivers
Driver profile and operational data such as:
- current status
- daily order limit
- last known latitude and longitude

### Orders
Core shipment record containing:
- current status
- government entity owner
- assigned driver
- geo-zone
- timestamps for each state transition

### OrderStatusHistory
Audit trail of every status transition.

### BulkUploadBatches
Metadata for each bulk upload job.

### BulkUploadRejectedRows
Stores rejected row data and validation reasons.

### GeoZones
Defines assignment boundaries.

### AssignmentOffers
Tracks which drivers were offered an order and their responses.

### OtpCodes
Stores hashed OTP values, expiry, attempt count, and purpose.

### ProofOfDelivery
Stores POD photo reference and metadata.

### Returns
Stores return lifecycle metadata.

### NotificationLog
Stores SMS and push attempts, provider response, and retry count.

### TrackingTokens
Stores public tracking token and expiry.

### RefreshTokens
Supports JWT refresh flow.

---

## 10. API Surface

### Auth
- `POST /api/auth/login`
- `POST /api/auth/refresh-token`
- `POST /api/auth/logout`

### Orders
- `POST /api/orders`
- `POST /api/orders/bulk-upload`
- `GET /api/orders/bulk-upload/{batchId}/report`
- `GET /api/orders`
- `GET /api/orders/{id}`
- `PUT /api/orders/{id}/address`
- `GET /api/orders/history`

### Assignment
- `POST /api/orders/{id}/assign`
- `POST /api/orders/{id}/assignment/override`
- `GET /api/drivers/available?zone={zoneId}`

### Driver App
- `GET /api/driver/orders/offered`
- `POST /api/driver/orders/{id}/accept`
- `POST /api/driver/orders/{id}/reject`
- `POST /api/driver/location`
- `POST /api/driver/orders/{id}/pod`
- `POST /api/driver/orders/{id}/delivery/request-otp`
- `POST /api/driver/orders/{id}/delivery/verify-otp`
- `POST /api/driver/orders/{id}/return/request-otp`
- `POST /api/driver/orders/{id}/return/verify-otp`

### Returns
- `POST /api/returns/{id}/approve`
- `POST /api/returns/{id}/reject`

### Reports
- `GET /api/reports/performance`
- `GET /api/reports/revenue`
- `GET /api/reports/driver-load`

### Public Tracking
- `GET /api/track/{token}`

### SignalR Hubs
- `/hubs/tracking`
- `/hubs/customer-tracking`

---

## 11. Error Handling Rules

- Invalid input must return structured validation errors
- Expired OTP must return a clear failure reason
- Wrong OTP attempts must increment attempt count
- Assignment race conflicts must return an already-taken response
- Unauthorized access must return 401 or 403 as appropriate
- Bulk upload row failures must not fail the whole batch
- Notification failures must be logged and retried

---

## 12. Background Processing Requirements

The system must use background jobs or hosted services for:
- bulk upload processing
- SMS retry
- push retry
- assignment timeout fallback

These operations must not block the HTTP request thread.

---

## 13. Security and Abuse Prevention

- OTP verification must have brute-force protection
- Public tracking must be rate-limited
- Refresh tokens must be rotated
- Sensitive provider responses should be logged safely
- Customer-facing links must be tokenized and expirable

---

## 14. Acceptance Criteria

The implementation is acceptable when:
- A user can authenticate by role
- Orders can be created and tracked
- Bulk upload processes row-by-row
- Manual and automatic assignment work
- Driver accept/reject is race-safe
- Driver location updates appear in real time
- Delivery OTP flow works end to end
- Return OTP flow works end to end
- Notification sending is asynchronous
- Audit history is recorded
- Reporting endpoints return correct aggregates

---

## 15. Open Questions

The following must be confirmed before final implementation:
- What happens when OTP SMS sending fails?
- Are offline maps needed for drivers?
- What is the exact bulk upload file format and row limit?
- What is the default assignment acceptance window?
- Is COD truly out of scope?
- Is ERP integration truly out of scope?
- Is there one government entity or many?
- How is geo-zone defined: polygon, radius, or administrative district?
- What is the retention policy for OTP logs and POD photos?

---

## 16. Implementation Notes

- Prefer a modular monolith over microservices
- Keep business rules inside the domain model where possible
- Use CQRS for reporting and read-heavy queries
- Use a shared OTP subsystem with a purpose flag
- Keep notification providers behind abstraction
- Make assignment transactional to avoid double-accept issues

---

## 17. Definition of Done

A feature is done only when:
- behavior matches the spec
- validation is in place
- errors are handled cleanly
- logs/audit entries are created where needed
- tests cover the critical paths
- build passes
- no unrelated refactors were introduced

