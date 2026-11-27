# Pratra Travel Technologies — Flight Search REST API

A RESTful Flight Search & Management API built with **ASP.NET Core (.NET 8)**, **C#**, **Entity Framework Core**, and **Swagger/OpenAPI** for the Pratra Travel Technologies technical assessment.

## Features

- **Create Flight (`POST /api/flights`)**: Stores flight details including `airlineName`, `flightNumber`, `source`, `destination`, `departureTimeUtc`, `arrivalTimeUtc`, and `price`.
- **Get Flight by ID (`GET /api/flights/{id}`)**: Retrieves a flight record or returns `404 ProblemDetails` if not found.
- **Search Flights (`GET /api/flights/search`)**: Filters flights by `from`, `to`, and `date` (`YYYY-MM-DD`) and returns matching results sorted ascending by lowest `price`.
- **Input Validation & Global Error Handling**: Validates query/body parameters and business rules (distinct airports, valid arrival after departure, positive price) and returns standardized `ProblemDetails` responses.
- **Clean Architecture & Async I/O**: Separates Controller, Service, Repository, and EF Core `DbContext` layers with Dependency Injection, `async`/`await`, and `CancellationToken` support.
- **Swagger / OpenAPI**: Interactive endpoint documentation and testing UI in Development mode.

## Project Structure

```text
PRATATechnicalAssessment/
├── Controllers/
│   └── FlightsController.cs
├── Data/
│   ├── FlightDbContext.cs
│   └── DbSeeder.cs
├── DTOs/
│   ├── CreateFlightRequest.cs
│   ├── FlightResponse.cs
│   └── FlightSearchQuery.cs
├── Middleware/
│   └── ExceptionHandlingMiddleware.cs
├── Models/
│   └── Flight.cs
├── Repositories/
│   ├── IFlightRepository.cs
│   └── FlightRepository.cs
├── Services/
│   ├── IFlightService.cs
│   └── FlightService.cs
├── appsettings.json
├── Program.cs
└── README.md
```

## Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) (`net8.0`)
- EF Core CLI tool (`dotnet-ef` `8.*`) if generating migrations manually:
  ```powershell
  dotnet tool install --global dotnet-ef --version 8.*
  ```

> **Database Note:** This repository is configured to run out of the box with **EF Core SQLite** (`Data Source=flights.db`) for zero-server local setup. To switch to **SQL Server**, install `Microsoft.EntityFrameworkCore.SqlServer` (`8.*`), update `ConnectionStrings:DefaultConnection` in `appsettings.json` to your SQL Server connection string, and use `options.UseSqlServer(...)` in `Program.cs`.

## Setup & Run Instructions

1. **Clone the repository:**
   ```bash
   git clone <your-github-repository-url>
   cd <repository-folder>/PRATATechnicalAssessment
   ```

2. **Restore NuGet packages:**
   ```powershell
   dotnet restore
   ```

3. **Create the EF Core migration (if `Migrations/` is not already present):**
   ```powershell
   dotnet ef migrations add InitialCreate
   ```

4. **Run the application:**
   ```powershell
   dotnet run
   ```
   On startup, EF Core applies pending migrations and `DbSeeder` seeds sample flight records automatically. Note the local `http` / `https` URLs printed in the console.

## API Testing

### 1. Swagger UI
Open `/swagger` in your browser while the app is running in Development mode (for example, `https://localhost:7001/swagger` or `http://localhost:5000/swagger`—check your console output for the exact port).

### 2. Search Flights (`GET`)
```http
GET /api/flights/search?from=MAA&to=DXB&date=2026-11-15
```

Example `curl` command:
```bash
curl -X GET "https://localhost:7001/api/flights/search?from=MAA&to=DXB&date=2026-11-15"
```

### 3. Create Flight (`POST`)
```bash
curl -X POST "https://localhost:7001/api/flights" \
  -H "Content-Type: application/json" \
  -d '{
    "airlineName": "Qatar Airways",
    "flightNumber": "QR529",
    "source": "MAA",
    "destination": "DXB",
    "departureTimeUtc": "2026-11-15T18:30:00Z",
    "arrivalTimeUtc": "2026-11-15T22:10:00Z",
    "price": 19500.00
  }'
```

### 4. Get Flight by ID (`GET`)
```bash
curl -X GET "https://localhost:7001/api/flights/1"
```
