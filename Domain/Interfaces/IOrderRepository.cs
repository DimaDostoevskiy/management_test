using asu_management.mvc.PageModel;
using asu_management.mvc.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace asu_management.mvc.Domain
{
    public interface IOrderRepository
    {
        Task<bool> CreateAsync(OrderViewModel model);
        Task<OrderViewModel> GetByIdAsync(int id);
        Task<OrderViewModel[]> GetAllAsync();
        Task<SelectList> GetListProvaidersAsync();
        Task<OrderViewModel[]> SortOrderAsync(IndexOrderPageModel model);
        Task<bool> UpdateAsync(OrderViewModel model);
        Task<bool> DeleteAsync(int id);
    }
}