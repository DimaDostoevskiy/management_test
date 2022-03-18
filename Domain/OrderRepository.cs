using asu_management.mvc.Data;
using asu_management.mvc.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace asu_management.mvc.Domain
{
    public class OrderRepository : IRepository<OrderModel>
    {
        private readonly ManagementDbContext _context;
        public static SelectList ProvidersList;
        public OrderRepository(ManagementDbContext context)
        {
            _context = context;
        }

        public async Task<OrderModel> GetByIdAsync(int id)
        {
            Order order = await _context.Orders
                .Include(i => i.Items)
                .Include(i => i.Provider)
                .AsNoTracking()
                .FirstOrDefaultAsync(i => i.Id == id);

            return (order == null) ? null : Mapper.MapOrderToModel(order);
        }
        public async Task<OrderModel[]> GetAllAsync()
        {
            var orders = await _context.Orders
                .Include(o => o.Items)
                .Include(o => o.Provider)
                .AsNoTracking()
                .ToListAsync();

            List<OrderModel> result = new();

            foreach (var item in orders)
            {
                result.Add(Mapper.MapOrderToModel(item));
            }

            return result.ToArray();
        }
        public async Task<OrderModel[]> SortAsync(int providerId, string number, DateTime startDay, DateTime endDay)
        {
            List<OrderModel> resultList = new();

            if (!string.IsNullOrEmpty(number))
            {
                var orders = await _context.Orders
                    .Where(o => o.Number == number)
                    .Include(i => i.Items)
                    .Include(p => p.Provider)
                    .AsNoTracking()
                    .ToListAsync();

                foreach (var item in orders)
                {
                    resultList.Add(Mapper.MapOrderToModel(item));
                }
                return resultList.ToArray();
            }

            var sortOrders = await _context.Orders
                .Where(o => o.Date >= startDay && o.Date <= endDay)
                .Include(i => i.Items)
                .Include(p => p.Provider)
                .AsNoTracking()
                .ToListAsync();

            sortOrders = sortOrders
                .Where(o => o.ProviderId == providerId)
                .ToList();

            foreach (var item in sortOrders)
            {
                resultList.Add(Mapper.MapOrderToModel(item));
            }

            return resultList.ToArray();
        }
        public async Task<bool> CreateAsync(OrderModel model)
        {
            Order newOrder = Mapper.MapModelToOrder(model);

            _context.Orders.Add(newOrder);
            int result = _context.SaveChanges();
            await _context.DisposeAsync();

            return (result > 0) ? true : false;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var deleteOrder = _context.Orders
                .FirstOrDefault(o => o.Id == id);

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
            int result = _context.SaveChanges();
            await _context.DisposeAsync();

            return (result > 0) ? true : false;
        }
        public async Task<bool> UpdateAsync(OrderModel model)
        {
            Order updateOrder = _context.Orders
                            .Include(i => i.Items)
                            .Include(i => i.Provider)
                            .FirstOrDefault(i => i.Id == model.Id);

            if (updateOrder == null)
            {
                return false;
            }

            updateOrder.Date = model.Date;
            updateOrder.Number = model.Number;
            updateOrder.ProviderId = model.ProviderId;

            _context.Orders.Update(updateOrder);
            int result = _context.SaveChanges();
            await _context.DisposeAsync();

            return (result > 0) ? true : false;
        }
    }
}