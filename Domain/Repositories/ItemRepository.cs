using asu_management.mvc.Data;
using asu_management.mvc.PageModel;
using asu_management.mvc.ViewModels;

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
            var newItem = await Mapper.MapModelToItemAsync(model, base._context);

            if (newItem == null)
            {
                return false;
            }

            return await base.CreateAsync(newItem);
        }

        public async Task<ItemViewModel> GetItemByIdAsync(int id)
        {
            ItemViewModel model = Mapper.MapItemToModel(await base.GetByIdAsync(id));
            return model;
        }
        public Task<ItemViewModel[]> SortItemsAsync(DetailsOrderPageModel model)
        {
            throw new NotImplementedException();
        }
        public async Task<bool> UpdateItemAsync(ItemViewModel model)
        {
            var updateItem = await Mapper.MapModelToItemAsync(model, base._context);

            if (updateItem == null)
            {
                return false;
            }
            return await base.UpdateAsync(updateItem);
        }
    }
}