using asu_management.mvc.Data;
using asu_management.mvc.Models;
using asu_management.mvc.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace asu_management.mvc.Domain
{
    public class ItemRepository : IRepository<ItemModel>
    {
        private readonly ManagementDbContext _context;
        public static SelectList ProvidersList;
        public ItemRepository(ManagementDbContext context)
        {
            _context = context;
        }

        public Task<bool> CreateAsync(ItemModel model)
        {
            throw new NotImplementedException();
        }

        public Task<ItemModel> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ItemModel[]> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ItemModel[]> SortAsync(int id, string number, DateTime startDay, DateTime endDay)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(ItemModel model)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}

