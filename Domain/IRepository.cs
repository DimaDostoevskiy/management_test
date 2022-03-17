namespace asu_management.mvc.Domain
{
    public interface IRepository<T> where T : class
    {
        Task<T> GetByIdAsync(int id);
        Task<ICollection<T>> GetAllAsync();
        Task<ICollection<T>> SortAsync(T model);
        Task<bool> Create(T model);
        Task<bool> Update(T model);
        Task<bool> Delete(T model);
    }
}