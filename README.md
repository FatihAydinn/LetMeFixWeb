# Let Me Fix

This project is a **web application** built with **ASP.NET Core 8** and follows the **Onion Architecture**.  
The main purpose is to provide a platform where users can **request and offer services** such as education, repair, cleaning, transportation, etc. (similar to *Armut and TaskRabbit*).  
The project is being developed for **personal growth and portfolio building**.

---

## 🚀 Technologies & Tools
- **ASP.NET Core 8**
- **Entity Framework Core 8**
- **Onion Architecture**
- **MongoDB (document-based data storage)**
- **SQL Server (SSMS)** (Identity & Authentication)
- **JWT Authentication**

---

## 🔑 Features Implemented
- **Authentication & Authorization**
  - JWT authentication service
  - Login with email or username
  - Register functionality
  - Email verification
  - Authorized access to endpoints via JWT
  - Refresh token & Access token
- **User Management**
  - Identity integrated with SQL Server
- **Architecture & Code Structure**
  - Onion Architecture with `Core`, `Infrastructure`, and `Presentation` layers
  - Dependency Injection with built-in ASP.NET Core IoC container
  - Service abstraction via interfaces
  - Centralized exception handling via custom middleware
  - Service registration for modularity
  - Integration Testing

---

## 🔐 Authentication & Token Management
The application features a secure JWT-based authentication system with the following endpoints via AuthController:

- **Register**: Create new user accounts with validated credentials.
- **Login**: Authenticate using email or username, returning access and refresh tokens.
- **RefreshToken**: Obtain a new access token using a valid refresh token.
- **Logout**: Revoke tokens on the server and clear tokens from cookies for secure sign-out.

Token management includes secure HTTP-only cookies and revocation mechanisms to prevent unauthorized access.

---

## 🧪 Testing
- Integration tests implemented to ensure stability of endpoints and services.

---

## 📂 Project Structure
```
LetMeFixWeb/
├─ Core
│  ├─ LetMeFix.Domain
│  │  ├─ Interfaces
│  │  │  └─ IGenericRepository.cs
│  │  └─ Entities
│  │
│  └─ LetMeFix.Application (references Domain)
│     ├─ DTOs
│     ├─ Interfaces
│     └─ Mappings
│
├─ Infrastructure
│  ├─ LetMeFix.Persistence (references Application)
│  │  ├─ Migrations
│  │  ├─ Repository
│  │  └─ Services
│  │
│  ├─ LetMeFix.Infrastructure (references Application)
│  │  ├─ Services
│  │  │  └─ EmailService.cs
│  │  ├─ MongoDBSettings.cs
│  │  ├─ ServiceRegistrations.cs
│  │  └─ UserDbContext.cs
│
├─ Presentation
│  └─ LetMeFix.API (references Persistence + Infrastructure + Application)
│     ├─ Controllers
│     ├─ logs
│     ├─ Middlewares
│     ├─ Properties
│     ├─ appsettings.json
│     └─ Program.cs
│
└─ Tests
```
