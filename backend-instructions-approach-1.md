# CarClinch Analytics Dashboard Team Repo

Welcome to the CarClinch Analytics Dashboard project. This repository contains the backend and frontend code for the MVP analytics dashboard, following our chosen **"First Approach"** (using Azure SQL, ASP .NET Core 9 Web API, and React SPA).

---

## Project Overview

We are building the **CarClinch Analytics Dashboard**, which will display search and sales insights by make, model, and body type over time. The dashboard will consist of:

- **Backend (ASP .NET Core 9 Web API)**
  - Connects to Azure SQL Database.
  - Exposes endpoints like `/api/search-counts` and `/api/sales-counts`.
  - Applies EF Core 9 for data access and migrations.

- **Frontend (React SPA)**
  - Queries the backend endpoints.
  - Renders interactive charts (e.g., with Chart.js or Recharts).
  - Provides filters for Make, Model, BodyType, and Date Range.

We have chosen to start with the **First Approach** (no Synapse) so that we can quickly scaffold the core API and agree on data models. Once the backend is stable, we’ll integrate Synapse if required.

---

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
  - Example: `feature/AB/backend-search-endpoint`

- **Do not commit directly to `main`**. Always open a PR.
- Keep your branch up to date by regularly pulling `main`:
  ```bash
  git checkout main
  git pull origin main
  git checkout feature/AB/backend-search-endpoint
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
6. **Access to Azure SQL Database** (connection string details supplied by CarClinch).  

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

We’ll begin by scaffolding the backend API. This allows us to:

- Agree on data models (SearchCount, SalesCount).
- Validate Azure SQL connectivity.
- Expose core endpoints early so the frontend team can stub UI components.

### 1. Clone the Repository

In your terminal, run:
```bash
git clone https://github.com/CarClinch/analytics-dashboard.git
cd analytics-dashboard
```

### 2. Create Your Feature Branch

Before making any changes, create a branch:

```bash
# Replace AB with your initials and describe your task
git checkout -b feature/AB/setup-backend-starter
```

Make sure you are on this feature branch before proceeding.

### 3. Install Dependencies

Navigate to the `backend` folder and restore packages:

```bash
cd backend
dotnet restore
```

This will download EF Core, SQL Server provider, and any other NuGet packages.

### 4. Configure Connection String

Open `backend/appsettings.json` and locate the placeholder for `DefaultConnection`:

```json
  "ConnectionStrings": {
    "DefaultConnection": "Server=<YOUR_SQL_SERVER>.database.windows.net;Database=CarClinchDB;User Id=<USERNAME>;Password=<PASSWORD>;TrustServerCertificate=False;"
  }
```

- Replace `<YOUR_SQL_SERVER>`, `<USERNAME>`, and `<PASSWORD>` with the credentials provided by CarClinch. 
- If you’re testing locally with a local SQL Server instance, update it accordingly (e.g., `Server=(localdb)\\mssqllocaldb;Database=CarClinchDB;Trusted_Connection=True;`).

> **Note:** Never commit real credentials. Use environment variables or Azure Key Vault for production. For now, the placeholder is fine for local dev.

### 5. Build & Run Locally

1. In a terminal (inside the `backend` folder):
   ```bash
   dotnet build
   dotnet run
   ```
   - The API should start (by default) on `https://localhost:5001`.

2. Verify it’s running by visiting `https://localhost:5001/health` (if a health endpoint is added) or the default Swagger page if configured.

3. If you see errors connecting to the database, double-check your connection string and network access rules in Azure SQL.

### 6. Define API Endpoints

Our initial goal is to create two endpoints:

- **GET `/api/search-counts?startDate={date}&endDate={date}&make={make}&model={model}`**
  - Returns aggregated search counts per day (or month) for the specified filters.

- **GET `/api/sales-counts?startDate={date}&endDate={date}&make={make}&model={model}`**
  - Returns aggregated sales counts per day (or month).

Steps:
1. In `Models/`, create two classes: `SearchCount` and `SalesCount` with properties like:
   ```csharp
   public class SearchCount {
     public int Id { get; set; }
     public DateTime Date { get; set; }
     public string Make { get; set; }
     public string Model { get; set; }
     public string BodyType { get; set; }
     public int Count { get; set; }
   }

   public class SalesCount {
     public int Id { get; set; }
     public DateTime Date { get; set; }
     public string Make { get; set; }
     public string Model { get; set; }
     public string BodyType { get; set; }
     public int Count { get; set; }
   }
   ```
2. In `Data/ApplicationDbContext.cs`, add `DbSet<SearchCount>` and `DbSet<SalesCount>`.  
   ```csharp
   public DbSet<SearchCount> SearchCounts { get; set; }
   public DbSet<SalesCount> SalesCounts { get; set; }
   ```
3. Run EF Core migrations:
   ```bash
   dotnet ef migrations add InitialCreate
   dotnet ef database update
   ```
4. Create controllers under `Controllers/`: `SearchCountsController.cs` and `SalesCountsController.cs`. Each should:
   - Inject `ApplicationDbContext`.
   - Define a GET action that accepts query parameters (`startDate`, `endDate`, `make`, `model`, `bodyType`, etc.).
   - Query the DbSet with LINQ, group by Date, Make, Model, BodyType, and return aggregated counts.
5. Test the endpoints using Swagger or Postman:
   ```bash
   GET https://localhost:5001/api/search-counts?startDate=2025-01-01&endDate=2025-02-01&make=Toyota
   ```

Once these steps are complete and verified, push your branch:
```bash
git add .
git commit -m "Setup backend project, add models & initial search/sales endpoints"
git push origin feature/AB/setup-backend-starter
```  
Then open a PR for review.

---
## Future Steps: Frontend & CI/CD

- **Frontend (React SPA)**
  - After backend endpoints are live, we’ll scaffold a React app in `/frontend`.
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
  - Later, we’ll add Bicep for SSL, networking, and CI/CD resource connections.

---
