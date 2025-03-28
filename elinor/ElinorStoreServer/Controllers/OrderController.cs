﻿using ElinorStoreServer.Data.Domain;
using ElinorStoreServer.Data.Entities;
using ElinorStoreServer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
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


        [Authorize(Roles = "Admin")]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _OrderService.GetAsync(id);
            return Ok(result);
        }


        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Gets()
        {
            var result = await _OrderService.GetsAsync();
            return Ok(result);
        }

        [Authorize(Roles = "Admin")]

        [HttpGet("GetsByProduct")]
        public async Task<IActionResult> GetsByProduct(int productId)
        {
            var result = await _OrderService.GetsByProductAsync(productId);
            return Ok(result);
        }

        [Authorize(Roles = "Admin")]

        [HttpGet("Search")]
        public async Task<IActionResult> Search([FromQuery] OrderSearchRequestDto model)
        {
            var result = await _OrderService.SearchAsync(model);
            return Ok(result);
        }

        [Authorize(Roles = "Admin")]

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

        [Authorize(Roles = "Admin")]

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

        [Authorize(Roles = "Admin")]

        [HttpPut]

        public async Task<IActionResult> Edit([FromBody] Order order)
        {
            await _OrderService.EditAsync(order);
            return Ok("تغییرت انجام شد عزیزم.");
        }

        [Authorize(Roles = "Admin")]

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _OrderService.DeleteAsync(id);
            return Ok("محصول و از دست دادی!");
        }

        [Authorize(Roles = "Admin")]

        [HttpGet("OrdersReportByDate")]
        public async Task<IActionResult> OrdersReportByDate([FromQuery] OrderReportByDateRequestDto model)
        {
          var resualt=  await _OrderService.OrdersReportByDateAsync(model);
            return Ok(resualt);
        }

        [Authorize(Roles = "Admin")]

        [HttpGet("OrderCountReportByProductAsync")]
        public async Task<IActionResult> OrderCountReportByProduct([FromQuery] OrderReportByProductRequestDto model)
        {
            var result = await _OrderService.OrderCountReportByProduct(model);
            if (!result.Any())
            {
                return NotFound(" داده ای پیدا نکردیم!");
            }
        
            return Ok(result);
        }

        [Authorize(Roles = "Admin")]

        [HttpGet("OrderTotalAsync")]
        public async Task<IActionResult> OrderTotalAsync([FromQuery] orderTotalRequestDto model)
        {
            var result = await _OrderService.OrderTotalAsync(model);
            if (!result.Any())
            {
                return NotFound(" داده ای پیدا نکردیم!");
            }

            return Ok(result);
        }

    }

}