using asu_management.mvc.Data;
using asu_management.mvc.PageModel;
using asu_management.mvc.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace asu_management.mvc.Domain
{
    public class OrderRepository : BaseRepository<Order>
    {
        public OrderRepository(ManagementDbContext context)
            : base(context)
        {
        }
        public async Task<OrderViewModel> GetOrderByIdAsync(int id)
        {
            OrderViewModel model = Mapper.MapOrderToModel(await base.GetByIdAsync(id));
            return model;
        }
        public async Task<OrderViewModel[]> GetAllOrdersAsync()
        {
            var orders = await base.GetAllAsync();

            if (orders == null)
            {
                return null;
            }

            List<OrderViewModel> resultList = new();

            foreach (var item in orders)
            {
                resultList.Add(Mapper.MapOrderToModel(item));
            }

            return resultList.ToArray();
        }
        public async Task<OrderViewModel[]> SortOrderAsync(IndexOrderPageModel model)
        {
            var filterOrders = await base.GetAllAsync();

            if (model.IsSortNumber && !string.IsNullOrWhiteSpace(model.SortNumber))
            {
                filterOrders = filterOrders
                                    .Where(x => x.Number.Contains(model.SortNumber))
                                    .ToList();
            }
            if (model.IsSortDay)
            {
                filterOrders = filterOrders
                                    .Where(x => x.Date >= model.StartSortDate
                                             && x.Date <= model.EndSortDate)
                                    .ToList();
            }
            if (model.IsSortProvider)
            {
                filterOrders = filterOrders
                                    .Where(x => x.Provider.Id == model.ProviderId)
                                    .ToList();
            }

            List<OrderViewModel> resultList = new();

            foreach (var item in filterOrders)
            {
                resultList.Add(Mapper.MapOrderToModel(item));
            }

            Log.Information($"   SortAsync 0k ");

            return resultList.ToArray();
        }
        public async Task<bool> CreateOrderAsync(OrderViewModel model)
        {
            var order = await Mapper.MapModelToOrderAsync(model, base._context);
            return await base.CreateAsync(order);
        }
        public async Task<bool> DeleteOrderAsync(int id)
        {
            return await base.RemoveAsync(id);
        }
        public async Task<bool> OrderUpdateAsync(OrderEditPageModel model)
        {
            Order order = await Mapper.MapModelToOrderAsync(model.Order, base._context);
            return await base.UpdateAsync(order);
        }
        public async Task<SelectList> GetListProvaidersAsync()
        {
            try
            {
                var selectList = new SelectList(await _context.Providers.ToListAsync(), "Id", "Name");
                Log.Information($"   GetListProvaidersAsync 0k ");
                return selectList;
            }
            catch (Exception ex)
            {
                Log.Fatal($"   GetListProvaidersAsync {ex.GetType().ToString()} | {ex.Message} ");
                return null;
            }
        }
    }
}