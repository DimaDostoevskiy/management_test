using asu_management.mvc.PageModel;
using asu_management.mvc.ViewModels;

namespace asu_management.mvc.Domain
{
    public interface IOrderRepository
    {
        Task<bool> CreateAsync(OrderViewModel model);
        Task<OrderViewModel> GetByIdAsync(int id);
        Task<OrderViewModel[]> GetAllAsync();
        ProviderViewModel[] GetAllProvaider();
        Task<OrderViewModel[]> SortAsync(IndexOrderPageModel model);
        Task<bool> UpdateAsync(OrderViewModel model);
        Task<bool> DeleteAsync(int id);
    }
}