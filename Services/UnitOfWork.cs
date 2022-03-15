using asu_management.mvc.Data;

namespace asu_management.mvc.Services
{
    public class UnitOfWork : IUnitOfWork
    {
        private ManagementDbContext _context;
        private BaseRepository<OrderItem>? _orderItems;
        private BaseRepository<Provider>? _providers;
        private BaseRepository<Order>? _orders;
        public UnitOfWork(ManagementDbContext context)
        {
            _context = context;
        }

        public IRepository<Provider> Providers
        {
            get
            {
                return _providers ??
                    (_providers = new BaseRepository<Provider>(_context));
            }
        }

        public IRepository<Order> Orders
        {
            get
            {
                return _orders ??
                    (_orders = new BaseRepository<Order>(_context));
            }
        }
        public IRepository<OrderItem> OrderItems
        {
            get
            {
                return _orderItems ??
                    (_orderItems = new BaseRepository<OrderItem>(_context));
            }
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}