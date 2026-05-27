# E-Commerce Backend API

Production-style E-Commerce Backend API built with ASP.NET Core Web API using Clean Architecture.

---

# Features

- JWT Authentication
- Role-based Authorization
- Product CRUD
- Category Management
- Shopping Cart
- Order Checkout
- Pagination / Search / Filter
- Global Exception Middleware
- FluentValidation
- Image Upload
- Caching
- Serilog Logging
- Background Service
- Unit Testing

---

# Tech Stack

- ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- JWT Authentication
- FluentValidation
- AutoMapper
- Serilog
- xUnit
- Moq
- Docker

---

# Architecture

```bash
src/
 ├── Shop.API
 ├── Shop.Application
 ├── Shop.Domain
 └── Shop.Infrastructure
```

---

# Database Design

## Entities

- User
- Product
- Category
- CartItem
- Order
- OrderItem

## Relationships

- Category -> Products
- Order -> OrderItems
- User -> Orders

---

# API Endpoints

## Authentication

```http
POST /api/auth/register
POST /api/auth/login
```

## Products

```http
GET /api/products
GET /api/products/{id}
POST /api/products
PUT /api/products/{id}
DELETE /api/products/{id}
```

## Categories

```http
GET /api/categories
POST /api/categories
PUT /api/categories/{id}
DELETE /api/categories/{id}
```

## Cart

```http
POST /api/cart/add
PUT /api/cart/update
DELETE /api/cart/remove/{id}
```

## Orders

```http
POST /api/orders/checkout
GET /api/orders/my-orders
```

---

# Setup Instructions

## Clone repository

```bash
git clone https://github.com/TruongLe3012/E-Commerce.git
```

## Open solution

```bash
cd E-commerce_Api
```

## Update database

```bash
Update-Database
```

## Run project

```bash
dotnet run
```

---

# Docker

```bash
docker-compose up --build
```

---

# Screenshots

## Swagger UI

Add swagger screenshots here.

## SQL Server

Add database screenshots here.

---

# Future Improvements

- Refresh Token
- Redis Cache
- CQRS + MediatR
- Email Service
- Soft Delete
- Audit Logging
- CI/CD GitHub Actions

---

# Author

Truong Le