using asu_management.mvc.Data;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace asu_management.mvc.Repository
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly ManagementDbContext _context;
        private readonly DbSet<T> _dbSet;
        public Repository(ManagementDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }
        public async Task<bool> Create(T entity)
        {
            try
            {
                _dbSet.Add(entity);
                if (await _context.SaveChangesAsync() < 1)
                {
                    return false;
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }
        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FirstOrDefaultAsync(item => item.Id == id);
        }
        public async Task<T> GetOrderWithItemAsync(int id)
        {
            return await _dbSet.Include(c => c.Id)
            .FirstOrDefaultAsync(item => item.Id == id);
        }
        public async Task<bool> Update(T entity)
        {
            try
            {
                _dbSet.Update(entity);
                if (await _context.SaveChangesAsync() < 1)
                {
                    return false;
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        public async Task<bool> Delete(T entity)
        {
            try
            {
                _dbSet.Remove(entity);
                if (await _context.SaveChangesAsync() < 1)
                {
                    return false;
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}