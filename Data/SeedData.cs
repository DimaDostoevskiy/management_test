using Serilog;

namespace asu_management.mvc.Data
{
    public static class SeedData
    {
        /// <summary>
        /// Initializes the database with values provided it is empty
        /// </summary>
        /// <param name="applicationBuilder"></param>
        public static void Initialize(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                try
                {
                    var context = serviceScope.ServiceProvider.GetService<ManagementDbContext>();

                    context.Database.EnsureCreated();

                    if (context.Providers.Any())
                    {
                        Log.Information("    Datebase is not empty");
                        return;
                    }

                    var providers = new List<Provider>()
                    {
                        new Provider() { Name = "Provider1" },
                        new Provider() { Name = "Provider2" },
                        new Provider() { Name = "Provider3" },
                        new Provider() { Name = "Provider4" },
                    };
                    context.Providers.AddRange(providers);
                    context.SaveChanges();

                    var orders = new List<Order>()
                    {
                        new Order() { Number = "0001", Date = DateTime.UtcNow.AddDays(-1), Provider = providers[0] },
                        new Order() { Number = "0002", Date = DateTime.UtcNow.AddDays(-5), Provider = providers[1] },
                        new Order() { Number = "0003", Date = DateTime.UtcNow.AddDays(-10),Provider = providers[1] },
                        new Order() { Number = "0004", Date = DateTime.UtcNow.AddDays(-30),Provider = providers[2] },
                    };
                    context.Orders.AddRange(orders);
                    context.SaveChanges();

                    var orderItems = new List<OrderItem>()
                    {
                        new OrderItem() { Name = "Item1", Quantity = 15.256M, Unit = "Unit1", Order = orders[0] },
                        new OrderItem() { Name = "Item2", Quantity = 15.256M, Unit = "Unit2", Order = orders[0] },
                        new OrderItem() { Name = "Item3", Quantity = 15.256M, Unit = "Unit3", Order = orders[1] },
                        new OrderItem() { Name = "Item4", Quantity = 15.256M, Unit = "Unit4", Order = orders[1] },
                        new OrderItem() { Name = "Item5", Quantity = 15.256M, Unit = "Unit5", Order = orders[2] },
                    };
                    context.OrderItems.AddRange(orderItems);
                    context.SaveChanges();

                    Log.Information("    SeedData.Initialize | 0k |");

                    return;
                }
                catch (Exception ex)
                {
                    Log.Fatal($"    SeedData.Initialize | {ex.GetType().ToString()} | {ex.Message}");
                    return;
                }
            }
        }
    }
}
