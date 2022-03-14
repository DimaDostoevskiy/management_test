using Microsoft.EntityFrameworkCore;

namespace asu_management.mvc.Data
{
    public static class SeedData
    {
        public static void Initialize(ManagementDbContext context)
        {
            if (context.Providers.Any())
            {
                return;
            }
            context.Database.EnsureCreated();

            context.Providers.AddRange
            (
                new Provider() { Name = "Provider1" },
                new Provider() { Name = "Provider2" },
                new Provider() { Name = "Provider3" },
                new Provider() { Name = "Provider4" }
            );
            
            context.SaveChanges();
        }
    }
}
