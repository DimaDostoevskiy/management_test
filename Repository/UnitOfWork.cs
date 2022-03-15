using asu_management.mvc.Data;

namespace asu_management.mvc.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private ManagementDbContext _context;
        private Repository<Order> _orders;
        private Repository<Provider> _providers;
        private Repository<OrderItem> _orderItems;
        public UnitOfWork(ManagementDbContext context)
        {
            _context = context;
        }
        public IRepository<Provider> Providers
        {
            get => _providers ?? (_providers = new Repository<Provider>(_context));
        }
        public IRepository<Order> Orders
        {
            get => _orders ?? (_orders = new Repository<Order>(_context));
        }
        public IRepository<OrderItem> OrderItems
        {
            get => _orderItems ?? (_orderItems = new Repository<OrderItem>(_context));
        }
        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
            await _context.DisposeAsync();
        }
    }
}