using asu_management.mvc.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services
    .AddControllers();

builder.Services
    .AddRazorPages();

builder.Services
    .AddDbContext<ManagementDbContext>(options => options
        .UseSqlServer("name=ConnectionStrings:DefaultConnection"));

builder.Services
    .AddDatabaseDeveloperPageExceptionFilter();

builder.Services
    .AddControllersWithViews();

builder.Services
    .AddDatabaseDeveloperPageExceptionFilter();

// Configure the HTTP request pipeline.

var app = builder.Build();

await CreateDbAsync(app);

if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();



async Task CreateDbAsync(IHost host)
{
    using (var scope = host.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        var logger = services.GetRequiredService<ILogger<Program>>();
        try
        {
            var context = services.GetRequiredService<ManagementDbContext>();
            logger.LogDebug("Creating the DB: OK.");
            await SeedData.Initialize(context);
        }
        catch (Exception ex)
        {
            logger.LogError($"Creating the DB: ERROR | {ex.GetType()} | {ex.Message}");
        }
    }
}