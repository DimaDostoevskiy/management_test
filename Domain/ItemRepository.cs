using asu_management.mvc.Data;
using asu_management.mvc.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
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
            var order = await _context.Orders
                .Include(i => i.Items)
                .AsNoTracking()
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

            return (result > 0) ? true : false;
        }

        public Task<bool> DeleteItemAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ItemViewModel[]> GetAllItemsAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<ItemViewModel> GetItemByIdAsync(int id)
        {
            var item = await _context.OrderItems
                .Include(i => i.Order)
                .FirstOrDefaultAsync(i => i.Id == id);

            return (item == null) ? null : Mapper.MapItemToModel(item);
        }
        public Task<ItemViewModel[]> SortItemsAsync(DetailsOrderViewModel model)
        {
            throw new NotImplementedException();
        }
        public async Task<bool> UpdateItemAsync(ItemViewModel model)
        {
            var item = _context.OrderItems
                                .FirstOrDefault(i => i.Id == model.Id);

            item.Quantity = model.Quantity;
            item.Name = model.Name;
            item.Unit = model.Unit;

            _context.OrderItems.Update(item);
            int result = _context.SaveChanges();
            await _context.DisposeAsync();

            return (result > 0) ? true : false;
        }
    }
}

