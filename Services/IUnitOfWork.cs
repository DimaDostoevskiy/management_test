using asu_management.mvc.Data;

namespace asu_management.mvc.Services
{
    public interface IUnitOfWork
    {
        IRepository<OrderItem> OrderItems { get; }
        IRepository<Order> Orders { get; }
        IRepository<Provider> Providers { get; }
        void SaveChanges();
    }
}