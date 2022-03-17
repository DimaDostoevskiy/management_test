using asu_management.mvc.Data;
using asu_management.mvc.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace asu_management.mvc.Domain
{
    public class OrderRepository : IRepository<OrderViewModel>
    {
        private readonly ManagementDbContext _context;
        public static SelectList ProvidersList;
        public OrderRepository(ManagementDbContext context)
        {
            _context = context;
        }
        private OrderViewModel Mapper(Order order)
        {
            var model = new OrderViewModel()
            {
                Id = order.Id,
                Number = order.Number,
                Date = order.Date,
            };

            model.Provider = new ProviderViewModel()
            {
                Id = order.Provider.Id,
                Name = order.Provider.Name
            };

            foreach (var item in order.Items)
            {
                model.Items.Add(new ItemViewModel()
                {
                    Id = item.Id,
                    Name = item.Name,
                    Quantity = item.Quantity,
                    Unit = item.Unit
                });
            }
            return model;
        }
        private ICollection<OrderViewModel> Mapper(IEnumerable<Order> orders)
        {
            var result = new List<OrderViewModel>();

            foreach (var order in orders)
            {
                result.Add(Mapper(order));
            }
            return result;
        }
        public async Task<OrderViewModel> GetByIdAsync(int id)
        {
            Order order = await _context.Orders
                .Include(i => i.Items)
                .Include(i => i.Provider)
                .AsNoTracking()
                .FirstOrDefaultAsync(i => i.Id == id);

            return (order == null) ? null : Mapper(order);
        }
        public async Task<ICollection<OrderViewModel>> GetAllAsync()
        {
            var orders = await _context.Orders
                .Include(o => o.Items)
                .Include(o => o.Provider)
                .AsNoTracking()
                .ToListAsync();

            return Mapper(orders);
        }
        public async Task<ICollection<OrderViewModel>> SortAsync(OrderViewModel model)
        {
            if (!string.IsNullOrEmpty(model.Number))
            {
                var orders = await _context.Orders
                    .Where(o => o.Number == model.Number)
                    .Include(i => i.Items)
                    .Include(p => p.Provider)
                    .AsNoTracking()
                    .ToListAsync();

                return Mapper(orders);
            }

            var result = await _context.Orders
                .Where(o => o.Date >= model.StartSortDate && o.Date <= model.EndSortDate)
                .Include(i => i.Items)
                .Include(p => p.Provider)
                .AsNoTracking()
                .ToListAsync();

            result = result
                .Where(o => o.ProviderId == model.Provider.Id)
                .ToList();

            return Mapper(result);
        }
        public async Task<bool> Create(OrderViewModel model)
        {
            try
            {
                var newOrder = new Order()
                {
                    Number = model.Number,
                    Date = model.Date,
                    ProviderId = model.Provider.Id
                };
                
                _context.Add(newOrder);

                return (await _context.SaveChangesAsync() > 0) ? true : false;
            }
            catch
            {
                return false;
            }
        }
        public async Task<bool> Delete(OrderViewModel model)
        {
            try
            {
                var deleteOrder = _context.Orders
                    .FirstOrDefault(o => o.Number == model.Number);

                if (deleteOrder == null)
                {
                    return false;
                }

                var items = _context.OrderItems
                                    .Where(item => item.OrderId == deleteOrder.Id)
                                    .ToArray();

                foreach (var item in items)
                {
                    _context.OrderItems.Remove(item);
                }
                _context.Remove(deleteOrder);

                return (await _context.SaveChangesAsync() > 0) ? true : false;
            }
            catch
            {
                return false;
            }
        }
        public async Task<bool> Update(OrderViewModel model)
        {
            throw new NotImplementedException();
        }

    }
}