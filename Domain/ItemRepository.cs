using asu_management.mvc.Data;
using asu_management.mvc.PageModel;
using asu_management.mvc.ViewModels;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace asu_management.mvc.Domain
{
    public class ItemRepository : IItemRepository
    {
        private readonly ManagementDbContext _context;
        public ItemRepository(ManagementDbContext context)
        {
            _context = context;
        }
        public async Task<bool> CreateItemAsync(ItemViewModel model)
        {
            try
            {
                var order = await _context.Orders
                    .Include(i => i.Items)
                    .FirstOrDefaultAsync(o => o.Id == model.OrderId);

                var newItem = Mapper.MapModelToItem(model);

                if (newItem == null || order == null)
                {
                    return false;
                }

                order.Items.Add(newItem);
                _context.Orders.Update(order);

                int result = _context.SaveChanges();
                await _context.DisposeAsync();

                Log.Information($"   CreateItemAsync 0k ");
                return (result > 0) ? true : false;
            }
            catch (Exception ex)
            {
                Log.Fatal($"   CreateItemAsync {ex.GetType().ToString()} | {ex.Message} ");
                return false;
            }
        }
        public async Task<bool> DeleteItemAsync(int id)
        {
            try
            {
                OrderItem deleteItem = await _context.OrderItems
                                        .FirstOrDefaultAsync(i => i.Id == id);

                if (deleteItem == null)
                {
                    return false;
                }

                _context.OrderItems.Remove(deleteItem);

                int result = _context.SaveChanges();
                await _context.DisposeAsync();

                Log.Information($"   DeleteItemAsync 0k ");
                return (result > 0) ? true : false;
            }
            catch (Exception ex)
            {
                Log.Fatal($"   DeleteItemAsync {ex.GetType().ToString()} | {ex.Message} ");
                return false;
            }
        }
        public async Task<ItemViewModel> GetItemByIdAsync(int id)
        {
            try
            {
                var item = await _context.OrderItems
                            .Include(i => i.Order)
                            .AsNoTracking()
                            .FirstOrDefaultAsync(i => i.Id == id);
                
                Log.Information($"   GetItemByIdAsync 0k ");

                return (item == null) ? null : Mapper.MapItemToModel(item);
            }
            catch (Exception ex)
            {
                Log.Fatal($"   GetItemByIdAsync {ex.GetType().ToString()} | {ex.Message} ");
                return null;
            }
        }
        public Task<ItemViewModel[]> SortItemsAsync(DetailsOrderPageModel model)
        {
            throw new NotImplementedException();
        }
        public async Task<bool> UpdateItemAsync(ItemViewModel model)
        {
            try
            {
                var item = await _context.OrderItems
                                    .FirstOrDefaultAsync(i => i.Id == model.Id);
                if (item == null)
                {
                    return false;
                }

                item.Quantity = model.Quantity;
                item.Name = model.Name;
                item.Unit = model.Unit;

                _context.OrderItems.Update(item);
                int result = _context.SaveChanges();
                await _context.DisposeAsync();

                Log.Information($"   UpdateItemAsync 0k ");
                return (result > 0) ? true : false;
            }
            catch (Exception ex)
            {
                Log.Fatal($"   UpdateItemAsync {ex.GetType().ToString()} | {ex.Message} ");
                return false;
            }
        }
    }
}