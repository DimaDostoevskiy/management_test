using asu_management.mvc.Data;
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
        public void Create(T? entity)
        {
            if(entity == null)
            {
                return;
            }
            _dbSet.Add(entity);
            _context.SaveChanges();
        }
        public List<T>? GetAll()
        {
            return _dbSet.ToList();;
        }
        public T? GetById(int? id)
        {
            if(id == null)
            {
                return null;
            }
            return _dbSet.FirstOrDefault(item => item.Id == id);
        }
        public T? GetByName(string? name)
        {
            if(name == null)
            {
                return null;
            }
            return _dbSet.FirstOrDefault(item => item.Name == name);
        }
        public void Update(T? entity)
        {
            if(entity == null)
            {
                return;
            }
            _dbSet.Update(entity);
            _context.SaveChanges();
        }
        public void Delete(T? entity)
        {
            if(entity == null)
            {
                return;
            }
            _dbSet.Remove(entity);
            _context.SaveChanges();
        }
    }
}