# Backend Scaffold for .NET Web API

A minimal starter template to get the backend up and running, with Auth0 integration and a dummy endpoint. Database integration details are left as “TBD” placeholders.

---

## Prerequisites

- [.NET 9.0 SDK (or higher)](https://dotnet.microsoft.com/download)  
- [Visual Studio Code](https://code.visualstudio.com/) (or another code editor)  
- [Azure CLI](https://docs.microsoft.com/cli/azure/install-azure-cli) (version 2.40.0+)  
- An [Auth0](https://auth0.com/) account (free tier is fine)  
- Basic familiarity with the command line (PowerShell, Bash, etc.)  

---

## Git Branching

Before you start coding, create a feature branch so your work stays isolated from `main`:

```bash
git checkout -b feature/auth0-integration
```

From here on, commit changes to this branch. When your work is ready, push and open a pull request against `main`.

---

## Assumptions & Folder Structure

1. The project is a .NET 9 Web API (empty-template) with the following folders (create them if they don’t exist):
   ```
   /Controllers
   /Services
   /Models
   appsettings.json
   Program.cs
   ```
2. Installing NuGet dependencies **does not** automatically create these folders or classes. You must verify they exist or create them manually:
   ```bash
   mkdir Controllers Services Models
   New-Item appsettings.json  # on PowerShell, or `touch appsettings.json` on Bash
   New-Item Program.cs
   ```
3. Auth0 will be used for authentication. Placeholders in `appsettings.json` must be replaced with real values.  
4. No real database exists yet—endpoints return hard-coded data until the schema is finalized.  
5. A simple health check endpoint (`GET /api/health`) returns “OK”.

---

## Setup Instructions

1. **Clone the repository and switch to your feature branch**  
   ```bash
   git clone https://github.com/your-org/your-backend-repo.git
   cd your-backend-repo
   git checkout -b feature/auth0-integration
   ```
2. **Verify or create folder structure**  
   ```bash
   # If any folder is missing, run:
   mkdir Controllers Services Models

   # Make sure appsettings.json and Program.cs exist:
   New-Item appsettings.json     # PowerShell
   New-Item Program.cs           # PowerShell
   # OR
   touch appsettings.json Program.cs   # Bash
   ```

3. **Open in VS Code (or your editor)**  
   ```bash
   code .
   ```

4. **Install NuGet packages & restore dependencies**  
   ```bash
   dotnet restore
   ```
   > **Note**: `dotnet restore` restores all NuGet packages referenced in your `.csproj` files. It does **not** create folders or generate code files. It only fetches external libraries.

5. **Configure Auth0 in `appsettings.json`**  
   ```jsonc
   {
     "Auth0": {
       "Domain": "<YOUR_AUTH0_DOMAIN>",
       "ClientId": "<YOUR_AUTH0_CLIENT_ID>",
       "ClientSecret": "<YOUR_AUTH0_CLIENT_SECRET>"
     },
     "ConnectionStrings": {
       "DefaultConnection": "<YOUR_DB_CONNECTION_STRING (TBD)>"
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
   - Replace `<YOUR_AUTH0_DOMAIN>` with your Auth0 tenant (e.g., `dev-abc123.us.auth0.com`).  
   - Replace `<YOUR_AUTH0_CLIENT_ID>` and `<YOUR_AUTH0_CLIENT_SECRET>` from your Auth0 application.  
   - Leave `DefaultConnection` blank or with a placeholder until the database is decided.

6. **Add the HealthController class in `/Controllers/HealthController.cs`**  
   ```csharp
   using Microsoft.AspNetCore.Mvc;

   namespace YourNamespace.Controllers
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

7. **Add the startup code in `Program.cs`**  
   ```csharp
   var builder = WebApplication.CreateBuilder(args);

   // Add Auth0 authentication
   builder.Services.AddAuthentication(options =>
   {
       options.DefaultAuthenticateScheme = "Auth0";
       options.DefaultChallengeScheme = "Auth0";
   })
   .AddJwtBearer("Auth0", options =>
   {
       options.Authority = $"https://{builder.Configuration["Auth0:Domain"]}/";
       options.Audience = builder.Configuration["Auth0:ClientId"];
   });

   builder.Services.AddControllers();
   var app = builder.Build();

   app.UseRouting();
   app.UseAuthentication();
   app.UseAuthorization();
   app.MapControllers();

   app.Run();
   ```

8. **Run the API locally**  
   ```bash
   dotnet run
   ```
   - By default, it will listen on `http://localhost:5000` and `https://localhost:5001`.

---

## Testing the Dummy Endpoint

1. Open your browser or a tool like cURL/Postman and call:
   ```
   GET http://localhost:5000/api/health
   ```
2. You should receive a 200 OK response with body:
   ```
   OK
   ```

This confirms:
- .NET runtime is installed.
- Your folder structure and classes are in place.
- Auth0 middleware is wired (but not yet enforced on `health`).
- A placeholder endpoint works.

---
