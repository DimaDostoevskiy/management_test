using System.Text;
using asu_management.mvc.Data;
using asu_management.mvc.PageModel;
using asu_management.mvc.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace asu_management.mvc.Domain
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ManagementDbContext _context;
        public static SelectList ProvidersList;
        public OrderRepository(ManagementDbContext context)
        {
            _context = context;
        }
        public async Task<OrderViewModel> GetByIdAsync(int id)
        {
            try
            {
                Order order = await _context.Orders
                    .Include(i => i.Items)
                    .Include(i => i.Provider)
                    .AsNoTracking()
                    .FirstOrDefaultAsync(i => i.Id == id);

                Log.Information($"   GetByIdAsync 0k ");
                return (order == null) ? null : Mapper.MapOrderToModel(order);
            }
            catch (Exception ex)
            {
                Log.Fatal($"   GetByIdAsync {ex.GetType().ToString()} | {ex.Message} ");
                return null;
            }
        }
        public async Task<OrderViewModel[]> GetAllAsync()
        {
            try
            {
                var orders = await _context.Orders
                    .Include(o => o.Items)
                    .Include(o => o.Provider)
                    .AsNoTracking()
                    .ToListAsync();

                if (orders == null)
                {
                    return null;
                }

                List<OrderViewModel> result = new();

                foreach (var item in orders)
                {
                    result.Add(Mapper.MapOrderToModel(item));
                }

                Log.Information($"   GetAllAsync 0k ");
                return result.ToArray();
            }
            catch (Exception ex)
            {
                Log.Fatal($"   GetAllAsync {ex.GetType().ToString()} | {ex.Message} ");
                return null;
            }
        }
        public async Task<OrderViewModel[]> SortOrderAsync(IndexOrderPageModel model)
        {
            List<OrderViewModel> resultList = new();

            StringBuilder searchString = new();

            searchString.Append("SELECT * FROM [managementdb].[dbo].[Orders]");
            bool isAND = false;

            if (model.IsSortNumber && !string.IsNullOrWhiteSpace(model.SortNumber))
            {
                searchString.Append((isAND) ? " AND" : " WHERE");
                searchString.Append(String.Format(" Number = {0}",
                model.SortNumber));
                isAND = true;
            }
            if (model.IsSortDay)
            {
                searchString.Append((isAND) ? " AND" : " WHERE");
                searchString.Append(String.Format(" ([Date] >= '{0}') AND ([Date] <= '{1}')",
                model.StartSortDate.ToString(),
                model.EndSortDate.ToString()));
                isAND = true;
            }
            if (model.IsSortProvider)
            {
                searchString.Append((isAND) ? " AND" : " WHERE");
                searchString.Append(String.Format(" [ProviderId] = {0}",
                model.ProviderId));
                isAND = true;
            }

            Log.Warning(searchString.ToString());

            try
            {
                var orders = await _context.Orders
                    .FromSqlRaw(searchString.ToString())
                    .Include(i => i.Items)
                    .Include(p => p.Provider)
                    .AsNoTracking()
                    .ToListAsync();

                foreach (var item in orders)
                {
                    resultList.Add(Mapper.MapOrderToModel(item));
                }

                // await _context.Orders
                //                 .Where(order => shouldBeFiltered && order.Field == valueToFilterBy)
                //                 .Where(order => shouldBeFiltered2 && order.Field2 < valueToFilterBy2)
                //                 .AsNoTracking()
                //                 .ToListAsync();


                Log.Information($"   SortAsync 0k ");

                return resultList.ToArray();
            }
            catch (Exception ex)
            {
                Log.Fatal($"   SortAsync {ex.GetType().ToString()} | {ex.Message} ");
                return null;
            }
        }
        public async Task<bool> CreateAsync(OrderViewModel model)
        {
            try
            {
                var newOrder = Mapper.MapModelToOrder(model);

                if (newOrder == null)
                {
                    return false;
                }

                _context.Orders.Add(newOrder);
                int result = _context.SaveChanges();
                await _context.DisposeAsync();

                Log.Information($"   CreateAsync 0k ");
                return (result > 0) ? true : false;
            }
            catch (Exception ex)
            {
                Log.Fatal($"   CreateAsync {ex.GetType().ToString()} | {ex.Message} ");
                return false;
            }
        }
        public async Task<bool> DeleteAsync(int id)
        {
            try
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

                Log.Information($"   DeleteAsync 0k ");
                return (result > 0) ? true : false;
            }
            catch (Exception ex)
            {
                Log.Fatal($"   DeleteAsync {ex.GetType().ToString()} | {ex.Message} ");
                return false;
            }
        }
        public async Task<bool> UpdateAsync(OrderViewModel model)
        {
            try
            {
                Order updateOrder = _context.Orders
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

                Log.Information($"   UpdateAsync 0k ");
                return (result > 0) ? true : false;
            }
            catch (Exception ex)
            {
                Log.Fatal($"   UpdateAsync {ex.GetType().ToString()} | {ex.Message} ");
                return false;
            }
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