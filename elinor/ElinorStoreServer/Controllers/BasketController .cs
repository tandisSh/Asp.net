using ElinorStoreServer.Data.Entities;
using ElinorStoreServer.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using share.Models.Basket;
using share.Models.Order;


namespace ElinorStoreServer.Controllers

{
    [Route("[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly BasketService _BasketService;

        public BasketController(BasketService basketService)
        {
            _BasketService = basketService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _BasketService.GetAsync(id);
            return Ok(result);
        }
        [HttpGet("جست و جو")]
        public async Task<IActionResult> Search([FromQuery] BasketSearchRequestDto model)
        {
            var result = await _BasketService.SearchAsync(model);
            return Ok(result);
        }
        [HttpGet]
        public async Task<IActionResult> Gets()
        {
            var result = await _BasketService.GetsAsync();
            return Ok(result);
        }
        [HttpGet("دریافت بر اساس محصول")]
        public async Task<IActionResult> GetsByProduct(int productId)
        {
            var result = await _BasketService.GetsByProductAsync(productId);
            return Ok(result);
        }
        [HttpGet("دریافت بر اساس کاربر")]
        public async Task<IActionResult> GetsByUser(string userId)
        {
            var result = await _BasketService.GetsByUserAsync(userId);
            return Ok(result);
        }
        [HttpPost("افزودن سبدخرید")]
        public async Task<IActionResult> Add(BasketAddRequestDto basket)
        {
            await _BasketService.AddAsync(basket);
            return Ok("اضافه شد!");
        }
        [HttpPut("تغییر سبدخرید")]
        public async Task<IActionResult> Edit([FromBody] Basket basket)
        {
            await _BasketService.EditAsync(basket);
            return Ok("تغییرات انجام شد.");
        }
        [HttpDelete("حذف سبدخرید")]
        public async Task<IActionResult> Delete(int id)
        {
            await _BasketService.DeleteAsync(id);
            return Ok("پاک شد.");
        }
        [HttpGet("BasketReportByUserIdAsync")]
        public async Task<IActionResult> BasketReportByUserIdAsync([FromQuery] BasketReportByUserRequestDto model)
        {
            var result = await _BasketService.BasketReportByUserIdAsync(model);
            return Ok(result);
            
        }
    }
}