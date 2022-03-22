using asu_management.mvc.Data;
using asu_management.mvc.Domain;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Logger
builder.Host.UseSerilog((ctx, lc) => lc
                .WriteTo.Console());

// Repositiry
builder.Services.AddScoped<OrderRepository>(container => 
            new OrderRepository(container.GetService<ManagementDbContext>()));

builder.Services.AddScoped<ItemRepository>(container => 
            new ItemRepository(container.GetService<ManagementDbContext>()));

builder.Services.AddControllers();

builder.Services.AddRazorPages();

builder.Services.AddDbContext<ManagementDbContext>(options => 
            options.UseSqlServer("name=ConnectionStrings:DefaultConnection"));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddControllersWithViews();

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// Configure the HTTP request pipeline.

Log.Information("    Start...\n");

var app = builder.Build();

// Seed Data
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


// От маппера "из коробки" пришлось отказаться.
// Не корректно маппит. Скорее всего я что-то не так делаю.
// Работаю над этим.

//Mapper
// InitializeMapper(builder.Services);

// void InitializeMapper(IServiceCollection services)
// {
//     var provider = new MapperConfigurationExpression();

//     provider.CreateMap<Order,OrderViewModel>();
//     provider.CreateMap<OrderViewModel,Order>();

//     provider.CreateMap<ItemViewModel,OrderItem>();
//     provider.CreateMap<OrderItem,ItemViewModel>();

//     var configure = new MapperConfiguration(provider);
//     var mapper = new AutoMapper.Mapper(configure);

//     services.AddScoped<IMapper>(x => mapper);
// }
