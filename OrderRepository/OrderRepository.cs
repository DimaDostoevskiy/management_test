using asu_management.mvc.Data;
using Microsoft.EntityFrameworkCore;

namespace asu_management.mvc.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ManagementDbContext _context;

        public OrderRepository(ManagementDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Create(Order entity)
        {
            try
            {
                _context.Add(entity);
                return (await _context.SaveChangesAsync() > 0) ? true : false;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> Delete(Order entity)
        {
            try
            {
                var items = _context.OrderItems
                .Where(item => item.Order == entity)
                .ToArray();
                foreach (var item in items)
                {
                    _context.OrderItems.Remove(item);
                }
                _context.Remove(entity);

                return (await _context.SaveChangesAsync() > 0) ? true : false;
            }
            catch
            {
                return false;
            }
        }

        public async Task<IEnumerable<Order>> GetAllAsync()
        {
            return await _context.Orders
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Order> GetByIdAsync(int id)
        {
            return await _context.Orders
                .AsNoTracking()
                .FirstOrDefaultAsync(i => i.Id == id);
        }
        public async Task<Order> GetByIdWithItemsAsync(int id)
        {
            return await _context.Orders
                .Include(i => i.Items)
                .AsNoTracking()
                .FirstOrDefaultAsync(i => i.Id == id);
        }

        public Task<bool> Update(Order entity)
        {
            throw new NotImplementedException();
        }
    }
}