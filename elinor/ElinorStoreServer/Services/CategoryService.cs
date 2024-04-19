using ElinorStoreServer.Data.Domain;
using ElinorStoreServer.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace ElinorStoreServer.Services
{
    public class CategoryService
    {
        private readonly StoreDbContext _context;
        public CategoryService(StoreDbContext context)
        {
            _context = context;
        }
        public Category? Get(int id)
        {
            Category? Category = _context.Categorys.Find(id);
            return Category;
        }
        public async Task<Category?> GetAsync(int id)
        {
            Category? Category = await _context.Categorys.FindAsync(id);
            return Category;
        }
        public async Task<List<Category>> GetsAsync()
        {
            List<Category> Categorys = await _context.Categorys.ToListAsync();
            return Categorys;
        }
        public async Task AddAsync(Category Category)
        {
            _context.Categorys.Add(Category);
            await _context.SaveChangesAsync();
        }
        public async Task EditAsync(Category Category)
        {
            Category? oldCategory = await _context.Categorys.FindAsync(Category.Id);
            if (oldCategory is null)
            {
                throw new Exception("محصولی با این شناسه پیدا نشد.");
            }

            oldCategory.Name = Category.Name;
            _context.Categorys.Update(oldCategory);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            Category? Category = await _context.Categorys.FindAsync(id);
            if (Category is null)
            {
                throw new Exception("محصولی با این شناسه پیدا نشد.");
            }
            _context.Categorys.Remove(Category);
            await _context.SaveChangesAsync();
        }
    }
}
