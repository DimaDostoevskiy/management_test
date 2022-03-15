using asu_management.mvc.Data;

namespace asu_management.mvc.Services
{
    public interface IRepository<T> where T : BaseEntity
    {
        T GetById(int id);
        IEnumerable<T> GetAll();
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}