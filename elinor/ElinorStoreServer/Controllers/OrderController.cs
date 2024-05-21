using ElinorStoreServer.Data.Domain;
using ElinorStoreServer.Data.Entities;
using ElinorStoreServer.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using share.Models.Order;

namespace ElinorStoreServer.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly OrderService _OrderService;
        private readonly StoreDbContext _context;

        public OrderController(OrderService orderService, StoreDbContext context)
        {
            _OrderService = orderService;
            _context = context;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _OrderService.GetAsync(id);
            return Ok(result);
        }
        [HttpGet]
        public async Task<IActionResult> Gets()
        {
            var result = await _OrderService.GetsAsync();
            return Ok(result);
        }
        [HttpGet("GetsByProduct")]
        public async Task<IActionResult> GetsByProduct(int productId)
        {
            var result = await _OrderService.GetsByProductAsync(productId);
            return Ok(result);
        }
        [HttpGet("Search")]
        public async Task<IActionResult> Search([FromQuery] OrderSearchRequestDto model)
        {
            var result = await _OrderService.SearchAsync(model);
            return Ok(result);
        }
        [HttpGet("GetsByUser")]
        public async Task<IActionResult> GetsByUser(string userId)
        {
            var result = await _OrderService.GetsByUserAsync(userId);
            return Ok(result);
        }
        /*     [HttpPost]
             public async Task<IActionResult> Add(OrderAddRequestDto order)
             {
                 await _OrderService.AddAsync(order);
                 return Ok();
             }*/

        [HttpPost("AddRange")]
        public async Task<IActionResult> AddRange(List<OrderAddRequestDto> orders)
        {
            var orderEntities = new List<Order>();
            foreach (var orderDto in orders)
            {
                Product? product = await _context.Products.FindAsync(orderDto.ProductId);
                if (product is null)
                {
                    return NotFound($"محصولی با شناسه {orderDto.ProductId} پیدا نشد.");
                }

                if (orderDto.Count > product.count)
                {
                    return BadRequest($"تعداد درخواستی شما از موجودی  بیشتر است. تعداد موجود: {product.count}");
                }
                product.count -= orderDto.Count;
                _context.Products.Update(product);

                var order = new Order
                {
                    Count = orderDto.Count,
                    Price = orderDto.Price,
                    ProductId = orderDto.ProductId,
                    UserId = orderDto.UserId,
                    CreatedAt = DateTime.Now
                };

                orderEntities.Add(order);
            }

            _context.Orders.AddRange(orderEntities);
            await _context.SaveChangesAsync();
            return Ok("مبارکه!");
        }
        [HttpPut]

        public async Task<IActionResult> Edit([FromBody] Order order)
        {
            await _OrderService.EditAsync(order);
            return Ok("تغییرت انجام شد عزیزم.");
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _OrderService.DeleteAsync(id);
            return Ok("محصول و از دست دادی!");
        }

        [HttpGet("OrdersReportByProduct")]
        public async Task<IActionResult>OrdersReportByProduct([FromQuery]OrderReportByProductRequestDto model)
        {
          var resualt=  await _OrderService.OrdersReportByProductAsync(model);
            return Ok(resualt);
        }
    }

}