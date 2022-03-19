using asu_management.mvc.ViewModels;

namespace asu_management.mvc.Domain
{
    public interface IOrderRepository
    {
        Task<bool> CreateAsync(OrderViewModel model);
        Task<OrderViewModel> GetByIdAsync(int id);
        Task<OrderViewModel[]> GetAllAsync();
        Task<OrderViewModel[]> SortAsync(IndexOrderViewModel model);
        Task<bool> UpdateAsync(OrderViewModel model);
        Task<bool> DeleteAsync(int id);
    }
}