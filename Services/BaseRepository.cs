using asu_management.mvc.Data;
using asu_management.mvc.Models;
using Microsoft.EntityFrameworkCore;

namespace asu_management.mvc.Services
{
    public class BaseRepository<T> : IRepository <T> 
    where T : BaseEntity
    {
        private readonly ManagementDbContext _context;
        private DbSet<T> _dbSet;
        public BaseRepository(ManagementDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }
        public void Create(T entity)
        {
            _dbSet.Add(entity);
            _context.SaveChanges();
        }
        public IEnumerable<T> GetAll()
        {
            return _dbSet.ToList();;
        }
        public T GetById(int id)
        {
            throw new NotImplementedException();
        }
        public void Update(T entity)
        {
            throw new NotImplementedException();
        }
        public void Delete(T entity)
        {
            throw new NotImplementedException();
        }
    }
}