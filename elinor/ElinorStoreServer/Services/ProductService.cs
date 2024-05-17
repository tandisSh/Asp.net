using ElinorStoreServer.Data.Domain;
using ElinorStoreServer.Data.Entities;
using Microsoft.EntityFrameworkCore;
using share.Models.Product;
using System.Linq;

namespace ElinorStoreServer.Services
{
    public class ProductService
    {
        private readonly StoreDbContext _context;
        public ProductService(StoreDbContext context)
        {
            _context = context;
        }
        public Product? Get(int id)
        {
            Product? product = _context.Products.Find(id);
            return product;
        }
        public async Task<Product?> GetAsync(int id)
        {
            Product? product = await _context.Products.FindAsync(id);
            return product;
        }
        public async Task<List<Product>> GetsAsync()
        {
            List<Product> products = await _context.Products.ToListAsync();
            return products;
        }
        public async Task AddAsync(ProductAddRequestDto model)
        {
            Product product = new Product
            {

                Name = model.Name,
                Description = model.Description,
                Price = model.Price,
                CreatedAt = model.CreatedAt,
                ImageFileName = model.ImageFileName,
                count = model.count,
                CategoryId = model.CategoryId,

            };

            _context.Products.Add(product);
            await _context.SaveChangesAsync();
        }
        public async Task EditAsync(Product product)
        {
            Product? oldProduct = await _context.Products.FindAsync(product.Id);
            if (oldProduct is null)
            {
                throw new Exception("محصولی با این شناسه پیدا نشد.");
            }
            oldProduct.Price = product.Price;
            oldProduct.Name = product.Name;
            oldProduct.Description = product.Description;
            oldProduct.ImageFileName = product.ImageFileName;
            _context.Products.Update(oldProduct);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            Product? product = await _context.Products.FindAsync(id);
            if (product is null)
            {
                throw new Exception("محصولی با این شناسه پیدا نشد.");
            }
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }

        public async Task<List<OrderSearchResponseDto>> SearchAsync(OrderSearchRequestDto model)
        {
            var products = await _context.Products
                                   .Where(a =>
                                    (model.count == null || a.count <= model.count) &&
                                    (model.FromDate == null || a.CreatedAt >= model.FromDate) &&
                                    (model.ToDate == null || a.CreatedAt <= model.ToDate) &&
                                    (model.CategoryName == null || a.Category.Name.Contains(model.CategoryName)) &&
                                    (model.ProductName == null || a.Name.Contains(model.ProductName)) &&
                                    (model.MinPrice == null || a.Price >= model.MinPrice) &&
                                    (model.Maxprice == null || a.Price <= model.Maxprice)
                                    )
                                   .Skip((model.PageNo ) * model.PageSize) 
                                   .Take(model.PageSize)
                                   .Select(a => new OrderSearchResponseDto
                                   {
                                       ProductId = a.Id,
                                       ProductName = a.Name,
                                       CategoryId = a.CategoryId,
                                       count = a.count,
                                       Price = a.Price,
                                       CreatedAt = a.CreatedAt,
                                       Description = a.Description,
                                       CategoryName = a.Category.Name,
                                       CategoryImageFileName = a.Category.ImageFileName,
                                       ProductImageFileName = a.ImageFileName
                                   })
                                   .ToListAsync();
            return products;
        }

    }
}