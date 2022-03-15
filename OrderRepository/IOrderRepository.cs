using asu_management.mvc.Data;

namespace asu_management.mvc.Repository
{
    public interface IOrderRepository
    {
        Task <IEnumerable<Order>> GetAllAsync();
        Task <Order> GetByIdAsync(int id);
        Task<Order> GetByIdWithItemsAsync(int id);
        Task<bool> Create(Order entity);
        Task<bool> Update(Order entity);
        Task<bool> Delete(Order entity);
    }
}