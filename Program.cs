using Microsoft.EntityFrameworkCore;
using SajidMvc.Data;

var builder = WebApplication.CreateBuilder(args);

// MVC with Razor views
builder.Services.AddControllersWithViews();

// EF Core + SQL Server (connection string lives in appsettings.json)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Create the sajid_mvc_db database and tables on first run (with seed data).
// If you prefer migrations, remove this block and run:
//   dotnet ef migrations add InitialCreate
//   dotnet ef database update
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
