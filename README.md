# My Fitt Peak

Plateforme web de mise en relation entre coachs sportifs et clients.

## Stack

- Frontend: React, TypeScript, Vite
- Backend: ASP.NET Core Web API, .NET 8, C#
- Database: SQL Server avec Entity Framework Core
- Auth: ASP.NET Core Identity + JWT + roles `Client` et `Coach`

## Structure

```text
backend/
  MyFittPeak.Api/
    Constants/
    Controllers/
    Data/
    Models/
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
dotnet ef database update --project .\backend\MyFittPeak.Api
dotnet run --project .\backend\MyFittPeak.Api
```

```powershell
cd frontend
npm install
npm run dev
```
