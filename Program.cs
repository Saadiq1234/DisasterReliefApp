using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using DisasterReliefApp.Data;          // ApplicationDbContext
using DisasterReliefApp.Models;        // ApplicationUser




var builder = WebApplication.CreateBuilder(args);

// ===== 1. Configure DbContext with SQL Server and retry-on-failure =====
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        sqlOptions =>
        {
            sqlOptions.EnableRetryOnFailure(
                maxRetryCount: 5,
                maxRetryDelay: TimeSpan.FromSeconds(10),
                errorNumbersToAdd: null
            );
        }
    )
);

// ===== 2. Configure Identity =====
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 8;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

// ===== 3. Add MVC and Razor Pages =====
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

// ===== 4. Build the app =====
var app = builder.Build();

// ===== 5. Seed roles & admin =====
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await SeedData.SeedAsync(services);

}

// ===== 6. Middleware =====
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

// ===== 7. Route mapping =====
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
