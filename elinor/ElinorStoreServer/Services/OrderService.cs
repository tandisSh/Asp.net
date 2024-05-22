using ElinorStoreServer.Data.Domain;
using ElinorStoreServer.Data.Entities;
using Microsoft.EntityFrameworkCore;
using share.Models.Basket;
using share.Models.Order;
using share.Models.Product;


namespace ElinorStoreServer.Services
{
    public class OrderService
    {
        private readonly StoreDbContext _context;
        private List<Order> order;

        public int ProductId { get; private set; }

        public OrderService(StoreDbContext context)
        {
            _context = context;
        }
        public async Task<Order?> GetAsync(int id)
        {
            Order? order = await _context.Orders.FindAsync(id);
            return order;
        }
        public async Task<List<Order>> GetsAsync()
        {
            List<Order> orders = await _context.Orders.ToListAsync();
            return orders;
        }
        public async Task<List<Order>> GetsByProductAsync(int productId)
        {
            List<Order> orders = await _context.Orders.Where(order => order.ProductId == productId).ToListAsync();
            return order;
        }
        public async Task<List<Order>> GetsByUserAsync(string userId)
        {
            List<Order> orders = await _context.Orders.Where(order => order.UserId == userId).ToListAsync();
            return order;
        }
        public async Task AddAsync(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
         
        }

        public async Task AddRangeAsync(List<OrderAddRequestDto> orders)
        {
            var order = orders.Select(orderDto => new Order
            {

                Count = orderDto.Count,
                Price = orderDto.Price,
                ProductId = orderDto.ProductId,
                UserId = orderDto.UserId,
            
            }).ToList();

            _context.Orders.AddRange(order);
            await _context.SaveChangesAsync();
        }

        public async Task EditAsync(Order order)
        {
            Order? oldOrder = await _context.Orders.FindAsync(order.Id);
            if (oldOrder is null)
            {
                throw new Exception("سفارشی  با این شناسه پیدا نشد.");
            }
            oldOrder.Count = order.Count;
            oldOrder.Price = order.Price; 
            oldOrder.ProductId = order.ProductId;
            oldOrder.UserId = order.UserId;

            _context.Orders.Update(oldOrder);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            Order? order = await _context.Orders.FindAsync(id);
            if (order is null)
            {
                throw new Exception("سفارشی  با این شناسه پیدا نشد.");
            }
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
        }

        public async Task<List<OrderSearchResponseDto>> SearchAsync(OrderSearchRequestDto model)
        {
            IQueryable<Order> Orders = _context.Orders
                                .Where(a =>
                                (model.Count == null || a.Count <= model.Count)
                               /* && (model.FromDate == null || a.CreatedAt >= model.FromDate)
                                && (model.ToDate == null || a.CreatedAt <= model.ToDate)*/
                               && (model.UserName == null || a.User.Name.Contains(model.UserName))
                               && (model.ProductName == null || a.Product.Name.Contains(model.ProductName))
                                );
                                 if (!string.IsNullOrEmpty(model.SortBy))
                                {
                                    switch (model.SortBy)
                                    {
                                        case "CountAsc":
                                            Orders = Orders.OrderBy(a => a.Count);
                                            break;
                                        case "PriceDesc":
                                            Orders = Orders.OrderByDescending(a => a.Count);
                                            break;
                                    }
                                  }

                                Orders = Orders.Skip(model.PageNo * model.PageSize).Take(model.PageSize);

                                var searchResults = await Orders
              
                                .Select(a => new OrderSearchResponseDto
                                {
                                    ProductId = a.Product.Id,
                                    UserName = a.User.Name,
                                    ProductName = a.Product.Name,
                                    count = a.Count,
                                    Price = a.Product.Price,
                                    /*  CreatedAt = a.CreatedAt,*/
                                    Description = a.Product.Description
                                }
                )
                                .ToListAsync();
            return searchResults;
        }


        public async Task<List<share.Models.Order.OrderReportByProductResponseDto>> OrdersReportByProductAsync(OrderReportByProductRequestDto model)
        {
            var ordersQuery = _context.Orders.Where(a =>
                                (model.FromDate == null || a.CreatedAt >= model.FromDate)
                               && (model.ToDate == null || a.CreatedAt <= model.ToDate)
                                )
                .GroupBy(a => a.ProductId)
                .Select(a => new
                {
                    ProductId = a.Key,
                    TotalSum = a.Sum(s => s.Price)
                });

            var productsQuery = from product in _context.Products
                                from order in ordersQuery.Where(a => a.ProductId == product.Id).DefaultIfEmpty()
                                select new share.Models.Order.OrderReportByProductResponseDto
                                {
                                    ProductName = product.Name,
                                    ProductCategoryName = product.Category.Name,
                                    ProductId = product.Id,
                                    TotalSum = (int?)order.TotalSum
                                };

            productsQuery = productsQuery.Skip(model.PageNo * model.PageSize)
                                .Take(model.PageSize);
            var result = await productsQuery.ToListAsync();
            return result;
        }
        public async Task<List<share.Models.Order.OrderReportByProductResponseDto>> OrderCountReportByProductAsync(OrderReportByProductRequestDto model)
        {
            var OrdersQuery = _context.Orders.Where(a =>
                                model.ProductId == null || a.Product.Id == model.ProductId

                                )
                .GroupBy(a => a.ProductId)
                .Select(a => new
                {
                    ProductId = a.Key,
                    Count = a.Count()
                });
            var result = await OrdersQuery.Select(b => new share.Models.Order.OrderReportByProductResponseDto
            {
                ProductId = b.ProductId,
                Count = b.Count
            }).ToListAsync();

            return result;

        }
    }

}
