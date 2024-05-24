using ElinorStoreServer.Data.Entities;
using ElinorStoreServer.Services;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize(Roles = "Admin")]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _BasketService.GetAsync(id);

            if (result == null)
            {
                return NotFound("کاربر پیدا نشد!");
            }
      
            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("جست و جو")]
        public async Task<IActionResult> Search([FromQuery] BasketSearchRequestDto model)
        {
            var result = await _BasketService.SearchAsync(model);
            return Ok(result);


        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Gets()
        {
            var result = await _BasketService.GetsAsync();
            return Ok(result);
        }

        [Authorize()]
        [HttpGet("دریافت بر اساس محصول")]
        public async Task<IActionResult> GetsByProduct(int ProductId)
        {
            var result = await _BasketService.GetsByProductAsync(ProductId);
            return Ok(result);
        }

        [Authorize()]
        [HttpGet("دریافت بر اساس کاربر")]
        public async Task<IActionResult> GetsByUser(string userId)
        {
            var result = await _BasketService.GetsByUserAsync(userId);
            return Ok(result);
        }

        [Authorize()]
        [HttpPost("افزودن سبدخرید")]
        public async Task<IActionResult> Add(BasketAddRequestDto basket)
        {
            await _BasketService.AddAsync(basket);
            return Ok("اضافه شد!");
        }

        [Authorize()]
        [HttpPut("تغییر سبدخرید")]
        public async Task<IActionResult> Edit([FromBody] Basket basket)
        {
            await _BasketService.EditAsync(basket);
            return Ok("تغییرات انجام شد.");
        }

        [Authorize()]
        [HttpDelete("حذف سبدخرید")]
        public async Task<IActionResult> Delete(int id)
        {
            await _BasketService.DeleteAsync(id);
            return Ok("پاک شد.");
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("BasketReportByUserIdAsync")]
        public async Task<IActionResult> BasketReportByUserIdAsync([FromQuery] BasketReportByUserRequestDto model)
        {
            var result = await _BasketService.BasketReportByUserIdAsync(model);
            if (!result.Any())
            {
                return NotFound(" داده ای پیدا نکردیم!");
            }
            return Ok(result);

        }

        [Authorize(Roles = "Admin")]
        [HttpGet("BasketReportByUserIdAllProAsync")]
        public async Task<IActionResult> BasketReportByUserAllProAsync([FromQuery] BasketReportByUserRequestDto model)
        {
            var result = await _BasketService.BasketReportByUserIdAllProAsync(model);
          

            if (!result.Any())
            {
                return NotFound(" داده ای پیدا نکردیم!");
            }
            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("BasketAllProductAsync")]
        public async Task<IActionResult> BasketAllProductAsync([FromQuery] BasketAllProductCountRequestDto model)
        {
            var result = await _BasketService.BasketAllProductAsync(model);


            if (!result.Any())
            {
                return NotFound(" داده ای پیدا نکردیم!");
            }
            return Ok(result);
        }
    }
}