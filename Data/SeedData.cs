using Microsoft.EntityFrameworkCore;

namespace asu_management.mvc.Data
{
    public static class SeedData
    {
        public static async Task Initialize(ManagementDbContext context)
        {
            if (context.Providers.Any())
            {
                return;
            }

            context.Database.EnsureCreated();

            context.Providers.AddRange(
                new Provider
                {
                    Name = "Provider1"
                },
                new Provider
                {
                    Name = "Provider2"
                },
                new Provider
                {
                    Name = "Provider3"
                }
            );

            context.Orders.AddRange(
                new Order
                {
                    Number = "0000-0000-0001",
                    Date = DateTime.UtcNow,
                    ProviderId = 1001,
                },
                new Order
                {
                    Number = "0000-0000-0002",
                    Date = DateTime.UtcNow,
                    ProviderId = 1002,
                },
                new Order
                {
                    Number = "0000-0000-0003",
                    Date = DateTime.UtcNow,
                    ProviderId = 1002,
                }
            );

            context.OrderItems.AddRange(
                new OrderItem
                {
                    Name = "Item1",
                    Quantity = 15.35M,
                    Unit = "P-123",
                    OrderId = 1
                },
                new OrderItem
                {
                    Name = "Item1",
                    Quantity = 9.35M,
                    Unit = "P-321",
                    OrderId = 1
                },
                new OrderItem
                {
                    Name = "Item1",
                    Quantity = 23.35M,
                    Unit = "P-456",
                    OrderId = 2
                }
            );
            await context.SaveChangesAsync();
        }
    }
}
