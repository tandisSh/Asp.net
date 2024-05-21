
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

        [Authorize(Roles="User")]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _productService.GetAsync(id);
            return Ok(result);
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Gets()
        {
            var result = await _productService.GetsAsync();
            return Ok(result);
        }
        /*sort*/
        [HttpGet("sort")]
        public async Task<IActionResult> Sort()
        {
            var result = await _productService.SortByPriceAsync();
            return Ok(result);
        }

        /*DesSort*/
        [HttpGet("DesSort")]
        public async Task<IActionResult> DesSort()
        {
            var result = await _productService.DesSorAsync();
            return Ok(result);
        }

        [HttpGet("Search")]
        public async Task<IActionResult> Search([FromQuery] SearchRequestDto model)
        {
            var result = await _productService.SearchAsync(model);
            return Ok(result);
        }
        [Authorize(Roles = "Admin")]

        [HttpPost]
        public async Task<IActionResult> Add(ProductAddRequestDto product)
        {
            await _productService.AddAsync(product);
            return Ok();
        }
        [HttpPut]
        public async Task<IActionResult> Edit([FromBody] Product product)
        {
            await _productService.EditAsync(product);
            return Ok();
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _productService.DeleteAsync(id);
            return Ok();
        }
    }
}