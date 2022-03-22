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
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Provider>()
                                .HasMany(x => x.Orders)
                                .WithOne(x => x.Provider);
        modelBuilder.Entity<Order>()
                                .HasMany(x => x.Items)
                                .WithOne(x => x.Order);

        modelBuilder.Entity<OrderItem>()
            .Property(x => x.Quantity).HasColumnType("decimal(18, 3)");

        base.OnModelCreating(modelBuilder);
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseLazyLoadingProxies();
        base.OnConfiguring(optionsBuilder);
    }
}