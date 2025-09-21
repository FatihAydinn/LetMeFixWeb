# Let Me Fix (Personal Development Project)

This project is a **web application** built with **ASP.NET Core 8** and follows the **Onion Architecture**.  
The main purpose is to provide a platform where users can **request and offer services** such as education, repair, cleaning, transportation, etc. (similar to *Armut.com*).  
The project is being developed for **personal growth and portfolio building**.

---

## 🚀 Technologies & Tools
- **ASP.NET Core 8**
- **Entity Framework Core 8**
- **Onion Architecture**
- **MongoDB** (for Tickets)
- **SQL Server (SSMS)** (for Identity & Authentication)
- **JWT Authentication**
- **Integration Testing**

---

## 🔑 Features Implemented
- **Authentication & Authorization**
  - JWT authentication service
  - Login with email or username
  - Register functionality
  - Authorized access to endpoints via JWT
- **User Management**
  - Identity integrated with SQL Server
- **Ticket Management**
  - Ticket entity stored in MongoDB
  - Ticket IDs converted to hashed format
  - CRUD operations:
    - Get Ticket by ID
    - Update Ticket
    - Delete Ticket
    - Get All Tickets
    - Create Ticket
- **Architecture & Code Structure**
  - Onion Architecture with `Core`, `Infrastructure`, and `Presentation` layers
  - Dependency Injection & IoC
  - Abstraction via `ITicketService`
  - Service registration for modularity

---

## 🔐 Authentication & Token Management
The application features a secure JWT-based authentication system with the following endpoints via AuthController:

- **Register**: Create new user accounts with validated credentials.
 **Login**: Authenticate using email or username, returning access and refresh tokens.
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
│── Core/                # Business logic & domain models
│── Infrastructure/      # Data access (EF Core, MongoDB, Identity)
│── Presentation/        # API layer (Controllers, JWT, Endpoints)
│── Tests/               # Integration tests
```


---

## ⚙️ Getting Started
### Prerequisites
- .NET 8 SDK
- SQL Server
- MongoDB
- Identity with JWT
