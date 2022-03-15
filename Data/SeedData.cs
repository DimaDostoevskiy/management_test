using asu_management.mvc.Services;
using Microsoft.EntityFrameworkCore;

namespace asu_management.mvc.Data
{
    public static class SeedData
    {
        public static void Initialize(ManagementDbContext context)
        {
            var unitOfWork = new UnitOfWork(context);
            var providers = unitOfWork.Providers.GetAll();

            if (providers == null)
            {
                unitOfWork.Providers.Create((new Provider() { Name = "Provider1" }));
                unitOfWork.Providers.Create((new Provider() { Name = "Provider2" }));
                unitOfWork.Providers.Create((new Provider() { Name = "Provider3" }));
                unitOfWork.Providers.Create((new Provider() { Name = "Provider4" }));
            }
        }
    }
}
