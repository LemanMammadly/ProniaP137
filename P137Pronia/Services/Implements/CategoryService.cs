using System;
using Microsoft.EntityFrameworkCore;
using P137Pronia.DataAccess;
using P137Pronia.Models;
using P137Pronia.Services.Interfaces;

namespace P137Pronia.Services.Implements
{
    public class CategoryService : ICategoryService 
    {
        readonly ProniaDBContext _context;
        public CategoryService(ProniaDBContext context)
        {
            _context = context;
        }

        public IQueryable<Category> GetTable => _context.Set<Category>();

        public async Task Create(string name)
        {
            if (name == null) throw new ArgumentNullException();
            if(await _context.Categories.AnyAsync(c=>c.Name==name))
            {
                throw new Exception();
            }
            await _context.Categories.AddAsync(new Category() { Name = name });
            await _context.SaveChangesAsync();
        }

        public Task Delete(int? id)
        {
            throw new NotImplementedException();
        }

        public async Task<ICollection<Category>> GetAll()
        {
            return await _context.Categories.ToListAsync();
        }

        public Task<Category> GetById(int? id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> isAllExist(List<int> ids)
        {
            foreach (var id in ids)
            {
                if(!await isExist(id))
                     return false;
            }
            return true;
        }

        public Task<bool> isExist(int id)
            => _context.Categories.AnyAsync(c=>c.Id==id);

        public Task Update(int? id, string name)
        {
            throw new NotImplementedException();
        }
    }
}

