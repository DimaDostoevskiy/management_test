using asu_management.mvc.Data;
using asu_management.mvc.PageModel;
using asu_management.mvc.ViewModels;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace asu_management.mvc.Domain
{
    public class ItemRepository : BaseRepository<OrderItem>  // ItemService?
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

        public async Task<bool> EditItemAsync(ItemViewModel model)
        {
            OrderItem item = await base.GetByIdAsync(model.Id);

            item.Quantity = model.Quantity;
            item.Name = model.Name;
            item.Unit = model.Unit;

            return await base.UpdateAsync(item);
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