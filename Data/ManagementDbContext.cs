using Microsoft.EntityFrameworkCore;

namespace asu_management.mvc.Data;

public class ManagementDbContext : DbContext
{
    public DbSet<Provider> Providers { set; get; }
    public DbSet<Order> Orders { set; get; }
    public DbSet<OrderItem> OrderItems { set; get; }
    public ManagementDbContext(DbContextOptions<ManagementDbContext> options)
    : base(options)
    {
    }
}