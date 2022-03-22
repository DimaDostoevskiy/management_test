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
        public OrderRepository(ManagementDbContext context) : base(context)
        {
        }
        public async Task<bool> CreateOrderAsync(OrderViewModel model)
        {
            Order newOrder = Mapper.MapModelToOrderAsync(model);

            Provider provider = await _context.Providers
                            .FirstOrDefaultAsync(x => x.Id == model.ProviderId);

            newOrder.Provider = provider;

            return await base.CreateAsync(newOrder);
        }

        public async Task<OrderViewModel> GetOrderByIdAsync(int id)
        {
            Order order = await base.GetByIdAsync(id);
            OrderViewModel model = Mapper.MapOrderToModel(order);
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

        public async Task<OrderViewModel[]> SortOrderAsync(IndexPageModel model)
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

        public ItemViewModel[] SortItems(ItemViewModel[] inItems,
                                                string sortName,
                                                string sortUnit,
                                                decimal startSortQuantity,
                                                decimal endSortQuantity)
        {
            var resultList = inItems.ToList();

            if (sortName != "All")
            {
                resultList = resultList
                            .Where(x => x.Name == sortName)
                            .ToList();
            }
            if (sortUnit != "All")
            {
                resultList = resultList
                            .Where(x => x.Unit == sortUnit)
                            .ToList();
            }

            if (startSortQuantity > 0)
            {
                resultList = resultList
                            .Where(x => x.Quantity > startSortQuantity)
                            .ToList();
            }

            if (endSortQuantity > 0)
            {
                resultList = resultList
                            .Where(x => x.Quantity < endSortQuantity)
                            .ToList();
            }

            return resultList.ToArray();
        }

        public async Task<bool> EditOrderAsync(OrderViewModel model)
        {
            try
            {
                Provider provider = await _context.Providers
                                .FirstOrDefaultAsync(x => x.Id == model.ProviderId);

                Order updateOrder = await base.GetByIdAsync(model.Id);

                if (provider == null || updateOrder == null)
                {
                    return false;
                }

                updateOrder.Provider = provider;
                updateOrder.Date = model.Date;
                updateOrder.Number = model.Number;

                return await base.UpdateAsync(updateOrder);
            }
            catch (Exception ex)
            {
                Log.Fatal($"    EditOrderAsync {ex.GetType().ToString()} | {ex.Message}");
                return false;
            }
        }

        public async Task<SelectList> GetListProvaidersAsync()
        {
            try
            {
                var providersList = await _context.Providers.ToListAsync();
                return new SelectList(providersList, "Id", "Name");
            }
            catch (Exception ex)
            {
                Log.Fatal($"   GetListProvaidersAsync {ex.GetType().ToString()} | {ex.Message} ");
                return null;
            }
        }

        public async Task<List<SelectList>> GetSelectListsAsync(int id)
        {
            try
            {
                Order order = await base.GetByIdAsync(id);

                if (order == null) { return null; };

                order.Items.Add(new OrderItem()
                {
                    Unit = "All",
                    Name = "All"
                });

                var unitSelect = new SelectList(order.Items.ToList(), "Unit", "Unit");
                var nameSelect = new SelectList(order.Items.ToList(), "Name", "Name");

                // resultList.Append(new SelectListItem("All", "Unit")); // ?

                return new List<SelectList>() { unitSelect, nameSelect };
            }
            catch (Exception ex)
            {
                Log.Fatal($"   GetListProvaidersAsync {ex.GetType().ToString()} | {ex.Message} ");
                return null;
            }
        }

        public async Task<bool> DeleteOrderAsync(int id)
        {
            Order deleteOrder = await base.GetByIdAsync(id);

            try
            {
                foreach (var item in deleteOrder.Items)
                {
                    _context.Remove(item);
                }
            }
            catch (Exception ex)
            {
                Log.Fatal($"   GetListProvaidersAsync {ex.GetType().ToString()} | {ex.Message} ");
                return false;
            }

            return await base.RemoveAsync(deleteOrder);
        }
    }
}