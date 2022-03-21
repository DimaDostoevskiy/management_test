using asu_management.mvc.Data;
using asu_management.mvc.PageModel;
using asu_management.mvc.ViewModels;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace asu_management.mvc.Domain
{
    public class ItemRepository : BaseRepository<OrderItem>
    {
        public ItemRepository(ManagementDbContext context)
            : base(context)
        {
        }
        public async Task<bool> CreateItemAsync(ItemViewModel model)
        {
            OrderItem newItem = Mapper.MapModelToItemAsync(model);

            Order order = await _context.Orders
                .FirstOrDefaultAsync(x => x.Id == model.OrderId);

            if (order == null) return false;

            newItem.Order = order;

            return await base.CreateAsync(newItem);
        }

        public async Task<ItemViewModel> GetItemByIdAsync(int id)
        {
            OrderItem item = await base.GetByIdAsync(id);
            ItemViewModel model = Mapper.MapItemToModel(item);
            return model;
        }

        public Task<ItemViewModel[]> SortItemsAsync(OrderDetailsViewModel model)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> EditItemAsync(ItemViewModel model)
        {
            OrderItem item = await base.GetByIdAsync(model.Id);

            Order order = await _context.Orders
                        .FirstOrDefaultAsync(x => x.Id == model.OrderId);

            if (order == null || item == null)
            {
                Log.Error("     NULL EditItemAsync   ");
                return false;
            }

            OrderItem updateItem = Mapper.MapModelToItemAsync(model);
            updateItem.Order = order;

            order.Items.Remove(item);
            order.Items.Add(updateItem);

            return (_context.SaveChanges() > 0) ? true : false;
        }

        public async Task<bool> DeleteItemAsync(int id)
        {
            OrderItem deleteItem = await base.GetByIdAsync(id);

            if (deleteItem == null)
            {
                Log.Error($"   NULL  DeleteItemAsync  ");
                return false;
            }
            return await base.RemoveAsync(deleteItem);
        }
    }
}