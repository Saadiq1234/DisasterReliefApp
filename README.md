# DisasterReliefApp (with Identity scaffolded)

## Quick start

1. Ensure .NET 8 SDK is installed.
2. Update `appsettings.json` connection string if you want to use a different SQL Server.
3. From project folder run:
   - `dotnet restore`
   - `dotnet ef database update`  (this will apply the included simplified migration)
   - `dotnet run`

## Admin account
- Email: admin@example.com
- Password: Admin123!

Notes:
- The included migration is a simplified placeholder to help get started. For production or a full schema, run `dotnet ef migrations add Initial` locally to create a precise migration that includes Identity tables.
