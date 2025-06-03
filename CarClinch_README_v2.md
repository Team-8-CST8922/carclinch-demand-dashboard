# CarClinch Analytics Dashboard Team Repo

Welcome to the CarClinch Analytics Dashboard project. This repository contains the backend and frontend code for the MVP analytics dashboard, following our chosen **First Approach** (using Azure SQL, ASP .NET Core 9 Web API, and React SPA).

---

## Project Overview

We are building the **CarClinch Analytics Dashboard**, which will display search and sales insights by make, model, and body type over time. The dashboard will consist of:

- **Backend (ASP .NET Core 9 Web API)**  
  - Uses an Azure SQL Database (later) or LocalDB for development.  
  - Exposes endpoints like `/api/search-counts` and `/api/sales-counts`.  
  - Applies EF Core 9 for data access and migrations.

- **Frontend (React SPA)**  
  - Queries the backend endpoints.  
  - Renders interactive charts (e.g., with Chart.js or Recharts).  
  - Provides filters for Make, Model, BodyType, and Date Range.

We have chosen to start with the **First Approach** (no Synapse) so that we can quickly scaffold the core API and agree on data models. Once the backend is stable, we’ll integrate Synapse if required.

---
## Team & Collaboration


We will work collaboratively using Git branches. Each member will:

1. Create a personal feature branch for assigned tasks.  
2. Commit changes to that branch.  
3. Submit a Pull Request (PR) to `main` for review.  
4. Address feedback, then merge to `main` once approved.

### Branching Strategy

- The **default branch** is `main` (protected).  
- For each new feature/task, create a branch following this pattern:  
  ```
  feature/<your-initials>/<short-description>
  ```  
  - Example: `feature/AB/backend-setup`

- **Do not commit directly to `main`**. Always open a PR.  
- Keep your branch up to date by regularly pulling `main`:  
  ```bash
  git checkout main
  git pull origin main
  git checkout feature/AB/backend-setup
  git merge main
  ```

### Code Review & Merging

- Once your feature is ready, push your branch and open a PR against `main`.  
- Assign at least one teammate to review your PR.  
- Address any comments or requested changes.  
- After approval, merge your branch into `main`.  
- Delete merged branches to keep the repo clean.

---
## Prerequisites

Before you begin, ensure you have:

1. **Git** (version control) installed.  
2. **.NET 9 SDK** installed (for the backend).  
3. **Node.js (v16+)** and **npm** installed (for the frontend).  
4. **Visual Studio 2022** (or VS Code) configured for ASP .NET Core development.  
5. **Azure CLI** (optional, for local testing of Azure SQL connection).  
6. **Access to Azure SQL Database** (connection string details will be supplied later).  

---
## Repository Structure

```
/ (root)
  |-- .github/
  |   `-- workflows/             # GitHub Actions CI/CD pipelines (later)
  |-- backend/                   # ASP .NET Core 9 Web API project
  |   |-- Controllers/
  |   |-- Data/                  # EF Core DbContext & Migrations
  |   |-- Models/                # Data Models (e.g., SearchCount, SalesCount)
  |   |-- appsettings.json       # Holds local connection string placeholder
  |   `-- Program.cs
  |-- frontend/                  # React SPA
  |   |-- public/
  |   |-- src/
  |   |   |-- components/
  |   |   |-- pages/
  |   |   `-- apiClient.js       # Axios or fetch wrapper
  |   `-- package.json
  |-- azure/                     # Bicep templates (IaC) for Azure SQL + App Service
  |-- README.md                  # This file
  `-- .gitignore
```

---
## Getting Started: Backend API

We’ll begin by scaffolding the backend API. Since we do not yet have a database connection string, we will add a simple health-check endpoint (HealthController) so you can run and verify the API without errors.

### 1. Clone the Repository

In your terminal, run:
```bash
git clone https://github.com/Team-8-CST8922/carclinch-demand-dashboard.git          
cd analytics-dashboard
```

### 2. Create Your Feature Branch

Before making any changes, create a branch:
```bash
# Replace AB with your initials and describe your task
git checkout -b feature/AB/backend-health-stub
```
Make sure you are on this feature branch before proceeding.

### 3. Install Dependencies

Navigate to the `backend` folder and restore packages:
```bash
cd backend
dotnet restore
```
This will download EF Core, SQL Server provider, and any other NuGet packages.

### 4. Add HealthController Stub

Since the real database is not yet available, add a controller that does not reference DbContext. Create `Controllers/HealthController.cs` with this content:
```csharp
using Microsoft.AspNetCore.Mvc;

namespace CarClinch.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HealthController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get() => Ok("OK");
    }
}
```
This endpoint will respond to `GET /api/health` with “OK,” allowing you to confirm that routing and middleware are configured correctly.

### 5. Build & Run Locally Without a Database

1. In a terminal (inside the `backend` folder):
   ```bash
   dotnet build
   dotnet run
   ```
   - The API should start (by default) on `https://localhost:5001`.
2. Test the health endpoint by visiting:
   ```
   GET https://localhost:5001/api/health
   ```
   You should receive a 200 OK response with body:
   ```
   OK
   ```
3. If this works, you know Auth0 integration and routing are correct. You can work on frontend or other tasks in parallel.

### 6. Configure Connection String (When Available)

Once the project owner supplies the Azure SQL connection string, update `backend/appsettings.json`:

```jsonc
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=<YOUR_SQL_SERVER>.database.windows.net;Database=CarClinchDB;User Id=<USERNAME>;Password=<PASSWORD>;TrustServerCertificate=False;"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}
```
- Replace `<YOUR_SQL_SERVER>`, `<USERNAME>`, and `<PASSWORD>` with the credentials provided by CarClinch.  
- Do not commit real credentials; use environment variables or Azure Key Vault in production.

### 7. Define EF Core Endpoints (Later)

After connection string is configured:

1. In `Models/`, create classes: `SearchCount` and `SalesCount` (as shown in the previous README).
2. In `Data/ApplicationDbContext.cs`, add:
   ```csharp
   public DbSet<SearchCount> SearchCounts { get; set; }
   public DbSet<SalesCount> SalesCounts { get; set; }
   ```
3. Run EF Core migrations:
   ```bash
   dotnet ef migrations add InitialCreate
   dotnet ef database update
   ```
4. Create controllers (`SearchCountsController.cs`, `SalesCountsController.cs`) that inject `ApplicationDbContext`, accept query parameters, and return aggregated results.
5. Build & run locally again:
   ```bash
   dotnet build
   dotnet run
   ```
6. Test `/api/search-counts` and `/api/sales-counts` endpoints via Swagger or Postman.

---

## Future Steps: Frontend & CI/CD

- **Frontend (React SPA)**  
  - After backend endpoints are live, scaffold a React app in `/frontend`.  
  - Set up API client (`apiClient.js`) to call `/api/search-counts` and `/api/sales-counts`.  
  - Build filter components (Make, Model, BodyType, Date Range) and chart components.

- **CI/CD (GitHub Actions)**  
  - Create workflows in `.github/workflows/` to build, test, and deploy:  
    1. Build & test backend.  
    2. Build frontend (npm install & npm run build).  
    3. Deploy backend to Azure App Service.  
    4. Deploy frontend to Azure Static Web App or App Service.

- **IaC (Azure Bicep)**  
  - The `/azure` folder will contain `main.bicep` to provision Azure SQL, App Service plans, and App Services.  
  - Later, add Bicep for SSL, networking, and CI/CD resource connections.

---

