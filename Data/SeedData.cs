using Microsoft.EntityFrameworkCore;

namespace asu_management.mvc.Data
{
    public static class SeedData
    {
        public static void Initialize(ManagementDbContext context)
        {
            context.Database.EnsureCreated();
            
            if (context.Providers.Any())
            {
                return;
            }

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
