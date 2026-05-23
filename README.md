# Project Task Management API

A clean and maintainable backend API built with ASP.NET Core Web API, Entity Framework Core, SQL Server, ASP.NET Core Identity, and JWT Authentication.

This project was developed as a technical assessment for the Backend .NET Developer position.

---

## Project Overview

The API allows authenticated users to manage their own projects and tasks.

Each authenticated user can:

* Register and login
* Create, update, delete, and view projects
* Create tasks inside projects
* View tasks by project
* Update task status
* Delete tasks

---

## Technologies Used

* .NET 9
* ASP.NET Core Web API
* Entity Framework Core
* SQL Server
* ASP.NET Core Identity
* JWT Authentication
* Swagger / OpenAPI

---

## Architecture Overview

The solution follows a simple Clean Architecture style with three main layers:

### Core Layer

Contains:

* Domain entities
* DTOs
* Enums
* Service contracts

### Infrastructure Layer

Contains:

* ApplicationDbContext
* EF Core configuration
* Service implementations
* Database migrations

### WebAPI Layer

Contains:

* Controllers
* Global exception middleware
* JWT authentication configuration
* Swagger configuration

---

## Project Structure

```text
ProjectTaskManagement
│
├── ProjectTaskManagement.Core
│   ├── Domain
│   │   └── Entities
│   ├── DTOs
│   │   ├── Auth
│   │   ├── Projects
│   │   └── Tasks
│   ├── Enums
│   └── ServiceContracts
│
├── ProjectTaskManagement.Infrastructure
│   ├── DbContext
│   ├── Services
│   └── Migrations
│
└── ProjectTaskManagement.WebAPI
    ├── Controllers
    ├── Middlewares
    ├── Program.cs
    └── appsettings.json
```

---

## Features

### Authentication

* Register
* Login
* JWT token generation

### Projects

* Create project
* Get all projects
* Get project by id
* Update project
* Delete project

### Tasks

* Create task inside project
* Get tasks by project
* Update task status
* Delete task

---

## Database Design

### ApplicationUser

Uses ASP.NET Core Identity.

Additional custom field:

| Field    | Type   |
| -------- | ------ |
| FullName | string |

---

### Project

| Field       | Type     |
| ----------- | -------- |
| Id          | Guid     |
| Name        | string   |
| Description | string?  |
| CreatedAt   | DateTime |
| UserId      | string   |

Relationship:

* One user can have many projects.
* Each project belongs to one user.

---

### ProjectTask

| Field       | Type                |
| ----------- | ------------------- |
| Id          | Guid                |
| Title       | string              |
| Description | string?             |
| Status      | ProjectTaskStatus   |
| DueDate     | DateTime?           |
| Priority    | ProjectTaskPriority |
| ProjectId   | Guid                |

Relationship:

* One project can have many tasks.
* Each task belongs to one project.
* Deleting a project deletes its related tasks.

---

## Authentication

The API uses JWT Bearer Authentication.

After login, the API returns a JWT token.

Use this token in Swagger to access protected endpoints.

In Swagger:

1. Login using `/api/auth/login`
2. Copy the returned token
3. Click `Authorize`
4. Paste the token only without the `Bearer` prefix

Example:

```text
eyJhbGciOiJIUzI1NiIsInR5cCI6...
```

Swagger will automatically send it as:

```text
Authorization: Bearer {token}
```

---

## API Documentation

Swagger UI is available after running the project:

```text
https://localhost:7293/swagger
```

Or depending on the selected launch profile:

```text
https://localhost:44385/swagger
```

Swagger is used as the API documentation for this project.

---

## API Endpoints

### Auth Endpoints

| Method | Endpoint             | Description             | Auth Required |
| ------ | -------------------- | ----------------------- | ------------- |
| POST   | `/api/auth/register` | Register new user       | No            |
| POST   | `/api/auth/login`    | Login and get JWT token | No            |

---

### Projects Endpoints

| Method | Endpoint             | Description           | Auth Required |
| ------ | -------------------- | --------------------- | ------------- |
| POST   | `/api/projects`      | Create project        | Yes           |
| GET    | `/api/projects`      | Get all user projects | Yes           |
| GET    | `/api/projects/{id}` | Get project by id     | Yes           |
| PUT    | `/api/projects/{id}` | Update project        | Yes           |
| DELETE | `/api/projects/{id}` | Delete project        | Yes           |

---

### Tasks Endpoints

| Method | Endpoint                          | Description                | Auth Required |
| ------ | --------------------------------- | -------------------------- | ------------- |
| POST   | `/api/projects/{projectId}/tasks` | Create task inside project | Yes           |
| GET    | `/api/projects/{projectId}/tasks` | Get tasks by project       | Yes           |
| PATCH  | `/api/tasks/{taskId}/status`      | Update task status         | Yes           |
| DELETE | `/api/tasks/{taskId}`             | Delete task                | Yes           |

---

## Setup Instructions

### Prerequisites

Make sure the following tools are installed:

* Visual Studio 2022
* .NET 9 SDK
* SQL Server
* SQL Server Management Studio or equivalent

---

### Clone Repository

```bash
git clone YOUR_REPOSITORY_LINK_HERE
```

```bash
cd ProjectTaskManagement
```

---

### Update Connection String

Open `appsettings.json` inside the WebAPI project and update the connection string if needed:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=.;Database=ProjectTaskManagementDb;Trusted_Connection=True;TrustServerCertificate=True"
}
```

If SQL Server uses localhost:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=ProjectTaskManagementDb;Trusted_Connection=True;TrustServerCertificate=True"
}
```

---

### JWT Settings

Make sure the JWT settings exist in `appsettings.json`:

```json
"Jwt": {
  "Key": "ThisIsASecretKeyForProjectTaskManagementApi123456",
  "Issuer": "ProjectTaskManagementAPI",
  "Audience": "ProjectTaskManagementClient",
  "ExpirationMinutes": 60
}
```

Note:

For production, JWT keys should be stored using environment variables or user secrets.

---

### Restore NuGet Packages

```bash
dotnet restore
```

---

### Apply Database Migrations

Using Package Manager Console:

```powershell
Update-Database
```

Make sure:

```text
Default project = ProjectTaskManagement.Infrastructure
Startup project = ProjectTaskManagement.WebAPI
```

Or using terminal:

```bash
dotnet ef database update --project ProjectTaskManagement.Infrastructure --startup-project ProjectTaskManagement.WebAPI
```

---

### Run the Project

Set `ProjectTaskManagement.WebAPI` as the startup project and run the application.

Swagger should open at:

```text
https://localhost:7293/swagger
```

---

## Database Migrations

Migration files are included in:

```text
ProjectTaskManagement.Infrastructure/Migrations
```

To add a new migration:

```powershell
Add-Migration MigrationName
```

To update the database:

```powershell
Update-Database
```

Using terminal:

```bash
dotnet ef migrations add MigrationName --project ProjectTaskManagement.Infrastructure --startup-project ProjectTaskManagement.WebAPI
```

```bash
dotnet ef database update --project ProjectTaskManagement.Infrastructure --startup-project ProjectTaskManagement.WebAPI
```

---

## Sample Test Data

### Register

```http
POST /api/auth/register
```

```json
{
  "fullName": "Alaa Mohamed",
  "email": "alaa.test@example.com",
  "password": "123456",
  "confirmPassword": "123456"
}
```

---

### Login

```http
POST /api/auth/login
```

```json
{
  "email": "alaa.test@example.com",
  "password": "123456"
}
```

Response example:

```json
{
  "token": "jwt-token-here",
  "expiration": "2026-05-23T03:30:00Z",
  "email": "alaa.test@example.com",
  "fullName": "Alaa Mohamed"
}
```

---

### Create Project

```http
POST /api/projects
```

```json
{
  "name": "Graduation Project",
  "description": "A project for managing graduation tasks and milestones"
}
```

---

### Create Task

```http
POST /api/projects/{projectId}/tasks
```

```json
{
  "title": "Create Database Design",
  "description": "Design tables and relationships for the project",
  "dueDate": "2026-05-30T10:00:00",
  "priority": 1
}
```

Priority values:

```text
0 = Low
1 = Medium
2 = High
```

---

### Update Task Status

```http
PATCH /api/tasks/{taskId}/status
```

```json
{
  "status": 1
}
```

Status values:

```text
0 = Pending
1 = InProgress
2 = Completed
```

---

## Error Handling

The project includes global exception handling middleware.

It returns clean JSON responses for common errors.

Example error response:

```json
{
  "success": false,
  "statusCode": 404,
  "message": "Project not found"
}
```

Handled examples:

* `KeyNotFoundException` → 404 Not Found
* `UnauthorizedAccessException` → 401 Unauthorized
* `InvalidOperationException` → 400 Bad Request
* `ArgumentException` → 400 Bad Request
* Unhandled exceptions → 500 Internal Server Error

---

## Validation

Validation is implemented using Data Annotations in DTOs.

Examples:

* Required fields
* Email format validation
* String length validation
* Password confirmation validation
* Nullable due date support

ASP.NET Core automatically validates request models using `[ApiController]`.

---

## Clean Architecture Notes

The solution keeps responsibilities separated:

* Controllers handle HTTP requests and responses.
* Services contain application logic.
* DbContext is used inside the Infrastructure layer.
* DTOs are used for request and response models.
* Entities are not exposed directly from controllers.
* Authentication logic is separated inside `AuthService`.
* JWT generation is separated inside `JwtService`.
* Error handling is centralized in middleware.

---

## Important Notes

* This project is a backend-only API.
* Swagger is used for API testing and documentation.
* Each authenticated user can access only their own projects and tasks.
* Project and task IDs are implemented using GUIDs.
* Project deletion cascades to its related tasks.
* Seed data is not required.
* Test data can be created using Swagger.
* The project focuses on clean, maintainable, and scalable backend structure.

---

## Author

Rana Khaled Ali 
