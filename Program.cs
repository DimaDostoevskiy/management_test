using asu_management.mvc.Data;
using asu_management.mvc.Domain;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Logger
builder.Host.UseSerilog((ctx, lc) => lc
    .WriteTo.Console()
    );

// Repositiry
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IItemRepository, ItemRepository>();

builder.Services.AddControllers();

builder.Services.AddRazorPages();

builder.Services.AddDbContext<ManagementDbContext>(options => 
            options.UseSqlServer("name=ConnectionStrings:DefaultConnection"));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddControllersWithViews();

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// Configure the HTTP request pipeline.

var app = builder.Build();

SeedData.Initialize(app);

if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Order/Error");
    app.UseHsts();
}

app.UseSerilogRequestLogging();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Order}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
