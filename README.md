# My Fitt Peak

Plateforme web de mise en relation entre coachs sportifs et clients.

## Stack

- Frontend: React, TypeScript, Vite
- Backend: ASP.NET Core Web API, .NET 8, C#
- Database: MySQL avec Entity Framework Core
- Auth: ASP.NET Core Identity + JWT + roles `Client` et `Coach`

## Structure

```text
backend/
  MyFittPeak.Domain/
    Constants/
    Entities/
    Repositories/
  MyFittPeak.Infrastructure/
    Data/
    DependencyInjection/
    Repositories/
  MyFittPeak.Api/
    Controllers/
    Contracts/
frontend/
  src/
    api/
    components/
    pages/
    routes/
    types/
```

## Demarrage local

```powershell
dotnet restore .\backend\MyFittPeak.Api\MyFittPeak.Api.csproj
dotnet ef database update --project .\backend\MyFittPeak.Infrastructure --startup-project .\backend\MyFittPeak.Api
dotnet run --project .\backend\MyFittPeak.Api
```

## Architecture backend

- `MyFittPeak.Api`: controllers, contrats HTTP, JWT, CORS, Swagger.
- `MyFittPeak.Domain`: entites metier, roles, interfaces de repositories.
- `MyFittPeak.Infrastructure`: `ApplicationDbContext`, configuration EF Core/Identity avec MySQL, implementations des repositories.

La chaine de connexion MySQL se trouve dans `backend/MyFittPeak.Api/appsettings.json`.

## Endpoints API initiaux

- `POST /api/auth/register` et `POST /api/auth/login`
- `GET /api/coaches` et `PUT /api/coaches/me`
- `GET /api/events`, `POST /api/events`, `POST /api/events/{eventId}/participants`
- `GET /api/bookings/me` et `POST /api/bookings`
- `GET /api/reviews/coach/{coachId}` et `POST /api/reviews`

```powershell
cd frontend
npm install
npm run dev
```
