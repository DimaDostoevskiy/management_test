using asu_management.mvc.Data;
using asu_management.mvc.Models;
using Microsoft.EntityFrameworkCore;

namespace asu_management.mvc.Services
{
    public class Repository
    {
        private readonly ManagementDbContext _context;
        public Repository(ManagementDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<OrderViewModel>> GetAllOrders()
        {
            var modelList = new List<OrderViewModel>();
            var list = await _context.Orders
                            .FromSqlRaw($"SELECT * FROM Orders")
                            .AsNoTracking()
                            .ToListAsync();

            foreach (var item in list)
            {
                modelList.Add(
                    new OrderViewModel
                    {
                        Id = item.Id,
                        Number = item.Number,
                        Date = item.Date,
                        ProviderId = item.ProviderId
                    }
                );
            }
            return modelList;
        }
        public async Task<bool> Add(OrderViewModel model)
        {
            try
            {
                _context.Orders.Add(new Order()
                {
                    Number = model.Number,
                    Date = model.Date,
                    ProviderId = model.ProviderId
                });
                await _context.SaveChangesAsync();
                await _context.DisposeAsync();
                return true;
            }
            catch
            {
                await _context.DisposeAsync();
                return false;
            }
        }
    }
}