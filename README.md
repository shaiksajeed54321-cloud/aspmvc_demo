# SajidMvc – ASP.NET Core MVC + EF Core + SQL Server

Requirements: .NET 8 SDK, SQL Server (or SQL Express) running locally.

## Run
    dotnet restore
    dotnet run

On first start the app creates the `sajid_mvc_db` database, a `Products` table and 3 sample rows.
Open the https URL printed in the console, then go to **Products**.

## Connection string
`appsettings.json` -> `ConnectionStrings:DefaultConnection`
- Default instance, Windows auth: `Server=localhost;Database=sajid_mvc_db;Trusted_Connection=True;TrustServerCertificate=True`
- SQL Express: `Server=.\SQLEXPRESS;...`
- LocalDB: `Server=(localdb)\MSSQLLocalDB;...`
- SQL login: `Server=localhost;Database=sajid_mvc_db;User Id=sa;Password=YOUR_PASSWORD;TrustServerCertificate=True`

## Using migrations instead of EnsureCreated
Remove the `EnsureCreated()` block in `Program.cs`, then:

    dotnet tool install --global dotnet-ef
    dotnet ef migrations add InitialCreate
    dotnet ef database update
