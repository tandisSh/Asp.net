using ElinorStoreServer.Data.Domain;
using ElinorStoreServer.Data.Entities;
using Microsoft.EntityFrameworkCore;
using share.Models.Basket;
using share.Models.Order;
using share.Models.Product;
using share.Models.User;

namespace ElinorStoreServer.Services
{
    public class BasketService
    {
        private readonly StoreDbContext _context;
        private List<Basket> basket;

        public int ProductId { get; private set; }

        public BasketService(StoreDbContext context)
        {
            _context = context;
        }
        public async Task<Basket?> GetAsync(int id)
        {
            Basket? basket = await _context.Baskets.FindAsync(id);

            return basket;
        }
        public async Task<List<Basket>> GetsAsync()
        {
            List<Basket> baskets = await _context.Baskets.ToListAsync();
            return baskets;
        }
        public async Task<List<Basket>> GetsByProductAsync(int ProductId)
        {
            List<Basket> baskets = await _context.Baskets.Where(basket => basket.ProductId == ProductId).ToListAsync();
            return baskets;
        }
        public async Task<List<Basket>> GetsByUserAsync(string userId)
        {
            List<Basket> baskets = await _context.Baskets.Where(basket => basket.UserId == userId).ToListAsync();
            return baskets;
        }
        public async Task AddAsync(BasketAddRequestDto model)
        {
            Basket basket = new Basket
            {
                UserId = model.UserId,
                Count = model.Count,
                ProductId = model.ProductId,
            };

            _context.Baskets.Add(basket);
            await _context.SaveChangesAsync();
        }
        public async Task EditAsync(Basket basket)
        {
            Basket? oldBasket = await _context.Baskets.FindAsync(basket.Id);
            if (oldBasket is null)
            {
                throw new Exception("سبد خریدی  با این شناسه پیدا نشد.");
            }
            oldBasket.Count = basket.Count;
            oldBasket.ProductId = basket.ProductId;
            oldBasket.UserId = basket.UserId;

            _context.Baskets.Update(oldBasket);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            Basket? basket = await _context.Baskets.FindAsync(id);
            if (basket is null)
            {
                throw new Exception("سبد خریدی  با این شناسه پیدا نشد.");
            }
            _context.Baskets.Remove(basket);
            await _context.SaveChangesAsync();
        }

        internal async Task AddAsync(Basket basket)
        {
            throw new NotImplementedException();
        }

       public async Task<List<BasketSearchResponseDto>> SearchAsync(BasketSearchRequestDto model)
        {
            //جستوجو بر اساس فیلدهای مختلف
            IQueryable<Basket> baskets = _context.Baskets
                                .Where(a =>
                                (model.UserName == null || a.User.Name.Contains(model.UserName))
                               && (model.ProductName == null || a.Product.Name.Contains(model.ProductName))
                                );
            if (!string.IsNullOrEmpty(model.SortBy))
            {
                switch (model.SortBy)
                {
                    case "CountAsc":
                        baskets = baskets.OrderBy(a => a.Count);
                        break;
                    case "CountDesc":
                        baskets = baskets.OrderByDescending(a => a.Count);
                        break;
                }
            }

            baskets = baskets.Skip(model.PageNo * model.PageSize).Take(model.PageSize);

            var searchResults = await baskets
                                .Select(a => new BasketSearchResponseDto
                                {
                                    ProductId = a.Product.Id,
                                    UserName = a.User.Name,
                                    ProductName = a.Product.Name,
                                    count = a.Count,
                                    Price = a.Product.Price,
                                    UserId = a.User.Id, 
                                    ProductImageFileName = a.Product.ImageFileName,
                                    Description = a.Product.Description
                                }
                )
                                .ToListAsync();
            return searchResults;
        }


        public async Task<List<share.Models.Basket.BasketReportByUserResponseDto>> BasketReportByUserIdAsync(BasketReportByUserRequestDto model)
        {
            /*   تعداد دفعاتی که یک کالای مشخص توسط یک کاربر مشخص ثبت شده   */
            var BasketsQuery =await  _context.Baskets.Where(a => a.User.Id == model.UserId
                                   )
                .GroupBy(a => a.ProductId ) // Group by both UserId and ProductId
                .Select(g => new
                {
                    UserId = g.Key,
                    number = g.Count(),
                    Product =g.First().Product,
                    
                })
            .ToListAsync();
            var result =  BasketsQuery.Select(b => new BasketReportByUserResponseDto
            {
                ProductId = b.Product.Id,
                ProductName = b.Product.Name,
                UserId = model.UserId,
                number = b.number
            }).ToList();

            return result;

        }
        public async Task<List<share.Models.Basket.BasketReportByUserAllProResponseDto>> BasketReportByUserIdAllProAsync(BasketReportByUserRequestDto model)
        {
            /*   تعداد کل کالاهایی که هر کاربر ثبت کرده   */
            var BasketsQuery = await _context.Baskets.Where(a => a.User.Id == model.UserId
                                   )
                .GroupBy(a => a.UserId) // Group by both UserId and ProductId
                .Select(g => new
                {
                    UserId = g.Key,
               

                    TotalSum = g.Sum(s => s.Count)

               

                })
            .ToListAsync();
            var result = BasketsQuery.Select(b => new BasketReportByUserAllProResponseDto
            {
                
                UserId = model.UserId,
                Count = b.TotalSum
            }).ToList();

            return result;

        }
        public async Task<List<BasketAllProductCountResponseDto>> BasketAllProductAsync(BasketAllProductCountRequestDto model)
        {
            //تعداد کل کالاهای توی بسکت
            var TotalCount = await _context.Baskets
             .Select(o => o.Count)
             .SumAsync();
        
            var result = new List<BasketAllProductCountResponseDto>
            {
                new BasketAllProductCountResponseDto
                {
                    
                    TotalCount = TotalCount
                }
            };
            /*            result = result.Skip((model.PageNo) * model.PageSize).Take(model.PageSize).ToList();
            */
            return result;
        }



    }
}
