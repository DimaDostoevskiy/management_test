using asu_management.mvc.Data;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace asu_management.mvc.Domain
{
    public abstract class BaseRepository<T> where T : BaseEntity
    {
        protected ManagementDbContext _context;
        protected DbSet<T> _dbSet;
        protected BaseRepository(ManagementDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }
        public async Task<T> GetByIdAsync(int id)
        {
            try
            {
                return await _dbSet.FirstOrDefaultAsync(x => x.Id == id);
            }
            catch (Exception ex)
            {
                Log.Fatal($"     GetByIdAsync | {ex.GetType().ToString()} | {ex.Message}");
                return null;
            }
        }
        public async Task<List<T>> GetAllAsync()
        {
            try
            {
                return await _dbSet.ToListAsync();
            }
            catch (Exception ex)
            {
                Log.Fatal($"     GetAllAsync | {ex.GetType().ToString()} | {ex.Message}");
                return null;
            }
        }
        public async Task<bool> CreateAsync(T entity)
        {
            try
            {
                _dbSet.Add(entity);
                return (await _context.SaveChangesAsync() > 0) ? true : false;
            }
            catch (Exception ex)
            {
                Log.Fatal($"     CreateAsync | {ex.GetType().ToString()} | {ex.Message}");
                return false;
            }
        }
        public async Task<bool> UpdateAsync(T entity)
        {
            try
            {
                _dbSet.Update(entity);
                return (await _context.SaveChangesAsync() > 0) ? true : false;
            }
            catch (Exception ex)
            {
                Log.Fatal($"     UpdateAsync | {ex.GetType().ToString()} | {ex.Message}");
                return false;
            }
        }
        public async Task<bool> RemoveAsync(T entity)
        {
            try
            {
                _dbSet.Remove(entity);
                return (await _context.SaveChangesAsync() > 0) ? true : false;
            }
            catch (Exception ex)
            {
                Log.Fatal($"     RemoveAsync | {ex.GetType().ToString()} | {ex.Message}");
                return false;
            }
        }
    }
}