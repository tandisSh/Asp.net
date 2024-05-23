using ElinorStoreServer.Data.Domain;
using ElinorStoreServer.Data.Entities;
using Microsoft.EntityFrameworkCore;
using share.Models.Order;
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
        /*sort*/
        public async Task<List<Product>> SortByPriceAsync()
        {

            var products = await _context.Products.ToListAsync();

            var sortedProducts = products.OrderBy(p => p.Price).ToList();

            return sortedProducts;
        }

        /*DesSort*/

        public async Task<List<Product>> DesSorAsync()
        {
            var products = await _context.Products.ToListAsync();

            var sortedProducts = products.OrderByDescending(p => p.Price).ToList();

            return sortedProducts;
        }


        /*add*/
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
        /*edit*/
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
        /*delete*/
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
       
        public async Task<List<SearchResponseDto>> SearchAsync(SearchRequestDto model)
        {
            /*search in products*/
            IQueryable<Product> products = _context.Products
               .Where(a =>
                    (model.count == null || a.count <= model.count)
                    && (model.FromDate == null || a.CreatedAt >= model.FromDate)
                    && (model.ToDate == null || a.CreatedAt <= model.ToDate)
                    && (model.CategoryName == null || a.Category.Name.Contains(model.CategoryName))
                     && (model.ProductName == null || a.Name.Contains(model.ProductName))
                    && (model.MinPrice == null || a.Price >= model.MinPrice)
                    && (model.Maxprice == null || a.Price <= model.Maxprice));

            if (!string.IsNullOrEmpty(model.SortBy))
            {
                switch (model.SortBy)
                {
                    case "PriceAsc":
                        products = products.OrderBy(a => a.Price);
                        break;
                    case "PriceDesc":
                        products = products.OrderByDescending(a => a.Price);
                        break;
                }
            }

            products = products.Skip(model.PageNo * model.PageSize).Take(model.PageSize);

            var searchResults = await products
               .Select(a => new SearchResponseDto
               {
                   ProductId = a.Id,
                   ProductName = a.Name,
                   CategoryId = a.CategoryId,
                   count = a.count,
                   Price = a.Price,
                   CreatedAt = a.CreatedAt,
                   Description = a.Description,
                   CategoryName = a.Category.Name,
               
               })
               .ToListAsync();

            return searchResults;
        }

        public async Task<List<Product>> GetsUnAvailableProductsAsync()
        {
            // کالاهای ناموجود
            List<Product> productsWithZeroCount = await _context.Products.Where(p => p.count <= 0).ToListAsync();
            return productsWithZeroCount;
        }


    }
}