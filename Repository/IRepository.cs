using asu_management.mvc.Data;

namespace asu_management.mvc.Repository
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task <T> GetByIdAsync(int id);
        Task <IEnumerable<T>> GetAllAsync();
        Task<bool> Create(T entity);
        Task<bool> Update(T entity);
        Task<bool> Delete(T entity);
    }
}