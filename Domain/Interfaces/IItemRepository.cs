using asu_management.mvc.PageModel;
using asu_management.mvc.ViewModels;

namespace asu_management.mvc.Domain
{
    public interface IItemRepository
    {
        Task<bool> CreateItemAsync(ItemViewModel model);
        Task<ItemViewModel> GetItemByIdAsync(int id);
        Task<ItemViewModel[]> SortItemsAsync(DetailsOrderPageModel model);
        Task<bool> UpdateItemAsync(ItemViewModel model);
        Task<bool> DeleteItemAsync(int id);
    }
}