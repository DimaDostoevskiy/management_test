namespace asu_management.mvc.Domain
{
    public interface IRepository<T> where T : class
    {
        Task<bool> CreateAsync(T model);
        Task<T> GetByIdAsync(int id);
        Task<T[]> GetAllAsync();
        Task<T[]> SortAsync(int id, string number, DateTime startDay, DateTime endDay);
        Task<bool> UpdateAsync(T model);
        Task<bool> DeleteAsync(int id);
    }
}