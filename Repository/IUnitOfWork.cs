using asu_management.mvc.Data;

namespace asu_management.mvc.Repository
{
    public interface IUnitOfWork
    {
        IRepository<OrderItem> OrderItems { get; }
        IRepository<Order> Orders { get; }
        IRepository<Provider> Providers { get; }
        Task SaveAsync();
    }
}