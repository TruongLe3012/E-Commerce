# E-Commerce Backend API

A modern and scalable E-Commerce Backend API built with ASP.NET Core 8 following Clean Architecture principles.

## Tech Stack

* ASP.NET Core 8 Web API
* Entity Framework Core
* SQL Server
* JWT Authentication
* AutoMapper
* FluentValidation
* xUnit
* FluentAssertions
* Swagger / OpenAPI

---

# Features

* JWT Authentication
* Role Authorization
* Product Management
* Category Management
* Shopping Cart
* Checkout System
* Order Management
* Order History
* Admin Order Management
* Image Upload
* Global Exception Middleware
* Background Services
* Caching
* Logging
* Unit Testing
* Swagger Documentation

---

# Architecture

```text
src/
│
├── Shop.API
├── Shop.Application
├── Shop.Domain
└── Shop.Infrastructure

tests/
└── Shop.Tests
```

### Clean Architecture Layers

| Layer          | Responsibility                   |
| -------------- | -------------------------------- |
| API            | Controllers, Middleware, Swagger |
| Application    | DTOs, Interfaces, Business Logic |
| Domain         | Entities                         |
| Infrastructure | Database, Services, Repository   |

---

# Authentication & Authorization

* JWT Bearer Authentication
* Role-based Authorization
* Admin / Customer roles
* Secure password hashing using BCrypt

---

# Main Modules

## Authentication

* Register
* Login
* JWT Token Generation

## Products

* Create Product
* Update Product
* Delete Product
* Get Products
* Product Image Upload

## Categories

* CRUD Categories

## Cart

* Add To Cart
* Update Quantity
* Remove Item

## Orders

* Checkout
* My Orders
* Admin Order Management
* Update Order Status

---

# Unit Testing

Implemented unit tests for:

* AuthService
* Business logic validation
* Login success
* Wrong password
* User not found

### Testing Tools

* xUnit
* Moq
* FluentAssertions
* EF Core InMemory

---

# API Documentation

Swagger UI available at:

```text
https://localhost:7010/swagger
```

---

# Screenshots

## Swagger UI

*Add Swagger screenshot here*

## SQL Tables

*Add SQL Server table screenshot here*

## Product API

*Add product API screenshot here*

## Order API

*Add order API screenshot here*

---

# Database

## Main Tables

* Users
* Products
* Categories
* CartItems
* Orders
* OrderItems

---

# Getting Started

## Clone Project

```bash
git clone https://github.com/TruongLe3012/E-Commerce.git
```

## Restore Packages

```bash
dotnet restore
```

## Update Database

```bash
dotnet ef database update
```

## Run Project

```bash
dotnet run
```

---

# Future Improvements

* Refresh Token
* Payment Integration
* Email Service
* Docker Support
* CI/CD Pipeline
* Redis Cache
* Frontend Integration

---

# Author

Developed by Trường Lê
