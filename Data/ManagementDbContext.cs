using Microsoft.EntityFrameworkCore;
using asu_management.mvc.Models;

namespace asu_management.mvc.Data;

public class ManagementDbContext : DbContext
{
    public ManagementDbContext(DbContextOptions<ManagementDbContext> options)
    : base(options)
    {
    }
    public DbSet<Provider> Providers { set; get; }
    public DbSet<Order> Orders { set;  get; }
}
