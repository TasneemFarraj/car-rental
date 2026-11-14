# Car Rental — Backend API

REST API for a car rental website: browsing and filtering cars, booking a reservation, checkout, and account management with email verification.

## Features

- **Accounts:** registration, login with JWT, passwords hashed with **BCrypt**.
- **Email flows (MailKit / SMTP):** verification code on sign-up, password reset, account management emails, and payment confirmation.
- **Cars:** car catalog with details and **filtering** (type, city, price per day).
- **Reservations:** book a car for a date range and follow the reservation.
- **Checkout:** payment details step with a confirmation email.
- **Website content:** Home, About, Why Us, Call-to-Action, Testimonials, and Contact sections managed through the API.
- **Image uploads** served as static files.

## Tech Stack

| Layer | Technology |
|---|---|
| Framework | ASP.NET Core Web API (.NET 8) |
| Database | SQL Server, Entity Framework Core 8 (code-first migrations) |
| Auth | JWT Bearer, BCrypt password hashing |
| Email | MailKit / MimeKit |
| Mapping & validation | AutoMapper, FluentValidation |
| Docs | Swagger / OpenAPI |

## Architecture

Layered structure using the **Repository** and **Unit of Work** patterns:

```
CarRentalPro/
├── Controllers/    # One controller per resource
├── Repository/     # Data access (interfaces + EF Core implementations)
├── UnitOfWork/     # Single entry point to all repositories
├── Services/       # Email services
├── Data/Models/    # Entities + DbContext
├── Dtos/           # Request / response models
├── AutoMapper/     # Entity <-> DTO mappings
├── Authorization/  # JWT generation
└── Migrations/     # EF Core migrations
```

## Getting Started

Requirements: [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) and SQL Server.

```bash
git clone https://github.com/TasneemFarraj/car-rental.git
cd car-rental/CarRentalPro

# Secrets are not stored in the repo. Set them locally:
dotnet user-secrets init
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Server=localhost;Database=CarRental;Trusted_Connection=True;TrustServerCertificate=True"
dotnet user-secrets set "Jwt:Key" "<random string, 32+ characters>"
dotnet user-secrets set "EmailSettings:Username" "<smtp user>"
dotnet user-secrets set "EmailSettings:Password" "<smtp app password>"

dotnet ef database update
dotnet run
```

Then open Swagger at `https://localhost:5001/swagger`.

## Note on Payments

This project is a **demo**. The checkout step saves card details to the database to simulate a payment flow; it is not connected to a real payment provider and must not be used with real cards.

## Roadmap

- [ ] Replace the simulated checkout with a payment gateway (e.g. Stripe) so card data never touches our database
- [ ] Role-based authorization for admin endpoints
- [ ] Global exception handling and consistent HTTP status codes
- [ ] Unit tests

## Author

**Tasneem Farraj** — [GitHub](https://github.com/TasneemFarraj)
