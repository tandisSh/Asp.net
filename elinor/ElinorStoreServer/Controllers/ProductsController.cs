
using ElinorStoreServer.Data.Domain;
using ElinorStoreServer.Data.Entities;
using ElinorStoreServer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using share.Models.Product;



namespace IbulakStoreServer.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ProductService _productService;
        public ProductsController(ProductService productService)
        {
            _productService = productService;
        }

        [Authorize()]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
           /* try
            {
                var result = await _productService.GetAsync(id);
                return Ok(result);
            }
            catch (UnauthorizedAccessException ex)
            {
                throw new UnauthorizedAccessException("You must be logged in to access this resource.", ex);
            }*/

            var result = await _productService.GetAsync(id);
            return Ok(result);
        }
        [Authorize()]

        [HttpGet]
        public async Task<IActionResult> Gets()
        {
            var result = await _productService.GetsAsync();
            return Ok(result);
        }
        /*sort*/

        [Authorize()]
        [HttpGet("sort")]
        public async Task<IActionResult> Sort()
        {
            var result = await _productService.SortByPriceAsync();
            return Ok(result);
        }

        /*DesSort*/

        [Authorize()]
        [HttpGet("DesSort")]
        public async Task<IActionResult> DesSort()
        {
            var result = await _productService.DesSorAsync();
            return Ok(result);
        }

        [Authorize()]
        [HttpGet("Search")]
        /*search in products*/
        public async Task<IActionResult> Search([FromQuery] SearchRequestDto model)
        {
            var result = await _productService.SearchAsync(model);
            if (!result.Any())
            {
                return NotFound(" داده ای پیدا نکردیم!");
            }

            return Ok(result);
        }
        [Authorize(Roles = "Admin")]

        [HttpPost]
        public async Task<IActionResult> Add(ProductAddRequestDto product)
        {
            await _productService.AddAsync(product);
            return Ok("محصول شما اضافه شد.");

        }

        [Authorize(Roles = "Admin")]

        [HttpPut]
        public async Task<IActionResult> Edit([FromBody] Product product)
        {
            await _productService.EditAsync(product);
            return Ok("تغییرات اعمال شد.");
        }

        [Authorize(Roles = "Admin")]

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _productService.DeleteAsync(id);
            return Ok("شما حذف محصول انجام دادید.");
        }

        [Authorize(Roles = "Admin")]

        [HttpGet("GetsUnAvailableProducts")]
        public async Task<IActionResult> GetsUnAvailableProductsAsync()
        {
            var result = await _productService.GetsUnAvailableProductsAsync();
            if (!result.Any())
            {
                return NotFound(" داده ای پیدا نکردیم!");
            }
            return Ok(result);
        }
    }
}